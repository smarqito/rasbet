using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Wallet
{
	private double balance;
	public int Id { get; set; }
	public virtual User User { get; set; }
	public double Balance
	{
		get { return balance; }
		set { balance = Math.Max(0,value); }
	}
}