using NewLibre.Models;
using NewLibre.Services;

Console.WriteLine("Would you like to retrieve data from LibreStore? Y/N");

var response = Console.ReadLine();
if (response.ToUpper() == "Y"){
   if (args.Length < 1){
      Console.WriteLine("Please provide a MainToken on the command line.");
      return;
   }
   CyaService cs = new CyaService(args[0]);
   var cya = await cs.GetCyaData();
   Console.WriteLine("Waiting for async call to complete.");

   if (cya != null){
      Console.WriteLine($"iv - {cya.CyaBucket.Iv}");
      Console.WriteLine($"hmac - {cya.CyaBucket.Hmac}");
   }
   return;
}
Crypton c = new();
if ( args.Length < 3 ){
   Console.WriteLine("Please provide hmac & string with \"iv:encryptedData\"");
}

Console.WriteLine($" Is HMAC valid?: {c.ValidateHmac(args[0], args[1], args[2])}");
Console.WriteLine("Driver program completed.");


