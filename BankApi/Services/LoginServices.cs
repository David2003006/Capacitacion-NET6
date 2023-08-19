using BankApi.Data;
using BankApi.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestBankApi.Data.DTOs;

public class LoginServices
 {
    
    private readonly BankContext _context;

    public LoginServices(BankContext context)
    {
        _context= context;
    }

    public async Task<User?> GetLogin(ClientLoginDto client)
    {
        return await _context.Users.
            SingleOrDefaultAsync(x => x.Email == client.Email && x.Pwd == client.Pwd);
    } 
 }