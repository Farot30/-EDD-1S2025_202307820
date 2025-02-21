using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;

public class Menus : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    public unsafe Menus(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos) : base("Menu del usuario")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;

        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Menu del usuario", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargas Masivas");
        button1.Clicked += Cargas;
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 150, 100);

        Button button2 = new Button("Ingresos individuales");
        button2.Clicked += Ingresos;
        button2.SetSizeRequest(100, 50);
        ApplyFont(button2, "Arial 20");
        fixedContainer.Put(button2, 125, 200);

        Button button3 = new Button("Gestión de Usuarios");
        button3.Clicked += Gestiones;
        button3.SetSizeRequest(100, 50);
        ApplyFont(button3, "Arial 20");
        fixedContainer.Put(button3, 130, 300);

        Button button4 = new Button("Generar Servicio");
        button4.Clicked += Generaciones;
        button4.SetSizeRequest(100, 50);
        ApplyFont(button4, "Arial 20");
        fixedContainer.Put(button4, 150, 400);

        Button button5 = new Button("Cancelar Factura");
        button5.Clicked += Cancelaciones;
        button5.SetSizeRequest(100, 50);
        ApplyFont(button5, "Arial 20");
        fixedContainer.Put(button5, 150, 500);

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
    }

    private unsafe void Cargas(object? sender, EventArgs e)
    {
        Console.WriteLine("Cargas Masivas");
        Masivo mas = new Masivo(lista, listaAutos);
        mas.ShowAll();
        this.Hide();
    }

    private unsafe void Ingresos(object? sender, EventArgs e)
    {
        Console.WriteLine("Ingreso individual");
        MenuIngresos individup = new MenuIngresos(lista, listaAutos);
        individup.ShowAll();
        this.Hide();
    }

    private unsafe void Gestiones(object? sender, EventArgs e)
    {
        Console.WriteLine("Gestión de Usuarios");
        Gestion gest = new Gestion(lista, listaAutos);
        gest.ShowAll();
        this.Hide();
    }

    private unsafe void Generaciones(object? sender, EventArgs e)
    {
        Console.WriteLine("Generar Servicio");
        Servir serv = new Servir(lista, listaAutos);
        serv.ShowAll();
        this.Hide();
    }

    private unsafe void Cancelaciones(object? sender, EventArgs e)
    {
        Console.WriteLine("Cancelar Factura");
        Factur fact = new Factur(lista, listaAutos);
        fact.ShowAll();
        this.Hide();
    }
}