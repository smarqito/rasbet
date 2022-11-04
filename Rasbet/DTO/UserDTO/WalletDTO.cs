using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.UserDTO;

public class WalletDTO
{
    public double Balance { get; set; }
    public int Id { get; set; }


    protected WalletDTO()
    {

    }
    public WalletDTO(int id, double balance)
    {
        Id = id;
        Balance = balance;
    }
}

