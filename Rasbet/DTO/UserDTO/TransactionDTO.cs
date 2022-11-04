using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class TransactionDTO
{
    public double Value { get; set; }
    public string UserId { get; set; }


    protected TransactionDTO()
    {

    }
    public TransactionDTO(string id, double value)
    {
        UserId = id;
        Value = value;
    }
}

