using System;

namespace Nodos.NodoBinarios
{
    public class NodoBinario
    {
        public  int valor;
        public NodoBinario? izquierdo;
        public NodoBinario? derecho;
        public NodoBinario(int valors)
        {
            valor = valors;
            izquierdo = null;
            derecho = null;
        }
    }
}