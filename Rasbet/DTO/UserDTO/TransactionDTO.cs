using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class TransactionDTO
{
    public string Value { get; set; }
    public string UserId { get; set; }


    public TransactionDTO()
    {
    }

    public TransactionDTO(string id, double value)
    {
        UserId = id;
        Value = ""+value;
    }
    public TransactionDTO(string id, string value)
    {
        UserId = id;
        Value = value;
    }
}

