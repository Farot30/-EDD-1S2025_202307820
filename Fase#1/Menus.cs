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

        EventBox labelesBox = CreateStyledLabel("Menu del usuario", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargas Masivas");
        button1.Clicked += Cargas;
        button1.SetSizeRequest(100, 50);
        ApplyFont(button1, "Arial 12");
        fixedContainer.Put(button1, 180, 100);

        Button button2 = new Button("Ingresos individuales");
        button2.Clicked += Ingresos;
        button2.SetSizeRequest(100, 50);
        ApplyFont(button2, "Arial 12");
        fixedContainer.Put(button2, 170, 170);

        Button button3 = new Button("Gestión de Usuarios");
        button3.Clicked += Gestiones;
        button3.SetSizeRequest(100, 50);
        ApplyFont(button3, "Arial 12");
        fixedContainer.Put(button3, 170, 240);

        Button button4 = new Button("Generar Servicio");
        button4.Clicked += Generaciones;
        button4.SetSizeRequest(100, 50);
        ApplyFont(button4, "Arial 12");
        fixedContainer.Put(button4, 180, 310);

        Button button5 = new Button("Cancelar Factura");
        button5.Clicked += Cancelaciones;
        button5.SetSizeRequest(100, 50);
        ApplyFont(button5, "Arial 12");
        fixedContainer.Put(button5, 180, 380);

        Button Reportes = new Button("Reportes");
        Reportes.Clicked += Reportes1;
        Reportes.SetSizeRequest(100, 50);
        ApplyFont(Reportes, "Arial 12");
        fixedContainer.Put(Reportes, 210, 450);

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

    private unsafe void Cargas(object? sender, EventArgs e)
    {
        Masivo mas = new Masivo(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        mas.ShowAll();
    }

    private unsafe void Ingresos(object? sender, EventArgs e)
    {
        MenuIngresos individup = new MenuIngresos(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        individup.ShowAll();
    }

    private unsafe void Gestiones(object? sender, EventArgs e)
    {
        Gestion gest = new Gestion(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        gest.ShowAll();
    }

    private unsafe void Generaciones(object? sender, EventArgs e)
    {
        Servir serv = new Servir(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        serv.ShowAll();
    }

    private unsafe void Cancelaciones(object? sender, EventArgs e)
    {
        Factur fact = new Factur(lista, listaAutos, listaRepuestos, colas, pilaFacturas);
        fact.ShowAll();
    }

    private unsafe void Reportes1(object? sender, EventArgs e)
    {
        // Generar y guardar las gráficas
        GenerarYGuardarGrafica("ListaSimple.dot", lista.GenerarGraphviz(), "Lista Simple");
        GenerarYGuardarGrafica("ListaDoblementeEnlazada.dot", listaAutos.GenerarGraphviz(), "Lista Doblemente Enlazada");
        GenerarYGuardarGrafica("ListaCircular.dot", listaRepuestos.GenerarGraphviz(), "Lista Circular");
        GenerarYGuardarGrafica("Cola.dot", colas.GenerarGraphviz(), "Cola");
        GenerarYGuardarGrafica("Pila.dot", pilaFacturas.GenerarGrafica(), "Pila");
        GenerarYGuardarGrafica("Top5Vehículos.dot", listaAutos.GenerarGraphvizTop5Antiguos(), "Top 5 Vehículos más antiguos");
        GenerarYGuardarGrafica("Top5VehiculosmasServicio.dot", colas.GenerarGraphvizTop5VehiculosConMasServicios(), "Top 5 vehículos con más servicios");

        // Convertir los archivos .dot a imágenes
        ConvertirDotAImagen("ListaSimple.dot");
        ConvertirDotAImagen("ListaDoblementeEnlazada.dot");
        ConvertirDotAImagen("ListaCircular.dot");
        ConvertirDotAImagen("Cola.dot");
        ConvertirDotAImagen("Pila.dot");
        ConvertirDotAImagen("Top5Vehículos.dot");
        ConvertirDotAImagen("Top5VehiculosmasServicio.dot");

        Console.WriteLine("Reportes generados y guardados.");
    }

    private void GenerarYGuardarGrafica(string nombreArchivo, string contenido, string titulo)
    {
        Grafica.Grafica.GenerarArchivoDot(nombreArchivo, contenido, titulo);
    }

    private void ConvertirDotAImagen(string nombreArchivo)
    {
        Grafica.Grafica.ConvertirDotAImagen(nombreArchivo);
    }
}