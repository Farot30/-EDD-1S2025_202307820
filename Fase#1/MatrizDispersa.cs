using System;
using System.Runtime.InteropServices;

namespace Matriz
{
    public unsafe class MatrizDispersa<T> where T : unmanaged
    {
        public int capa; // Representa una capa adicional que puede ser utilizada en la visualización de la matriz.
        public ListaEncabezado<int> filas; // Lista de encabezados para las filas
        public ListaEncabezado<int> columnas; // Lista de encabezados para las columnas

        // Constructor de la clase MatrizDispersa
        public MatrizDispersa(int capa)
        {
            this.capa = capa; // Inicializa la capa
            filas = new ListaEncabezado<int>("Fila"); // Inicializa la lista de encabezados de filas
            columnas = new ListaEncabezado<int>("Columna"); // Inicializa la lista de encabezados de columnas
        }

        // Método para insertar un nuevo nodo en la matriz dispersa
        // (x,y ,dato)
        // (repuesto (R1), vehiculo (V1), detalle)
        public void insert(int pos_x, int pos_y, string nombre)
        {
            // Creación de un nuevo nodo interno que será insertado en la matriz
            // MARSHAL  -> asigna memoria no administrada para un nodo de tipo NodoInterno<int>
            // y devuelve un puntero a la dirección de memoria asignada
            NodoInterno<int>* nuevo = (NodoInterno<int>*)Marshal.AllocHGlobal(sizeof(NodoInterno<int>));
            nuevo->id = 1; // Asigna un ID al nodo
            nuevo->nombre = nombre; // Asigna el nombre proporcionado al nodo
            nuevo->coordenadaX = pos_x; // Asigna la coordenada X (fila)
            nuevo->coordenadaY = pos_y; // Asigna la coordenada Y (columna)
            nuevo->arriba = null;
            nuevo->abajo = null;
            nuevo->derecha = null;
            nuevo->izquierda = null;

            // Verificar si ya existen los encabezados para la fila y columna en la matriz
            NodoEncabezado<int>* nodo_X = filas.getEncabezado(pos_x); // Obtener el encabezado de la fila
            NodoEncabezado<int>* nodo_Y = columnas.getEncabezado(pos_y); // Obtener el encabezado de la columna

            // Si el encabezado de la fila no existe, crearlo
            if (nodo_X == null)
            {
                filas.insertar_nodoEncabezado(pos_x); // Crear encabezado para la fila
                nodo_X = filas.getEncabezado(pos_x); // Obtener el encabezado de la fila recién creado
            }

            // Si el encabezado de la columna no existe, crearlo
            if (nodo_Y == null)
            {
                columnas.insertar_nodoEncabezado(pos_y); // Crear encabezado para la columna
                nodo_Y = columnas.getEncabezado(pos_y); // Obtener el encabezado de la columna recién creado
            }

            // Verificar que ambos encabezados hayan sido creados correctamente
            if (nodo_X == null || nodo_Y == null)
            {
                throw new InvalidOperationException("Error al crear los encabezados.");
            }

            // Insertar el nuevo nodo en la fila correspondiente
            if (nodo_X->acceso == null)
            {
                nodo_X->acceso = nuevo; // Si la fila está vacía, asignamos el nuevo nodo como el primer acceso
            }
            else
            {
                // Si ya hay nodos en la fila, buscamos el lugar adecuado para insertar el nuevo nodo
                NodoInterno<int>* tmp = nodo_X->acceso;
                while (tmp != null)
                {
                    // Si la columna del nuevo nodo es menor que la columna del nodo actual, insertamos el nuevo nodo antes
                    if (nuevo->coordenadaY < tmp->coordenadaY)
                    {
                        nuevo->derecha = tmp;
                        nuevo->izquierda = tmp->izquierda;
                        tmp->izquierda->derecha = nuevo;
                        tmp->izquierda = nuevo;
                        break;
                    }
                    else if (nuevo->coordenadaX == tmp->coordenadaX && nuevo->coordenadaY == tmp->coordenadaY)
                    {
                        // Verificar que no haya nodos duplicados (con las mismas coordenadas)
                        break;
                    }
                    else
                    {
                        // Si no hemos encontrado el lugar, seguimos buscando
                        if (tmp->derecha == null)
                        {
                            tmp->derecha = nuevo; // Insertamos el nodo al final de la fila
                            nuevo->izquierda = tmp;
                            break;
                        }
                        else
                        {
                            tmp = tmp->derecha; // Avanzamos al siguiente nodo en la fila
                        }
                    }
                }
            }

            // Insertar el nuevo nodo en la columna correspondiente
            if (nodo_Y->acceso == null)
            {
                nodo_Y->acceso = nuevo; // Si la columna está vacía, asignamos el nuevo nodo como el primer acceso
            }
            else
            {
                // Si ya hay nodos en la columna, buscamos el lugar adecuado para insertar el nuevo nodo
                NodoInterno<int>* tmp2 = nodo_Y->acceso;
                while (tmp2 != null)
                {
                    // Si la fila del nuevo nodo es menor que la fila del nodo actual, insertamos el nuevo nodo antes
                    if (nuevo->coordenadaX < tmp2->coordenadaX)
                    {
                        nuevo->abajo = tmp2;
                        nuevo->arriba = tmp2->arriba;
                        tmp2->arriba->abajo = nuevo;
                        tmp2->arriba = nuevo;
                        break;
                    }
                    else if (nuevo->coordenadaX == tmp2->coordenadaX && nuevo->coordenadaY == tmp2->coordenadaY)
                    {
                        // Verificar que no haya nodos duplicados (con las mismas coordenadas)
                        break;
                    }
                    else
                    {
                        // Si no hemos encontrado el lugar, seguimos buscando
                        if (tmp2->abajo == null)
                        {
                            tmp2->abajo = nuevo; // Insertamos el nodo al final de la columna
                            nuevo->arriba = tmp2;
                            break;
                        }
                        else
                        {
                            tmp2 = tmp2->abajo; // Avanzamos al siguiente nodo en la columna
                        }
                    }
                }
            }
        }

