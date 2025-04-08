namespace Final;

public class ConsoleUI {
    FileSaver fileSaver;

    public ConsoleUI() {
        fileSaver = new FileSaver("recipes.txt");
    }

    public void Show() {
        string command; 

        do {
            
            string recipeName = AskForInput("Enter recipe name: ");

            string sourceName = AskForInput("Enter source of recipe (name or online): ");

            fileSaver.AppendLine(recipeName + ":" + sourceName);

            command = AskForInput("Enter command (end or continue): ");

        } while(command != "end");

        Console.Write("Program ended");
    }

    public static string AskForInput(string message) {
        Console.Write(message);
        return Console.ReadLine();
    }
}