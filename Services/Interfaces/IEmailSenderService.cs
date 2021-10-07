using System.Threading.Tasks;

namespace webapp.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task Execute(string subject, string message, string emailTo, string emailFrom, string name);
    }
}
