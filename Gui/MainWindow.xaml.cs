using DataAccess;
using Entities;
using Services;
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

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Repository repo;
        private List<Person> persons;

        public MainWindow()
        {
            InitializeComponent();
            WeatherService weatherService = new();
            string weather = weatherService.GetWeather();
            try
            {
                // Initialize repo field:
                repo = new();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Fejl under tilgang til data: {e.Message}", "Opstartsfejl", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

            // Get all contact infos from database
            List<ContactInformation> contactInformations = repo.GetAllContactInformations();

            List<Address> allAddressesWithPeople = repo.GetAllAddresses();

            // Load contact infos into the datagrid:
            persons = repo.GetAllPersons();
            dg.ItemsSource = persons;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Mock input data:
            string mailInput = "person@mail.com";
            string phoneNumberInput = "34567987";

            // Make object to send to repository:
            ContactInformation contactInformation = new()
            {
                Mail = mailInput,
                PhoneNumber = phoneNumberInput
            };

            // Call the repository:
            repo.AddNewContactInformation(contactInformation);
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedIndex > 0)
            {
                Person editedPerson = dg.Items[dg.SelectedIndex - 1] as Person;
                if (editedPerson != null)
                {
                    repo.Update(editedPerson);
                    //persons = repo.GetAllPersons();
                    //dg.ItemsSource = null;
                    //dg.ItemsSource = persons;
                }
            }
        }
    }
}