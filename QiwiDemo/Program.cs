using QiwiDemo;
using RestSharp;
using Newtonsoft.Json;
using System.Text.Json;
using System.Collections.Generic;

string token = ""; // Здесь ввоится токен
List<Wallet> WalletList = new List<Wallet>(); // Создаём Лист аккаунтов
WalletList.Add(new Wallet(token)); // Добавляем наш кошелёк
foreach(Wallet wallet in WalletList) // Проходим по каждому кошельку в Листе
{
    Request requester = new Request(wallet); // Создаём запрос
    var kek = JsonConvert.DeserializeObject<dynamic>( await requester.MakeRequestForNumber(wallet)); // Получаем в переменную результат ответа от сервера
    wallet.Number = kek.contractInfo.contractId.ToString(); // переносим данные (номер телефона) из результата в экземпляр класса 
    var k = JsonConvert.DeserializeObject<dynamic>(await requester.MakeRequestForBalance(wallet)); // Делаем запрос и получаем информацию о количестве и суммах кошельков

    foreach (var account in k.accounts) // для каждого валютного кошелька переносим значения (валюта, сумма) в экземпляр класса 
    {
       wallet.Currency.Add(account.balance.currency, account.balance.amount);
    }

    Console.WriteLine(wallet.Currency);

}

Console.WriteLine(WalletList[0].Number);
