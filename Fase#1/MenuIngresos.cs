using Gtk;
using Usuarios;
using List;
using System;
using ListaDobleUnsafe;
using System.Threading.Tasks;
using Repuestos;
using ListaCircular;

public class MenuIngresos : Window
{
    private ListaDoblementeEnlazada listaAutos;
    private ListaSimple<Usuario> lista;
    private ListaCircular.ListaCircular listaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe MenuIngresos(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Menu de ingresos")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 500);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Menu de ingresos", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Ingresar Usuario");
        button1.Clicked += Usus1;
        button1.SetSizeRequest(100, 50);
        ApplyFont(button1, "Arial 12");
        fixedContainer.Put(button1, 180, 100);

        Button button2 = new Button("Ingresar Vehículo");
        button2.Clicked += Vehí;
        button2.SetSizeRequest(100, 50);
        ApplyFont(button2, "Arial 12");
        fixedContainer.Put(button2, 178, 200);

        Button button3 = new Button("Repuestos");
        button3.Clicked += Repuestos;
        button3.SetSizeRequest(100, 50);
        ApplyFont(button3, "Arial 12");
        fixedContainer.Put(button3, 195, 300);

        Button button4 = new Button("Ingresar Servicios");
        button4.Clicked += IngresarServicios;
        button4.SetSizeRequest(100, 50);
        ApplyFont(button4, "12");
        fixedContainer.Put(button4, 175, 400);

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

    private unsafe void Usus1(object? sender, EventArgs e)
    {
        Individuals individuals = new Individuals(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        individuals.ShowAll();
    }

    private unsafe void Vehí(object? sender, EventArgs e)
    {
        Autos autos = new Autos(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        autos.ShowAll();
    }

    private unsafe void Repuestos(object? sender, EventArgs e)
    {
        Piezas piezas = new Piezas(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        piezas.ShowAll();
    }

    private unsafe void IngresarServicios(object? sender, EventArgs e)
    {
        Servir servir = new Servir(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        servir.ShowAll();
    }
}