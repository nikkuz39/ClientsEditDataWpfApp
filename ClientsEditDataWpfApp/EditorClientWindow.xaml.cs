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
using ClientLibrary;

namespace ClientsEditDataWpfApp
{
    /// <summary>
    /// Interaction logic for EditorClientWindow.xaml
    /// </summary>
    public partial class EditorClientWindow : Window
    {
        public Client Client { get; private set; }
        public EditorClientWindow(Client cli)
        {
            InitializeComponent();

            Client = cli;
            this.DataContext = Client;
        }

        private void EditorClientAccept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
