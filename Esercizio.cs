using System;
using System.Collections;
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


        // ===ESECUZIONE IMMEDIATA E DIFFERITA===

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
            });

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



        // ===SINTASSI===
        public static void Syntax()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            #region ===method syntax===
            // method syntax
            var methodList = productList
                .Where(p => p.UnitPrice >= 600)
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .ToList();
            #endregion


            #region ===query syntax===
            // query syntax
            var queryList =
                (from p in productList
                 where p.UnitPrice <= 600
                 select new { Nome = p.Name, Prezzo = p.UnitPrice }).ToList();
            // si fa il .ToList() su tutta la query, cosicché il risultato della query (che è
            // un IEnumerable) viene castato a List
            #endregion


        }


        // ===OPERATORI===

        public static void Operators()
        {
            var productList = CreateProductList();
            var orderList = CreateOrderList();

            // scrittura a schermo delle liste
            Console.WriteLine("Lista prodotti: ");
            foreach (var p in productList)
            {
                Console.WriteLine($"{p.ID} - {p.Name} - {p.UnitPrice}");
            }

            Console.WriteLine("Lista ordini: ");
            foreach (var o in orderList)
            {
                Console.WriteLine($"{o.ID} - {o.ProductID} - {o.Quantity}");
            }

            #region ===of type===
            // FILTRO OFTYPE
            var list = new ArrayList();

            list.Add(productList);
            list.Add("Ciao");
            list.Add(42);

            var typeQuery =
                from item in list.OfType<int>()
                select item;

            Console.WriteLine("OFTYPE");
            foreach (var item in typeQuery)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region ===element===
            // ELEMENT
            Console.WriteLine("ELEMENT");
            int[] empty = { };
            var el1 = empty.FirstOrDefault();
            Console.WriteLine(el1);

            /* type inference: capisce che è un Product poiché
            è dentro una list di Product. Element at seleziona l'elemento
            all'indice indicato. Dato che è riconosciuto come Product possono
            anche accedere alle sue proprietà, tipo name
            */
            var p1 = productList.ElementAt(0).Name;
            Console.WriteLine(p1);
            #endregion

            #region ===ordinamento===
            // ORDINAMENTO

            Console.WriteLine("ORDINAMENTO");

            // query syntax
            var orderedList =
                from p in productList
                orderby p.Name ascending, p.UnitPrice descending
                select new { Nome = p.Name, Prezzo = p.UnitPrice };

            // method syntax
            var orderedList2 = productList
                .OrderBy(p => p.Name) //sa che p è di tipo Product perché è in una lista di Product.             
                .ThenByDescending(p => p.UnitPrice) // così indichi che vuoi la proprietà chiamata name del Product p
                .Select(p => new { Nome = p.Name, Prezzo = p.UnitPrice })
                .Reverse();


            foreach (var p in orderedList)
            {
                Console.WriteLine($"List: {p.Nome} - {p.Prezzo}");
            }

            foreach (var p in orderedList2)
            {
                Console.WriteLine($"List2: {p.Nome} - {p.Prezzo}");
            }
            #endregion

            #region ===quantificatori===
            // QUANTIFICATORI

            Console.WriteLine("QUANTIFICATORI");

            var hasProductWithT = 
                productList.Any(p => p.Name.StartsWith("T"));


            var allProductWithT = 
                productList.All(p => p.Name.StartsWith("T"));


            Console.WriteLine("Ci sono prodotti che iniziano con T? {0}", hasProductWithT);
            Console.WriteLine("Tutti i prodotti iniziano con T? {0}", allProductWithT);
            #endregion

            #region ===groupby===
            // GROUPBY

            Console.WriteLine("GROUP BY");

            // query syntax
            var groupByList =
                from o in orderList
                group o by o.ProductID
                into groupList //salva la query come tabella provvisoria
                select groupList; //tipo query annidate. 
                //In questo specifico caso,dato che non continuo la subquery dopo into, 
                //groupList sarà uguale a groupByList

            foreach (var order in groupByList)
            {
                Console.WriteLine(order.Key);

                foreach (var item in order)
                {
                    Console.WriteLine($"\t ID: {item.ProductID} - QNT: {item.Quantity}");
                }
            }

            // method syntax

            var groupByList2 = orderList
                .GroupBy(o => o.ProductID);

            foreach (var order in groupByList2)
            {
                Console.WriteLine("List 2: " + order.Key);

                foreach (var item in order)
                {
                    Console.WriteLine($"\t ID2: {item.ProductID} - QNT2: {item.Quantity}");
                }
            }

            #endregion


        }
    }
}
