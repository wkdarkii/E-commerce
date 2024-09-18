using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;

namespace eticaret.Controllers
{
    public class DatabaseController : Controller
    {
        private readonly string _connectionString = "Server=127.0.0.1;Port=3306;Database=e_ticaret;Uid=root;Pwd=semih180;";

        public IActionResult Index()
        {
            bool isConnected = TestDatabaseConnection();
            ViewBag.ConnectionStatus = isConnected ? "Bağlantı başarılı." : "Bağlantı hatası.";
            return View();
        }

        private bool TestDatabaseConnection()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
