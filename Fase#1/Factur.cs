using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using Facturas;

public class Factur : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    private Label ids2;
    private Label ID_ORDEN2;
    private Label Total2;

    public unsafe Factur(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Facturación")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Facturación");
        fixedContainer.Put(labelesBox, 200, 50);

        Label ids1 = new Label("ID:");
        fixedContainer.Put(ids1, 100, 150);

        ids2 = new Label("Aquí va el id:");
        fixedContainer.Put(ids2, 250, 150);

        Label ID_ORDEN = new Label("Id_Orden:");
        fixedContainer.Put(ID_ORDEN, 100, 250);

        ID_ORDEN2 = new Label("Aquí va el Id_Orden");
        fixedContainer.Put(ID_ORDEN2, 250, 250);

        Label Total = new Label("TOTAL:");
        fixedContainer.Put(Total, 100, 350);

        Total2 = new Label("Aquí va el total");
        fixedContainer.Put(Total2, 250, 350);

        Add(fixedContainer);

        // Obtener la última factura de la pila y actualizar los labels
        ActualizarLabelsConUltimaFactura();
    }

    private unsafe void ActualizarLabelsConUltimaFactura()
    {
        Facturas.Facturas* ultimaFactura = pilaFacturas.ObtenerUltimaFactura();
        if (ultimaFactura != null)
        {
            ids2.Text = ultimaFactura->ID.ToString();
            ID_ORDEN2.Text = ultimaFactura->ID_Orden.ToString();
            Total2.Text = ultimaFactura->Total.ToString("C");
        }
        else
        {
            ids2.Text = "No hay servicio hecho ";
            ID_ORDEN2.Text = "No hay servicio hecho";
            Total2.Text = "No hay servicio hecho";
        }
    }
}