using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1.Notification
{
	public class NotificationRequest : INotification
	{
		public string Message { get; set; }
	}

	public class Notification1 : INotificationHandler<NotificationRequest>
	{
		public Task Handle(NotificationRequest notification, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Notification 1 service received a notification : {notification.Message}");
			return Task.CompletedTask;
		}
	}

	public class Notification2 : INotificationHandler<NotificationRequest>
	{
		public Task Handle(NotificationRequest notification, CancellationToken cancellationToken)
		{
			Console.WriteLine($"Notification 2 service received a notification : {notification.Message}");
			return Task.CompletedTask;
		}
	}
}
