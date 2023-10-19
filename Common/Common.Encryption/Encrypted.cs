using DeadmanSwitchFailed.Common.ArgumentChecks;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace Common.Encryption;

public class Encrypted<T>
{
  private readonly byte[] _data;

  public static Encrypted<T> Create(T payload, string password) =>
    new(Encrypt(payload.CheckNotNull(), password.CheckNotNull()));

  private Encrypted(byte[] data)
  {
    _data = data.CheckNotNull();
  }

  private static byte[] Encrypt(T payload, string password)
  {
    var iv = RandomNumberGenerator.GetBytes(16);

    using var memoryStream = new MemoryStream();
    using var cryptoStream = EncryptionHelper.CreateEncryptionCryptoStream(memoryStream, password, iv);
    JsonSerializer.SerializeAsync(cryptoStream, payload);

    cryptoStream.FlushFinalBlock();

    return iv
      .Concat(memoryStream.ToArray())
      .ToArray();
  }

  public T Decrypt(string password)
  {
    using var memoryStream = new MemoryStream(_data);

    var iv = new byte[16];
    memoryStream.ReadExactly(iv, 0, 16);

    using var cryptoStream = EncryptionHelper.CreateDecryptionCryptoStream(memoryStream, password.CheckNotNull(), iv);

    return JsonSerializer.Deserialize<T>(cryptoStream);
  }
}