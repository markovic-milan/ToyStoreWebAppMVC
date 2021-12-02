using System.Collections.Generic;
using ToyStoreWebAppMVC.Entities;

namespace ToyStoreWebAppMVC.Models
{
    public class ToyModel
    {
        public IEnumerable<Toy> Toys { get; set; } 
    }
}
