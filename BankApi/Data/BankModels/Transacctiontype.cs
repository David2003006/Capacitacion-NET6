﻿using System;
using System.Collections.Generic;

namespace BankApi.Data.BankModels;

public partial class Transacctiontype
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime RegDate { get; set; }

    public virtual ICollection<Bancktransaction> Bancktransactions { get; set; } = new List<Bancktransaction>();
}
