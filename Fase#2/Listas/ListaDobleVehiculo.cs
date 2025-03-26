using CLases.Vehiculo;
using Nodos.NodoVehiculo;

namespace Listas.ListaDobleVehiculos
{
    public class ListaDobleVehiculo
    {
        private NodoVehiculo? primero;
        private NodoVehiculo? ultimo;

        public ListaDobleVehiculo()
        {
            primero = null;
            ultimo = null;
        }

        public void Insertar(Vehiculos data)
        {
            NodoVehiculo nuevo = new NodoVehiculo(data);
            if (primero == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                if (ultimo != null)
                {
                    ultimo.Siguiente = nuevo;
                    nuevo.Anterior = ultimo;
                    ultimo = nuevo;
                }
            }
        }

        public void Eliminar(int id)
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                if (actual.Data.Id == id)
                {
                    if (actual.Anterior != null)
                    {
                        actual.Anterior.Siguiente = actual.Siguiente;
                    }
                    else
                    {
                        primero = actual.Siguiente;
                    }

                    if (actual.Siguiente != null)
                    {
                        actual.Siguiente.Anterior = actual.Anterior;
                    }
                    else
                    {
                        ultimo = actual.Anterior;
                    }

                    break;
                }
                actual = actual.Siguiente;
            }
        }

        public void Mostrar()
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                Console.WriteLine(actual.Data);
                actual = actual.Siguiente;
            }
        }

        public Vehiculos? BuscarVehiculoPorId(int id)
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                if (actual.Data.Id == id)
                {
                    return actual.Data;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public List<Vehiculos> BuscarVehiculosPorIdUsuario(int idUsuario)
        {
            List<Vehiculos> vehiculos = new List<Vehiculos>();
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                if (actual.Data.IdUsuario == idUsuario)
                {
                    vehiculos.Add(actual.Data);
                }
                actual = actual.Siguiente;
            }
            return vehiculos;
        }

        public bool VehiculoExiste(int id)
        {
            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                if (actual.Data.Id == id)
                {
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        public string GenerarGraphviz()
        {
            if (primero == null) return string.Empty;

            string graph = "digraph G {\n";
            graph += "rankdir=LR;\n"; // Dirección de izquierda a derecha
            graph += "node [shape=record];\n"; // Forma de los nodos
            graph += "subgraph cluster_ListaDobleEnlazada {\n";
            graph += "label = \"Lista Doblemente Enlazada\";\n";

            NodoVehiculo? actual = primero;
            while (actual != null)
            {
                string label = $"ID: {actual.Data.Id}\\nID Usuario: {actual.Data.IdUsuario}\\nMarca: {actual.Data.Marca}\\nModelo: {actual.Data.Modelo}\\nPlaca: {actual.Data.Placa}";
                graph += $"\"{actual.Data.Id}\" [label=\"{label}\"];\n";
                if (actual.Siguiente != null)
                {
                    graph += $"\"{actual.Data.Id}\" -> \"{actual.Siguiente.Data.Id}\";\n";
                    graph += $"\"{actual.Siguiente.Data.Id}\" -> \"{actual.Data.Id}\";\n";
                }
                actual = actual.Siguiente;
            }

            graph += "}\n";
            graph += "}\n";
            return graph;
        }
    }
}