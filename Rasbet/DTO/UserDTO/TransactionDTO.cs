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
    public string Type { get; set; }


    public TransactionDTO()
    {
    }

    public TransactionDTO(string id, double value, DateTime date)
    {
        UserId = id;
        Value = "" + value;
        Date = date;
    }

    public TransactionDTO(string id, double value, string type)
    {
        UserId = id;
        Value = "" + value;
        Type = type;
    }

    public TransactionDTO(string id, string value, DateTime date, string type)
    {
        UserId = id;
        Value = value;
        Date = date;
        Type = type;
    }

    public TransactionDTO(string id, string value, string type)
    {
        UserId = id;
        Value = value;
        Type = type;
    }

    public TransactionDTO(double value, DateTime date, string type)
    {
        Value = "" + value;
        Date = date;
        Type = type;
    }

}