        // Método para mostrar la matriz dispersa
        public unsafe void mostrar()
        {
            // Imprimir los encabezados de columnas
            NodoEncabezado<int>* y_columna = columnas.primero;
            Console.Write("\t"); // Espacio inicial para alinear las columnas

            // Imprimir los IDs de las columnas
            while (y_columna != null)
            {
                Console.Write("R"+ y_columna->id + "\t"); // Imprimir el encabezado de cada columna
                y_columna = y_columna->siguiente;
            }
            Console.WriteLine(); // Salto de línea después de las cabeceras de las columnas

            // Imprimir los nodos de cada fila
            NodoEncabezado<int>* x_fila = filas.primero;
            while (x_fila != null)
            {
                // Imprimir el encabezado de la fila
                Console.Write("V"+ x_fila->id + "\t");

                // Imprimir los valores de la fila
                NodoInterno<int>* interno = x_fila->acceso;
                NodoEncabezado<int>* y_columna_iter = columnas.primero;

                // Imprimir los valores de las columnas de la fila
                while (y_columna_iter != null)
                {
                    if (interno != null && interno->coordenadaY == y_columna_iter->id)
                    {
                        Console.Write(interno->nombre + "\t"); // Si el nodo interno existe, mostrar su nombre
                        interno = interno->derecha; // Mover al siguiente nodo en la fila
                    }
                    else
                    {
                        Console.Write("0\t"); // Si no hay nodo, mostrar 0 (representa la ausencia de un valor en esa posición)
                    }
                    y_columna_iter = y_columna_iter->siguiente; // Avanzar a la siguiente columna
                }
                Console.WriteLine(); // Salto de línea después de imprimir una fila
                x_fila = x_fila->siguiente; // Avanzar a la siguiente fila
            }
        }

