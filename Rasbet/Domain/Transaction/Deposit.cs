﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Deposit : Transaction
{
    public Deposit(double balance) : base(balance)
    {
    }
}
