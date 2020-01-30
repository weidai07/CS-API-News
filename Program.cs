using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Collections.Generic;

namespace ApiTest
{
  class Program
  {
    static void Main(string[] args)
    {
      var apiCallTask = ApiHelper.ApiCall("KcK6fDjAAqo2gIv9e0LJboxvgQW8tcpD");
      var result = apiCallTask.Result;
      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      List<TopStory> stories = JsonConvert.DeserializeObject<List<TopStory>>(jsonResponse["results"].ToString());
      for(int i=0; i<stories.Count; i++)
      {
                Console.WriteLine($"Section: {stories[i].Section}");
                Console.WriteLine($"Title: {stories[i].Title}");
                Console.WriteLine($"Abstract: {stories[i].Abstract}");
                Console.WriteLine($"Url: {stories[i].Url}");
                Console.WriteLine($"Byline: {stories[i].Byline}");
                Console.WriteLine("--------------------------------------------------------------");
      }
      
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey)
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}
