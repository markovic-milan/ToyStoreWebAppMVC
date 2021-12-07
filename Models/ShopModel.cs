using System.Collections.Generic;
using ToyStoreWebAppMVC.Entities;

namespace ToyStoreWebAppMVC.Models
{
    public class ShopModel
    {
        public IEnumerable<Toy> Toys { get; set; }

        public List<Toy> Cart { get; set; }

        public string NotInStock { get; set; }
    }
}
