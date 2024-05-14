namespace TaskManager.Models
{
    public class TaskModel : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
