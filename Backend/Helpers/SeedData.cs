//using Microsoft.EntityFrameworkCore;
//using WebDb.Entities;

//namespace WebDb.Helpers
//{
//    public static class SeedData
//    {
//        public static void SeedDatabase(DbContext context)
//        {
//            //context.Database.Migrate();

//            //Category cat1 = new Category { Id = Guid.NewGuid(), Name = "Мелкая бытовая техника"};
//            //Category cat2 = new Category { Id = Guid.NewGuid(), Name = "Средняя бытовая техника"};

//            Customer cust1 = new Customer { Id = Guid.NewGuid(), Name = "Филатова Ольга Петровна" };
//            Customer cust2 = new Customer { Id = Guid.NewGuid(), Name = "Овечкин Олег Александрович" };

//            Manufacture man1 = new Manufacture { Id = Guid.NewGuid(), Name = "LG" };
//            Manufacture man2 = new Manufacture { Id = Guid.NewGuid(), Name = "Samsung" };

//            Store st1 = new Store { Id = Guid.NewGuid(), Name = "Филиал на Киевской" };
//            Store st2 = new Store { Id = Guid.NewGuid(), Name = "Филиал на Пушкина" };

//            Product prod1 = new Product { Id = Guid.NewGuid(), Name = "Стиральная машина", Manufacture = man1 };
//            Product prod2 = new Product { Id = Guid.NewGuid(), Name = "Холодильник", Manufacture = man1 };

//            Delivery del1 = new Delivery { Id = Guid.NewGuid(), DeliveryDate = DateTime.Now, Product = prod1, Store = st1 };
//            Delivery del2 = new Delivery { Id = Guid.NewGuid(), DeliveryDate = DateTime.Now, Product = prod2, Store = st2 };

//            PriceChange pc1 = new PriceChange { Id = Guid.NewGuid(), DataPriceChange = DateTime.Now, NewPrice = 12000, Product = prod1 };
//            PriceChange pc2 = new PriceChange { Id = Guid.NewGuid(), DataPriceChange = DateTime.Now, NewPrice = 52000, Product = prod2 };

//            Purchase pur1 = new Purchase { Id = Guid.NewGuid(), PurshaseDate = DateTime.Now, Customer = cust1, Store = st1 };
//            Purchase pur2 = new Purchase { Id = Guid.NewGuid(), PurshaseDate = DateTime.Now, Customer = cust2, Store = st2 };

//            PurchaseItem pi1 = new PurchaseItem { Id = Guid.NewGuid(), ProductCount = 2, ProductPrice = 24000, Purchase = pur1, Product = prod1 };
//            PurchaseItem pi2 = new PurchaseItem { Id = Guid.NewGuid(), ProductCount = 1, ProductPrice = 52000, Purchase = pur2, Product = prod2 };
//            context.AddRangeAsync(cust1, cust2, man1, man2, st1, st2, prod1, prod2, del1, del2, pc1, pc2, pur1, pur2, pi1, pi2);
//            context.SaveChangesAsync();
//        }
//    }
//}
