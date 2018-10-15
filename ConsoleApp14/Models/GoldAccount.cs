using System;
namespace Bank
{
    [Serializable]
    class GoldAccount : Account
    {
		public GoldAccount()
		{
			AccountType = AccountType.Copper;
			DepositPercentage = 5.5f;
		}

    }
}