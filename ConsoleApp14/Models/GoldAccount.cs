using System;
namespace Bank
{
    [Serializable]
    class GoldAccount : Account
    {
        public override string AccountType { get; } = "gold";
        public GoldAccount(string id, string password)
        {
            CreditPercentage = 2.2f;
            DepositPercentage = 5.5f;
            ID = id;
            Password = password;
        }
    }
}