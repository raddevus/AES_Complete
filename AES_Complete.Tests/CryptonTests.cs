using System.Security.Cryptography;
using System.Text;
using NewLibre.Models;
using NewLibre.Services;

namespace AES_Complete.Tests;

public class CryptonTests
{
   [Fact]
   public void EncryptDecrypt(){
      Crypton c = new();
      var pwdKey = "a40ebcd6611aee763d931762dbc5ff75bf0f54d4f029eb2f3524adf9036ffdfb";
      string ivFromEncrypt;
      var encryptedBase64 = c.Encrypt("abc", pwdKey, out ivFromEncrypt);
      Console.WriteLine($"Encrypted base64: {encryptedBase64}");
      var decryptedMessage = c.Decrypt(encryptedBase64, pwdKey, ivFromEncrypt);
      Console.WriteLine($"Decrypted message: {decryptedMessage}");
   }
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

    [Fact]
    public void RetrieveDataAndDecrypt(){
    }

    [Fact]
    public void Base64EncodeTest(){
       Crypton c = new();
       var unencodedText = "this is the test";
       Console.WriteLine($"encoding: \"{unencodedText}\"");
       Console.WriteLine($"result: {c.Base64Encode(unencodedText)}");
    }

    [Fact]
    public void Base64DecodeTest(){
       Crypton c = new();
       var encodedText = "dGhpcyBpcyB0aGUgdGVzdA==";
       Console.WriteLine($"decoding: \"{encodedText}\"");
       Console.WriteLine($"result: {c.Base64Decode(encodedText)}");
    }

    [Fact] void DecryptTest(){
       // Calls Crypton Decrypt()
      Console.WriteLine("### Running Decrypt()... ####");
      Crypton c = new();
      string encryptedData = "5FH38LpqpfkRqQ5Tfvbr1T7PNQP\u002BPyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR\u002BJnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3\u002B1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU\u002BWg6jWVGz7qal39ahHwRFkpTkb0u6zu\u002BdwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J\u002BeQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV\u002Bba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1\u002B1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm\u002BfKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0\u002BoELTnIqdNIn1YQ2xOEiq6lwHy78Q\u002BRCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh";
      string keyPwd = "c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644";
      string iv = "b4afcd8bfc1b17b926fb3ebe4d84027a";
      Console.WriteLine($"{c.Decrypt(encryptedData, keyPwd, iv)}");
    }

    [Fact]
    public void FullDecryptionTest(){

      // ############## STEPS FOR DECRYPTING DATA ####################################
      // 1. Decode the data from Base64 to Bytes -- these are the encrypted bytes
      // 2. Set the pwdkey by taking your sha256 hash Hex string and decoding it to bytes (32 bytes)
      // 3. Decode the IV from hex string into 16 bytes
      // 4. Call DecryptStringFromBytes with encrypted bytes, pwdkey, & iv bytes
      // 5. Use C# AESManaged algorithm
      // 6. set the aesAlg.Padding = PaddingMode.PKCS7; to PKCS7 Padding
       // 7. set key (pwdkey)  aesAlg.Key = Key;
       // 8. set IV aesAlg.IV = IV;
      // 9. The returned string will be the decrypted bytes.
      // 10. Convert that string to bytes
      // 11. convert the bytes to a hex string
      // 12. convert the hex string to characters -- this is the decrypted and decoded data (cleartext)
      
      Crypton c = new ();      
      // ##### NEXT LINE DECODES the Base64, encrypted data into BYTES
      Byte [] encryptedBytes =   c.Base64DecodeAsBytes("5FH38LpqpfkRqQ5Tfvbr1T7PNQP\u002BPyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR\u002BJnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3\u002B1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU\u002BWg6jWVGz7qal39ahHwRFkpTkb0u6zu\u002BdwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J\u002BeQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV\u002Bba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1\u002B1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm\u002BfKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0\u002BoELTnIqdNIn1YQ2xOEiq6lwHy78Q\u002BRCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh");// HexStringToBytes(encryptedString);
      Byte [] keyPwd = c.HexStringToBytes("c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644");
      Console.WriteLine($"keyPwd.Length {keyPwd.Length}");
      Byte [] ivBytes = c.HexStringToBytes("b4afcd8bfc1b17b926fb3ebe4d84027a");
      Console.WriteLine($"ivBytes.Length {ivBytes.Length}");
      // DECRYPTION is applied and the bytes are turned into ClearText HEX BYTES
      // Those HEX BYTES can then be turned into UTF-8 chars
      String decryptedHexString = c.BytesToHex(c.StringToBytes(c.DecryptStringFromBytes_Aes(encryptedBytes,keyPwd,ivBytes)));
      Console.WriteLine($"decryptedHexString \n {decryptedHexString}");
      Console.WriteLine(Encoding.UTF8.GetChars( c.HexStringToBytes(decryptedHexString)));
      //Console.WriteLine(c.HexStringToBytes("5b7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a22514852795957357a5a6d567953325635227d2c7b224d61784c656e677468223a223332222c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a747275652c224b6579223a2259574a6a4c584e706447553d227d2c7b224d61784c656e677468223a223136222c224861735370656369616c4368617273223a747275652c22486173557070657243617365223a66616c73652c224b6579223a225957353555326c305a513d3d227d2c7b224d61784c656e677468223a223136222c224861735370656369616c4368617273223a747275652c22486173557070657243617365223a747275652c224b6579223a225a3231686157784259324e7664573530227d2c7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a226332466d5a554a68626d733d227d2c7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a22633356775a584a7a6158526c227d2c7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a2265573931636b31686157784259324e7664573530227d2c7b224d61784c656e677468223a223332222c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a747275652c224b6579223a22656e70364c55466a59323931626e513d227d5d"));

    }
}
