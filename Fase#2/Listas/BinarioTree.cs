using System;
using Nodos.NodoBinarios;
using CLases.Servicios;

namespace Listas.BinarioTree
{
    public class Binariostree
    {
        public NodoBinario? raiz;

        public Binariostree()
        {
            raiz = null;
        }

        public void Insertar(Servicio servicio)
        {
            if (raiz == null)
            {
                raiz = new NodoBinario(servicio);
            }
            else
            {
                Insertar(raiz, servicio);
            }
        }

        private void Insertar(NodoBinario nodo, Servicio servicio)
        {
            if (servicio.ID < nodo.servicios.ID)
            {
                if (nodo.izquierdo == null)
                {
                    nodo.izquierdo = new NodoBinario(servicio);
                }
                else
                {
                    Insertar(nodo.izquierdo, servicio);
                }
            }
            else
            {
                if (nodo.derecho == null)
                {
                    nodo.derecho = new NodoBinario(servicio);
                }
                else
                {
                    Insertar(nodo.derecho, servicio);
                }
            }
        }

        public void PreOrden(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                Console.WriteLine(nodo.servicios);
                PreOrden(nodo.izquierdo);
                PreOrden(nodo.derecho);
            }
        }

        public void InOrder(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                InOrder(nodo.izquierdo);
                Console.WriteLine(nodo.servicios);
                InOrder(nodo.derecho);
            }
        }

        public void PostOrden(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                PostOrden(nodo.izquierdo);
                PostOrden(nodo.derecho);
                Console.WriteLine(nodo.servicios);
            }
        }

        public void Eliminar(int id)
        {
            raiz = Eliminar(raiz, id);
        }

        private NodoBinario? Eliminar(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (id < nodo.servicios.ID)
            {
                nodo.izquierdo = Eliminar(nodo.izquierdo, id);
            }
            else if (id > nodo.servicios.ID)
            {
                nodo.derecho = Eliminar(nodo.derecho, id);
            }
            else
            {
                if (nodo.izquierdo == null)
                {
                    return nodo.derecho;
                }
                else if (nodo.derecho == null)
                {
                    return nodo.izquierdo;
                }

                nodo.servicios = Minimo(nodo.derecho).servicios;
                nodo.derecho = Eliminar(nodo.derecho, nodo.servicios.ID);
            }

            return nodo;
        }

        private NodoBinario Minimo(NodoBinario? nodo)
        {
            NodoBinario actual = nodo!;
            while (actual.izquierdo != null)
            {
                actual = actual.izquierdo;
            }
            return actual;
        }

        public bool Existeid(int id)
        {
            return Existeid(raiz, id);
        }

        private bool Existeid(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return false;
            }

            if (nodo.servicios.ID == id)
            {
                return true;
            }
            else if (id < nodo.servicios.ID)
            {
                return Existeid(nodo.izquierdo, id);
            }
            else
            {
                return Existeid(nodo.derecho, id);
            }
        }

        public Servicio? BuscarServicioPorId(int id)
        {
            return BuscarServicioPorId(raiz, id);
        }

        private Servicio? BuscarServicioPorId(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (nodo.servicios.ID == id)
            {
                return nodo.servicios;
            }
            else if (id < nodo.servicios.ID)
            {
                return BuscarServicioPorId(nodo.izquierdo, id);
            }
            else
            {
                return BuscarServicioPorId(nodo.derecho, id);
            }
        }
        public string GenerarGraphviz()
        {
            var graphviz = new System.Text.StringBuilder();
            graphviz.AppendLine("digraph G {");
            GenerarGraphviz(raiz, graphviz);
            graphviz.AppendLine("}");
            return graphviz.ToString();
        }

        private void GenerarGraphviz(NodoBinario? nodo, System.Text.StringBuilder graphviz)
        {
            if (nodo != null)
            {
                string nodoLabel = $"ID: {nodo.servicios.ID}\\nID Repuesto: {nodo.servicios.ID_repuesto}\\nID Vehiculo: {nodo.servicios.ID_vehiculo}\\nDetalle: {nodo.servicios.Detalle}\\nCosto: {nodo.servicios.Costo}";
                graphviz.AppendLine($"    \"{nodo.servicios.ID}\" [label=\"{nodoLabel}\"];");

                if (nodo.izquierdo != null)
                {
                    graphviz.AppendLine($"    \"{nodo.servicios.ID}\" -> \"{nodo.izquierdo.servicios.ID}\";");
                    GenerarGraphviz(nodo.izquierdo, graphviz);
                }
                if (nodo.derecho != null)
                {
                    graphviz.AppendLine($"    \"{nodo.servicios.ID}\" -> \"{nodo.derecho.servicios.ID}\";");
                    GenerarGraphviz(nodo.derecho, graphviz);
                }
            }
        }

        // Método para mostrar los servicios en la consola
        public void Mostrar()
        {
            MostrarRecursivo(raiz);
        }

        private void MostrarRecursivo(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                MostrarRecursivo(nodo.izquierdo);
                Console.WriteLine($"ID: {nodo.servicios.ID}, ID Repuesto: {nodo.servicios.ID_repuesto}, ID Vehiculo: {nodo.servicios.ID_vehiculo}, Detalle: {nodo.servicios.Detalle}, Costo: {nodo.servicios.Costo}");
                MostrarRecursivo(nodo.derecho);
            }
        }
    }
}