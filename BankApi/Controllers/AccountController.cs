using System.Xml.Linq;
using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AccountController: ControllerBase
{
        private readonly BankContext _context;

        public AccountController(BankContext context)
        {
            _context= context;
        }

        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _context.Accounts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Account> GetById(int id)
        {
            var account= _context.Accounts.Find(id);

            if(account is null)
                return NotFound();
           
            return account;
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new{id= account.Id}, account);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, Account account)
        {
                if(id != account.Id)
                     return BadRequest();
                
                var existAccount= _context.Accounts.Find(id);

                if(existAccount is null)
                    return NotFound();

                existAccount.AccountType= account.AccountType;
                existAccount.ClientId= account.ClientId;
                existAccount.Balance= account.Balance;

                _context.SaveChanges();

                return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existAcount = _context.Accounts.Find(id);
            if(existAcount is null)
                return NotFound();
            
            _context.Accounts.Remove(existAcount);
            _context.SaveChanges();
            return Ok();
        }
}