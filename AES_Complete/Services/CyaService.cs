using System.Net.Http.Json;
using NewLibre.Models;

namespace NewLibre.Services;

public class CyaService{
   private String MainToken; 

   public CyaService(string mainToken){
      MainToken = mainToken;
   }

   async public Task<CyaDTO> GetCyaData(){
      var http = new HttpClient();

      var url = $"https://actionmobile.app/Cya/GetData?key={MainToken}";

      // Strongly-typed fetch
      var cya = await http.GetFromJsonAsync<CyaDTO>(url);
      if (cya?.Success ?? false){
         Console.WriteLine($"Success! {cya.CyaBucket.Id}");
         return cya;
      }
      
      Console.WriteLine($"Success: {cya?.Success}");
      return null;
   }

}
