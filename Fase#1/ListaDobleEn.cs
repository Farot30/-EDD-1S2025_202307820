using System;
using System.Runtime.InteropServices;
using Vehiculos;

namespace ListaDobleUnsafe
{
    public unsafe class ListaDoblementeEnlazada 
    {
        // apuntador head y tail
        private Node* head;
        // apuntador tail que es el último nodo de la lista
        private Node* tail;

        public Node* Head => head;

        public ListaDoblementeEnlazada()
        {
            head = null;
            tail = null;
        }

        public void Insertar(int id, int idUsuario, string marca, int modelo, string placa)
        {
            Node* nuevoNodo = (Node*)NativeMemory.Alloc((nuint)sizeof(Node));
            *nuevoNodo = new Node(id, idUsuario, marca, modelo, placa);
            // Si la lista está vacía, el nuevo nodo será el primero
            if (head == null)
            {
                head = tail = nuevoNodo;
            }
            else
            // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
            {
                tail->Next = nuevoNodo;
                nuevoNodo->Prev = tail;
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            // Si la lista está vacía, no hay nada que eliminar
            Node* actual = head;
            while (actual != null)
            {
                // Si el nodo a eliminar es la cabeza de la lista, movemos la cabeza al siguiente nodo
                if (actual->Id == id)
                {
                    if (actual->Prev != null)
                        actual->Prev->Next = actual->Next;
                    else
                        head = actual->Next;

                    if (actual->Next != null)
                        actual->Next->Prev = actual->Prev;
                    else
                        tail = actual->Prev;
                    // Liberamos la memoria del nodo eliminado
                    NativeMemory.Free(actual);
                    return;
                }
                // Si el nodo a eliminar no es la cabeza de la lista, avanzamos al siguiente nodo
                actual = actual->Next;
            }
        }

        // Método para mostrar los nodos de la lista inicializados del head al tail
        public void Mostrar()
        {
            // Si la lista está vacía, no hay nada que mostrar
            Node* actual = head;
            // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Next;
            }
        }

        // Método para mostrar los nodos de la lista inicializados del tail al head
        public void MostrarReversa()
        {
            // Si la lista está vacía, no hay nada que mostrar
            Node* actual = tail;
            // Si la lista no está vacía, recorremos la lista hasta llegar al último nodo
            while (actual != null)
            {
                Console.WriteLine(actual->ToString());
                actual = actual->Prev;
            }
        }

        public string GenerarGraphviz()
        {
            if (head == null) return string.Empty;

            string graph = "digraph G {\n";
            graph += "rankdir=LR;\n"; // Dirección de izquierda a derecha
            graph += "node [shape=record];\n"; // Forma de los nodos
            graph += "subgraph cluster_ListaDobleEnlazada {\n";
            graph += "label = \"Lista Doblemente Enlazada\";\n";

            Node* actual = head;
            while (actual != null)
            {
                string label = $"ID: {actual->Id}\\nID Usuario: {actual->IdUsuario}\\nMarca: {new string(actual->Marca)}\\nModelo: {actual->Modelo}\\nPlaca: {new string(actual->Placa)}";
                graph += $"\"{actual->Id}\" [label=\"{label}\"];\n";
                if (actual->Next != null)
                {
                    graph += $"\"{actual->Id}\" -> \"{actual->Next->Id}\";\n";
                    graph += $"\"{actual->Next->Id}\" -> \"{actual->Id}\";\n";
                }
                actual = actual->Next;
            }

            graph += "}\n";
            graph += "}\n";
            return graph;
        }

        // Método para generar una gráfica en Graphviz de los 5 vehículos más antiguos
        public string GenerarGraphvizTop5Antiguos()
        {
            if (head == null) return string.Empty;

            // Crear una lista temporal para almacenar los nodos
            Node*[] nodos = new Node*[100]; // Suponiendo un máximo de 100 nodos
            int count = 0;
            Node* actual = head;
            while (actual != null && count < 100)
            {
                nodos[count++] = actual;
                actual = actual->Next;
            }

            // Ordenar los nodos por el año del modelo en orden ascendente usando una función de ordenación manual
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (nodos[j]->Modelo > nodos[j + 1]->Modelo)
                    {
                        Node* temp = nodos[j];
                        nodos[j] = nodos[j + 1];
                        nodos[j + 1] = temp;
                    }
                }
            }

            // Tomar los 5 vehículos más antiguos
            int topCount = Math.Min(5, count);
            Node*[] top5Antiguos = new Node*[topCount];
            Array.Copy(nodos, top5Antiguos, topCount);

            string graph = "rankdir=LR;\n"; // Dirección de izquierda a derecha
            graph += "node [shape=record];\n"; // Forma de los nodos

            for (int i = 0; i < topCount; i++)
            {
                Node* nodo = top5Antiguos[i];
                string label = $"ID: {nodo->Id}\\nID Usuario: {nodo->IdUsuario}\\nMarca: {new string(nodo->Marca)}\\nModelo: {nodo->Modelo}\\nPlaca: {new string(nodo->Placa)}";
                graph += $"\"{nodo->Id}\" [label=\"{label}\"];\n";
                if (i < topCount - 1)
                {
                    graph += $"\"{nodo->Id}\" -> \"{top5Antiguos[i + 1]->Id}\";\n";
                }
            }

            return graph;
        }

        // Destructor de la clase
        ~ListaDoblementeEnlazada()
        {
            // Liberamos la memoria de todos los nodos de la lista
            Node* actual = head;
            while (actual != null)
            {
                Node* temp = actual;
                actual = actual->Next;
                NativeMemory.Free(temp);
            }
        }
    }
}