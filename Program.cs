using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using System.Security.Cryptography;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

CryptoAlgo ca = new CryptoAlgo();
ca.Main();
class CryptoAlgo{

public void Main()
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
	
	byte[] mackey = StringToBytes("c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644");
	hmac = new HMACSHA256(mackey);
	plainText = StringToBytes("8c087c023bd23434947a4a477b19dba9:GoHi/mW23ZUwxFkRkbxaCByudTl8FsWw23Yz+KB0ALt9yil8yZ9bQsdabQs2mN4IPJtHYMyg2pOiR08VoDvY2xgD6RmsmUsdpIap6cMWPXe3/5VAc8lHmU+tZenBhDw4pYLepLW5Boz8zchDmXCiAN75ZChy3YxCjS/cySJ8YVoAIRCBOIaXQcDjh8TB/iw3EDwsLJ/1NNbB1k3bQelxlYuIgMODN9qkv16pExGPoQF/Zmml/nXEvbUw0qkP8N6uSGZRVx9lsTN7KvPrTY/w0RAzXXOxih+bn7snPtr0phvB+ipHN3sJd3+Wx/1MT8IE53hxNHsRuDEjHMxnbKk5qM5QJqecUyS9d9UqJLEQ15QIpaeXsRzuwfrl0SE6XIIc/uKHjnzuQg1t2cvtlO6EpLd4sm/lK2FvM6xQsU/VRfwcTm4MPFA3Z2etse9fDT7D7c13unH06Vix76k6J+exB+aAdsKzf/QJokC8WRbkde0EvsXBVFHVNaaJir/tx7PU2JIaPaw3ZPaIpo0kW28a/mMp3ezoOfMBtPDNa4gYrDnjMaNv1sjceDD2uzaZifsvw/XBV2a2+USgRJzpgBEwHBotDeD/ztSqFZrSyg/04uMGhKaT2q7onZknqWY2b90Xy2R5OX0Py9FrF1xcrcqs9hZku0Aho8Q12HcW3cnWcC9QpWFcMj9WF1kUWoQ8fHopxB6koHMe441oNf28+NXb9M/Z+6cfzrx6MStqmdX8D6i+/wULDdkK1Lw8akCABjpjBpcUzEZRra1JyWGOWdpRSUbOxvtOJyGT0RJME64IlmLfuUGxX+UHr/CF+0WnVRvIeBtrM5fqvc6V6uYkVpI5JbLFiB3O7CNL9bE4KsRZNs3iC46xevj7gVrIXgMMUa6Y8RRc+369tpkpJvYWdPKY+A==");
	hashValue = hmac.ComputeHash(plainText);
	Console.WriteLine(BytesToHex(hashValue));
	
	// from web
	// 40d5db89b3c6c4a859398ee0d997912df6516972bf1fea8a8cc78bf068f6eaa2
	// from this code
	// 40D5DB89B3C6C4A859398EE0D997912DF6516972BF1FEA8A8CC78BF068F6EAA2
	
	generateHmac("c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644","b4afcd8bfc1b17b926fb3ebe4d84027a:5FH38LpqpfkRqQ5Tfvbr1T7PNQP+PyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR+JnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3+1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU+Wg6jWVGz7qal39ahHwRFkpTkb0u6zu+dwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J+eQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV+ba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1+1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm+fKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0+oELTnIqdNIn1YQ2xOEiq6lwHy78Q+RCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh");
	
