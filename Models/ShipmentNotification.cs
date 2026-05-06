namespace LogiTech.Models
{
    public enum NotificationChannel { Sms, Email }
    public enum NotificationStatus { Pending, Sent, Failed }

    public class ShipmentNotification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string TrackingNumber { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty;
        public NotificationChannel Channel { get; set; }
        public string Recipient { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationStatus Status { get; set; } = NotificationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? SentAt { get; set; }
        public string? ErrorMessage { get; set; }
    }
}