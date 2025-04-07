
namespace Nodos.IdRepuestoId
{
    public class NodoRepuestosID
    {
        public int ID_repuesto { get; set; }
        public NodoRepuestosID? Siguiente { get; set; }

        public NodoRepuestosID(int id)
        {
            ID_repuesto = id;
            Siguiente = null;
        }
    }
}