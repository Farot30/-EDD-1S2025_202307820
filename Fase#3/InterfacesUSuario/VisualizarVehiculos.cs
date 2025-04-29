using Gtk;
using System;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;

namespace InterfacesUsuario.VisualizarVehiculos
{
    public class VisualizarVehiculo : Window
    {
        private Blockchain blockchainUsuarios; 
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ArbolMerklesFac ListaFacturas;
        private ListaDeListas grafoNodiri;
        private int IdUsuario;
        private TreeView treeView;
        private ListStore listStore;
        public VisualizarVehiculo(Blockchain BlockchainUsuarios, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo, int idUsuario) : base("Visualizar Vehículo")
        {
            blockchainUsuarios = BlockchainUsuarios;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;
            IdUsuario = idUsuario;

            SetDefaultSize(600, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Vehículos del Usuario", "Arial 20");
            fixeds.Put(label, 190, 20);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("Id", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Id Usuario", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Marca", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Modelo", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Placa", new CellRendererText(), "text", 4);

            listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(500, 250);
            fixeds.Put(scrolledWindow, 50, 60);

            // Cargar los vehículos del usuario
            CargarVehiculosUsuario();

            Add(fixeds);
        }

        private void CargarVehiculosUsuario()
        {
            // Obtener los vehículos asociados al usuario actual
            var vehiculos = ListaDobleVehiculos.BuscarVehiculosPorIdUsuario(IdUsuario);

            // Agregar los vehículos al TreeView
            foreach (var vehiculo in vehiculos)
            {
                listStore.AppendValues(
                    vehiculo.Id.ToString(),
                    vehiculo.IdUsuario.ToString(),
                    vehiculo.Marca,
                    vehiculo.Modelo.ToString(),
                    vehiculo.Placa
                );
            }

            // Mostrar mensaje si no hay vehículos
            if (listStore.IterNChildren() == 0)
            {
                MostrarMensaje("No existen vehículos asociados a este usuario.");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje))
            {
                md.Run();
                md.Destroy();
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