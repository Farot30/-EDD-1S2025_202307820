namespace Servicios
{
    public unsafe struct Servicio
    {
        public int Id { get; set; }
        public string ID_Repuesto { get; set; }
        public string ID_Vehiculo { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }

        public Servicio(int id, string id_repuesto, string id_vehiculo, string detalles, double costo)
        {
            Id = id;
            ID_Repuesto = id_repuesto;
            ID_Vehiculo = id_vehiculo;
            Detalles = detalles;
            Costo = costo;
        }

        public override string ToString()
        {
            return $"Id: {Id}, ID Repuesto: {ID_Repuesto}, ID Vehiculo: {ID_Vehiculo}, Detalles: {Detalles}, Costo: {Costo}";
        }   
    }
}