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

    public unsafe Servir(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola ListaCola, Pila.Pila pila) : base("Servicios")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = ListaCola;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 550);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Ingreso de Servicios");
        fixedContainer.Put(labelesBox, 180, 50);

        Label ids1 = new Label("ID:");
        fixedContainer.Put(ids1, 100, 150);

        Idss = new Entry();
        fixedContainer.Put(Idss, 260, 150);

        Label id_respuesto = new Label("ID Repuesto:");
        fixedContainer.Put(id_respuesto, 100, 200);

        ID_respuesto = new Entry();
        fixedContainer.Put(ID_respuesto, 260, 200);

        Label id_vehículo = new Label("ID Vehículo:");
        fixedContainer.Put(id_vehículo, 100, 250);

        ID_Vehículo = new Entry();
        fixedContainer.Put(ID_Vehículo, 260, 250);

        Label detalles = new Label("Detalles:");
        fixedContainer.Put(detalles, 100, 300);

        Detalles = new Entry();
        fixedContainer.Put(Detalles, 260, 300);

        Label costo = new Label("Costo:");
        fixedContainer.Put(costo, 100, 350);
        
        Costo = new Entry();
        fixedContainer.Put(Costo, 260, 350);

        Button button = new Button("Guardar");
        button.Clicked += Guardando;
        button.SetSizeRequest(100, 50);
        fixedContainer.Put(button, 100, 400);

        Add(fixedContainer);
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

        // Crear una factura y almacenarla en la pila
        double total = costo + ObtenerCostoRepuesto(idRepuesto);
        pilaFacturas.Apila(idServicio, idServicio, total);
        pilaFacturas.Mostrar();

        using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Servicio guardado y factura generada"))
        {
            md.Run();
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