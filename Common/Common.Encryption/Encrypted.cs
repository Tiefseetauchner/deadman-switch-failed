using System.IO;
using System.Text.Json;
using DeadManSwitchFailed.Common.ArgumentChecks;

namespace Common.Encryption;

public class Encrypted<T>
{
  private readonly byte[] _data;

  public static Encrypted<T> Create(T payload, string password) =>
    new(Encrypt(payload, password));

  private Encrypted(byte[] data)
  {
    _data = data.CheckNotNull();
  }

  private static byte[] Encrypt(T payload, string password)
  {
    using var memoryStream = new MemoryStream();
    using var cryptoStream = EncryptionHelper.CreateEncryptionCryptoStream(memoryStream, password);
    JsonSerializer.SerializeAsync(cryptoStream, payload);

    return memoryStream.ToArray();
  }

  public T Decrypt(string password)
  {
    using var memoryStream = new MemoryStream(_data);
    using var cryptoStream = EncryptionHelper.CreateDecryptionCryptoStream(memoryStream, password);

    // using var streamreader = new StreamReader(cryptoStream);
    // Console.WriteLine(streamreader.ReadToEnd());

    return JsonSerializer.Deserialize<T>(cryptoStream);
  }
}