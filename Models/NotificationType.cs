﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GraduationProject.Models
{
	public class NotificationType
	{
		public byte Id { get; set; }

		[Required, MaxLength(50), Column(TypeName = "varchar")]
		public string Name { get; set; }
	}
}
