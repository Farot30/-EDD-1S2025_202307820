using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using List;
using Servicios;
using Matriz;

namespace Cola
{
    public unsafe class Cola
    {
        private Servicio* frente;

        private Servicio* final;
        private ListaEncabezado<int> listaVehiculos;
        private ListaEncabezado<int> listaRepuestosEncabezado;

        public Servicio* Frente => frente;
        public Servicio* Final => final;

        // Metodo Constructor
        public Cola()
        {
            frente = null;
            final = null;
            listaVehiculos = new ListaEncabezado<int>("Vehículos");
            listaRepuestosEncabezado = new ListaEncabezado<int>("Repuestos");
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
            graph += "node [shape=record];\n"; // Forma de los nodos
            int o = 0;

            Servicio* presente = frente;
            while (presente != null)
            {
                // Convertir los detalles a string y agregar saltos de línea
                string detalles = ObtenerDetalles(presente).Replace(", ", "\\n");
                string label = $"Servicio {o}\\nID: {presente->Id}\\nID Repuesto: {presente->ID_Repuesto}\\nID Vehículo: {presente->ID_Vehiculo}\\nDetalles: {detalles}\\nCosto: {presente->Costo}";
                graph += $"\"{presente->Id}\" [label=\"{label}\"];\n";
                o++;
                if (presente->Next != null)
                {
                    graph += $"\"{presente->Id}\" -> \"{presente->Next->Id}\";\n";
                }
                presente = presente->Next;
            }

            graph += "}\n";
            return graph;
        }

        // Método para generar una gráfica en Graphviz de los 5 vehículos con más servicios
        public string GenerarGraphvizTop5VehiculosConMasServicios()
        {
            if (frente == null) return string.Empty;

            // Crear un diccionario para contar los servicios por vehículo
            Dictionary<int, int> vehiculoServicios = new Dictionary<int, int>();
            Servicio* presente = frente;
            while (presente != null)
            {
                if (vehiculoServicios.ContainsKey(presente->ID_Vehiculo))
                {
                    vehiculoServicios[presente->ID_Vehiculo]++;
                }
                else
                {
                    vehiculoServicios[presente->ID_Vehiculo] = 1;
                }
                presente = presente->Next;
            }

            // Ordenar los vehículos por la cantidad de servicios en orden descendente y tomar los 5 primeros
            var top5Vehiculos = vehiculoServicios.OrderByDescending(v => v.Value).Take(5).ToList();

            string graph = "digraph G {\n";
            graph += "rankdir=TB;\n"; // Dirección de arriba hacia abajo
            graph += "node [shape=record];\n"; // Forma de los nodos

            foreach (var vehiculo in top5Vehiculos)
            {
                graph += $"\"Vehiculo_{vehiculo.Key}\" [label=\"ID Vehículo: {vehiculo.Key}\\nServicios: {vehiculo.Value}\"];\n";
            }

            graph += "}\n";
            return graph;
        }

        // Método para generar una gráfica en Graphviz de la matriz dispersa
        public unsafe string GenerarGraphvizMatrizDispersa()
        {
            string graph = "digraph G {\n";
            graph += "node [shape=record];\n";
            graph += "rankdir=LR;\n";

            // Crear nodos para los encabezados de vehículos y repuestos
            graph += "node0 [label=\"\"];\n"; // Nodo vacío en la esquina superior izquierda

            NodoEncabezado<int>* vehiculoActual = listaVehiculos.primero;
            int vehiculoIndex = 1;
            while (vehiculoActual != null)
            {
                graph += $"nodeV{vehiculoIndex} [label=\"V{vehiculoIndex}\"];\n";
                graph += $"node0 -> nodeV{vehiculoIndex};\n";
                vehiculoActual = vehiculoActual->siguiente;
                vehiculoIndex++;
            }

            NodoEncabezado<int>* repuestoActual = listaRepuestosEncabezado.primero;
            int repuestoIndex = 1;
            while (repuestoActual != null)
            {
                graph += $"nodeR{repuestoIndex} [label=\"R{repuestoIndex}\"];\n";
                graph += $"node0 -> nodeR{repuestoIndex};\n";
                repuestoActual = repuestoActual->siguiente;
                repuestoIndex++;
            }

            // Crear nodos para los detalles de los servicios
            Servicio* servicioActual = frente;
            while (servicioActual != null)
            {
                int vehiculoId = servicioActual->ID_Vehiculo;
                int repuestoId = servicioActual->ID_Repuesto;
                string detalles = ObtenerDetalles(servicioActual);

                int vehiculoPos = ObtenerPosicionEncabezado(listaVehiculos, vehiculoId);
                int repuestoPos = ObtenerPosicionEncabezado(listaRepuestosEncabezado, repuestoId);

                graph += $"nodeV{vehiculoPos}_R{repuestoPos} [label=\"{detalles}\"];\n";
                graph += $"nodeV{vehiculoPos} -> nodeV{vehiculoPos}_R{repuestoPos};\n";
                graph += $"nodeR{repuestoPos} -> nodeV{vehiculoPos}_R{repuestoPos};\n";

                servicioActual = servicioActual->Next;
            }

            graph += "}\n";
            return graph;
        }

        private unsafe int ObtenerPosicionEncabezado(ListaEncabezado<int> lista, int id)
        {
            NodoEncabezado<int>* actual = lista.primero;
            int posicion = 1;
            while (actual != null)
            {
                if (actual->id == id)
                {
                    return posicion;
                }
                actual = actual->siguiente;
                posicion++;
            }
            return -1; // No encontrado
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