using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using System.Threading.Tasks;
using Repuestos;
using ListaCircular;
using Matriz;

public class Piezas : Window
{
    private Entry IDs1;
    private Entry Repuesto;
    private Entry Descripcion;
    private Entry Costo;

    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular listaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;
    private MatrizDispersa<int> matriz;

    public unsafe Piezas(ListaSimple<Usuario> lista, ListaDoblementeEnlazada ListaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila, MatrizDispersa<int> matrizDispersa) : base("Ingreso de Repuestos")
    {
        this.listaAutos = ListaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;
        this.matriz = matrizDispersa;

        SetDefaultSize(500, 480);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Repuestos", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 12");
        fixedContainer.Put(ids1, 100, 150);

        IDs1 = new Entry();
        ApplyFont(IDs1, "Arial 12");
        fixedContainer.Put(IDs1, 250, 150);

        EventBox repuesto = CreateStyledLabel("Repuesto:", "Arial 12");
        fixedContainer.Put(repuesto, 100, 200);

        Repuesto = new Entry();
        ApplyFont(Repuesto, "Arial 12");
        fixedContainer.Put(Repuesto, 250, 200);

        EventBox descripcion = CreateStyledLabel("Descripción:", "Arial 12");
        fixedContainer.Put(descripcion, 100, 250);
        
        Descripcion = new Entry();
        ApplyFont(Descripcion, "Arial 12");
        fixedContainer.Put(Descripcion, 250, 250);

        EventBox costo = CreateStyledLabel("Costo:", "Arial 12");
        fixedContainer.Put(costo, 100, 300);

        Costo = new Entry();
        ApplyFont(Costo, "Arial 12");
        fixedContainer.Put(Costo, 250, 300);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 12");
        fixedContainer.Put(button, 100, 350);

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

    private unsafe void OnShowButtonClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(IDs1.Text) || 
            string.IsNullOrEmpty(Repuesto.Text) || 
            string.IsNullOrEmpty(Descripcion.Text) || 
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
            if(int.TryParse(IDs1.Text, out int id) && 
                double.TryParse(Costo.Text, out double costo))
            {
                // Verificar si el ID ya existe en la lista de repuestos
                if (IdExists(id))
                {
                    using (MessageDialog md2 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID ya existe. Por favor, ingrese otro ID."))
                    {
                        md2.Run();
                        md2.Destroy();
                    }
                    return;
                }

                listaRepuestos.Insertar(id, Repuesto.Text, Descripcion.Text, costo);
                using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardado"))
                {
                    md1.Run();
                    md1.Destroy();
                }
                listaRepuestos.Mostrar();
            }
            else
            {
                using (MessageDialog md3 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Favor de llenar los campos con el tipo de dato correcto"))
                {
                    md3.Run();
                    md3.Destroy();
                }
            }
        }
    }

    private unsafe bool IdExists(int id)
    {
        Repuesto* actual = listaRepuestos.Head;
        if (actual == null) return false;

        do
        {
            if (actual->Id == id)
            {
                return true;
            }
            actual = actual->Next;
        } while (actual != listaRepuestos.Head);

        return false;
    }
}