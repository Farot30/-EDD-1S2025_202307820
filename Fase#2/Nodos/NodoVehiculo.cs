using CLases.Vehiculo;

namespace Nodos.NodoVehiculo
{
    public class NodoVehiculo
    {
        public Vehiculo Data { get; set; }
        public NodoVehiculo? Siguiente { get; set; }
        public NodoVehiculo? Anterior { get; set; }

        public NodoVehiculo(Vehiculo data)
        {
            Data = data;
            Siguiente = null;
            Anterior = null;
        }
    }
}