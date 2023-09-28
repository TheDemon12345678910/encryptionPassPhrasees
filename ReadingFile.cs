using System.Security.AccessControl;
using Spectre.Console;

namespace encryptionPassPhrase;

using System.IO;
using System;

public class ReadingFile
{
    readonly string fileDirectory = "..\\..\\..\\EncryptedFiles";
    
    public void readingFile(int key)
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
        AnsiConsole.WriteLine($"{option} is your chosen file to read!");
        Console.WriteLine("But first we need some identification. What is the shared key you will use to decrypt this file" +
                          "\n(It is: " + key + ")");
        int readKey = Convert.ToInt32(Console.ReadLine());
        int indexOfOption = readableFiles.IndexOf(option);
        readSpecificFile(indexOfOption, readKey);
    }

    private void readSpecificFile(int indexOfOption, int readKey)
    {
        // Specify the relative path to the SpecificObjects folder
        string relativePath = Path.Combine(fileDirectory);
        // List all .txt files in the SpecificObjects folder
        string[] txtFiles = Directory.GetFiles(relativePath, "*.txt");

        // To read a text file line by line
        if (File.Exists(txtFiles[indexOfOption]))
        {
            // Store each line in array of strings
            string[] lines = File.ReadAllLines(txtFiles[indexOfOption]);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Gray;
            foreach (string ln in lines)
            {
                string line = decrypt(ln, readKey);
                Console.Write(line + "\n");
            }
        }
        // Restore original colors  
        Console.ResetColor();
    }

    private string decrypt(string ln, int key)
    {
        //The cypher we produced is a caesar cypher.
        char[] alphabet =
        {
            'a', 'b', 'c', 'd', 'e',
            'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o',
            'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y',
            'z', ' '
        };
        //The encryption will happen with the key, being the number of letters we go in the alphabet
        //and a random generated "Salt", that we also keep in the file.
        string decryptedMessage = "";
        char[] lineArr = ln.ToLower().ToCharArray();
        //Encrypting the message
        foreach (char c in lineArr)
        {
            int index = Array.IndexOf(alphabet, c);
            int newIndex = index - key;
            if (0 > newIndex)
            {
                newIndex = newIndex + alphabet.Length;
            }

            char newChar = alphabet[newIndex];
            decryptedMessage = decryptedMessage + newChar;
        }
        return decryptedMessage;
    }

    private List<string> getAllFiles()
    {
        // Specify the relative path to the SpecificObjects folder
        string relativePath = Path.Combine(fileDirectory);
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