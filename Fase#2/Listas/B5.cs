using System;
using Clases.Factura;
using Nodos.NodoFacturas;
using System.Collections.Generic;
using System.Text;

namespace Listas.B5Factura
{
    public class B5Facturas
    {
        private NodoFactura raiz;
        private const int ORDEN = 5;
        private const int Maximo = ORDEN - 1;
        private const int Minimo = (ORDEN / 2) - 1;

        public B5Facturas()
        {
            raiz = new NodoFactura();
        }

        public void Insertar(int id, int id_servicio, double total)
        {
            Facturas nuevaFactura = new Facturas(id, id_servicio, total);

            if (raiz.EstaLleno())
            {
                NodoFactura nuevaRaiz = new NodoFactura();
                nuevaRaiz.EsHoja = false;
                nuevaRaiz.Hijos.Add(raiz);
                DividirHijo(nuevaRaiz, 0);
                raiz = nuevaRaiz;
            }
            insertarNoLleno(raiz, nuevaFactura);
        }   

        private void DividirHijo(NodoFactura padre, int IndiceHijo)
        {
            NodoFactura hijo = padre.Hijos[IndiceHijo];
            NodoFactura nuevoHijo = new NodoFactura();
            nuevoHijo.EsHoja = hijo.EsHoja;

            Facturas mediana = hijo.Claves[Minimo];

            for(int i = Minimo + 1; i < Maximo; i++)
            {
                nuevoHijo.Claves.Add(hijo.Claves[i]);
            }

            if(!hijo.EsHoja)
            {
                for(int i = Minimo + 1; i < ORDEN; i++)
                {
                    nuevoHijo.Hijos.Add(hijo.Hijos[i]);
                }
                hijo.Hijos.RemoveRange((ORDEN / 2), hijo.Hijos.Count - (ORDEN / 2));
            }

            hijo.Claves.RemoveRange(Minimo, hijo.Claves.Count - Minimo);

            padre.Hijos.Insert(IndiceHijo + 1, nuevoHijo);

            int j = 0;
            while(j< padre.Claves.Count && padre.Claves[j].ID < mediana.ID)
            {
                j++;
            }
            padre.Claves.Insert(j, mediana);
        }

        private void insertarNoLleno(NodoFactura nodo, Facturas nuevaFactura)
        {
            int i = nodo.Claves.Count - 1;

            if (nodo.EsHoja)
            {
                nodo.Claves.Insert(nodo.Claves.Count, nuevaFactura); // Insertar el nuevo elemento en la posición correcta
                while (i >= 0 && nuevaFactura.ID < nodo.Claves[i].ID)
                {
                    nodo.Claves[i + 1] = nodo.Claves[i];
                    i--;
                }
                nodo.Claves[i + 1] = nuevaFactura;
            }
            else
            {
                while (i >= 0 && nuevaFactura.ID < nodo.Claves[i].ID)
                {
                    i--;
                }
                i++;

                if (nodo.Hijos[i].EstaLleno())
                {
                    DividirHijo(nodo, i);
                    if (nuevaFactura.ID > nodo.Claves[i].ID)
                    {
                        i++;
                    }
                }
                insertarNoLleno(nodo.Hijos[i], nuevaFactura);
            }
        }

        public Facturas? Buscar(int id)
        {
            return BuscarRecursivo(raiz, id);
        }

        private Facturas? BuscarRecursivo(NodoFactura nodo, int id)
        {
            int i = 0;
            while(i < nodo.Claves.Count && id > nodo.Claves[i].ID)
            {
                i++;
            }

            if(i < nodo.Claves.Count && id == nodo.Claves[i].ID)
            {
                return nodo.Claves[i];
            }

            if(nodo.EsHoja)
            {
                return null;
            }

            return BuscarRecursivo(nodo.Hijos[i], id);

        }

        public int GenerarNuevoID()
        {
            int nuevoID = 1;
            if (raiz != null)
            {
                List<Facturas> facturas = RecorridoInOrden();
                if (facturas.Count > 0)
                {
                    nuevoID = facturas.Max(f => f.ID) + 1;
                }
            }
            return nuevoID;
        }

        public void Elminar(int id)
        {
            EliminarRecursivo(raiz, id);

            if(raiz.Claves.Count == 0 && !raiz.EsHoja)
            {
                NodoFactura antiguaRaiz = raiz;
                raiz = raiz.Hijos[0];
            }
        }

