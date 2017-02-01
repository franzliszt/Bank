using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dips {
    public class Bank {
        private static int accountNumber = 1;
        private const double minimumAmount = 50;
        private List<Account> accounts; // All the accounts in the bank.
        // Error messages.
        public const string NotValidAmount = "Not valid amount.";
        public const string AmountIsMoreThanBalance = "Amount is more than balance.";
        public const string NotAValidCustomer = "Not a valid customer.";
        public const string NotAValidAccount = "Not a valid account.";

        public Bank() {
            accounts = new List<Account>();
        }

        public Account CreateAccount(Person customer, Money initialDeposit) {
            if (initialDeposit.amount < minimumAmount)
                throw new ArgumentOutOfRangeException("amount", NotValidAmount);
            if (customer == null)
                throw new ArgumentNullException("customer", NotAValidCustomer);

            List<Account> existingCustomer = accounts.Where(account => account.owner.name == customer.name).ToList();
            if (existingCustomer.Count() > 0) {
                accountNumber = existingCustomer.Count() + 1;
                accounts.Add(new Account() {
                    accountName = customer.name + accountNumber,
                    currentAmount = initialDeposit.amount,
                    owner = customer
                });
                return accounts.Last();
            } else {
                // New customer.
                accountNumber = 1;
                Account newAccount = new Account() {
                    accountName = customer.name + accountNumber,
                    owner = customer,
                    currentAmount = initialDeposit.amount
                };
                accounts.Add(newAccount);
                return accounts.Last();
            }
        }

        public Account[] GetAccountsForCustomer(Person customer) {
            List<Account> myAccounts = new List<Account>();
            if (customer == null)
                throw new ArgumentNullException("customer", NotAValidCustomer);

            myAccounts = accounts.Where(owner => owner.owner.name == customer.name).ToList();
            return myAccounts.ToArray();
        }

        public void Deposit(Account to, Money amount) {
            if (amount.amount < minimumAmount)
                throw new ArgumentOutOfRangeException("amount", amount.amount, NotValidAmount);
            if (to == null)
                throw new ArgumentNullException(NotAValidCustomer);
            
            to.currentAmount += amount.amount;
            Console.WriteLine("Current amount for " + to.accountName + " is now: " + to.currentAmount + ".");
            
        }

        public void Withdraw(Account from, Money amount) {
            if (amount.amount <= 0)
                throw new ArgumentOutOfRangeException("amount", NotValidAmount);
            if (amount.amount <= from.currentAmount) {
                    from.currentAmount -= amount.amount;
                    Console.WriteLine("After witdraw " + amount.amount + " from account name " + from.accountName +
                        ", current amount is " + from.currentAmount + ".");
            } else {
                throw new ArgumentOutOfRangeException("amount", AmountIsMoreThanBalance);
            }        
        }

        public void Transfer(Account from, Account to, Money money) {
            if ((from == null) || (to == null))
                throw new ArgumentNullException("account", NotAValidAccount);
            if (money.amount > from.currentAmount)
                throw new ArgumentOutOfRangeException("amount", AmountIsMoreThanBalance);
            if (money.amount <= 0)
                throw new ArgumentOutOfRangeException("amount", NotValidAmount);
            
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
