using Gtk;
using CLases.Usuarios;
using CLases.Repuesto;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using System.Collections.Generic;
using Nodos.NodoAVLRepus;

namespace InterfacesAdmin.VisualizarRepuestos
{
    public class VisualizarRepuesto : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Button PRE_Orden;
        private Button IN_Orden;
        private Button POST_Orden;
        private TreeView treeView;
        private ListStore listStore;

        public VisualizarRepuesto(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto) : base("Visualización de Repuestos")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(425, 400);
            Fixed fixedcontainer = new Fixed();
            
            EventBox Titulo = CreateStyledLabel("Visualización de Repuestos", "Arial 20");
            fixedcontainer.Put(Titulo, 50, 20);

            PRE_Orden = new Button("Pre-Orden");
            PRE_Orden.Clicked += PreOrden;
            PRE_Orden.SetSizeRequest(100, 50);
            ApplyFont(PRE_Orden, "Arial 12");
            fixedcontainer.Put(PRE_Orden, 50, 70);

            IN_Orden = new Button("In-Orden");
            IN_Orden.Clicked += InOrden;
            IN_Orden.SetSizeRequest(100, 50);
            ApplyFont(IN_Orden, "Arial 12");
            fixedcontainer.Put(IN_Orden, 170, 70);

            POST_Orden = new Button("Post-Orden");
            POST_Orden.Clicked += PostOrden;
            POST_Orden.SetSizeRequest(100, 50);
            ApplyFont(POST_Orden, "Arial 12");
            fixedcontainer.Put(POST_Orden, 290, 70);

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
            fixedcontainer.Put(scrolledWindow, 50, 150);

            Add(fixedcontainer);
        }

        private void PreOrden(object? sender, System.EventArgs e)
        {
            listStore.Clear();
            Preorden(listaRepuesto.raiz);
        }

        private void InOrden(object? sender, System.EventArgs e)
        {
            listStore.Clear();
            Inorden(listaRepuesto.raiz);
        }

        private void PostOrden(object? sender, System.EventArgs e)
        {
            listStore.Clear();
            Postorden(listaRepuesto.raiz);
        }

        private void Preorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                listStore.AppendValues(nodo.val.Id, nodo.val.Nombre, nodo.val.Descripcion, nodo.val.Precio);
                Preorden(nodo.izq);
                Preorden(nodo.der);
            }
        }

        private void Inorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Inorden(nodo.izq);
                listStore.AppendValues(nodo.val.Id, nodo.val.Nombre, nodo.val.Descripcion, nodo.val.Precio);
                Inorden(nodo.der);
            }
        }

        private void Postorden(NodeAVLRepu? nodo)
        {
            if (nodo != null)
            {
                Postorden(nodo.izq);
                Postorden(nodo.der);
                listStore.AppendValues(nodo.val.Id, nodo.val.Nombre, nodo.val.Descripcion, nodo.val.Precio);
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