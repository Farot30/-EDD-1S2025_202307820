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

        SetDefaultSize(500, 550);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 10);

        Label labelesBox = new Label("Ingreso de Servicios");
        vbox.PackStart(labelesBox, false, false, 10);

        HBox hbox1 = new HBox(false, 10);
        Label ids1 = new Label("ID:");
        Idss = new Entry();
        hbox1.PackStart(ids1, false, false, 10);
        hbox1.PackStart(Idss, false, false, 10);
        vbox.PackStart(hbox1, false, false, 10);

        HBox hbox2 = new HBox(false, 10);
        Label id_respuesto = new Label("ID Repuesto:");
        ID_respuesto = new Entry();
        hbox2.PackStart(id_respuesto, false, false, 10);
        hbox2.PackStart(ID_respuesto, false, false, 10);
        vbox.PackStart(hbox2, false, false, 10);

        HBox hbox3 = new HBox(false, 10);
        Label id_vehículo = new Label("ID Vehículo:");
        ID_Vehículo = new Entry();
        hbox3.PackStart(id_vehículo, false, false, 10);
        hbox3.PackStart(ID_Vehículo, false, false, 10);
        vbox.PackStart(hbox3, false, false, 10);

        HBox hbox4 = new HBox(false, 10);
        Label detalles = new Label("Detalles:");
        Detalles = new Entry();
        hbox4.PackStart(detalles, false, false, 10);
        hbox4.PackStart(Detalles, false, false, 10);
        vbox.PackStart(hbox4, false, false, 10);

        HBox hbox5 = new HBox(false, 10);
        Label costo = new Label("Costo:");
        Costo = new Entry();
        hbox5.PackStart(costo, false, false, 10);
        hbox5.PackStart(Costo, false, false, 10);
        vbox.PackStart(hbox5, false, false, 10);

        Button button = new Button("Guardar");
        button.Clicked += Guardando;
        button.SetSizeRequest(100, 50);
        vbox.PackStart(button, false, false, 10);

        Add(vbox);
        ShowAll();
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
            }
            return;
        }

        if (ServicioExiste(idServicio))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "El servicio ya existe"))
            {
                md1.Run();
            }
            return;
        }

        if (!RepuestoExiste(idRepuesto))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "El repuesto no existe"))
            {
                md1.Run();
            }
            return;
        }

        if (!VehiculoExiste(idVehiculo))
        {
            using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "El vehículo no existe"))
            {
                md1.Run();
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
        double total = costo + ObtenerCostoRepuesto(idRepuesto);
        pilaFacturas.Apila(idServicio, idServicio, total);
        pilaFacturas.Mostrar();

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