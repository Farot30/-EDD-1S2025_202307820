using Gtk;
using Newtonsoft.Json;
using System;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using Listas.Blockchains;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;
using Clases.Vehiculo;
using Clases.Repuesto;
using Clases.Servicios;
using Clases.Usuarios;
using Clases.Facturas;

namespace INterfacesAdmin.CargasMasivas
{
    public class CargaMasivaS : Window
    {
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ArbolMerklesFac ListaFacturas;
        private ListaDeListas grafoNodiri;
        private Blockchain blockchainUsuarios;
        public CargaMasivaS(Blockchain BlockchainUsuarios, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo) : base("Carga Masiva")
        {
            blockchainUsuarios = BlockchainUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Carga Masiva", "Arial 20");
            fixeds.Put(label, 120, 50);

            Button cargarDatos = new Button("Cargar Datos");
            cargarDatos.SetSizeRequest(200, 30);
            cargarDatos.Clicked += CargarDatosMasivos;
            ApplyFont(cargarDatos, "Arial 12");
            fixeds.Put(cargarDatos, 110, 100);

            Add(fixeds);
        }

        private void CargarDatosMasivos(object? sender, EventArgs e)
        {
            FileChooserDialog fileChooser = new FileChooserDialog(
                "Selecciona un archivo JSON",
                this,
                FileChooserAction.Open,
                "Cancelar", ResponseType.Cancel,
                "Abrir", ResponseType.Accept);

            fileChooser.Filter = new FileFilter();
            fileChooser.Filter.AddPattern("*.json");

            if (fileChooser.Run() == (int)ResponseType.Accept)
            {
                string filePath = fileChooser.Filename;
                fileChooser.Destroy(); // Destruir el diálogo antes de continuar

                if (!string.IsNullOrEmpty(filePath))
                {
                    DeserializarYGuardar(filePath);
                    ImprimirDatos();
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Archivo cargado exitosamente"))
                    {
                        md.Run();
                        md.Destroy(); // Destruir el diálogo después de usarlo
                    }
                }
            }
            else
            {
                fileChooser.Destroy(); // Asegúrate de destruir el diálogo en todos los casos
            }
        }

