using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Services;

public class AccountTypeServices
{
    private readonly BankContext _context;

    public AccountTypeServices(BankContext context)
    {
        _context= context;
    }

    public async Task<Accounttype?> GetById(int id)
    {
        return await _context.Accounttypes.FindAsync(id);
    }
}