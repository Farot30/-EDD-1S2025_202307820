namespace Clases.Servicios
{
    public struct Servicio
    {
        public int ID { get; set; }
        public int ID_repuesto { get; set; }
        public int ID_vehiculo { get; set; }
        public string Detalle { get; set; }
        public double Costo { get; set; }
        public string Metodo_pago { get; set; }


        public Servicio(int id, int id_repuesto, int id_vehiculo, string detalle, double costo, string metodo_pago)
        {
            ID = id;
            ID_repuesto = id_repuesto;
            ID_vehiculo = id_vehiculo;
            Detalle = detalle;
            Costo = costo;
            Metodo_pago = metodo_pago;
        }

        public override string ToString()
        {
            return $"ID: {ID}, ID Repuesto: {ID_repuesto}, ID Vehiculo: {ID_vehiculo}, Detalle: {Detalle}, Costo: {Costo}, Metodo de Pago: {Metodo_pago}";
        }
    }
}