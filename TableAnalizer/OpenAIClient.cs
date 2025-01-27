using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class OpenAIClient
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string apiKey = "TU_API_KEY"; // Reemplaza con tu API Key

    public static async Task<string> AnalyzeData(string input)
    {
        var requestBody = new
        {
            model = "text-davinci-003",
            prompt = $"Analiza los siguientes datos y deduce la causa probable: \n{input}",
            max_tokens = 150,
            temperature = 0.7
        };
        var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, );
    }
}
