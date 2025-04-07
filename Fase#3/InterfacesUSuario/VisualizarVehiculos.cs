using Gtk;
using System;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;

namespace InterfacesUsuario.VisualizarVehiculos
{
    public class VisualizarVehiculo : Window
    {
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ListaDeListas grafoNodiri;
        private TreeView treeView;
        private ListStore listStore;
        public VisualizarVehiculo(ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Visualizar Vehículo")
        {
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Visualizar Vehículo", "Arial 20");
            fixeds.Put(label, 50, 50);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("Id", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Id_Usuario", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Marca", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Modelo", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Placa", new CellRendererText(), "text", 4);


            listStore = new ListStore(typeof(string), typeof(string), typeof(double));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(300, 275);
            fixeds.Put(scrolledWindow, 50, 100);

            Add(fixeds);
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
            else if (widget is Entry entry)
            {
                entry.ModifyFont(fontDesc);
            }
        }
    }
}