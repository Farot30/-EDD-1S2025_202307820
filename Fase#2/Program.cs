using System;
using Gtk;
using logiarse;
using CLases.Repuesto;
using CLases.Usuarios;
using Nodos.NodoAVLRepus;
using Listas.AVLTREEsRE;
using Listas.ListaUsuarioSimple;
using GeneracionGrafica;
using Listas.ListaDobleVehiculos;
using Listas.B5Factura;
using Clases.Factura;
using Nodos.NodoFacturas;

namespace Program
{
    class MainClass
    {
        static void Main()
        {
            ListaUsuarioSimple<Usuario> listaUsuarios = new ListaUsuarioSimple<Usuario>();
            ListaDobleVehiculo listaAutos = new ListaDobleVehiculo();
            AVLTREErepues ListaRepuestos = new AVLTREErepues();
            B5Facturas ListaFacturas = new B5Facturas();

            listaUsuarios.Insertar(new Usuario(1, "Juan", "Perez", "juan@example.com", 25, "password123"));
            listaUsuarios.Insertar(new Usuario(2, "Maria", "Lopez", "maria@example.com", 30, "password456"));
            listaUsuarios.Insertar(new Usuario(3, "Carlos", "Garcia", "carlos@example.com", 28, "password789"));
            listaUsuarios.Mostrar();
            /*
            Application.Init();
            Logiarse login = new Logiarse(listaUsuarios, listaAutos, ListaRepuestos);
            login.ShowAll();
            Application.Run();
            */

            ListaFacturas.Insertar(1, 1, 100);
            ListaFacturas.Insertar(2, 2, 200);
            ListaFacturas.Insertar(3, 3, 300);
            ListaFacturas.Insertar(4, 4, 400);
            ListaFacturas.Insertar(5, 5, 500);
            ListaFacturas.Insertar(6, 6, 600);
            ListaFacturas.Insertar(7, 7, 700);

            Console.WriteLine("Árbol B de orden 5 creado");

            string graphviz = ListaFacturas.GraficarGraphviz();

            // Mostrar elementos en orden
            Console.WriteLine("Recorrido INORDEN del arbol: ");
            ListaFacturas.RecorridoInOrden().ForEach(x => 
                Console.WriteLine($"ID: {x.ID}, ID Servicio: {x.ID_servicio}, Total: {x.Total}"));

            Console.WriteLine("\nÁrbol después de eliminar:");
            int id = 1;
            ListaFacturas.Elminar(id);
            string graphviz2 = ListaFacturas.GraficarGraphviz();

            Grafica.GenerarArchivoDot("arbol.dot", graphviz, "Árbol B de orden 5");
            Grafica.ConvertirDotAImagen("arbol.dot");
            Grafica.GenerarArchivoDot("arbol2.dot", graphviz2, "Árbol B de orden 5");
            Grafica.ConvertirDotAImagen("arbol2.dot");

            

            // Crear el árbol AVL y agregar algunos repuestos
            AVLTREErepues arbolRepuestos = new AVLTREErepues();
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
        
            // Generar y mostrar el gráfico Graphviz de la lista de usuarios
            string graphviz3= listaUsuarios.GenerarGraphviz();
            
            string graphviz4 = arbolRepuestos.GenerarGraphviz();
            Grafica.GenerarArchivoDot("repuestos.dot", graphviz4, "Árbol AVL de repuestos");
            Grafica.ConvertirDotAImagen("repuestos.dot");

            // Generar el archivo .dot y convertirlo a imagen
            Grafica.GenerarArchivoDot("lista_usuarios.dot", graphviz3, "Lista de Usuarios");
            Grafica.ConvertirDotAImagen("lista_usuarios.dot");

            
        }
    }
}