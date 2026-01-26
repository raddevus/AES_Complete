using System.Net.Http.Json;
using NewLibre.Models;

namespace NewLibre.Services;

public class CyaService{
   private String MainToken; 
   private String apiBaseUrl = "https://actionmobile.app/Cya/GetData?key=";

   public CyaService(string mainToken){
      MainToken = mainToken;
      // If you want to set the URL to newlibre.com/LibreStore 
      apiBaseUrl = "https://newlibre.com/LibreStore/cya/GetData?key=";
   }

   async public Task<CyaDTO> GetCyaData(){
      var http = new HttpClient();

      var url = $"{apiBaseUrl}{MainToken}";

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
