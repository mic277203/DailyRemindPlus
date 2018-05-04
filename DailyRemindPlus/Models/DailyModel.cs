using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyRemindPlus
{
    /// <summary>
    /// 日报模型
    /// </summary>
    public class DailyModel
    {
        public string BillNo { get; set; }
        public string BillDate { get; set; }
        public string PersonId { get; set; }
        public string MakerId { get; set; }
        public string MakeDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifyDate { get; set; }
        public string InternalId { get; set; }
        public string PersonName { get; set; }
        public string DeptName { get; set; }
        public string PositionName { get; set; }
        public string ToUserIds { get; set; }
        public string CcUserIds { get; set; }
        public string MailTitle { get; set; }
    }
}