        public unsafe string GenerarGraphvizDispersa()
        {
            string graph = "digraph G {\n";
            graph += "node [shape=box];\n"; 
            graph += "rankdir=TB;\n"; // Top-bottom para la dirección principal
            graph += "splines=ortho;\n"; // Líneas ortogonales
            
            // Nodo matriz principal (esquina superior izquierda)
            graph += "node0 [label=\"\", width=0.5, height=0.5, shape=box];\n";
            
            // Crear rangos y nodos para encabezados de columnas (R)
            graph += "{ rank=same; node0; "; 
            
            // Generar nodos de encabezados de columnas
            NodoEncabezado<int>* y_columna = columnas.primero;
            int columnaIndex = 1;
            while (y_columna != null)
            {
                graph += $"nodeC{columnaIndex}; ";
                y_columna = y_columna->siguiente;
                columnaIndex++;
            }
            graph += "}\n";
            
            // Definir nodos de encabezados de columnas
            y_columna = columnas.primero;
            columnaIndex = 1;
            while (y_columna != null)
            {
                graph += $"nodeC{columnaIndex} [label=\"R{y_columna->id}\"];\n";
                y_columna = y_columna->siguiente;
                columnaIndex++;
            }
            
            // Conectar nodo0 con primer encabezado de columna
            graph += "node0:e -> nodeC1:w [minlen=3];\n";
            graph += "nodeC1:w -> node0:e [minlen=3];\n";
            
            // Conectar encabezados de columnas horizontalmente
            columnaIndex = 1;
            y_columna = columnas.primero;
            while (y_columna != null)
            {
                y_columna = y_columna->siguiente;
                columnaIndex++;
            }
            for (int i = 1; i < columnaIndex - 1; i++)
            {
                graph += $"nodeC{i} -> nodeC{i+1} [minlen=2];\n";
                graph += $"nodeC{i+1} -> nodeC{i} [minlen=2];\n";
            }
            
            // Generar nodos de encabezados de filas
            NodoEncabezado<int>* x_fila = filas.primero;
            int filaIndex = 1;
            
            while (x_fila != null)
            {
                graph += $"nodeF{filaIndex} [label=\"V{x_fila->id}\"];\n";
                
                // Conectar encabezados de filas verticalmente (solo conexiones verticales entre nodos V)
                if (filaIndex > 1)
                {
                    graph += $"nodeF{filaIndex-1}:s -> nodeF{filaIndex}:n [minlen=2];\n";
                    graph += $"nodeF{filaIndex}:n -> nodeF{filaIndex-1}:s [minlen=2];\n";
                }
                
                // Crear subgrafo para mantener alineada cada fila
                graph += $"{{ rank=same; nodeF{filaIndex}; ";
                
                // Añadir nodos de detalle para esta fila
                NodoInterno<int>* interno = x_fila->acceso;
                while (interno != null)
                {
                    int columnaPos = ObtenerPosicionEncabezado(columnas, interno->coordenadaY);
                    graph += $"nodeF{filaIndex}_C{columnaPos}; ";
                    interno = interno->derecha;
                }
                graph += "}\n";
                
                // Definir nodos de detalle para esta fila
                interno = x_fila->acceso;
                while (interno != null)
                {
                    int columnaPos = ObtenerPosicionEncabezado(columnas, interno->coordenadaY);
                    // Solo añadir "Detalle:" si el nombre no está vacío
                    string label = string.IsNullOrEmpty(interno->nombre) ? "Detalle:" : $"Detalle:\\n{interno->nombre}";
                    graph += $"nodeF{filaIndex}_C{columnaPos} [label=\"{label}\"];\n";
                    interno = interno->derecha;
                }
                
                x_fila = x_fila->siguiente;
                filaIndex++;
            }
            
            // Conectar nodo0 con primer encabezado de fila
            graph += "node0:s -> nodeF1:n [minlen=2];\n";
            graph += "nodeF1:n -> node0:s [minlen=2];\n";
            
            // Conectar los nodos internos con sus encabezados y entre sí
            x_fila = filas.primero;
            filaIndex = 1;
            while (x_fila != null)
            {
                // Primero conectamos el encabezado de fila con su primer nodo de detalle (si existe)
                NodoInterno<int>* interno = x_fila->acceso;
                if (interno != null)
                {
                    int columnaPos = ObtenerPosicionEncabezado(columnas, interno->coordenadaY);
                    graph += $"nodeF{filaIndex}:e -> nodeF{filaIndex}_C{columnaPos}:w [minlen=3];\n";
                    graph += $"nodeF{filaIndex}_C{columnaPos}:w -> nodeF{filaIndex}:e [minlen=3];\n";
                    
                    // Luego conectamos los nodos de detalle entre sí horizontalmente
                    NodoInterno<int>* prevInterno = interno;
                    interno = interno->derecha;
                    int prevColumnaPos = ObtenerPosicionEncabezado(columnas, prevInterno->coordenadaY);
                    
                    while (interno != null)
                    {
                        columnaPos = ObtenerPosicionEncabezado(columnas, interno->coordenadaY);
                        graph += $"nodeF{filaIndex}_C{prevColumnaPos} -> nodeF{filaIndex}_C{columnaPos} ;\n";
                        graph += $"nodeF{filaIndex}_C{columnaPos} -> nodeF{filaIndex}_C{prevColumnaPos} ;\n";
                        prevInterno = interno;
                        prevColumnaPos = columnaPos;
                        interno = interno->derecha;
                    }
                }
                
                x_fila = x_fila->siguiente;
                filaIndex++;
            }
            
            // Conectar encabezados de columna con sus nodos debajo
            y_columna = columnas.primero;
            columnaIndex = 1;
            while (y_columna != null)
            {
                NodoInterno<int>* interno = y_columna->acceso;
                if (interno != null)
                {
                    int filaPos = ObtenerPosicionEncabezado(filas, interno->coordenadaX);
                    graph += $"nodeC{columnaIndex} -> nodeF{filaPos}_C{columnaIndex} [minlen=2];\n";
                    graph += $"nodeF{filaPos}_C{columnaIndex} -> nodeC{columnaIndex} [minlen=2];\n";
                    
                    // Conectar nodos de columna verticalmente
                    NodoInterno<int>* prevInterno = interno;
                    interno = interno->abajo;
                    int prevFilaPos = filaPos;
                    
                    while (interno != null)
                    {
                        filaPos = ObtenerPosicionEncabezado(filas, interno->coordenadaX);
                        graph += $"nodeF{prevFilaPos}_C{columnaIndex} -> nodeF{filaPos}_C{columnaIndex}[minlen=3]\n";
                        graph += $"nodeF{filaPos}_C{columnaIndex} -> nodeF{prevFilaPos}_C{columnaIndex}[minlen=3];\n";
                        prevInterno = interno;
                        prevFilaPos = filaPos;
                        interno = interno->abajo;
                    }
                }
                
                y_columna = y_columna->siguiente;
                columnaIndex++;
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

        // Destructor para liberar la memoria de los nodos internos y encabezados
        ~MatrizDispersa()
        {
            // Liberar memoria de los nodos internos y encabezados de filas
            NodoEncabezado<int>* x_fila = filas.primero;
            while (x_fila != null)
            {
                // Liberar los nodos internos de la fila
                NodoInterno<int>* interno = x_fila->acceso;
                while (interno != null)
                {
                    NodoInterno<int>* tmp = interno;
                    interno = interno->derecha;
                    if (tmp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)tmp);
                    }
                }

                // Liberar el encabezado de fila
                NodoEncabezado<int>* tmp_fila = x_fila;
                x_fila = x_fila->siguiente;
                if (tmp_fila != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tmp_fila);
                }
            }

            // Liberar memoria de los nodos internos y encabezados de columnas
            NodoEncabezado<int>* x_columna = columnas.primero;
            while (x_columna != null)
            {
                // Liberar los nodos internos de la columna
                NodoInterno<int>* interno = x_columna->acceso;
                while (interno != null)
                {
                    NodoInterno<int>* tmp = interno;
                    interno = interno->abajo;
                    if (tmp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)tmp);
                    }
                }

                // Liberar el encabezado de columna
                NodoEncabezado<int>* tmp_columna = x_columna;
                x_columna = x_columna->siguiente;
                if (tmp_columna != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tmp_columna);
                }
            }
        }
    }
}
