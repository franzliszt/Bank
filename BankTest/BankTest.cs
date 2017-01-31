using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dips;

namespace BankTest {
    [TestClass]
    public class BankTest {
        [TestMethod]
        public void CreateAccountOK_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            string expectedName = "dolly";
            double amount = 100;

            // act
            Account account = bank.CreateAccount(customer, new Money(amount));

            // assert
            Assert.IsNotNull(account);
            Assert.AreEqual(expectedName, account.owner.name);
        }

        [TestMethod]
        public void CreateAccount_InitialAmountIsLessThanZero_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            double initialDeposit = -1;

            // act
            try {
                bank.CreateAccount(customer, new Money(initialDeposit));
            } catch (ArgumentOutOfRangeException e) {
                // assert
                StringAssert.Contains(e.Message, Bank.AmountIsLessThanZero);
            }
        }

        [TestMethod]
        public void CreateAccount_CustomerIsNull_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = null;
            double initialDeposit = 200.50;

            // act
            try {
                bank.CreateAccount(customer, new Money(initialDeposit));
            } catch (ArgumentNullException e) {
                // assert
                StringAssert.Contains(e.Message, Bank.NotAValidCustomer);
            }
        }

        
        [TestMethod]
        public void GetAccountsForCustomerOK_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account1 = bank.CreateAccount(customer, new Money(1000));
            Account account2 = bank.CreateAccount(customer, new Money(2000));
            Account account3 = bank.CreateAccount(customer, new Money(3000));
            Account account4 = bank.CreateAccount(customer, new Money(4000));

            string expectedName = "donald";
            double expectedAmount1 = 1000;
            string expectedAccountName1 = account1.accountName;
            
            double expectedAmount2 = 2000;
            string expectedAccountName2 = account2.accountName;
            
            double expectedAmount3 = 3000;
            string expectedAccountName3 = account3.accountName;
            
            double expectedAmount4 = 4000;
            string expectedAccountName4 = account4.accountName;

            // act
            Account[] accounts = bank.GetAccountsForCustomer(customer);

            //assert
            Assert.IsTrue(accounts.Length == 4);

            Assert.AreEqual(expectedName, accounts[0].owner.name);
            Assert.IsTrue(expectedAmount1 == accounts[0].currentAmount);
            Assert.AreEqual(expectedAccountName1, accounts[0].accountName);

            Assert.AreEqual(expectedName, accounts[1].owner.name);
            Assert.IsTrue(expectedAmount2 == accounts[1].currentAmount);
            Assert.AreEqual(expectedAccountName2, accounts[1].accountName);

            Assert.AreEqual(expectedName, accounts[02].owner.name);
            Assert.IsTrue(expectedAmount3 == accounts[2].currentAmount);
            Assert.AreEqual(expectedAccountName3, accounts[2].accountName);

            Assert.AreEqual(expectedName, accounts[3].owner.name);
            Assert.IsTrue(expectedAmount4 == accounts[3].currentAmount);
            Assert.AreEqual(expectedAccountName4, accounts[3].accountName);
        }

        [TestMethod]
        public void GetAccountsForCustomerFail_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("donald");
            bank.CreateAccount(customer, new Money(1000));
            bank.CreateAccount(customer, new Money(1000));
            bank.CreateAccount(customer, new Money(1000));
            bank.CreateAccount(customer, new Money(1000));

            Person dolly = new Person("dolly");
            string expectedName = dolly.name;
            int expectedLength = 0;

            // act
            Account[] accounts = bank.GetAccountsForCustomer(dolly);

            //assert
            foreach (Account account in accounts) {
                Assert.IsNull(account);
                Assert.AreNotEqual(expectedName, account.owner.name);
            }
            Assert.IsTrue(expectedLength == accounts.Length);
        }

        [TestMethod]
        public void GetAccountsForCustomer_NotAValidCustomer_Test() {
            // act
            Bank bank = new Bank();
            Person customer = null;
            Account[] accounts = null;

            // act
            try {
                accounts = bank.GetAccountsForCustomer(customer);
            } catch (ArgumentNullException e) {
                // assert
                StringAssert.Contains(e.Message, Bank.NotAValidCustomer);
            }
        }

        [TestMethod]
        public void DepositOK_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account1 = bank.CreateAccount(customer, new Money(2000));
            int expectedResult1 = 2500;
            Account account2 = bank.CreateAccount(customer, new Money(3000));
            int expectedResult2 = 3400;

            // act
            bank.Deposit(account1, new Money(500));
            bank.Deposit(account2, new Money(400));

            // assert
            Assert.AreEqual(expectedResult1, account1.currentAmount);
            Assert.AreEqual(expectedResult2, account2.currentAmount);
        }

        [TestMethod]
        public void DepositFail_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account1 = bank.CreateAccount(customer, new Money(2000));
            int expectedResult1 = 2500;
            Account account2 = bank.CreateAccount(customer, new Money(3000));
            int expectedResult2 = 3400;

            // act
            bank.Deposit(account1, new Money(500));
            bank.Deposit(account2, new Money(400));

            // assert
            Assert.AreEqual(expectedResult1, account1.currentAmount);
            Assert.AreEqual(expectedResult2, account2.currentAmount);
        }

        [TestMethod]
        public void Withdraw_WhenAmountIsMoreThanBalance_Test() {
            // arrange
            double startBalance = 11;
            double amount = 200;
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account = bank.CreateAccount(customer, new Money(startBalance));

            // act
            try {
                bank.Withdraw(account, new Money(amount));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.AmountIsMoreThanBalance);
            }
        }

        [TestMethod]
        public void Withdraw_WhenAmountIsLessThanZero_Test() {
            // arrange
            double startBalance = 11;
            double amount = -1;
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account = bank.CreateAccount(customer, new Money(startBalance));

            // act
            try {
                bank.Withdraw(account, new Money(amount));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.AmountIsMoreThanBalance);
            }
        }

        [TestMethod]
        public void WithdrawOK_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("donald");
            double expectedValue1 = 1000;
            double expectedValue2 = 500;

            // act
            Account account1 = bank.CreateAccount(customer, new Money(2000));
            Account account2 = bank.CreateAccount(customer, new Money(2000));
            bank.Withdraw(account1, new Money(1000));
            bank.Withdraw(account2, new Money(1500));

            // assert
            Assert.AreEqual(expectedValue1, account1.currentAmount);
            Assert.AreEqual(expectedValue2, account2.currentAmount);
        }

        [TestMethod]
        public void Transfer_WhenAmountIsMoreThanBalance_Test() {
            // arrange
            double startBalance = 11;
            double amount = 200;
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account = bank.CreateAccount(customer, new Money(startBalance));

            // act
            try {
                bank.Withdraw(account, new Money(amount));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.AmountIsMoreThanBalance);
            }
        }

        [TestMethod]
        public void Transfer_WhenAmountIsLessThanZero_Test() {
            // arrange
            double startBalance = 2000;
            double amount = -0.5;
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account = bank.CreateAccount(customer, new Money(startBalance));

            // act
            try {
                bank.Withdraw(account, new Money(amount));
            } catch (ArgumentOutOfRangeException e) {
                // assert
                StringAssert.Contains(e.Message, Bank.AmountIsMoreThanBalance);
            }
        }

        [TestMethod]
        public void TransferOK_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("donald");
            Account account1 = bank.CreateAccount(customer, new Money(10));
            Account account2 = bank.CreateAccount(customer, new Money(20));
            double expectedResult = 25;

            // act
            bank.Transfer(account1, account2, new Money(5));
            double actualResult = account2.currentAmount;

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
