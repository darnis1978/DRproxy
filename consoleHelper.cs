namespace DRproxy.Helper;
public static class ConsoleHelper
{
    private static readonly String[] projects = {"pepco"};

    private static void ShowHelpMessage()
    {
        // Console.WriteLine(Figgle.FiggleFonts.Standard.Render("Hello, World!"));
        Console.Clear();
        Console.WriteLine("You have to run this app with following commands:");
        Console.WriteLine("DProxy.dll --project <PROJECT_NAME>");
        Console.WriteLine("where <PROJECT_NAME> define configuration for specific customer.");
        Console.WriteLine();
        Console.WriteLine("Currently we support following customers:");
        Console.WriteLine("pepco");

    }

    public static string? CheckArguments(string[] args)
    {
        if (args.Length > 1)
        {
            if (args[0].ToLower() == "--project" || args[0].ToLower() == "-project")
            {
                 if (projects.Any(item => item == args[1]) == true) 
                    return  args[1];
            }
        } 
        
        ShowHelpMessage();
        return null;
    }
}