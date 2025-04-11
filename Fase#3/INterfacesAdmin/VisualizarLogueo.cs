using Gtk;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using Programa;

namespace INterfacesAdmin.VisualizarLogueo
{
    public class Visualizacion_Logueo : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ListaDeListas grafoNodiri;

        private TreeView treeView;
        private ListStore listStore;
        public Visualizacion_Logueo(Blockchain BlockcahinUsuarios, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Visualización de Logueo")
        {
            blockchainUsuarios = BlockcahinUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Visualización de Logueo", "Arial 20");
            fixeds.Put(label, 50, 20);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("Usuario", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Entrada", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Salida", new CellRendererText(), "text", 2);

            listStore = new ListStore(typeof(string), typeof(string), typeof(string));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(500, 300);
            fixeds.Put(scrolledWindow, 50, 60);

            Add(fixeds);

            // Cargar los datos de logueo
            CargarDatosLogueo();
        }
        private void CargarDatosLogueo()
        {
            listStore.Clear();
            foreach (var log in MainClass.LogUsuarios)
            {
                listStore.AppendValues(log.Usuario, log.Entrada.ToString("yyyy-MM-dd HH:mm:ss.00"), log.Salida?.ToString("yyyy-MM-dd HH:mm:ss.00") ?? "N/A");
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
        }
        
    }   

}