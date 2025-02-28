using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using System.Threading.Tasks;
using Cola;
using Servicios;
using ListaCircular;
using Repuestos;
using Vehiculos;
using Matriz;

public class Servir : Window
{
    private Entry Idss;
    private Entry ID_respuesto;
    private Entry ID_Vehículo;
    private Entry Detalles;
    private Entry Costo;

    private ListaDoblementeEnlazada listaAutos;
    private ListaSimple<Usuario> lista;
    private ListaCircular.ListaCircular listaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;
    private MatrizDispersa<int> matriz; // Agregar la instancia de MatrizDispersa

    public unsafe Servir(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola ListaCola, Pila.Pila pila, Matriz.MatrizDispersa<int> matriz) : base("Servicios")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = ListaCola;
        this.pilaFacturas = pila;
        this.matriz = matriz; // Inicializar la instancia de MatrizDispersa

        this.SetDefaultSize(500, 550);
        this.SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Servicios", "Arial 20");
        fixedContainer.Put(labelesBox, 10, 10);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 12");
        Idss = new Entry();
        ApplyFont(Idss, "Arial 12");
        fixedContainer.Put(ids1, 10, 50);
        fixedContainer.Put(Idss, 100, 50);

        EventBox id_respuesto = CreateStyledLabel("ID Repuesto:", "Arial 12");
        ID_respuesto = new Entry();
        ApplyFont(ID_respuesto, "Arial 12");
        fixedContainer.Put(id_respuesto, 10, 90);
        fixedContainer.Put(ID_respuesto, 100, 90);

        EventBox id_vehículo = CreateStyledLabel("ID Vehículo:", "Arial 12");
        ID_Vehículo = new Entry();
        ApplyFont(ID_Vehículo, "Arial 12");
        fixedContainer.Put(id_vehículo, 10, 130);
        fixedContainer.Put(ID_Vehículo, 100, 130);

        EventBox detalles = CreateStyledLabel("Detalles:", "Arial 12");
        Detalles = new Entry();
        ApplyFont(Detalles, "Arial 12");
        fixedContainer.Put(detalles, 10, 170);
        fixedContainer.Put(Detalles, 100, 170);

        EventBox costo = CreateStyledLabel("Costo:", "Arial 12");
        Costo = new Entry();
        ApplyFont(Costo, "Arial 12");
        fixedContainer.Put(costo, 10, 210);
        fixedContainer.Put(Costo, 100, 210);

        Button button = new Button("Guardar");
        button.Clicked += Guardando;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 12");
        fixedContainer.Put(button, 10, 250);

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

    private unsafe void Guardando(object? sender, EventArgs e)
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
            return;
        }

        if (!int.TryParse(Idss.Text, out int idServicio) ||
            !int.TryParse(ID_respuesto.Text, out int idRepuesto) ||
            !int.TryParse(ID_Vehículo.Text, out int idVehiculo) ||
            !float.TryParse(Costo.Text, out float costo))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Favor de ingresar valores válidos en los campos de ID y Costo"))
            {
                md1.Run();
                md1.Destroy();
            }
            return;
        }

        if (ServicioExiste(idServicio))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "El servicio ya existe"))
            {
                md1.Run();
                md1.Destroy();
            }
            return;
        }

        if (!RepuestoExiste(idRepuesto))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "El repuesto no existe"))
            {
                md1.Run();
                md1.Destroy();
            }
            return;
        }

        if (!VehiculoExiste(idVehiculo))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "El vehículo no existe"))
            {
                md1.Run();
                md1.Destroy();
            }
            return;
        }

        colas.Encolar(idServicio, idRepuesto, idVehiculo, Detalles.Text, costo);
        colas.Mostrar();

        string detalle = new string(Detalles.Text);
        matriz.insert(idVehiculo, idRepuesto, detalle); // Usar la instancia de MatrizDispersa
        Console.WriteLine("Matriz de repuestos y vehículos");
        matriz.mostrar();
        // Crear una factura y almacenarla en la pila
        int o = 1;
        double total = costo + ObtenerCostoRepuesto(idRepuesto);
        pilaFacturas.Apila(idServicio, o, total);
        pilaFacturas.Mostrar();
        o ++;

        using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Servicio guardado y factura generada"))
        {
            md.Run();
            md.Destroy();
        }
    }

    private unsafe double ObtenerCostoRepuesto(int idRepuesto)
    {
        Repuesto* actual = listaRepuestos.Head;
        while (actual != null)
        {
            if (actual->Id == idRepuesto)
            {
                return actual->Precio;
            }
            actual = actual->Next;
        }
        return 0;
    }

    private unsafe bool ServicioExiste(int id)
    {
        Servicio* actual = colas.Frente;
        while (actual != null)
        {
            if (actual->Id == id)
            {
                return true;
            }
            actual = actual->Next;
        }
        return false;
    }

    private unsafe bool RepuestoExiste(int id)
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

    private unsafe bool VehiculoExiste(int id)
    {
        Vehiculos.Node* actual = listaAutos.Head;
        while (actual != null)
        {
            if (actual->Id == id)
            {
                return true;
            }
            actual = actual->Next;
        }
        return false;
    }

    private unsafe string ObtenerDetalles(Servicio* servicio)
    {
        char[] detalles = new char[50];
        for (int i = 0; i < 50 && servicio->Detalles[i] != '\0'; i++)
        {
            detalles[i] = servicio->Detalles[i];
        }
        return new string(detalles).TrimEnd('\0');
    }
}