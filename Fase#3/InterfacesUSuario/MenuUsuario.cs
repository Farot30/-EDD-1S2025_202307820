using Gtk;
using System;
using Logiarse;
using Clases.ControlLogeo;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using InterfacesUsuario.VisualizarVehiculos;
using InterfacesUsuario.VisualizarServicios;
using InterfacesUsuario.VisualizarFacturas;
using Programa;

namespace InterfacesUsuario.MenuUsuario
{
    
    public class MenuUsuarios : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ListaDeListas grafoNodiri;
        private Button VisualizarVehiculo;
        private Button Visualizar_Servicio;
        private Button Visualizar_Facturas;
        private Button Cerrar_Sesion;
        private int IdUsuario;
        public MenuUsuarios(Blockchain BlockchainUsuarios, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ListaDeListas Grafo, int idUsuario) : base("Menu de Usuario")
        {
            blockchainUsuarios = BlockchainUsuarios;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            grafoNodiri = Grafo;
            IdUsuario = idUsuario;

            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Menu de Usuario", "Arial 20");
            fixeds.Put(label, 100, 50);

            VisualizarVehiculo = new Button("Visualizar Vehiculo");
            VisualizarVehiculo.SetSizeRequest(200, 30);
            VisualizarVehiculo.Clicked += VisualizarVehiculo_Clicked;
            ApplyFont(VisualizarVehiculo, "Arial 12");
            fixeds.Put(VisualizarVehiculo, 100, 100);

            Visualizar_Servicio = new Button("Visualizar Servicio");
            Visualizar_Servicio.SetSizeRequest(200, 30);
            Visualizar_Servicio.Clicked += VisualizarServicio_Clicked;
            ApplyFont(Visualizar_Servicio, "Arial 12");
            fixeds.Put(Visualizar_Servicio, 100, 140);

            Visualizar_Facturas = new Button("Visualizar Facturas");
            Visualizar_Facturas.SetSizeRequest(200, 30);
            Visualizar_Facturas.Clicked += VisualizarFacturas_Clicked;
            ApplyFont(Visualizar_Facturas, "Arial 12");
            fixeds.Put(Visualizar_Facturas, 100, 180);

            Cerrar_Sesion = new Button("Cerrar Sesion");
            Cerrar_Sesion.SetSizeRequest(200, 30);
            Cerrar_Sesion.Clicked += CerrarSesion_Clicked;
            ApplyFont(Cerrar_Sesion, "Arial 12");
            fixeds.Put(Cerrar_Sesion, 100, 220);

            Add(fixeds);
        }

        private void VisualizarVehiculo_Clicked(object? sender, EventArgs e)
        {
            VisualizarVehiculo visualizarVehiculos = new VisualizarVehiculo(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri, IdUsuario);
            visualizarVehiculos.ShowAll();
        }
        private void VisualizarServicio_Clicked(object? sender, EventArgs e)
        {
            VisualizarServicio visualizarServicios = new VisualizarServicio(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri, IdUsuario);
            visualizarServicios.ShowAll();
        }
        private void VisualizarFacturas_Clicked(object? sender, EventArgs e)
        {
            VisualizarFactura visualizarFacturas = new VisualizarFactura(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri, IdUsuario);
            visualizarFacturas.ShowAll();
        }
        private void CerrarSesion_Clicked(object? sender, EventArgs e)
        {
            // Obtener el correo del usuario actual
            string? correoUsuario = blockchainUsuarios.BuscarUsuarioPorId(IdUsuario)?.Correo;

            if (!string.IsNullOrEmpty(correoUsuario))
            {
                // Buscar el registro del usuario en LogUsuarios
                LogUsuario? log = MainClass.LogUsuarios.LastOrDefault(l => l.Usuario == correoUsuario && l.Salida == null);

                if (log != null)
                {
                    // Registrar la hora de salida
                    log.Salida = DateTime.Now;
                }

                // Exportar los logs a un archivo JSON
                ControlLogeo.ExportarLogUsuarios(MainClass.LogUsuarios);

                // Redirigir al login
                Logiarses logiarses = new Logiarses(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
                logiarses.ShowAll();
                this.Hide();
            }
            else
            {
                // Mostrar un mensaje de error si no se encuentra el usuario
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Error: No se pudo encontrar el usuario actual."))
                {
                    md.Run();
                    md.Destroy();
                }
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
    
