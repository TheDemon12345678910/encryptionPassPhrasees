// See https://aka.ms/new-console-template for more information

using encryptionPassPhrase;
using Spectre.Console;


Console.WriteLine("Hello there.\nIt seems you have some secret stuff you want to either encrypt or decrypt");
//Getting an input from the user
Console.WriteLine("What would you like to do right now?");

ReadingFile readingFile = new ReadingFile();
EncryptingAndCreatingFile encryptingAndCreatingFile = new EncryptingAndCreatingFile();

// Ask for the user's, what page they would like to go to
string[] choices =
{
    "A) Read a secret file",
    "B) Write a secret file"
};
//Send the user to the wanted page
desission();


void desission()
{
    var desicion = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("What would you like [green]to do[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](To get more options, use up and down arrows)[/]")
            .AddChoices(choices));

    if (desicion == choices[0])
    {
        readingFile.readingFile();
    }
    else if (desicion == choices[1])
    {
        //encryptingAndCreatingFile.encryptingAndCreatingFile();
    }
    else
    {
        Console.WriteLine("That is not a valid choice, please try again");
    }
}