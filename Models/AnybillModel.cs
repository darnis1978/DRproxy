// <auto-generated />
//
// To parse this JSON data, add NuGet 'System.Text.Json' then do:
//
//    using AnybillModel;
//
//    var AnybillDTO = AnybillDTO.FromJson(jsonString);
#nullable enable
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8603

namespace AnybillModel
{
    using System;
    using System.Collections.Generic;

    using System.Text.Json;
    using System.Text.Json.Serialization;
    using System.Globalization;

    public partial class AnybillDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("storeId")]
        public string StoreId { get; set; }

        [JsonPropertyName("userIdentification")]
        public object UserIdentification { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("bill")]
        public Bill Bill { get; set; }
    }

    public partial class Bill
    {
        [JsonPropertyName("countryCode")]
        public object CountryCode { get; set; }

        [JsonPropertyName("id")]
        public object Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cashRegister")]
        public CashRegister CashRegister { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("head")]
        public Head Head { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("security")]
        public Security Security { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("misc")]
        public Misc Misc { get; set; }
    }

    public partial class CashRegister
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonPropertyName("number")]
        public object Number { get; set; }

        [JsonPropertyName("version")]
        public object Version { get; set; }
    }

    public partial class Data
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("fullAmountInclVat")]
        public double? FullAmountInclVat { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("paymentTypes")]
        public PaymentType[] PaymentTypes { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("vatAmounts")]
        public DataVatAmount[] VatAmounts { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("lines")]
        public Line[] Lines { get; set; }

        [JsonPropertyName("positionCount")]
        public object PositionCount { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public DataExtensionAnybill ExtensionAnybill { get; set; }
    }

    public partial class DataExtensionAnybill
    {
        [JsonPropertyName("discounts")]
        public object Discounts { get; set; }

        [JsonPropertyName("fullAmountInclVatBeforeDiscounts")]
        public object FullAmountInclVatBeforeDiscounts { get; set; }

        [JsonPropertyName("tip")]
        public object Tip { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("disableVatAmountsValidation")]
        public bool? DisableVatAmountsValidation { get; set; }

        [JsonPropertyName("equivalentValueName")]
        public object EquivalentValueName { get; set; }

        [JsonPropertyName("fullEquivalentValue")]
        public object FullEquivalentValue { get; set; }
    }

    public partial class Line
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("additionalText")]
        public string AdditionalText { get; set; }

        [JsonPropertyName("vatAmounts")]
        public LineVatAmount[] VatAmounts { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("fullAmountInclVat")]
        public double? FullAmountInclVat { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("item")]
        public Item Item { get; set; }

        [JsonPropertyName("deliveryPeriodStart")]
        public object DeliveryPeriodStart { get; set; }

        [JsonPropertyName("deliveryPeriodEnd")]
        public object DeliveryPeriodEnd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public LineExtensionAnybill ExtensionAnybill { get; set; }

        [JsonPropertyName("relatedLines")]
        public object RelatedLines { get; set; }
    }

    public partial class LineExtensionAnybill
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sequenceNumber")]
        public long? SequenceNumber { get; set; }

        [JsonPropertyName("fullAmountInclVatBeforeDiscounts")]
        public object FullAmountInclVatBeforeDiscounts { get; set; }

        [JsonPropertyName("discounts")]
        public object Discounts { get; set; }

        [JsonPropertyName("categoryId")]
        public object CategoryId { get; set; }

        [JsonPropertyName("returnBarcodeReference")]
        public object ReturnBarcodeReference { get; set; }

        [JsonPropertyName("equivalentValue")]
        public object EquivalentValue { get; set; }

        [JsonPropertyName("isReturn")]
        public object IsReturn { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("sublines")]
        public object Sublines { get; set; }
    }

    public partial class Item
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Number { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("quantity")]
        public long? Quantity { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("quantityMeasure")]
        public string QuantityMeasure { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pricePerUnit")]
        public double? PricePerUnit { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public ItemExtensionAnybill ExtensionAnybill { get; set; }
    }

    public partial class ItemExtensionAnybill
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("gtin")]
        public string Gtin { get; set; }

        [JsonPropertyName("serialNumber")]
        public object SerialNumber { get; set; }

        [JsonPropertyName("plu")]
        public object Plu { get; set; }

        [JsonPropertyName("pricePerUnitBeforeDiscounts")]
        public object PricePerUnitBeforeDiscounts { get; set; }

        [JsonPropertyName("warranty")]
        public object Warranty { get; set; }
    }

    public partial class LineVatAmount
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("percentage")]
        public long? Percentage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("inclVat")]
        public double? InclVat { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("exclVat")]
        public double? ExclVat { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("vat")]
        public double? Vat { get; set; }

        [JsonPropertyName("groupId")]
        public object GroupId { get; set; }
    }

    public partial class PaymentType
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }

        [JsonPropertyName("foreignAmount")]
        public object ForeignAmount { get; set; }

        [JsonPropertyName("foreignCurrency")]
        public object ForeignCurrency { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public PaymentTypeExtensionAnybill ExtensionAnybill { get; set; }
    }

    public partial class PaymentTypeExtensionAnybill
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("conversionFactor")]
        public object ConversionFactor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("paymentDetails")]
        public PaymentDetails PaymentDetails { get; set; }

        [JsonPropertyName("equivalentValue")]
        public object EquivalentValue { get; set; }

        [JsonPropertyName("remainingAmount")]
        public object RemainingAmount { get; set; }
    }

    public partial class PaymentDetails
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cardNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CardNumber { get; set; }

        [JsonPropertyName("bankName")]
        public object BankName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("terminalId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? TerminalId { get; set; }

        [JsonPropertyName("terminalDateTime")]
        public object TerminalDateTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("terminalTime")]
        public DateTimeOffset? TerminalTime { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("terminalDate")]
        public string TerminalDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("traceNumber")]
        public long? TraceNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cardPan")]
        public string CardPan { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cardSequenceNumber")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? CardSequenceNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cardExpiryDate")]
        public string CardExpiryDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("amountGiven")]
        public long? AmountGiven { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("amountReturned")]
        public double? AmountReturned { get; set; }
    }

    public partial class DataVatAmount
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("percentage")]
        public long? Percentage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("inclVat")]
        public double? InclVat { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("exclVat")]
        public double? ExclVat { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("vat")]
        public double? Vat { get; set; }

        [JsonPropertyName("groupId")]
        public object GroupId { get; set; }

        [JsonPropertyName("groupName")]
        public object GroupName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("isHidden")]
        public bool? IsHidden { get; set; }
    }

    public partial class Head
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Number { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("date")]
        public DateTimeOffset? Date { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("deliveryPeriodStart")]
        public DateTimeOffset? DeliveryPeriodStart { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("deliveryPeriodEnd")]
        public DateTimeOffset? DeliveryPeriodEnd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("seller")]
        public Seller Seller { get; set; }

        [JsonPropertyName("buyerText")]
        public object BuyerText { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("buyer")]
        public Buyer Buyer { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public HeadExtensionAnybill ExtensionAnybill { get; set; }
    }

    public partial class Buyer
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("customerNumber")]
        public string CustomerNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("taxNumber")]
        public string TaxNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("eMailAddress")]
        public object EMailAddress { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sendReceiptByMail")]
        public bool? SendReceiptByMail { get; set; }
    }

    public partial class Address
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("postalCode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? PostalCode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }
    }

    public partial class HeadExtensionAnybill
    {
        [JsonPropertyName("additionalHeaderInformation")]
        public object AdditionalHeaderInformation { get; set; }
    }

    public partial class Seller
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("vatId")]
        public object VatId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("taxNumber")]
        public string TaxNumber { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("taxExemption")]
        public bool? TaxExemption { get; set; }

        [JsonPropertyName("taxExemptionNote")]
        public object TaxExemptionNote { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("address")]
        public Address Address { get; set; }
    }

    public partial class Misc
    {
        [JsonPropertyName("logo")]
        public object Logo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("footerText")]
        public string FooterText { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("additionalReceipts")]
        public AdditionalReceipt[] AdditionalReceipts { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public MiscExtensionAnybill ExtensionAnybill { get; set; }
    }

    public partial class AdditionalReceipt
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("isPrimaryReceipt")]
        public bool? IsPrimaryReceipt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("isPrintBuffer")]
        public bool? IsPrintBuffer { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("contentType")]
        public string ContentType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public partial class MiscExtensionAnybill
    {
        [JsonPropertyName("receiptLanguage")]
        public object ReceiptLanguage { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("isExample")]
        public bool? IsExample { get; set; }

        [JsonPropertyName("isInvoice")]
        public object IsInvoice { get; set; }

        [JsonPropertyName("isHospitalityBill")]
        public object IsHospitalityBill { get; set; }

        [JsonPropertyName("isCopy")]
        public object IsCopy { get; set; }

        [JsonPropertyName("categories")]
        public object Categories { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("afterSalesCoupons")]
        public AfterSalesCoupon[] AfterSalesCoupons { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("returnBarcode")]
        public string ReturnBarcode { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("returnBarcodeType")]
        public string ReturnBarcodeType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cashierName")]
        public string CashierName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("customSections")]
        public CustomSection[] CustomSections { get; set; }

        [JsonPropertyName("countrySpecificAttributes")]
        public object CountrySpecificAttributes { get; set; }

        [JsonPropertyName("receiptTransactionType")]
        public object ReceiptTransactionType { get; set; }
    }

    public partial class AfterSalesCoupon
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("codeType")]
        public string CodeType { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("redeemStartDate")]
        public DateTimeOffset? RedeemStartDate { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("redeemEndDate")]
        public DateTimeOffset? RedeemEndDate { get; set; }
    }

    public partial class CustomSection
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("position")]
        public string Position { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("section")]
        public string Section { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("data")]
        public Datum[] Data { get; set; }
    }

    public partial class Datum
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sequenceNumber")]
        public long? SequenceNumber { get; set; }

        [JsonPropertyName("customSectionId")]
        public object CustomSectionId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public partial class Security
    {
        [JsonPropertyName("fiscalization")]
        public object Fiscalization { get; set; }

        [JsonPropertyName("tse")]
        public object Tse { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("extension:anybill")]
        public SecurityExtensionAnybill ExtensionAnybill { get; set; }
    }

    public partial class SecurityExtensionAnybill
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("failure")]
        public bool? Failure { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("tseFailure")]
        public bool? TseFailure { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("required")]
        public bool? ExtensionAnybillRequired { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("tseRequired")]
        public bool? TseRequired { get; set; }

        [JsonPropertyName("publicKey")]
        public object PublicKey { get; set; }
    }

    public partial class AnybillDTO
    {
        public static AnybillDTO FromJson(string json) => JsonSerializer.Deserialize<AnybillDTO>(json, AnybillModel.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AnybillDTO self) => JsonSerializer.Serialize(self, AnybillModel.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerOptions Settings = new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                new DateOnlyConverter(),
                new TimeOnlyConverter(),
                IsoDateTimeOffsetConverter.Singleton
            },
        };
    }

    internal class ParseStringConverter : JsonConverter<long>
    {
        public override bool CanConvert(Type t) => t == typeof(long);

        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToString(), options);
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
    
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private readonly string serializationFormat;
        public DateOnlyConverter() : this(null) { }

        public DateOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "yyyy-MM-dd";
        }

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    public class TimeOnlyConverter : JsonConverter<TimeOnly>
    {
        private readonly string serializationFormat;

        public TimeOnlyConverter() : this(null) { }

        public TimeOnlyConverter(string? serializationFormat)
        {
            this.serializationFormat = serializationFormat ?? "HH:mm:ss.fff";
        }

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString(serializationFormat));
    }

    internal class IsoDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override bool CanConvert(Type t) => t == typeof(DateTimeOffset);

        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private string? _dateTimeFormat;
        private CultureInfo? _culture;

        public DateTimeStyles DateTimeStyles
        {
            get => _dateTimeStyles;
            set => _dateTimeStyles = value;
        }

        public string? DateTimeFormat
        {
            get => _dateTimeFormat ?? string.Empty;
            set => _dateTimeFormat = (string.IsNullOrEmpty(value)) ? null : value;
        }

        public CultureInfo Culture
        {
            get => _culture ?? CultureInfo.CurrentCulture;
            set => _culture = value;
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            string text;


            if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
                || (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
            {
                value = value.ToUniversalTime();
            }

            text = value.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);

            writer.WriteStringValue(text);
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? dateText = reader.GetString();

            if (string.IsNullOrEmpty(dateText) == false)
            {
                if (!string.IsNullOrEmpty(_dateTimeFormat))
                {
                    return DateTimeOffset.ParseExact(dateText, _dateTimeFormat, Culture, _dateTimeStyles);
                }
                else
                {
                    return DateTimeOffset.Parse(dateText, Culture, _dateTimeStyles);
                }
            }
            else
            {
                return default(DateTimeOffset);
            }
        }


        public static readonly IsoDateTimeOffsetConverter Singleton = new IsoDateTimeOffsetConverter();
    }
}
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8603
