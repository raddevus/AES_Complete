using System.Net.Http.Json;
using NewLibre.Models;

namespace NewLibre.Services;

public class CyaService{
   private String MainToken; 
   private String ApiBaseUrl; 
   // For the URI string build to work the GetDataUrl
   // CANNOT have a leading / (slash)
   // If it does, then parts of the URL are stripped off
   private String GetDataUrl = "Cya/GetData?key=";
   private string PostDataUrl = "Cya/SaveData";
   
   public CyaService(string mainToken, string apiBaseUrl){
      MainToken = mainToken;
      // If you want to set the URL to newlibre.com/LibreStore 
      ApiBaseUrl = apiBaseUrl;
   }

   async public Task<CyaDTO> GetCyaData(){
      var http = new HttpClient();

      // Check & insure that last char is a slash (if not, fix) 
      if (ApiBaseUrl.Substring(ApiBaseUrl.Length-1,1) != "/"){
         ApiBaseUrl += "/";
      }
      var uri = new Uri(ApiBaseUrl);
      Console.WriteLine($"uri: {uri}"); 
      // new Uri() call safely concats URLs considering / etc.      
      string url = $"{new Uri(uri, GetDataUrl)}{MainToken}";
      Console.WriteLine($"url : {url}");
      // Strongly-typed fetch
      var cya = await http.GetFromJsonAsync<CyaDTO>(url);
      if (cya?.Success ?? false){
         Console.WriteLine($"Success! {cya.CyaBucket.Id}");
         return cya;
      }
      
      Console.WriteLine($"Success: {cya?.Success}");
      return null;
   }

   async public Task<bool> SaveCyaData(string data,
         string hmac, string iv){
        
      var http = new HttpClient();

      var formData = new FormUrlEncodedContent(new[]
      {
          new KeyValuePair<string, string>("key", MainToken),
          new KeyValuePair<string, string>("data", data),
          new KeyValuePair<string, string>("hmac", hmac),
          new KeyValuePair<string, string>("iv", iv)
      });


      // Check & insure that last char is a slash (if not, fix) 
      if (ApiBaseUrl.Substring(ApiBaseUrl.Length-1,1) != "/"){
         ApiBaseUrl += "/";
      }
      var uri = new Uri(ApiBaseUrl);
      Console.WriteLine($"uri: {uri}"); 
      // new Uri() call safely concats URLs considering / etc.      
      string url = $"{new Uri(uri, PostDataUrl)}";
      Console.WriteLine($"url : {url}");
      // Strongly-typed fetch
      var response = await http.PostAsync(url, formData);
      Console.WriteLine($"reponse: {response}");
     // response.EnsureSuccessStatusCode(); 
     return true;
   }

    //SaveData([FromForm] String key,

}
