using Microsoft.Phone.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using VoucherWorld.Model;
using VoucherWorld.Resources;
using VoucherWorld.Settings;

namespace VoucherWorld.PageGroups.SettingsGroup
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode != NavigationMode.Back)
            {
                LoadAnimation();
            }
        }

        private void orientaionAnimListPicker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrientationAnimation selectedItem = orientaionAnimListPicker.SelectedItem as OrientationAnimation;
            AnimationSettingHelper.SetLanguage(selectedItem.id);
        }

        private void LoadAnimation()
        {
            ObservableCollection<OrientationAnimation> animationModeCollection = new ObservableCollection<OrientationAnimation>();

            OrientationAnimation newMode = new OrientationAnimation();
            newMode.id = "0";
            newMode.value = AppResources.Settings_LoadAnimation_Like_Liquid;

            animationModeCollection.Add(newMode);

            newMode = new OrientationAnimation();
            newMode.id = "1";
            newMode.value = AppResources.Settings_LoadAnimation_Fade_changes;

            animationModeCollection.Add(newMode);

            newMode = new OrientationAnimation();
            newMode.id = "2";
            newMode.value = AppResources.Settings_LoadAnimation_Hybrid_changes;

            animationModeCollection.Add(newMode);

            orientaionAnimListPicker.ItemsSource = animationModeCollection;

            string lan = AnimationSettingHelper.GetLanguage();
            if (lan != null)
            {
                orientaionAnimListPicker.SelectedIndex = Convert.ToInt32(lan);
            }
        }
    }
}