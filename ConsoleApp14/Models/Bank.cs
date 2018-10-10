using System;
using System.Collections.Generic;
using System.Linq;
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
		Account _currentAcc;
		public Account CurrentAccount
		{
			get => _currentAcc;
			set
			{
				_currentAcc = value;

			}

		}

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
            Console.Write("Enter type - key: ");
            string type = String.Empty;
            while (true)
            {
				type = Console.ReadLine();
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
                Console.Write("Wrong key, try again:");
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
			bool IsSuccsess = false;
			while (IsSuccsess == false)
			{
				try
				{
					
					tmpAccount = FabricMethod(GetAccountType());
					tmpAccount.ID = GetId();
					tmpAccount.Password = GetPassword();
					IsSuccsess = true;
				}
				catch (Exception ex)
				{
					IsSuccsess = false;
					Console.WriteLine(ex.Message);
				}
			}
			CurrentAccount = tmpAccount;
			return tmpAccount;
		}

		public void AddAccount(Account account)
		{
            bool isSuccsess = true;
            try
            {
				if(ClientBase.Exists(a => a.ID == account.ID) == true)
				{
					throw new Exception("This id is already exists");
				}
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

		public void Transfering(Account accFrom)
		{
			Console.Write("Enter destination id: ");
			string ID = Console.ReadLine();
			if (ClientBase.Count(a => a.ID == ID) == 0)
			{
				throw new Exception("There is no such id in base.");
			}
			if (ID == CurrentAccount.ID)
			{
				throw new Exception("You can't transfer your money on your accaunt");
			}
			Account accTo = null;
			foreach (var item in ClientBase)
			{
				if(item.ID == ID)
				{
					accTo = item;
					break;
				}
			}

			Console.Write("Enter money value: ");
			int value = Convert.ToInt32(Console.ReadLine());
			TransferMoney(accFrom, accTo, value);
		}

		public void TransferMoney(Account accFrom, Account accTo, int value)
		{
            try
            {
				accFrom.AccountState -= value;
				accTo.AccountState += value;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		public void AddMoney(Account account, int value)
		{
			account.AccountState += value;
		}
		public void MoneyWithdraw(Account account, int value)
		{
			try
			{
				account.AccountState -= value;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
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
			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(300);
				Console.Write('.');
			}
			Thread.Sleep(300);
			Console.Clear();
			Console.WriteLine("Account was successfully created.");
			Thread.Sleep(1200);
			Console.Clear();

			bool isWorking = true;
			while (isWorking == true)
			{
				Console.WriteLine("Choose action: ");
				Console.WriteLine("1. Money transfer.");
				Console.WriteLine("2. Widthdraw money.");
				Console.WriteLine("3. Add money.");
				Console.WriteLine("0. Exit.");
				Console.Write("> ");
				choise = Convert.ToInt32(Console.ReadLine());
				Thread.Sleep(200);
				Console.Clear();
				bool isSuccsess = true;
				if (choise == 1)
				{

					try
					{
						Transfering(CurrentAccount);
					}
					catch (Exception ex)
					{
						isSuccsess = false;
						Console.WriteLine(ex.Message);
					}

					for (int i = 0; i < 3; i++)
					{
						Thread.Sleep(300);
						Console.Write('.');
					}
					Console.WriteLine();
					if (isSuccsess == true)
					{
						Console.WriteLine("Transfering was succsessful.");
					}
					Thread.Sleep(2000);
				}
				if (choise == 0)
				{
					break;
				}
				if (choise == 3)
				{
					Console.Write("Enter count of money: ");
					int moneyCount = Convert.ToInt32(Console.ReadLine());
					AddMoney(CurrentAccount, moneyCount);
					if (isSuccsess == true)
					{
						Console.WriteLine("Operation is succsessful.");
					}
					for (int i = 0; i < 3; i++)
					{
						Thread.Sleep(300);
						Console.Write('.');
					}
					Console.WriteLine();
					Thread.Sleep(1500);


				}
				if (choise == 2)
				{
					Console.Write("Enter count of money: ");
					int moneyCount = Convert.ToInt32(Console.ReadLine());
					
					MoneyWithdraw(CurrentAccount, moneyCount);
					
					for (int i = 0; i < 3; i++)
					{
						Thread.Sleep(300);
						Console.Write('.');
					}			
					Thread.Sleep(2000);
				}


				Console.Clear();
			}
			foreach (var item in ClientBase)
			{
				SerializeAccount(item);
			}
			

        }
    }
}