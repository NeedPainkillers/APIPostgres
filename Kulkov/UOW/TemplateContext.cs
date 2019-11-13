using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Npgsql;
using Kulkov.Data;
using Microsoft.Extensions.Options;

namespace Kulkov.UOW
{
    public class TemplateContext : IDisposable
    {
        //string _connString = "Host=host.docker.internal;Username=postgres;Password=sdfl234;Database=taskdb";
        private readonly NpgsqlConnection _conn = null;

        public TemplateContext(IOptions<Settings> settings)
        {
            _conn = new NpgsqlConnection(settings.Value.ConnectionString);
        }

        public Task<IEnumerable<Account>> Accounts
        {
            get
            {
                return getAccountsInternal();
            }
        }

        private async Task<IEnumerable<Account>> getAccountsInternal()
        {
            await _conn.OpenAsync();

            List<Account> Response = new List<Account>();
            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT t.*, CTID FROM public.account t ORDER BY user_id DESC", _conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                {
                    Response.Add(new Account()
                    {
                        user_id = Int32.Parse(reader.GetValue(0).ToString()),
                        username = reader.GetValue(1).ToString(),
                        password = reader.GetValue(2).ToString(),
                        email = reader.GetValue(3).ToString(),
                        created_on = reader.GetValue(4).ToString(),
                        last_login = reader.GetValue(5).ToString()
                    });
                }
            return Response;
        }

        //private async Task<IEnumerable<string>> TestConnection()
        //{
        //    await using var conn = new NpgsqlConnection(_connString);
        //    await conn.OpenAsync();

        //    // Insert some data
        //    await using (var cmd = new NpgsqlCommand("INSERT INTO taskdb.public.account ( username, password, email, created_on, last_login) VALUES ((@p), 'somePwd', 'Test@email.example', now(), now());", conn))
        //    {
        //        cmd.Parameters.AddWithValue("p", "Hello world");
        //        await cmd.ExecuteNonQueryAsync();
        //    }

        //    List<string> Response = new List<string>();
        //    // Retrieve all rows
        //    await using (var cmd = new NpgsqlCommand("SELECT username FROM account", conn))
        //    await using (var reader = await cmd.ExecuteReaderAsync())
        //        while (await reader.ReadAsync())
        //            Response.Add(reader.GetString(0));
        //    return Response;

        //}

        public void Dispose()
        {
            _conn.CloseAsync();
            _conn.Dispose();
        }
    }
}
