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

        string addAnotherIngredient;

        if(mode == "Add a Recipe") {
            do {
                string recipeName = AskForInput("Enter recipe name: ");
                string sourceName = AskForInput("Enter source of recipe (name or online): ");
                List<Ingredient> ingredients = new List<Ingredient>();

                Recipe recipe = new Recipe(recipeName, sourceName, ingredients);

                do {
                    recipe.Ingredients.Add(new Ingredient(AskForInput("Enter an ingredient: ")));
                    addAnotherIngredient = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What would you like to do?")
                            .AddChoices(new[] {
                                "Add another",
                                "That's all"
                            })
                    );
                } while(addAnotherIngredient != "That's all");
                
                string formattedIngredients = string.Join(", ", recipe.Ingredients.Select(i => i.Name));
                fileSaver.AppendLine($"Recipe: {recipe.Name}, Source: {recipe.SourceName}, Ingredients: {formattedIngredients}");

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

        // Need to add functionality for other commands

        Console.Write("Program ended");
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}