using System;
using Microsoft.Bot.Connector.DirectLine;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
namespace DemoBot
{
    public class BotConnection
    {
        public DirectLineClient Client = new DirectLineClient("3hjHxPgQIUs.cwA.SIM.IiQVBuFt5j5uu4M09PyItDbOm-U-BGUSc2eamkm_tz8");
        public Conversation MainConversation;
        public ChannelAccount Account;

		public BotConnection(String accountName)
		{
			MainConversation = Client.Conversations.StartConversation();
			Account = new ChannelAccount { Id = accountName, Name = accountName };
		}

        public void SendMessage(String message)
        {
            Activity activity = new Activity
            {
                From = Account,
                Text = message,
                Type = ActivityTypes.Message
            };
            Client.Conversations.PostActivity(MainConversation.ConversationId, activity);
        }
		public async Task GetMessagesAsync(ObservableCollection<MessageListItem> collection)
		{
			string watermark = null;

			//Loop retrieval
			while (true)
			{
				Debug.WriteLine("Reading Message Every 1 seconds");

				//Get activities (messages) after the watermark
				var activitySet = await Client.Conversations.GetActivitiesAsync(MainConversation.ConversationId, watermark);

				//Set new watermark
				watermark = activitySet?.Watermark;

				//Loop through the activities and add them to the list
				foreach (Activity activity in activitySet.Activities)
				{
					if (activity.From.Name == "DemoBot")
					{
						collection.Add(new MessageListItem(activity.Text, activity.From.Name));
					}
				}

				//Wait 1 seconds
				await Task.Delay(1000);
			}
		}
    }
}
