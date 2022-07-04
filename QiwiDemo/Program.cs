using QiwiDemo;
using RestSharp;
using Newtonsoft.Json;
using System.Text.Json;
using System.Collections.Generic;


List<Account> AccountList = new List<Account>(); // Создаём Лист аккаунтов
Console.WriteLine("Input token and press 'Enter', when done press 'Enter' again");
string token;
while(true)
{
    token = Console.ReadLine();
    if (token.Length == 0) { Console.WriteLine("Empty string"); break; }
    else
    {
        AccountList.Add(await Account.CreateAccount(token));
    }
    

}


foreach(Account account in AccountList) // Проходим по каждому кошельку в Листе
{
    Console.WriteLine("Nickname = {0}, Number = {1}", account.Number, account.Nickname);
    foreach(KeyValuePair<int, float> wallet in account.Wallet)
    {
        Console.WriteLine("currency = {0}, amount = {1}", wallet.Key, wallet.Value);
    }

}

Console.ReadKey();
