using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI {
    class Program {
        static void Main(string[] args) {

            CarManager carManager = new CarManager(new EfCarDal());


            carManager.Add(new Car { Id = 1, BrandId = 1, ColorId = 2, ModelYear = 2020, DailyPrice = 1500, Description = "Doblo" });
            //carManager.Add(new Car { Id = 2, BrandId = 2, ColorId = 2, ModelYear = 2005, DailyPrice = 3000, Description = "Transit" });

            carManager.Update(new Car { Id = 1, BrandId = 2, ColorId = 2, ModelYear = 2005, DailyPrice = 1000, Description = "Transit" });
            //carManager.Delete(new Car { Id = 1 });



            foreach (var c in carManager.GetAll()) {
                Console.WriteLine("{0} {1}", c.Id, c.DailyPrice);
            }


            Console.WriteLine("-----------GetCarsByBrandId----------");
            foreach (var c in carManager.GetCarsByBrandId(2)) {
                Console.WriteLine("{0} {1}", c.Id, c.DailyPrice);
            }


            Console.WriteLine("-----------GetCarsByColorId----------");
            foreach (var c in carManager.GetCarsByColorId(2)) {
                Console.WriteLine("{0} {1}", c.Id, c.DailyPrice);
            }



        }
    }
}
