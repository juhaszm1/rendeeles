using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using rendelesnyilvantarto.Models;

namespace rendelesnyilvantarto.Controllers
{
    [ApiController]
    [Route("rendeles")]
    public class RendelesController : ControllerBase
    {
        public string ConnectionString = "server=localhost;database=etterem;uid=root;password=your_password";

        [HttpGet("all")]
        public object GetAllRendeles()
        {
            List<Rendeles> lista = new List<Rendeles>();
            var conn = new MySqlConnection(ConnectionString);

            conn.Open();

            string sql = "SELECT * FROM `rendeles`";

            var cmd = new MySqlCommand(sql, conn);

            var dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                var r = new Rendeles
                {
                    Id = dr.GetInt32(0),
                    Etel = dr.IsDBNull(1) ? null : dr.GetString(1),
                    Leiras = dr.IsDBNull(2) ? null : dr.GetString(2),
                    RendelesIdopont = dr.GetDateTime(3),
                    FrissitesIdopont = dr.GetDateTime(4),
                    VendegId = dr.GetInt32(5)
                };
                lista.Add(r);
            }

            conn.Close();

            return new { message = "Sikeres lekerdezes", result = lista };
        }

        [HttpGet("byid")]
        public object GetById(int id)
        {
            var conn = new MySqlConnection(ConnectionString);

            conn.Open();

            string sql = "SELECT * FROM `rendeles` WHERE `Id` = @id;";
            var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            var dr = cmd.ExecuteReader();

            dr.Read();

            var r = new Rendeles
            {
                Id = dr.GetInt32(0),
                Etel = dr.IsDBNull(1) ? null : dr.GetString(1),
                Leiras = dr.IsDBNull(2) ? null : dr.GetString(2),
                RendelesIdopont = dr.GetDateTime(3),
                FrissitesIdopont = dr.GetDateTime(4),
                VendegId = dr.GetInt32(5)
            };

            conn.Close();
            return new { message = "Sikeres lekerdezes", result = r };
        }

        [HttpPost("add")]
        public object AddNew([FromBody] Rendeles uj)
        {
            var conn = new MySqlConnection(ConnectionString);

            conn.Open();

            string sql = "INSERT INTO rendeles (Dish, Description, OrderTime, UpdateTime, VendegId) VALUES (@d, @desc, @ot, @ut, @vid)";
            var cmd = new MySqlCommand(sql, conn);
            var now = DateTime.UtcNow;
            cmd.Parameters.AddWithValue("@d", uj.Etel ?? string.Empty);
            cmd.Parameters.AddWithValue("@desc", (object?)uj.Leiras ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ot", now);
            cmd.Parameters.AddWithValue("@ut", now);
            cmd.Parameters.AddWithValue("@vid", uj.VendegId);

            cmd.ExecuteNonQuery();

            conn.Close();
            return new { message = "sikeres hozzaadas" };
        }
    }
}
