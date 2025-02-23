using System.Runtime.InteropServices;
using List;
using Servicios;

namespace Cola
{
    public unsafe class Cola
    {
        private Servicio* frente;

        private Servicio* final;

        public Servicio* Frente => frente;
        public Servicio* Final => final;

        // Metodo Constructor
        public Cola()
        {
            frente = null;
            final = null;
        }


        public void Encolar(int id, int idRepuesto, int idVehiculo, string detalles, float costo)
        {
            Servicio* nuevo = (Servicio*)Marshal.AllocHGlobal(sizeof(Servicio));
            nuevo->Id = id;
            nuevo->ID_Repuesto = idRepuesto;
            nuevo->ID_Vehiculo = idVehiculo;

            // limpiar el buffer
            for (int i = 0; i < 50; i++)
            {
                nuevo->Detalles[i] = '\0';
            }
            nuevo->Costo = costo;
            nuevo->Next = null;

            // Copiar los detalles al buffer fijo
            for (int i = 0; i < detalles.Length && i < 50; i++)
            {
                nuevo->Detalles[i] = detalles[i];
            }

            // Si la cola esta vacia, el nuevo nodo es tanto el frente como el final
            if (frente == null)
            {
                frente = nuevo;
                final = nuevo;
            }
            else
            {
                // Si no está vacía, agregamos el nodo al final
                final->Next = nuevo;
                final = nuevo; //se acutaliza el final de la cola
            }
        }

        public Servicio* Desencolar()
        {
            if (frente == null)
            {
                Console.WriteLine("La cola está vacía");
                return null;
            }

            Servicio* temp = frente;
            frente = frente->Next; //El nuevo frente es el siguiente del frente actual

            // Si el frente es null, la cola está vacía
            
            if (frente == null)
            {
                final = null;
            }

            // liberar memoria del nodo desencolado
            Marshal.FreeHGlobal((IntPtr)temp);

            return temp;
        }

        // Método para imprimir la cola

        public void Mostrar()
        {
            Servicio* presente = frente;
            while (presente != null)
            {
                String Detalles = ObtenerDetalles(presente);
                Console.WriteLine("ID: {0}, ID Repuesto: {1}, ID Vehículo: {2}, Detalles: {3}, Costo: {4}", presente->Id, presente->ID_Repuesto, presente->ID_Vehiculo, Detalles, presente->Costo);
                presente = presente->Next;
            }
        }

        public string ObtenerDetalles(Servicio* servicio)
        {
            char[] detalles = new char[50];
            for (int i = 0; i < 50 && servicio->Detalles[i] != '\0'; i++)
            {
                detalles[i] = servicio->Detalles[i];
            }
            return new string(detalles).TrimEnd('\0');
        }

        public string GenerarGraphviz()
        {
            if (frente == null) return string.Empty;

            string graph = "digraph G {\n";
            graph += "rankdir=LR;\n"; // Dirección de izquierda a derecha

            Servicio* presente = frente;
            while (presente != null)
            {
                graph += $"\"ID: {presente->Id}, Repuesto: {presente->ID_Repuesto}, Vehículo: {presente->ID_Vehiculo}, Costo: {presente->Costo}\"";
                if (presente->Next != null)
                {
                    graph += $" -> \"ID: {presente->Next->Id}, Repuesto: {presente->Next->ID_Repuesto}, Vehículo: {presente->Next->ID_Vehiculo}, Costo: {presente->Next->Costo}\"";
                }
                graph += ";\n";
                presente = presente->Next;
            }

            graph += "}\n";
            return graph;
        }

        public void Liberar()
        {
            Servicio* presente = frente;
            while (frente != null)
            {
                Servicio* temp = frente;
                frente = frente->Next;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
            frente = null;
            final = null;
        }
    }
}