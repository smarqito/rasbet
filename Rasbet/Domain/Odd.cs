using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;

public class Odd
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double OddValue { get; set; }
    private double OddSum { get; set; }
    private int OddNum { get; set; }
    public bool Win { get; set; }

    protected Odd()
    {
    }

    public Odd(string name)
    {
        Name = name;
        OddValue = 0;
        OddSum = 0;
        OddNum = 0;
        Win = false;
    }

    public void ResetOdd()
    {
        OddNum = 0;
        OddSum = 0;
        OddValue = 0;
    }

    public void UpdateOdd(double odd)
    {
        OddSum += odd;
        OddNum++;
        OddValue = OddSum / OddNum;

    }
}
