using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace Catalog.API.Extensions
{
  public static class Guider
  {

    private const char EqualsChar = '=';
    private const char ForwardSlash = '/';
    private const byte ForwardSlashByte = (byte)'/';
    private const char Plus = '+';
    private const byte PlusByte = (byte)'+';
    private const char Hyphen = '-';
    private const char Underscore = '_';
    private const char Space = ' ';
    private const char SpaceLeft = ' ';
    
    public static string ToStringFromGuid(Guid id)
    {
      return Convert.ToBase64String(id.ToByteArray())
        .Replace("/", "-")
        .Replace("+", "_")
        .Replace("=", string.Empty);
    }

    public static string ToStringFromGuidOpitimized(Guid id)
    {
      Span<byte> idBytes = stackalloc byte[16];
      Span<byte> base64Bytes = stackalloc byte[24];

      MemoryMarshal.TryWrite(idBytes, ref id);

      Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);

      Span<char> finalChars = stackalloc char[22];

      for (var i = 0; i < 22; i++)
      {
        finalChars[i] = base64Bytes[i] switch
        {
          ForwardSlashByte => Hyphen,
          PlusByte => Underscore,
          _=> (char)base64Bytes[i]
        };
      }

      return new string(finalChars);
    }

    public static Guid ToGuidFromString(string id)
    {
      var efficientBase64 = Convert.FromBase64String(id
          .Replace("-", "/")
          .Replace("_", "+") + "==");
      return new Guid(efficientBase64);
    }

    public static Guid ToGuidFromStringOpitimized(ReadOnlySpan<char> id)
    {
      Span<char> base64Chars = stackalloc char[24];

      for(var i = 0; i < 22; i++)
      {
        base64Chars[i] = id[i] switch
        {
          Hyphen => ForwardSlash,
          Underscore => Plus,
          _ => id[i]
        };
      }

      base64Chars[22] = EqualsChar;
      base64Chars[22] = EqualsChar;

      Span<byte> idBytes = stackalloc byte[16];

      Convert.TryFromBase64Chars(base64Chars, idBytes, out _);
      
      return new Guid(idBytes);
    }
  }
}
