using Newtonsoft.Json;
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
        private readonly HttpClient client;


        public Request(Account account)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {account.Token}"); // при инициализации клиента создаём экземпляр
                                                                                         //  client.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }


        public async Task<string> MakeRequestForNumber() // хоба, асинхронный метод в классе
        {
            var response = await client.GetAsync(url + accurl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }


    public async Task<string> MakeRequestForBalance(Account account) // то же самое что и выше, только для запроса баланса
        {
            string number = account.GetNumber(account);
            string CurrentBalanceUrl = url+BalanceUrl.Replace("{number}", number);
            var respone = await client.GetAsync(CurrentBalanceUrl);
            respone.EnsureSuccessStatusCode();
            return await respone.Content.ReadAsStringAsync();
        }
        
        

        

    }
}
