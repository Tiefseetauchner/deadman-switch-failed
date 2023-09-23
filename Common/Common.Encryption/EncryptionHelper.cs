using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Encryption;

public static class EncryptionHelper
{
  public static CryptoStream CreateEncryptionCryptoStream(Stream stream, string password, byte[] iv) =>
    new(stream,
      CreateEncryptionAesTransformFromPassword(password, iv),
      CryptoStreamMode.Write);

  private static ICryptoTransform CreateEncryptionAesTransformFromPassword(string password, byte[] iv)
  {
    var passwordSha256 = GetPasswordHash(password);

    return CreateAes(passwordSha256, iv).CreateEncryptor();
  }

  public static CryptoStream CreateDecryptionCryptoStream(Stream stream, string password, byte[] iv) =>
    new(stream,
      CreateDecryptionAesTransformFromPassword(password, iv),
      CryptoStreamMode.Read);

  private static ICryptoTransform CreateDecryptionAesTransformFromPassword(string password, byte[] iv)
  {
    var passwordSha256 = GetPasswordHash(password);

    return CreateAes(passwordSha256, iv).CreateDecryptor();
  }

  private static byte[] GetPasswordHash(string password) =>
    SHA256.HashData(Encoding.UTF8.GetBytes(password));

  private static Aes CreateAes(byte[] passwordSha256, byte[] iv)
  {
    var aes = Aes.Create();

    aes.Key = passwordSha256;
    aes.IV = iv;
    aes.Padding = PaddingMode.ISO10126;

    return aes;
  }
}