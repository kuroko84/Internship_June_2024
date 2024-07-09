namespace Student_Management.ViewModels
{
    public class StudentInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string ClassName { get; set; }
        public List<ScoreInfo> Scores { get; set; }
        public double AverageMark { get; set; }
    }

    public class ScoreInfo
    {
        public string SubjectName { get; set; }
        public double Mark { get; set; }
    }
}
