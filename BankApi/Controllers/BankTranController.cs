using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using TestBankApi.Data.DTOs;

namespace BankApi.Controllers;

//Te falta el put Delte que se por token y ya solo grabas video 

[ApiController]
[Route("api/[Controller]")]
public class BankTranContoller: ControllerBase
{
    private readonly BankTranServices _services;
    private readonly AccountServices _acount;
    private readonly TranTypeServices _type;

    public  BankTranContoller(BankTranServices services, AccountServices account, TranTypeServices type)
    {
        _services= services;
        _acount= account;
        _type= type;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Bancktransaction?>> GetById(int id)
    {
        return await _services.GetById(id);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BankTranDTO bankDto)
    {
            var result= await ValidateTran(bankDto);

            if(!result.Equals("valid"))
                return BadRequest(new {message = result });

            var newTran= await _services.Create(bankDto);

             return CreatedAtAction(nameof(GetById), new{id= newTran.Id}, newTran);
            
    }

    [NonAction]
    public async Task<String> ValidateTran(BankTranDTO bank )
        {
            String result= "valid";

            var accountId= await _acount.GetById(bank.AccountId);

            if(accountId is null)
              result= $"El tipo de cuenta {accountId }, no existe";
            
            var TranId= bank.TransactionType;

            var Tranid= await _type.GetBiId(TranId);

            if(Tranid is null)
              result= $"El tipo de transacion {Tranid}, no existe";

            return result;

        }
}