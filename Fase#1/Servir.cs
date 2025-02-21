using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using System.Threading.Tasks;

public class Servir : Window
{
    private Entry Idss;
    private Entry ID_respuesto;
    private Entry ID_Vehículo;
    private Entry Detalles;
    private Entry Costo;

    private ListaDoblementeEnlazada listaAutos;
    private ListaSimple<Usuario> lista;

    public unsafe Servir(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos) : base("Servicios")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Servicios", "Arial 20");
        fixedContainer.Put(labelesBox, 140, 50);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids1, 100, 150);

        Idss = new Entry();
        ApplyFont(Idss, "Arial 12");
        fixedContainer.Put(Idss, 260, 150);

        EventBox id_respuesto = CreateStyledLabel("ID Repuesto:", "Arial 20");
        fixedContainer.Put(id_respuesto, 100, 200);

        ID_respuesto = new Entry();
        ApplyFont(ID_respuesto, "Arial 12");
        fixedContainer.Put(ID_respuesto, 260, 200);

        EventBox id_vehículo = CreateStyledLabel("ID Vehículo:", "Arial 20");
        fixedContainer.Put(id_vehículo, 100, 250);

        ID_Vehículo = new Entry();
        ApplyFont(ID_Vehículo, "Arial 12");
        fixedContainer.Put(ID_Vehículo, 260, 250);

        EventBox detalles = CreateStyledLabel("Detalles:", "Arial 20");
        fixedContainer.Put(detalles, 100, 300);

        Detalles = new Entry();
        ApplyFont(Detalles, "Arial 12");
        fixedContainer.Put(Detalles, 260, 300);

        EventBox costo = CreateStyledLabel("Costo:", "Arial 20");
        fixedContainer.Put(costo, 100, 350);
        
        Costo = new Entry();
        ApplyFont(Costo, "Arial 12");
        fixedContainer.Put(Costo, 260, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 100, 400);

        Button Regreso = new Button("Regresar");
        Regreso.Clicked += Regresar;
        Regreso.SetSizeRequest(100, 50);
        ApplyFont(Regreso, "Arial 20");
        fixedContainer.Put(Regreso, 260, 400);

        Add(fixedContainer);
    }

    private unsafe void OnShowButtonClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Idss.Text) || 
            string.IsNullOrEmpty(ID_respuesto.Text) || 
            string.IsNullOrEmpty(ID_Vehículo.Text) || 
            string.IsNullOrEmpty(Detalles.Text) || 
            string.IsNullOrEmpty(Costo.Text))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Favor de llenar todos los campos"))
            {
                md1.Run();
                md1.Destroy();
            }
        }
        else
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardando"))
            {
                Console.WriteLine("Guardando");
                md.Run();
                md.Destroy();
            }
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