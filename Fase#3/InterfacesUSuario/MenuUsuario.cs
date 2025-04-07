using Gtk;
using System;
using Logiarse;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using InterfacesUsuario.VisualizarVehiculos;
using InterfacesUsuario.VisualizarServicios;
using InterfacesUsuario.VisualizarFacturas;

namespace InterfacesUsuario.MenuUsuario
{
    
    public class MenuUsuarios : Window
    {
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ListaDeListas grafoNodiri;
        private Button VisualizarVehiculo;
        private Button Visualizar_Servicio;
        private Button Visualizar_Facturas;
        private Button Cerrar_Sesion;
        public MenuUsuarios(ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Menu de Usuario")
        {
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            grafoNodiri = Grafo;

            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Menu de Usuario", "Arial 20");
            fixeds.Put(label, 50, 50);

            VisualizarVehiculo = new Button("Visualizar Vehiculo");
            VisualizarVehiculo.SetSizeRequest(200, 30);
            VisualizarVehiculo.Clicked += VisualizarVehiculo_Clicked;
            ApplyFont(VisualizarVehiculo, "Arial 12");
            fixeds.Put(VisualizarVehiculo, 50, 100);

            Visualizar_Servicio = new Button("Visualizar Servicio");
            Visualizar_Servicio.SetSizeRequest(200, 30);
            Visualizar_Servicio.Clicked += VisualizarServicio_Clicked;
            ApplyFont(Visualizar_Servicio, "Arial 12");
            fixeds.Put(Visualizar_Servicio, 50, 140);

            Visualizar_Facturas = new Button("Visualizar Facturas");
            Visualizar_Facturas.SetSizeRequest(200, 30);
            Visualizar_Facturas.Clicked += VisualizarFacturas_Clicked;
            ApplyFont(Visualizar_Facturas, "Arial 12");
            fixeds.Put(Visualizar_Facturas, 50, 180);

            Cerrar_Sesion = new Button("Cerrar Sesion");
            Cerrar_Sesion.SetSizeRequest(200, 30);
            Cerrar_Sesion.Clicked += CerrarSesion_Clicked;
            ApplyFont(Cerrar_Sesion, "Arial 12");
            fixeds.Put(Cerrar_Sesion, 50, 220);

            Add(fixeds);
        }

        private void VisualizarVehiculo_Clicked(object? sender, EventArgs e)
        {
            VisualizarVehiculo visualizarVehiculos = new VisualizarVehiculo(ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
            visualizarVehiculos.ShowAll();
        }
        private void VisualizarServicio_Clicked(object? sender, EventArgs e)
        {
            VisualizarServicio visualizarServicios = new VisualizarServicio(ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
            visualizarServicios.ShowAll();
        }
        private void VisualizarFacturas_Clicked(object? sender, EventArgs e)
        {
            VisualizarFactura visualizarFacturas = new VisualizarFactura(ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
            visualizarFacturas.ShowAll();
        }
        private void CerrarSesion_Clicked(object? sender, EventArgs e)
        {
            Logiarses logiarses = new Logiarses(ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
            logiarses.ShowAll();
            this.Hide();
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
    
