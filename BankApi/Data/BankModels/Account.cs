using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BankApi.Data.BankModels;

public partial class Account
{
    public int Id { get; set; }

    public int AccountType { get; set; }

    public int? ClientId { get; set; }

    public decimal Balance { get; set; }

    public DateTime RegDate { get; set; }

    [JsonIgnore]
    public virtual Accounttype AccountTypeNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Bancktransaction> Bancktransactions { get; set; } = new List<Bancktransaction>();

    [JsonIgnore]
    public virtual User Client { get; set; } = null!;
}
