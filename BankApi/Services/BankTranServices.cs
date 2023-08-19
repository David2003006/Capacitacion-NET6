using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using TestBankApi.Data.DTOs;

namespace BankApi.Services;

public class BankTranServices
{

    private readonly BankContext _context;

    public BankTranServices(BankContext context)
    {
        this._context= context;
    }

    public async Task<Bancktransaction?> GetById(int id)
    {
        return await _context.Bancktransactions.FindAsync(id);
    } 

    public async Task<Bancktransaction> Create(BankTranDTO newbankDTO)
    {
        var bank= new Bancktransaction();

        bank.AccountId= newbankDTO.AccountId;
        bank.TransactionType= newbankDTO.TransactionType;
        bank.Amount= newbankDTO.Amount;
        bank.ExternalAcount= newbankDTO.ExternalAcount;

        _context.Bancktransactions.Add(bank);
        await  _context.SaveChangesAsync();

        return bank;
    }

    public async Task Update(int id, BankTranDTO bank)
    {
        var ExistTan= await GetById(id);

        if(ExistTan is not null)
        {
            ExistTan.AccountId= bank.AccountId;
            ExistTan.TransactionType= bank.TransactionType;
            ExistTan.Amount= bank.Amount;
            ExistTan.ExternalAcount= bank.ExternalAcount;

            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var DeleteTran= await GetById(id);

        if(DeleteTran is not null)
        {
            _context.Bancktransactions.Remove(DeleteTran);
            await _context.SaveChangesAsync();
        }
    }

}