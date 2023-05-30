using BBQ_Schedule.Domain.Interfaces;
using BBQ_Schedule.Domain.Models;

namespace BBQ_Schedule.Domain.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new(1);
        }
        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
            _notifications.Capacity += 1;
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
