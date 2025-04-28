using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Text;

namespace Clases.Facturas
{
    public struct Factura
    {
        public int ID { get; set; }
        public int ID_servicio { get; set; }
        public double Total { get; set; }
        public string Fecha { get; set; }
        public string Metodo_pago { get; set; }

        public Factura(int Id, int id_servicio, double total, string metodo_pago)
        {
            ID = Id;
            ID_servicio = id_servicio;
            Total = total;
            Fecha = DateTime.Now.ToString("yyyy-MM-dd");
            Metodo_pago = metodo_pago;
        }

        public override string ToString()
        {
            return $"ID: {ID}, ID Servicio: {ID_servicio}, Total: {Total}, Fecha: {Fecha}, Metodo de Pago: {Metodo_pago}";
        }

        public string GetHash()
        {
            string data = JsonConvert.SerializeObject(this); // Serializar la factura a JSON
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convertir a hexadecimal
                }
                return builder.ToString();
            }
        }
    }
}