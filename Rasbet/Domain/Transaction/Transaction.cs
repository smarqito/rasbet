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

    public Transaction(double balance)
    {
        Balance = balance;
    }
}
