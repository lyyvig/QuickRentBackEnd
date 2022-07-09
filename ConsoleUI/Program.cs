using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.IO;

namespace ConsoleUI {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine(Directory.GetCurrentDirectory());

            Console.WriteLine(Path.GetDirectoryName(@"wwwrootz\images\cars\9026680c-c979-4e23-be47-7889c379c5b9.jpg"));


            ColorTest();

            BrandTest();

            CarTest();

            CarDetailDtoTest();

            UserTest();

            CustomerTest();

            RentalTest();

        }

        private static void RentalTest() {
            //RentalManager rentalManager = new RentalManager(new EfRentalDal());

            //Console.WriteLine("\n--------Rental-------");
            //var result = rentalManager.Add(new Rental { CarId = 3, CustomerId = 1, RentDate = new DateTime(2021, 11, 19), ReturnDate = new DateTime(2021, 11, 20) });
            //Console.WriteLine(result.Message);

            //result = rentalManager.Add(new Rental { CarId = 3, CustomerId = 2, RentDate = new DateTime(2021, 11, 20) });
            //Console.WriteLine(result.Message);

            //result = rentalManager.Add(new Rental { CarId = 3, CustomerId = 1, RentDate = new DateTime(2021, 11, 20) });
            //Console.WriteLine(result.Message);
        }

        private static void CustomerTest() {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal(), new EfUserDal());

            Console.WriteLine("\n--------Customer-------");
            var result = customerManager.Add(new Customer { Id = 1, CompanyName = "Lyvig" });
            Console.WriteLine(result.Message);

            result = customerManager.Add(new Customer { Id = 2, CompanyName = "Corpo" });
            Console.WriteLine(result.Message);
            
            result = customerManager.Add(new Customer { Id = 3, CompanyName = "Corp" });
            Console.WriteLine(result.Message);
        }

        private static void UserTest() {
            //UserManager userManager = new UserManager(new EfUserDal());


            Console.WriteLine("\n--------USER-------");
            /*var result = userManager.Add(new User { FirstName = "Baris", LastName = "Bilir", Email = "baris.blirr@gmail.com", Password = "123456789" });
            Console.WriteLine(result.Message);

            result = userManager.Add(new User { FirstName = "Mert", LastName = "Patlar", Email = "abc@gmail.com", Password = "1234567890" });
            Console.WriteLine(result.Message);*/
        }

        private static void CarDetailDtoTest() {
            CarManager carManager = new CarManager(new EfCarDal(), new CarImageManager(new EfCarImageDal()));

            Console.WriteLine("\n--------CAR DETAIL-------");
            var result = carManager.GetDetails();
            foreach (var car in result.Data) {
                Console.WriteLine("{0}--{1}--{2}--{3}", car.Description, car.BrandName, car.ColorName, car.DailyPrice);
            }
            Console.WriteLine(result.Message);
        }

        private static void CarTest() {
            CarManager carManager = new CarManager(new EfCarDal(), new CarImageManager(new EfCarImageDal()));

            carManager.Add(new Car { Description = "Mini Cooper", BrandId = 1, ColorId = 1, DailyPrice = 1000, ModelYear = 2009 });
            carManager.Add(new Car { Description = "Mini Cooper", BrandId = 1, ColorId = 2, DailyPrice = 2000, ModelYear = 2021 });
            carManager.Add(new Car { Description = "Model S", BrandId = 2, ColorId = 3, DailyPrice = 1100, ModelYear = 2020 });
            carManager.Add(new Car { Description = "Model S", BrandId = 2, ColorId = 2, DailyPrice = 1500, ModelYear = 2021 });


            Console.WriteLine("\n--------CAR-------");
            var results = carManager.GetAll();
            foreach (var car in results.Data) {
                Console.WriteLine(car.Description);
            }
            Console.WriteLine(results.Message);
            

            var voidResult = carManager.Update(new Car { Id = 3, Description = "Model X", BrandId = 2, ColorId = 1, DailyPrice = 4000, ModelYear = 2022 });
            Console.WriteLine(voidResult.Message);

            voidResult = carManager.Delete(new Car { Id = 2 });
            Console.WriteLine(voidResult.Message);


            Console.WriteLine("--------getAll-------");
            results = carManager.GetAll();
            foreach (var car in results.Data) {
                Console.WriteLine(car.Description);
            }
            Console.WriteLine(results.Message);

            Console.WriteLine("-------getByBrand--------");
            results = carManager.GetCarsByBrandId(1);
            foreach (var car in results.Data) {
                Console.WriteLine(car.Description);
            }
            Console.WriteLine(results.Message);

            Console.WriteLine("--------GetByColor-------");
            results = carManager.GetCarsByColorId(1);
            foreach (var car in results.Data) {
                Console.WriteLine(car.Description);
            }
            Console.WriteLine(results.Message);


            Console.WriteLine("-------get--------");
            var result = carManager.Get(4);
            Console.WriteLine(result.Data.Description);
            Console.WriteLine(result.Message);
        }

        private static void BrandTest() {
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            brandManager.Add(new Brand { Name = "Mini" });
            brandManager.Add(new Brand { Name = "Tesla" });
            brandManager.Add(new Brand { Name = "Ford" });
            brandManager.Add(new Brand { Name = "Mercedes" });

            Console.WriteLine("\n-------BRAND--------");
            var voidResult = brandManager.Get(3);
            Console.WriteLine(voidResult.Data.Name);
            Console.WriteLine(voidResult.Message);


            var result = brandManager.Update(new Brand { Id = 3, Name = "Fiat" });
            Console.WriteLine(result.Message);

            result = brandManager.Delete(new Brand { Id = 3 }); ;
            Console.WriteLine(result.Message);


            Console.WriteLine("-------getAll--------");
            var results = brandManager.GetAll();
            foreach (var color in results.Data) {
                Console.WriteLine(color.Name);
            }
            Console.WriteLine(results.Message);
        }

        private static void ColorTest() {
            ColorManager colorManager = new ColorManager(new EfColorDal());

            colorManager.Add(new Color { Name = "Red" });
            colorManager.Add(new Color { Name = "Black" });
            colorManager.Add(new Color { Name = "Blue" });
            colorManager.Add(new Color { Name = "White" });

            Console.WriteLine("\n--------COLOR-------");
            var voidResult = colorManager.Get(3);
            Console.WriteLine(voidResult.Data.Name);
            Console.WriteLine(voidResult.Message);


            var result = colorManager.Update(new Color { Id = 3, Name = "Magenta" });
            Console.WriteLine(result.Message);

            result = colorManager.Delete(new Color { Id = 4 }); ;
            Console.WriteLine(result.Message);


            Console.WriteLine("--------getAll-------");
            var results = colorManager.GetAll();
            foreach (var color in results.Data) {
                Console.WriteLine(color.Name);
            }
            Console.WriteLine(results.Message);

        }


    }
}
