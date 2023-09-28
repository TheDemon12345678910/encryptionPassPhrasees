using System.Security.AccessControl;
using Spectre.Console;

namespace encryptionPassPhrase;

using System.IO;
using System;

public class ReadingFile
{
    public void readingFile()
    {
        Console.Clear();
        Console.WriteLine(
            "So You want to Read a File I see.\nHere is a list of all the files, that you can choose to read from:");

        //Get all the txt-files into this List
        List<string> readableFiles = getAllFiles();
        

        // Ask for the user's preferred file to read
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What's your [green]preferred file to read[/]?")
                .PageSize(20)
                .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                .AddChoices(
                    readableFiles
                ));

        // Echo the fruit back to the terminal
        AnsiConsole.WriteLine($"{option} is now your favorite!");
        int indexOfOption = readableFiles.IndexOf(option);
        readSpecificFile(indexOfOption);

    }

    private void readSpecificFile(int indexOfOption)
    {
        // Specify the relative path to the SpecificObjects folder
        string relativePath = Path.Combine("..\\..\\..\\EncryptedFiles");
        // List all .txt files in the SpecificObjects folder
        string[] txtFiles = Directory.GetFiles(relativePath, "*.txt");
        
        // To read a text file line by line
        if (File.Exists(txtFiles[indexOfOption])) {
            // Store each line in array of strings
            string[] lines = File.ReadAllLines(txtFiles[indexOfOption]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Gray;
            foreach(string ln in lines)
                Console.Write(ln + "\n");
        }
        // Restore original colors  
        Console.ResetColor();
    }

    private List<string> getAllFiles()
    {
        // Specify the relative path to the SpecificObjects folder
        string relativePath = Path.Combine("..\\..\\..\\EncryptedFiles");
        // List all .txt files in the SpecificObjects folder
        string[] txtFiles = Directory.GetFiles(relativePath, "*.txt");
        // Display the file names in the console
        List<string> readableFiles = new List<string>();
        foreach (var file in txtFiles)
        {
            var fileName = Path.GetFileName(file);
            readableFiles.Add(fileName);
        }
        return readableFiles;
    }
}