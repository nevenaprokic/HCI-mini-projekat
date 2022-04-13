using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_mini_projekat
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        void SetProperties()
        {
            this.Title = "Error message";

            Uri iconUri = new Uri("../../images/warning.png", UriKind.RelativeOrAbsolute);
            this.Icon = BitmapFrame.Create(iconUri);
        }

        public MessageWindow(Window parent, string message)
        {
            InitializeComponent();
            SetProperties();
            Uri iconUri = new Uri("../../images/warning.png", UriKind.RelativeOrAbsolute);
            imegePicture.Source = BitmapFrame.Create(iconUri);
            labelMessage.Content = message;
            Canvas.SetLeft(this, parent.Left + parent.Width/2.5);
            Canvas.SetTop(this, parent.Top + parent.Height/2.3);
        }
        private void CloseHandler(object sender, RoutedEventArgs e) =>
            Close();
   }
}
