using System;
using System.Data.SQLite;
using Dapper;
using System.Linq;
using SaharaLib;


namespace SaharaServer
{
    public class DatabaseManager : BaseSingleton<DatabaseManager>
    {
        private const string _dbSource = "XXXXXXXXXXXXXXXXXX";
        private readonly Random rng = new Random();

        public bool CreateAccount(string email, string password)
        {
            int numRowsChanged = 0;

            string tag = GenerateUserTag();

            string sqlInsert = $"insert into AccountData (Tag, Email, Password) values('{tag}', '{email}', '{password}')";

            using (var connection = new SQLiteConnection("XXXXXXXXXXXXX"))
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
            string sqlSelect = $"select Password from AccountData where Email='{email}'";

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


        public AccountData GetAccountData(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            string sqlQuery = $"select * from AccountData where Email='{email}";

            using (var connection = new SQLiteConnection("XXXXXXXXXXXXXXXX"))
            {
                try
                {
                    var accountData = connection.QuerySingle<AccountData>(sqlQuery);

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

        private string GenerateUserTag()
        {
            string tag = rng.Next(0, 9999).ToString("D4");

            return tag;
        }
    }
}