        private void DeserializarYGuardar(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(jsonContent))
                {
                    MostrarMensaje("El archivo JSON está vacío");
                    return;
                }
                if (filePath.Contains("Usuarios"))
                {
                    var usuariosTemp = JsonConvert.DeserializeObject<List<UsuarioTemp>>(jsonContent);
                    if (usuariosTemp != null)
                    {
                        foreach (var usuarioTemp in usuariosTemp)
                        {
                            if (!blockchainUsuarios.VerificarId(usuarioTemp.ID))
                            {
                                if (!blockchainUsuarios.VerificarCorreo(usuarioTemp.Correo))
                                {
                                    Usuario usuario = new Usuario(usuarioTemp.ID, usuarioTemp.Nombres, usuarioTemp.Apellidos, usuarioTemp.Correo, usuarioTemp.Edad, usuarioTemp.Contrasenia);
                                    blockchainUsuarios.AddBlock(usuario); // Agregar usuario como bloque en la blockchain
                                }
                                else
                                {
                                    MostrarMensaje($"El correo {usuarioTemp.Correo} ya existe en la blockchain.");
                                }
                            }
                            else
                            {
                                MostrarMensaje($"El ID {usuarioTemp.ID} ya existe en la blockchain.");
                            }
                        }
                    }
                }
                else if (filePath.Contains("Vehiculos"))
                {
                    var vehiculosTemp = JsonConvert.DeserializeObject<List<VehiculoTemp>>(jsonContent);
                    if(vehiculosTemp != null){
                        foreach (var vehiculoTemp in vehiculosTemp)
                        {
                            if (!listaVehiculos.VehiculoExiste(vehiculoTemp.ID))
                            {
                                if (blockchainUsuarios.VerificarId(vehiculoTemp.ID_Usuario))
                                {
                                    Vehiculos vehiculo = new Vehiculos(vehiculoTemp.ID, vehiculoTemp.ID_Usuario, vehiculoTemp.Marca, vehiculoTemp.Modelo, vehiculoTemp.Placa);
                                    listaVehiculos.Insertar(vehiculo);
                                }
                                else
                                {
                                    MostrarMensaje($"El ID {vehiculoTemp.ID_Usuario} no existe en la lista de usuarios.");
                                }
                            }
                            else
                            {
                                MostrarMensaje($"El ID {vehiculoTemp.ID} ya existe en la lista de vehículos.");
                            }
                        }
                    }
                }
                else if (filePath.Contains("Repuestos"))
                {
                    var repuestosTemp = JsonConvert.DeserializeObject<List<RepuestoTemp>>(jsonContent);
                    if(repuestosTemp != null){
                        foreach (var repuestoTemp in repuestosTemp)
                        {
                            if (!avlTreeRepuestos.RepuestoExiste(repuestoTemp.ID))
                            {
                                Repuesto repuesto = new Repuesto(repuestoTemp.ID, repuestoTemp.Repuesto, repuestoTemp.Detalles, repuestoTemp.Costo);
                                avlTreeRepuestos.Add(repuesto);
                            }
                            else
                            {
                                MostrarMensaje($"El ID {repuestoTemp.ID} ya existe en la lista de repuestos.");
                            }
                        }
                    }
                }
                else if (filePath.Contains("Servicios"))
                {
                try
                {
                    var serviciosTemp = JsonConvert.DeserializeObject<List<ServicioTemp>>(jsonContent);
                    if (serviciosTemp != null)
                    {
                        Console.WriteLine($"Se encontraron {serviciosTemp.Count} servicios en el archivo JSON.");
                        foreach (var servicioTemp in serviciosTemp)
                        {
                            // Validar que todos los campos estén presentes
                            if (servicioTemp.Id == 0 || servicioTemp.Id_repuesto == 0 || servicioTemp.Id_vehiculo == 0 ||
                                string.IsNullOrWhiteSpace(servicioTemp.Detalles) || servicioTemp.Costo == 0 ||
                                string.IsNullOrWhiteSpace(servicioTemp.MetodoPago))
                            {
                                Console.WriteLine($"Error: Servicio con datos incompletos o inválidos: {JsonConvert.SerializeObject(servicioTemp)}");
                                continue;
                            }

                            // Procesar el servicio
                            if (!binarioServicio.Existeid(servicioTemp.Id))
                            {
                                if (avlTreeRepuestos.RepuestoExiste(servicioTemp.Id_repuesto))
                                {
                                    if (listaVehiculos.VehiculoExiste(servicioTemp.Id_vehiculo))
                                    {
                                        Servicio servicio = new Servicio(servicioTemp.Id, servicioTemp.Id_repuesto, servicioTemp.Id_vehiculo, servicioTemp.Detalles, servicioTemp.Costo, servicioTemp.MetodoPago);
                                        binarioServicio.Insertar(servicio);
                                        binarioServicio.Mostrar();
                                        grafoNodiri.AgregarVehiculoConRepuesto(servicioTemp.Id_vehiculo, servicioTemp.Id_repuesto);
                                        string MetodoPago = servicioTemp.MetodoPago;
                                        int IdServicio = servicioTemp.Id;
                                        
                                        // Calcular el total de la factura
                                        Repuesto? repuesto = avlTreeRepuestos.BuscarRepuestoPorId(servicioTemp.Id_repuesto);
                                        if (repuesto != null)
                                        {
                                            double total = servicioTemp.Costo + repuesto.Value.Precio;
                                            // Crear e insertar la factura
                                            try
                                            {
                                                if (ListaFacturas == null)
                                                {
                                                    Console.WriteLine("Error: ListaFacturas no está inicializada.");
                                                    return;
                                                }
                                                Console.WriteLine($"Factura creada: {IdServicio}, Total: {total}, Método de Pago: {MetodoPago}");
                                                
                                                // Agregar la factura al árbol de Merkle
                                                ListaFacturas.InsertarFactura(IdServicio, total, MetodoPago);
                                                Console.WriteLine($"Factura insertada en el árbol de Merkle para el servicio con ID {servicioTemp.Id}");

                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"Error al crear o insertar la factura para el servicio con ID {servicioTemp.Id}: {ex.Message}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Error: El repuesto con ID {servicioTemp.Id_repuesto} no se encontró al calcular la factura.");
                                            MostrarMensaje($"Error: El repuesto con ID {servicioTemp.Id_repuesto} no se encontró al calcular la factura.");
                                        }
                                    }
                                    else
                                    {
                                        MostrarMensaje($"Error: El vehículo con ID {servicioTemp.Id_vehiculo} no existe en la lista de vehículos.");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje($"Error: El repuesto con ID {servicioTemp.Id_repuesto} no existe en la lista de repuestos.");
                                }
                            }
                            else
                            {
                                MostrarMensaje($"Error: El servicio con ID {servicioTemp.Id} ya existe en la lista de servicios.");
                            }
                        }
                    }
                    else
                    {
                        MostrarMensaje("El archivo JSON de servicios no contiene datos válidos.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al procesar el archivo JSON de servicios: {ex.Message}");
                    MostrarMensaje($"Error al procesar el archivo JSON de servicios: {ex.Message}");
                }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo JSON: {ex.Message}");
                MostrarMensaje($"Error al leer el archivo JSON: {ex.Message}");
            }
        }

        private void ImprimirDatos()
        {/*
            Console.WriteLine("Usuarios:");
            blockchainUsuarios.Chain.ForEach(block => Console.WriteLine(block.Data.ToString()));

            Console.WriteLine("Vehículos:");
            listaVehiculos.Mostrar();

            Console.WriteLine("Repuestos:");
            avlTreeRepuestos.Mostrar();

            Console.WriteLine("Servicios:");
            binarioServicio.Mostrar();*/

            Console.WriteLine("Facturas:");
            ListaFacturas.MostrarFacturas();
        }

        private void MostrarMensaje(string mensaje)
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje))
            {
                md.Run();
                md.Destroy();
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
            if (widget is Button button)
            {
                button.Child.ModifyFont(fontDesc);
            }else if (widget is Label label)
            {
                label.ModifyFont(fontDesc);
            }
        }
    }
}

public class UsuarioTemp
{
    public int ID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Contrasenia { get; set; }
}

public class VehiculoTemp
{
    public int ID { get; set; }
    public int ID_Usuario { get; set; }
    public string Marca { get; set; }
    public int Modelo { get; set; }
    public string Placa { get; set; }
}

public class RepuestoTemp
{
    public int ID { get; set; }
    public string Repuesto { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }
}

public class ServicioTemp
{
    public int Id { get; set; }
    public int Id_repuesto { get; set; }
    public int Id_vehiculo { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }
    public string MetodoPago { get; set; }
}