using Gtk;
using Newtonsoft.Json;
using System;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using Clases.Vehiculo;
using Clases.Repuesto;
using Clases.Servicios;

namespace INterfacesAdmin.CargasMasivas
{
    public class CargaMasivaS : Window
    {
        private ListaDobleVehiculo listaAutos;
        private AVLTREErepues ListaRepuestos;
        private Binariostree ListaServicios;
        public CargaMasivaS(ListaDobleVehiculo ListaAuto) : base("Carga Masiva")
        {
            listaAutos = ListaAuto;

            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Carga Masiva", "Arial 20");
            fixeds.Put(label, 50, 50);

            Button cargarDatos = new Button("Cargar Datos");
            cargarDatos.SetSizeRequest(200, 30);
            cargarDatos.Clicked += CargarDatosMasivos;
            ApplyFont(cargarDatos, "Arial 12");
            fixeds.Put(cargarDatos, 50, 100);

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
                fileChooser.Destroy();

                // Verificar que el archivo no sea nulo o vacío
                if (!string.IsNullOrEmpty(filePath))
                {
                    DeserializarYGuardar(filePath);
                    ImprimirDatos();
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Archivo cargado exitosamente"))
                    {
                        md.Run();
                        md.Destroy();
                    }
                }
            }
            else
            {
                fileChooser.Destroy();
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
                    if(usuariosTemp != null){
                        foreach (var usuarioTemp in usuariosTemp)
                        {
                            if (!ListaUsuarioSimple.UsuarioExiste(usuarioTemp.ID))
                            {
                                if(!ListaUsuarioSimple.UsuarioExisteCorreo(usuarioTemp.Correo)){
                                    Usuario usuario = new Usuario(usuarioTemp.ID, usuarioTemp.Nombres, usuarioTemp.Apellidos, usuarioTemp.Correo, usuarioTemp.Edad, usuarioTemp.Contrasenia);
                                    ListaUsuarioSimple.Insertar(usuario);
                                }
                                else{
                                    MostrarMensaje($"El correo {usuarioTemp.Correo} ya existe en la lista de usuarios.");
                                }
                            }
                            else
                            {
                                MostrarMensaje($"El ID {usuarioTemp.ID} ya existe en la lista de usuarios.");
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
                            if (!listaAutos.VehiculoExiste(vehiculoTemp.ID))
                            {
                                if (ListaUsuarioSimple.UsuarioExiste(vehiculoTemp.ID_Usuario))
                                {
                                    Vehiculos vehiculo = new Vehiculos(vehiculoTemp.ID, vehiculoTemp.ID_Usuario, vehiculoTemp.Marca, vehiculoTemp.Modelo, vehiculoTemp.Placa);
                                    listaAutos.Insertar(vehiculo);
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
                            if (!ListaRepuestos.RepuestoExiste(repuestoTemp.ID))
                            {
                                Repuesto repuesto = new Repuesto(repuestoTemp.ID, repuestoTemp.Repuesto, repuestoTemp.Detalles, repuestoTemp.Costo);
                                ListaRepuestos.Add(repuesto);
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
                    var serviciosTemp = JsonConvert.DeserializeObject<List<ServicioTemp>>(jsonContent);
                    if(serviciosTemp != null){
                        foreach (var servicioTemp in serviciosTemp)
                        {
                            if (!ListaServicios.Existeid(servicioTemp.ID))
                            {
                                if (ListaRepuestos.RepuestoExiste(servicioTemp.ID_Repuesto))
                                {
                                    if (listaAutos.VehiculoExiste(servicioTemp.ID_Vehiculo))
                                    {
                                        Servicio servicio = new Servicio(servicioTemp.ID, servicioTemp.ID_Repuesto, servicioTemp.ID_Vehiculo, servicioTemp.Detalles, servicioTemp.Costo);
                                        ListaServicios.Insertar(servicio);
                                    }
                                    else
                                    {
                                        MostrarMensaje($"El ID {servicioTemp.ID_Vehiculo} no existe en la lista de vehículos.");
                                    }
                                }
                                else
                                {
                                    MostrarMensaje($"El ID {servicioTemp.ID_Repuesto} no existe en la lista de repuestos.");
                                }
                            }
                            else
                            {
                                MostrarMensaje($"El ID {servicioTemp.ID} ya existe en la lista de servicios.");
                            }
                        }
                    }
                }
                else
                {
                    MostrarMensaje("El archivo JSON no es válido o no se reconoce su formato.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al leer el archivo JSON: {ex.Message}");
            }
        }

        private void ImprimirDatos()
        {
            Console.WriteLine("Usuarios:");
            ListaUsuarioSimple.Mostrar();

            Console.WriteLine("Vehículos:");
            listaAutos.Mostrar();

            Console.WriteLine("Repuestos:");
            ListaRepuestos.Mostrar();
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
    public int ID { get; set; }
    public int ID_Repuesto { get; set; }
    public int ID_Vehiculo { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }
}