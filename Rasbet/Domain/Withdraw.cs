using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Withdraw : Transaction
{
    public Withdraw(int id, double balance) : base(id,balance) {
        Id = id;
        Balance = balance;
    }
}
