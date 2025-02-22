using Gtk;
using System;
using List;
using Usuarios;
using ListaDobleUnsafe;

class Program{

    static unsafe void Main(){
        Application.Init();
        ListaSimple<Usuario> listaUsuarios = new ListaSimple<Usuario>();
        ListaDoblementeEnlazada listaAutos = new ListaDoblementeEnlazada();
        ListaCircular.ListaCircular ListasPiezas = new ListaCircular.ListaCircular();
        InicioS Patata = new InicioS(listaUsuarios, listaAutos, ListasPiezas);
        Patata.ShowAll();
        Application.Run();
    }
}