using CLases.Vehiculo;
using Nodos.NodoVehiculo;

namespace Listas.ListaDobleVehiculos
{
    public class ListaDobleVehiculo
    {
        private NodoVehiculo? primero;
        private NodoVehiculo? ultimo;

        public ListaDobleVehiculo()
        {
            primero = null;
            ultimo = null;
        }

        public void Insertar(Vehiculo data)
        {
            NodoVehiculo nuevo = new NodoVehiculo(data);
            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                if (ultimo != null)
                {
                    ultimo.Siguiente = nuevo;
                    nuevo.Anterior = ultimo;
                    ultimo = nuevo;
                }
            }
        }
    }
}