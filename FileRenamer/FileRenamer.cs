using System.Text.RegularExpressions;

public class FileRenamer {

    /// <summary>
    /// It takes a directory, finds all files in that directory and all subdirectories that match the
    /// regex pattern, and then renames them by removing the regex pattern (which is a date format)
    /// </summary>
    public static void Main(string[] args) {
        if (args.Length < 1 || args.Length > 3) {
            Console.WriteLine("Usage: FileRenamer <directory> [pattern] [--dry-run]");
            Console.WriteLine(@"Default pattern: (\s+)\(([^\)]+)\)");
            return;
        }

        var directory = args[0];
        var pattern = args.Length == 2 ? args[1] : @"(\s+)\(([^\)]+)\)";
        var isDryRun = args.Any(a => a.Equals("--dry-run", StringComparison.OrdinalIgnoreCase));

        if (!Directory.Exists(directory)) {
            Console.WriteLine($"Directory does not exist: {directory}");
            return;
        }

        var reg = new Regex(pattern);

        Console.WriteLine($"Searching for files in: {directory}");
        Console.WriteLine($"Using pattern: {pattern}");
        Console.WriteLine($"Dry-run mode: {(isDryRun ? "ON" : "OFF")}");

        var fileNames = Directory.GetFiles(directory, "*", SearchOption.AllDirectories)
                                                .Where(path => reg.IsMatch(path))
                                                .ToList();

        if (fileNames.Count == 0) {
            Console.WriteLine("No matching files found.");
            return;
        }

        Console.WriteLine($"Found {fileNames.Count} matching files.");

        foreach (var path in fileNames) {
            var dir = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);
            var newFileName = reg.Replace(fileName, "");
            var newPath = Path.Combine(dir!, newFileName);

            if (path == newPath) {
                continue;
            }

            Console.WriteLine($"{(isDryRun ? "[DRY-RUN]" : "[RENAME]")} {fileName} → {newFileName}");

            if (!isDryRun) {
                try {
                    File.Move(path, newPath);
                } catch (Exception ex) {
                    Console.WriteLine($"Failed to rename {fileName}: {ex.Message}");
                }
            }
        }

        Console.WriteLine(isDryRun ? "Dry-run complete." : "File renaming complete.");
    }
}
