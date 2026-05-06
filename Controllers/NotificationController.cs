using LogiTech.Models;
using LogiTech.Services;
using Microsoft.AspNetCore.Mvc;

namespace LogiTech.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _repository;
        private readonly WebhookDispatcher _webhookDispatcher;

        public NotificationController(INotificationRepository repository,
            WebhookDispatcher webhookDispatcher)
        {
            _repository = repository;
            _webhookDispatcher = webhookDispatcher;
        }

        // GET: /Notification
        public IActionResult Index() => View();

        // GET: api/notifications
        [HttpGet("/api/notifications")]
        public IActionResult GetAll() => Ok(_repository.GetNotifications());

        // GET: api/notifications/kpis
        [HttpGet("/api/notifications/kpis")]
        public IActionResult GetKpis()
        {
            var all = _repository.GetNotifications();
            var sent = all.Count(n => n.Status == NotificationStatus.Sent);
            var total = all.Count;
            return Ok(new
            {
                notificationsSent = sent,
                deliveryRate = total == 0 ? 0 : Math.Round((double)sent / total * 100, 1)
            });
        }

        // POST: api/notifications
        [HttpPost("/api/notifications")]
        public async Task<IActionResult> Create([FromBody] CreateNotificationRequest req)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var notification = new ShipmentNotification
            {
                TrackingNumber = req.TrackingNumber.Trim(),
                CustomerName = req.CustomerName.Trim(),
                EventType = req.EventType.Trim(),
                Channel = req.Channel,
                Recipient = req.Recipient.Trim(),
                Message = BuildMessage(req.TrackingNumber, req.CustomerName, req.EventType),
                Status = NotificationStatus.Pending
            };

            _repository.AddNotification(notification);

            var success = !string.IsNullOrWhiteSpace(notification.Recipient);
            notification.Status = success ? NotificationStatus.Sent : NotificationStatus.Failed;
            notification.SentAt = success ? DateTime.UtcNow : null;
            notification.ErrorMessage = success ? null : "Recipient was missing or invalid.";
            _repository.UpdateNotification(notification);

            await _webhookDispatcher.DispatchAsync(notification);

            return Ok(notification);
        }

        private static string BuildMessage(string tracking, string name, string eventType) =>
            eventType.ToLower() switch
            {
                "order confirmed" => $"Hi {name}, your shipment {tracking} has been confirmed.",
                "dispatched" => $"Hi {name}, your shipment {tracking} has been dispatched.",
                "out for delivery" => $"Hi {name}, your shipment {tracking} is out for delivery today.",
                "delivered" => $"Hi {name}, your shipment {tracking} has been delivered.",
                "delayed" => $"Hi {name}, your shipment {tracking} has been delayed.",
                _ => $"Hi {name}, your shipment {tracking} status changed to: {eventType}."
            };
    }

    public class CreateNotificationRequest
    {
        public string TrackingNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public NotificationChannel Channel { get; set; }
        public string Recipient { get; set; } = string.Empty;
    }
}