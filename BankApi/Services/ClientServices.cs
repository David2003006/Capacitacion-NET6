using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services;
public class ClientServices
{
        private readonly BankContext _context;

        public ClientServices(BankContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
          return  await  _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> create(User Newuser )
        {
            _context.Users.Add(Newuser);
           await  _context.SaveChangesAsync();

           return Newuser;
        }

        public async Task Update ( User user)
        {
            var FoundUser= await GetById(user.Id);

            if(FoundUser is not null)
            {
                FoundUser.Name= user.Name;
                FoundUser.PhoneNumber= user.PhoneNumber;
                FoundUser.Email = user.Email;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var FoundUser = await GetById(id);

            if(FoundUser is not null)
            {
                _context.Users.Remove(FoundUser);
                await _context.SaveChangesAsync();
            }
        }
}