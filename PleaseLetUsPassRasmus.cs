namespace encryptionPassPhrase;


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SecretMessage
{
    public byte[] Nonce { init; get; }
    public byte[] Tag { init; get; }
    public byte[] Salt { init; get; }
    public byte[] Cipher { init; get; }
}

class PleaseLetUsPassRasmus
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the CLI Encryption/Decryption Tool!");

        Console.Write("Enter your passphrase: ");
        string passphrase = Console.ReadLine();

        bool exit = false;

        do
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Encrypt and save to file");
            Console.WriteLine("2. Read from file, decrypt, and show");
            Console.WriteLine("3. Exit");

            Console.Write("Enter option (1, 2, or 3): ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Enter the message to encrypt: ");
                    string messageToEncrypt = Console.ReadLine();

                    SecretMessage secretMessage = EncryptString(messageToEncrypt, passphrase);

                    Console.Write("Enter the file name to save the encrypted message: ");
                    string fileNameEncrypt = Console.ReadLine();

                    SaveSecretMessageToFile(secretMessage, fileNameEncrypt);

                    Console.WriteLine("Message encrypted and saved to file successfully.");
                    break;

                case "2":
                    Console.Write("Enter the file name to read the encrypted message: ");
                    string fileNameDecrypt = Console.ReadLine();

                    SecretMessage secretMessageFromFile = ReadSecretMessageFromFile(fileNameDecrypt);

                    string decryptedMessage = DecryptString(secretMessageFromFile, passphrase);

                    Console.WriteLine("Decrypted message: ");
                    Console.WriteLine(decryptedMessage);
                    break;

                case "3":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please choose 1, 2, or 3.");
                    break;
            }

        } while (!exit);
    }
    

    static SecretMessage EncryptString(string plainText, string passphrase)
    {
        using (Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(passphrase, salt: new byte[8], iterations: 10000))
        {
            // Choose the appropriate key size (256 bits for AES-256)
            byte[] key = keyDerivation.GetBytes(32); // 32 bytes = 256 bits

            using (AesGcm aesGcm = new AesGcm(key))
            {
                byte[] nonce = new byte[12]; // GCM nonce size is 12 bytes
                byte[] additionalData = Encoding.UTF8.GetBytes("additionalData");

                // Combine additional data and plaintext into a single array
                byte[] plaintextBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] combinedData = new byte[additionalData.Length + plaintextBytes.Length];
                Buffer.BlockCopy(additionalData, 0, combinedData, 0, additionalData.Length);
                Buffer.BlockCopy(plaintextBytes, 0, combinedData, additionalData.Length, plaintextBytes.Length);

                // Encrypt the combined data
                byte[] tag = new byte[16];
                byte[] ciphertext = new byte[combinedData.Length];

                aesGcm.Encrypt(nonce, combinedData, ciphertext, tag, additionalData);

                SecretMessage secretMessage = new SecretMessage
                {
                    Nonce = nonce,
                    Tag = tag,
                    Salt = keyDerivation.Salt,
                    Cipher = ciphertext
                };

                return secretMessage;
            }
        }
    }

    static void SaveSecretMessageToFile(SecretMessage secretMessage, string fileName)
    {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            writer.Write(secretMessage.Nonce);
            writer.Write(secretMessage.Tag);
            writer.Write(secretMessage.Salt);
            writer.Write(secretMessage.Cipher);
        }
    }

    static SecretMessage ReadSecretMessageFromFile(string fileName)
    {
        using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
        using (BinaryReader reader = new BinaryReader(fileStream))
        {
            SecretMessage secretMessage = new SecretMessage
            {
                Nonce = reader.ReadBytes(12),
                Tag = reader.ReadBytes(16),
                Salt = reader.ReadBytes(8),
                Cipher = reader.ReadBytes((int)(fileStream.Length - reader.BaseStream.Position))
            };

            return secretMessage;
        }
    }

    static string DecryptString(SecretMessage secretMessage, string passphrase)
    {
        using (Rfc2898DeriveBytes keyDerivation = new Rfc2898DeriveBytes(passphrase, secretMessage.Salt, iterations: 10000))
        {
            byte[] key = keyDerivation.GetBytes(32); // 32 bytes = 256 bits

            using (AesGcm aesGcm = new AesGcm(key))
            {
                byte[] nonce = secretMessage.Nonce;
                byte[] additionalData = Encoding.UTF8.GetBytes("additionalData");
                byte[] combinedData = new byte[secretMessage.Cipher.Length + additionalData.Length];

                Buffer.BlockCopy(secretMessage.Cipher, 0, combinedData, 0, secretMessage.Cipher.Length);
                Buffer.BlockCopy(additionalData, 0, combinedData, secretMessage.Cipher.Length, additionalData.Length);

                byte[] decryptedBytes = new byte[secretMessage.Cipher.Length];

                // Decrypt the combined data
                aesGcm.Decrypt(nonce, combinedData, secretMessage.Tag, decryptedBytes);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }


}

