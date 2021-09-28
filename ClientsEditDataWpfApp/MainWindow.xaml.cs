using Microsoft.EntityFrameworkCore;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientLibrary;
using Microsoft.Win32;
using System.Xml;

namespace ClientsEditDataWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Clients.Load();

                    this.DataContext = db.Clients.Local.ToBindingList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClientAdd_Click(object sender, RoutedEventArgs e)
        {            
            EditorClientWindow editorClientWindow = new EditorClientWindow(new Client());

            if (editorClientWindow.ShowDialog() == true)
            {
                Client client = editorClientWindow.Client;

                try
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        db.Clients.Add(client);
                        db.SaveChanges();

                        clientsList.ItemsSource = db.Clients.ToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ClientEdit_Click(object sender, RoutedEventArgs e)
        {
            if (clientsList.SelectedItem == null) return;

            Client client = clientsList.SelectedItem as Client;

            EditorClientWindow editorClientWindow = new EditorClientWindow(new Client
            {
                Id = client.Id,
                CardCode = client.CardCode,
                StartDate = client.StartDate,
                FinishDate = client.FinishDate,
                Lastname = client.Lastname,
                Firstname = client.Firstname,
                Surname = client.Surname,
                Gender = client.Gender,
                Birthday = client.Birthday,
                PhoneHome = client.PhoneHome,
                PhoneMobil = client.PhoneMobil,
                Email = client.Email,
                City = client.City,
                Street = client.Street,
                House = client.House,
                Apartment = client.Apartment,
            });

            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (editorClientWindow.ShowDialog() == true)
                    {
                        client = db.Clients.Find(editorClientWindow.Client.Id);

                        if (client != null)
                        {
                            client.CardCode = editorClientWindow.Client.CardCode;
                            client.StartDate = editorClientWindow.Client.StartDate;
                            client.FinishDate = editorClientWindow.Client.FinishDate;
                            client.Lastname = editorClientWindow.Client.Lastname;
                            client.Firstname = editorClientWindow.Client.Firstname;
                            client.Surname = editorClientWindow.Client.Surname;
                            client.Gender = editorClientWindow.Client.Gender;
                            client.Birthday = editorClientWindow.Client.Birthday;
                            client.PhoneHome = editorClientWindow.Client.PhoneHome;
                            client.PhoneMobil = editorClientWindow.Client.PhoneMobil;
                            client.Email = editorClientWindow.Client.Email;
                            client.City = editorClientWindow.Client.City;
                            client.Street = editorClientWindow.Client.Street;
                            client.House = editorClientWindow.Client.House;
                            client.Apartment = editorClientWindow.Client.Apartment;

                            db.Entry(client).State = EntityState.Modified;
                            db.SaveChanges();

                            clientsList.ItemsSource = db.Clients.ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClientDelete_Click(object sender, RoutedEventArgs e)
        {
            if (clientsList.SelectedItem == null) return;

            Client client = clientsList.SelectedItem as Client;

            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Clients.Remove(client);
                    db.SaveChanges();

                    clientsList.ItemsSource = db.Clients.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClientOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML documents (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(openFileDialog.FileName);

                XmlElement xmlRoot = xmlDocument.DocumentElement;

                Client client = new Client();

                foreach (XmlNode xnode in xmlRoot)
                {
                    XmlNode attr0 = xnode.Attributes.GetNamedItem("CARDCODE");
                    client.CardCode = Convert.ToDouble(attr0.InnerText);

                    XmlNode attr1 = xnode.Attributes.GetNamedItem("STARTDATE");
                    client.StartDate = attr1.Value;

                    XmlNode attr2 = xnode.Attributes.GetNamedItem("FINISHDATE");
                    client.FinishDate = attr2.Value;

                    XmlNode attr3 = xnode.Attributes.GetNamedItem("LASTNAME");
                    client.Lastname = attr3.Value;

                    XmlNode attr4 = xnode.Attributes.GetNamedItem("FIRSTNAME");
                    client.Firstname = attr4.Value;

                    XmlNode attr5 = xnode.Attributes.GetNamedItem("SURNAME");
                    client.Surname = attr5.Value;

                    XmlNode attr6 = xnode.Attributes.GetNamedItem("GENDER");
                    client.Gender = attr6.Value;

                    XmlNode attr7 = xnode.Attributes.GetNamedItem("BIRTHDAY");
                    client.Birthday = attr7.Value;

                    XmlNode attr8 = xnode.Attributes.GetNamedItem("PHONEHOME");
                    client.PhoneHome = attr8.Value;

                    XmlNode attr9 = xnode.Attributes.GetNamedItem("PHONEMOBIL");
                    client.PhoneMobil = attr9.Value;

                    XmlNode attr10 = xnode.Attributes.GetNamedItem("EMAIL");
                    client.Email = attr10.Value;

                    XmlNode attr11 = xnode.Attributes.GetNamedItem("CITY");
                    client.City = attr11.Value;

                    XmlNode attr12 = xnode.Attributes.GetNamedItem("STREET");
                    client.Street = attr12.Value;

                    XmlNode attr13 = xnode.Attributes.GetNamedItem("HOUSE");
                    client.House = attr13.Value;

                    XmlNode attr14 = xnode.Attributes.GetNamedItem("APARTMENT");
                    client.Apartment = attr14.Value;
                }

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Clients.Add(client);
                    db.SaveChanges();

                    clientsList.ItemsSource = db.Clients.ToList();
                }
            }
        }
    }
}
