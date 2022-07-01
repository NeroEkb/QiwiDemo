using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QiwiDemo;

namespace QiwiDemo
{
    public class Wallet
    {
        public string Number { get; set; } // для хранения номера телефона

        public string Token { get; set; } // для токена

        public string UserInfo { get; set; } // на всякий случай
        // хоба, коллекция <ключ, значение> для хранения неопределенного количества кошельков и их суммы
        public Dictionary<int, float> Currency { get; set; } = new Dictionary<int, float>(); 



        public Wallet(string token) // при создании обьекта обязан добавить токен, иначе ЭКСЕПШН!!
        {
            this.Token = token;
        }

    }


}
