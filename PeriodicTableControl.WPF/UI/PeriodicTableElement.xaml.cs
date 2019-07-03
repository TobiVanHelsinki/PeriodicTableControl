using PeriodicTableControl.Model;
using System.Windows;
using System.Windows.Controls;

namespace PeriodicTableControl.UI
{
    public partial class PeriodicTableElement : UserControl
    {
        public Element MyElement
        {
            get { return (Element)GetValue(MyElementProperty); }
            set { SetValue(MyElementProperty, value); }
        }
        public static readonly DependencyProperty MyElementProperty = DependencyProperty.Register(nameof(MyElement), typeof(Element), typeof(PeriodicTableElement), new PropertyMetadata(null));

        public PeriodicTableElement(Element item)
        {
            DataContext = this;
            MyElement = item;
            InitializeComponent();
        }

    }
}
