using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VoucherWorld.Model;
using VoucherWorld.Utilities;

namespace VoucherWorld.ViewModel
{
    public class RouteListViewModel
    {
        public ObservableCollection<Route> RouteCollection { get; set; }

        public async Task LoadData(string cat)
        {
            string temp = "";

            if (!StaticData.isOfflineMode)
            {
                temp =
                    await
                        StaticMethod.GetHttpAsString(
                            "http://voucherworld.azurewebsites.net/api/routes?lat=10.8221&lon=106.6876&distance=5&cat=" +
                            cat);
            }
            else
            {
                temp =
                    "[{\"Id\":1,\"Name\":\"KFC\",\"IsHidden\":false,\"Category\":0,\"Distance\":4.6274461736471313,\"Gift\":{\"Id\":1,\"GiftName\":\"Name of Gift_0\",\"Images\":[]},\"PlaceIcon\":\"http://mohinhgiayvn.com/diendan/favicon.ico\",\"Question\":{\"Id\":1,\"Content\":\"Open-ended question for route0\"},\"Place\":{\"Id\":1,\"Name\":\"Place 01\",\"Address\":\"\u0110\u01af\u1edcNG Ph\u1ea1m Ng\u1ecdc Th\u1ea1ch 4, Ho Chi Minh City, Vietnam\",\"BonusPoint\":0,\"Longitude\":106.697502,\"Latitude\":10.781637,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":1,\"Name\":\"KFC\",\"Address\":\"KFC Manager Address\",\"PhoneNumber\":\"0123456789\",\"Website\":\"http://bing.com/\"}},{\"Id\":3,\"Name\":\"Burger King Phan X\u00edch Long\",\"IsHidden\":false,\"Category\":0,\"Distance\":2.6985724393301647,\"Gift\":{\"Id\":3,\"GiftName\":\"Name of Gift_2\",\"Images\":[]},\"PlaceIcon\":\"http://mohinhgiayvn.com/diendan/favicon.ico\",\"Question\":{\"Id\":3,\"Content\":\"Open-ended question for route2\"},\"Place\":{\"Id\":7,\"Name\":\"Place 07\",\"Address\":\"Nh\u00e0 d\u00e2n\",\"BonusPoint\":0,\"Longitude\":106.688885,\"Latitude\":10.797864,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":2,\"Name\":\"BurgerKing\",\"Address\":\"BurgerKing Manager Address\",\"PhoneNumber\":\"0123456789\",\"Website\":\"http://bing.com/\"}},{\"Id\":4,\"Name\":\"Burger King C\u00e1ch m\u1ea1ng th\u00e1ng T\u00e1m\",\"IsHidden\":false,\"Category\":0,\"Distance\":4.8649979690515739,\"Gift\":{\"Id\":4,\"GiftName\":\"Name of Gift_3\",\"Images\":[]},\"PlaceIcon\":\"http://mohinhgiayvn.com/diendan/favicon.ico\",\"Question\":{\"Id\":4,\"Content\":\"Open-ended question for route3\"},\"Place\":{\"Id\":10,\"Name\":\"Place 10\",\"Address\":\"C\u00e2u l\u1ea1c b\u1ed9 Lan Anh\",\"BonusPoint\":0,\"Longitude\":106.678316,\"Latitude\":10.779309,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":2,\"Name\":\"BurgerKing\",\"Address\":\"BurgerKing Manager Address\",\"PhoneNumber\":\"0123456789\",\"Website\":\"http://bing.com/\"}},{\"Id\":7,\"Name\":\"VIC Coconut\",\"IsHidden\":false,\"Category\":0,\"Distance\":0.10380183976916138,\"Gift\":{\"Id\":7,\"GiftName\":\"Coconut-covered Ice Cream\",\"Images\":[]},\"PlaceIcon\":\"http://mohinhgiayvn.com/diendan/favicon.ico\",\"Question\":{\"Id\":7,\"Content\":\"Which flavour do you like most?\"},\"Place\":{\"Id\":21,\"Name\":\"Kem Vi\u1ec7t Nguy\u1ec5n V\u0103n Nghi\",\"Address\":\"Kem Vi\u1ec7t Nguy\u1ec5n V\u0103n Nghi\",\"BonusPoint\":0,\"Longitude\":106.6885,\"Latitude\":10.8218,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":4,\"Name\":\"Vi\u1ec7t Ice Cream\",\"Address\":\"Vi\u1ec7t Ice Cream Address\",\"PhoneNumber\":\"0866772508\",\"Website\":\"http://vieticecream.com\"}},{\"Id\":10,\"Name\":\"Vi\u1ec7t Ice Cream Nguy\u1ec5n V\u0103n Nghi\",\"IsHidden\":false,\"Category\":0,\"Distance\":0.16597340240899303,\"Gift\":{\"Id\":10,\"GiftName\":\"Chocolate Ice Cream\",\"Images\":[]},\"PlaceIcon\":null,\"Question\":{\"Id\":10,\"Content\":\"Which flavour do you like most\"},\"Place\":{\"Id\":19,\"Name\":\"47 Nguy\u1ec5n V\u0103n Nghi, P4, G\u00f2 V\u1ea5p\",\"Address\":\"47 Nguy\u1ec5n V\u0103n Nghi, P4, G\u00f2 V\u1ea5p\",\"BonusPoint\":0,\"Longitude\":106.6872,\"Latitude\":10.82354,\"Altitude\":0.0,\"Question\":null},\"Merchant\":{\"Id\":4,\"Name\":\"Vi\u1ec7t Ice Cream\",\"Address\":\"Vi\u1ec7t Ice Cream Address\",\"PhoneNumber\":\"0866772508\",\"Website\":\"http://vieticecream.com\"}}]";
            }

            JArray jArray = JArray.Parse(temp);

            //RouteCollection = jObject.ToObject<ObservableCollection<Route>>();
            RouteCollection = jArray.ToObject<ObservableCollection<Route>>();

            foreach (Enrrollments errollmentse in StaticData.ErrollmentHistory)
            {
                int routeId = errollmentse.Id;
                for (int i = RouteCollection.Count - 1; i >= 0; i--)
                {
                    if (RouteCollection[i].Id == routeId)
                    {
                        RouteCollection.RemoveAt(i);
                    }
                }
            }
        }
    }
}
