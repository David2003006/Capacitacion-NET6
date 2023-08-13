using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Accounttype
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime RegDate { get; set; }
}
