namespace Final;

public class Ingredient {
    public string Name { get; }

    public Ingredient(string name) {
        this.Name = name;
    }

    public override string ToString() {
        return this.Name;
    }
}

public class Recipe {
    public string Name { get; }
    public string SourceName { get; }
    public List<Ingredient> Ingredients { get; }

    public Recipe(string name, string sourceName, List<Ingredient> ingredients) {
        this.Name = name;
        this.SourceName = sourceName;
        this.Ingredients = ingredients;
    }
}

/*public class GroceryList {
    public string ItemName { get; }

    public GroceryList(string name) {
        this.ItemName = name;
    }
}*/