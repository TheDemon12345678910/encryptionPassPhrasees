using Spectre.Console;

namespace encryptionPassPhrase;

using System.IO;
using System;

public class ReadingFile
{
    public void readingFile()
    {
        Console.WriteLine("You have chosen to read an already existing file");

        Console.WriteLine(
            "So You want to Read a File i see.\nHere is a list of all the files, that you can choose to read from:");
        string path = @"C:\Users\aneho\#AllProjects\encryptionPassPhrase\EncryptedFiles";
        //string[] files = Directory.GetFiles(path);
        DirectoryInfo d = new DirectoryInfo(path); //Assuming Test is your Folder

        FileInfo[] files = d.GetFiles("*.txt"); //Getting Text files
        foreach (FileInfo file in files)
        {
            Console.WriteLine(file.Name);
            // Ask for the user's favorite fruit
            var fruit = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What's your [green]favorite fruit[/]?")
                    .PageSize(20)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[]
                    {
                        "Apple", "Apricot", "Avocado",
                        "Banana", "Blackcurrant", "Blueberry",
                        "Cherry", "Cloudberry", "Cocunut",
                    }));

// Echo the fruit back to the terminal
            AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
        }
    }
}