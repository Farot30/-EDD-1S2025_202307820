using Gtk;
using System;
using List;
using Usuarios;
using ListaDobleUnsafe;

namespace ProgramaInicial
{
    class Program{

    static unsafe void Main(){
        Application.Init();
        Pila.Pila pilaFacturas = new Pila.Pila();
        Cola.Cola colas = new Cola.Cola();
        ListaSimple<Usuario> listaUsuarios = new ListaSimple<Usuario>();
        ListaDoblementeEnlazada listaAutos = new ListaDoblementeEnlazada();
        ListaCircular.ListaCircular ListasPiezas = new ListaCircular.ListaCircular();
        InicioS Patata = new InicioS(listaUsuarios, listaAutos, ListasPiezas, colas, pilaFacturas);
        Patata.ShowAll();
        Application.Run();
    }
}
    
}
