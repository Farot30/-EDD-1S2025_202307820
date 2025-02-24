using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using ListaCircular;
using Repuestos;
using Newtonsoft.Json;
using System.IO;
using System;

public class Masivo : Window
{
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe Masivo(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Cargas masivas")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Cargas masivas");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargar");
        button1.Clicked += Cargaras;
        fixedContainer.Put(button1, 200, 250);

        Add(fixedContainer);
    }

    private unsafe void Cargaras(object? sender, EventArgs e)
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
                }
            }
        }
        else
        {
            fileChooser.Destroy();
        }
    }

    private unsafe void DeserializarYGuardar(string filePath)
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
                    Usuario usuario = new Usuario(usuarioTemp.ID, usuarioTemp.Nombres, usuarioTemp.Apellidos, usuarioTemp.Correo, usuarioTemp.Contrasenia);
                    lista.Insertar(usuario);
                }
            }
            else if (filePath.Contains("Vehiculos"))
            {
                var vehiculosTemp = JsonConvert.DeserializeObject<List<VehiculoTemp>>(jsonContent);
                foreach (var vehiculoTemp in vehiculosTemp)
                {
                    listaAutos.Insertar(vehiculoTemp.ID, vehiculoTemp.ID_Usuario, vehiculoTemp.Marca, int.Parse(vehiculoTemp.Modelo), vehiculoTemp.Placa);
                }
            }
            else if (filePath.Contains("Repuestos"))
            {
                var repuestosTemp = JsonConvert.DeserializeObject<List<RepuestoTemp>>(jsonContent);
                foreach (var repuestoTemp in repuestosTemp)
                {
                    Repuesto repuesto = new Repuesto(repuestoTemp.ID, repuestoTemp.Repuesto, repuestoTemp.Descripcion, repuestoTemp.Costo);
                    string repuestoString = new string(repuesto.Repuestos);
                    string repuestoDescripcion = new string(repuesto.Descripcion);

                    ListaRepuestos.Insertar(repuesto.Id, repuestoString, repuestoDescripcion, repuesto.Precio);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al leer el archivo JSON: {ex.Message}");
        }
    }

    private unsafe void ImprimirDatos()
    {
        Console.WriteLine("Usuarios:");
        lista.Imprimir();

        Console.WriteLine("Vehículos:");
        listaAutos.Mostrar();

        Console.WriteLine("Repuestos:");
        ListaRepuestos.Mostrar();
    }
}

// Clases temporales para deserializar el JSON
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
    public string Descripcion { get; set; }
    public double Costo { get; set; }
}