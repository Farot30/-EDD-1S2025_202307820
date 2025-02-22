namespace Servicios
{
    public unsafe struct Servicio
    {
        public int Id;
        public int ID_Repuesto;
        public int ID_Vehiculo;
        public fixed char Detalles[50];
        public float Costo;

        public Servicio* Next; 
    }
}