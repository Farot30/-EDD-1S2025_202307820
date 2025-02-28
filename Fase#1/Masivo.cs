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

        EventBox labelesBox = CreateStyledLabel("Cargas masivas", "Arial 20");
        fixedContainer.Put(labelesBox, 200, 50);

        Button button1 = new Button("Cargar");
        button1.Clicked += Cargaras;
        ApplyFont(button1, "Arial 12");
        fixedContainer.Put(button1, 200, 150);

        Add(fixedContainer);
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
        else if (widget is Entry entry)
        {
            entry.ModifyFont(fontDesc);
        }
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
                    md.Destroy();
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
                    if (!ExisteUsuario(usuarioTemp.ID))
                    {
                        Usuario usuario = new Usuario(usuarioTemp.ID, usuarioTemp.Nombres, usuarioTemp.Apellidos, usuarioTemp.Correo, usuarioTemp.Contrasenia);
                        lista.Insertar(usuario);
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
                    if (!ExisteVehiculo(vehiculoTemp.ID))
                    {
                        listaAutos.Insertar(vehiculoTemp.ID, vehiculoTemp.ID_Usuario, vehiculoTemp.Marca, int.Parse(vehiculoTemp.Modelo), vehiculoTemp.Placa);
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
                    if (!ExisteRepuesto(repuestoTemp.ID))
                    {
                        Repuesto repuesto = new Repuesto(repuestoTemp.ID, repuestoTemp.Repuesto, repuestoTemp.Detalles, repuestoTemp.Costo);
                        string repuestoString = new string(repuesto.Repuestos);
                        string repuestoDescripcion = new string(repuesto.Descripcion);

                        ListaRepuestos.Insertar(repuesto.Id, repuestoString, repuestoDescripcion, repuesto.Precio);
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

    private unsafe bool ExisteUsuario(int id)
    {
        Nodo<Usuario>* actual = lista.Cabeza;
        while (actual != null)
        {
            if (actual->Data.Id == id)
            {
                return true;
            }
            actual = actual->Sig;
        }
        return false;
    }

    private unsafe bool ExisteVehiculo(int id)
    {
        Vehiculos.Node* actual = listaAutos.Head;
        while (actual != null)
        {
            if (actual->Id == id)
            {
                return true;
            }
            actual = actual->Next;
        }
        return false;
    }

    private unsafe bool ExisteRepuesto(int id)
    {
        Repuesto* actual = ListaRepuestos.Head;
        if (actual == null) return false;
        do
        {
            if (actual->Id == id)
            {
                return true;
            }
            actual = actual->Next;
        } while (actual != ListaRepuestos.Head);
        return false;
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
    public string Detalles { get; set; }
    public double Costo { get; set; }
}