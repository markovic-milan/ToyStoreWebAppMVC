using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToyStoreWebAppMVC.Models
{
    public class TransactionModel
    {
        public IEnumerable<UserTransaction> UserTransactions { get; set; }

    }

    public class UserTransaction
    {
        [Display(Name = "Korisnicko Ime")]
        public string Username { get; set; }
        [Display(Name = "Ime")]
        public string FirstName { get; set; }
        [Display(Name = "Prezime")]
        public string LastName { get; set; }
        [Display(Name = "Datum kupovine")]
        public DateTime TransactionDate { get; set; }
        [Display(Name = "Ukupno")]
        public decimal Total { get; set; }
        public int OrderId { get; set; }
    }
}
