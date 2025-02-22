using System;
using System.Runtime.InteropServices;
using Repuestos;

namespace ListaCircular
{
    public unsafe class ListaCircular
    {
        private Repuesto* head;

        public Repuesto* Head => head;

        public ListaCircular()
        {
            head = null;
        }

        public void Insertar(int id, string repuesto, string detalles, double costo)
        {
            Repuesto* nuevoNodo = (Repuesto*)Marshal.AllocHGlobal(sizeof(Repuesto));
            *nuevoNodo = new Repuesto(id, repuesto, detalles, costo);

            if (head == null)
            {
                head = nuevoNodo;
                head->Next = head; // Apunta a sí mismo
            }
            else
            {
                Repuesto* temp = head;
                // Recorremos la lista hasta llegar al último nodo
                while (temp->Next != head)
                {
                    temp = temp->Next;
                }
                // Insertamos el nuevo nodo al final de la lista
                temp->Next = nuevoNodo;
                nuevoNodo->Next = head;
            }
        }

        public void Eliminar(int id)
        {
            // si la lista está vacía, no hay nada que eliminar
            if (head == null) return;
            // si el nodo a eliminar es la cabeza de la lista
            if (head->Id == id && head->Next == head)
            {
                Marshal.FreeHGlobal((IntPtr)head);
                head = null;
                return;
            }

            Repuesto* temp = head;
            Repuesto* prev = null;
            do
            {
                // si el nodo a eliminar es la cabeza de la lista
                if (temp->Id == id)
                {
                    if (prev != null)
                    {
                        prev->Next = temp->Next;
                    }
                    else
                    {
                        Repuesto* last = head;
                        while (last->Next != head)
                        {
                            last = last->Next;
                        }
                        head = head->Next;
                        last->Next = head;
                    }
                    // liberamos la memoria del nodo eliminado
                    Marshal.FreeHGlobal((IntPtr)temp);
                    return;
                }
                // avanzamos al siguiente nodo
                prev = temp;
                temp = temp->Next;
            } while (temp != head);
        }

        public void Mostrar()
        {
            if (head == null)
            {
                Console.WriteLine("Lista vacía.");
                return;
            }

            Repuesto* temp = head;
            do
            {
                Console.WriteLine(temp->ToString());
                temp = temp->Next;
            } while (temp != head);
        }

        // destructor de la clase
        ~ListaCircular()
        {
            if (head == null) return;

            Repuesto* temp = head;
            do
            {
                Repuesto* next = temp->Next;
                Marshal.FreeHGlobal((IntPtr)temp);
                temp = next;
            } while (temp != head);
        }

        public string GenerarGraphviz()
        {
            if (head == null) return string.Empty;

            string graph = "node [shape=box];\n"; // Cambia la forma de los nodos a cuadrados

            Repuesto* temp = head;
            do
            {
                graph += $"\"ID: {temp->Id}, Repuesto: {new string(temp->Repuestos)}, Costo: {temp->Precio}\" -> \"ID: {temp->Next->Id}, Repuesto: {new string(temp->Next->Repuestos)}, Costo: {temp->Next->Precio}\";\n";
                temp = temp->Next;
            } while (temp != head);

            return graph;
        }
    }
}