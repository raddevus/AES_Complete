using NewLibre.Services;

namespace AES_Complete.Tests;

public class CyaServiceTests
{
   [Fact]
   async public Task PostCyaData(){
      CyaService svc = new("2026-03-01Test", "https://newlibre.com/LibreStore/");
      var result = await svc.SaveCyaData("this is just some test data", "HMAC - fake hmac for test", "IV fake - 232245223");
      Console.WriteLine($"Posted: {result}");
     
      Console.WriteLine("Yepper!");
   }
}
