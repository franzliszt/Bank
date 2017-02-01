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
            string expectedAccountName = "dolly1";
            double expectedAmount = 100;
            double amount = 100;

            // act
            Account account = bank.CreateAccount(customer, new Money(amount));

            // assert
            Assert.IsNotNull(account);
            Assert.AreEqual(expectedName, account.owner.name);
            Assert.AreEqual(expectedAccountName, account.accountName);
            Assert.AreEqual(expectedAmount, account.currentAmount);
        }

        [TestMethod]
        public void CreateAccount_InitialAmountIsLessThanStartAmount_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            double initialDeposit = -1;

            // act
            try {
                bank.CreateAccount(customer, new Money(initialDeposit));
            } catch (ArgumentOutOfRangeException e) {
                // assert
                StringAssert.Contains(e.Message, Bank.NotValidAmount);
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
            string expectedAccountName1 = account1.accountName;
            string expectedAccountName2 = account2.accountName;
            string expectedAccountName3 = account3.accountName;
            string expectedAccountName4 = account4.accountName;

            Account[] expectedArray = new Account[4];
            expectedArray[0] = account1;
            expectedArray[1] = account2;
            expectedArray[2] = account3;
            expectedArray[3] = account4;

            // act
            Account[] actualArray = bank.GetAccountsForCustomer(customer);

            //assert
            Assert.IsTrue(actualArray.Length == 4);

            Assert.AreEqual(expectedName, actualArray[0].owner.name);
            Assert.IsTrue(expectedArray[0].currentAmount == actualArray[0].currentAmount);
            Assert.AreEqual(expectedArray[0].accountName, actualArray[0].accountName);

            Assert.AreEqual(expectedName, actualArray[1].owner.name);
            Assert.IsTrue(expectedArray[1].currentAmount == actualArray[1].currentAmount);
            Assert.AreEqual(expectedArray[1].accountName, actualArray[1].accountName);

            Assert.AreEqual(expectedName, actualArray[02].owner.name);
            Assert.IsTrue(expectedArray[2].currentAmount == actualArray[2].currentAmount);
            Assert.AreEqual(expectedArray[2].accountName, actualArray[2].accountName);

            Assert.AreEqual(expectedName, actualArray[3].owner.name);
            Assert.IsTrue(expectedArray[3].currentAmount == actualArray[3].currentAmount);
            Assert.AreEqual(expectedArray[3].accountName, actualArray[3].accountName);
        }

        [TestMethod]
        public void GetAccountsForCustomer_HasAccount_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("scrooge");
            int expectedAccounts = 1;

            // act
            Account account = bank.CreateAccount(customer, new Money(1000));
            Account[] myAccounts = bank.GetAccountsForCustomer(customer);

            // assert
            Assert.IsTrue(expectedAccounts == myAccounts.Length);
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
            int expectedValue = 0;

            // act
            Account[] accounts = bank.GetAccountsForCustomer(dolly);

            //assert
            Assert.IsTrue(expectedValue == accounts.Length);
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
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            Account account = bank.CreateAccount(customer, new Money(1000));

            // act
            try {
                bank.Withdraw(account, new Money(1001));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.AmountIsMoreThanBalance);
            }
        }

        [TestMethod]
        public void Withdraw_WhenAmountIsLessThanZero_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            Account account = bank.CreateAccount(customer, new Money(1000));

            // act
            try {
                bank.Withdraw(account, new Money(49));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.NotValidAmount);
            }
        }

        [TestMethod]
        public void Transfer_WhenAmountIsMoreThanBalance_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            Account from = bank.CreateAccount(customer, new Money(1000));
            Account to = bank.CreateAccount(customer, new Money(2000));

            // act
            try {
                bank.Transfer(from, to, new Money(1500));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.AmountIsMoreThanBalance);
            }
        }

        [TestMethod]
        public void Transfer_WhenAmountIsLessThanZero_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            Account from = bank.CreateAccount(customer, new Money(1000));
            Account to = bank.CreateAccount(customer, new Money(2000));

            // act
            try {
                bank.Transfer(from, to, new Money(-1));
            } catch (ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, Bank.NotValidAmount);
            }
        }

        [TestMethod]
        public void Transfer_NotValidAccount_Test() {
            // arrange
            Bank bank = new Bank();
            Person customer = new Person("dolly");
            Account from = null;
            Account to = bank.CreateAccount(customer, new Money(2000));

            // act
            try {
                bank.Transfer(from, to, new Money(100));
            } catch (ArgumentNullException e) {
                StringAssert.Contains(e.Message, Bank.NotAValidAccount);
            }
        }
    }
}
