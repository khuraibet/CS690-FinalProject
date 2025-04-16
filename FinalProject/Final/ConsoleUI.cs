namespace Final;

using Spectre.Console;

public class ConsoleUI {
    private FileSaver fileSaver;
    private List<Recipe> recipes;

    public ConsoleUI() {
        fileSaver = new FileSaver("recipes.txt");
        recipes = new List<Recipe>();
        ReloadRecipesFromFile();
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
                    string recipeName;
                    
                    do {
                        recipeName = AskForInput("Enter recipe name: ");
                        if (recipes.Any(recipe => recipe.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase))) {
                            AnsiConsole.WriteLine("Recipe already exists. Please enter a different name.");
                         } 
                    } while(recipes.Any(recipe => recipe.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase)));

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
                    
                    recipes.Add(recipe);

                    AnsiConsole.WriteLine($"Recipe '{recipe.Name}' added successfully!");
                    
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
                if(recipes.Count == 0) {
                    AnsiConsole.WriteLine("No recipes found");
                } else {
                    foreach (var recipe in recipes) {
                        string formattedIngredients = string.Join(", ", recipe.Ingredients.Select(i => i.Name));
                        AnsiConsole.MarkupLine($"Recipe: {recipe.Name}, Source: {recipe.SourceName}, Ingredients: {formattedIngredients}");
                    }
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

    private void ReloadRecipesFromFile() {
        recipes.Clear();
        if (File.Exists(fileSaver.fileName)) {
            var lines = File.ReadAllLines(fileSaver.fileName);
            foreach (var line in lines) {
                var parts = line.Split(", Ingredients: ");
                if (parts.Length == 2) {
                    var nameAndSource = parts[0].Replace("Recipe: ", "").Split(", Source: ");
                    if (nameAndSource.Length == 2) {
                        var recipeName = nameAndSource[0];
                        var sourceName = nameAndSource[1];
                        var ingredients = parts[1].Split(", ").Select(i => new Ingredient(i)).ToList();
                        recipes.Add(new Recipe(recipeName, sourceName, ingredients));
                    }
                }
            }
        }
    }
}