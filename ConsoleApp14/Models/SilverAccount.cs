using System;
namespace Bank
{
    [Serializable]
    class SilverAccount : Account
    {
        public override string AccountType { get; } = "silver";
        public SilverAccount(string id, string password)
        {
            CreditPercentage = 3.5f;
            DepositPercentage = 3.5f;
            ID = id;
            Password = password;
        }
    }
}