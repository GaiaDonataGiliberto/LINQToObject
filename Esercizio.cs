using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQToObject
{
    public class Esercizio
    {
        // creazione liste

        public static List<Product> CreateProductList()
        {
            var lista = new List<Product>
            {
                new Product {ID = 1, Name = "Telefono", UnitPrice = 300.99 },
                new Product {ID = 2, Name = "Computer", UnitPrice = 800 },
                new Product {ID = 3, Name = "Tablet", UnitPrice = 550.99 },

            };

            return lista;

        }

        public static List<Order> CreateOrderList()
        {
            var lista = new List<Order>();

            var order = new Order
            {
                ID = 1, 
                ProductID = 1, 
                Quantity = 4
            };

            lista.Add(order);

            var order1 = new Order
            {
                ID = 2,
                ProductID = 2,
                Quantity = 1
            };

            lista.Add(order1);

            var order2 = new Order
            {
                ID = 3,
                ProductID = 1,
                Quantity = 1
            };

            lista.Add(order2);

            return lista;

        }


        // esecuzione immediata e ritardata

        public static void DeferredExecution()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            // vediamo i risultati
            foreach (var p in productList)
            {
                Console.WriteLine($"ID: {p.ID} - Nome: {p.Name} - Prezzo: {p.UnitPrice}");
            }

            
            foreach (var o in orderList)
            {
                Console.WriteLine($"ID: {o.ID} - Codice: {o.ProductID} - Quantità: {o.Quantity}");
            }

            // creazione query
            var list = productList
                .Where(product => product.UnitPrice >= 400)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice });


            // aggiungo prodotto

            productList.Add(new Product
            {
                ID = 4,
                Name = "Bicicletta",
                UnitPrice = 500.99
            }) ;

            #region ===esecuzione differita===
            // eseguo la query dichiarata a riga 82

            Console.WriteLine("ESECUZIONE DIFFERITA: ");
            foreach (var p in list)
            {
                Console.WriteLine($"{p.Nome} - {p.Prezzo}");
            }
            #endregion 

            #region ===esecuzione immediata===

            var list1 = productList
                .Where(p => p.UnitPrice >= 400)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList();

            productList.Add(new Product { ID = 5, Name = "Divano", UnitPrice = 450.99 });

            Console.WriteLine("ESECUZIONE IMMEDIATA: ");

            foreach (var p in list1)
            {
                Console.WriteLine($"{p.Nome} - {p.Prezzo}");
            }


            #endregion

        }

    }
}
