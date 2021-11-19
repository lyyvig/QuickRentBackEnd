using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI {
    class Program {
        static void Main(string[] args) {
            ColorTest();

            BrandTest();

            CarTest();

            CarDetalDtoTest();







            //CarTest();

        }

        private static void CarDetalDtoTest() {
            CarManager carManager = new CarManager(new EfCarDal());

            Console.WriteLine("\n--------CAR DETAIL-------");
            foreach (var car in carManager.GetCarDetails()) {
                Console.WriteLine("{0}--{1}--{2}--{3}", car.CarName, car.BrandName, car.ColorName, car.DailyPrice);
            }
        }

        private static void CarTest() {
            CarManager carManager = new CarManager(new EfCarDal());

            carManager.Add(new Car { Description = "Mini Cooper", BrandId = 1, ColorId = 1, DailyPrice = 1000, ModelYear = 2009 });
            carManager.Add(new Car { Description = "Mini Cooper", BrandId = 1, ColorId = 2, DailyPrice = 2000, ModelYear = 2021 });
            carManager.Add(new Car { Description = "Model S", BrandId = 2, ColorId = 3, DailyPrice = 1100, ModelYear = 2020 });
            carManager.Add(new Car { Description = "Model S", BrandId = 2, ColorId = 2, DailyPrice = 1500, ModelYear = 2021 });


            Console.WriteLine("\n--------CAR-------");
            foreach (var car in carManager.GetAll()) {
                Console.WriteLine(car.Description);
            }


            carManager.Update(new Car { Id = 3, Description = "Model X", BrandId = 2, ColorId = 1, DailyPrice = 4000, ModelYear = 2022 });
            carManager.Delete(new Car { Id = 2 });


            Console.WriteLine("--------getAll-------");
            foreach (var car in carManager.GetAll()) {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("-------getByBrand--------");
            foreach (var car in carManager.GetCarsByBrandId(1)) {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("--------GetByColor-------");
            foreach (var car in carManager.GetCarsByColorId(1)) {
                Console.WriteLine(car.Description);
            }

            Console.WriteLine("-------get--------");
            Console.WriteLine(carManager.Get(4).Description);
        }

        private static void BrandTest() {
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            brandManager.Add(new Brand { Name = "Mini" });
            brandManager.Add(new Brand { Name = "Tesla" });
            brandManager.Add(new Brand { Name = "Ford" });
            brandManager.Add(new Brand { Name = "Mercedes" });

            Console.WriteLine("\n-------BRAND--------");
            Console.WriteLine(brandManager.Get(3).Name);


            brandManager.Update(new Brand { Id = 3, Name = "Fiat" });
            brandManager.Delete(new Brand { Id = 3 });


            Console.WriteLine("-------getAll--------");
            foreach (var color in brandManager.GetAll()) {
                Console.WriteLine(color.Name);
            }
        }

        private static void ColorTest() {
            ColorManager colorManager = new ColorManager(new EfColorDal());

            colorManager.Add(new Color { Name = "Red" });
            colorManager.Add(new Color { Name = "Black" });
            colorManager.Add(new Color { Name = "Blue" });
            colorManager.Add(new Color { Name = "White" });

            Console.WriteLine("\n--------COLOR-------");
            Console.WriteLine(colorManager.Get(3).Name);


            colorManager.Update(new Color { Id = 3, Name = "Magenta" });
            colorManager.Delete(new Color { Id = 4 });


            Console.WriteLine("--------getAll-------");
            foreach (var color in colorManager.GetAll()) {
                Console.WriteLine(color.Name);
            }
        }

       
    }
}
