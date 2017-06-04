using System;
namespace DemoBot
{
    public class MessageListItem
    {
        public string Text { get; set; }
        public string Sender { get; set; }

        public MessageListItem(String text, string sender)
        {
            Text = text;
            Sender = sender;
        }
    }
}
