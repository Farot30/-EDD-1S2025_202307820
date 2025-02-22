using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using ListaCircular;
using Repuestos;

public class Masivo : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    public unsafe Masivo(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas) : base("Cargas masivas"){
        
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Cargas masivas", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargar");
        button1.Clicked += Cargaras;
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 200, 250);

        Button button2 = new Button("Regresar");
        button2.Clicked += Regresar;
        ApplyFont(button2, "Arial 20");
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
        Menus menus = new Menus(lista, listaAutos, ListaRepuestos);
        menus.ShowAll();
        this.Hide();
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
}