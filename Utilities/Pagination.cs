namespace GraduationProject.Utilities
{
	public class Pagination
	{
		public const int PerPageElements = 10;
		public int CurrentPage { get; set; }
		public int LastPage { get; set; }
		public int PerPage { get; set; } = PerPageElements;
		public int Total { get; set; }

		public Pagination()
		{

		}

		public Pagination(int currentPage, int lastPage, int total)
		{
			CurrentPage = currentPage;
			LastPage = lastPage;
			Total = total;
		}
	}
}