	//string encryptedString = Base64Decode("5FH38LpqpfkRqQ5Tfvbr1T7PNQP\u002BPyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR\u002BJnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3\u002B1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU\u002BWg6jWVGz7qal39ahHwRFkpTkb0u6zu\u002BdwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J\u002BeQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV\u002Bba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1\u002B1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm\u002BfKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0\u002BoELTnIqdNIn1YQ2xOEiq6lwHy78Q\u002BRCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh");
	//string encryptedString = "5FH38LpqpfkRqQ5Tfvbr1T7PNQP\u002BPyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR\u002BJnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3\u002B1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU\u002BWg6jWVGz7qal39ahHwRFkpTkb0u6zu\u002BdwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J\u002BeQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV\u002Bba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1\u002B1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm\u002BfKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0\u002BoELTnIqdNIn1YQ2xOEiq6lwHy78Q\u002BRCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh";
	//string encryptedString = "5FH38LpqpfkRqQ5Tfvbr1T7PNQP\u002BPyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR\u002BJnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3\u002B1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU\u002BWg6jWVGz7qal39ahHwRFkpTkb0u6zu\u002BdwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J\u002BeQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV\u002Bba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1\u002B1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm\u002BfKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0\u002BoELTnIqdNIn1YQ2xOEiq6lwHy78Q\u002BRCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh";
	//Console.WriteLine($"encrypted: {encryptedString}");
	
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
	
	
	// ##### NEXT LINE DECODES the Base64, encrypted data into BYTES
	Byte [] encryptedBytes =   Base64DecodeAsBytes("5FH38LpqpfkRqQ5Tfvbr1T7PNQP\u002BPyAHjV/KDlsZ0gPP34EV3DjIKxgYlwtik9500FcYcqGhiOnyrKMtgc4xObrC4kUKz1RCP3EqKWGy9kbGfoDCsjPLoh38fluYiD0riRV6y6yY13jnWlp0WR\u002BJnXS287TUFLNj/qrHVnKBwAVOcw1xmZt29qRgp3\u002B1l3iGwQJSVLaKpKoHNTGd09VN/c0ZS6oQ6p6RDPF7XTgMXB4DHzILU\u002BWg6jWVGz7qal39ahHwRFkpTkb0u6zu\u002BdwpUEp5qnQ1cEPpgvSaTrjKWoN3W9NQVCosQqGvPNDvk7qpOj4yl2YrAnm7tmd3yZ/b6h1J\u002BeQhGyzXzGNKYtX71ZCLjTL2twBe33Z9V2SWufvrgqAEuv89bbUShKTcjvUmGCsuSF8QfnInPl6SWYCE0jn5JRhW65N6MJiG4iKdb1aFnV\u002Bba0Cgp/wPrwr6w9pynP2VSJ65Yn2srfGYfVUlyFccxJtV2/D4PoCF51gzXVOjbcUjOTpCe35CruSuwU1KXIuulCTY83leNpPDTanclcsE9HIJgX1qeboVwChfNoqLOlQd8K2S/PFX8HohI7Oqnrm98wipJVut3hkAsiNcSxcF6wyy76tIXa9Bi2WBBozB8KMGnB8tEgTpGBr5N9ZcgHihNEpOigOEeP/8WC5mbn0NMqmtdW3narNxnk6dr/lU5PAM0Q945vaGepuvm5pOP2twqESfXaJxsZ1\u002B1NjFWFdByx0We/ZvRCMnxBNyp6ILw/EUbbm\u002BfKpfZKxujQl0l4PxE1KZmE7En2oUf34Kqoiq75hrjGsjVFu2Pt6apDZLhtYRNVLRkQltNuxM2laec0\u002BoELTnIqdNIn1YQ2xOEiq6lwHy78Q\u002BRCw66m2kv5caOpHKdLYqYejgJd/rV0eAgmASM6hxXWjfas1wtZavGmxpeiPcSO/ESScKa7XcAc0NwRt/x/wBZcOtanXaASCXqrWRMGYiCURhrJd6wpp1tY1a8pD6CEvaZzxJ3j6T0JEh");// HexStringToBytes(encryptedString);
	Byte [] keyPwd = HexStringToBytes("c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644");
	Console.WriteLine(keyPwd.Length);
	Byte [] ivBytes = HexStringToBytes("b4afcd8bfc1b17b926fb3ebe4d84027a");
	Console.WriteLine(ivBytes.Length);
	// DECRYPTION is applied and the bytes are turned into ClearText HEX BYTES
	// Those HEX BYTES can then be turned into UTF-8 chars
	String decryptedHexString = BytesToHex(StringToBytes(DecryptStringFromBytes_Aes(encryptedBytes,keyPwd,ivBytes)));
	Console.WriteLine(decryptedHexString);
	Console.WriteLine(Encoding.UTF8.GetChars( HexStringToBytes(decryptedHexString)));
	//Console.WriteLine(HexStringToBytes("5b7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a22514852795957357a5a6d567953325635227d2c7b224d61784c656e677468223a223332222c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a747275652c224b6579223a2259574a6a4c584e706447553d227d2c7b224d61784c656e677468223a223136222c224861735370656369616c4368617273223a747275652c22486173557070657243617365223a66616c73652c224b6579223a225957353555326c305a513d3d227d2c7b224d61784c656e677468223a223136222c224861735370656369616c4368617273223a747275652c22486173557070657243617365223a747275652c224b6579223a225a3231686157784259324e7664573530227d2c7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a226332466d5a554a68626d733d227d2c7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a22633356775a584a7a6158526c227d2c7b224d61784c656e677468223a302c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a66616c73652c224b6579223a2265573931636b31686157784259324e7664573530227d2c7b224d61784c656e677468223a223332222c224861735370656369616c4368617273223a66616c73652c22486173557070657243617365223a747275652c224b6579223a22656e70364c55466a59323931626e513d227d5d"));
	//sample HMAC
	// 57bfeffc96a32402266abe2035801e8f65cbd58fc9d88dceb591572acb1dca74
	// 87BED3BB8821D3196854B37290E5A1C3E518F99EFF4101219D1D492FED1EBA1A
	
