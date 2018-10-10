using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
namespace Bank
{
	public enum AccountType {Gold, Silver, Copper}

    class Bank
    {
        public List<Account> ClientBase = new List<Account>();

		public Account FabricMethod(AccountType accountType)
		{
            if (accountType == AccountType.Gold)
            { return new CopperAccount(); }
            if (accountType == AccountType.Silver)
            { return new SilverAccount(); }
            if (accountType == AccountType.Copper)
            { return new CopperAccount(); }
            return null;
        }

		public AccountType GetAccountType()
		{
            Console.WriteLine("Enter type - key: ");
            string type = Console.ReadLine();
            while (true)
            {
                if (type == "Gold")
                {
                    return AccountType.Gold;
                }
                if (type == "Silver")
                {
                    return AccountType.Silver;
                }
                if (type == "Copper")
                {
                    return AccountType.Copper;
                }
                Console.WriteLine("Wrong key, try again:");
            }
		}

		private string GetId()
		{
            Console.Write("Enter account ID: ");
            return Console.ReadLine();
		}

        private string GetPassword()
        {
            Console.Write("Enter account password: ");
            return Console.ReadLine();
        }

        public Account CreateAccount()
        {
           
			Account tmpAccount = null;
			try
			{
				tmpAccount = FabricMethod(GetAccountType());
				tmpAccount.ID = GetId();
                tmpAccount.Password = GetPassword();
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
            return tmpAccount;
			}

		public void AddAccount(Account account)
		{
            bool isSuccsess = true;
            try
            {
                ClientBase.Add(account);
            }
            catch (Exception ex)
            {
                isSuccsess = false;
                Console.WriteLine(ex.Message);
            }

            if (isSuccsess == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(300);
                    Console.Write('.');
                }
                Thread.Sleep(300);
                Console.Clear();
                Console.WriteLine("Account was successfully created.");
            }
        }
        public void SerializeAccount(Account someAccount)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream("User" + someAccount.ID + ".txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(file, someAccount);
                Console.WriteLine("User id:" + someAccount.ID + " was saved to database.");
            }
        }
        public Account DeserializeAccount(string id)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream("User" + id+".txt", FileMode.OpenOrCreate))
            {
                Account someAcc = (Account)formatter.Deserialize(file);
                Console.WriteLine("User id:" + id + "was deserialized.");
                return someAcc;
            }

        }
		public void Transfer(Account accFrom, Account accTo, int value)
		{
            try
            {

            }
            catch { }
		}


        public void Process()
        {
            int choise = new int();
            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Sign in");
            Console.WriteLine("2. Sign up");
            Console.Write("> ");
			bool isCorrect = false;
            while (isCorrect != true)
            {
                choise = Convert.ToInt32(Console.ReadLine());
                if (choise == 1)
                {
                    Console.Clear();
					isCorrect = true;
				}
                if (choise == 2)
                {
                    Console.Clear();
                    ClientBase.Add(CreateAccount());
					isCorrect = true;
                }
                else
                {
                    Console.WriteLine("Wrong choise, try again:");
                    Console.Write("> ");
                }
            }
			Console.WriteLine("Choose action: ");
			Console.WriteLine("");


        }
    }
}