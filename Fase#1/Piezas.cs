using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using System.Threading.Tasks;

public class Piezas : Window
{
    private Entry IDs1;
    private Entry Repuesto;
    private Entry Descripcion;
    private Entry Costo;

    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;

    public unsafe Piezas(ListaSimple<Usuario> lista, ListaDoblementeEnlazada ListaAutos) : base("Ingreso de Repuestos")
    {
        this.listaAutos = ListaAutos;
        this.lista = lista;
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Repuestos", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids1, 100, 150);

        IDs1 = new Entry();
        ApplyFont(IDs1, "Arial 12");
        fixedContainer.Put(IDs1, 250, 150);

        EventBox repuesto = CreateStyledLabel("Repuesto:", "Arial 20");
        fixedContainer.Put(repuesto, 100, 200);

        Repuesto = new Entry();
        ApplyFont(Repuesto, "Arial 12");
        fixedContainer.Put(Repuesto, 250, 200);

        EventBox descripcion = CreateStyledLabel("Descripción:", "Arial 20");
        fixedContainer.Put(descripcion, 100, 250);
        
        Descripcion = new Entry();
        ApplyFont(Descripcion, "Arial 12");
        fixedContainer.Put(Descripcion, 250, 250);

        EventBox costo = CreateStyledLabel("Costo:", "Arial 20");
        fixedContainer.Put(costo, 100, 300);

        Costo = new Entry();
        ApplyFont(Costo, "Arial 12");
        fixedContainer.Put(Costo, 250, 300);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 100, 350);

        Button Regreso = new Button("Regresar");
        Regreso.Clicked += Regresar;
        Regreso.SetSizeRequest(100, 50);
        ApplyFont(Regreso, "Arial 20");
        fixedContainer.Put(Regreso, 250, 350);

        Add(fixedContainer);
    }

    private unsafe void OnShowButtonClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(IDs1.Text) || 
            string.IsNullOrEmpty(Repuesto.Text) || 
            string.IsNullOrEmpty(Descripcion.Text) || 
            string.IsNullOrEmpty(Costo.Text))
        {
            MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Favor de llenar todos los campos");
            md1.Run();
            md1.Destroy();
        }
        else
        {
            MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardado");
            md1.Run();
            md1.Destroy();
        }
    }

    private unsafe void Regresar(object? sender, EventArgs e)
    {
        Menus menus = new Menus(lista, listaAutos);
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