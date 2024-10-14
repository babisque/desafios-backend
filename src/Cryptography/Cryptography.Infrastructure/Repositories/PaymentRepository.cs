using Cryptography.Entities;
using Cryptography.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cryptography.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Payment>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<Payment> FindAsync(long id)
    {
        return await _context.Payments.FindAsync(id) ?? throw new Exception("No payment found.");
    }

    public async Task CreateAsync(Payment entity)
    {
        try
        {
            _context.Payments.Add(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error creating entity.", e);
        }
    }

    public async Task UpdateAsync(Payment entity)
    {
        try
        {
            _context.Payments.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error updating entity.", e);
        }
    }

    public async Task RemoveAsync(long id)
    {
        try
        {
            _context.Payments.Remove(await _context.Payments.FindAsync(id) ?? throw new Exception("Payment not found."));
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error removing entity.", e);
        }
    }
}