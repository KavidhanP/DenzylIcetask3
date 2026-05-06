using LogiTech.Models;

namespace LogiTech.Services
{
    public interface INotificationRepository
    {
        IReadOnlyList<ShipmentNotification> GetNotifications();
        ShipmentNotification? GetNotification(Guid id);
        void AddNotification(ShipmentNotification n);
        void UpdateNotification(ShipmentNotification n);

        IReadOnlyList<WebhookSubscription> GetWebhooks();
        WebhookSubscription? GetWebhook(Guid id);
        void AddWebhook(WebhookSubscription w);
        void DisableWebhook(Guid id);
        void RecordWebhookAttempt(Guid webhookId, bool successful);
    }
}