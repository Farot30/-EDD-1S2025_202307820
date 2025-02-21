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
        InicioS Patata = new InicioS(listaUsuarios, listaAutos);
        Patata.ShowAll();
        Application.Run();
    }
}