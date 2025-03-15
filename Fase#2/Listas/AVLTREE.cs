using CLases.Repuesto;
using Nodos.NodoAVLRepus;
using System;

namespace Listas.AVLTREEs
{
    public class AVLTREE
    {
        public NodeAVLRepu? raiz;

        public AVLTREE()
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
    }
}