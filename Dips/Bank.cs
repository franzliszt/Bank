using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dips {
    public class Bank {
        private static int accountNumber = 1;
        private List<Account> accounts;
        public const string AmountIsLessThanZero = "Amount is less than zero";
        public const string AmountIsMoreThanBalance = "Amount is more than balance";
        public const string NotAValidCustomer = "Not a valid customer";

        public Bank() {
            accounts = new List<Account>();
        }

        public Account CreateAccount(Person customer, Money initialDeposit) {
            if (initialDeposit.amount < 0)
                throw new ArgumentOutOfRangeException("amount", AmountIsLessThanZero);

            if (customer == null)
                throw new ArgumentNullException("customer", NotAValidCustomer);
            
                Account newAccount = new Account() {
                    accountName = customer.name + accountNumber,
                    owner = customer,
                    currentAmount = initialDeposit.amount
                };
                accounts.Add(newAccount);

                // Increment for next account number
                accountNumber++;
                return newAccount;
        }

        public Account[] GetAccountsForCustomer(Person customer) {
            if (customer == null)
                throw new ArgumentNullException("customer", NotAValidCustomer);

            
            List<Account> myAccounts = new List<Account>();
            foreach (var account in accounts) {
                if (customer.name.Equals(account.owner.name))
                    myAccounts.Add(account);
            }
            return myAccounts.ToArray();
        }

        public void Deposit(Account to, Money amount) {
            if (amount.amount < 0)
                throw new ArgumentOutOfRangeException("amount", amount.amount, AmountIsLessThanZero);
            if (to == null)
                throw new ArgumentNullException("account do not exist");
            
            to.currentAmount += amount.amount;
            Console.WriteLine("Current amount for " + to.accountName + " is now: " + to.currentAmount + ".");
            
        }

        public void Withdraw(Account from, Money amount) {
            if (amount.amount <= from.currentAmount) {
                from.currentAmount -= amount.amount;
                Console.WriteLine("After witdraw " + amount.amount + " from account name " + from.accountName + 
                    ", current amount is " + from.currentAmount + ".");
            }
        }

        public void Transfer(Account from, Account to, Money money) {
            if (money.amount < 0)
                throw new ArgumentOutOfRangeException("amount", money.amount, AmountIsLessThanZero);
            if (money.amount > from.currentAmount)
                throw new ArgumentOutOfRangeException("amount", money.amount, AmountIsMoreThanBalance);
            
            from.currentAmount -= money.amount;
            to.currentAmount += money.amount;

            Console.WriteLine("Transfered " + money.amount + " to account " + to.accountName + 
                " from account " + from.accountName + ".\nCurrent amount is now " + from.currentAmount 
                + " and account " + to.accountName + " is now " + to.currentAmount + ".");
        }
    }


    public class Person {
        public string name { get; set; }

        public Person(string name) {
            this.name = name;
        }
    }

    public class Money {
        public double amount { get; set; }

        public Money(double amount) {
            this.amount = amount;
        }
    }

    public class Account {
        public string accountName { get; set; }
        public Person owner { get; set; }
        public double currentAmount { get; set; }
    }
}
