namespace Kol2.Models;

public class Payment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public int IdClient { get; set; }
    public Client Client { get; set; }
    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }
}