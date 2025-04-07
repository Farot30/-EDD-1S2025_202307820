using Gtk;
using System;
using Programa;
using Logiarse;
using INterfacesAdmin.CargasMasivas;
using INterfacesAdmin.GenerarServicio;
using INterfacesAdmin.GesionUsuarios;
using INterfacesAdmin.VisualizarLogueo;
using INterfacesAdmin.VisualizarRepuesto;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using GeneracionGrafica;

namespace INterfacesAdmin.Menu
{
    public class Menus : Window
    {
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ListaDeListas grafoNodiri;

        private Button masivox;
        private Button Gestion_Usuarios;
        private Button Visualizacion_Repuestos;
        private Button Visualizacion_Logueo;
        private Button Generar_servicio;
        private Button Generar_Reporte;
        private Button Generar_Backup;
        private Button Cerrar_Sesion;

        public Menus(ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Menú Principal")
        {
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            grafoNodiri = Grafo;

            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Menú Principal", "Arial 20");
            fixeds.Put(label, 50, 50);

            masivox = new Button("Carga Masiva");
            masivox.SetSizeRequest(200, 30);
            masivox.Clicked += GestionMasivas;
            ApplyFont(masivox, "Arial 12");
            fixeds.Put(masivox, 50, 100);

            Gestion_Usuarios = new Button("Gestión de Usuarios");
            Gestion_Usuarios.SetSizeRequest(200, 30);
            Gestion_Usuarios.Clicked += GestionarUsuarios;
            ApplyFont(Gestion_Usuarios, "Arial 12");
            fixeds.Put(Gestion_Usuarios, 50, 140);

            Visualizacion_Repuestos = new Button("Visualización de Repuestos");
            Visualizacion_Repuestos.SetSizeRequest(200, 30);
            Visualizacion_Repuestos.Clicked += VisualizarRepuestos;
            ApplyFont(Visualizacion_Repuestos, "Arial 12");
            fixeds.Put(Visualizacion_Repuestos, 50, 180);

            Visualizacion_Logueo = new Button("Visualización de Logueo");
            Visualizacion_Logueo.SetSizeRequest(200, 30);
            Visualizacion_Logueo.Clicked += VisualizarLogueo;
            ApplyFont(Visualizacion_Logueo, "Arial 12");
            fixeds.Put(Visualizacion_Logueo, 50, 220);

            Generar_servicio = new Button("Generar Servicio");
            Generar_servicio.SetSizeRequest(200, 30);
            Generar_servicio.Clicked += GenerarServicio;
            ApplyFont(Generar_servicio, "Arial 12");
            fixeds.Put(Generar_servicio, 50, 260);

            Generar_Reporte = new Button("Generar Reporte");
            Generar_Reporte.SetSizeRequest(200, 30);
            Generar_Reporte.Clicked += GenerarReporte;
            ApplyFont(Generar_Reporte, "Arial 12");
            fixeds.Put(Generar_Reporte, 50, 300);

            Generar_Backup = new Button("Generar Backup");
            Generar_Backup.SetSizeRequest(200, 30);
            Generar_Backup.Clicked += GenerarBackup;
            ApplyFont(Generar_Backup, "Arial 12");
            fixeds.Put(Generar_Backup, 50, 340);

            Cerrar_Sesion = new Button("Cerrar Sesión");
            Cerrar_Sesion.SetSizeRequest(200, 30);
            Cerrar_Sesion.Clicked += CerrarSesion;
            ApplyFont(Cerrar_Sesion, "Arial 12");
            fixeds.Put(Cerrar_Sesion, 50, 380);

            Add(fixeds);
        }

        private void GestionMasivas(object? sender, EventArgs e)
        {
            CargaMasivaS cargasMasivas = new CargaMasivaS(listaVehiculos, avlTreeRepuestos, binarioServicio, grafoNodiri);
            cargasMasivas.Show();
        }
        private void GestionarUsuarios(object? sender, EventArgs e)
        {
            GestionUsuario gestionUsuario = new GestionUsuario(listaVehiculos, avlTreeRepuestos, binarioServicio, grafoNodiri);
            gestionUsuario.Show();
        }

        private void VisualizarRepuestos(object? sender, EventArgs e)
        {
            VisualizarRepuestos visualizarRepuesto = new VisualizarRepuestos(listaVehiculos, avlTreeRepuestos, binarioServicio, grafoNodiri);
            visualizarRepuesto.Show();
        }

        private void VisualizarLogueo(object? sender, EventArgs e)
        {
            Visualizacion_Logueo visualizacion = new Visualizacion_Logueo(listaVehiculos, avlTreeRepuestos, binarioServicio, grafoNodiri);
            visualizacion.Show();
        }

        private void GenerarServicio(object? sender, EventArgs e)
        {
            GenerarServicios generarservicio = new GenerarServicios(listaVehiculos, avlTreeRepuestos, binarioServicio, grafoNodiri);
            generarservicio.Show();
        }

        private void GenerarReporte(object? sender, EventArgs e)
        {
            // Generar y guardar graficas dot
            GenerarYGUardarGrafica(listaVehiculos.GenerarGraphviz(), "ListaAutosDoble.dot", "Árbol Binario");
            GenerarYGUardarGrafica(avlTreeRepuestos.GenerarGraphviz(), "ListaRepuestoAVL.dot", "Árbol Binario");
            GenerarYGUardarGrafica(binarioServicio.GenerarGraphviz(), "ListaServicioBinario.dot", "Árbol Binario");

            // Convertir dot a imagen
            ConvertirDotAImagen("ListaAutosDoble.dot");
            ConvertirDotAImagen("ListaRepuestoAVL.dot");
            ConvertirDotAImagen("ListaServicioBinario.dot");
        }

        private void GenerarYGUardarGrafica(string graphviz, string nombreArchivo, string titulo)
        {
            Grafica.GenerarArchivoDot(nombreArchivo, graphviz, titulo);
        }

        private void ConvertirDotAImagen(string nombreArchivo)
        {
            Grafica.ConvertirDotAImagen(nombreArchivo);
        }

        private void GenerarBackup(object? sender, EventArgs e)
        {
            // Implementar la lógica para generar backup
        }

        private void CerrarSesion(object? sender, EventArgs e)
        {
            Logiarses logiarses = new Logiarses();
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