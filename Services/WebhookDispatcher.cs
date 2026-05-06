using System.Net.Http.Json;
using LogiTech.Models;

namespace LogiTech.Services
{
    public class WebhookDispatcher
    {
        private readonly HttpClient _http;
        private readonly INotificationRepository _repository;
        private readonly ILogger<WebhookDispatcher> _logger;

        public WebhookDispatcher(HttpClient http, INotificationRepository repository,
            ILogger<WebhookDispatcher> logger)
        {
            _http = http;
            _repository = repository;
            _logger = logger;
        }

        public async Task DispatchAsync(ShipmentNotification notification)
        {
          

            var payload = new
            {
                eventType = notification.EventType,
                trackingNumber = notification.TrackingNumber,
                customerName = notification.CustomerName,
                notificationStatus = notification.Status.ToString(),
                sentAt = notification.SentAt,
                message = notification.Message
            };

           
        }
    }
}