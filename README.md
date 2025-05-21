![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet&logoColor=white)

# FileRenamer

Ever moved (back) from Linux to Windows, and had all your files on your external drive suddenly appended with some datestamp, like so:

![Image of files with datestamps appended to them](https://github.com/user-attachments/assets/b5b71fd5-cdaf-42f6-9dc5-0a7b20f7c4d7)

Well I have, so that's what this little tool can solve. Simply plug the path to your problematic files into its arguments and boom:

![Example usage GIF showcasing FileRenamer in action](https://github.com/user-attachments/assets/fd65058e-73b2-4e6f-86db-b3534f22aa0c)

It can also rename using any custom regex pattern should you wish to do so. 

**Be sure to test and dry-run the command before you alter your actual data!** -> Use the --dry-run flag to simulate renaming before making changes.

## Download

Grab the latest release from the [Releases page](https://github.com/Z3R-0/FileRenamer/releases).

## Usage
**Be sure to prefix with .\ if you run this in Powershell like in the example GIF**

```sh
FileRenamer.exe "C:/path/to/files" "(\s+)\(([^\)]+)\)" --dry-run
```
The only required parameter is the file path, eveything else is optional. 

Use the --help flag for more info.

```sh
FileRenamer.exe --help
```

### Dry run example

Add --dry-run to preview what the tool would rename — without actually touching your files.

This shows you exactly what would be renamed, so you can safely test custom patterns or check behavior before making real changes.

Dry-run mode (safe):
```sh
FileRenamer.exe "C:/TestFolder" --dry-run
```
Custom pattern + dry-run:
```sh
FileRenamer.exe "C:/TestFolder" "(_backup\d{4})" --dry-run
```
Real rename (careful!):
```sh
FileRenamer.exe "C:/TestFolder"
```
