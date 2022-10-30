using Domain.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ResultDomain;

public class Result
{
    public int Id { get; set; }
    public double Odd { get; set; }
    public int NumberOfBets { get; set; }
    public virtual Game Game { get; set; }
    public ResultState State { get; set; }
    public virtual Specialist Specialist { get; set; }
}
