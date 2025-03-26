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
using Listas.BinarioTree;
using CLases.Servicios;
using CLases.ControlLogeo;

namespace Program
{
    class MainClass
    {
        public static List<LogUsuario> LogUsuarios = new List<LogUsuario>();
        static void Main()
        {
            ListaUsuarioSimple<Usuario> listaUsuarios = new ListaUsuarioSimple<Usuario>();
            ListaDobleVehiculo listaAutos = new ListaDobleVehiculo();
            AVLTREErepues ListaRepuestos = new AVLTREErepues();
            B5Facturas ListaFacturas = new B5Facturas();
            Binariostree ListaServicios = new Binariostree();

            /*listaUsuarios.Insertar(new Usuario(1, "Juan", "Perez", "juan@example.com", 25, "password123"));
            listaUsuarios.Insertar(new Usuario(2, "Maria", "Lopez", "maria@example.com", 30, "password456"));
            listaUsuarios.Insertar(new Usuario(3, "Carlos", "Garcia", "carlos@example.com", 28, "password789"));
            listaUsuarios.Mostrar();*/
            
            Application.Init();
            Logiarse login = new Logiarse(listaUsuarios, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            login.ShowAll();
            Application.Run();
            
/*
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
            
            ListaRepuestos.Add(new Repuesto(1, "Filtro de aceite", "Filtro para motor", 15.99));
            ListaRepuestos.Add(new Repuesto(2, "Bujía", "Bujía de encendido", 5.99));
            ListaRepuestos.Add(new Repuesto(3, "Pastillas de freno", "Juego de pastillas de freno", 45.99));
            ListaRepuestos.Add(new Repuesto(4, "Aceite de motor", "Aceite sintético", 29.99));
            ListaRepuestos.Add(new Repuesto(5, "Batería", "Batería de coche", 89.99));
            ListaRepuestos.Add(new Repuesto(6, "Neumático", "Neumático de repuesto", 120.00));
            ListaRepuestos.Add(new Repuesto(7, "Amortiguador", "Amortiguador delantero", 75.50));
            ListaRepuestos.Add(new Repuesto(8, "Radiador", "Radiador de coche", 150.00));
            ListaRepuestos.Add(new Repuesto(9, "Alternador", "Alternador de coche", 200.00));
            ListaRepuestos.Add(new Repuesto(10, "Faro", "Faro delantero", 60.00));

            // Mostrar los repuestos en orden
            Console.WriteLine("Repuestos en preorden:");
            ListaRepuestos.Preorden(ListaRepuestos.raiz);
            Console.WriteLine();

            Console.WriteLine("Repuestos en inorden:");
            ListaRepuestos.Inorden(ListaRepuestos.raiz);
            Console.WriteLine();

            Console.WriteLine("Repuestos en postorden:");
            ListaRepuestos.Postorden(ListaRepuestos.raiz);
            Console.WriteLine();
        
            // Generar y mostrar el gráfico Graphviz de la lista de usuarios
            string graphviz3= listaUsuarios.GenerarGraphviz();
            
            string graphviz4 = ListaRepuestos.GenerarGraphviz();
            Grafica.GenerarArchivoDot("repuestos.dot", graphviz4, "Árbol AVL de repuestos");
            Grafica.ConvertirDotAImagen("repuestos.dot");

            // Generar el archivo .dot y convertirlo a imagen
            Grafica.GenerarArchivoDot("lista_usuarios.dot", graphviz3, "Lista de Usuarios");
            Grafica.ConvertirDotAImagen("lista_usuarios.dot");

            ListaServicios.Insertar(new Servicio(3, 103, 203, "Cambio de amortiguadores", 200.00));
            ListaServicios.Insertar(new Servicio(5, 105, 205, "Cambio de aceite", 50.00));
            ListaServicios.Insertar(new Servicio(8, 108, 208, "Cambio de alternador", 300.00));
            ListaServicios.Insertar(new Servicio(6, 106, 206, "Cambio de pastillas de freno", 75.00));
            ListaServicios.Insertar(new Servicio(1, 101, 201, "Cambio de neumáticos", 100.00));
            ListaServicios.Insertar(new Servicio(2, 102, 202, "Cambio de batería", 150.00));
            ListaServicios.Insertar(new Servicio(4, 104, 204, "Cambio de radiador", 250.00));
            ListaServicios.Insertar(new Servicio(9, 109, 209, "Cambio de filtro de aceite", 20.00));  
            ListaServicios.Insertar(new Servicio(7, 107, 207, "Cambio de faros", 80.00));
            
            
            

            // Generar el archivo Graphviz
            string graphviz5 = ListaServicios.GenerarGraphviz();
            Grafica.GenerarArchivoDot("servicios.dot", graphviz5, "Árbol Binario de Servicios");
            Grafica.ConvertirDotAImagen("servicios.dot");
*/

            
        }
    }
}