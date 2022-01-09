using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace WPFwithWin2D
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public Visibility WPFGraphicsVisibility { get; set; } = Visibility.Visible;

        public Visibility UWPGraphicsVisibility { get; set; } = Visibility.Collapsed;

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            WPFGraphicsVisibility = WPFGraphicsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(WPFGraphicsVisibility)));

            UWPGraphicsVisibility = UWPGraphicsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(UWPGraphicsVisibility)));
        }
    }
}
