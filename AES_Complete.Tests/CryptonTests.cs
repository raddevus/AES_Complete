
using System.Security.Cryptography;
using AES_Complete.Models;

namespace AES_Complete.Tests;

public class CryptonTests
{
    [Fact]
    public void GenerateHMAC()
    {
      Crypton c = new();
      byte[] secretkey = new Byte[64];
      secretkey[0] = 65;
      secretkey[1] = 66;
      secretkey[2] = 67;
      Console.WriteLine($"{Convert.ToChar(secretkey[0])}");
      HMACSHA256 hmac = new HMACSHA256(secretkey);
      //Console.WriteLine(hmac);
      byte [] fakeFile = {65,66,67};
      byte[] hashValue = hmac.ComputeHash(fakeFile);
      //Console.WriteLine(hashValue);
      Console.WriteLine(c.BytesToHex(hashValue));
      
      Console.WriteLine(c.BytesToHex(secretkey));
      
      secretkey = c.StringToBytes("ABC");
      hmac = new HMACSHA256(secretkey);
      hashValue = hmac.ComputeHash(fakeFile);
      Console.WriteLine(c.BytesToHex(hashValue));
      
      secretkey = c.StringToBytes("a");
      hmac = new HMACSHA256(secretkey);
      byte [] plainText = c.StringToBytes("a");
      hashValue = hmac.ComputeHash(plainText);
      Console.WriteLine($"HMAC: {c.BytesToHex(hashValue)}");
    }
}
