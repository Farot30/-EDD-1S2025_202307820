using Gtk;
using Clases.Repuesto;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;
using Nodos.NodoAVLRepus;

namespace INterfacesAdmin.VisualizarRepuesto
{
    public class VisualizarRepuestos : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ArbolMerklesFac ListaFacturas;
        private ListaDeListas grafoNodiri;
        private Button PRE_Orden;
        private Button IN_Orden;
        private Button POST_Orden;
        private TreeView treeView;
        private ListStore listStore;
        public VisualizarRepuestos(Blockchain BlockchainUsuarios, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo) : base("Visualizar Repuestos")
        {
            blockchainUsuarios = BlockchainUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;
            
            SetDefaultSize(425, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Visualizar Repuestos", "Arial 20");
            fixeds.Put(label, 100, 25);

            PRE_Orden = new Button("Pre-Orden");
            PRE_Orden.Clicked += PreOrden;
            PRE_Orden.SetSizeRequest(100, 50);
            ApplyFont(PRE_Orden, "Arial 12");
            fixeds.Put(PRE_Orden, 50, 70);

            IN_Orden = new Button("In-Orden");
            IN_Orden.Clicked += InOrden;
            IN_Orden.SetSizeRequest(100, 50);
            ApplyFont(IN_Orden, "Arial 12");
            fixeds.Put(IN_Orden, 170, 70);

            POST_Orden = new Button("Post-Orden");
            POST_Orden.Clicked += PostOrden;
            POST_Orden.SetSizeRequest(100, 50);
            ApplyFont(POST_Orden, "Arial 12");
            fixeds.Put(POST_Orden, 290, 70);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Repuesto", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Detalles", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Costo", new CellRendererText(), "text", 3);

            listStore = new ListStore(typeof(int), typeof(string), typeof(string), typeof(double));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(350, 200);
            fixeds.Put(scrolledWindow, 50, 150);

            Add(fixeds);
        }

        private void PreOrden(object? sender, System.EventArgs e)
        {
            listStore.Clear();
            Preorden(avlTreeRepuestos.raiz);
        }

        private void InOrden(object? sender, System.EventArgs e)
        {
            listStore.Clear();
            Inorden(avlTreeRepuestos.raiz);
        }

        private void PostOrden(object? sender, System.EventArgs e)
        {
            listStore.Clear();
            Postorden(avlTreeRepuestos.raiz);
        }

        private void Preorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                listStore.AppendValues(nodo.val.Id, nodo.val.Nombre, nodo.val.Descripcion, Math.Round(nodo.val.Precio, 2));
                Preorden(nodo.izq);
                Preorden(nodo.der);
            }
        }

        private void Inorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Inorden(nodo.izq);
                listStore.AppendValues(nodo.val.Id, nodo.val.Nombre, nodo.val.Descripcion, Math.Round(nodo.val.Precio, 2));
                Inorden(nodo.der);
            }
        }

        private void Postorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Postorden(nodo.izq);
                Postorden(nodo.der);
                listStore.AppendValues(nodo.val.Id, nodo.val.Nombre, nodo.val.Descripcion, Math.Round(nodo.val.Precio, 2));
            }
        }

        private EventBox CreateStyledLabel(string text, string font)
        {
            Label label = new Label(text);
            ApplyFont(label, font);
            EventBox eventBox = new EventBox();
            eventBox.Add(label);
            return eventBox;
        }

        private  void ApplyFont(Widget widget, string fontDescription)
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
    
}