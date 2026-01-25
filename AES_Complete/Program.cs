using NewLibre.Models;

Console.WriteLine("Hello, World!");

Crypton c = new();
if ( args.Length < 3 ){
   Console.WriteLine("Please provide hmac & string with \"iv:encryptedData\"");
}

Console.WriteLine($" Is HMAC valid?: {c.ValidateHmac(args[0], args[1], args[2])}");
Console.WriteLine("Driver program completed.");
