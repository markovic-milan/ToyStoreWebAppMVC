using System.Collections.Generic;
using ToyStoreWebAppMVC.Entities;

namespace ToyStoreWebAppMVC.Models
{
    public class CartModel
    {
        public IEnumerable<Toy> Cart { get; set; }
    }
}
