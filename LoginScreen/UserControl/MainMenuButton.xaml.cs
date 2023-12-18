using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginScreen
{
    /// <summary>
    /// Interaktionslogik für MainMenuButton.xaml
    /// </summary>
    public partial class MainMenuButton : UserControl
    {
        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register("ButtonContent", typeof(string), typeof(MainMenuButton));
        public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(MainMenuButton));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(string), typeof(MainMenuButton));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public event RoutedEventHandler Click
        {
            add { MainButton.Click += value; }
            remove { MainButton.Click -= value; }
        }

        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public MainMenuButton()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {

            ButtonBackground.ImageSource = new BitmapImage(new Uri(@"C:\Users\RAMDAN\OneDrive - zubIT\Bilder\NeonButtonOnnew.png"));
            RobotImage.Visibility = Visibility.Collapsed;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonBackground.ImageSource = new BitmapImage(new Uri(@"C:\Users\RAMDAN\OneDrive - zubIT\Bilder\NeonButtonOffnew.png"));

            RobotImage.Visibility = Visibility.Collapsed;
        }
    }
}
