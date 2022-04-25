﻿namespace GraduationProject.Utilities.CustomApiResponses
{
	public class SuccessWithPagination : Success
	{
		public Pagination Pagination { get; set; }

		public SuccessWithPagination(object obj, Pagination pagination) : base(obj)
		{
			Pagination = pagination;
		}
	}
}
