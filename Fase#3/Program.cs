using System;
using Gtk;
using Clases.ControlLogeo;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using ListasArboles.ArbolMerkles;
using Logiarse;

namespace Programa
{
    class MainClass
    {
        public static List<LogUsuario> LogUsuarios = new List<LogUsuario>();
        static void Main()
        {
            ArbolMerklesFac arbolMerkle = new ArbolMerklesFac();
            Blockchain blockChain = new Blockchain();
            ListaDobleVehiculo listaVehiculos = new ListaDobleVehiculo();
            AVLTREErepues avlTreeRepuestos = new AVLTREErepues();
            Binariostree binarioServicio = new Binariostree();
            ListaDeListas grafoNodiri = new ListaDeListas();

            Application.Init();
            Logiarses login = new Logiarses(blockChain, listaVehiculos, avlTreeRepuestos, binarioServicio, arbolMerkle, grafoNodiri);
            login.ShowAll();
            Application.Run();
        }
    }
}