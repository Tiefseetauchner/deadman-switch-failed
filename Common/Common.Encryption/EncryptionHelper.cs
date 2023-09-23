using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Encryption;

public static class EncryptionHelper
{
  public static CryptoStream CreateEncryptionCryptoStream(Stream stream, string password) =>
    new(stream,
      CreateEncryptionAesTransformFromPassword(password),
      CryptoStreamMode.Write);

  private static ICryptoTransform CreateEncryptionAesTransformFromPassword(string password)
  {
    var (passwordSha256, passwordMd5) = GetPasswordHashes(password);

    return CreateAes(passwordSha256, passwordMd5).CreateEncryptor(passwordSha256, passwordMd5);
  }

  public static CryptoStream CreateDecryptionCryptoStream(Stream stream, string password) =>
    new(stream,
      CreateDecryptionAesTransformFromPassword(password),
      CryptoStreamMode.Read);

  private static ICryptoTransform CreateDecryptionAesTransformFromPassword(string password)
  {
    var (passwordSha256, passwordMd5) = GetPasswordHashes(password);

    return CreateAes(passwordSha256, passwordMd5).CreateDecryptor(passwordSha256, passwordMd5);
  }

  private static (byte[] Sha256, byte[] Md5) GetPasswordHashes(string password)
  {
    var passwordBytes = Encoding.UTF8.GetBytes(password);

    return (SHA256.HashData(passwordBytes), MD5.HashData(passwordBytes));
  }

  private static Aes CreateAes(byte[] passwordSha256, byte[] passwordMd5)
  {
    var aes = Aes.Create();

    aes.Key = passwordSha256;
    aes.IV = passwordMd5;
    aes.Padding = PaddingMode.Zeros;

    return aes;
  }
}