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
using System.Collections.ObjectModel;

namespace ECS
{
    /// <summary>
    /// Логика взаимодействия для AddContact.xaml
    /// </summary>
    public partial class AddContact : Window
    {
        public event EventHandler<Contactis> ContactAdded;

        public AddContact()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string name = NameContTextBox.Text.Trim();
            string numberText = NumberTextBox.Text.Trim();
            string notes = CommentTextBox.Text.Trim();          
            string status = FAMContComboBox.Text.Trim();  

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(numberText))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.");
                return;
            }

            if (!long.TryParse(numberText, out long phoneNumber))
            {
                MessageBox.Show("Введите корректный номер телефона.");
                return;
            }

            var newContact = new Contactis
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Status = status,
                Notes = notes
            };

            ContactAdded?.Invoke(this, newContact);
            this.Close();
        }
    }
}
