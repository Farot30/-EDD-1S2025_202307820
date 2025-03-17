using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using System;
using System.Collections.Generic;
using InterfacesUsuario.GenerarVehiculo;
using InterfacesUsuario.VisualizarServicio;
using InterfacesUsuario.VisualizarFactura;
using InterfacesUsuario.PagarFacturas;

namespace InterfacesUsuario.MenuUsuarios
{
    public class MenuUsuario : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Button Insertar_vehiculo;
        private Button Visuarlizar_Servicio;
        private Button Visualizar_Facturas;
        private Button Cancelar_Reporte;
        public MenuUsuario(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto) : base("Menu de Usuarios")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 400);

            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Menu de Usuarios", "Arial 20");
            fixedcontainer.Put(Titulo, 100, 50);

            Insertar_vehiculo = new Button("Insertar Vehiculo");
            Insertar_vehiculo.SetSizeRequest(150, 50);
            Insertar_vehiculo.Clicked += InsertarVehiculo;
            ApplyFont(Insertar_vehiculo, "Arial 12");
            fixedcontainer.Put(Insertar_vehiculo, 120, 100);

            Visuarlizar_Servicio = new Button("Visualizar Servicio");
            Visuarlizar_Servicio.SetSizeRequest(150, 50);
            Visuarlizar_Servicio.Clicked += Visualizar_servicio;
            ApplyFont(Visuarlizar_Servicio, "Arial 12");
            fixedcontainer.Put(Visuarlizar_Servicio, 110, 160);

            Visualizar_Facturas = new Button("Visualizar Facturas");
            Visualizar_Facturas.SetSizeRequest(150, 50);
            Visualizar_Facturas.Clicked += Visualizar_facturas;
            ApplyFont(Visualizar_Facturas, "Arial 12");
            fixedcontainer.Put(Visualizar_Facturas, 105, 220);

            Cancelar_Reporte = new Button("Cancelar Reporte");
            Cancelar_Reporte.SetSizeRequest(150, 50);
            Cancelar_Reporte.Clicked += Cancelar_reporte;
            ApplyFont(Cancelar_Reporte, "Arial 12");
            fixedcontainer.Put(Cancelar_Reporte, 120, 280);

            Add(fixedcontainer);
        }

        private void InsertarVehiculo(object? sender, EventArgs e)
        {
            InsertarVehiuclo insertarVehiuclo = new InsertarVehiuclo(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto);
            insertarVehiuclo.DeleteEvent += delegate { insertarVehiuclo.Destroy(); };
            insertarVehiuclo.ShowAll();
        }

        private void Visualizar_servicio(object? sender, EventArgs e)
        {
            VerServicio verServicio = new VerServicio(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto);
            verServicio.DeleteEvent += delegate { verServicio.Destroy(); };
            verServicio.ShowAll();
        }

        private void Visualizar_facturas(object? sender, EventArgs e)
        {
            Visualizar_Facturas visualizarFactura = new Visualizar_Facturas(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto);
            visualizarFactura.DeleteEvent += delegate { visualizarFactura.Destroy(); };
            visualizarFactura.ShowAll();
        }

        private void Cancelar_reporte(object? sender, EventArgs e)
        {
            PagarFactura pagarFactura = new PagarFactura(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto);
            pagarFactura.DeleteEvent += delegate { pagarFactura.Destroy(); };
            pagarFactura.ShowAll();
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