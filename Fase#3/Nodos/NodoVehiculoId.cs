using Nodos.IdRepuestoId;


namespace Nodos.IdVehiculoId
{
    public class NodoVehiculosID
    {
        public int ID_vehiculo { get; set; }
        public NodoVehiculosID? Siguiente { get; set; }
        public NodoVehiculosID? Anterior { get; set; }
        public NodoRepuestosID? ListaRepuestos { get; set; } // Sublista de repuestos

        public NodoVehiculosID(int id)
        {
            ID_vehiculo = id;
            Siguiente = null;
            Anterior = null;
            ListaRepuestos = null;
        }

        public void AgregarRepuesto(int idRepuesto)
        {
            NodoRepuestosID nuevoRepuesto = new NodoRepuestosID(idRepuesto);
            if (ListaRepuestos == null)
            {
                ListaRepuestos = nuevoRepuesto;
            }
            else
            {
                NodoRepuestosID actual = ListaRepuestos;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevoRepuesto;
            }
        }

        public void ImprimirRepuestos()
        {
            NodoRepuestosID? actual = ListaRepuestos;
            while (actual != null)
            {
                Console.WriteLine($"  Repuesto ID: {actual.ID_repuesto}");
                actual = actual.Siguiente;
            }
        }
    }
}