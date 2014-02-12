using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Linq;

namespace LayoutAggregatorExample.ViewModel
{
    public class WorkspaceViewModel : ViewModelBase
    {
        #region Properties
        public RelayCommand SaveWorkspaceLayoutCommand { get; private set; }

        public RelayCommand LoadWorkspaceLayoutCommand { get; private set; }

        public ObservableCollection<AnchorableVM> AnchorableVMs { get; private set; }

        public ObservableCollection<DocumentVM> DocumentVMs { get; private set; } 
        #endregion


        public static WorkspaceViewModel Instance = new WorkspaceViewModel();
        private WorkspaceViewModel()
        {
            AnchorableVMs = new ObservableCollection<AnchorableVM>();
            DocumentVMs = new ObservableCollection<DocumentVM>();

            AnchorableVMs.Add(new AnchorableVM("Anchorable1", "Anchorable1"));
            AnchorableVMs.Add(new AnchorableVM("Anchorable2", "Anchorable2"));
            AnchorableVMs.Add(new AnchorableVM("Anchorable3", "Anchorable3"));

            DocumentVMs.Add(new DocumentVM("Doc1", "Doc1"));
            DocumentVMs.Add(new DocumentVM("Doc2", "Doc2"));
            DocumentVMs.Add(new DocumentVM("Doc3", "Doc3"));

            SaveWorkspaceLayoutCommand = new RelayCommand(SaveWorkspaceLayout);
            LoadWorkspaceLayoutCommand = new RelayCommand(LoadWorkspaceLayout, () => !string.IsNullOrEmpty(current_layout)); 
        }


        #region Workspace Managment
        public ViewModelBase ContentViewModelFromID(string content_id)
        {
            var document_vm = DocumentVMs.FirstOrDefault(d => d.ContentId == content_id);
            if (document_vm != null)
                return document_vm;

            var anchorable_vm = AnchorableVMs.FirstOrDefault(d => d.ContentId == content_id);
            if (anchorable_vm != null)
                return anchorable_vm;

            return null;
        }


        void SaveWorkspaceLayout()
        {
            Messenger.Default.Send(new NotificationMessageAction<string>(Notifications.GetWorkspaceLayout, result => current_layout = result));
        }


        void LoadWorkspaceLayout()
        {
            if (current_layout != null)
            {
                Messenger.Default.Send(new NotificationMessage<string>(current_layout, Notifications.LoadWorkspaceLayout));
            }
        } 
        #endregion


        private string current_layout;
    }
}