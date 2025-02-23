using Gtk;
using List;
using System;
using Usuarios;
using ListaDobleUnsafe;

public class InicioS : Window
{
    private Entry Usuarios;
    private Entry Contrasenas;
    private ListaDoblementeEnlazada listaAutos;
    private ListaSimple<Usuario> lista;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe InicioS(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Interfaz Inicio")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;
        
        SetDefaultSize(500, 500);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Inicio de sesión");
        fixedContainer.Put(labelesBox, 170, 50);

        Label SUS = new Label("Usuario:");
        fixedContainer.Put(SUS, 100, 150);

        Usuarios = new Entry();
        fixedContainer.Put(Usuarios, 250, 150);

        Label Contras = new Label("Contraseña:");
        fixedContainer.Put(Contras, 100, 250);

        Contrasenas = new Entry();
        Contrasenas.Visibility = false;
        fixedContainer.Put(Contrasenas, 250, 250);

        Button button = new Button("Iniciar sesión");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50); // Aumenta el tamaño del botón
        fixedContainer.Put(button, 170, 350);

        Add(fixedContainer);
    }

    private unsafe void OnShowButtonClicked(object? sender, System.EventArgs e)
    {
        if (Usuarios.Text == "root@gmail.com" && Contrasenas.Text == "root123")
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Inicio de sesión exitoso"))
            {
                Console.WriteLine("Inicio de sesión exitoso");
                md.Run();
            }
            Menus menus = new Menus(lista, listaAutos, ListaRepuestos, colas, pilaFacturas);
            menus.ShowAll();
            this.Destroy();
        }
        else
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Datos errones carnal"))
            {
                md1.Run();
            }
        }
    }
}