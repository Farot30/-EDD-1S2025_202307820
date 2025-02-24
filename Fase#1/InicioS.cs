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


        EventBox labelesBox = CreateStyledLabel("Inicio de Sesión", "Arial 20");
        fixedContainer.Put(labelesBox, 170, 50);


        EventBox SUS = CreateStyledLabel("Usuario:", "Arial 12");
        fixedContainer.Put(SUS, 100, 150);

        Usuarios = new Entry();
        ApplyFont(Usuarios, "Arial 12");
        fixedContainer.Put(Usuarios, 250, 150);

        EventBox Contras = CreateStyledLabel("Contraseña:", "Arial 12");
        fixedContainer.Put(Contras, 100, 250);

        Contrasenas = new Entry();
        ApplyFont(Contrasenas, "Arial 12");
        Contrasenas.Visibility = false;
        fixedContainer.Put(Contrasenas, 250, 250);

        Button button = new Button("Iniciar sesión");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50); // Aumenta el tamaño del botón
        ApplyFont(button, "Arial 12");
        fixedContainer.Put(button, 170, 350);

        Add(fixedContainer);
    }


    private EventBox CreateStyledLabel(string text, string font)
    {
        Label label = new Label(text);
        ApplyFont(label, font);
        EventBox eventBox = new EventBox();
        eventBox.Add(label);
        return eventBox;
    }

    private void ApplyFont(Widget widget, string fontDescription)
    {
        Pango.FontDescription fontDesc = Pango.FontDescription.FromString(fontDescription);
        if (widget is Label label)
        {
            label.ModifyFont(fontDesc);
        }
        else if (widget is Button button)
        {
            button.Child.ModifyFont(fontDesc);
        }
        else if (widget is Entry entry)
        {
            entry.ModifyFont(fontDesc);
        }
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