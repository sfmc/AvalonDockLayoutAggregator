using GalaSoft.MvvmLight;

namespace LayoutAggregatorExample.ViewModel
{
    public class AnchorableVM : ViewModelBase
    {
        public string Title { get; private set; }
        public string ContentId { get; private set; }


        public AnchorableVM(string title, string content_id)
        {
            Title = title;
            ContentId = content_id;
        }
    }
}