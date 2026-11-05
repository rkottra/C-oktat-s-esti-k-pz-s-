using System.Windows;

namespace BindingElemekKozott;

// Nincs eseménykezelő: a vezérlők közötti kapcsolatot a XAML-beli Binding-ok adják.
// (Az előző órán ugyanezt ValueChanged és TextChanged eseményekkel, kézzel kellett volna megírni.)
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
