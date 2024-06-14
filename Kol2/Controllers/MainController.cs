using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Kol2.DTO;

namespace Kol2.Controllers;

[Route("api")]
[ApiController]
public class MainController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public MainController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("client/{idClient:int}")]
    public async Task<IActionResult> GetClientWithSubscriptions(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.Subscriptions)
            .ThenInclude(s => s.Payments)
            .FirstOrDefaultAsync(c => c.Id == idClient);

        if (client == null)
            return NotFound(new { message = "Client not found." });

        var response = new
        {
            firstName = client.FirstName,
            lastName = client.LastName,
            email = client.Email,
            phone = client.Phone,
            subscriptions = client.Subscriptions.Select(s => new
            {
                IdSubscription = s.Id,
                Name = s.Name,
                TotalPaidAmount = s.Payments.Count * s.Price
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPost("payment")]
    public async Task<IActionResult> AddPayment([FromBody] Payment paymentRequest)
    {
        var client = await _context.Clients.FindAsync(paymentRequest.IdClient);
        if (client == null)
            return NotFound(new { message = "Client not found." });

        var subscription = await _context.Subscriptions.FindAsync(paymentRequest.IdSubscription);
        if (subscription == null)
            return NotFound(new { message = "Subscription not found." });

        var sales = await _context.Sales
            .Where(s => s.IdSubscription == paymentRequest.IdSubscription)
            .OrderByDescending(s => s.CreatedAt)
            .FirstOrDefaultAsync();


        var newPayment = new Models.Payment()
        {
            IdClient = paymentRequest.IdClient,
            IdSubscription = paymentRequest.IdSubscription,
            Date = DateTime.Now
        };

        _context.Payments.Add(newPayment);
        await _context.SaveChangesAsync();

        return Ok(new { Id = newPayment.Id });
    }
}