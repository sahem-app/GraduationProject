using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace GraduationProject.Utilities.NotificationsManagement
{
	public class NotificationHandler
	{
		private readonly Notification _notification;

		public NotificationHandler(string title, string body)
		{
			_notification = new Notification(title, body);
		}

		public async Task SendAsync(string token, string key)
		{
			var content = InitContent(token);
			using (var client = CreateHttpClient(key))
				await MakeRequestAsync(client, content);
		}

		public async Task SendAsync(string[] tokens, string key)
		{
			using (var client = CreateHttpClient(key))
			{
				foreach (var token in tokens)
				{
					var content = InitContent(token);
					await MakeRequestAsync(client, content);
				}
			}
		}

		private static HttpClient CreateHttpClient(string key)
		{
			var client = new HttpClient();
			client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", key);
			return client;
		}

		private JsonContent InitContent(string token)
		{
			return JsonContent.Create(new
			{
				To = token,
				Data = new
				{
					Notification = _notification
				}
			});
		}

		private static async Task MakeRequestAsync(HttpClient client, JsonContent content)
		{
			var response = await client.PostAsJsonAsync("https://fcm.googleapis.com/fcm/send", content.Value);
			response.Dispose();
			//var responseMessage = await response.Content.ReadAsStringAsync();
			//System.Console.WriteLine(responseMessage);
		}
	}
}
