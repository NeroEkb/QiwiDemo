using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QiwiDemo
{
    internal class Request
    {
        private const string url = "https://edge.qiwi.com"; // ссылка на сервис
        private readonly string accurl = "/person-profile/v1/profile/current?authInfoEnabled=True"; // ссылка для получения информации об аккаунте
        private readonly string BalanceUrl = "/funding-sources/v2/persons/{number}/accounts"; // ссылка для получения информации о кошельках
        private readonly RestClient client;

        public Request(Account account)
        {
            client = new RestClient(); // при инициализации клиента создаём экземпляр
        }

        private RestRequest AddHeaders(RestRequest request, string token) // для работы запросов необходимы соответствующие заголовки 
        {
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {token}");
            return request;
        }

        public async Task<string> MakeRequestForNumber(Account account) // хоба, асинхронный метод в классе
        {
            RestRequest request = new RestRequest(url + accurl); // создаём запрос и корректируем ссылку
            AddHeaders(request, account.Token); // добавляем заголовки
            var response = await client.ExecuteAsync(request); // получаем ассинхронно ответ
            string? result = null; // результат может быть null
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK) // Если ответ пиздат то
            {
                result = response.Content.ToString(); // в строку его

            }

            return result; // и на вывод
        }

        public async Task<string> MakeRequestForBalance(Account account) // то же самое что и выше, только для запроса баланса
        {
            string CurrentBalanceUrl = BalanceUrl.Replace("{number}", account.Number);
            RestRequest request = new RestRequest(url + CurrentBalanceUrl);
            AddHeaders(request, account.Token);
            var respone = await client.ExecuteAsync(request);
            string? result = null;
            if (respone != null && respone.StatusCode == System.Net.HttpStatusCode.OK)
            {
                result = respone.Content.ToString();
            }
            return result;
        }
        
        

        

    }
}
