﻿using System.ComponentModel.DataAnnotations;

namespace GraduationProject.Models
{
	public class Notification
	{
		public int Id { get; set; }

		[Required, MaxLength(4000)]
		public string Title { get; set; }

		[Required, MaxLength(4000)]
		public string Body { get; set; }

		public bool IsRead { get; set; }

		public Mediator Mediator { get; set; }
		public int MediatorId { get; set; }

		public NotificationType Type { get; set; }
		public byte TypeId { get; set; }

		public int TaskId { get; set; }
	}
}
