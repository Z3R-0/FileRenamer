# FileRenamer

Ever moved (back) from Linux to Windows, and had all your files on your external drive suddenly appended with some datestamp, like so:

![Image of a list of files with some random date and timestamp appended](img\ewwww.png)

Well I have, so that's what this little tool can solve. Simply plug the path to your problematic files into its arguments and boom:

![Example usage GIF of FileRenamer](img\much-better.gif) 

It can also rename using any custom regex pattern should you wish to do so, but be sure to test and dry-run the command before you alter your real data!

## Download

Grab the latest release from the [Releases page](https://github.com/Z3R-0/FileRenamer/releases).

## Usage

```sh
FileRenamer.exe "C:/path/to/files" "(\s+)\(([^\)]+)\)"