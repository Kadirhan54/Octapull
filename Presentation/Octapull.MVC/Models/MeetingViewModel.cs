namespace Octapull.MVC.Models
{
    public class MeetingViewModel
    {
        public string Name { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
    }
}
