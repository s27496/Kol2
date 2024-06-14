namespace Kol2.Models;

public class Discount
{
    public int Id { get; set; }
    public decimal Value { get; set; }
    public int IdSubscription { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public Subscription Subscription { get; set; }
}