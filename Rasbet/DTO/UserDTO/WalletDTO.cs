using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class WalletDTO
{
    public double Balance { get; set; }
    public string UserId { get; set; }


    protected WalletDTO()
    {

    }
    public WalletDTO(String id, double balance)
    {
        UserId = id;
        Balance = balance;
    }
}

