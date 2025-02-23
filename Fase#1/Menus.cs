using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using Repuestos;
using ListaCircular;

public class Menus : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular listaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe Menus(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Menu del usuario")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Menu del usuario");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargas Masivas");
        button1.Clicked += Cargas;
        fixedContainer.Put(button1, 150, 100);

        Button button2 = new Button("Ingresos individuales");
        button2.Clicked += Ingresos;
        button2.SetSizeRequest(100, 50);
        fixedContainer.Put(button2, 125, 200);

        Button button3 = new Button("Gestión de Usuarios");
        button3.Clicked += Gestiones;
        button3.SetSizeRequest(100, 50);
        fixedContainer.Put(button3, 130, 300);

        Button button4 = new Button("Generar Servicio");
        button4.Clicked += Generaciones;
        button4.SetSizeRequest(100, 50);
        fixedContainer.Put(button4, 150, 400);

        Button button5 = new Button("Cancelar Factura");
        button5.Clicked += Cancelaciones;
        button5.SetSizeRequest(100, 50);
        fixedContainer.Put(button5, 150, 500);

        Add(fixedContainer);
    }

    private unsafe void Cargas(object? sender, EventArgs e)
    {
        Masivo mas = new Masivo(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        mas.ShowAll();
        this.Hide();
    }

    private unsafe void Ingresos(object? sender, EventArgs e)
    {
        MenuIngresos individup = new MenuIngresos(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        individup.ShowAll();
        this.Hide();
    }

    private unsafe void Gestiones(object? sender, EventArgs e)
    {
        Gestion gest = new Gestion(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        gest.ShowAll();
        this.Hide();
    }

    private unsafe void Generaciones(object? sender, EventArgs e)
    {
        Servir serv = new Servir(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        serv.ShowAll();
        this.Hide();
    }

    private unsafe void Cancelaciones(object? sender, EventArgs e)
    {
        Factur fact = new Factur(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        fact.ShowAll();
        this.Hide();
    }
}