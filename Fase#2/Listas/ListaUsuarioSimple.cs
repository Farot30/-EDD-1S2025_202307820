using System;
using CLases.Usuarios;
using Nodos.NodoUsuario;

namespace Listas.ListaUsuarioSimple
{
    public class ListaUsuarioSimple<T>
    {
        private NodoUsuario<T>? primero;

        public ListaUsuarioSimple()
        {
            primero = null;
        }

        public void Insertar(T data)
        {
            NodoUsuario<T> nuevo = new NodoUsuario<T>(data);
            if (primero == null)
            {
                primero = nuevo;
            }
            else
            {
                NodoUsuario<T>? actual = primero;
                while (actual?.despues != null)
                {
                    actual = actual.despues;
                }
                if (actual != null)
                {
                    actual.despues = nuevo;
                }
            }
        }

        public void Eliminar(T data)
        {
            if (primero != null)
            {
                if (primero.Data != null && primero.Data.Equals(data))
                {
                    primero = primero.despues;
                }
                else
                {
                    NodoUsuario<T>? actual = primero;
                    while (actual?.despues != null)
                    {
                        if (actual.despues.Data != null && actual.despues.Data.Equals(data))
                        {
                            actual.despues = actual.despues.despues;
                            break;
                        }
                        actual = actual.despues;
                    }
                }
            }
        }

        public void Actualizar(int id, string nombre, string apellido, string correo, int edad, string contrasena)
        {
            if (primero != null)
            {
                NodoUsuario<T>? actual = primero;
                while (actual != null)
                {
                    if (actual.Data is Usuario usuario)
                    {
                        if (usuario.Id == id)
                        {
                            usuario.Nombre = nombre;
                            usuario.Apellido = apellido;
                            usuario.Correo = correo;
                            usuario.Edad = edad;
                            usuario.Contrasena = contrasena;
                            break;
                        }
                    }
                    actual = actual.despues;
                }
            }
        }
        
        public string GenerarGraphviz()
        {
            if (primero == null) return string.Empty;

            string graph = "digraph G {\n";
            graph += "rankdir=LR;\n"; // Dirección de izquierda a derecha
            graph += "node [shape=box];\n"; // Forma de los nodos
            graph += "subgraph cluster_ListaSimple {\n";
            graph += "label = \"Lista Simple\";\n";

            NodoUsuario<T>? actual = primero;
            int index = 0;
            while (actual != null)
            {
                string data;
                if (actual.Data is Usuario usuario)
                {
                    data = $"ID: {usuario.Id}\\nNombre: {usuario.Nombre}\\nApellido: {usuario.Apellido}\\nCorreo: {usuario.Correo}";
                }
                else
                {
                    data = actual.Data?.ToString()?.Replace(", ", "\\n") ?? string.Empty;
                }
                graph += $"node{index} [label=\"{data}\"];\n";
                if (actual.despues != null)
                {
                    graph += $"node{index} -> node{index + 1};\n";
                }
                actual = actual.despues;
                index++;
            }

            graph += "}\n";
            graph += "}\n";
            return graph;
        }

        public bool UsuarioExiste(int id)
        {
            NodoUsuario<T>? actual = primero;
            while (actual != null)
            {
                if (actual.Data is Usuario usuario)
                {
                    if (usuario.Id == id)
                    {
                        return true;
                    }
                }
                actual = actual.despues;
            }
            return false;
        }

        public bool CorreoUsuario(string correo)
        {
            NodoUsuario<T>? actual = primero;
            while (actual != null)
            {
                if (actual.Data is Usuario usuario)
                {
                    if (usuario.Correo == correo)
                    {
                        return true;
                    }
                }
                actual = actual.despues;
            }
            return false;
        }

        public void Mostrar()
        {
            NodoUsuario<T>? actual = primero;
            while (actual != null)
            {
                if (actual.Data is Usuario usuario)
                {
                    Console.WriteLine(usuario);
                }
                actual = actual.despues;
            }
        }
    }
}