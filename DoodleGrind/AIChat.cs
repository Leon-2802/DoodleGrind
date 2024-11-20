using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DoodleGrind
{
    internal static class AIChat
    {
        public static async Task<String> SendChat(string message)
        {
            // Define the API endpoint and the authorization key
            string url = "https://api.cohere.com/v2/chat";
            string? loadedKey = Helpers.ReadFileToString("C:\\Users\\Le_go\\Documents\\GitHub\\DoodleGrind\\DoodleGrind\\COHERE_API_KEY.txt"); // Load API key
            if (loadedKey == null) // check if this is good practice (probably not)
            {
                throw new ArgumentNullException("Error when loading API key");
            }
            string apiKey = loadedKey; // Replace with your actual API key

            // Create the JSON data
            var jsonData = new
            {
                model = "command-r-plus-08-2024",
                messages = new[]
                {
                    new { role = "user", content = message }
                }
            };

            // Serialize the JSON data to a string
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                // Add the required headers
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

                // Create the HTTP content
                HttpContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Read and display the response
                string responseString = await response.Content.ReadAsStringAsync();
                dynamic? jsonResponse = JsonConvert.DeserializeObject(responseString);
                if (jsonResponse?.message?.content[0]?.text != null)
                {
                    return jsonResponse.message.content[0].text;
                }
                else
                {
                    throw new InvalidOperationException("Text content is missing in the JSON response.");
                }
            }
        }

        private static String ExtractDoodleIdeas(dynamic jsonRes)
        {
            return "";
        }
    }
}
