using SQLite;

namespace c971.Models
{
    public class Assessment
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public string SelectedType { get; set; }
        public bool StartDateNotification { get; set; }
        public int StartDateNotificationId { get; set; } = -1;
        public bool EndDateNotification { get; set; }
        public int EndDateNotificationId { get; set; } = -1;
        public int CourseId { get; set; }
        public bool IsTestData { get; set; }
    }
}