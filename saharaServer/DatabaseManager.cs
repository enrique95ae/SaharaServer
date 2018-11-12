using System;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using SaharaLib;


namespace SaharaServer
{
    public class DatabaseManager : BaseSingleton<DatabaseManager>
    {
        private const string _dbSource = "Data Source = /Users/enriquealonsoesposito/Desktop/SaharaDB.db";
        private readonly Random rng = new Random();

        public bool CreateAccount(string UserEmail, string UserPassword)
        {
            int numRowsChanged = 0;
             

            string sqlInsert = $"insert into UserData (UserEmail, UserPassword) values('{UserEmail}', '{UserPassword}')";

            using (var connection = new SQLiteConnection(_dbSource))
            {
                connection.Open();

                try
                {
                    numRowsChanged = connection.Execute(sqlInsert);
                }
                catch (SQLiteException e)
                {
                    Console.WriteLine("\n Database: ERROR occured while inserting into DB\n");
                    Console.WriteLine(e);
                    return false;
                }
            }
            return false;
        }

        public bool VerifyLoginInfo(string email, string password)
        {
            string sqlSelect = $"select UserPassword from UserData where UserEmail='{email}'";

            using (var connection = new SQLiteConnection(_dbSource))
            {
                connection.Open();

                try
                {
                    var selectedPassword = connection.Query<string>(sqlSelect).First();

                    Console.WriteLine($"Trying to find {email}'s password: {password}");
                    Console.WriteLine($"Found password: {selectedPassword}");

                    if (selectedPassword.Equals(password))
                    {
                        Console.WriteLine("Passwords were equal");
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n DB MANAGER: Error occured when selecting user info from DB \n");
                    Console.WriteLine(e);

                    return false;
                }
            }

            return false;
        }

        /*
        public bool VerifyLogout(string email, string password)
        {

        }
        */

        public UserData GetUserData(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            string sqlQuery = $"select * from UserData where UserEmail='{email}";

            using (var connection = new SQLiteConnection(_dbSource))
            {
                try
                {
                    var accountData = connection.QuerySingle<UserData>(sqlQuery);

                    if (accountData != null)
                    {
                        return accountData;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Database: ERROR selecting user");
                    Console.WriteLine(e);
                }
                return null;
            }
        }

        public ItemData GetItemData(string ItemTitle)
        {
            if (String.IsNullOrWhiteSpace(ItemTitle))
            {
                return null;
            }

            string sqlQuery = $"select * from ItemData";

            using (var connection = new SQLiteConnection("Data Source=SaharaDB.db"))
            {
                try
                {
                    var ItemData = connection.QuerySingle<ItemData>(sqlQuery);

                    if (ItemData != null)
                    {
                        return ItemData;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Database: Error selecting item");
                    Console.WriteLine(e);
                }
                return null;
            }
        }

        public BillingInfo UpdateBillingInfo(string UserEmail)
        {
            if (String.IsNullOrWhiteSpace(UserEmail))
            {
                return null;
            }

            string sqlQuery = $"select * from BillingInfo";

            using (var connection = new SQLiteConnection("Data Source=SaharaDB.db"))
            {
                try
                {
                    var BillingInfo = connection.QuerySingle<BillingInfo>(sqlQuery);

                    if (BillingInfo != null)
                    {
                        return BillingInfo;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Database: Error selecting billing information");
                    Console.WriteLine(e);
                }
                return null;
            }
        }

        public PaymentInfo UpdatePaymentInfo(string UserCreditCardNumber)
        {
            if (String.IsNullOrWhiteSpace(UserCreditCardNumber))
            {
                return null;
            }

            string sqlQuery = $"select * from PaymentInfo";

            using (var connection = new SQLiteConnection("Data Source=SaharaDB.db"))
            {
                try
                {
                    var PaymentInfo = connection.QuerySingle<PaymentInfo>(sqlQuery);

                    if (PaymentInfo != null)
                    {
                        return PaymentInfo;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Database: Error selecting payment information");
                    Console.WriteLine(e);
                }
                return null;
            }
        }
    }
}

