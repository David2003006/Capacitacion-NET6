using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using TestBankApi.Data.DTOs;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Authorization;

namespace BankApi.Controllers;

//Ya hiciste el delete ahora solo falta investigar la cuenta que solo se pueda ver su ceunta
[Authorize]
[ApiController]
[Route("api/[Controller]")]
public class BankTranController: ControllerBase
{
    private readonly BankTranServices _services;
    private readonly AccountServices _acount;
    private readonly TranTypeServices _type;

    public  BankTranController(BankTranServices services, AccountServices account, TranTypeServices type)
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTRan(int id, BankTranDTO bank)
    {
        var ExistTran= await _services.GetById(id);

        if(ExistTran is not null)
        {
            string result = await ValidateTran(bank);
            
            if(!result.Equals("valid"))
                return BadRequest(new {message= result});
            
            await _services.Update(id, bank);
            return NoContent();
        }else
        {
            return AccountNoFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> delete(int id)
    {
        var ExistTran= await _services.GetById(id);
        
        if(ExistTran is not null && ExistTran.Amount == 0)
        {
            await _services.Delete(id);
            return Ok();
        }else
        {
            return  AccountNoFound(id);
        }
    }

     [NonAction]
        public NotFoundObjectResult AccountNoFound(int id)
        {
          return NotFound(new {message= $"La cuenta con ID {id} no esxiste o nu pude boorrar porque su saldo debe de estar en 0"});
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