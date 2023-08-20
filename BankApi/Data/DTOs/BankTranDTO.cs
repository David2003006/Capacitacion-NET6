namespace TestBankApi.Data.DTOs;

public class BankTranDTO
{
    public int Id { get; set; }
    public int AccountId { get; set; }

    public int TransactionType { get; set; }

    public decimal Amount { get; set; }

    public int? ExternalAcount { get; set; }

}