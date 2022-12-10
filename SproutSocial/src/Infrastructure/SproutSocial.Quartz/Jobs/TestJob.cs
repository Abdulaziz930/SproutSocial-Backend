using Newtonsoft.Json;
using Quartz;
using SproutSocial.Application.DTOs.MailDtos;

namespace SproutSocial.Quartz.Jobs;

[DisallowConcurrentExecution]
public class TestJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        using (HttpClient client = new())
        {
            MailRequestDto mailRequestDto = new();
            mailRequestDto.ToEmail = "karimliabdulaziz5@gmail.com";
            mailRequestDto.Subject = "TestSubject";
            mailRequestDto.Body = "TestBody";

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
