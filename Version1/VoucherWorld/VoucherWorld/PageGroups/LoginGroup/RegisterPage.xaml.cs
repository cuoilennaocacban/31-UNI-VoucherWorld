using Microsoft.Phone.Controls;
using System;
using System.Windows;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.LoginGroup
{
    public partial class RegisterPage : PhoneApplicationPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void NextIconButton_OnClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(UsernameTextBox.Text) || !String.IsNullOrEmpty(PasswordTextBox.Password) ||
                !String.IsNullOrEmpty(ConfirmPasswordTextBox.Password))
            {
                if (PasswordTextBox.Password == ConfirmPasswordTextBox.Password)
                {
                    StaticData.CurrentUser.UserName = UsernameTextBox.Text;
                    StaticData.CurrentUser.Password = PasswordTextBox.Password;
                    NavigationService.Navigate(new Uri("/PageGroups/LoginGroup/Register2Page.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("Confirm password does not match", "Warning", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all box", "Warning", MessageBoxButton.OK);
            }
        }
    }
}