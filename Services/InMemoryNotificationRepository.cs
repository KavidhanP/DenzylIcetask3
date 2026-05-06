using LogiTech.Models;

namespace LogiTech.Services
{
    public class InMemoryNotificationRepository : INotificationRepository
    {
        private readonly List<ShipmentNotification> _notifications = new();
        private readonly List<WebhookSubscription> _webhooks = new();
        private readonly object _lock = new();

        public InMemoryNotificationRepository()
        {
            _notifications.AddRange(new[]
            {
                new ShipmentNotification
                {
                    TrackingNumber = "LOG-9824-01",
                    CustomerName   = "Aiden Naidoo",
                    EventType      = "Out for Delivery",
                    Channel        = NotificationChannel.Sms,
                    Recipient      = "+27821234567",
                    Message        = "Your parcel LOG-9824-01 is out for delivery.",
                    Status         = NotificationStatus.Sent,
                    SentAt         = DateTime.UtcNow.AddMinutes(-20)
                },
                new ShipmentNotification
                {
                    TrackingNumber = "LOG-8721-04",
                    CustomerName   = "Sarah Lee",
                    EventType      = "Order Confirmed",
                    Channel        = NotificationChannel.Email,
                    Recipient      = "sarah@example.com",
                    Message        = "Your shipment LOG-8721-04 has been confirmed.",
                    Status         = NotificationStatus.Sent,
                    SentAt         = DateTime.UtcNow.AddMinutes(-40)
                },
                new ShipmentNotification
                {
                    TrackingNumber = "LOG-5561-88",
                    CustomerName   = "Mike Chen",
                    EventType      = "Status Update",
                    Channel        = NotificationChannel.Sms,
                    Recipient      = "+27829876543",
                    Message        = "Your shipment LOG-5561-88 status has been updated.",
                    Status         = NotificationStatus.Pending
                }
            });

            _webhooks.Add(new WebhookSubscription
            {
                ClientName = "Demo Enterprise Client",
                EndpointUrl = "https://webhook.site/demo-endpoint",
                Secret = "demo-secret",
                TriggerCount = 43
            });
        }

        public IReadOnlyList<ShipmentNotification> GetNotifications()
        {
            lock (_lock) return _notifications.OrderByDescending(n => n.CreatedAt).ToList();
        }

        public ShipmentNotification? GetNotification(Guid id)
        {
            lock (_lock) return _notifications.FirstOrDefault(n => n.Id == id);
        }

        public void AddNotification(ShipmentNotification n)
        {
            lock (_lock) _notifications.Add(n);
        }

        public void UpdateNotification(ShipmentNotification n)
        {
            lock (_lock)
            {
                var i = _notifications.FindIndex(x => x.Id == n.Id);
                if (i >= 0) _notifications[i] = n;
            }
        }

        public IReadOnlyList<WebhookSubscription> GetWebhooks()
        {
            lock (_lock) return _webhooks.OrderByDescending(w => w.CreatedAt).ToList();
        }

        public WebhookSubscription? GetWebhook(Guid id)
        {
            lock (_lock) return _webhooks.FirstOrDefault(w => w.Id == id);
        }

        public void AddWebhook(WebhookSubscription w)
        {
            lock (_lock) _webhooks.Add(w);
        }

        public void DisableWebhook(Guid id)
        {
            lock (_lock)
            {
                var w = _webhooks.FirstOrDefault(x => x.Id == id);
                if (w is not null) w.IsActive = false;
            }
        }

        public void RecordWebhookAttempt(Guid webhookId, bool successful)
        {
            lock (_lock)
            {
                var w = _webhooks.FirstOrDefault(x => x.Id == webhookId);
                if (w is null) return;
                w.TriggerCount++;
                if (!successful) w.FailedCount++;
            }
        }
    }
}