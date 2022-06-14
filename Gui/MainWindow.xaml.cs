using DataAccess;
using Entities;
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

        public MainWindow()
        {
            InitializeComponent();
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

            // Load contact infos into the listbox:
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
    }
}
