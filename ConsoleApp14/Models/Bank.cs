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


			return new CopperAccount();
		}

		public AccountType GetAccountType()
		{
			bool validType = false;
            Regex reg = new Regex(@"Gold|Silver|Copper");
            Console.Write("Enter type-key: ");
            string type = String.Empty;
            while (validType != true)
            {
                type = Console.ReadLine();
                if (type == "default" || type == "DEFAULT")
                {
                    for (int i = 0; i < 3; i++)
                    {
                        Thread.Sleep(300);
                        Console.Write('.');
                    }
                    Thread.Sleep(300);
                    Console.Clear();
                    Console.WriteLine("Account was successfully created.");
                    string def_id = Convert.ToString(rnd.Next(100000, 999999));
                    string def_pass = Convert.ToString(rnd.Next(100000, 999999999));
                    return new CopperAccount(def_id, def_pass);
                }
                if (reg.IsMatch(type))
                {
                    validType = true;
                }
                else
                {
                    Console.Write("Wrong key, try again: ");
                }
            }

			//
			return AccountType.Copper;


		}

		private string GetId()
		{

			return "111222";
		}

        public Account CreateAccount()
        {
            Random rnd = new Random();
			Account tmpAccount = null;
			try
			{

				tmpAccount = FabricMethod(GetAccountType());
				tmpAccount.ID = GetId();
			}
			catch (CretionException ex)
			{
			}
			catch()



		 





			Console.Clear();

			// ID
			Console.Write("Enter account ID: ");
			string id = String.Empty;
			id = Console.ReadLine();

			Console.Clear();


			//PASSWORD
			bool validPassword = false;
			Console.Write("Enter account password: ");
			string password = String.Empty;
			while (validPassword != true)
			{
				password = Console.ReadLine();
				if (Account.CheckPassword(password))
				{
					validPassword = true;
				}
			}

			AddAccount(type, id, password);
			Console.Clear();

			return tmpAccount;
			}

		public void AddAccount(string type, string id, string password)
		{
			if (type == "Gold")
			{
				for (int i = 0; i < 3; i++)
				{
					Thread.Sleep(300);
					Console.Write('.');
				}
				Thread.Sleep(300);
				Console.Clear();
				Console.WriteLine("Account was successfully created.");
				ClientBase.Add(new GoldAccount(id, password));
			}
			if (type == "Silver")
			{
				for (int i = 0; i < 3; i++)
				{
					Thread.Sleep(300);
					Console.Write('.');
				}
				Thread.Sleep(300);
				Console.Clear();
				Console.WriteLine("Account was successfully created.");
				ClientBase.Add(new SilverAccount(id, password));
			}
			if (type == "Copper")
			{
				for (int i = 0; i < 3; i++)
				{
					Thread.Sleep(300);
					Console.Write('.');
				}
				Thread.Sleep(300);
				Console.Clear();
				Console.WriteLine("Account was successfully created.");
				ClientBase.Add(new CopperAccount(id, password));
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