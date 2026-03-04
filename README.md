### AES_Complete Provides 
- Provides encryption & decryption using AES256 algorithm and implementing Authenticated encryption.
- Authenticated encryption uses a new IV value every time it encrypts the data.
- Authenticated encryption produces an HMAC over the data:iv so you can be sure when decrypting the data that it has not been hacked or corrupted.
1. Authenticated* encryption using AES256
2. Decryption of AES256 data


### AES_Complete Used With CYaPass-Avalonia Project
Please see my other project which uses this Library: https://github.com/raddevus/CYaPass-Avalonia
### Tests: Check The Test Project To Learn Usage
You can run `$ dotnet test` to see everything run.

### Basic Usage

```C#
// new up a Crypton 
Crypton c = new();
// call Encrypt with the proper parameters
var plainText = "abc" // what you want to encrypt
// hex string of 32 characters (will be converted to bytes)
var pwdKey = "c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644"
var iv = string.Empty; //randomly gen'd IV is allowed to be seen in the clear
// random iv is returned in the iv string from the Encrypt method -- changes every
// time you encrypt your data.
var encryptedBase64Bytes = c.Encrypt(plainText, pwdKey, out iv);


// sample of Encrypt method found in Crypton class
 string Encrypt(string plainText, string pwdKey, out string iv){
      byte [] ivBytes = new byte[16];
      new RNGCryptoServiceProvider().GetBytes(ivBytes);
      iv = BytesToHex(ivBytes);
      var encryptedBytes = EncryptStringToBytes_Aes( plainText, HexStringToBytes(pwdKey), HexStringToBytes(iv));
      return Base64Encode(encryptedBytes);
}
```
