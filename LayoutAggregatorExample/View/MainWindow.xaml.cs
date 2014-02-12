using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using LayoutAggregatorExample.ViewModel;
using System.IO;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace LayoutAggregatorExample
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = WorkspaceViewModel.Instance;

            Messenger.Default.Register<NotificationMessageAction<string>>(this, notication_message_action_recieved);
            Messenger.Default.Register<NotificationMessage<string>>(this, notification_message_string_received);
        }


        #region Workspace Layout Management
        private void notication_message_action_recieved(NotificationMessageAction<string> message)
        {
            string xmlLayoutString = string.Empty;
            using (StringWriter fs = new StringWriter())
            {
                var xmlLayout = new XmlLayoutSerializer(dockManager);
                xmlLayout.Serialize(fs);
                xmlLayoutString = fs.ToString();
            }
            message.Execute(xmlLayoutString);
        }


        private void notification_message_string_received(NotificationMessage<string> message)
        {
            if (message.Notification == Notifications.LoadWorkspaceLayout)
            {
                StringReader sr = new StringReader(message.Content);
                var layoutSerializer = new XmlLayoutSerializer(dockManager);
                layoutSerializer.LayoutSerializationCallback += UpdateLayout;
                layoutSerializer.Deserialize(sr);
            }
        }


        private static void UpdateLayout(object sender, LayoutSerializationCallbackEventArgs args)
        {
            ViewModelBase content_view_model = WorkspaceViewModel.Instance.ContentViewModelFromID(args.Model.ContentId);
            args.Content = content_view_model;
            if (content_view_model == null)
            {
                args.Cancel = true;
            }
        }
        #endregion
    }
}