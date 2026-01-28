using System.Net.Http.Json;
using NewLibre.Models;

namespace NewLibre.Services;

public class CyaService{
   private String MainToken; 
   private String ApiBaseUrl; 
   public CyaService(string mainToken, string apiBaseUrl){
      MainToken = mainToken;
      // If you want to set the URL to newlibre.com/LibreStore 
      ApiBaseUrl = apiBaseUrl;
   }

   async public Task<CyaDTO> GetCyaData(){
      var http = new HttpClient();

      var url = $"{ApiBaseUrl}{MainToken}";

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
