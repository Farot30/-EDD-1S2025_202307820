using System;
using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;
using InterfacesAdmin.CargaMasiva;
using InterfacesAdmin.Gestion;
using InterfacesAdmin.ActualizarRepuestos;
using InterfacesAdmin.VisualizarRepuestos;
using InterfacesAdmin.GenerarServicios;
using logiarse;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CLases.ControlLogeo;
using Program;
using GeneracionGrafica;

namespace InterfacesAdmin.menu
{
    public class Menus : Window
    {
        private Button masivos;
        private Button Gestion_Entidades;
        private Button Actualizacion_Repuestos;
        private Button Visualizacion_repuestos;
        private Button Generar_servicio;
        private Button Control_Logueo;
        private Button Generar_Reporte;
        private Button Cerrar_Sesion;
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo listaAutos;
        private AVLTREErepues ListaRepuestos;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;

        public Menus(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo lIstaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, B5Facturas ListaFactura) : base("Administrador")
        {
            ListaUsuarioSimple = listaUsuario;
            listaAutos = lIstaAuto;
            ListaRepuestos = ListaRepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;

            SetDefaultSize(500, 550);
            SetPosition(WindowPosition.Center);

            Fixed fixedcontainer = new Fixed(); 

            EventBox labelesBox = CreateStyledLabel("Menu Administrador", "Arial 20");
            fixedcontainer.Put(labelesBox, 140, 50);

            masivos = new Button("Cargas Masivas");
            masivos.SetSizeRequest(150,50);
            masivos.Clicked += Cargas;
            ApplyFont(masivos, "Arial 12");
            fixedcontainer.Put(masivos, 180, 100);

            Gestion_Entidades = new Button("Gestión de Entidades");
            Gestion_Entidades.SetSizeRequest(150,50);
            Gestion_Entidades.Clicked += GestionesEntidades;
            ApplyFont(Gestion_Entidades, "Arial 12");
            fixedcontainer.Put(Gestion_Entidades, 165, 160);

            Actualizacion_Repuestos = new Button("Actualización de Repuestos");
            Actualizacion_Repuestos.SetSizeRequest(150, 50);
            Actualizacion_Repuestos.Clicked += Actualizarrepuestos;
            ApplyFont(Actualizacion_Repuestos, "Arial 12");
            fixedcontainer.Put(Actualizacion_Repuestos, 150, 220);

            Visualizacion_repuestos = new Button("Visualización de Repuestos");
            Visualizacion_repuestos.SetSizeRequest(150, 50);
            Visualizacion_repuestos.Clicked += Visualizarrepuestos;
            ApplyFont(Visualizacion_repuestos, "Arual 12");
            fixedcontainer.Put(Visualizacion_repuestos, 150, 280);

            Generar_servicio = new Button("Generación de Servicios");
            Generar_servicio.SetSizeRequest(150, 50);
            Generar_servicio.Clicked += GenerarServicio;
            ApplyFont(Generar_servicio, "Arial 12");
            fixedcontainer.Put(Generar_servicio, 160, 340);

            Control_Logueo = new Button("Control de Logueo");
            Control_Logueo.SetSizeRequest(150, 50);
            Control_Logueo.Clicked += ControlLogueo;
            ApplyFont(Control_Logueo, "Arial 12");
            fixedcontainer.Put(Control_Logueo, 175, 400);

            Generar_Reporte = new Button("Generar Reporte");
            Generar_Reporte.SetSizeRequest(150, 50);
            Generar_Reporte.Clicked += GenerarReporte;
            ApplyFont(Generar_Reporte, "Arial 12");
            fixedcontainer.Put(Generar_Reporte, 185, 460);

            Cerrar_Sesion = new Button("Cerrar Sesión");
            Cerrar_Sesion.SetSizeRequest(150, 50);
            Cerrar_Sesion.Clicked += CerrarSesion;
            ApplyFont(Cerrar_Sesion, "Arial 12");
            fixedcontainer.Put(Cerrar_Sesion, 185, 520);

            Add(fixedcontainer);
        }

        private void Cargas(object? sender, EventArgs e)
        {
            CargaMasivas cargaMasivas = new CargaMasivas(ListaUsuarioSimple, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            cargaMasivas.ShowAll();
        }

        private void GestionesEntidades(object? sender, EventArgs e)
        {
            Gestiones gestiones = new Gestiones(ListaUsuarioSimple, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            gestiones.ShowAll();
        }

        private void Actualizarrepuestos(object? sender, EventArgs e)
        {
            ActualizarRepuesto actualizarRepuesto = new ActualizarRepuesto(ListaUsuarioSimple, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            actualizarRepuesto.ShowAll();
        }

        private void Visualizarrepuestos(object? sender, EventArgs e)
        {
            VisualizarRepuesto visualizarRepuestos = new VisualizarRepuesto(ListaUsuarioSimple, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            visualizarRepuestos.ShowAll();
        }

        private void GenerarServicio(object? sender, EventArgs e)
        {
            GenerarServicio generarServicio = new GenerarServicio(ListaUsuarioSimple, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            generarServicio.ShowAll();
        }

        private void ControlLogueo(object? sender, EventArgs e)
        {
            ControlLogeo.ExportarLogUsuarios(MainClass.LogUsuarios);
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Log de usuarios exportado con éxito.");
            md.Run();
            md.Hide();
        }

        private void GenerarReporte(object? sender, EventArgs e)
        {
            // Generar y guardar graficas dot
            GenerarYGUardarGrafica(ListaUsuarioSimple.GenerarGraphviz(), "ListaUsuariosSimple.dot", "Árbol Binario");
            GenerarYGUardarGrafica(listaAutos.GenerarGraphviz(), "ListaAutosDoble.dot", "Árbol Binario");
            GenerarYGUardarGrafica(ListaRepuestos.GenerarGraphviz(), "ListaRepuestoAVL.dot", "Árbol Binario");
            GenerarYGUardarGrafica(ListaServicios.GenerarGraphviz(), "ListaServicioBinario.dot", "Árbol Binario");
            GenerarYGUardarGrafica(ListaFacturas.GraficarGraphviz(), "ListaFacturaB5.dot", "Árbol Binario");

            // Convertir dot a imagen
            ConvertirDotAImagen("ListaUsuariosSimple.dot");
            ConvertirDotAImagen("ListaAutosDoble.dot");
            ConvertirDotAImagen("ListaRepuestoAVL.dot");
            ConvertirDotAImagen("ListaServicioBinario.dot");
            ConvertirDotAImagen("ListaFacturaB5.dot");

        }

        private void GenerarYGUardarGrafica(string graphviz, string nombreArchivo, string titulo)
        {
            Grafica.GenerarArchivoDot(nombreArchivo, graphviz, titulo);
        }

        private void ConvertirDotAImagen(string nombreArchivo)
        {
            Grafica.ConvertirDotAImagen(nombreArchivo);
        }

        private void CerrarSesion(object? sender, EventArgs e)
        {
            Logiarse logiarse = new Logiarse(ListaUsuarioSimple, listaAutos, ListaRepuestos, ListaServicios, ListaFacturas);
            logiarse.ShowAll();
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
        }
    }
}