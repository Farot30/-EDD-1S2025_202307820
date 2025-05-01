using Gtk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using ListasArboles.ArbolMerkles;
using ListasArboles.CompresionHUffman;
using Clases.Usuarios;
using Clases.Repuesto;
using Clases.Vehiculo;
using INterfacesAdmin.Menu;
using InterfacesUsuario.MenuUsuario;
using Programa;
using Clases.ControlLogeo;
using Nodos.NofoHUffman;

namespace Logiarse
{
    public class Logiarses : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ArbolMerklesFac ListaFacturas;
        private ListaDeListas grafoNodiri;

        private Entry Correo;
        private Entry Contrasena;
        private Button IniciarSesion;
        private EventBox Usus;
        private EventBox Contras;
        public Logiarses(Blockchain BlockchainUsuarios, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo) : base("Inicio de sesión")
        {
            blockchainUsuarios = BlockchainUsuarios;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Loginis", "Arial 20");
            fixeds.Put(label, 150, 50);

            Usus = CreateStyledLabel("Correo", "Arial 12");
            fixeds.Put(Usus, 165, 100);

            Correo = new Entry();
            Correo.SetSizeRequest(200, 30);
            fixeds.Put(Correo, 100, 130);
            ApplyFont(Correo, "Arial 12");

            Contras = CreateStyledLabel("Contraseña", "Arial 12");
            fixeds.Put(Contras, 150, 170);

            Contrasena = new Entry();
            Contrasena.SetSizeRequest(200, 30);
            Contrasena.Visibility = false;
            fixeds.Put(Contrasena, 100, 200);
            ApplyFont(Contrasena, "Arial 12");

            IniciarSesion = new Button("Iniciar sesión");
            IniciarSesion.SetSizeRequest(200, 30);
            IniciarSesion.Clicked += Validación;
            fixeds.Put(IniciarSesion, 100, 270);
            ApplyFont(IniciarSesion, "Arial 12");

            Add(fixeds);
            
        }

        private void Validación(object? sender, EventArgs e)
        {
            string correo = Correo.Text;
            string contrasena = Contrasena.Text;

            if (correo == "admin@usac.com" && contrasena == "admin123")
            {

                Menus menu = new Menus(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas , grafoNodiri);
                menu.ShowAll();
                this.Hide();
            }
            else if (blockchainUsuarios.ValidarUsuario(correo, contrasena))
            {
                // Obtener el ID del usuario
                Usuario? usuario = blockchainUsuarios.BuscarUsuarioPorCorreo(correo);
                if (usuario != null)
                {
                    // Registrar el inicio de sesión del usuario
                    MainClass.LogUsuarios.Add(new LogUsuario(correo, DateTime.Now));

                    // Redirigir al menú de usuario con el ID del usuario
                    MenuUsuarios menuUsuario = new MenuUsuarios(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, ListaFacturas, grafoNodiri, usuario.Value.Id);
                    menuUsuario.ShowAll();
                    this.Hide();
                }
            }
            else
            {
                // Mostrar mensaje de error
                MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario o contraseña incorrectos.");
                md1.Run();
                md1.Destroy();
            }
        }

