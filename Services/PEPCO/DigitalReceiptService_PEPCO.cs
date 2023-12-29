using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using DRproxy.Hubs;
using FiscalModel;
using System.Text.Json.Nodes;
using anybill.POS.Client.Exceptions;
using anybill.POS.Client.Factories;
using anybill.POS.Client.Models.Bill;
using anybill.POS.Client.Models.Bill.Data.PaymentType;
using anybill.POS.Client.Models.Bill.Data.VatAmount;
using anybill.POS.Client.Models.Bill.Data.Line;
using anybill.POS.Client.Models.Bill.Data.Line.VatAmount;
using anybill.POS.Client.Models.Bill.Misc;
using anybill.POS.Client.Models.Bill.Misc.Extension;
using anybill.POS.Client.Models.Bill.Response;
using anybill.POS.Client.Models.Bill.Security;
using anybill.POS.Client.Models.Bill.Security.Extension;
using anybill.POS.Client.Options;
using anybill.POS.Client;
using anybill.POS.Client.Models.Bill.Data;
using System.Globalization;
using anybill.POS.Client.Models.Bill.Data.PaymentType.Extension.PaymentDetails;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using DRproxy.Responses.PEPCO;
using anybill.POS.Client.Models.Bill.Data.Line.Extension;
using anybill.POS.Client.Models.Bill.Data.Extension.Discount;
using anybill.POS.Client.Models.Bill.Data.Extension.Discount.VatAmount;

namespace DRproxy.Services.PEPCO;

public class DigitalReceiptService_PEPCO: DigitalReceiptService
{    
    private readonly IMemeoryStorageService _connectionsPool;
    private readonly IHubContext <MessageHub> _messageHub;   
    private readonly ILogger<DigitalReceiptService_PEPCO> _logger;   
    private readonly string _currency;   
    private readonly string [] _payments;

    public DigitalReceiptService_PEPCO(
                                IHubContext < MessageHub > messageHub, 
                                IMemeoryStorageService connectionPool,
                                ILogger<DigitalReceiptService_PEPCO> logger
                                ){

        _connectionsPool = connectionPool;
        _messageHub = messageHub;
        _logger = logger;

        // Set credentials 
        User = "diebold-nixdorf-stg";
        Password  = "kYIm$$~\"_Gk94,";
        Client_id = "e2db9b99-866f-41b8-b148-bf501dc11d4a";
        MerchantId  = "h2hXN6kN2B";

        // set bill settings
        _currency = "PLN";   // according to ISO 4217
        _payments = new string[] {  // 1 - Gotówka, 2 - Karta, 3 - Czek, 4 - Bon, 5 - Kredyt, 6 - Inna, 7 - Voucher, 8 - Przelew
            "",
            "Gotówka",
            "Karta", 
            "Czek", 
            "Bon", 
            "Kredyt", 
            "Inna", 
            "Voucher", 
            "Przelew"
        };
   
        // Create Anybill service
        CreateService();   
    }

    private PaymentTypeInformation CreatedPayment(Payment fiscalPayment) {

        PaymentTypeInformation payment = new() {
            Amount = (decimal)fiscalPayment.Amount/100,
            Name = _payments[fiscalPayment.Type],
        };
 
        switch (fiscalPayment.Type) {

            case 1:
                payment.Extension = new() {  
                    Type = anybill.POS.Client.Models.Bill.Data.PaymentType.Extension.PaymentType.Cash,
                };
            break;

            case 2:
                payment.Extension = new() {    
                    Type = anybill.POS.Client.Models.Bill.Data.PaymentType.Extension.PaymentType.OnlinePayment,
                };
            break;

            case 3:
                payment.Extension = new() {    
                    Type = anybill.POS.Client.Models.Bill.Data.PaymentType.Extension.PaymentType.OnlinePayment,
                };
            break;

        }

       return payment;
    }

