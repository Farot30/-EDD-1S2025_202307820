using Gtk;
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using Programa;
using Logiarse;
using INterfacesAdmin.CargasMasivas;
using INterfacesAdmin.GenerarServicio;
using INterfacesAdmin.GesionUsuarios;
using INterfacesAdmin.VisualizarLogueo;
using INterfacesAdmin.VisualizarRepuesto;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;
using GeneracionGrafica;

namespace INterfacesAdmin.Menu
{
    public class Menus : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ArbolMerklesFac listaFacturas;
        private ListaDeListas grafoNodiri;

        private Button masivox;
        private Button Gestion_Usuarios;
        private Button Visualizacion_Repuestos;
        private Button Visualizacion_Logueo;
        private Button Generar_servicio;
        private Button Generar_Reporte;
        private Button Generar_Backup;
        private Button Cerrar_Sesion;

        public Menus(Blockchain BlockchainUsuarios, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo) : base("Menú Principal")
        {
            blockchainUsuarios = BlockchainUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            listaFacturas = ListaFactura;
            grafoNodiri = Grafo;

            SetDefaultSize(400, 450);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Menú Principal", "Arial 20");
            fixeds.Put(label, 120, 50);

            masivox = new Button("Carga Masiva");
            masivox.SetSizeRequest(200, 30);
            masivox.Clicked += GestionMasivas;
            ApplyFont(masivox, "Arial 12");
            fixeds.Put(masivox, 100, 100);

            Gestion_Usuarios = new Button("Gestión de Usuarios");
            Gestion_Usuarios.SetSizeRequest(200, 30);
            Gestion_Usuarios.Clicked += GestionarUsuarios;
            ApplyFont(Gestion_Usuarios, "Arial 12");
            fixeds.Put(Gestion_Usuarios, 100, 140);

            Visualizacion_Repuestos = new Button("Visualización de Repuestos");
            Visualizacion_Repuestos.SetSizeRequest(200, 30);
            Visualizacion_Repuestos.Clicked += VisualizarRepuestos;
            ApplyFont(Visualizacion_Repuestos, "Arial 12");
            fixeds.Put(Visualizacion_Repuestos, 90, 180);

            Visualizacion_Logueo = new Button("Visualización de Logueo");
            Visualizacion_Logueo.SetSizeRequest(200, 30);
            Visualizacion_Logueo.Clicked += VisualizarLogueo;
            ApplyFont(Visualizacion_Logueo, "Arial 12");
            fixeds.Put(Visualizacion_Logueo, 95, 220);

            Generar_servicio = new Button("Generar Servicio");
            Generar_servicio.SetSizeRequest(200, 30);
            Generar_servicio.Clicked += GenerarServicio;
            ApplyFont(Generar_servicio, "Arial 12");
            fixeds.Put(Generar_servicio, 100, 260);

            Generar_Reporte = new Button("Generar Reporte");
            Generar_Reporte.SetSizeRequest(200, 30);
            Generar_Reporte.Clicked += GenerarReporte;
            ApplyFont(Generar_Reporte, "Arial 12");
            fixeds.Put(Generar_Reporte, 100, 300);

            Generar_Backup = new Button("Generar Backup");
            Generar_Backup.SetSizeRequest(200, 30);
            Generar_Backup.Clicked += GenerarBackup;
            ApplyFont(Generar_Backup, "Arial 12");
            fixeds.Put(Generar_Backup, 100, 340);

            Cerrar_Sesion = new Button("Cerrar Sesión");
            Cerrar_Sesion.SetSizeRequest(200, 30);
            Cerrar_Sesion.Clicked += CerrarSesion;
            ApplyFont(Cerrar_Sesion, "Arial 12");
            fixeds.Put(Cerrar_Sesion, 100, 380);

            Add(fixeds);
        }

        private void GestionMasivas(object? sender, EventArgs e)
        {
            CargaMasivaS cargasMasivas = new CargaMasivaS(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, listaFacturas, grafoNodiri);
            cargasMasivas.ShowAll();
        }
        private void GestionarUsuarios(object? sender, EventArgs e)
        {
            GestionUsuario gestionUsuario = new GestionUsuario(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, listaFacturas, grafoNodiri);
            gestionUsuario.ShowAll();
        }

        private void VisualizarRepuestos(object? sender, EventArgs e)
        {
            VisualizarRepuestos visualizarRepuesto = new VisualizarRepuestos(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, listaFacturas, grafoNodiri);
            visualizarRepuesto.ShowAll();
        }

        private void VisualizarLogueo(object? sender, EventArgs e)
        {
            Visualizacion_Logueo visualizacion = new Visualizacion_Logueo(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, listaFacturas, grafoNodiri);
            visualizacion.ShowAll();
        }

        private void GenerarServicio(object? sender, EventArgs e)
        {
            GenerarServicios generarservicio = new GenerarServicios(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, listaFacturas, grafoNodiri);
            generarservicio.ShowAll();
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
            string reportesPath = System.IO.Path.Combine("Reportes");
            grafoNodiri.GenerarGrafo(reportesPath);
            blockchainUsuarios.GenerarDot("blockchain");

            listaFacturas.GenerateDotAndConvertToImage("MerkleFacturas", reportesPath);
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
            try
            {
                string backupPath = "Backups";
                if (!Directory.Exists(backupPath))
                {
                    Directory.CreateDirectory(backupPath);
                }

                // 1. Generar archivo JSON para Usuarios (Blockchain)
                string usuariosJsonPath = System.IO.Path.Combine(backupPath, "Usuarios.json");
                string usuariosJson = blockchainUsuarios.GenerarJson(); // Método que genera el JSON de usuarios
                File.WriteAllText(usuariosJsonPath, usuariosJson);
                Console.WriteLine($"Backup de usuarios generado en: {usuariosJsonPath}");
/*
                // 2. Generar archivo .edd para Vehículos
                string vehiculosPath = Path.Combine(backupPath, "Vehiculos.edd");
                string vehiculosData = listaVehiculos.GenerarTextoPlano(); // Método que genera el texto plano de vehículos
                string vehiculosComprimido = ComprimirHuffman(vehiculosData); // Método para comprimir con Huffman
                File.WriteAllText(vehiculosPath, vehiculosComprimido);
                Console.WriteLine($"Backup comprimido de vehículos generado en: {vehiculosPath}");

                // 3. Generar archivo .edd para Repuestos
                string repuestosPath = Path.Combine(backupPath, "Repuestos.edd");
                string repuestosData = avlTreeRepuestos.GenerarTextoPlano(); // Método que genera el texto plano de repuestos
                string repuestosComprimido = ComprimirHuffman(repuestosData); // Método para comprimir con Huffman
                File.WriteAllText(repuestosPath, repuestosComprimido);
                Console.WriteLine($"Backup comprimido de repuestos generado en: {repuestosPath}");
*/
                MostrarMensaje("Backup generado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el backup: {ex.Message}");
                MostrarMensaje($"Error al generar el backup: {ex.Message}");
            }
        }

        private void CerrarSesion(object? sender, EventArgs e)
        {
            Logiarses logiarses = new Logiarses(blockchainUsuarios, listaVehiculos, avlTreeRepuestos, binarioServicio, listaFacturas, grafoNodiri);
            logiarses.ShowAll();
            this.Hide();
        }

        private void MostrarMensaje(string mensaje)
        {
            MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, mensaje);
            messageDialog.Run();
            messageDialog.Destroy();
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