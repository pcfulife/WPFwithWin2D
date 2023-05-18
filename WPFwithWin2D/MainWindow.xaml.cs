using System.ComponentModel;
using System.Windows;

namespace WPFwithWin2D
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public Visibility UWPGraphicsVisibility { get; set; } = Visibility.Visible;

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
