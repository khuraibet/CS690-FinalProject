namespace Final;

class Program
{
    static void Main(string[] args)
    {
        string command; 

        do {
            
            Console.Write("Enter recipe name: ");
            string recipeName = Console.ReadLine();

            Console.Write("Enter source of recipe (name or online): ");
            string sourceName = Console.ReadLine();

            File.AppendAllText("recipes.txt", recipeName + ":" + sourceName + Environment.NewLine);

            Console.Write("Enter command (end or continue): ");
            command = Console.ReadLine();
        } while(command != "end");

        Console.Write("Program ended");
    }
}
