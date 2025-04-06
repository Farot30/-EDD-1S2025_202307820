using Clases.Repuesto;
using Nodos.NodoAVLRepus;
using System;

namespace Listas.AVLRepuestos
{
    public class AVLTREErepues
    {
        public NodeAVLRepu? raiz;

        public AVLTREErepues()
        {
            raiz = null;
        }

        public void Add(Repuesto val)
        {
            if (raiz != null)
            {
                Add(val, ref raiz);
            }
            else
            {
                raiz = new NodeAVLRepu(val);
            }
        }

        public void Add(Repuesto val, ref NodeAVLRepu? padre)
        {
            if (padre == null)
                padre = new NodeAVLRepu(val);
            else if (val.Id < padre.val.Id)
            {
                Add(val, ref padre.izq);
                if (Altura(padre.izq) - Altura(padre.der) == 2)
                {
                    if (padre.izq != null && val.Id < padre.izq.val.Id)
                        padre = RotacionIzquierda(padre);
                    else
                        padre = RotacionDobleIzquierda(padre);
                }
            }
            else if (val.Id > padre.val.Id)
            {
                Add(val, ref padre.der);
                if (Altura(padre.der) - Altura(padre.izq) == 2)
                {
                    if (padre.der != null && val.Id > padre.der.val.Id)
                        padre = RotacionDerecha(padre);
                    else
                        padre = RotacionDobleDerecha(padre);
                }
            }

            int alturaIzq = Altura(padre.izq);
            int alturaDer = Altura(padre.der);
            int m = Maxi(alturaIzq, alturaDer);
            padre.altura = m + 1;
        }

        public int Altura(NodeAVLRepu? nodo)
        {
            return nodo == null ? -1 : nodo.altura;
        }

        public int Maxi(int a, int b)
        {
            return a > b ? a : b;
        }

        public void Preorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Console.WriteLine(nodo.val);
                Preorden(nodo.izq);
                Preorden(nodo.der);
            }
        }

        public void Inorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Inorden(nodo.izq);
                Console.WriteLine(nodo.val);
                Inorden(nodo.der);
            }
        }

        public void Postorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Postorden(nodo.izq);
                Postorden(nodo.der);
                Console.WriteLine(nodo.val);
            }
        }

        public NodeAVLRepu RotacionIzquierda(NodeAVLRepu temp)
        {
            NodeAVLRepu? aux = temp.izq;
            if (aux != null)
            {
                temp.izq = aux.der;
                aux.der = temp;
                temp.altura = Maxi(Altura(temp.izq), Altura(temp.der)) + 1;
                aux.altura = Maxi(Altura(aux.izq), temp.altura) + 1;
            }
            return aux ?? temp;
        }

        public NodeAVLRepu RotacionDerecha(NodeAVLRepu temp)
        {
            NodeAVLRepu? aux = temp.der;
            if (aux != null)
            {
                temp.der = aux.izq;
                aux.izq = temp;
                temp.altura = Maxi(Altura(temp.izq), Altura(temp.der)) + 1;
                aux.altura = Maxi(Altura(aux.der), temp.altura) + 1;
            }
            return aux ?? temp;
        }

        public NodeAVLRepu RotacionDobleIzquierda(NodeAVLRepu temp)
        {
            if (temp.izq != null)
            {
                temp.izq = RotacionDerecha(temp.izq);
            }
            return RotacionIzquierda(temp);
        }

        public NodeAVLRepu RotacionDobleDerecha(NodeAVLRepu temp)
        {
            if (temp.der != null)
            {
                temp.der = RotacionIzquierda(temp.der);
            }
            return RotacionDerecha(temp);
        }

        public void Mostrar()
        {
            Mostrar(raiz);
        }

        private void Mostrar(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Mostrar(nodo.izq);
                Console.WriteLine(nodo.val);
                Mostrar(nodo.der);
            }
        }

        public bool RepuestoExiste(int id)
        {
            return RepuestoExiste(raiz, id);
        }

        private bool RepuestoExiste(NodeAVLRepu? nodo, int id)
        {
            if (nodo == null)
            {
                return false;
            }

            if (nodo.val.Id == id)
            {
                return true;
            }
            else if (id < nodo.val.Id)
            {
                return RepuestoExiste(nodo.izq, id);
            }
            else
            {
                return RepuestoExiste(nodo.der, id);
            }
        }

        public Repuesto? BuscarRepuestoPorId(int id)
        {
            return BuscarRepuestoPorId(raiz, id);
        }

        private Repuesto? BuscarRepuestoPorId(NodeAVLRepu? nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (nodo.val.Id == id)
            {
                return nodo.val;
            }
            else if (id < nodo.val.Id)
            {
                return BuscarRepuestoPorId(nodo.izq, id);
            }
            else
            {
                return BuscarRepuestoPorId(nodo.der, id);
            }
        }

        public bool ActualizarRepuesto(int id, string nombre, string descripcion, double precio)
        {
            return ActualizarRepuesto(raiz, id, nombre, descripcion, precio);
        }

        private bool ActualizarRepuesto(NodeAVLRepu? nodo, int id, string nombre, string descripcion, double precio)
        {
            if (nodo == null)
            {
                return false;
            }

            if (nodo.val.Id == id)
            {
                nodo.val.Nombre = nombre;
                nodo.val.Descripcion = descripcion;
                nodo.val.Precio = precio;
                return true;
            }
            else if (id < nodo.val.Id)
            {
                return ActualizarRepuesto(nodo.izq, id, nombre, descripcion, precio);
            }
            else
            {
                return ActualizarRepuesto(nodo.der, id, nombre, descripcion, precio);
            }
        }

        public string GenerarGraphviz()
        {
            var graphviz = new System.Text.StringBuilder();
            graphviz.AppendLine("digraph G {");
            graphviz.AppendLine("    node [shape=record];");
            GenerarGraphviz(raiz, graphviz);
            graphviz.AppendLine("}");
            return graphviz.ToString();
        }

        private void GenerarGraphviz(NodeAVLRepu? nodo, System.Text.StringBuilder graphviz)
        {
            if (nodo != null)
            {
                string nodoLabel = $"{{ID: {nodo.val.Id}|Nombre: {nodo.val.Nombre}|Descripcion: {nodo.val.Descripcion}|Precio: {nodo.val.Precio}}}";
                graphviz.AppendLine($"    \"{nodo.val.Id}\" [label=\"{nodoLabel}\"];");

                if (nodo.izq != null)
                {
                    graphviz.AppendLine($"    \"{nodo.val.Id}\" -> \"{nodo.izq.val.Id}\";");
                    GenerarGraphviz(nodo.izq, graphviz);
                }
                if (nodo.der != null)
                {
                    graphviz.AppendLine($"    \"{nodo.val.Id}\" -> \"{nodo.der.val.Id}\";");
                    GenerarGraphviz(nodo.der, graphviz);
                }
            }
        }
    }
}