        public void CargarBackup()
        {
            try
            {
                string backupPath = "Backups";

                // 1. Cargar archivo JSON de Usuarios en la Blockchain
                string usuariosJsonPath = System.IO.Path.Combine(backupPath, "Usuarios.json");
                if (File.Exists(usuariosJsonPath))
                {
                    string usuariosJson = File.ReadAllText(usuariosJsonPath);
                    List<Usuario> usuarios = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Usuario>>(usuariosJson);

                    if (usuarios != null)
                    {
                        foreach (var usuario in usuarios)
                        {
                            blockchainUsuarios.AddBlock(usuario);
                        }

                        // Validar la integridad del Blockchain
                        if (!blockchainUsuarios.ValidarIntegridad())
                        {
                            Console.WriteLine("Error: La cadena de bloques está corrupta.");
                            MostrarMensaje("Error: La cadena de bloques está corrupta. No se cargará el backup.");
                            return;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: No se encontró el archivo Usuarios.json.");
                    MostrarMensaje("Error: No se encontró el archivo Usuarios.json.");
                    return;
                }

                // 2. Descomprimir y cargar Vehículos
                string vehiculosEddPath = System.IO.Path.Combine(backupPath, "Vehiculos.edd");
                string treeFile = System.IO.Path.Combine(backupPath, "huffman_vehiculo.json");
                if (File.Exists(vehiculosEddPath) && File.Exists(treeFile))
                {
                    string vehiculosComdprimido = File.ReadAllText(vehiculosEddPath);
                    NodoHuffman vehiculosRaiz = JsonSerializer.Deserialize<NodoHuffman>(File.ReadAllText(treeFile));
                    string vehiculosDescomprimido = CompresionHuffman.Descomprimir(vehiculosComdprimido, vehiculosRaiz);

                    string[] vehiculosLineas = vehiculosDescomprimido.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string linea in vehiculosLineas)
                    {
                        string[] datos = linea.Split(',');
                        if (datos.Length == 5)
                        {
                            int id = int.Parse(datos[0]);
                            int idUsuario = int.Parse(datos[1]);
                            string marca = datos[2];
                            int modelo = int.Parse(datos[3]);
                            string placa = datos[4];

                            ListaDobleVehiculos.Insertar(new Vehiculos(id, idUsuario, marca, modelo, placa));
                        }
                    }
                    ListaDobleVehiculos.Mostrar();
                }
                else
                {
                    Console.WriteLine("Error: No se encontró el archivo Vehiculos.edd.");
                    MostrarMensaje("Error: No se encontró el archivo Vehiculos.edd.");
                    return;
                }

                // 3. Descomprimir y cargar Repuestos
                string repuestosEddPath = System.IO.Path.Combine(backupPath, "Repuestos.edd");
                string treeFileRepuestos = System.IO.Path.Combine(backupPath, "huffman_repuesto.json");
                if (File.Exists(repuestosEddPath))
                {
                    string repuestosComprimido = File.ReadAllText(repuestosEddPath);

                    NodoHuffman repuestosRaiz = JsonSerializer.Deserialize<NodoHuffman>(File.ReadAllText(treeFileRepuestos));
                    string repuestosDescomprimido = CompresionHuffman.Descomprimir(repuestosComprimido, repuestosRaiz);

                    string[] repuestosLineas = repuestosDescomprimido.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string linea in repuestosLineas)
                    {
                        try
                        {
                            // Dividir la línea en partes, manejando comas dentro de los campos
                            string[] datos = linea.Split(',', StringSplitOptions.None);

                            // Verificar que la línea tenga al menos 4 elementos
                            if (datos.Length >= 4)
                            {
                                int id = int.Parse(datos[0]);
                                string nombre = datos[1];
                                string descripcion = datos[2];
                                double precio = double.Parse(datos[3]);

                                // Agregar el repuesto a la lista
                                listaRepuesto.Add(new Repuesto(id, nombre, descripcion, precio));
                            }
                            else
                            {
                                Console.WriteLine($"Línea inválida: {linea}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error al procesar la línea: {linea}. Detalles: {ex.Message}");
                        }
                    }
                    Console.WriteLine("Texto Descomprimido");
                    Console.WriteLine(repuestosDescomprimido);
                    listaRepuesto.Mostrar();
                }
                else
                {
                    Console.WriteLine("Error: No se encontró el archivo Repuestos.edd.");
                    MostrarMensaje("Error: No se encontró el archivo Repuestos.edd.");
                    return;
                }

                // 4. Validar consistencia de los datos
                int vehiculosCargados = ListaDobleVehiculos.ContarVehiculos();
                int repuestosCargados = listaRepuesto.ContarRepuestos();

                if (vehiculosCargados == 0 || repuestosCargados == 0)
                {
                    Console.WriteLine("Error: No se cargaron todos los datos correctamente.");
                    MostrarMensaje("Error: No se cargaron todos los datos correctamente.");
                    return;
                }

                Console.WriteLine("Backup cargado exitosamente.");
                MostrarMensaje("Backup cargado exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el backup: {ex.Message}");
                MostrarMensaje($"Error al cargar el backup: {ex.Message}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje);
            md.Run();
            md.Destroy();
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