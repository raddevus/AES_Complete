using System.Security.Cryptography;
using System.Text;

namespace AES_Complete.Models;

public class Crypton{
   public Crypton(){
   }


   public string Decrypt(string encryptedData, string pwdKey, string iv){
      Byte [] encryptedBytes = Base64DecodeAsBytes(encryptedData);
      Byte [] keyPwd = HexStringToBytes(pwdKey);
      Byte [] ivBytes = HexStringToBytes(iv);

      String decryptedHexString = BytesToHex(StringToBytes(DecryptStringFromBytes_Aes(encryptedBytes,keyPwd,ivBytes)));
      String decryptedData = Encoding.UTF8.GetString(HexStringToBytes(decryptedHexString));
      return decryptedData;
   }
private string generateHmac(string mackey, string ivAndEncrypted){
	// NOTE: mackey is lowercased hex values
	// Example mackey: "c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644"
	// NOTE: ivAndEncrypted is lowercased in format of iv:encryptedData
	// Example data: "8c087c023bd23434947a4a477b19dba9:GoHi/mW23ZUwxFkRkbxaCByudTl8FsWw23Yz+KB0ALt9yil8y"
	byte[] mackeyBytes = StringToBytes(mackey);
	var hmac = new HMACSHA256(mackeyBytes);
	var plainText = StringToBytes(ivAndEncrypted);
	var hmacOut = hmac.ComputeHash(plainText);
	Console.WriteLine(BytesToHex(hmacOut));
	return BytesToHex(hmacOut);
}

public bool ValidateHmac(string mackey, string ivAndEncrypted, string targetMac){
   var generatedHmac = generateHmac(mackey, ivAndEncrypted);
   return generatedHmac == targetMac;
}

byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
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
    using (Aes aesAlg = Aes.Create())
    {
		aesAlg.Padding = PaddingMode.PKCS7;
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        // Create an encryptor to perform the stream transform.
        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    //Write all data to the stream.
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
        }
    }

    // Return the encrypted bytes from the memory stream.
    return encrypted;
}

public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
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
    using (Aes aesAlg = Aes.Create())
    {
		aesAlg.Padding = PaddingMode.PKCS7;
        aesAlg.Key = Key;
        aesAlg.IV = IV;

        // Create a decryptor to perform the stream transform.
        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        {
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {

                    // Read the decrypted bytes from the decrypting stream
                    // and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
        }
    }

    return plaintext;
}

public string Base64Encode(string plainText) 
{
  var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
  return System.Convert.ToBase64String(plainTextBytes);
}

public string Base64Decode(string base64EncodedData) 
{
  var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
  return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
}

public  Byte[] Base64DecodeAsBytes(string base64EncodedData) 
{
  return System.Convert.FromBase64String(base64EncodedData);
  //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
}

public string BytesToHex(byte[] bytes) 
{ 
   //return String.Concat(Array.ConvertAll(bytes, x => x.ToString("X2")));
   // returns lowercase hex string
   return String.Concat(Array.ConvertAll(bytes, x => x.ToString("x2"))); 
}

public byte[] HexStringToBytes(string hex){
	return System.Convert.FromHexString(hex);
}

public byte[] StringToBytes(string source){
	var utf8 = new UTF8Encoding();
	byte[] targetBytes = utf8.GetBytes(source);
	return targetBytes;
}
}
