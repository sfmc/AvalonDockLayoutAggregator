using GalaSoft.MvvmLight;

namespace LayoutAggregatorExample.ViewModel
{
    public class DocumentVM : ViewModelBase
    {
        public string Title { get; private set; }
        public string ContentId { get; private set; }


        public DocumentVM(string title, string content_id)
        {
            Title = title;
            ContentId = content_id;
        }
    }
}