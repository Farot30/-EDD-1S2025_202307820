using Clases.Vehiculo;

namespace Nodos.NodoVehiculo
{
    public class NodoVehiculo
    {
        public Vehiculos Data { get; set; }
        public NodoVehiculo? Siguiente { get; set; }
        public NodoVehiculo? Anterior { get; set; }

        public NodoVehiculo(Vehiculos data)
        {
            Data = data;
            Siguiente = null;
            Anterior = null;
        }
    }
}