using System;
using Gtk;
using logiarse;
using CLases.Repuesto;
using CLases.Usuarios;
using Nodos.NodoAVLRepus;
using Listas.AVLTREEs;
using Listas.ListaUsuarioSimple;
using GeneracionGrafica;

namespace Program
{
    class MainClass
    {
        static void Main()
        {
            /*Application.Init();
            Logiarse login = new Logiarse();
            login.ShowAll();
            Application.Run();*/

            // Crear el árbol AVL y agregar algunos repuestos
            AVLTREE arbolRepuestos = new AVLTREE();
            arbolRepuestos.Add(new Repuesto(1, "Filtro de aceite", "Filtro para motor", 15.99));
            arbolRepuestos.Add(new Repuesto(2, "Bujía", "Bujía de encendido", 5.99));
            arbolRepuestos.Add(new Repuesto(3, "Pastillas de freno", "Juego de pastillas de freno", 45.99));
            arbolRepuestos.Add(new Repuesto(4, "Aceite de motor", "Aceite sintético", 29.99));
            arbolRepuestos.Add(new Repuesto(5, "Batería", "Batería de coche", 89.99));
            arbolRepuestos.Add(new Repuesto(6, "Neumático", "Neumático de repuesto", 120.00));
            arbolRepuestos.Add(new Repuesto(7, "Amortiguador", "Amortiguador delantero", 75.50));
            arbolRepuestos.Add(new Repuesto(8, "Radiador", "Radiador de coche", 150.00));
            arbolRepuestos.Add(new Repuesto(9, "Alternador", "Alternador de coche", 200.00));
            arbolRepuestos.Add(new Repuesto(10, "Faro", "Faro delantero", 60.00));

            // Mostrar los repuestos en orden
            Console.WriteLine("Repuestos en preorden:");
            arbolRepuestos.Preorden(arbolRepuestos.raiz);
            Console.WriteLine();

            Console.WriteLine("Repuestos en inorden:");
            arbolRepuestos.Inorden(arbolRepuestos.raiz);
            Console.WriteLine();

            Console.WriteLine("Repuestos en postorden:");
            arbolRepuestos.Postorden(arbolRepuestos.raiz);
            Console.WriteLine();

            // Crear la lista de usuarios y agregar algunos usuarios
            ListaUsuarioSimple<Usuario> listaUsuarios = new ListaUsuarioSimple<Usuario>();
            listaUsuarios.Insertar(new Usuario(1, "Juan", "Perez", "juan@example.com", 25, "password123"));
            listaUsuarios.Insertar(new Usuario(2, "Maria", "Lopez", "maria@example.com", 30, "password456"));
            listaUsuarios.Insertar(new Usuario(3, "Carlos", "Garcia", "carlos@example.com", 28, "password789"));

            // Generar y mostrar el gráfico Graphviz de la lista de usuarios
            string graphviz = listaUsuarios.GenerarGraphviz();

            // Generar el archivo .dot y convertirlo a imagen
            Grafica.GenerarArchivoDot("lista_usuarios.dot", graphviz, "Lista de Usuarios");
            Grafica.ConvertirDotAImagen("lista_usuarios.dot");
        }
    }
}