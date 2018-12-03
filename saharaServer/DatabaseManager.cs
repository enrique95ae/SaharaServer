using System;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using SaharaLib;

/*
 * 
 *  This file defines how the information insertion or retreival to or from the database will be handled.
 *  For each possible interaction with the database there is an specific function.
 * 
 * 
 * 
 * 
 * 
 */


namespace SaharaServer
{
    public class DatabaseManager : BaseSingleton<DatabaseManager>
    {
        private const string _dbSource = "Data Source = /Users/enriquealonsoesposito/Desktop/SaharaDB.db";
        private readonly Random rng = new Random();




        /*
         * 
         *  Handles the server-database interaction when the server receives a createaccount request from the client
         * 
         */

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

            if(numRowsChanged == 1)
            {
                return true;
            }
            return false;
        }


        /*
         * 
         *  Handles the server-database interaction when the server receives a log in request from the client
         * 
         */

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

        /*  Commented out until implemented
        public bool VerifyLogout(string email, string password)
        {

        }
        */

        /*
         * 
         *  Handles the server-database interaction when the server receives a get user data request from the client
         * 
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

        /*
         * 
         *  Handles the server-database interaction when the server receives a get info from item request from the client
         * 
         */

        public GetItemDataEvent GetItemData(int ItemID)
        {
            if (ItemID < 0) //make sure we are looking for a valid ID?
            {
                return null;
            }

            string sqlQuery = $"select * from ItemData where id='{ItemID}'";

            using (var connection = new SQLiteConnection("Data Source=SaharaDB.db"))
            {
                try
                {
                    var ItemData = connection.QuerySingle<GetItemDataEvent>(sqlQuery);

                    if (ItemData != null)
                    {
                        ItemData.ItemTitle = $"select ItemTitle from ItemDara where id='{ItemID}'";
                        ItemData.ItemDescription = $"select ItemDescription from ItemDara where id='{ItemID}'";
                        ItemData.ItemPrice = Convert.ToDouble($"select ItemPrice from ItemDara where id='{ItemID}'");
                        ItemData.ItemImageSRC1 = $"select ImageSRC1 from ItemDara where id='{ItemID}'";
                        ItemData.ItemImageSRC2 = $"select ImageSRC2 from ItemDara where id='{ItemID}'";
                        ItemData.ItemImageSRC3 = $"select ImageSRC3 from ItemDara where id='{ItemID}'";
                        ItemData.ItemImageSRC4 = $"select ImageSRC4 from ItemDara where id='{ItemID}'";
                        ItemData.ItemImageSRC5 = $"select ImageSRC5 from ItemDara where id='{ItemID}'";
                        ItemData.ItemImageSRC6 = $"select ImageSRC6 from ItemDara where id='{ItemID}'";

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

        /*
        * 
        *  Handles the server-database interaction when the server receives a update billing info from item request from the client
        * 
        */


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


        /*
        * 
        *  Handles the server-database interaction when the server receives a update payment info from item request from the client
        * 
        */

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

