namespace GraduationProject.Utilities.CustomApiResponses
{
	public interface IApiResponse
	{
		public byte Status { get; }
		public string Message { get; }
	}
}
