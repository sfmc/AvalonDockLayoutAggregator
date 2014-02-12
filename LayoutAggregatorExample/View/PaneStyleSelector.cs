using LayoutAggregatorExample.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace LayoutAggregatorExample.View
{
    class PanesStyleSelector : StyleSelector
    {
        public Style AnchorableStyle { get; set; }

        public Style DocumentStyle { get; set; }


        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            if (item is DocumentVM)
                return DocumentStyle;

            if (item is AnchorableVM)
                return AnchorableStyle;

            return base.SelectStyle(item, container);
        }
    }
}