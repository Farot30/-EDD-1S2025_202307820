using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;
using System;
using System.Collections.Generic;

namespace InterfacesUsuario.VisualizarFactura
{
    public class Visualizar_Facturas : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        private TreeView treeView;
        private ListStore listStore;
        public Visualizar_Facturas(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, B5Facturas ListaFactura) : base("Visualizar Facturas")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 425);

            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Visualizar Facturas", "Arial 20");
            fixedcontainer.Put(Titulo, 90, 50);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Orden", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Total", new CellRendererText(), "text", 2);

            listStore = new ListStore(typeof(string), typeof(string), typeof(double));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(300, 275);
            fixedcontainer.Put(scrolledWindow, 50, 100);

            Add(fixedcontainer);
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