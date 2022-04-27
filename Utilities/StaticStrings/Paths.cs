using System.Text;
using Microsoft.AspNetCore.Http;

namespace GraduationProject.Utilities.StaticStrings
{
	public static class Paths
	{
		private static string _common;
		private const string _profilePicture = "/api/mediators/profile-image/";
		private const string _nationalIdImage = "/api/mediators/nationalid-image/";
		private const string _caseImage = "/api/cases/images/";
		public static HttpRequest Request { get; set; }

		private static StringBuilder InitCommon()
		{
			if (string.IsNullOrWhiteSpace(_common))
				_common = new StringBuilder(Request.Scheme).Append("://").Append(Request.Host).Append(Request.PathBase.ToString()).ToString();

			return new StringBuilder(_common);
		}

		public static string ProfilePicture(int id)
		{
			return InitCommon().Append(_profilePicture).Append(id).ToString();
		}

		public static string NationalIdImage(int id)
		{
			return InitCommon().Append(_nationalIdImage).Append(id).ToString();
		}

		public static string CaseImage(int id)
		{
			return InitCommon().Append(_caseImage).Append(id).ToString();
		}
	}
}
