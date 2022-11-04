
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Wallet
{
	private double balance;

	[Key]
	public int Id { get; set; }
    //public virtual User User { get; set; } 

    public string UserId { get; set; }

	public double Balance
	{
		get { return balance; }
		set { balance = Math.Max(0,value); }
	}

	public virtual ICollection<Transaction> Transactions { get; set; }

	protected Wallet ()
	{

	}
	public Wallet (string id)
	{
		UserId = id;
		Balance = 0;
	}
}