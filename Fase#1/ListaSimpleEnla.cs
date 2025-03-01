using System;
using System.Runtime.InteropServices;

namespace List
{
    public unsafe class ListaSimple<T> where T : unmanaged
    {
        private Nodo<T>* cabeza;

        public Nodo<T>* Cabeza
        {
            get { return cabeza; }
        }

        public ListaSimple()
        {
            cabeza = null;
        }

        public unsafe void Insertar(T data)
        {
            Nodo<T>* nuevo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevo->Data = data;
            nuevo->Sig = null;

            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                Nodo<T>* temp = cabeza;
                while (temp->Sig != null)
                {
                    temp = temp->Sig;
                }
                temp->Sig = nuevo;
            }
        }

        public unsafe void Eliminar(int id)
        {
            if (cabeza == null) return;

            Nodo<T>* actual = cabeza;
            Nodo<T>* anterior = null;

            while (actual != null)
            {
                Usuarios.Usuario* pUsuario = (Usuarios.Usuario*)(&actual->Data);
                if (pUsuario->Id == id)
                {
                    if (anterior == null)
                    {
                        cabeza = actual->Sig;
                    }
                    else
                    {
                        anterior->Sig = actual->Sig;
                    }
                    Marshal.FreeHGlobal((IntPtr)actual);
                    return;
                }
                anterior = actual;
                actual = actual->Sig;
            }
        }
        public unsafe bool ModificarUsuario(int id, string nuevoNombre, string nuevoApellido, string nuevoCorreo, string nuevaContrasena)
        {
            // se valida que sea tipo usuario
            if (typeof(T) != typeof(Usuarios.Usuario))
            {
                throw new InvalidOperationException("El método ModificarUsuario solo puede ser utilizado con listas de usuarios.");
            }
            // Recorre la lista enlazada.
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                // Obtiene el puntero al dato y lo interpreta como un Usuario.
                Usuarios.Usuario* pUsuario = (Usuarios.Usuario*)(&temp->Data);
                if (pUsuario->Id == id)
                {
                    // Como la memoria es no administrada, los buffers ya tienen una dirección fija.
                    // Simplemente se asignan a punteros locales.
                    char* n = pUsuario->Nombre;
                    char* l = pUsuario->Apellido;
                    char* c = pUsuario->Correo;
                    char* p = pUsuario->Contraseña;
                    int i;

                    // Actualiza el campo Nombre
                    for (i = 0; i < nuevoNombre.Length && i < 50 - 1; i++)
                    {
                        n[i] = nuevoNombre[i];
                    }
                    n[i] = '\0';

                    // Actualiza el campo Apellido
                    for (i = 0; i < nuevoApellido.Length && i < 50 - 1; i++)
                    {
                        l[i] = nuevoApellido[i];
                    }
                    l[i] = '\0';

                    // Actualiza el campo Correo
                    for (i = 0; i < nuevoCorreo.Length && i < 50 - 1; i++)
                    {
                        c[i] = nuevoCorreo[i];
                    }
                    c[i] = '\0';

                    // Actualiza el campo Contraseña
                    for (i = 0; i < nuevaContrasena.Length && i < 50 - 1; i++)
                    {
                        p[i] = nuevaContrasena[i];
                    }
                    p[i] = '\0';

                    Console.WriteLine("Usuario modificado exitosamente.");
                    return true; // finalizar tras modificar
                }
                temp = temp->Sig;
            }
            Console.WriteLine("Usuario no encontrado.");
            return false;
        }



        public unsafe void Imprimir()
        {
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                Console.WriteLine(temp->Data);
                temp = temp->Sig;
            }
        }

       
        public unsafe string GenerarGraphviz()
        {
            if (cabeza == null) return string.Empty;

            string graph = "digraph G {\n";
            graph += "rankdir=LR;\n"; // Dirección de izquierda a derecha
            graph += "node [shape=box];\n"; // Forma de los nodos
            graph += "subgraph cluster_ListaSimple {\n";
            graph += "label = \"Lista Simple\";\n";

            Nodo<T>* actual = cabeza;
            int index = 0;
            while (actual != null)
            {
                string data;
                if (typeof(T) == typeof(Usuarios.Usuario))
                {
                    Usuarios.Usuario* pUsuario = (Usuarios.Usuario*)(&actual->Data);
                    data = $"ID: {pUsuario->Id}\\nNombre: {new string(pUsuario->Nombre)}\\nApellido: {new string(pUsuario->Apellido)}\\nCorreo: {new string(pUsuario->Correo)}";
                }
                else
                {
                    data = actual->Data.ToString().Replace(", ", "\\n");
                }
                graph += $"node{index} [label=\"{data}\"];\n";
                if (actual->Sig != null)
                {
                    graph += $"node{index} -> node{index + 1};\n";
                }
                actual = actual->Sig;
                index++;
            }

            graph += "}\n";
            graph += "}\n";
            return graph;
        }

        ~ListaSimple()
        {
            while (cabeza != null)
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }
    }
}
