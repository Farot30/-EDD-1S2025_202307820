using System;
using Nodos.NodoBinarios;

namespace Listas.BinarioTree
{
    public class Binariostree
    {
        public NodoBinario? raiz;

        public Binariostree()
        {
            raiz = null;
        }

        public void Insertar(int valor)
        {
            if (raiz == null)
            {
                raiz = new NodoBinario(valor);
            }
            else
            {
                Insertar(raiz, valor);
            }
        }

        private void Insertar(NodoBinario nodo, int valor)
        {
            if (valor < nodo.valor)
            {
                if (nodo.izquierdo == null)
                {
                    nodo.izquierdo = new NodoBinario(valor);
                }
                else
                {
                    Insertar(nodo.izquierdo, valor);
                }
            }
            else
            {
                if (nodo.derecho == null)
                {
                    nodo.derecho = new NodoBinario(valor);
                }
                else
                {
                    Insertar(nodo.derecho, valor);
                }
            }
        }

        public void PreOrden(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                Console.WriteLine(nodo.valor + " ");
                PreOrden(nodo.izquierdo);
                PreOrden(nodo.derecho);
            }
        }

        public void InOrder(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                InOrder(nodo.izquierdo);
                Console.WriteLine(nodo.valor + " ");
                InOrder(nodo.derecho);
            }
        }

        public void PostOrden(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                PostOrden(nodo.izquierdo);
                PostOrden(nodo.derecho);
                Console.WriteLine(nodo.valor + " ");
            }
        }

        public void Eliminar(int valor)
        {
            raiz = Eliminar(raiz, valor);
        }

        private NodoBinario? Eliminar(NodoBinario? nodo, int valor)
        {
            if (nodo == null)
            {
                return null;
            }

            if (valor < nodo.valor)
            {
                nodo.izquierdo = Eliminar(nodo.izquierdo, valor);
            }
            else if (valor > nodo.valor)
            {
                nodo.derecho = Eliminar(nodo.derecho, valor);
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

                nodo.valor = Minimo(nodo.derecho);
                nodo.derecho = Eliminar(nodo.derecho, nodo.valor);
            }

            return nodo;
        }

        private int Minimo(NodoBinario? nodo)
        {
            int minv = nodo!.valor;
            while (nodo!.izquierdo != null)
            {
                minv = nodo.izquierdo.valor;
                nodo = nodo.izquierdo;
            }
            return minv;
        }

        public bool Existeid(int valor)
        {
            return Existeid(raiz, valor);
        }

        private bool Existeid(NodoBinario? nodo, int valor)
        {
            if (nodo == null)
            {
                return false;
            }

            if (nodo.valor == valor)
            {
                return true;
            }
            else if (valor < nodo.valor)
            {
                return Existeid(nodo.izquierdo, valor);
            }
            else
            {
                return Existeid(nodo.derecho, valor);
            }
        }

    }
}