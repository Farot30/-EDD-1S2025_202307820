using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using Facturas;
using Matriz;

public class Factur : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;
    private MatrizDispersa<int> matriz;

    private Label ids2;
    private Label ID_ORDEN2;
    private Label Total2;

    public unsafe Factur(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila, MatrizDispersa<int> matrizDispersa) : base("Facturación")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;
        this.matriz = matrizDispersa;

        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = CreateStyledLabel("Facturación", "Arial 20");
        fixedContainer.Put(labelesBox, 200, 50);

        Label ids1 = CreateStyledLabel("ID:", "Arial 12");
        fixedContainer.Put(ids1, 100, 150);

        ids2 = CreateStyledLabel("Aquí va el id:", "Arial 12");
        fixedContainer.Put(ids2, 250, 150);

        Label ID_ORDEN = CreateStyledLabel("Id_Orden:", "Arial 12");
        fixedContainer.Put(ID_ORDEN, 100, 250);

        ID_ORDEN2 = CreateStyledLabel("Aquí va el Id_Orden", "Arial 12");
        fixedContainer.Put(ID_ORDEN2, 250, 250);

        Label Total = CreateStyledLabel("TOTAL:", "Arial 12");
        fixedContainer.Put(Total, 100, 350);

        Total2 = CreateStyledLabel("Aquí va el total", "Arial 12");
        fixedContainer.Put(Total2, 250, 350);

        Add(fixedContainer);

        // Obtener la última factura de la pila y actualizar los labels
        ActualizarLabelsConUltimaFactura();
    }

    private Label CreateStyledLabel(string text, string font)
    {
        Label label = new Label(text);
        ApplyFont(label, font);
        return label;
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
            ids2.Text = "No hay servicio hecho";
            ID_ORDEN2.Text = "No hay servicio hecho";
            Total2.Text = "No hay servicio hecho";
        }
    }
}