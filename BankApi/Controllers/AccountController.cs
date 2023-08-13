using System.Xml.Linq;
using BankApi.Services;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AccountController: ControllerBase
{
        private readonly AccountServices _services;

        public AccountController(AccountServices services)
        {
            _services= services;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _services.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetById(int id)
        {
            var account= _services.GetById(id);

            if(account is null)
                return NotFound();
           
            return account;
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            var newAccount = _services.Create(account);

            return CreatedAtAction(nameof(GetById), new{id= newAccount.Id}, newAccount);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Account account)
        {
                if(id != account.Id)
                     return BadRequest();
                
               var accounttoUpdate = _services.GetById(id);

               if(accounttoUpdate is not null)
               {
                _services.Update(id, account);
                return NoContent();
               } else
               {
                    return NotFound();
               }
               
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
          var accountToDelete = _services.GetById(id);

          if(accountToDelete is not null)
          {
            _services.Delete(id);
             return Ok();
          }else
          {
            return  NotFound();
          }
        }
}