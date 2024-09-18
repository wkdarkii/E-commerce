using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using eticaret.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using eticaret.Models;

namespace ChatMvcApp.Controllers
{
    public class ChatController : Controller
    {
        private readonly string apiKey = "sk-AUafTjEcm00ONcB6VKsZbzgh69wRydHAQQRy35SD6ET3BlbkFJkgf133VPubD95_NWDofx9lQRP_eNyo-JiAS6GtgjUA";

        [HttpGet]
        public IActionResult AskQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(QuestionModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Question))
            {
                ViewBag.ErrorMessage = "Soru boş olamaz.";
                return View();
            }

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "system", content = "Sen bir e-ticaret satıcısısın ve 4 kategori (spor eşyaları, ev eşyaları, moda ve giyim, elektronik eşyalar) üzerindeki ürünlerin hakkında cevap verebilirsin. Bu satırların dışarısına çıkmamalısın." },
                    new { role = "user", content = model.Question }
                }
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    // JSON yanıtını JObject olarak işleyin
                    var responseJson = JObject.Parse(responseString);

                    // Hata mesajını kontrol edin
                    if (responseJson["error"] != null)
                    {
                        var errorMessage = responseJson["error"]["message"]?.ToString();
                        ViewBag.Answer = $"Hata: {errorMessage}";
                    }
                    else if (responseJson["choices"] != null && responseJson["choices"].Any())
                    {
                        var answer = responseJson["choices"][0]["message"]["content"]?.ToString();
                        ViewBag.Answer = answer ?? "Cevap alınamadı.";
                    }
                    else
                    {
                        ViewBag.Answer = "Cevap içeriği bulunamadı.";
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Answer = $"Bir hata oluştu: {ex.Message}";
            }

            return View();
        }
    }
}
