using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyRemindPlus
{
    /// <summary>
    /// 公共常亮
    /// </summary>
    public class ConstTag
    {
        /// <summary>
        /// 登录地址
        /// </summary>
        public const string LoginUrl = "http://192.168.1.7/EIPDevManager/Login/UceLogin?ExcuteProcInfo=eyJQcm9nSWQiOiJEZXZMb2dpbiIsIkhhbmRsZSI6Ii0yIiwiUGFnZUlkIjoiNTM2NDA1MDUxNTE0NDUyOTA0MSJ9";
        
        /// <summary>
        /// 日报地址
        /// </summary>
        public const string DailyUrl = "http://192.168.1.7/EIPDevManager/QueryPage/GetQueryList?ExcuteProcInfo=eyJQcm9nSWQiOiJEZXZEYWlseSIsIkhhbmRsZSI6IjEwOTk3Njc1NTQiLCJQYWdlSWQiOiI0NjQwOTM0NDYyMzk4OTgyODU0In0=&readOptions=%7B%22take%22%3A20%2C%22skip%22%3A0%2C%22page%22%3A1%2C%22pageSize%22%3A20%2C%22filter%22%3A%7B%22logic%22%3A%22or%22%2C%22filters%22%3A%5B%7B%22field%22%3A%22A.PersonName%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3A%22%E6%9E%97%E5%9F%B9%E5%8D%8E%22%7D%2C%7B%22field%22%3A%22A.PositionName%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3A%22%E6%9E%97%E5%9F%B9%E5%8D%8E%22%7D%2C%7B%22field%22%3A%22A.BillDate%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3Anull%7D%5D%7D%7D&_=1525232237392";

        /// <summary>
        /// 周报地址
        /// </summary>
        public const string WeekUrl = "http://192.168.1.7/EIPDevManager/QueryPage/GetQueryList?ExcuteProcInfo=eyJQcm9nSWQiOiJEZXZXZWVrbHkiLCJIYW5kbGUiOiIxNjc2Nzk3NzI0IiwiUGFnZUlkIjoiNTAwMTE0NTk0Mzk3MjI0NTk5MCJ9&readOptions=%7B%22take%22%3A20%2C%22skip%22%3A0%2C%22page%22%3A1%2C%22pageSize%22%3A20%2C%22filter%22%3A%7B%22logic%22%3A%22or%22%2C%22filters%22%3A%5B%7B%22field%22%3A%22A.PersonName%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3A%22%E6%9E%97%E5%9F%B9%E5%8D%8E%22%7D%2C%7B%22field%22%3A%22A.PositionName%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3A%22%E6%9E%97%E5%9F%B9%E5%8D%8E%22%7D%2C%7B%22field%22%3A%22A.StartDate%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3Anull%7D%2C%7B%22field%22%3A%22A.EndDate%22%2C%22operator%22%3A%22contains%22%2C%22value%22%3Anull%7D%5D%7D%7D&_=1525421489821";
    }
}
