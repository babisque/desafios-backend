﻿using Cryptography.Entities;

namespace Cryptography.Repositories;

public interface IPaymentRepository
{
    Task<IList<Payment>> GetAllAsync();
    Task<Payment> FindAsync(long id);
    Task CreateAsync(Payment entity);
    Task UpdateAsync(Payment entity);
    Task RemoveAsync(long id);
}