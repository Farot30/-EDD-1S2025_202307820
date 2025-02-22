using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
public class Factur : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;

    public unsafe Factur (ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas) : base ("Facturación"){
        
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Facturación", "Arial 20");
        fixedContainer.Put(labelesBox, 200, 50);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids1, 100, 150);
        
        EventBox ids2 = CreateStyledLabel("Aquí va el id:", "Arial 20");
        fixedContainer.Put(ids2, 250, 150);

        EventBox ID_ORDEN = CreateStyledLabel("Id_Orden:", "Arial 20");
        fixedContainer.Put(ID_ORDEN, 100, 250);

        EventBox ID_ORDEN2 = CreateStyledLabel("Aquí va el Id_Orden", "Arial 20");
        fixedContainer.Put(ID_ORDEN2, 250, 250);

        EventBox Total = CreateStyledLabel("TOTAL:", "Arial 20");
        fixedContainer.Put(Total, 100, 350);

        EventBox Total2 = CreateStyledLabel("Aquí va el total", "Arial 20");
        fixedContainer.Put(Total2, 250, 350);

        Button button = new Button("Regresar");
        button.Clicked += Regresar;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 190, 400);

        Add(fixedContainer);
    }

    private unsafe void Regresar(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Regresando");
        Console.WriteLine("Regresando");
        md.Run();
        md.Destroy();
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