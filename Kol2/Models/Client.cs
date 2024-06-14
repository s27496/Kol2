using System.ComponentModel.DataAnnotations;

namespace Kol2.Models;

public class Client
{
    [Required] public int Id { get; set; }
    [Required] [MaxLength(100)] public string FirstName { get; set; }
    [Required] [MaxLength(100)] public string LastName { get; set; }
    [Required] [MaxLength(100)] public string Email { get; set; }
    [MaxLength(100)] public string Phone { get; set; }

    public ICollection<Sale> Sales { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
    public ICollection<Payment> Payments { get; set; }
}