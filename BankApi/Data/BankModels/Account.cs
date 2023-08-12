using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Account
{
    public int ID { get; set; }

    public int AccountType { get; set; }

    public int ClientId { get; set; }

    public decimal Balance { get; set; }

    public DateTime RegDate { get; set; }

    public virtual Accounttype AccountTypeNavigation { get; set; } = null!;

    public virtual ICollection<Bancktransaction> Bancktransactions { get; set; } = new List<Bancktransaction>();

    public virtual User Client { get; set; } = null!;
}
