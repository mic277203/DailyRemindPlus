using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyRemindPlus
{
   public class WeekModel
    {
       public string BillNo { get; set; }
	    public string BillDate { get; set; }
        public string PersonId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Subject { get; set; }
        public string Summarize { get; set; }
        public string MakerId { get; set; }
        public string MakeDate { get; set; }
        public string ModifierId { get; set; }
        public string ModifyDate { get; set; }
        public string InternalId { get; set; }
        public string PersonName { get; set; }
        public string DeptName { get; set; }
        public string PositionName { get; set; }
        public string Kind { get; set; }
        public string VirSummarize { get; set; }
        public string VirSummarizePic { get; set; }
        public string ToUserIds { get; set; }
        public string CcUserIds { get; set; }
        public string MailTitle { get; set; }
    }
}
