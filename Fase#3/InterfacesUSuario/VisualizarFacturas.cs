using Gtk;
using System;

using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
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
        private ListaDeListas grafoNodiri;
        private int IdUsuario;

        private TreeView treeView;
        private ListStore listStore;

        public VisualizarFactura(Blockchain BlockchainUsuarios, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ListaDeListas Grafo, int idUsuario) : base("Visualizar Facturas")
        {
            blockchainUsuarios = BlockchainUsuarios;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
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
            scrolledWindow.SetSizeRequest(300, 350);
            fixeds.Put(scrolledWindow, 50, 100);

            Add(fixeds);
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