        private void EliminarRecursivo(NodoFactura nodo, int id)
        {
            int i = EncontrarIndice(nodo, id);

            if(i < nodo.Claves.Count && nodo.Claves[i].ID == id)
            {
                if(nodo.EsHoja)
                {
                    nodo.Claves.RemoveAt(i);
                }
                else
                {
                    EliminarDeNodoInterno(nodo, i);
                }
            }
            else
            {

                // caso 2: la clave no está en este nodo
                if(nodo.EsHoja)
                {
                    Console.WriteLine($"El elemento con Id {id} no existe en el arbol" );
                    return;
                }
                
                bool ultimoHijo =(i == nodo.Claves.Count);

                if(!nodo.Hijos[i].EstaVacio())
                {
                    RellenarHijo(nodo, i);
                }

                if(ultimoHijo && i > nodo.Claves.Count - 1)
                {
                    EliminarRecursivo(nodo.Hijos[i - 1], id);
                }
                else
                {
                    EliminarRecursivo(nodo.Hijos[i], id);
                }
            }
        }

        private int EncontrarIndice(NodoFactura nodo, int id)
        {
            int i = 0;
            while(i < nodo.Claves.Count && nodo.Claves[i].ID < id)
            {
                i++;
            }
            return i;
        }

        private void EliminarDeNodoInterno(NodoFactura nodo, int indice)
        {
            Facturas clave = nodo.Claves[indice];

             // Caso 2a: Si el hijo anterior tiene más del mínimo de claves
            if(nodo.Hijos[indice].Claves.Count > Minimo)
            {
                // Reemplazar clave con el predecesor
                Facturas predecesor = ObtenerPredecesor(nodo, indice);
                nodo.Claves[indice] = predecesor;
                EliminarRecursivo(nodo.Hijos[indice], predecesor.ID);
            }
            else if(nodo.Hijos[indice + 1].Claves.Count > Minimo)
            {
                // Caso 2b: Si el hijo siguiente tiene más del mínimo de claves
                Facturas sucesor = ObtenerSucesor(nodo, indice);
                nodo.Claves[indice] = sucesor;
                EliminarRecursivo(nodo.Hijos[indice + 1], sucesor.ID);
            }
            // Caso 2c: Si ambos hijos tienen el mínimo de claves
            else
            {
                Fusionar(nodo, indice);
                EliminarRecursivo(nodo.Hijos[indice], clave.ID);
            }

        }

        private Facturas ObtenerPredecesor(NodoFactura nodo, int indice)
        {
            NodoFactura actual = nodo.Hijos[indice];
            while(!actual.EsHoja)
            {
                actual = actual.Hijos[actual.Claves.Count];
            }
            return actual.Claves[actual.Claves.Count - 1];
        }

        private Facturas ObtenerSucesor(NodoFactura nodo, int indice)
        {
            NodoFactura actual = nodo.Hijos[indice + 1];
            while(!actual.EsHoja)
            {
                actual = actual.Hijos[0];
            }
            return actual.Claves[0];
        }

        private void RellenarHijo(NodoFactura nodo, int indice)
        {
            if(indice != 0 && nodo.Hijos[indice - 1].Claves.Count > Minimo)
            {
                TomarPrestadoDeAnterior(nodo, indice);
            }
            else if(indice != nodo.Claves.Count && nodo.Hijos[indice + 1].Claves.Count > Minimo)
            {
                TomarPrestadoDeSiguiente(nodo, indice);
            }
            else
            {
                if(indice != nodo.Claves.Count)
                {
                    Fusionar(nodo, indice);
                }
                else
                {
                    Fusionar(nodo, indice - 1);
                }
            }
        }

        private void TomarPrestadoDeAnterior(NodoFactura nodo, int indice)
        {
            NodoFactura hijo = nodo.Hijos[indice];
            NodoFactura hermano = nodo.Hijos[indice - 1];

            hijo.Claves.Insert(0, nodo.Claves[indice - 1]);
            if(!hermano.EsHoja)
            {
                hijo.Hijos.Insert(0, hermano.Hijos[hermano.Claves.Count]);
                hermano.Hijos.RemoveAt(hermano.Claves.Count);
            }

            nodo.Claves[indice - 1] = hermano.Claves[hermano.Claves.Count - 1];
            hermano.Claves.RemoveAt(hermano.Claves.Count - 1);
        }

