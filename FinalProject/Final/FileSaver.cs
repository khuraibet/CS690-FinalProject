namespace Final;

using System.IO;

public class FileSaver {
    public string fileName;

    public FileSaver(string fileName) {
        this.fileName = fileName;
        File.Create(this.fileName).Close();
    }

    public void AppendLine(string line) {
        File.AppendAllText(this.fileName, line + Environment.NewLine);
    }
}