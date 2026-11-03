using System.Collections.ObjectModel;
using MAUI_Data_Acces.DataAccess;
using MAUI_Data_Acces.Models;

namespace MAUI_Data_Acces
{
    public partial class MainPage : ContentPage
    {
        PersonData personData;
        public ObservableCollection<Person> People { get; set; } = new();

        public MainPage()
        {
            InitializeComponent();
            personData = new PersonData();
            BindingContext = this;
            UpdatePeopleList();
        }

        private async void UpdatePeopleList()
        {
            People.Clear();
            var list = await personData.GetPeopleAsync();
            foreach (var p in list)
                People.Add(p);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                await DisplayAlert("Error", "First name cannot be empty", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                await DisplayAlert("Error", "Last name cannot be empty", "OK");
                return;
            }
            if (dpDateOfBirth.Date > DateTime.Today)
            {
                await DisplayAlert("Error", "Date of Birth cannot be in the future", "OK");
                return;
            }

            var person = new Person
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                DateOfBirth = dpDateOfBirth.Date
            };
            await personData.SavePersonAsync(person);

            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            UpdatePeopleList();
        }
    }
}