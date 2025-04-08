namespace Final;

using Spectre.Console;

public class ConsoleUI {
    FileSaver fileSaver;

    public ConsoleUI() {
        fileSaver = new FileSaver("recipes.txt");
    }

    public void Show() {
        string command; 

        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(new[] {
                    "Add a Recipe", 
                    "Search for a Recipe", 
                    "Show All Recipes", 
                    "Show Grocery List",
                    "End"
                })
        );

        if(mode == "Add a Recipe") {
            do {
                
                string recipeName = AskForInput("Enter recipe name: ");

                string sourceName = AskForInput("Enter source of recipe (name or online): ");

                fileSaver.AppendLine(recipeName + ":" + sourceName);

                command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices(new[] {
                            "Add a Recipe", 
                            "Search for a Recipe", 
                            "Show All Recipes", 
                            "Show Grocery List",
                            "End"
                    })
                );

            } while(command != "End");
        }

        Console.Write("Program ended");
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}