using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Bank
{

    class Program
    {
        static void Main(string[] args)
        {

           Bank a = new Bank();
            // a.ClientBase.Add(a.CreateAccount());
            // a.ClientBase[0].ShowInfo();
            // Console.WriteLine();
            // a.SerializeAccount(a.ClientBase[0]);
            // a.DeserializeAccount(a.ClientBase[0].ID).ShowInfo();
            a.Process();


        }
    }
}