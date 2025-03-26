using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;
using System;
using System.Collections.Generic;
using InterfacesUsuario.GenerarVehiculo;
using InterfacesUsuario.VisualizarServicio;
using InterfacesUsuario.VisualizarFactura;
using InterfacesUsuario.PagarFacturas;
using logiarse;
using Clases;
using Program;
using CLases.ControlLogeo;

namespace InterfacesUsuario.MenuUsuarios
{
    public class MenuUsuario : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        private Button Insertar_vehiculo;
        private Button Visuarlizar_Servicio;
        private Button Visualizar_Facturas;
        private Button Cancelar_Reporte;
        private Button Cerrar_Sesion;
        private string correoUsuario;

        public MenuUsuario(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, B5Facturas ListaFactura, string correo) : base("Menu de Usuarios")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;
            correoUsuario = correo;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(425, 400);

            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel(correoUsuario, "Arial 20");
            fixedcontainer.Put(Titulo, 100, 50);

            Insertar_vehiculo = new Button("Insertar Vehiculo");
            Insertar_vehiculo.SetSizeRequest(150, 50);
            Insertar_vehiculo.Clicked += InsertarVehiculo;
            ApplyFont(Insertar_vehiculo, "Arial 12");
            fixedcontainer.Put(Insertar_vehiculo, 120, 100);

            Visuarlizar_Servicio = new Button("Visualizar Servicio");
            Visuarlizar_Servicio.SetSizeRequest(155, 50);
            Visuarlizar_Servicio.Clicked += Visualizar_servicio;
            ApplyFont(Visuarlizar_Servicio, "Arial 12");
            fixedcontainer.Put(Visuarlizar_Servicio, 110, 160);

            Visualizar_Facturas = new Button("Visualizar Facturas");
            Visualizar_Facturas.SetSizeRequest(160, 50);
            Visualizar_Facturas.Clicked += Visualizar_facturas;
            ApplyFont(Visualizar_Facturas, "Arial 12");
            fixedcontainer.Put(Visualizar_Facturas, 105, 220);

            Cancelar_Reporte = new Button("Cancelar Factura");
            Cancelar_Reporte.SetSizeRequest(150, 50);
            Cancelar_Reporte.Clicked += Cancelar_reporte;
            ApplyFont(Cancelar_Reporte, "Arial 12");
            fixedcontainer.Put(Cancelar_Reporte, 120, 280);

            Cerrar_Sesion = new Button("Cerrar Sesion");
            Cerrar_Sesion.SetSizeRequest(155, 50);
            Cerrar_Sesion.Clicked += CerrarSesion;
            ApplyFont(Cerrar_Sesion, "Arial 12");
            fixedcontainer.Put(Cerrar_Sesion, 120, 340);

            Add(fixedcontainer);
        }

        private void InsertarVehiculo(object? sender, EventArgs e)
        {
            Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioCorreo(correoUsuario);
            if (usuario != null)
            {
                InsertarVehiuclo insertarVehiuclo = new InsertarVehiuclo(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas, usuario.Value.Id);
                insertarVehiuclo.DeleteEvent += delegate { insertarVehiuclo.Destroy(); };
                insertarVehiuclo.ShowAll();
            }
            else
            {
                MostrarMensaje("Usuario no encontrado.");
            }
        }

        private void Visualizar_servicio(object? sender, EventArgs e)
        {
            Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioCorreo(correoUsuario);
            if(usuario != null){
                VerServicio verServicio = new VerServicio(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas, usuario.Value.Id);
                verServicio.DeleteEvent += delegate { verServicio.Destroy(); };
                verServicio.ShowAll();
            }
        }

        private void Visualizar_facturas(object? sender, EventArgs e)
        {
            Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioCorreo(correoUsuario);
            if(usuario != null){
                Visualizar_Facturas visualizarFactura = new Visualizar_Facturas(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas, usuario.Value.Id);
                visualizarFactura.DeleteEvent += delegate { visualizarFactura.Destroy(); };
                visualizarFactura.ShowAll();
            }
        }

        private void Cancelar_reporte(object? sender, EventArgs e)
        {
            PagarFactura pagarFactura = new PagarFactura(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas, correoUsuario);
            pagarFactura.DeleteEvent += delegate { pagarFactura.Destroy(); };
            pagarFactura.ShowAll();
        }

        private void CerrarSesion(object? sender, EventArgs e)
        {
            LogUsuario? logUsuario = MainClass.LogUsuarios.Find(log => log.Usuario == correoUsuario && log.Salida == null);
            if (logUsuario != null)
            {
                logUsuario.Salida = DateTime.Now;
            }
            Logiarse login = new Logiarse(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas);
            login.ShowAll();
            this.Hide();
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

        private void MostrarMensaje(string mensaje)
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje))
            {
                md.Run();
                md.Destroy();
            }
        }
    }
}