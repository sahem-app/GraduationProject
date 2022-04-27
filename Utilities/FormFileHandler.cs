﻿using System.IO;
using Microsoft.AspNetCore.Http;

namespace GraduationProject.Utilities
{
	public static class FormFileHandler
	{
		public static byte[] ConvertToBytes(IFormFile file)
		{
			using var stream = new MemoryStream();
			file.CopyTo(stream);
			return stream.ToArray();
		}
	}
}
