using Clases.Repuesto;

namespace Nodos.NodoAVLRepus
{
    public class NodeAVLRepu
    {
        public Repuesto val;
        public NodeAVLRepu? izq;
        public NodeAVLRepu? der;
        public int altura;

        public NodeAVLRepu(Repuesto val_)
        {
            val = val_;
            izq = null;
            der = null;
            altura = 0;
        }
    }
}