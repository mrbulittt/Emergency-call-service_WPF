using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ECS
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollectionView contactsView;
        private ObservableCollection<Contactis> contacts = new ObservableCollection<Contactis>();
        private ObservableCollection<EmergencyService> emergencyServices = new ObservableCollection<EmergencyService>();

        public MainWindow()
        {
            InitializeComponent();
            LoadEmergencyServices();
            LoadSampleData();
            ApplySort();
        }
        private void LoadEmergencyServices()
        { 
            emergencyServices.Add(new EmergencyService
            {
                ServiceName = "Пожарная служба",
                ServicePhoneNumber = "101",
                ServiceComment = "Тушение пожаров"
            });
            emergencyServices.Add(new EmergencyService
            {
                ServiceName = "Полиция",
                ServicePhoneNumber = "102",
                ServiceComment = "Вызов полиции"
            });

            emergencyServices.Add(new EmergencyService
            {
                ServiceName = "Скорая помощь",
                ServicePhoneNumber = "103",
                ServiceComment = "Медицинская помощь"
            });
            
            emergencyServices.Add(new EmergencyService
            {
                ServiceName = "Служба газовой безопасности",
                ServicePhoneNumber = "104",
                ServiceComment = "Аварийная газовая служба"
            });

            EmergencyListLb.ItemsSource = emergencyServices;
        }
        private void LoadSampleData()
        {
            var cont1 = new Contactis { Name = "Ильнар", PhoneNumber = 89083390403, Status = "Друг", Notes = ""};
            var cont2 = new Contactis { Name = "Артур", PhoneNumber = 89196959739, Status = "Друг", Notes="" };

            
            

            contacts.Add(cont1);
            contacts.Add(cont2);
            Contacts.ItemsSource = contacts;
            contactsView = CollectionViewSource.GetDefaultView(contacts);
            Contacts.ItemsSource = contactsView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mess = MessageBox.Show("Ваш сигнал о спасении и координаты переданы службе 112! Ожидайте и сохраняйте спокойствие", "Сигнал о спасении принят");
        }

        

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addContact = new AddContact();

            addContact.ContactAdded += (s, contact) =>  
            {
                contacts.Add(contact);
            };

            addContact.ShowDialog();
        }

        private void Emergency_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowEmergencyDetails();
        }
        private void ShowEmergencyDetails()
        {
            var selected = EmergencyListLb.SelectedItem as EmergencyService;
            if (selected != null)
            {
                string message = $"Название: {selected.ServiceName}\n" +
                                 $"Номер: {selected.ServicePhoneNumber}\n" +
                                 $"Примечание: {selected.ServiceComment}";

                MessageBox.Show(message, "Детали службы");
            }
        }

        private void Contacts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ShowContactDetails();
        }

        private void ShowContactDetails()
        {
            var selectedContact = Contacts.SelectedItem as Contactis;
            if (selectedContact != null)
            {
                string message = $"Имя: {selectedContact.Name}\n" +
                                 $"Номер телефона: {selectedContact.PhoneNumber}\n" +
                                 $"Взаимоотношение: {selectedContact.Status}\n" +
                                 $"Примечание: {selectedContact.Notes}";

                MessageBox.Show(message, "Дополнительная информация");
            }
        }

        private void ApplySort()
        {
            if (contactsView == null) return;

            contactsView.SortDescriptions.Clear();

            var selectedItem = SortToCont.SelectedItem as ComboBoxItem;

            if (selectedItem?.Content.ToString() == "По алфавиту(А-Я)")
            {
                contactsView.SortDescriptions.Add(
                    new SortDescription("Name", ListSortDirection.Ascending));
            }
            else if (selectedItem?.Content.ToString() == "По алфавиту(Я-А)")
            {
                contactsView.SortDescriptions.Add(
                    new SortDescription("Name", ListSortDirection.Descending));
            }
        }

        private void SortToCont_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplySort();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = SearchTextBox.Text.ToLower();

            contactsView.Filter = item =>
            {
                if (item is Contactis contact)
                {
                    return contact.Name.ToLower().Contains(filter) ||
                           contact.PhoneNumber.ToString().Contains(filter) ||
                           contact.CommentDisplay.ToLower().Contains(filter);
                }
                return true;
            };
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Tag is Contactis contactis)
            {
                var result = MessageBox.Show(
                    $"Удалить контакт '{contactis.Name}' из списка?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    contacts.Remove(contactis);
                    
                }
            }
        }
    }
}
