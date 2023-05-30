using BBQ_Schedule.Domain.Models;

namespace BBQ_Schedule.Domain.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
