using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Bank
{
	[Serializable]
	abstract class Account
	{
		private int _AccState;
        private string _id;
        private string _password;
        public string ID
		{
			get => _id;
			set
			{
				Regex regex = new Regex(@"^\d+\d$");
				if (value.Length < 6 || value.Length > 6 || regex.IsMatch(value)!=true)
				{
					throw new Exception("This ID doesn't match template, choose another one. ");

				}
				_id = value;
			}
		}
		public string Password
			{
				get => _id;
				set
				{
                 if (value.Length < 6 || value.Length > 10)
                 {
                     throw new Exception("This password is too short or long, choose another one");
                 }
                 _password = value;
				}
			}

		public virtual string AccountType { get; }
		public int AccountState
		{
			get => _AccState;
			set
			{
				if(value < 0)
				{
					throw new Exception("Negative AccountState value.");
				}
				_AccState = value;
			}
		}

        public float CreditAccount { get; set; }
        public float DepositAccount { get; set; }
        public float CreditPercentage { get; set; }
        public float DepositPercentage { get; set; }
        public void ShowInfo()
        {
            Console.WriteLine($"> ID: {ID}");
            Console.WriteLine($"> Password: {Password}");
            Console.WriteLine($"> Account type: {AccountType}");
            Console.WriteLine($"> Account status: {AccountState} $");
            Console.WriteLine($"> Credit account status: {CreditAccount} $");
            Console.WriteLine($"> Deposit account status: {DepositAccount} $");
            Console.WriteLine($"> Credit percentage: {CreditPercentage} %");
            Console.WriteLine($"> Deposit percentage: {DepositPercentage} %");
        }
      
    }
}