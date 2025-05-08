using System.CommandLine;
using System.Text.RegularExpressions;

public class FileRenamer {

    static async Task<int> Main(string[] args) {
        var directoryArg = new Argument<DirectoryInfo>("directory", "The directory containing files to rename.");
        var patternArg = new Argument<string?>("pattern", () => @"(\s+)\(([^\)]+)\)", "Regex pattern to remove.");
        var dryRunOption = new Option<bool>("--dry-run", "Simulate the renaming without changing any files.");

        var rootCommand = new RootCommand("FileRenamer - remove unwanted patterns from filenames")
        {
            directoryArg,
            patternArg,
            dryRunOption
        };

        rootCommand.SetHandler((DirectoryInfo directory, string? pattern, bool dryRun) => {
            RenameFiles(directory.FullName, pattern ?? "", dryRun);
        }, directoryArg, patternArg, dryRunOption);

        return await rootCommand.InvokeAsync(args);
    }

    /// <summary>
    /// It takes a directory, finds all files in that directory and all subdirectories that match the
    /// regex pattern, and then renames them by removing the regex pattern (which is a date format)
    /// </summary>
    private static void RenameFiles(string directory, string pattern, bool isDryRun) {
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

            Console.WriteLine($"{(isDryRun ? "[DRY-RUN]" : "[RENAME]")} {fileName} -> {newFileName}");

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
