using System.Collections.Generic;

namespace GraduationProject.Utilities.AuthenticationConfigurations
{
	public interface IAuthenticationTokenGenerator
	{
		string Token { get; }
		string Generate(string id);
		string Generate(string id, IDictionary<string, string> data);
	}
}
