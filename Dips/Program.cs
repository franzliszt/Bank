using System;

namespace Dips {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Programmet er skrevet av Stian Reistad Røgeberg.\n");
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Console.WriteLine(customer.name + " is opening an account.\n");
            
            try {
                // Creating an account for customer donald.
                Account myAccount1 = bank.CreateAccount(customer, new Money(1000));
                Console.WriteLine("Account name: " + myAccount1.accountName + ".\nCurrent amount: " +
                    myAccount1.currentAmount + ". Owner: " + myAccount1.owner.name);

                Console.WriteLine(customer.name + " is opening two more accounts.\n");
                Account myAccount2 = bank.CreateAccount(customer, new Money(2300));
                Account myAccount3 = bank.CreateAccount(customer, new Money(90000));

                // Customers accounts.
                Console.WriteLine("**** Here is customer " + customer.name + " accounts: ****\n");
                Account[] myAccounts = bank.GetAccountsForCustomer(customer);
                if (myAccounts.Length > 0 && myAccounts != null) {
                    foreach (Account account in myAccounts) {
                        Console.WriteLine("Account name " + account.accountName + ", current amount is " + account.currentAmount +
                            " and owner is " + account.owner.name);
                    }
                } else {
                    Console.WriteLine("Empty table of accounts.");
                }
                double deposit = 100.0;
                double withdrawAmount = 50.0;
                double transferAmount = 25.0;
                Console.WriteLine("\nDeposit amount " + deposit + " to account " + myAccounts[0].accountName + ", current amount is " +
                myAccounts[0].currentAmount + ".");
                // Deposit.
                bank.Deposit(myAccount1, new Money(deposit));

                // Withdraw.
                Console.WriteLine("\nWithdraw " + withdrawAmount + " from " + myAccounts[0].accountName + ".");
                bank.Withdraw(myAccounts[0], new Money(withdrawAmount));

                // Transfer.
                Console.WriteLine("\nTansfer amount " + transferAmount + " from account " + myAccounts[0].accountName +
                    " to account " + myAccounts[1].accountName + ".");
                bank.Transfer(myAccounts[0], myAccounts[1], new Money(transferAmount));

                Person customer2 = new Person("dolly");
                Account dollyAccount = bank.CreateAccount(customer2, new Money(300000));
                Console.WriteLine("\n" + dollyAccount.owner.name + " has opened an account.\nAccount name is " +
                    dollyAccount.accountName + " and has initial deposit " + dollyAccount.currentAmount + ".");
                double moreThanBalance = 1000000;
                Console.WriteLine(dollyAccount.owner.name + " will try to withdraw more money than current balance:");
                Console.WriteLine("Current balance is " + dollyAccount.currentAmount + ",-.");
                try {
                    bank.Withdraw(dollyAccount, new Money(moreThanBalance));
                } catch (ArgumentOutOfRangeException e) {
                    Console.WriteLine(Bank.AmountIsMoreThanBalance);
                }
            } catch (ArgumentOutOfRangeException e) {
                Console.WriteLine(Bank.NotValidAmount);
            } catch (ArgumentNullException e) {
                Console.WriteLine(Bank.NotAValidCustomer);
            } 
            Console.ReadKey();
        }
    }
}
