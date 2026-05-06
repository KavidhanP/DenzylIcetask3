using LogiTech.Models;

namespace LogiTech.Services
{
    public interface INotificationRepository
    {
        IReadOnlyList<ShipmentNotification> GetNotifications();
        ShipmentNotification? GetNotification(Guid id);
        void AddNotification(ShipmentNotification n);
        void UpdateNotification(ShipmentNotification n);


    }
}