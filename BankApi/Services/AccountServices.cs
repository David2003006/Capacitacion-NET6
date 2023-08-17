using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TestBankApi.Data.DTOs;

namespace BankApi.Services;

public class AccountServices
{
    private readonly BankContext _context;

    public AccountServices(BankContext context)
    {
        _context= context;
    }

    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetById(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account> Create(AccountDTO newAccountDTO)
    {
            var newAccount= new Account();
            
            newAccount.AccountType= newAccountDTO.AccountType;
            newAccount.ClientId= newAccountDTO.ClientId;
            newAccount.Balance= newAccountDTO.Balance; 

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();

            return newAccount;
    }

    public async Task Update(int id, AccountDTO account)
    {
         var existAccount= await GetById(id);

         if(existAccount is not null )
         {
             existAccount.AccountType= account.AccountType;
                existAccount.ClientId= account.ClientId;
                existAccount.Balance= account.Balance;

                 await _context.SaveChangesAsync();
         }
    }

    public async Task Delete(int id)
    {
        var accountToDelte=  await GetById(id);

        if(accountToDelte is not null)
        {
            _context.Accounts.Remove(accountToDelte);
            await _context.SaveChangesAsync();
        }

    }
}