using System;
using Gtk;
using Clases.ControlLogeo;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using Logiarse;

namespace Programa
{
    class MainClass
    {
        public static List<LogUsuario> LogUsuarios = new List<LogUsuario>();
        static void Main()
        {
            ListaDobleVehiculo listaVehiculos = new ListaDobleVehiculo();
            AVLTREErepues avlTreeRepuestos = new AVLTREErepues();
            Binariostree binarioServicio = new Binariostree();
            ListaDeListas grafoNodiri = new ListaDeListas();

            Application.Init();
            Logiarses login = new Logiarses(listaVehiculos, avlTreeRepuestos, binarioServicio, grafoNodiri);
            login.ShowAll();

            
        }
    }
}