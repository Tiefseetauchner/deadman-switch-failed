using System;
using System.IO;
using System.Security.Cryptography;
using NUnit.Framework;

namespace Common.Encryption.Tests.Unit;

public class AesTest
{
  [Test]
  public void Aes()
  {
    AesExample.Run();
  }
}

class AesExample
{
  public static void Run()
  {
    try
    {
      var original = "Here is some data to encrypt!";

      // Create a new instance of the Aes
      // class.  This generates a new key and initialization
      // vector (IV).
      using var myAes = Aes.Create();

      // Encrypt the string to an array of bytes.
      var encrypted = EncryptStringToBytes(original, myAes.Key, myAes.IV);

      // Decrypt the bytes to a string.
      var roundtrip = DecryptStringFromBytes(encrypted, myAes.Key, myAes.IV);

      //Display the original data and the decrypted data.
      Console.WriteLine("Original:   {0}", original);
      Console.WriteLine("Round Trip: {0}", roundtrip);
    }
    catch (Exception e)
    {
      Console.WriteLine("Error: {0}", e.Message);
    }
  }

  static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
  {
    // Check arguments.
    if (plainText == null || plainText.Length <= 0)
      throw new ArgumentNullException("plainText");
    if (Key == null || Key.Length <= 0)
      throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
      throw new ArgumentNullException("IV");
    byte[] encrypted;

    // Create an Aes object
    // with the specified key and IV.
    using var rijAlg = Aes.Create();

    rijAlg.Key = Key;
    rijAlg.IV = IV;
    rijAlg.Padding = PaddingMode.ISO10126;

    // Create an encryptor to perform the stream transform.
    var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

    // Create the streams used for encryption.
    using var msEncrypt = new MemoryStream();

    using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

    using (var swEncrypt = new StreamWriter(csEncrypt))
    {
      //Write all data to the stream.
      swEncrypt.Write(plainText);
    }

    encrypted = msEncrypt.ToArray();

    // Return the encrypted bytes from the memory stream.
    return encrypted;
  }

  static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
  {
    // Check arguments.
    if (cipherText == null || cipherText.Length <= 0)
      throw new ArgumentNullException("cipherText");
    if (Key == null || Key.Length <= 0)
      throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
      throw new ArgumentNullException("IV");

    // Declare the string used to hold
    // the decrypted text.
    string plaintext = null;

    // Create an Aes object
    // with the specified key and IV.
    using var rijAlg = Aes.Create();

    rijAlg.Key = Key;
    rijAlg.IV = IV;
    rijAlg.Padding = PaddingMode.ISO10126;

    // Create a decryptor to perform the stream transform.
    var decryptor = rijAlg.CreateDecryptor(Key, IV);

    // Create the streams used for decryption.
    using var msDecrypt = new MemoryStream(cipherText);
    using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
    using var srDecrypt = new StreamReader(csDecrypt);

    // Read the decrypted bytes from the decrypting stream
    // and place them in a string.
    plaintext = srDecrypt.ReadToEnd();

    return plaintext;
  }
}