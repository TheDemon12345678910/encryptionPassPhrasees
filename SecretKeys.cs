using System.Numerics;

namespace encryptionPassPhrase;

public class SecretKeys
{
    public void sharedKeys()
    {
        // Step 1: Choose prime numbers p and g (you can choose your own)
        int p = 23; // Prime number
        int g = 5;  // Primitive root modulo p

        // Step 2: Generate private keys for Alice and Bob
        int a = 4; // Alice's private key
        int b = 3; // Bob's private key

        // Step 3: Calculate public keys for Alice and Bob
        BigInteger A = BigInteger.ModPow(g, a, p); // Alice's public key
        BigInteger B = BigInteger.ModPow(g, b, p); // Bob's public key

        // Step 4: Exchange public keys (Assuming A and B are exchanged)

        // Step 5: Calculate shared secret keys
        BigInteger SharedKeyAlice = BigInteger.ModPow(B, a, p); // Alice's shared key
        BigInteger SharedKeyBob = BigInteger.ModPow(A, b, p);   // Bob's shared key

        // Both Alice and Bob now have the same shared key
        Console.WriteLine("Shared Key (Alice): " + SharedKeyAlice);
        Console.WriteLine("Shared Key (Bob): " + SharedKeyBob);
    }
}