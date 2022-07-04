using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QiwiDemo;

namespace QiwiDemo
{
    public class Account
    {
        public string Number { get; set; } // для хранения номера телефона

        public string Token { get; set; } // для токена

        public string UserInfo { get; set; } // на всякий случай
        // хоба, коллекция <ключ, значение> для хранения неопределенного количества кошельков и их суммы
        public Dictionary<int, float> Wallet { get; set; } = new Dictionary<int, float>(); 



        public Account(string token) // при создании обьекта обязан добавить токен, иначе ЭКСЕПШН!!
        {
            this.Token = token;
            
        }

        public static async Task<Account> CreateAccount (string token)
        {
            Account account = new Account(token);
            await account.InitializeAsync(account);
            return account;
        }

        private async Task InitializeAsync(Account account)
        {
            Request requester = new Request(account); // Создаём запрос
            var number = JsonConvert.DeserializeObject<dynamic>(await requester.MakeRequestForNumber(account)); // Получаем в переменную результат ответа от сервера
            account.Number = number.contractInfo.contractId.ToString(); // переносим данные (номер телефона) из результата в экземпляр класса 
            var k = JsonConvert.DeserializeObject<dynamic>(await requester.MakeRequestForBalance(account)); // Делаем запрос и получаем информацию о количестве и суммах кошельков

            foreach (var acc in k.accounts) // для каждого валютного кошелька переносим значения (валюта, сумма) в экземпляр класса 
            {
                int q = acc.balance.currency;
                float z = acc.balance.amount;
                account.Wallet.Add(q, z);
            }
        }
    }


}
