namespace learning_pract.Models
{
    public class Audit
    {
        public string Day { get; set; }
        public int Num { get; set; }

        public string Subject
        {
            get => subject;
        }

        public string subject;

        public string Auditory
        {
            get => auditory;
        }

        public string auditory;

        public string Group
        {
            get => group;
        }

        public string group;

        public Audit(Schedule schedule)
        {
            auditory = schedule.Auditory.num.ToString();
            group = schedule.Group.name;
            subject = schedule.Subject.Theme;
            Num = schedule.Num;
            Day = schedule.Day;
        }
    }
}