namespace HangfireJob
{
    public class JobService
    {
        public void SendEmail()
        {
            Console.WriteLine("Email sent successfully!");
        }

        public void GenerateReport()
        {
            Console.WriteLine("Report generated!");
        }

        public void CleanUp()
        {
            Console.WriteLine("Cleanup task executed!");
        }
    }
}
