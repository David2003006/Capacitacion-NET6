using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Bancktransaction
{
    public int Id { get; set; }

    public int AccountId { get; set; }

    public int TransactionType { get; set; }

    public decimal Amount { get; set; }

    public int? ExternalAcount { get; set; }

    public DateTime RegDate { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Transacctiontype TransactionTypeNavigation { get; set; } = null!;
}
