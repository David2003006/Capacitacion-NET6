namespace TestBankApi.Data.DTOs;

public class AccountDtoOunt
{
    public int Id { get; set; }

    public String AccountName { get; set; } = null;

    public String ClientName { get; set; } = null;

    public decimal Balance { get; set; }

    public DateTime RegDate { get; set; }

}