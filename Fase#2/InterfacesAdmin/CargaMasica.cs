using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Gtk;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using CLases.Usuarios;
using CLases.Vehiculo;
using CLases.Repuesto;

namespace InterfacesAdmin.CargaMasiva
{
    public class CargaMasivas : Window
    {

        private Button Cargar;
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo listaAutos;
        private AVLTREErepues ListaRepuestos;
        public CargaMasivas(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto) : base("Carga Masiva")
        {
            ListaUsuarioSimple = listaUsuario;
            listaAutos = ListaAuto;
            ListaRepuestos = ListaRepuesto;
            
            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixedcontainer = new Fixed(); 

            EventBox labelesBox = CreateStyledLabel("Carga Masiva", "Arial 20");
            fixedcontainer.Put(labelesBox, 120, 50);

            Cargar = new Button("Cargar");
            Cargar.SetSizeRequest(150,50);
            ApplyFont(Cargar, "Arial 12");
            fixedcontainer.Put(Cargar, 135, 100);

            Add(fixedcontainer);

        }

        private void Cargar_Clicked(object? sender, EventArgs e)
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

        private void ImprimirDatos()
        {
            Console.WriteLine("Usuarios:");
            ListaUsuarioSimple.Mostrar();

            Console.WriteLine("Vehículos:");
            listaAutos.Mostrar();

            Console.WriteLine("Repuestos:");
            ListaRepuestos.Mostrar();
        }


        private void DeserializarYGuardar(string filePath)
        {
            try
            {
                string jsonContent = File.ReadAllText(filePath);

                if (string.IsNullOrEmpty(jsonContent))
                {
                    Console.WriteLine("El archivo JSON está vacío");
                    return;
                }

                if (filePath.Contains("Usuarios"))
                {
                    var usuariosTemp = JsonConvert.DeserializeObject<List<UsuarioTemp>>(jsonContent);
                    foreach (var usuarioTemp in usuariosTemp)
                    {
                        if (!ListaUsuarioSimple.UsuarioExiste(usuarioTemp.ID))
                        {
                            Usuario usuario = new Usuario(usuarioTemp.ID, usuarioTemp.Nombres, usuarioTemp.Apellidos, usuarioTemp.Correo, 0, usuarioTemp.Contrasenia);
                            ListaUsuarioSimple.Insertar(usuario);
                        }
                        else
                        {
                            Console.WriteLine($"El ID {usuarioTemp.ID} ya existe en la lista de usuarios.");
                        }
                    }
                }
                else if (filePath.Contains("Vehiculos"))
                {
                    var vehiculosTemp = JsonConvert.DeserializeObject<List<VehiculoTemp>>(jsonContent);
                    foreach (var vehiculoTemp in vehiculosTemp)
                    {
                        if (!listaAutos.VehiculoExiste(vehiculoTemp.ID) && ListaUsuarioSimple.UsuarioExiste(vehiculoTemp.ID_Usuario))
                        {
                            Vehiculos vehiculo = new Vehiculos(vehiculoTemp.ID, vehiculoTemp.ID_Usuario, vehiculoTemp.Marca, vehiculoTemp.Modelo, vehiculoTemp.Placa);
                            listaAutos.Insertar(vehiculo);
                        }
                        else
                        {
                            Console.WriteLine($"El ID {vehiculoTemp.ID} ya existe en la lista de vehículos.");
                        }
                    }
                }
                else if (filePath.Contains("Repuestos"))
                {
                    var repuestosTemp = JsonConvert.DeserializeObject<List<RepuestoTemp>>(jsonContent);
                    foreach (var repuestoTemp in repuestosTemp)
                    {
                        if (!ListaRepuestos.RepuestoExiste(repuestoTemp.ID))
                        {
                            Repuesto repuesto = new Repuesto(repuestoTemp.ID, repuestoTemp.Repuesto, repuestoTemp.Detalles, repuestoTemp.Costo);
                            ListaRepuestos.Add(repuesto);
                            Console.WriteLine($"Repuesto insertado: ID={repuestoTemp.ID}, Repuesto={repuestoTemp.Repuesto}, Descripcion={repuestoTemp.Detalles}, Costo={repuestoTemp.Costo}");
                        }
                        else
                        {
                            Console.WriteLine($"El ID {repuestoTemp.ID} ya existe en la lista de repuestos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo JSON: {ex.Message}");
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
    public string Contrasenia { get; set; }
}

public class VehiculoTemp
{
    public int ID { get; set; }
    public int ID_Usuario { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Placa { get; set; }
}

public class RepuestoTemp
{
    public int ID { get; set; }
    public string Repuesto { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }
}