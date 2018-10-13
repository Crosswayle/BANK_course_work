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

    [Serializable]
    class Bank
    {
		bool isSigningInPossible = true;
        public List<string> BaseID = new List<string>();
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
            BaseID.Add(tmpAccount.ID);
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
        public void SignIn(string id, string password)
        {
            bool isExist = false;
            Account tmpAccount= null;
            foreach (var item in ClientBase)
            {
                if(item.ID == id)
                {
                    isExist = true;
                    tmpAccount = item;
                }
            }
            if(isExist == false)
            {
                throw new Exception("There is no such account.");
            }
            else
            {
                CurrentAccount = tmpAccount;
                Console.WriteLine("You have entered to your account succsessfully.");
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
        public void SerializeIDBase(List<string> IDbase)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream("BaseID", FileMode.OpenOrCreate))
            {
                formatter.Serialize(file, IDbase);
                Console.WriteLine("ID base was serialized");
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

        public List<Account> AccountBaseDeserializator()
        { 
            List<Account> tmpClientBase = new List<Account>();
            foreach (var item in BaseID)
            {
                tmpClientBase.Add(DeserializeAccount(item));
            }
            return tmpClientBase;
        }

		public bool IsIdBaseEmpty()
		{
			if (System.IO.File.Exists("BaseID") == false)
			{
				return true;
			}

			string tmpIDBase;

			using (FileStream file = new FileStream("BaseID", FileMode.OpenOrCreate, FileAccess.Read))
			{
				StreamReader formatter = new StreamReader(file);
				tmpIDBase = formatter.ReadToEnd();
			}
	
			if (tmpIDBase.Replace(" ", "").Length != 0)
			{
				return false;
			}

			return true;
		}

		public List<string> IDBaseDeserializator()
        {

            List<string> tmpIDBase = new List<string>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream file = new FileStream("BaseID", FileMode.OpenOrCreate))
            {
                tmpIDBase = (List<string>)formatter.Deserialize(file);
            }
            return tmpIDBase;
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
		public void ShowAccountInfo(Account account)
		{
			account.ShowInfo();
		}
		public void AddToDeposit(int value)
		{

		}

        public void MenuIO()
        {
            int choise = new int();
            Console.WriteLine("Choose action:");
		flag:;
            Console.WriteLine("1. Sign in");
            Console.WriteLine("2. Sign up");
            Console.Write("> ");
            bool isCorrect = false;
            while (isCorrect != true)
            {
                choise = Convert.ToInt32(Console.ReadLine());
				if (choise == 1)
				{
					if (isSigningInPossible == false)
					{
						Console.WriteLine("ID Base is empty, please sign up.");
						Thread.Sleep(1200);
						Console.Clear();
						goto flag;
					}
					else
					{
						while (isCorrect != true)
						{
							Console.Write("Enter your id: ");
							string _signInId = Console.ReadLine();
							Console.Write("Enter your password: ");
							string _signInPass = Console.ReadLine();
							try
							{
								SignIn(_signInId, _signInId);
								isCorrect = true;
							}
							catch (Exception ex)
							{
								isCorrect = false;
								Console.WriteLine(ex.Message);
							}
							for (int i = 0; i < 3; i++)
							{
								Thread.Sleep(300);
								Console.Write('.');
							}
							Thread.Sleep(300);
							Console.Clear();
						}
					}
				}
				else
				{
					if (choise == 2)

					{
						Console.Clear();
						ClientBase.Add(CreateAccount());
						isCorrect = true;
						for (int i = 0; i < 3; i++)
						{
							Thread.Sleep(300);
							Console.Write('.');
						}
						Thread.Sleep(300);
						Console.Clear();
						Console.WriteLine("Account was successfully created.");
					}
					else
					{
						Console.WriteLine("Wrong choise, try again:");
						Console.Write("> ");
					}
				}
            }
           
            Thread.Sleep(1200);
            Console.Clear();

            
            while (true)
            {
                Console.WriteLine("Choose action: ");
                Console.WriteLine("1. Money transfer.");
                Console.WriteLine("2. Widthdraw money.");
				Console.WriteLine("3. Add money.");
				Console.WriteLine("4. Show account info.");
				Console.WriteLine("5. Deposit money.");
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
				else if (choise == 0)
				{
					break;
				}
				else if (choise == 3)
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
				else if (choise == 2)
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
				else if (choise == 4)
				{
					ShowAccountInfo(CurrentAccount);
					Console.ReadKey();
				}
				else if (choise == 5)
				{
					isSuccsess = false;
					while (isSuccsess == false)
					{
						Console.WriteLine("Choose action: ");
						Console.WriteLine("1. Add money.");
						Console.WriteLine("2. Widthdraw money.");
						Console.Write("> ");
						choise = Convert.ToInt32(Console.ReadLine());

						if(choise == 1)
						{
							Console.Write("Enter value: ");
							int value = Convert.ToInt32(Console.ReadLine());

						}
					}
				}
				else
				{
					Console.WriteLine("Invalid value, try again.");
					Thread.Sleep(1500);
				}

                Console.Clear();
            }


        }

        public void Process()
        {

      
			if (IsIdBaseEmpty() == false)
			{
				BaseID = IDBaseDeserializator();
			}
			else { isSigningInPossible = false;}
			ClientBase = AccountBaseDeserializator();

			Console.WriteLine();
			MenuIO();

            foreach (var item in ClientBase)
            {
                SerializeAccount(item);
            }
            SerializeIDBase(BaseID);
        }
    }
}