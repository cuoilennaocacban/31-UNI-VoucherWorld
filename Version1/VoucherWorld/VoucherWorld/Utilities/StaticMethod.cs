using Microsoft.Phone.Shell;
using System;
using System.Device.Location;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace VoucherWorld.Utilities
{
    public class StaticMethod
    {
        public static async Task<string> GetHttpAsString(string link)
        {
            WebRequest request = WebRequest.Create(new Uri(link, UriKind.Absolute));
            WebResponse response = await request.GetResponseAsync();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            return result;
        }

        public static async Task<string> PostHttpAsString(string link)
        {
            WebRequest request = WebRequest.Create(new Uri(link, UriKind.Absolute));
            request.Method = "POST";
            WebResponse response = await request.GetResponseAsync();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            string result = reader.ReadToEnd();

            return result;
        }

        public static void Quit()
        {
            IsolatedStorageSettings.ApplicationSettings.Save();
            Application.Current.Terminate();
        }

        public static double Distance(GeoCoordinate a, GeoCoordinate b)
        {
            double e = (3.1415926538*a.Latitude/180);
            double f = (3.1415926538*a.Longitude/180);
            double g = (3.1415926538*b.Latitude/180);
            double h = (3.1415926538*b.Longitude/180);
            double i = (Math.Cos(e)*Math.Cos(g)*Math.Cos(f)*Math.Cos(h) +
                        Math.Cos(e)*Math.Sin(f)*Math.Cos(g)*Math.Sin(h) + Math.Sin(e)*Math.Sin(g));
            double j = (Math.Acos(i));
            double k = (6371*j);

            return k;
        }

        /// <summary>
        /// Show or iide the progress bar
        /// </summary>
        /// <param name="sender">the current page that call the progress</param>
        /// <param name="displayText">Text to be displayed</param>
        /// <param name="displayTime">Time to automatically hide the progress, in miliseconds</param>
        /// <param name="isIndeterminate">True for non progress loading</param>
        /// <param name="isVisible">True to be displayed, false for hide</param>
        public static void ShowProgress(object sender, string displayText, int displayTime, bool isIndeterminate,
            bool isVisible)
        {
            var progress = new ProgressIndicator
            {
                IsVisible = isVisible,
                IsIndeterminate = isIndeterminate,
                Text = displayText
            };
            SystemTray.SetIsVisible((DependencyObject) sender, isVisible);
            SystemTray.SetOpacity((DependencyObject) sender, 0.7);
            SystemTray.SetProgressIndicator((DependencyObject) sender, progress);

            if (displayTime > 0)
            {
                ThreadPool.QueueUserWorkItem(obj =>
                {
                    Thread.Sleep(displayTime);

                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        //MessageBox.Show("after delay");
                        SystemTray.SetIsVisible((DependencyObject) sender, false);
                    });
                });
            }
        }

    }
}