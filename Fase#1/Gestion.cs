using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
public class Gestion : Window
{
    private Entry ID_Vision;
    private Entry ID_Edisicón;
    private Entry ID_Eliminación;
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    public unsafe Gestion(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas) : base("Gestionar los Usuarios")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        SetDefaultSize(500, 375);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Gestionar los Usuarios", "Arial 20");
        fixedContainer.Put(labelesBox, 130, 50);

        Button id_vision = new Button("Ver Usuario");
        id_vision.Clicked += VerUsuario;
        id_vision.SetSizeRequest(100, 50);
        ApplyFont(id_vision, "Arial 20");
        fixedContainer.Put(id_vision, 260, 115);

        ID_Vision = new Entry();
        ApplyFont(ID_Vision, "Arial 12");
        fixedContainer.Put(ID_Vision, 90, 125);

        Button id_edición = new Button("Editar Usuario");
        id_edición.Clicked += EditarUsuario;
        id_edición.SetSizeRequest(100, 50);
        ApplyFont(id_edición, "Arial 20");
        fixedContainer.Put(id_edición, 260, 190);

        ID_Edisicón = new Entry();
        ApplyFont(ID_Edisicón, "Arial 12");
        fixedContainer.Put(ID_Edisicón, 90, 200);

        Button id_eliminación = new Button("Eliminar Usuario");
        id_eliminación.Clicked += EliminarUsuario;
        id_eliminación.SetSizeRequest(100, 50);
        ApplyFont(id_eliminación, "Arial 20");
        fixedContainer.Put(id_eliminación, 260, 265);

        ID_Eliminación = new Entry();
        ApplyFont(ID_Eliminación, "Arial 12");
        fixedContainer.Put(ID_Eliminación, 90, 275);

        Button button = new Button("Regresar");
        button.Clicked += Regresar;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 190, 325);

        Add(fixedContainer);
    }

    private unsafe void VerUsuario(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a ver usuario");
        Console.WriteLine("entrando a ver usuario");
        md.Run();
        md.Destroy();
    }

    private unsafe void EditarUsuario(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a editar usuario");
        Console.WriteLine("entrando a editar usuario");
        md.Run();
        md.Destroy();
    }

    private unsafe void EliminarUsuario(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a eliminar usuario");
        Console.WriteLine("entrando a eliminar usuario");
        md.Run();
        md.Destroy();
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
        else if (widget is Entry entry)
        {
            entry.ModifyFont(fontDesc);
        }
    }
}