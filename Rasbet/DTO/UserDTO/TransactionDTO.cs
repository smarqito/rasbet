using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class TransactionDTO
{
    public double Value { get; set; }
    public int Id { get; set; }


    protected TransactionDTO()
    {

    }
    public TransactionDTO(int id, double value)
    {
        Id = id;
        Value = value;
    }
}

