using System;
namespace Bank
{
    [Serializable]
    class CopperAccount : Account
    {
		public CopperAccount()
		{
			AccountType = AccountType.Copper;
			DepositPercentage = 2.2f;

		}
       

    }
}