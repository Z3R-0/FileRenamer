using System.Text.RegularExpressions;

public class FileRenamer {

    /// <summary>
    /// It takes a directory, finds all files in that directory and all subdirectories that match the
    /// regex pattern, and then renames them by removing the regex pattern (which is a date format)
    /// </summary>
    public static void Main(string[] args) {
        if (args.Length == 0 || args.Length > 2) {
            Console.WriteLine("Usage: FileRenamer <directory> [pattern]");
            Console.WriteLine(@"Default pattern: (\s+)\(([^\)]+)\)");
            return;
        }

        var directory = args[0];
        var pattern = args.Length == 2 ? args[1] : @"(\s+)\(([^\)]+)\)";
        var reg = new Regex(pattern);

        if (!Directory.Exists(directory)) {
            Console.WriteLine($"Directory does not exist: {directory}");
            return;
        }

        Console.WriteLine("Starting file rename...");

        var fileNames = Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
                                                .Where(path => reg.IsMatch(path))
                                                .ToList();

        foreach (var path in fileNames) {
            var dir = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var newPath = Path.Combine(dir, reg.Replace(fileName, ""));

            Console.WriteLine($"Found {fileName}, renaming to {newPath}");

            File.Move(path, newPath);
        }

        Console.WriteLine("Finished renaming files");
    }
}
