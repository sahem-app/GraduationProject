using System.Collections.Generic;

namespace GraduationProject.Utilities.AuthenticationConfigurations
{
    public interface IAuthenticationTokenGenerator
    {
        string Token { get; }
        string Generate(string id, string role);
        string Generate(string id, string role, IDictionary<string, string> data);
    }
}
