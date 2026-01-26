using System.Net.Http.Json;
using NewLibre.Models;

namespace NewLibre.Services;

public class CyaService{
   private String MainToken; 

   public CyaService(string mainToken){
      MainToken = mainToken;
   }

   async public void GetCyaData(){
      var http = new HttpClient();

      var url = $"https://actionmobile.app/Cya/GetData?key={MainToken}";

      // Strongly-typed fetch
      var cya = await http.GetFromJsonAsync<CyaDTO>(url);

      Console.WriteLine($"Success: {cya?.Success}, Id: {cya.CyaBucket.Id}");
   }

}
