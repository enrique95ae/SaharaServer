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

        public bool CreateAccount(string UserName, string UserEmail, string UserPassword, string UserRepeatPassword)
        {
            int numRowsChanged = 0;
             

            string sqlInsert = $"insert into UserData (UserName, UserEmail, UserPassword) values('{UserName}', '{UserEmail}', '{UserPassword}')";

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
    
    }
}

