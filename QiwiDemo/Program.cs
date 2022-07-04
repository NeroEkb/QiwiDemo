using QiwiDemo;
using RestSharp;
using Newtonsoft.Json;
using System.Text.Json;
using System.Collections.Generic;

string token = ""; // Здесь ввоится токен
List<Account> AccountList = new List<Account>(); // Создаём Лист аккаунтов
AccountList.Add(await Account.CreateAccount(token)); // Добавляем наш кошелёк
foreach(Account account in AccountList) // Проходим по каждому кошельку в Листе
{
    Console.WriteLine(AccountList[0].Number);
    foreach(KeyValuePair<int, float> wallet in AccountList[0].Wallet)
    {
        Console.WriteLine("currency = {0}, amount = {1}", wallet.Key, wallet.Value);
    }

}


