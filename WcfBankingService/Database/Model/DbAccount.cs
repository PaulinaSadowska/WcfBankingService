﻿using LinqToDB.Mapping;

namespace WcfBankingService.Database.Model
{
    /// <summary>
    /// Accounts table data model
    /// </summary>
    [Table(Name = "Accounts")]
    public class DbAccount
    {
        [PrimaryKey] public int Id;
        [Column(Name = "user_id"), NotNull] public int UserId;
        [Column(Name = "balance"), NotNull] public decimal BalanceValue;
        [Column(Name = "innerAccountNum"), NotNull] public string InnerAccountNumber;
    }
}