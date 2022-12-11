using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SproutSocial.Application.DTOs.MailDtos;
using SproutSocial.Domain.Entities.Identity;

namespace SproutSocial.Quartz.Jobs;

[DisallowConcurrentExecution]
public class BirthDayJob : IJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IHostingEnvironment _environment;

    public BirthDayJob(IServiceScopeFactory serviceScopeFactory, IHostingEnvironment environment)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _environment = environment;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        DateTime currentDate = DateTime.Now;

        using var scope = _serviceScopeFactory.CreateScope();
        var userManager = scope.ServiceProvider.GetService<UserManager<AppUser>>();
        var users = await userManager.Users.Where(u => u.BirthDay != null 
                && u.BirthDay.Value.Month == currentDate.Month && u.BirthDay.Value.Day == currentDate.Day)
            .ToListAsync();
        if (users is not null && users.Count > 0)
        {
            using (HttpClient client = new())
            {
                foreach (var user in users)
                {
                    MailRequestDto mailRequestDto = new();
                    mailRequestDto.ToEmail = user.Email;
                    mailRequestDto.Subject = "Happy Birthday";
                    mailRequestDto.Body = GetEmailTemplate(user.Fullname ?? user.UserName);

                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("ToEmail", mailRequestDto.ToEmail),
                        new KeyValuePair<string, string>("Subject", mailRequestDto.Subject),
                        new KeyValuePair<string, string>("Body", mailRequestDto.Body)
                    });

                    var response = await client.PostAsync($"{Configuration.BaseUrl}/Mail", formContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseStr = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseStr);
                    }
                    else
                    {
                        Console.WriteLine("Problem occurred when sending email");
                    }
                }
            }
        }

        Console.WriteLine("Job successfully executed");
    }

    private string GetEmailTemplate(string name)
    {
        var filePath = $"{_environment.WebRootPath}/templates/index.html";

        StreamReader streamReader = new StreamReader(filePath);
        string mailText = streamReader.ReadToEnd();
        streamReader.Close();

        mailText = mailText.Replace("[Name]", name);

        return mailText;
    }
}