using Clases.Factura;

namespace Nodos.NodoFacturas
{
    public class NodoFactura
    {
        private const int Orden = 5;
        private const int Max_claves = Orden -1;
        private const int Min_Claves = (Orden / 2) - 1;

        public List<Facturas> Claves { get; set; }
        public List<NodoFactura> Hijos { get; set; }
        public bool EsHoja { get; set; }

        public NodoFactura()
        {
            Claves = new List<Facturas>(Max_claves);
            Hijos = new List<NodoFactura>(Orden);
            EsHoja = true;
        }

        public bool EstaLleno()
        {
            return Claves.Count >= Max_claves;
        }

        public bool EstaVacio()
        {
            return Claves.Count >= Min_Claves;
        }


    }
}