    public override async Task<ActionResult>ProcessTransaction(JsonObject json) {

        FiscalDTO fiscalDTO;
        string clientId;
        string connectionId; 

 
        try {

            // create token
            await CreateToken();

            if (json == null)
                throw new Exception( message: $"_connectionsPool or _messageHub object is empty" );

            // check main objects
            if (_connectionsPool == null || _messageHub == null)
                throw new Exception( message: $"_connectionsPool or _messageHub object is empty" );

            // parse request body to FiscalDTO model
            fiscalDTO = FiscalDTO.FromJson(json.ToJsonString());

            // get client id (POS number) from fiscalDTO     
            clientId = fiscalDTO.Body.OfType<Body>().First(item => item.FiscalFooter !=null).FiscalFooter.CashNumber;

            // get connection id for this client id from connections pool
            connectionId = _connectionsPool.GetConnectiontId(clientId);
            // if (connectionId.Length == 0)
            //     throw new Exception( message: $"there is no connectionId for clientId: {clientId}" );

            // _logger.LogInformation("Got request from client,  [POS: " + clientId + "] [Connection ID: " + connectionId + "]" );


            // Process payload - fiscal receipt
            var payments = new List<PaymentTypeInformation>();
            var vats = new List<DataVatAmount>();
            var lines = new List<LineBase>();
            var billDiscounts = new List<BillDiscount>();
            var sequenceNmbr = 0;   
            decimal totalValue = 0;
            decimal totalDiscountValue = 0;

       
            
            // Collect summary VAT information
            List<VatRatesSummary> vatDetails = fiscalDTO.Body.OfType<Body>().First(item => item.VatSummary !=null).VatSummary.VatRatesSummary.ToList();       
            FiscalFooter fiscalFooter =  fiscalDTO.Body.OfType<Body>().First(item => item.FiscalFooter !=null).FiscalFooter;       

            _logger.LogInformation("New transaction to process: " + fiscalFooter.BillNumber);


            // Collect and prepare all items information and payment information
            foreach (var fiscalLine in fiscalDTO.Body)
            {
                // Line Information
                if (fiscalLine.SellLine != null && fiscalLine.SellLine.IsStorno == false) 
                {
                    decimal itemPrice = (decimal)fiscalLine.SellLine.Price/100;

                    DefaultLine defultLine = new();           
                    // item name
                    defultLine.Text = fiscalLine.SellLine.Name;  
                    // additional desc
                    defultLine.AdditionalText = fiscalLine.SellLine.Desc;
                    // item price
                    defultLine.FullAmountInclVat = itemPrice;
                    // main item information
                    defultLine.Item = new();
                    // ToDo: there is not item number in the fiscal payload
                    defultLine.Item.Number = fiscalLine.SellLine.Name;
                    defultLine.Item.PricePerUnit = itemPrice;
                    defultLine.Item.Quantity = Decimal.Parse(fiscalLine.SellLine.Quantity); 

                    // to count FullAmountInclVatBeforeDiscounts
                    totalValue += defultLine.Item.PricePerUnit;

                    // item VAT information
                    defultLine.VatAmounts = new();
                    LineVatAmount lineVat = new();
                    var VATid = fiscalLine.SellLine.VatId;
                    VatRatesSummary vatDetail = vatDetails.First(item=>item.VatId == VATid);    
                    lineVat.GroupId = vatDetail.VatId.ToString();
                    lineVat.Percentage =  (decimal)vatDetail.VatRate/100;
                    defultLine.VatAmounts.Add(lineVat);
                    // ToDo: there is no information about VAT per line - need to ne counted manually = we need to avoid this.
                    
                    // extension information
                    defultLine.Extension = new()
                    {
                        FullAmountInclVatBeforeDiscounts = itemPrice,
                        SequenceNumber = sequenceNmbr,
                        Discounts = new(),
                    };

                    lines.Add(defultLine);

                };

                // Discount Line information
                if (fiscalLine.DiscountLine != null)
                {
                    // take the last added line - should be SaleLine or DiscountLine
                    DefaultLine defultLine = (DefaultLine)lines.Last();
                    DefaultLineDiscount discount = new() {
                        SequenceNumber = defultLine.Extension.Discounts.Count,
                        DiscountId = fiscalLine.DiscountLine.Name + fiscalLine.DiscountLine.Value.ToString(),
                        FullAmountInclVat = (decimal)fiscalLine.DiscountLine.Value/100,         
                    };

                    // update line information about disc info
                    defultLine.Extension.Discounts.Add(discount);
                    
                    // add disc info to summary disc object
                    BillDiscount billDisc = new BillDiscount();
                    billDisc.Id = fiscalLine.DiscountLine.Name + (fiscalLine.DiscountLine.Value).ToString();
                    billDisc.Name =  "OPUST " + fiscalLine.DiscountLine.Name;
                    billDisc.Value = (decimal)fiscalLine.DiscountLine.Value/100;             
                    if (fiscalLine.DiscountLine.IsPercent)
                        billDisc.Type = BillDiscountType.Percentage;
                    else 
                        billDisc.Type = BillDiscountType.Monetary;
              
                    billDiscounts.Add(billDisc);
                    
                    // to count FullAmountInclVat
                    totalDiscountValue += (decimal)fiscalLine.DiscountLine.Value/100;            
                }

                 // Discount Total information
                if (fiscalLine.DiscountVat != null)
                {         
                     // take the last added line - should be SaleLine or DiscountLine
                    DefaultLine defultLine = (DefaultLine)lines.Last();
                    
                    DefaultLineDiscount discount = new() {
                        SequenceNumber = defultLine.Extension.Discounts.Count,
                        DiscountId = fiscalLine.DiscountVat.Name + fiscalLine.DiscountVat.Value.ToString(),
                        FullAmountInclVat = (decimal)fiscalLine.DiscountVat.Value/100,         
                    };
                    
                     // update line information about disc info
                    defultLine.Extension.Discounts.Add(discount);
                    
                    BillDiscount billDisc = new BillDiscount();
                    billDisc.Id = fiscalLine.DiscountVat.Name + (fiscalLine.DiscountVat.Value).ToString();
                    billDisc.AdditionalText = "OPUST";
                    billDisc.Name =  "OPUST " + fiscalLine.DiscountVat.Name;
                    billDisc.Value = (decimal)fiscalLine.DiscountVat.Value/100;     
                    billDisc.FullAmountInclVat =   (decimal)fiscalLine.DiscountVat.Value/100;     
                    if (fiscalLine.DiscountVat.IsPercent)
                        billDisc.Type = BillDiscountType.Percentage;
                    else 
                        billDisc.Type = BillDiscountType.Monetary;


                    VatRatesSummary vatDetail = vatDetails.First(item=>item.VatId == fiscalLine.DiscountVat.VatId);   
                    BillDiscountVatAmount vat = new() {
                        GroupId = vatDetail.VatId.ToString(),
                        Percentage = (decimal)vatDetail.VatRate / 100,
                        InclVat = (decimal)vatDetail.VatSale / 100,
                        Vat = (decimal)vatDetail.VatAmount / 100,
                        ExclVat =  ((decimal)vatDetail.VatSale / 100) - ((decimal)vatDetail.VatAmount / 100),
                    };
       
                    
                    List<BillDiscountVatAmount> vatAmounts = new()
                    {
                        vat
                    };
                    billDisc.VatAmounts= vatAmounts;


                    // add disc info to summary disc object
                    billDiscounts.Add(billDisc);
                    totalDiscountValue += (decimal)fiscalLine.DiscountVat.Value/100;
                   
                }

                 // Payments information
                if (fiscalLine.Payment != null)
                {
                    PaymentTypeInformation payment = CreatedPayment(fiscalLine.Payment);
                    payments.Add(payment);
                }

            };

            // Collect and prepare VAT summary information           
            foreach (var vat in vatDetails) 
            {
                DataVatAmount vatInfo = new()
                {
                    GroupId = vat.VatId.ToString(),
                    Percentage = (decimal)vat.VatRate / 100,
                    InclVat = (decimal)vat.VatSale / 100,
                    Vat = (decimal)vat.VatAmount / 100,
                };
                vatInfo.ExclVat = vatInfo.InclVat - vatInfo.Vat;
               
                vats.Add(vatInfo);
            }

            AddBill bill = new()
            {
                StoreId = MerchantId,
                Bill = new()
                {
                    CashRegister = new()
                    {
                        Number = clientId,
                    },
                    Head = new() {
                        Date = fiscalFooter.Date,
                    },
                    Data = new() {
                        Currency = _currency,
                        FullAmountInclVat = totalValue - totalDiscountValue,
                        Extension = new () {
                            Discounts = billDiscounts,
                            FullAmountInclVatBeforeDiscounts = totalValue,
                        }
                    },
                    Security = new Security()
                    {
                        Extension = new AnybillSecurityExtension()
                        {
                            Failure = true,
                        }
                    },
                    Misc = new Misc()
                    {
                        Extension = new AnybillMiscExtension()
                            {
                                IsExample = true,
                                IsInvoice = false,
                            }
                        }
                    },
            };
            
            bill.Bill.Data.Lines = lines;
            bill.Bill.Data.VatAmounts = vats;
            bill.Bill.Data.PaymentTypes = payments;    

            var billOptions = new AddBillOptions();

            // 3. add Bill
            _logger.LogInformation("Send bill to Digital Receipt server....." );
            var billResponse = await AnybillClient.Bill.CreateAsync(bill, billOptions);
            switch (billResponse)
            {
                case IExternalIdResponse externalIdResponse:
                    _logger.LogInformation("IsAssigned: " + externalIdResponse.IsAssigned);
                    break;
                case ILoyaltyCardResponse loyaltyCardResponse:
                    _logger.LogInformation("IsAssigned: " + loyaltyCardResponse.IsAssigned);
                    break;
                case IMatchedBillResponse matchedBillResponse:
                    _logger.LogInformation("IsAssigned: " + matchedBillResponse.IsAssigned);
                    break;
                case IUrlBillResponse urlBillResponse:
                    _logger.LogInformation("GetMy website: " + urlBillResponse.Url);
                    break;
                case IUserIdResponse userIdResponse:
                    _logger.LogInformation("IsAssigned: " + userIdResponse.IsAssigned);
                    break;
            }
                

            // ----------------------------------------------------------
            // Prepare and send response to connected client via socket
            // _logger.LogInformation("send ClientResponse - [POS: " + clientId.ToString() + "] [Connection ID: " + connectionId + "]" );
            // var clientResponse = new ClientResponse() { 
            //     ClientId = clientId.ToString(), 
            //     QRcode = clientId.ToString() + "|" + connectionId
            // };         
            // await _messageHub.Clients.Client(connectionId).SendAsync("ReceiveMessage", JsonSerializer.Serialize<ClientResponse>(clientResponse));



            // ----------------------------------------------------------
            // Prepare and send response to fiscal printer / WS client
            Response response = new ()
            {
                ErrorCode=null, 
                UID= "billResponse",
            };
            // string jobj = JsonSerializer.Serialize(response);
            // return new OkObjectResult(null);
            return new NoContentResult();

        } 
        catch (AuthenticationException authenticationException)
        {
            // Authentication failed because username, password or clientId is wrong.
            _logger.LogError(authenticationException.ErrorDescription);
            return new OkObjectResult(null);
        }
        catch (BadRequestException badRequestException)
        {
            // Request failed. e.g. validation error.
            _logger.LogError(badRequestException.Message);
            return new OkObjectResult(null);
        }
        catch (ForbiddenException forbiddenException)
        {
            // Request failed because user is not allowed to use the called endpoint.
            _logger.LogError(forbiddenException.Message);
            return new OkObjectResult(null);
        }
        catch (NotFoundException notFoundException)
        {
            // Request failed because called endpoint does not exists.
            _logger.LogError(notFoundException.Message);
            return new OkObjectResult(null);
        }
        catch (UnauthorizedException unauthorizedException)
        {
            // Request failed because user is not authorized.
            _logger.LogError(unauthorizedException.Message);
            return new OkObjectResult(null);
        }
        catch (UnhandledException unhandledException)
        {
            // Request failed for some reason.
            _logger.LogError(unhandledException.Message);
            return new OkObjectResult(null);
        }
        catch(Exception e) {
            _logger.LogError(e.Message);
            return new OkObjectResult(null);
        }

    }

}
