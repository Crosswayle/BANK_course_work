using System;
namespace Bank
{
    [Serializable]
    class CopperAccount : Account
    {
		public CopperAccount()
		{

		}
        public override string AccountType { get; } = "copper";
        public CopperAccount(string id, string password)
        {
            CreditPercentage = 5.5f;
            DepositPercentage = 2.2f;
            ID = id;
            Password = password;
        }

    }
}