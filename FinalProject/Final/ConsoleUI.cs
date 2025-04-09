namespace Final;

using Spectre.Console;

public class ConsoleUI {
    FileSaver fileSaver;

    public ConsoleUI() {
        fileSaver = new FileSaver("recipes.txt");
    }

    public void Show() {
        string addAnotherIngredient;

        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What would you like to do?")
                .AddChoices(new[] {
                    "Add a Recipe", 
                    "Search for a Recipe", 
                    "Show All Recipes", 
                    "End"
                })
        );

        while (mode != "End") {
            if(mode == "Add a Recipe") {
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
                    
                    fileSaver.AppendLine($"Recipe: {recipe.Name}, Ingredients: {formattedIngredients}, Source: {recipe.SourceName}");

                    mode = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What would you like to do?")
                            .AddChoices(new[] {
                                "Add a Recipe", 
                                "Search for a Recipe", 
                                "Show All Recipes", 
                                "End"
                        })
                    );
                }

            if(mode == "Search for a Recipe") {
                    string searchTerm = AskForInput("Enter recipe name, source name, or ingredient to search: ");
                    string[] recipes = File.ReadAllLines(fileSaver.fileName);
                    bool found = false;
                    AnsiConsole.WriteLine("Search Results:");
                    foreach (var line in recipes) {
                        if (line.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) {
                            AnsiConsole.WriteLine(line);
                            found = true;
                        }
                    }
                    if (!found) {
                        AnsiConsole.WriteLine("No recipes found.");
                    }
                    mode = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What would you like to do?")
                            .AddChoices(new[] {
                                "Add a Recipe", 
                                "Search for a Recipe", 
                                "Show All Recipes", 
                                "End"
                            })
                    );
            }

            if(mode == "Show All Recipes") {

                    string[] recipes = File.ReadAllLines(fileSaver.fileName);
                    AnsiConsole.WriteLine("Recipes:");
                    foreach (var line in recipes) {
                        AnsiConsole.WriteLine(line);
                    }
                    mode = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .Title("What would you like to do?")
                            .AddChoices(new[] {
                                "Add a Recipe", 
                                "Search for a Recipe", 
                                "Show All Recipes", 
                                "End"
                            })
                    );
            } 
        }

        Console.Write("Program ended");
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}