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

    public DateTime Date { get; set; }


    public TransactionDTO()
    {
    }

    public TransactionDTO(string id, double value, DateTime date)
    {
        UserId = id;
        Value = "" + value;
        Date = date;
    }

    public TransactionDTO(string id, double value)
    {
        UserId = id;
        Value = "" + value;
    }

    public TransactionDTO(string id, string value, DateTime date)
    {
        UserId = id;
        Value = value;
        Date = date;
    }

    public TransactionDTO(string id, string value)
    {
        UserId = id;
        Value = value;
    }

    public TransactionDTO(double value, DateTime date)
    {
        Value = "" + value;
        Date = date;
    }

}

