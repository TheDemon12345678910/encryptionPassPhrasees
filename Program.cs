// See https://aka.ms/new-console-template for more information

using encryptionPassPhrase;
using Spectre.Console;


Console.WriteLine("Hello there.\n it seems you have some secret stuff you want to either encrypt or decrypt");
//Getting an input from the user
Console.WriteLine("What would you like to do right now?\nA) Read a secret file\nB) Write a secret file");
string choice = Console.ReadLine();
ReadingFile readingFile = new ReadingFile();
EncryptingAndCreatingFile encryptingAndCreatingFile = new EncryptingAndCreatingFile();

// Ask for the user's, what page they would like to go to
var fruit = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("What's your [green]favorite fruit[/]?")
        .PageSize(10)
        .MoreChoicesText("[grey](You can allways come back, if you want to go back)[/]")
        .AddChoices(new[] {
            "A) Read a secret file", "B) Write a secret file"
        }));

// Echo the fruit back to the terminal
AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");
//Sending the user to the wanted page
if (choice != null)
{
    if (choice == "A")
    {        
        Console.WriteLine("You have chosen to read an already existing file");
        readingFile.readingFile();
    }

    if (choice == "B")
    {
        
        Console.WriteLine("You have chosen to write a new file");
        //encryptingAndCreatingFile.encryptingAndCreatingFile();
    }
}