using System.Runtime.InteropServices;

namespace Pila
{
    public unsafe class Pila
    {
        private Facturas.Facturas* cima;

        // Metodo Constructor
        public Pila()
        {
            cima = null;
        }


        //Metodo para apilar un nuevo nodo
        public void Apila(int id, int id_orden, double total)
        {
            Facturas.Facturas* nuevo = (Facturas.Facturas*)Marshal.AllocHGlobal(sizeof(Facturas.Facturas));
            nuevo->ID = id;
            nuevo->ID_Orden = id_orden;
            nuevo->Total = total;
            nuevo->Siguiente = cima;
            cima = nuevo;
        }

        //Metodo para desapilar un nodo
        public void Desapilar()
        {
            if (cima != null)
            {
                Facturas.Facturas* aux = cima;
                cima = cima->Siguiente; // La nueva cima es el siguiente nodo en la lista

                // liberamos la memoria del nodo que se desapila
                Marshal.FreeHGlobal((IntPtr)aux);
            }
        }

        //Metodo para mostrar la pila
        public void Mostrar()
        {
            Facturas.Facturas* aux = cima;
            while (aux != null)
            {
                Console.WriteLine("ID: {0}, ID_Orden: {1}, Total: {2}", aux->ID, aux->ID_Orden, aux->Total);
                aux = aux->Siguiente;
            }
        }

        //Metodo para liberar la memoria de la pila
        public void Liberar()
        {
            Facturas.Facturas* presente = cima;
            while (presente != null)
            {
                Facturas.Facturas* temp = presente;
                presente = presente->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                Desapilar();
            }
            cima = null;
        }

        // Metodo para generar el código Graphviz de la pila
        public string GenerarGrafica()
        {
            // si la pila está vacia, se muestra solo un nodo "null"
            if (cima == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            // si la pila no está vacia, se genera el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=TB;\n";  // Definir el flujo de arriba hacia abajo para la pila
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Pila\";\n";
            return graphviz;
        }
    }
}