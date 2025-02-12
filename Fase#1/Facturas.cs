namespace Facturas
{
    public class Factura
    {
        public int ID { get; set; }
        public int ID_Orden { get; set; }  
        public double Total { get; set; }

        public Factura(int id, int id_orden, double total)
        {
            ID = id;
            ID_Orden = id_orden;
            Total = total;
        }

        public override string ToString()
        {
            return $"Id: {ID}, ID Orden: {ID_Orden}, Total: {Total}";
        }
    }
}