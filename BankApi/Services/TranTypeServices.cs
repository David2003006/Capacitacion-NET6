using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Services;

public class TranTypeServices
{
    private readonly BankContext _context;

    public TranTypeServices(BankContext context)
    {
        _context= context;
    }

    public async Task<Transacctiontype?> GetBiId(int id)
    {
        return await _context.Transacctiontypes.FindAsync(id);
    }
}