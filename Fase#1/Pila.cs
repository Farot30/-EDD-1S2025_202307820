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
        public unsafe void Apila(int id, int id_orden, double total)
        {
            Facturas.Facturas* nuevo = (Facturas.Facturas*)Marshal.AllocHGlobal(sizeof(Facturas.Facturas));
            nuevo->ID = id;
            nuevo->ID_Orden = id_orden;
            nuevo->Total = total;
            nuevo->Siguiente = cima;
            cima = nuevo;
        }

        //Metodo para desapilar un nodo
        public unsafe void Desapilar()
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
        public unsafe void Mostrar()
        {
            Facturas.Facturas* aux = cima;
            while (aux != null)
            {
                Console.WriteLine("ID: {0}, ID_Orden: {1}, Total: {2}", aux->ID, aux->ID_Orden, aux->Total);
                aux = aux->Siguiente;
            }
        }

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

        public unsafe string GenerarGrafica()
        {
            // si la pila está vacia, se muestra solo un nodo "null"
            if (cima == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            // si la pila no está vacia, se genera el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=TB;\n";  // Definir el flujo de abajo hacia arriba para la pila
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Pila\";\n";

            // Iterar sobre los nodos de la pila y constructores de los nodos
            Facturas.Facturas* aux = cima;
            int ind = 0;
            int o = 1;

            while (aux != null)
            {
                graphviz += $"    node{ind} [label = \"{{Factura {o}\\nID: {aux->ID}\\nID_Orden: {aux->ID_Orden}\\nTotal: {aux->Total:C}}}\"];\n";
                aux = aux->Siguiente;
                ind++;
                o++;
            }

            aux = cima;
            for (int i = 0; aux != null && aux->Siguiente != null; i++)
            {
                graphviz += $"    node{i + 1} -> node{i};\n"; // Ajustar la dirección de las flechas
                aux = aux->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            return graphviz;
        }

        public unsafe Facturas.Facturas* ObtenerUltimaFactura()
        {
            return cima;
        }
    }
}