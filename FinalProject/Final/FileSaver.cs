namespace Final;

using System.IO;

public class FileSaver {
    public string fileName;

    public FileSaver(string fileName) {
        this.fileName = fileName;

        if (!File.Exists(fileName)) {
            File.Create(fileName).Close();
        }
    }

    public void AppendLine(string line) {
        File.AppendAllText(this.fileName, line + Environment.NewLine);
    }
}