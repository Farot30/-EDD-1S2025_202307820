using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using System.Threading.Tasks;
using Repuestos;
using ListaCircular;

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

    public unsafe Piezas(ListaSimple<Usuario> lista, ListaDoblementeEnlazada ListaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Ingreso de Repuestos")
    {
        this.listaAutos = ListaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 480);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Ingreso de Repuestos");
        fixedContainer.Put(labelesBox, 160, 50);

        Label ids1 = new Label("ID:");
        fixedContainer.Put(ids1, 100, 150);

        IDs1 = new Entry();
        fixedContainer.Put(IDs1, 250, 150);

        Label repuesto = new Label("Repuesto:");
        fixedContainer.Put(repuesto, 100, 200);

        Repuesto = new Entry();
        fixedContainer.Put(Repuesto, 250, 200);

        Label descripcion = new Label("Descripción:");
        fixedContainer.Put(descripcion, 100, 250);
        
        Descripcion = new Entry();
        fixedContainer.Put(Descripcion, 250, 250);

        Label costo = new Label("Costo:");
        fixedContainer.Put(costo, 100, 300);

        Costo = new Entry();
        fixedContainer.Put(Costo, 250, 300);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        fixedContainer.Put(button, 100, 350);

        Add(fixedContainer);
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
                    }
                    return;
                }

                listaRepuestos.Insertar(id, Repuesto.Text, Descripcion.Text, costo);
                using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardado"))
                {
                    md1.Run();
                }
                listaRepuestos.Mostrar();
            }
            else
            {
                using (MessageDialog md3 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Favor de llenar los campos con el tipo de dato correcto"))
                {
                    md3.Run();
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