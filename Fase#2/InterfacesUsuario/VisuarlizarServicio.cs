using Gtk;
using CLases.Usuarios;
using CLases.Vehiculo;
using CLases.Servicios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;
using Nodos.NodoBinarios;
using System;

namespace InterfacesUsuario.VisualizarServicio
{
    public class VerServicio : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        private Button Pre_Orden;
        private Button In_Orden;
        private Button Post_Orden;
        private TreeView treeView;
        private ListStore listStore;
        private int idUsuario;

        public VerServicio(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, B5Facturas ListaFactura, int IDUsuario) : base("Visualizar Servicio")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;
            idUsuario = IDUsuario;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 400);

            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Visualizar Servicio", "Arial 20");
            fixedcontainer.Put(Titulo, 110, 50);

            Pre_Orden = new Button("Pre-Orden");
            Pre_Orden.Clicked += PreOrden;
            Pre_Orden.SetSizeRequest(100, 50);
            ApplyFont(Pre_Orden, "Arial 12");
            fixedcontainer.Put(Pre_Orden, 50, 100);

            In_Orden = new Button("In-Orden");
            In_Orden.Clicked += InOrden;
            In_Orden.SetSizeRequest(100, 50);
            ApplyFont(In_Orden, "Arial 12");
            fixedcontainer.Put(In_Orden, 170, 100);

            Post_Orden = new Button("Post-Orden");
            Post_Orden.Clicked += PostOrden;
            Post_Orden.SetSizeRequest(100, 50);
            ApplyFont(Post_Orden, "Arial 12");
            fixedcontainer.Put(Post_Orden, 290, 100);

            treeView = new TreeView();
            treeView.HeadersVisible = true;
            treeView.AppendColumn("ID", new CellRendererText(), "text", 0);
            treeView.AppendColumn("Repuesto", new CellRendererText(), "text", 1);
            treeView.AppendColumn("Vehiculo", new CellRendererText(), "text", 2);
            treeView.AppendColumn("Detalle", new CellRendererText(), "text", 3);
            treeView.AppendColumn("Costo", new CellRendererText(), "text", 4);

            listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(double));
            treeView.Model = listStore;

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(treeView);
            scrolledWindow.SetSizeRequest(350, 200);
            fixedcontainer.Put(scrolledWindow, 50, 150);

            Add(fixedcontainer);
        }

        private void PreOrden(object? sender, EventArgs e)
        {
            listStore.Clear();
            PreOrdenRecursivo(ListaServicios.raiz);
            if (listStore.IterNChildren() == 0)
            {
                MostrarMensaje("No existen servicios para el usuario.");
            }
        }

        private void PreOrdenRecursivo(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                Servicio servicio = nodo.servicios;
                Vehiculos? vehiculo = ListaDobleVehiculos.BuscarVehiculoPorId(servicio.ID_vehiculo);
                if (vehiculo != null)
                {
                    Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioPorId(vehiculo.Value.IdUsuario);
                    if (usuario != null && usuario.Value.Id == idUsuario)
                    {
                        listStore.AppendValues(servicio.ID.ToString(), servicio.ID_repuesto.ToString(), servicio.ID_vehiculo.ToString(), servicio.Detalle, servicio.Costo);
                    }
                }
                PreOrdenRecursivo(nodo.izquierdo);
                PreOrdenRecursivo(nodo.derecho);
            }
        }

        private void InOrden(object? sender, EventArgs e)
        {
            listStore.Clear();
            InOrdenRecursivo(ListaServicios.raiz);
            if (listStore.IterNChildren() == 0)
            {
                MostrarMensaje("No existen servicios para el usuario.");
            }
        }

        private void InOrdenRecursivo(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                InOrdenRecursivo(nodo.izquierdo);

                Servicio servicio = nodo.servicios;
                Vehiculos? vehiculo = ListaDobleVehiculos.BuscarVehiculoPorId(servicio.ID_vehiculo);
                if (vehiculo != null)
                {
                    Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioPorId(vehiculo.Value.IdUsuario);
                    if (usuario != null && usuario.Value.Id == idUsuario)
                    {
                        listStore.AppendValues(servicio.ID.ToString(), servicio.ID_repuesto.ToString(), servicio.ID_vehiculo.ToString(), servicio.Detalle, servicio.Costo);
                    }
                }

                InOrdenRecursivo(nodo.derecho);
            }
        }

        private void PostOrden(object? sender, EventArgs e)
        {
            listStore.Clear();
            PostOrdenRecursivo(ListaServicios.raiz);
            if (listStore.IterNChildren() == 0)
            {
                MostrarMensaje("No existen servicios para el usuario.");
            }
        }

        private void PostOrdenRecursivo(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                PostOrdenRecursivo(nodo.izquierdo);
                PostOrdenRecursivo(nodo.derecho);

                Servicio servicio = nodo.servicios;
                Vehiculos? vehiculo = ListaDobleVehiculos.BuscarVehiculoPorId(servicio.ID_vehiculo);
                if (vehiculo != null)
                {
                    Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioPorId(vehiculo.Value.IdUsuario);
                    if (usuario != null && usuario.Value.Id == idUsuario)
                    {
                        listStore.AppendValues(servicio.ID.ToString(), servicio.ID_repuesto.ToString(), servicio.ID_vehiculo.ToString(), servicio.Detalle, servicio.Costo);
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