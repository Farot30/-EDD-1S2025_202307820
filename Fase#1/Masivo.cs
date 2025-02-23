using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using ListaCircular;
using Repuestos;
using Newtonsoft.Json;
using System.IO;
using System;

public class Masivo : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe Masivo(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Cargas masivas")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Cargas masivas");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargar");
        button1.Clicked += Cargaras;
        fixedContainer.Put(button1, 200, 250);

        Button button2 = new Button("Regresar");
        button2.Clicked += Regresar;
        fixedContainer.Put(button2, 200, 300);

        Add(fixedContainer);
    }

    private unsafe void Cargaras(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Aquí se cargará el archivo en teoría");
        Console.WriteLine("Se debe de cargar un archivo");
        md.Run();
        md.Destroy();
        /*FileChooserDialog fileChooser = new FileChooserDialog("Seleccione el archivo a cargar", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
        fileChooser.Run();
        fileChooser.Destroy();*/
    }

    private unsafe void Regresar(object? sender, EventArgs e)
    {
        Menus menus = new Menus(lista, listaAutos, ListaRepuestos, colas, pilaFacturas);
        menus.ShowAll();
        this.Destroy();
    }
}