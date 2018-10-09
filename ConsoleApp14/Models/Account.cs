using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Bank
{
	[Serializable]
	abstract class Account
	{
		private string _id;
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
					
					_id = value;

				}
			}
		public virtual string AccountType { get; }
		public decimal AccountState { get; set; }

        public float CreditAccount { get; set; }
        public float DepositAccount { get; set; }
        public float CreditPercentage { get; set; }
        public float DepositPercentage { get; set; }
        public void ShowInfo()
        {
            Console.WriteLine($"> ID: {ID}");
            Console.WriteLine($"> Password: {Password}");
            Console.WriteLine($"> Account type: {AccountType}");
            Console.WriteLine($"> Account status: {AccountStatus} $");
            Console.WriteLine($"> Credit account status: {CreditAccount} $");
            Console.WriteLine($"> Deposit account status: {DepositAccount} $");
            Console.WriteLine($"> Credit percentage: {CreditPercentage} %");
            Console.WriteLine($"> Deposit percentage: {DepositPercentage} %");
        }
        static public bool CheckID(string ID, List<Account> ClientBase)
        {
           
           
           // catch (Exception ex)
           // {
           //     Console.Write("Error: ");
           //     Console.WriteLine(ex.Message);
           //     return false;
           // }
        }
        static public bool CheckPassword(string password)
        {
            try
            {
                if (password.Length < 6 || password.Length > 10)
                {
                    throw new Exception("This password is too short or long, choose another one");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}