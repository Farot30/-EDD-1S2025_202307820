using System;
using Clases.Servicios;

namespace Nodos.NodoBinarios
{
    public class NodoBinario
    {
        public  Servicio servicios;
        public NodoBinario? izquierdo;
        public NodoBinario? derecho;
        public NodoBinario(Servicio servicio)
        {
            this.servicios = servicio;
            izquierdo = null;
            derecho = null;
        }
    }
}