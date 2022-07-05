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

        private string Number { get; set; } // для хранения номера телефона

        public string Token { get; set; } // для токена

        private string Nickname { get; set; } // название кошелька

        private string UserInfo { get; set; } // на всякий случай


        // хоба, коллекция <ключ, значение> для хранения неопределенного количества кошельков и их суммы
        private Dictionary<int, float> Wallet { get; set; } = new Dictionary<int, float>(); 



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
            var response = JsonConvert.DeserializeObject<dynamic>(await requester.MakeRequestForNumber()); // Получаем в переменную результат ответа от сервера
            account.Number = response.contractInfo.contractId.ToString(); // переносим данные (номер телефона) из результата в экземпляр класса 
            account.Nickname = response.contractInfo.nickname.nickname.ToString();
            response = JsonConvert.DeserializeObject<dynamic>(await requester.MakeRequestForBalance(account)); // Делаем запрос и получаем информацию о количестве и суммах кошельков

            foreach (var acc in response.accounts) // для каждого валютного кошелька переносим значения (валюта, сумма) в экземпляр класса 
            {
                account.Wallet.Add((int)acc.balance.currency, (float)acc.balance.amount);
            }
        }
        public string GetNumber(Account account) { return this.Number; }
        public string GetNickname(Account account) { return this.Nickname; }
        public Dictionary<int,float> GetWallets (Account account) { return this.Wallet; }

    }


}