        private void TomarPrestadoDeSiguiente(NodoFactura nodo, int indice)
        {
            NodoFactura hijo = nodo.Hijos[indice];
            NodoFactura hermano = nodo.Hijos[indice + 1];

            hijo.Claves.Add(nodo.Claves[indice]);
            if(!hermano.EsHoja)
            {
                hijo.Hijos.Add(hermano.Hijos[0]);
                hermano.Hijos.RemoveAt(0);
            }

            nodo.Claves[indice] = hermano.Claves[0];
            hermano.Claves.RemoveAt(0);
        }

        private void Fusionar(NodoFactura nodo, int indice)
        {
            NodoFactura hijo = nodo.Hijos[indice];
            NodoFactura hermano = nodo.Hijos[indice + 1];

            hijo.Claves.Add(nodo.Claves[indice]);

            for(int i = 0; i < hermano.Claves.Count; i++)
            {
                hijo.Claves.Add(hermano.Claves[i]);
            }

            if(!hijo.EsHoja)
            {
                for(int i = 0; i < hermano.Hijos.Count; i++)
                {
                    hijo.Hijos.Add(hermano.Hijos[i]);
                }
            }

            nodo.Claves.RemoveAt(indice);
            nodo.Hijos.RemoveAt(indice + 1);
        }

        //Recorrido INORDEN del árbol
        public List<Facturas> RecorridoInOrden()
        {
            List<Facturas> lista = new List<Facturas>();
            RecorridoInOrdenRecursivo(raiz, lista);
            return lista;
        }

        private void RecorridoInOrdenRecursivo(NodoFactura? nodo, List<Facturas> restultado)
        {
            if(nodo == null)
                return;

            int i;
            for(i = 0; i < nodo.Claves.Count; i++)
            {
                if(!nodo.EsHoja)
                {
                    RecorridoInOrdenRecursivo(nodo.Hijos[i], restultado);
                }
                restultado.Add(nodo.Claves[i]);
            }
            if(!nodo.EsHoja)
            {
                RecorridoInOrdenRecursivo(nodo.Hijos[i], restultado);
            }
        }

        public bool FacturaExiste(int id)
        {
            return Buscar(id) != null;
        }

        // Método para generar el archivo .dot para Graphviz
        public string GraficarGraphviz()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("digraph BTree {");
            sb.AppendLine("    node [shape=record];");
            
            int contadorNodos = 0;
            GraficarGraphvizRecursivo(raiz, sb, ref contadorNodos);
            
            sb.AppendLine("}");
            Console.WriteLine(sb.ToString());
            return sb.ToString();
        }

        private void GraficarGraphvizRecursivo(NodoFactura nodo, StringBuilder sb, ref int contadorNodos)
        {
            if (nodo == null)
                return;

            int nodoActual = contadorNodos++;
            
            // Construir la etiqueta del nodo
            StringBuilder etiquetaNodo = new StringBuilder();
            etiquetaNodo.Append($"node{nodoActual} [label=\"");
            
            for (int i = 0; i < nodo.Claves.Count; i++)
            {
                if (i > 0)
                    etiquetaNodo.Append("|");
                etiquetaNodo.Append($"<f{i}> Id: {nodo.Claves[i].ID}|ID_orden: {nodo.Claves[i].ID_servicio}|Total: {nodo.Claves[i].Total}");
            }
            
            // Añadir un puerto más para el último hijo
            if (nodo.Claves.Count > 0)
                etiquetaNodo.Append($"|<f{nodo.Claves.Count}>");
            
            etiquetaNodo.Append("\"];");
            sb.AppendLine(etiquetaNodo.ToString());
            
            // Graficar los hijos y sus conexiones
            if (!nodo.EsHoja)
            {
                for (int i = 0; i <= nodo.Claves.Count; i++)
                {
                    int hijoPosicion = contadorNodos;
                    GraficarGraphvizRecursivo(nodo.Hijos[i], sb, ref contadorNodos);
                    sb.AppendLine($"    node{nodoActual}:f{i} -> node{hijoPosicion};");
                }
            }
        }
        // Método para mostrar las facturas en la consola
        public void Mostrar()
        {
            List<Facturas> facturas = RecorridoInOrden();
            foreach (Facturas factura in facturas)
            {
                Console.WriteLine($"ID: {factura.ID}, ID Servicio: {factura.ID_servicio}, Total: {factura.Total}");
            }
        }

    }
    
}