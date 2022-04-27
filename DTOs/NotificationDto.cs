﻿namespace GraduationProject.DTOs
{
	public class NotificationDto
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public byte TypeId { get; set; }

		public NotificationDto(string title, string body, byte typeId)
		{
			Title = title;
			Body = body;
			TypeId = typeId;
		}
	}
}
