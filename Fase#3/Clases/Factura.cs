namespace Clases.Factura
{
    public struct Facturas
    {
        public int ID { get; set; }
        public int ID_servicio { get; set; }
        public double Total { get; set; }

        public Facturas(int id, int id_servicio, double total)
        {
            ID = id;
            ID_servicio = id_servicio;
            Total = total;
        }

        public override string ToString()
        {
            return $"Id: {ID}, Id Servicio: {ID_servicio}, Total: {Total}";
        }
    }
}