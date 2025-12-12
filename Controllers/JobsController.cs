using Hangfire;
using HangfireJob;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly JobService _jobService;
    private readonly EmailService _emailService;

    public JobsController(JobService jobService,EmailService emailService)
    {
        _jobService = jobService;
        _emailService = emailService;
    }

    [HttpGet("fire-and-forget")]
    public IActionResult FireAndForgetJob()
    {
        BackgroundJob.Enqueue(() =>_emailService.SendEmailAsync("shh@gmail.com","hello","mail"));
        return Ok("Fire-and-forget job executed!");
    }

    [HttpGet("delayed")]
    public IActionResult DelayedJob()
    {
        for (int i = 1; i <= 10; i++)
        {
            BackgroundJob.Schedule(
                () => _emailService.SendEmailAsync(
                    "shh@gmail.com",
                    $"Hello #{i}",
                    $"This is email number {i}"
                ),
                TimeSpan.FromSeconds(10*i) 
            );
        }

        return Ok("10 delayed email jobs scheduled!");
    }


    [HttpGet("recurring")]
    public IActionResult RecurringJobs()
    {
        RecurringJob.AddOrUpdate("cleanup-job", () => _jobService.CleanUp(), Cron.Minutely);
        return Ok("Recurring job scheduled (runs every minute)");
    }
}
