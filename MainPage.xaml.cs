using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace DemoBot
{
    public partial class MainPage : ContentPage
	{
		//Make New Bot 
		BotConnection connection = new BotConnection("User");

		//Make new Message List
		ObservableCollection<MessageListItem> messagelist = new ObservableCollection<MessageListItem>();

		public MainPage() 
		{
			InitializeComponent();

            //Bind List View To The Observable Collection
            MessageListView.ItemsSource = messagelist;
                
            //Start Listening to messages and add any new ones to the collection 
            var messageTask = connection.GetMessagesAsync(messagelist);
		}

        public void Send(object sender, EventArgs e)
        {
            //Get Text In Entry 
            var message = ((Entry)sender).Text;

            if(message.Length > 0)
            {
                //Clear entry
                ((Entry)sender).Text = "";
            }

            //Make Object to Be Place In List View
            var messageListItem = new MessageListItem(message, connection.Account.Name);
            messagelist.Add(messageListItem);

            //Send Message To Bot
            connection.SendMessage(message);
        }
	}
}
