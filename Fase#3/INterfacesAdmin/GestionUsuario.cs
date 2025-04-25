using Gtk;
using System;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;
using INterfacesAdmin.AgregarUsuarios;
using INterfacesAdmin.VerUsuario;

namespace INterfacesAdmin.GesionUsuarios
{
    public class GestionUsuario : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ArbolMerklesFac ListaFacturas;
        private ListaDeListas grafoNodiri;
        private Button btnAgregarUsuario;
        private Button VisualizarUsuario;
        public GestionUsuario(Blockchain BlockchainUsuarios,ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo) : base("Gestion de Usuarios")
        {
            blockchainUsuarios = BlockchainUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Gestión de Usuarios", "Arial 20");
            fixeds.Put(label, 85, 50);

            btnAgregarUsuario = new Button("Agregar Usuario");
            btnAgregarUsuario.SetSizeRequest(200, 30);
            btnAgregarUsuario.Clicked += AgregarUsuario;
            ApplyFont(btnAgregarUsuario, "Arial 12");
            fixeds.Put(btnAgregarUsuario, 90, 100);

            VisualizarUsuario = new Button("Visualizar Usuarios");
            VisualizarUsuario.SetSizeRequest(200, 30);
            VisualizarUsuario.Clicked += VisualizarUsuarios;
            ApplyFont(VisualizarUsuario, "Arial 12");
            fixeds.Put(VisualizarUsuario, 90, 140);

            Add(fixeds);

        }

        private void AgregarUsuario(object sender, EventArgs e)
        {
            AgregarUsuario agregarUsuario = new AgregarUsuario(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, ListaFacturas, grafoNodiri);
            agregarUsuario.ShowAll();
        }

        private void VisualizarUsuarios(object sender, EventArgs e)
        {
            VerUsuarios verUsuario = new VerUsuarios(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, ListaFacturas, grafoNodiri);
            verUsuario.ShowAll();
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