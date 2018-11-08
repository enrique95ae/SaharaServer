using System;
using System.Data.SQLite;
using Dapper;
using System.Linq;

namespace SaharaServer
{
    public class DatabaseManager : BaseSingleton<DatabaseManager>
    {
        private const string            _dbSource = "XXXXXXXXXXXXXXXXXX";
        private readonly Random rng               = new Random();

        public bool CreateAccount(string email, string password)
        {
            int numRowsChanged = 0;

            string tag = GeneratedUserTag();

            string sqlInsert = $"insert into AccountData (Tag, Email, Password) values('{tag}', '{email}', '{password}')";

            using (var connection = new SQLiteConnection("XXXXXXXXXXXXX"))
            {
                connection.Open();

                try
                {
                    numRowsChanged = connection.Execute(sqlInsert);
                }
                catch(SQLiteException e)
                {
                    Console.WriteLine("\n Database: ERROR occured while inserting into DB\n");
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

                    if(accountData != null)
                    {
                        return accountData;
                    }
                }
                catch(Exception e)
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

