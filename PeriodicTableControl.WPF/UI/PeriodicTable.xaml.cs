using PeriodicTableControl.Model;
using PeriodicTableControl.WPF.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PeriodicTableControl.UI
{
    public partial class PeriodicTable : UserControl
    {
        #region Events
        public delegate void ElementSelectionChangedEventHandler(PeriodicTable sender, Element selectedElement, Element previousElement = null);
        public event ElementSelectionChangedEventHandler SelectionChanged;
        public event ElementSelectionChangedEventHandler HoverSelectionChanged;
        #endregion
        #region StyleProperties
        public bool ShowLanthanidesActinides
        {
            get { return (bool)GetValue(ShowLanthanidesActinidesProperty); }
            set { SetValue(ShowLanthanidesActinidesProperty, value); }
        }
        public static readonly DependencyProperty ShowLanthanidesActinidesProperty = DependencyProperty.Register(nameof(ShowLanthanidesActinides), typeof(bool), typeof(PeriodicTable), new PropertyMetadata(false, (sender, e) => (sender as PeriodicTable)?.CreateUI()));

        public bool ShowGridLines
        {
            get { return (bool)GetValue(ShowGridLinesProperty); }
            set { SetValue(ShowGridLinesProperty, value); }
        }
        public static readonly DependencyProperty ShowGridLinesProperty = DependencyProperty.Register(nameof(ShowGridLines), typeof(bool), typeof(PeriodicTable), new PropertyMetadata(false, (sender, e) =>
        {
            if (sender is PeriodicTable My && e.NewValue is bool b) My.MyPeriodicTable.ShowGridLines = b;
        }));

        public bool Wide
        {
            get { return (bool)GetValue(WideProperty); }
            set { SetValue(WideProperty, value); }
        }
        public static readonly DependencyProperty WideProperty = DependencyProperty.Register(nameof(Wide), typeof(bool), typeof(PeriodicTable), new PropertyMetadata(false, (sender, e) => (sender as PeriodicTable)?.CreateUI()));


        #endregion
        readonly IEnumerable<Element> Elements;
        public PeriodicTable()
        {
            Elements = GeneralIO.CreateElements();
            DataContext = this;
            InitializeComponent();
            CreateUI();
        }

        private void CreateUI()
        {
            MyPeriodicTable.Children.Clear();
            MyPeriodicTable.RowDefinitions.Clear();
            MyPeriodicTable.ColumnDefinitions.Clear();
            if (Wide)
            {
                ShowLanthanidesActinides = true;
            }
            for (int i = 0; i < 18; i++)
            {
                MyPeriodicTable.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 7; i++)
            {
                MyPeriodicTable.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            var ElementsLanAc = Elements.Where(x => x.Type == Element.ElementTypes.Actinide || x.Type == Element.ElementTypes.Lanthanide);

            foreach (var item in Elements.Except(ElementsLanAc))
            {
                var element = new PeriodicTableElement(item);
                element.MouseDown += Element_MouseDown;
                element.IsMouseDirectlyOverChanged += Element_IsMouseDirectlyOverChanged;
                MyPeriodicTable.Children.Add(element);
                Grid.SetRow(element, item.Period - 1);
                Grid.SetColumn(element, item.Group - 1);
            }

            if (ShowLanthanidesActinides)
            {
                var LanAcGrid = new Grid();
                LanAcGrid.ShowGridLines = true;
                for (int i = 0; i < 15; i++)
                {
                    LanAcGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                }
                for (int i = 0; i < 2; i++)
                {
                    LanAcGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }
                MyPeriodicTable.Children.Add(LanAcGrid);
                if (Wide)
                {
                    Grid.SetRow(LanAcGrid, 3);
                    Grid.SetColumn(LanAcGrid, 6);
                    MyPeriodicTable.ColumnDefinitions[3].Width = new GridLength(12, GridUnitType.Star);
                }
                else
                {
                    MyPeriodicTable.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                    MyPeriodicTable.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(2, GridUnitType.Star) });
                    Grid.SetRow(LanAcGrid, 10);

                    Grid.SetColumn(LanAcGrid, 3);
                    Grid.SetColumnSpan(LanAcGrid, 18 - 2);
                }
                foreach (var item in ElementsLanAc)
                {
                    var element = new PeriodicTableElement(item);
                    element.MouseDown += Element_MouseDown;
                    element.IsMouseDirectlyOverChanged += Element_IsMouseDirectlyOverChanged;
                    LanAcGrid.Children.Add(element);
                    Grid.SetRow(element, item.Period - 6);
                    Grid.SetColumn(element, item.Group - 3);
                }

            }
        }

        private void Element_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PeriodicTableElement pte)
            {
                HoverSelectionChanged.Invoke(this, pte.MyElement);
            }
        }

        private void Element_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is PeriodicTableElement pte)
            {
                SelectionChanged.Invoke(this, pte.MyElement);
            }
        }
    }
}
