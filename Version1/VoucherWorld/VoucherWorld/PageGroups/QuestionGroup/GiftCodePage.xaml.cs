using System;
using Microsoft.Phone.Controls;
using System.Windows;
using Microsoft.Phone.Tasks;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.QuestionGroup
{
    public partial class GiftCodePage : PhoneApplicationPage
    {
        public GiftCodePage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (StaticData.isHistory)
            {
                NavigationService.RemoveBackEntry();
            }
            else
            {
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
            }
            GiftCodeTextBlock.Text = StaticData.CurrentErrollments.GiftCode;
        }

        private void email_Click(object sender, EventArgs e)
        {
            EmailComposeTask task = new EmailComposeTask();
            task.Subject = "Hey, I've just got some cool gift with Voucher World App";
            task.Body = "I've received a ";
            task.Show();
        }

        private void fb_Click(object sender, EventArgs e)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.LinkUri = new Uri("http://voucherworld.azurewebsites.net", UriKind.Absolute);
            shareLinkTask.Message = "Hey, I've just got some cool gift with Voucher World App. Download and enjoy with me";
            shareLinkTask.Show();
        }

        private void twitter_Click(object sender, EventArgs e)
        {
            ShareLinkTask shareLinkTask = new ShareLinkTask();
            shareLinkTask.LinkUri = new Uri("http://voucherworld.azurewebsites.net", UriKind.Absolute);
            shareLinkTask.Message = "Hey, I've just got some cool gift with Voucher World App. Download and enjoy with me";
            shareLinkTask.Show();
        }
    }
}