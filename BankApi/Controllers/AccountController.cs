using System.Xml.Linq;
using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using TestBankApi.Data.DTOs;

namespace BankApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AccountController: ControllerBase
{
        private readonly AccountServices AccountServices;
        private readonly AccountTypeServices accountServices;
        private readonly ClientServices clientServices;

        public AccountController(AccountServices accountServices, AccountTypeServices accountType,
        ClientServices serviceClient)
        {
            this.AccountServices= accountServices;
            this.accountServices= accountType;
            this.clientServices= serviceClient;
        }

        [HttpGet]
        public async Task<IEnumerable<Account>> Get()
        {
            return await AccountServices.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var account= await AccountServices.GetById(id);

            if(account is null)
                return NotFound();
           
            return account;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AccountDTO account)
        {
            var result  = await ValidateAccount(account);

            if(!result.Equals("valid"))
              return BadRequest(new {message = result }); 

            var  newAccount= await AccountServices.Create(account);

            return CreatedAtAction(nameof(GetById), new{id= newAccount.Id}, newAccount);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AccountDTO account)
        {
                if(id != account.Id)
                     return BadRequest(new {messge=$"El id {id} no coincide con el id {account.Id}. Ingreselo correctamente "});
                
               var accounttoUpdate = await AccountServices.GetById(id);

               if(accounttoUpdate is not null)
               {

                String result = await ValidateAccount(account);

                if(!result.Equals("valid"))
                  return BadRequest(new {message= result});

                await AccountServices.Update(id, account);
                return NoContent();
               } else
               {
                    return AccountNoFound(id);
               }
               
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
          var accountToDelete = await AccountServices.GetById(id);

          if(accountToDelete is not null)
          {
            await AccountServices.Delete(id);
             return Ok();
          }else
          {
            return  AccountNoFound(id);
          }
        }

        public NotFoundObjectResult AccountNoFound(int id)
        {
          return NotFound(new {message= $"La cuenta con ID {id} no esxiste"});
        }

        public async Task<String> ValidateAccount(AccountDTO account )
        {
            String result= "valid";

            var accountType= await accountServices.GetById(account.AccountType);

            if(accountType is null)
              result= $"El tipo de cuenta {account.AccountType}, no existe";
            
            var ClientId= account.ClientId.GetValueOrDefault();

            var Client= clientServices.GetById(ClientId);

            if(Client is null)
              result= $"El cliente con el id {ClientId}, no existe";

            return result;

        }
}