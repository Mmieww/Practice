namespace zadanie1_19.Models
{
    public class StudyTask
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime DeadLine { get; set; }
        public bool Completed { get; set; }
        public int Priority { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
