using System;
namespace Bank
{
    [Serializable]
    class SilverAccount : Account
    {
        public SilverAccount()
        {
			AccountType = AccountType.Copper;
			DepositPercentage = 3.5f;
		}

    }
}