	// 3ECF5388E220DA9E0F919485DEB676D8BEE3AEC046A779353B463418511EE622
	// 3ECF5388E220DA9E0F919485DEB676D8BEE3AEC046A779353B463418511EE622

	// ##### NEXT LINE DECODES the Base64, encrypted data into BYTES
	encryptedBytes =   Base64DecodeAsBytes("28ChkwP5M76bByKy2NcVEt5zDsaou/2J77i5Ut0OdWA=");// HexStringToBytes(encryptedString);
	keyPwd = HexStringToBytes("c4747607e721580882e7186c136b22d9670779af296772a7abb76f0f40526644");
	Console.WriteLine(keyPwd.Length);
	ivBytes = HexStringToBytes("e403e0dc70660abcfc4aec50ea5400f3");
	Console.WriteLine(ivBytes.Length);
	// DECRYPTION is applied and the bytes are turned into ClearText HEX BYTES
	// Those HEX BYTES can then be turned into UTF-8 chars
	string decryptedString = DecryptStringFromBytes_Aes(encryptedBytes,keyPwd,ivBytes);
	Console.WriteLine($"decryptedString: {decryptedString}");
	decryptedHexString = BytesToHex(StringToBytes(decryptedString));
	Console.WriteLine(decryptedHexString);
	Console.WriteLine(Encoding.UTF8.GetChars( HexStringToBytes(decryptedHexString)));

	 // IV 

	
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

static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
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
    using (Aes aesAlg = AesManaged.Create())
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

static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
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
    using (Aes aesAlg = AesManaged.Create())
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

public static string Base64Encode(string plainText) 
{
  var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
  return System.Convert.ToBase64String(plainTextBytes);
}

public static string Base64Decode(string base64EncodedData) 
{
  var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
  return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
}

public static Byte[] Base64DecodeAsBytes(string base64EncodedData) 
{
  return System.Convert.FromBase64String(base64EncodedData);
  //return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
}

private string BytesToHex(byte[] bytes) 
{ 
   //return String.Concat(Array.ConvertAll(bytes, x => x.ToString("X2"))); 
   return String.Concat(Array.ConvertAll(bytes, x => x.ToString("X2"))); 
}

private byte[] HexStringToBytes(string hex){
	return System.Convert.FromHexString(hex);
}

private byte[] StringToBytes(string source){
	var utf8 = new UTF8Encoding();
	byte[] targetBytes = utf8.GetBytes(source);
	return targetBytes;
}

}