namespace TaskManager.Utils
{
    public static class TaskStatusUtil
    {
        static string[] statuses = { "Открыто", "В работе", "Закрыто" };

        public static string GetFirstStatusOfTask()
        {
            return statuses[0];
        }

        public static bool IsValidTaskStatus(string status)
        {
            return statuses.Contains(status);
        }

        public static string GetStatuses(string sep = "\n")
        {
            return String.Join(sep, statuses);
        }
    }
}
