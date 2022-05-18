using Twilio.Rest.Api.V2010.Account;

namespace GraduationProject.Services
{
    public interface ISMSService
    {
        MessageResource Send(string mobileNumber, string body);
    }
}
