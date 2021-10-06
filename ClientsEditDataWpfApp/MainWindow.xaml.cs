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
                // Подключаемся к базе данных
                using (ApplicationContext db = new ApplicationContext())
                {
                    //// Загружаем данные из таблица Clients
                    db.Clients.Load();

                    // Передаем данные из таблицы в таблицу "clientsList"
                    this.DataContext = db.Clients.Local.ToBindingList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавляем данные в таблицу Clients
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

        // Редактируем данные в таблице Clients
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
                FullName = client.FullName,
                GenderId = client.GenderId,
                Birthday = client.Birthday,
                PhoneHome = client.PhoneHome,
                PhoneMobil = client.PhoneMobil,
                Email = client.Email,
                City = client.City,
                Street = client.Street,
                House = client.House,
                Apartment = client.Apartment,
                AltAddress = client.AltAddress,
                CardType = client.CardType,
                Ownerguid = client.Ownerguid,
                Cardper = client.Cardper,
                Turnover = client.Turnover,
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
                            client.FullName = editorClientWindow.Client.FullName;
                            client.GenderId = editorClientWindow.Client.GenderId;
                            client.Birthday = editorClientWindow.Client.Birthday;
                            client.PhoneHome = editorClientWindow.Client.PhoneHome;
                            client.PhoneMobil = editorClientWindow.Client.PhoneMobil;
                            client.Email = editorClientWindow.Client.Email;
                            client.City = editorClientWindow.Client.City;
                            client.Street = editorClientWindow.Client.Street;
                            client.House = editorClientWindow.Client.House;
                            client.Apartment = editorClientWindow.Client.Apartment;
                            client.AltAddress = editorClientWindow.Client.AltAddress;
                            client.CardType = editorClientWindow.Client.CardType;
                            client.Ownerguid = editorClientWindow.Client.Ownerguid;
                            client.Cardper = editorClientWindow.Client.Cardper;
                            client.Turnover = editorClientWindow.Client.Turnover;

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

        // Удаляем данные в таблице Clients
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

        // Окно открытие диалогового окна и выбор файла XML и передача его данных в таблицу Clients
        private void ClientOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "XML documents (*.xml)|*.xml";

            if (openFileDialog.ShowDialog() == true)
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(openFileDialog.FileName);

                // Получаем корневой элемент
                XmlElement xmlRoot = xmlDocument.DocumentElement;

                // Выбераем все узлы "Card" из XML документа
                XmlNodeList xmlNodeList = xmlRoot.SelectNodes("Card");

                // Обходим все узлы в корневом элементе
                foreach (XmlNode xnode in xmlNodeList)
                {
                    // Создаем объект класса "Client"
                    Client client = new Client();

                    // Получаем значение атрибутов, в корневом элементе, и передаем их в свойства объекта класса "Client"
                    XmlNode cardCodeXml = xnode.Attributes.GetNamedItem("CARDCODE");
                    client.CardCode = Convert.ToDouble(cardCodeXml.InnerText);

                    XmlNode startDateXml = xnode.Attributes.GetNamedItem("STARTDATE");
                    client.StartDate = startDateXml.Value;

                    XmlNode finishDateXml = xnode.Attributes.GetNamedItem("FINISHDATE");
                    client.FinishDate = finishDateXml.Value;

                    XmlNode lastnameXml = xnode.Attributes.GetNamedItem("LASTNAME");
                    client.Lastname = lastnameXml.Value;

                    XmlNode firstnameXml = xnode.Attributes.GetNamedItem("FIRSTNAME");
                    client.Firstname = firstnameXml.Value;

                    XmlNode surnameXml = xnode.Attributes.GetNamedItem("SURNAME");
                    client.Surname = surnameXml.Value;

                    XmlNode fullNameXml = xnode.Attributes.GetNamedItem("FULLNAME");
                    client.FullName = fullNameXml.Value;

                    XmlNode genderIdXml = xnode.Attributes.GetNamedItem("GENDERID");
                    client.GenderId = genderIdXml.Value;

                    XmlNode birthdayXml = xnode.Attributes.GetNamedItem("BIRTHDAY");
                    client.Birthday = birthdayXml.Value;

                    XmlNode phoneHomeXml = xnode.Attributes.GetNamedItem("PHONEHOME");
                    client.PhoneHome = phoneHomeXml.Value;

                    XmlNode phoneMobilXml = xnode.Attributes.GetNamedItem("PHONEMOBIL");
                    client.PhoneMobil = phoneMobilXml.Value;

                    XmlNode emailXml = xnode.Attributes.GetNamedItem("EMAIL");
                    client.Email = emailXml.Value;

                    XmlNode cityXml = xnode.Attributes.GetNamedItem("CITY");
                    client.City = cityXml.Value;

                    XmlNode streetXml = xnode.Attributes.GetNamedItem("STREET");
                    client.Street = streetXml.Value;

                    XmlNode houseXml = xnode.Attributes.GetNamedItem("HOUSE");
                    client.House = houseXml.Value;

                    XmlNode apartmentXml = xnode.Attributes.GetNamedItem("APARTMENT");
                    client.Apartment = apartmentXml.Value;

                    XmlNode altAddressXml = xnode.Attributes.GetNamedItem("ALTADDRESS");
                    client.AltAddress = altAddressXml.Value;

                    XmlNode cardTypeXml = xnode.Attributes.GetNamedItem("CARDTYPE");
                    client.CardType = cardTypeXml.Value;

                    XmlNode ownerguidXml = xnode.Attributes.GetNamedItem("OWNERGUID");
                    client.Ownerguid = ownerguidXml.Value;

                    XmlNode cardperXml = xnode.Attributes.GetNamedItem("CARDPER");
                    client.Cardper = cardperXml.Value;

                    XmlNode turnoverXml = xnode.Attributes.GetNamedItem("TURNOVER");
                    client.Turnover = turnoverXml.InnerText;

                    try
                    {
                        // Подключаемся к базе данных
                        using (ApplicationContext db = new ApplicationContext())
                        {
                            // В базу данных передаем объект класса "Client", в котором хранятся данные из XML документа
                            db.Clients.Add(client);
                            // Сохраняем изменения в базе данных
                            db.SaveChanges();

                            clientsList.ItemsSource = db.Clients.ToList();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}
