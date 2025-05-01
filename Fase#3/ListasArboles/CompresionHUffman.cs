using System;
using System.Collections.Generic;
using System.Text;
using Nodos.NofoHUffman;

namespace ListasArboles.CompresionHUffman
{
    public class CompresionHuffman
    {

        public static (string Compresion, NodoHuffman raiz) ComprimirTextoHuffman(string entrada)
        {
            if (string.IsNullOrEmpty(entrada))
                throw new ArgumentException("La entrada no puede estar vacía.");
            //Recorre el texto para saber cuantas veces aparece un caracter
            Dictionary<char, int> frequencies = new Dictionary<char, int>();
            foreach (char c in entrada)
            {
                
                if (frequencies.ContainsKey(c))
                    frequencies[c]++;
                else
                    frequencies[c] = 1;
            }

            //Cola de prioridad para manejar los nodos segun su frecuencia
            PriorityQueue<NodoHuffman> priorityQueue = new PriorityQueue<NodoHuffman>();
            //Crea un nodo por caracter y lo añade a la cola de prioridad
            foreach (var kvp in frequencies)
            {
                priorityQueue.Encolar(new NodoHuffman { Character = kvp.Key, Frequency = kvp.Value });
            }

            //Construir el arbol de huffman, debe haber 2
            //elementos como minimo para ejecutarse
            while (priorityQueue.Count > 1)
            {
                NodoHuffman left = priorityQueue.Desencolar();
                Console.WriteLine(left.Character);            
                NodoHuffman right = priorityQueue.Desencolar();
                Console.WriteLine(right.Character);
                Console.WriteLine("----");
                //Combina dos nodos con menor frecuencia en un nodo padre
                NodoHuffman parent = new NodoHuffman
                {
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };
            
                priorityQueue.Encolar(parent);
            }
            //Crea nodo raiz
            NodoHuffman root = priorityQueue.Desencolar();
            Dictionary<char, string> codes = GenerarCodigoHuffman(root);

            StringBuilder compressed = new StringBuilder();
            //Reemplaza cada caracter del texto original por su codigo huffman
            foreach (char c in entrada)
            {
                compressed.Append(codes[c]);
            }

            return (compressed.ToString(), root);
        }


        public static string Descomprimir(string compressed, NodoHuffman root)
        {
            StringBuilder decompressed = new StringBuilder();
            NodoHuffman current = root;
            //Recorre los bits comprimidos por el arbol 
            //Al llegar al nodo hoja se añade el caracter al resultado y se reinicia desde la raiz
            foreach (char bit in compressed)
            {
                if (bit == '0')
                    current = current.Left;
                else if (bit == '1')
                    current = current.Right;

                if (current.Character != '\0')
                {
                    decompressed.Append(current.Character);
                    current = root;
                }
            }

            return decompressed.ToString();
        }

        private static Dictionary<char, string> GenerarCodigoHuffman(NodoHuffman raiz)
        {
            var codes = new Dictionary<char, string>(); //Crea diccionario para almacenar el codigo a cada caracter
            GenerarCodigoHuffman(raiz, "", codes);
            return codes;
        }

        private static void GenerarCodigoHuffman(NodoHuffman nodo, string codigo, Dictionary<char, string> codigos)
        {
            //Realiza un recorrido del arbol
            
            if (nodo == null)
                return;


            if (nodo.Character != '\0')
                codigos[nodo.Character] = codigo;

            //A los nodos izquierdos les asigna un 0
            GenerarCodigoHuffman(nodo.Left, codigo + "0", codigos);
            //A los nodos derecha les asigna un 1
            GenerarCodigoHuffman(nodo.Right, codigo + "1", codigos);
        }
        //Los caracteres en las hojas obtienen el codigo acumulado del camino desde la raiz
        


    }

    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> list = new List<T>();

        public int Count => list.Count;

        public void Encolar(T Elemento)
        {
            //Añade al final
            list.Add(Elemento);
            int i = list.Count - 1;

            //Sube el elemento 
            while (i > 0)
            {
                //Compara el elemento con su padre
                //Si es menor intercambia las posiciones
                int parent = (i - 1) / 2;
                //si nodo riaz >= nodo no intercambia
                if (list[i].CompareTo(list[parent]) >= 0)
                    break;
                //Intercambio
                Intercambiar(i, parent);
                i = parent;
            }
        }

        public T Desencolar()
        {
            if (list.Count == 0)
                throw new InvalidOperationException("Queue is empty");
            //Obtiene el nodo minoma frecuencia
            T front = list[0];
            list[0] = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            int current = 0;

            //Reordena
            while (true)
            {
                int left = 2 * current + 1;
                int right = 2 * current + 2;
                int smallest = current;
                //Encuentra el menor entre el actual y sus hijos
                if (left < list.Count && list[left].CompareTo(list[smallest]) < 0)
                    smallest = left;
                if (right < list.Count && list[right].CompareTo(list[smallest]) < 0)
                    smallest = right;
                if (smallest == current)
                    break;

                //Intercambia con el hijo menor
                Intercambiar(current, smallest);
                current = smallest;
            }
            return front;
        }

        private void Intercambiar(int i, int j)
        {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}