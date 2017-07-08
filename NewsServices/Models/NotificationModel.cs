using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsServices.Models
{
    /// <summary>
    /// Notification title model
    /// </summary>
    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
    }
    /// <summary>
    /// Notification roo model for hold fcm notification
    /// </summary>
    public class NotificationModel
    {
        public string to { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
        public bool content_available { get; set; }
    }
}