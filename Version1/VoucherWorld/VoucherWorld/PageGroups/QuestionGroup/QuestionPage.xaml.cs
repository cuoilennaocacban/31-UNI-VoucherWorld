using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.Utilities;
using VoucherWorld.ViewModel;

namespace VoucherWorld.PageGroups.QuestionGroup
{
    public partial class QuestionPage : PhoneApplicationPage
    {

        private int placeIndex = 0;

        public QuestionPage()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            placeIndex =
                StaticViewModel.MapViewModel.RouteDetailsModel.Places.IndexOf(StaticViewModel.MapViewModel.currentPlace);

            if (placeIndex < 2)
            {
                QuestionTextBlock.Text = StaticData.CurrentErrollments.Places[placeIndex].Question.Content;
                MainTextBox.Visibility = Visibility.Collapsed;
                ScalingAnswerStackPanel.Visibility = Visibility.Visible;
            }
            else
            {
                QuestionTextBlock.Text = StaticData.CurrentErrollments.Question.Content;
                MainTextBox.Visibility = Visibility.Visible;
                ScalingAnswerStackPanel.Visibility = Visibility.Collapsed;
            }
        }

        private async void SubmitApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            if (placeIndex < 2)
            {
                StaticData.CurrentErrollments.CompletedPlace = placeIndex + 1;
                try
                {
                    await
                        UserAPI.AnswerScalingQuestionTask(ScalingSlider.Value.ToString(),
                            StaticData.CurrentUser.Id.ToString(),
                            StaticData.CurrentErrollments.Places[placeIndex].Id.ToString(), this);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            else
            {
                try
                {
                    await
                        UserAPI.AnswerOpenEndedQuestionTask(MainTextBox.Text,
                            StaticData.CurrentUser.Id.ToString(),
                            StaticData.CurrentErrollments.Places[placeIndex].Id.ToString(), this);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }


            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
            xmlWriterSettings.Indent = true;

            using (IsolatedStorageFile isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                //Create a file stream to open or create file
                using (
                    IsolatedStorageFileStream stream = new IsolatedStorageFileStream("EnrollmentHistory.xml",
                        FileMode.OpenOrCreate, isoStorage))
                {
                    XmlSerializer serializer = new XmlSerializer(StaticData.ErrollmentHistory.GetType());
                    using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                    {
                        // Viết dữ liệu theo Serialize
                        serializer.Serialize(xmlWriter, StaticData.ErrollmentHistory);
                    }
                }
            }

            if (placeIndex == 2)
            {
                //Last diamond collected
                NavigationService.RemoveBackEntry();
                NavigationService.RemoveBackEntry();
                NavigationService.Navigate(new Uri("/PageGroups/QuestionGroup/GiftCodePage.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.RemoveBackEntry();
                NavigationService.GoBack();
            }
        }

        private void ScalingSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ScalingSlider != null)
            {
                ScalingSlider.Value = Math.Round(e.NewValue, 0);
            }
        }
    }
}