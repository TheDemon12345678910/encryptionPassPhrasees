// See https://aka.ms/new-console-template for more information

using encryptionPassPhrase;


Console.WriteLine("Hello there.\n it seems you have some secret stuff you want to either encrypt or decrypt");
//Getting an input from the user
Console.WriteLine("What would you like to do right now?\nA) Read a secret file\nB) Write a secret file");
string choice = Console.ReadLine();
ReadingFile readingFile = new ReadingFile();
EncryptingAndCreatingFile encryptingAndCreatingFile = new EncryptingAndCreatingFile();

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