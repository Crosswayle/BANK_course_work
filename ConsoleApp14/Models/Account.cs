using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
namespace Bank
{
	[Serializable]
	abstract class Account
	{
		private int _AccState;
		private string _id;
		private float _deposite;
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
                if (File.Exists("User"+value+".txt"))
                {
                    throw new Exception("This ID is already created in base, choose another one. ");
                }
				_id = value;
			}
		}
		public string Password
			{
				get => _password;
				set
				{
                 if (value.Length < 6 || value.Length > 10)
                 {
                     throw new Exception("This password is too short or long, choose another one");
                 }
                 _password = value;
				}
			}

		public AccountType AccountType { get; set; } 
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

        public float DepositAccount
		{
			get => _deposite; 
			set
			{
				if(value >= 0)
				{
					_deposite = value;
				}
				else
				{
					throw new Exception("Negative DepositState value.");
				}
			}

		}

        public float DepositPercentage { get; set; }
        public void ShowInfo()
        {
			
            Console.WriteLine($"> ID: {ID}");
            Console.WriteLine($"> Password: {Password}");
            Console.WriteLine($"> Account type: {AccountType}");
            Console.WriteLine($"> Account status: {AccountState} $");
            Console.WriteLine($"> Deposit account status: {DepositAccount} $");
            Console.WriteLine($"> Deposit percentage: {DepositPercentage} %");
        }
      
    }
}