using DailyRemindPlus.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyRemindPlus.Extentions
{
    public class UsersConfig
    {
        public static List<User> GetAllUsers()
        {
            string json = File.ReadAllText("Users.json", Encoding.UTF8);

            return JsonConvert.DeserializeObject<List<User>>(json);
        }
    }
}
