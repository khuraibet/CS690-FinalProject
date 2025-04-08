namespace Final.Tests;

using Final;

public class FileSaverTests
{
    FileSaver fileSaver;
    string testFileName;

    public FileSaverTests() {
        testFileName = "test-doc.txt";
        fileSaver = new FileSaver(testFileName);
    }

    [Fact]
    public void Test_FileSaver_Append() {
        fileSaver.AppendLine("Hello, world!");
        var contentFromFile = File.ReadAllText(testFileName);
        Assert.Equal("Hello, world!" + Environment.NewLine, contentFromFile);
    }
}