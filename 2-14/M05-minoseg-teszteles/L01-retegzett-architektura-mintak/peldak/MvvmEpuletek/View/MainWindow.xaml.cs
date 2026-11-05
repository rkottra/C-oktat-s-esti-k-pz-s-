using System.Windows;
using MvvmEpuletek.ViewModel;

namespace MvvmEpuletek.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // MVVM-ben a code-behind szinte üres: csak összekötjük a nézetet a ViewModellel.
        DataContext = new EpuletekViewModel();
    }
}
