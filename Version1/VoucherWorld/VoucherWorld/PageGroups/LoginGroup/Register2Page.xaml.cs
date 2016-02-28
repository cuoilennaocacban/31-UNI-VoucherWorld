using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.LoginGroup
{
    public partial class Register2Page : PhoneApplicationPage
    {
        public Register2Page()
        {
            InitializeComponent();
        }

        private async void NextIconButton_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(FullNameTextBox.Text) || !String.IsNullOrEmpty(EmailTextBox.Text) ||
                !String.IsNullOrEmpty(AddressTextBox.Text) || !String.IsNullOrEmpty(PhoneNumberTextBox.Text))
            {
                StaticData.CurrentUser.Name = FullNameTextBox.Text;
                StaticData.CurrentUser.Email = EmailTextBox.Text;
                StaticData.CurrentUser.Address = AddressTextBox.Text;
                StaticData.CurrentUser.PhoneNumber = PhoneNumberTextBox.Text;

                StaticMethod.ShowProgress(this, "Registering...", 0, true, true);

                string temp = await UserAPI.Register(StaticData.CurrentUser);

                StaticMethod.ShowProgress(this, "Registering...", 0, true, false);

                NavigationService.Navigate(
                    new Uri("/PageGroups/LoginGroup/LoginPage.xaml?username=" + StaticData.CurrentUser.UserName +
                            "&password=" + StaticData.CurrentUser.Password, UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Please fill in all box", "Warning", MessageBoxButton.OK);
            }
        }
    }
}