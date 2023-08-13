using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Services;

public class AccountServices
{
    private readonly BankContext _context;

    public AccountServices(BankContext context)
    {
        _context= context;
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id)
    {
        return _context.Accounts.Find(id);
    }

    public Account Create(Account newAccount)
    {
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            return newAccount;
    }

    public void Update(int id, Account account)
    {
         var existAccount= _context.Accounts.Find(id);

         if(existAccount is not null )
         {
             existAccount.AccountType= account.AccountType;
                existAccount.ClientId= account.ClientId;
                existAccount.Balance= account.Balance;

                _context.SaveChanges();
         }
    }

    public void Delete(int id)
    {
        var accountToDelte= GetById(id);

        if(accountToDelte is not null)
        {
            _context.Accounts.Remove(accountToDelte);
            _context.SaveChanges();
        }

    }
}