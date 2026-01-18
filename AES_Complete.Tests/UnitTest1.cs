namespace AES_Complete.Tests;

public class CryptonTests
{
    [Fact]
    public void Test1()
    {
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
      Console.WriteLine(BytesToHex(hashValue));
      
      Console.WriteLine(BytesToHex(secretkey));
      
      secretkey = StringToBytes("ABC");
      hmac = new HMACSHA256(secretkey);
      hashValue = hmac.ComputeHash(fakeFile);
      Console.WriteLine(BytesToHex(hashValue));
      
      secretkey = StringToBytes("a");
      hmac = new HMACSHA256(secretkey);
      byte [] plainText = StringToBytes("a");
      hashValue = hmac.ComputeHash(plainText);
      Console.WriteLine($"HMAC: {BytesToHex(hashValue)}");
    }
}
