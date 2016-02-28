using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Data.Migrations
{
    class VoucherWorldDataSeeder
    {
        VoucherWorldContext _vwct;

        public VoucherWorldDataSeeder(VoucherWorldContext vwct)
        {
            _vwct = vwct;
        }

        public void Seed()
        {
            try
            {
                UnitOfWork myUnitOfWork = new UnitOfWork(_vwct);

                //Create ManagerUsers
                List<MerchantManager> merchantManagers = new List<MerchantManager>();
                merchantManagers.Add(new MerchantManager
                {
                    Name = "KFC",
                    UserName = "kfc",
                    Password = "123123",
                    Email = "kfc@kfc.com",
                    Address = "34 Đường Lê Duẩn, Phường Bến Nghé, Quận 1, Thành Phố Hồ Chí Minh",
                    PhoneNumber = "0123456789",
                    //ObjectState = ObjectState.Added
                });
                merchantManagers.Add(new MerchantManager
                {
                    Name = "Burger King",
                    UserName = "bgk",
                    Password = "123123",
                    Email = "bgk@bgk.com",
                    Address = "26-28 Đường Phạm Hồng Thái, Phường Bến Thành, Quận 1, Thành Phố Hồ Chí Minh",
                    PhoneNumber = "012345678",
                    //ObjectState = ObjectState.Added
                });
                merchantManagers.Add(new MerchantManager
                {
                    Name = "Cinebox Cinema",
                    UserName = "cnb",
                    Password = "123123",
                    Email = "cnb@cnb.com",
                    Address = "212 Đường Lý Chính Thắng, Phường 9, Quận 3, Thành Phố Hồ Chí Minh",
                    PhoneNumber = "083935061",
                    //ObjectState = ObjectState.Added
                });
                merchantManagers.Add(new MerchantManager
                {
                    Name = "Viet Ice Cream",
                    UserName = "vic",
                    Password = "123123",
                    Email = "contact@vic.com",
                    Address = "Địa chỉ thì ở đâu mà chả được",
                    PhoneNumber = "0866772508",
                    //ObjectState = ObjectState.Added
                });
                myUnitOfWork.Repository<MerchantManager>().InsertRange(merchantManagers);

                ////Create NormalUsers
                List<NormalUser> normalUsers = new List<NormalUser>();
                normalUsers.Add(new NormalUser
                {
                    Name = "Tuấn Trần Văn",
                    UserName = "cuoilennaocacban",
                    Password = "123123",
                    Email = "cuoilennaocacban@gmail.com",
                    Address = "Gò Vấp, HCMC",
                    PhoneNumber = "0963230397",
                    IsFacebookUser = false
                });
                normalUsers.Add(new NormalUser
                {
                    Name = "Quang Lê Thái Phúc",
                    UserName = "gaulois",
                    Password = "123123",
                    Email = "phuc.quang102@gmail.com",
                    Address = "Quận 4, HCMC",
                    PhoneNumber = "01278989636",
                    IsFacebookUser = false
                });
                normalUsers.Add(new NormalUser
                {
                    Name = "Phát Đỗ Hữu",
                    UserName = "bubungbu",
                    Password = "123123",
                    Email = "bubungbu@gmail.com",
                    Address = "Thủ Đức, HCMC",
                    PhoneNumber = "0123456789",
                    IsFacebookUser = false
                });
                normalUsers.Add(new NormalUser
                {
                    Name = "Tuấn Trần Văn",
                    UserName = "tuantv",
                    Password = "123123",
                    Email = "cuoilennaocacban@gmail.com",
                    Address = "Thủ Đức, HCMC",
                    PhoneNumber = "0866771508",
                });
                myUnitOfWork.Repository<NormalUser>().InsertRange(normalUsers);

                ////Create Merchants
                List<Merchant> merchants = new List<Merchant>();
                merchants.Add(new Merchant
                {
                    Name = "KFC",
                    Address = "KFC Manager Address",
                    PhoneNumber = "0123456789",
                    Website = "http://bing.com/",
                    MerchantManager = merchantManagers[0]
                });
                merchants.Add(new Merchant
                {
                    Name = "BurgerKing",
                    Address = "BurgerKing Manager Address",
                    PhoneNumber = "0123456789",
                    Website = "http://bing.com/",
                    MerchantManager = merchantManagers[1]
                });
                merchants.Add(new Merchant
                {
                    Name = "Cinebox",
                    Address = "Cinebox Manager Address",
                    PhoneNumber = "0123456789",
                    Website = "http://bing.com/",
                    MerchantManager = merchantManagers[2]
                });
                merchants.Add(new Merchant
                {
                    Name = "Việt Ice Cream",
                    Address = "Việt Ice Cream Address",
                    PhoneNumber = "0866772508",
                    Website = "http://vieticecream.com",
                    MerchantManager = merchantManagers[3]
                });
                myUnitOfWork.Repository<Merchant>().InsertRange(merchants);

                //Create Places
                List<Place> places = new List<Place>();
                places.Add(new Place
                {
                    Longitude = 106.697502,
                    Latitude = 10.781637,
                    Name = "Place 01",
                    Address = "ĐƯỜNG Phạm Ngọc Thạch 4, Ho Chi Minh City, Vietnam",
                    Merchant = merchants[0],
                });
                places.Add(new Place
                {
                    Longitude = 106.697715,
                    Latitude = 10.78103,
                    Name = "Place 02",
                    Address = "ĐƯỜNG Phạm Ngọc Thạch 1, Ho Chi Minh City, Vietnam",
                    Merchant = merchants[0],
                });
                places.Add(new Place
                {
                    Longitude = 106.698342,
                    Latitude = 10.781088,
                    Name = "Place 03",
                    Address = "Diamond Plaza, tầng 5",
                    Merchant = merchants[0],
                });


                places.Add(new Place
                {
                    Longitude = 106.695461,
                    Latitude = 10.770516,
                    Name = "Place 04",
                    Address = "Công viên 23/9",
                    Merchant = merchants[1],
                });
                places.Add(new Place
                {
                    Longitude = 106.695752,
                    Latitude = 10.77103,
                    Name = "Place 05",
                    Address = "Khách san New World",
                    Merchant = merchants[1],
                });
                places.Add(new Place
                {
                    Longitude = 106.695746,
                    Latitude = 10.771259,
                    Name = "Place 06",
                    Address = "Burger King Bến Thành",
                    Merchant = merchants[1],
                });


                places.Add(new Place
                {
                    Longitude = 106.688885,
                    Latitude = 10.797864,
                    Name = "Place 07",
                    Address = "Nhà dân",
                    Merchant = merchants[1],
                });
                places.Add(new Place
                {
                    Longitude = 106.689315,
                    Latitude = 10.797628,
                    Name = "Place 08",
                    Address = "Nhà dân",
                    Merchant = merchants[1],
                });
                places.Add(new Place
                {
                    Longitude = 106.689845,
                    Latitude = 10.797557,
                    Name = "Place 09",
                    Address = "Nhà dân",
                    Merchant = merchants[1],
                });

                places.Add(new Place
                {
                    Longitude = 106.678316,
                    Latitude = 10.779309,
                    Name = "Place 10",
                    Address = "Câu lạc bộ Lan Anh",
                    Merchant = merchants[1],
                });
                places.Add(new Place
                {
                    Longitude = 106.67916,
                    Latitude = 10.779019,
                    Name = "Place 11",
                    Address = "Nhà dân",
                    Merchant = merchants[1],
                });
                places.Add(new Place
                {
                    Longitude = 106.679823,
                    Latitude = 10.778735,
                    Name = "Place 12",
                    Address = "Burger King Cách mạng tháng Tám",
                    Merchant = merchants[1],
                });

                places.Add(new Place
                {
                    Longitude = 106.682439,
                    Latitude = 10.781172,
                    Name = "Place 13",
                    Address = "Nhà dân",
                    Merchant = merchants[2],
                });
                places.Add(new Place
                {
                    Longitude = 106.682549,
                    Latitude = 10.780975,
                    Name = "Place 14",
                    Address = "Cao ốc Toàn Thắng",
                    Merchant = merchants[2],
                });
                places.Add(new Place
                {
                    Longitude = 106.682463,
                    Latitude = 10.78069,
                    Name = "Place 15",
                    Address = "Cinebox Cinema",
                    Merchant = merchants[2],
                });

                places.Add(new Place
                {
                    Longitude = 106.672437,
                    Latitude = 10.772572,
                    Name = "Place 16",
                    Address = "Nhà thiếu nhi Quận 10",
                    Merchant = merchants[2],
                });
                places.Add(new Place
                {
                    Longitude = 106.672963,
                    Latitude = 10.771692,
                    Name = "Place 17",
                    Address = "Việt Nam Quốc tự",
                    Merchant = merchants[2],
                });
                places.Add(new Place
                {
                    Longitude = 106.674309,
                    Latitude = 10.771781,
                    Name = "Place 18",
                    Address = "Cinebox Hòa Bình",
                    Merchant = merchants[2],
                });
                /////////////////////////////////////////
                /// // i=18
                places.Add(new Place
                {
                    Longitude = 106.6872,
                    Latitude = 10.82354,
                    Name = "47 Nguyễn Văn Nghi, P4, Gò Vấp",
                    Address = "47 Nguyễn Văn Nghi, P4, Gò Vấp",
                    Merchant = merchants[3],
                });
                places.Add(new Place
                {
                    Longitude = 106.6882,
                    Latitude = 10.8226,
                    Name = "Trường Đại học Công nghiệp, TP.HCM",
                    Address = "Trường Đại học Công nghiệp, TP.HCM",
                    Merchant = merchants[3],
                });
                places.Add(new Place
                {
                    Longitude = 106.6885,
                    Latitude = 10.8218,
                    Name = "Kem Việt Nguyễn Văn Nghi",
                    Address = "Kem Việt Nguyễn Văn Nghi",
                    Merchant = merchants[3],
                });


                places.Add(new Place
                {
                    Longitude = 106.6882,
                    Latitude = 10.82137,
                    Name = "Trường tiểu học Hạnh Thông",
                    Address = "Trường tiểu học Hạnh Thông",
                    Merchant = merchants[3],
                });
                places.Add(new Place
                {
                    Longitude = 106.6882,
                    Latitude = 10.8214,
                    Name = "Trường mầm non Hoa Lan",
                    Address = "Trường mầm non Hoa Lan",
                    Merchant = merchants[3],
                });


                places.Add(new Place
                {
                    Longitude = 106.6863,
                    Latitude = 10.8229,
                    Name = "67 Nguyễn Văn Bảo, P4, Gò Vấp",
                    Address = "67 Nguyễn Văn Bảo, P4, Gò Vấp",
                    Merchant = merchants[3],
                });
                places.Add(new Place
                {
                    Longitude = 106.6865,
                    Latitude = 10.82251,
                    Name = "45 Nguyễn Văn Bảo, P4, Gò Vấp",
                    Address = "45 Nguyễn Văn Bảo, P4, Gò Vấp",
                    Merchant = merchants[3],
                });

                myUnitOfWork.Repository<Place>().InsertRange(places);

                //Create scaling questions
                var scalingQuestions = new List<ScalingQuestion>();

                for (int i = 0; i < places.Count; i++)
                {
                    scalingQuestions.Add(new ScalingQuestion
                    {
                        Content = "Scaling question for place " + i,
                        Place = places[i]
                    });
                }

                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "Are Việt Ice Cream's employees friendly?",
                    Place = places[18]
                });
                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "How does Milk-Coconut flavour satisfy you?",
                    Place = places[19]
                });
                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "Is Việt Ice Cream's website friendly?",
                    Place = places[20]
                });
                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "Can you easily find Việt Ice Cream's store?",
                    Place = places[21]
                });
                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "Let's rate our Vanilla flavour",
                    Place = places[22]
                });
                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "Do you like our promotion?",
                    Place = places[23]
                });
                scalingQuestions.Add(new ScalingQuestion
                {
                    Content = "Are Việt Ice Cream beautiful?",
                    Place = places[24]
                });
                myUnitOfWork.Repository<ScalingQuestion>().InsertRange(scalingQuestions);
                
                //Create Routes
                List<Route> routes = new List<Route>();
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    Name = "KFC",
                    Merchant = merchants[0],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    Name = "Burger King Bến Thành",
                    Merchant = merchants[1],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    Name = "Burger King Phan Xích Long",
                    Merchant = merchants[1],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    Name = "Burger King Cách mạng tháng Tám",
                    Merchant = merchants[1],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Game,
                    Name = "Cinebox Cinema",
                    Merchant = merchants[2],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Game,
                    Name = "Cinebox Hòa Bình",
                    Merchant = merchants[2],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });


                ////// i=6
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    IsHidden = false,
                    Name = "VIC Coconut",
                    Merchant = merchants[3],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    IsHidden = true,
                    Name = "VIC Hidden Chocolate",
                    Merchant = merchants[3],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                routes.Add(new Route
                {
                    Category = RouteCategory.Food,
                    IsHidden = true,
                    Name = "VIC Another Hidden Chocolate",
                    Merchant = merchants[3],
                    PlaceIcon = "http://mohinhgiayvn.com/diendan/favicon.ico"
                });
                myUnitOfWork.Repository<Route>().InsertRange(routes);

                //Create Gifts
                var gifts = new List<Gift>();
                for (int i = 0; i < 6; i++)
                {
                    var gift = new Gift
                    {
                        GiftName = "Name of Gift_" + i,
                        StockAmount = 100,
                        Route = routes[i],
                    };
                    gifts.Add(gift);
                }

                gifts.Add(new Gift
                {
                    GiftName = "Coconut-covered Ice Cream",
                    StockAmount = 50,
                    Route = routes[6],
                });
                gifts.Add(new Gift
                {
                    GiftName = "Chocolate Ice Cream",
                    StockAmount = 50,
                    Route = routes[7],
                });
                gifts.Add(new Gift
                {
                    GiftName = "Chocolate Ice Cream",
                    StockAmount = 50,
                    Route = routes[8],
                });
                myUnitOfWork.Repository<Gift>().InsertRange(gifts);

                //Create open-ended questions
                var openEndedQuestions = new List<OpenEndedQuestion>();
                for (int i = 0; i < routes.Count; i++)
                {
                    openEndedQuestions.Add(new OpenEndedQuestion
                    {
                        Content = "Open-ended question for route" + i,
                        Route = routes[i]
                    });
                }


                openEndedQuestions.Add(new OpenEndedQuestion
                {
                    Content = "Which flavour do you like most?",
                    Route = routes[6]
                });
                openEndedQuestions.Add(new OpenEndedQuestion
                {
                    Content = "Which one do you prefer: in box or on stick?",
                    Route = routes[7]
                });
                openEndedQuestions.Add(new OpenEndedQuestion
                {
                    Content = "Which flavour do you like most?",
                    Route = routes[8]
                });
                myUnitOfWork.Repository<OpenEndedQuestion>().InsertRange(openEndedQuestions);
                
                //Create RoutePlace
                List<RoutePlace> routePlaces = new List<RoutePlace>();
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[0],
                    Place = places[0],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[0],
                    Place = places[1],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[0],
                    Place = places[2],
                });

                routePlaces.Add(new RoutePlace
                {
                    Route = routes[1],
                    Place = places[3],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[1],
                    Place = places[4],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[1],
                    Place = places[5],
                });


                routePlaces.Add(new RoutePlace
                {
                    Route = routes[2],
                    Place = places[6],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[2],
                    Place = places[7],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[2],
                    Place = places[8],
                });


                routePlaces.Add(new RoutePlace
                {
                    Route = routes[3],
                    Place = places[9],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[3],
                    Place = places[10],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[3],
                    Place = places[11],
                });

                routePlaces.Add(new RoutePlace
                {
                    Route = routes[4],
                    Place = places[12],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[4],
                    Place = places[13],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[4],
                    Place = places[14],
                });

                routePlaces.Add(new RoutePlace
                {
                    Route = routes[5],
                    Place = places[15],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[5],
                    Place = places[16],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[5],
                    Place = places[17],
                });



                routePlaces.Add(new RoutePlace
                {
                    Route = routes[6],
                    Place = places[21],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[6],
                    Place = places[22],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[6],
                    Place = places[20],
                });

                routePlaces.Add(new RoutePlace
                {
                    Route = routes[7],
                    Place = places[23],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[7],
                    Place = places[19],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[7],
                    Place = places[20],
                });

                routePlaces.Add(new RoutePlace
                {
                    Route = routes[8],
                    Place = places[19],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[8],
                    Place = places[24],
                });
                routePlaces.Add(new RoutePlace
                {
                    Route = routes[8],
                    Place = places[20],
                });
                myUnitOfWork.Repository<RoutePlace>().InsertRange(routePlaces);
                myUnitOfWork.SaveChanges();

                //Create Clients
                List<MerchantClient> clients = new List<MerchantClient>();
                var client = new MerchantClient
                {
                    Name = "VIC Client #1",
                    UserName = "vicclient",
                    Password = "123123",
                    Email = "vicclient@gmail.com",
                    Address = "Nguyễn Văn Nghi",
                    PhoneNumber = "0866772508",
                    Merchant = merchants[3],
                    UserType = UserType.MerchantClient,
                    ObjectState = ObjectState.Added
                };
                myUnitOfWork.Repository<MerchantClient>().InsertGraph(client);
                

                myUnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                //foreach (var failure in ex.EntityValidationErrors)
                //{
                //    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                //    foreach (var error in failure.ValidationErrors)
                //    {
                //        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                //        sb.AppendLine();
                //    }
                //}

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    ex.InnerException.InnerException.Message.ToString(), ex
                    ); // Add the original exception as the innerException
            }
        }
    }
}
