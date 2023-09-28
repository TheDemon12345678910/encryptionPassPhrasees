// See https://aka.ms/new-console-template for more information

using encryptionPassPhrase;
using Spectre.Console;


Console.WriteLine("It seems you have some secret stuff you want to either encrypt or decrypt");
//Getting an input from the user
Console.WriteLine("What would you like to do right now?");

ReadingFile readingFile = new ReadingFile();
EncryptingAndCreatingFile encryptingAndCreatingFile = new EncryptingAndCreatingFile();
SecretKeys secretKeys = new SecretKeys(); //This file is 

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
        readingFile.readingFile((int)secretKeys.sharedKeys());
        waitForeAction();
        wouldYouLikeOneMoreTime();
    }
    else if (desicion == choices[1])
    {
        int sharedKey = (int)secretKeys.sharedKeys();
        Console.WriteLine("Here you can type your Shared-key with Bob and Alice (you have a shared key of this value: "+ sharedKey +")");
        int inputKey = Convert.ToInt32(Console.ReadLine());
        encryptingAndCreatingFile.encryptMessage(inputKey);
        waitForeAction();
        wouldYouLikeOneMoreTime();
    }
    else
    {
        Console.WriteLine("That is not a valid choice, please try again");
    }
}

void wouldYouLikeOneMoreTime()
{
    Console.Clear();
    //Ask the user if they are finished, or if they want to go again.
    Console.WriteLine("Are you finished, or would you like to read and write more files?");
    var desicion = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("What would you like [green]to do[/]?")
            .PageSize(10)
            .MoreChoicesText("[grey](To get more options, use up and down arrows)[/]")
            .AddChoices(new[] {"Yes, I would like to do more", "No, I want to finish"}));

    if (desicion == "Yes, I would like to do more")
    {
        desission();
    }
}

void waitForeAction()
{
    Console.WriteLine("Press Enter to continue");
    do {
        while (! Console.KeyAvailable) {
            Thread.Sleep(50);
        }       
    } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
}