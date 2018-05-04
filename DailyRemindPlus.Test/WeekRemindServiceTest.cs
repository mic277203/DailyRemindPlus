using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DailyRemindPlus.Test
{
    [TestFixture]
    public class WeekRemindServiceTest
    {
        WeekRemindService service;

        [SetUp]
        public void Init()
        {
            service = new WeekRemindService();
        }

        [Test]
        public void CheckTest()
        {
            service.Check();
        }
    }
}
