namespace encryptionPassPhrase;

public class EncryptingAndCreatingFile
{
    public void encryptMessage(int key)
    {
        Console.WriteLine(
            "Please enter, what you would like to encrypt, You can ONLY use characters from the alphabet and 'Space'");
        char[] message = Console.ReadLine().ToCharArray();

        //The cypher we produce is a caesar cypher.
        char[] alphabet =
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z', ' '
        };
        //The encryption will happen with the key, being the number of letters we go in the alphabet
        //and a random generated "Salt", that we also keep in the file.
        string encryptedMessage = "";
        
        //Encrypting the message
        foreach (char c in message)
        {
            int index = Array.IndexOf(alphabet, c);
            int newIndex = index + key;
            if (alphabet.Length - 1 < newIndex)
            {
                newIndex = newIndex - alphabet.Length;
            }

            char newChar = alphabet[newIndex];
            encryptedMessage = encryptedMessage + newChar;
        }

        //Find out what number of file we have reached
        int num = findTheNumber();
        Console.WriteLine("Please enter the wanted name of the .txt file\n");
        string fileName = Console.ReadLine();
        // path of the file that we want to create
        string pathName = "..\\..\\..\\EncryptedFiles\\" + fileName + ".txt";

        // Create() creates a file at pathName 
        FileStream fs = File.Create(pathName);
        fs.Close();
        File.WriteAllText(pathName, encryptedMessage);
        Console.WriteLine("You have successfully encrypted a message and saved it in a file");
    }

    private int findTheNumber()
    {
        string fileDirectory = "..\\..\\..\\EncryptedFiles";
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

        return readableFiles.Count + 1;
    }
}