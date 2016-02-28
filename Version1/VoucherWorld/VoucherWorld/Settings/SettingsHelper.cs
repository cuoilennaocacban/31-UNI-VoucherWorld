using System.IO.IsolatedStorage;

namespace VoucherWorld.Settings
{
    public static class ApplicationSettings
    {
        public static void SetSetting<T>(string key, T value, bool save = false)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
            if (save)
                IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static T GetSetting<T>(string key)
        {
            return GetSetting(key, default(T));
        }

        public static T GetSetting<T>(string key, T defaultValue)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key) &&
                    IsolatedStorageSettings.ApplicationSettings[key] is T
                ? (T)IsolatedStorageSettings.ApplicationSettings[key]
                : defaultValue;
        }

        public static bool HasSetting<T>(string key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key) &&
                IsolatedStorageSettings.ApplicationSettings[key] is T;
        }

        public static bool RemoveSetting(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                var result = IsolatedStorageSettings.ApplicationSettings.Remove(key);
                if (result)
                    IsolatedStorageSettings.ApplicationSettings.Save();

                return result;
            }

            return false;
        }

        public static void SetSettingSafe<T>(string key, T value, bool save = false)
        {
            if (HasSetting<T>(key))
            {
                RemoveSetting(key);
            }
            ;
            SetSetting<T>(key, value, true);
        }
    }

    public class AnimationSettingHelper
    {
        public static string GetLanguage()
        {
            string settingKey = "OrientaionAnimationMode";
            string result = "2";

            if (ApplicationSettings.HasSetting<string>(settingKey))
            {
                result = ApplicationSettings.GetSetting<string>(settingKey);
                return result;
            }
            else
            {
                ApplicationSettings.SetSettingSafe(settingKey, result, true);
            }

            return result;
        }

        public static void SetLanguage(string language)
        {
            string settingKey = "OrientaionAnimationMode";
            ApplicationSettings.SetSettingSafe(settingKey, language, true);
        }
    }

    public class FacebookLoginSettingHelper
    {
        string settingKey = "IsLoggedInWithFacebook";
        public bool GetLanguage()
        {
            
            bool result = false;

            if (ApplicationSettings.HasSetting<bool>(settingKey))
            {
                result = ApplicationSettings.GetSetting<bool>(settingKey);
                return result;
            }
            else
            {
                ApplicationSettings.SetSettingSafe(settingKey, result, true);
            }

            return result;
        }

        public void SetLanguage(bool isLoggedInWithFacebook)
        {
            ApplicationSettings.SetSettingSafe(settingKey, isLoggedInWithFacebook, true);
        }
    }
}
