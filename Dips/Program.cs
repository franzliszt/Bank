using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dips {
    class Program {
        static void Main(string[] args) {
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Console.WriteLine(customer.name + " is opening an account.\n");

            // Creating an account for customer donald.
            Account myAccount1 = null;
            Account myAccount2 = null;
            Account myAccount3 = null;
            try {
                myAccount1 = bank.CreateAccount(customer, new Money(1000));
                Console.WriteLine("Account name: " + myAccount1.accountName + ".\nCurrent amount: " +
                    myAccount1.currentAmount + ". Owner: " + myAccount1.owner.name);

                Console.WriteLine(customer.name + " is opening two more accounts.\n");
                myAccount2 = bank.CreateAccount(customer, new Money(2300));
                myAccount3 = bank.CreateAccount(customer, new Money(90000));
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine(Bank.AmountIsLessThanZero);
            } catch (ArgumentNullException e) {
                Console.WriteLine(Bank.NotAValidCustomer);
            }

            // Customers accounts.
            Console.WriteLine("**** Here is customer " + customer.name + " accounts: ****\n");
            Account[] myAccounts = bank.GetAccountsForCustomer(customer);
            if (myAccounts.Length > 0) {
                foreach (Account account in myAccounts) {
                    Console.WriteLine("Account name " + account.accountName + ", current amount is " + account.currentAmount +
                        " and owner is " + account.owner.name);
                }
            }

            double deposit = 100.0;
            double withdrawAmount = 50.0;
            double transferAmount = 25.0;
            Console.WriteLine("\nDeposit amount " + deposit +" to account " + myAccount1.accountName + ", current amount is " +
                myAccount1.currentAmount + ".");

            // Deposit, Withdraw and transfer money.
            try {
                // Deposit.
                bank.Deposit(myAccount1, new Money(deposit));

                // Withdraw.
                Console.WriteLine("\nWithdraw " + withdrawAmount + " from " + myAccount1.accountName + ".");
                bank.Withdraw(myAccount1, new Money(withdrawAmount));

                // Transfer.
                Console.WriteLine("\nTansfer amount " + transferAmount + " from account " + myAccount1.accountName +
                    " to account " + myAccount2.accountName + ".");
                bank.Transfer(myAccount1, myAccount2, new Money(transferAmount));
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine(Bank.AmountIsLessThanZero);
            } catch (ArgumentNullException e) {
                Console.WriteLine(Bank.NotAValidCustomer);
            }

            Console.ReadKey();
        }
    }
}
