namespace Final.Tests;

using Final;
using System.IO;
using Xunit;

public class FinalTests
{
    FileSaver fileSaver;
    string testFileName;

    public FinalTests() {
        testFileName = "test-doc.txt";
        fileSaver = new FileSaver(testFileName);
    }

    [Fact]
    public void Test_FileSaver_Append() {
        fileSaver.AppendLine("Hello, world!");
        var contentFromFile = File.ReadAllText(testFileName);
        Assert.Contains("Hello, world!" + Environment.NewLine, contentFromFile);
    }

    [Fact]
    public void Test_IfRecipeExists() {
        fileSaver.AppendLine("Recipe: Pancakes, Source: Grandma, Ingredients: Flour, Eggs");
        var contentFromFile = File.ReadAllText(testFileName);

        Assert.True(File.Exists(testFileName), "The test file was not created.");
        Assert.Contains("Pancakes", contentFromFile);
    }
}
