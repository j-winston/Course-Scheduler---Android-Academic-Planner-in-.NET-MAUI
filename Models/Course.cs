using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c971.Models
{
    using SQLite;

    namespace c971.Models
    {
        public class Course
        {
            [PrimaryKey, AutoIncrement] public int Id { get; set; }
            public string Title { get; set; }
            public DateTime StartDate { get; set; } = DateTime.Now;
            public DateTime EndDate { get; set; } = DateTime.Now;
            public string SelectedStatus { get; set; }
            public string InstructorName { get; set; }
            public string InstructorPhone { get; set; }
            public string InstructorEmail { get; set; }
            public string Notes { get; set; }
            public bool StartDateNotification { get; set; }
            public int StartDateNotificationId { get; set; } = -1; 
            public bool EndDateNotification { get; set; }
            public int EndDateNotificationId { get; set; } = -1;
            public bool IsTestData { get; set; }

            public int TermId { get; set; }

        }
    }

}
