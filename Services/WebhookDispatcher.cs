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
            var activeWebhooks = _repository.GetWebhooks().Where(w => w.IsActive).ToList();

            var payload = new
            {
                eventType = notification.EventType,
                trackingNumber = notification.TrackingNumber,
                customerName = notification.CustomerName,
                notificationStatus = notification.Status.ToString(),
                sentAt = notification.SentAt,
                message = notification.Message
            };

            foreach (var webhook in activeWebhooks)
            {
                var success = false;
                try
                {
                    using var req = new HttpRequestMessage(HttpMethod.Post, webhook.EndpointUrl)
                    {
                        Content = JsonContent.Create(payload)
                    };
                    req.Headers.Add("X-LogiTech-Event", notification.EventType);
                    if (!string.IsNullOrWhiteSpace(webhook.Secret))
                        req.Headers.Add("X-LogiTech-Secret", webhook.Secret);

                    var res = await _http.SendAsync(req);
                    success = res.IsSuccessStatusCode;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Webhook failed for {Url}", webhook.EndpointUrl);
                }
                finally
                {
                    _repository.RecordWebhookAttempt(webhook.Id, success);
                }
            }
        }
    }
}