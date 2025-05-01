using Gtk;
using System;

using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;
using System.Diagnostics;

namespace InterfacesUsuario.VisualizarFacturas
{
    public class VisualizarFactura : Window
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

        public VisualizarFactura(Blockchain BlockchainUsuarios, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo, int idUsuario) : base("Visualizar Facturas")
        {
            blockchainUsuarios = BlockchainUsuarios;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;
            IdUsuario = idUsuario;

            SetDefaultSize(425, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Visualizar Facturas", "Arial 20");
            fixeds.Put(label, 120, 50);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Id_Servicio", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Total", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Fecha", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Metodo_de_Pago", new CellRendererText(), "text", 4);

            listStore = new ListStore(typeof(string), typeof(string), typeof(double));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(365, 310);
            fixeds.Put(scrolledWindow, 50, 100);

            Add(fixeds);

            // Llenar el TreeView con las facturas del usuario logueado
            CargarFacturasUsuario();
        }

        private void CargarFacturasUsuario()
        {
            // Limpiar el ListStore
            listStore.Clear();

            // Recorrer las facturas en el árbol de Merkle
            foreach (var nodo in ListaFacturas.ObtenerNodosHojas())
            {
                if (nodo.Factura != null)
                {
                    // Obtener el ID del servicio de la factura
                    int idServicio = nodo.Factura.Value.ID_servicio;

                    // Buscar el servicio en la lista de servicios
                    var servicio = ListaServicios.BuscarServicioPorId(idServicio);

                    if (servicio != null)
                    {
                        // Obtener el ID del vehículo asociado al servicio
                        int idVehiculo = servicio.Value.ID_vehiculo;

                        // Buscar el vehículo en la lista de vehículos
                        var vehiculo = ListaDobleVehiculos.BuscarVehiculoPorId(idVehiculo);

                        if (vehiculo != null && vehiculo.Value.IdUsuario == IdUsuario)
                        {
                            // Si el vehículo pertenece al usuario logueado, agregar la factura al TreeView
                            listStore.AppendValues(
                                nodo.Factura.Value.ID.ToString(),
                                nodo.Factura.Value.ID_servicio.ToString(),
                                nodo.Factura.Value.Total,
                                nodo.Factura.Value.Fecha,
                                nodo.Factura.Value.Metodo_pago
                            );
                        }
                    }
                }
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

        private void ApplyFont(Widget widget, string fontDescription)
        {
            Pango.FontDescription fontDesc = Pango.FontDescription.FromString(fontDescription);
            if (widget is Label label)
            {
                label.ModifyFont(fontDesc);
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
    }
}