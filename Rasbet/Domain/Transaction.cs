using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Transaction
{
    public int Id { get; set; }
    public double Balance { get; set; }

    public Transaction(int id, double balance) {
        Id = id;
        Balance = balance;
    }
}
