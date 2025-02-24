using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using Ediciones;

public class Gestion : Window
{
    private Entry ID_Vision;
    private Entry ID_Edisicón;
    private Entry ID_Eliminación;
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe Gestion(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Gestionar los Usuarios")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Gestionar los Usuarios");
        fixedContainer.Put(labelesBox, 130, 50);

        Button id_vision = new Button("Ver Usuario");
        id_vision.Clicked += VerUsuario;
        id_vision.SetSizeRequest(100, 50);
        fixedContainer.Put(id_vision, 260, 115);

        ID_Vision = new Entry();
        fixedContainer.Put(ID_Vision, 90, 125);

        Button id_edición = new Button("Editar Usuario");
        id_edición.Clicked += EditarUsuario;
        id_edición.SetSizeRequest(100, 50);
        fixedContainer.Put(id_edición, 260, 190);

        ID_Edisicón = new Entry();
        fixedContainer.Put(ID_Edisicón, 90, 200);

        Button id_eliminación = new Button("Eliminar Usuario");
        id_eliminación.Clicked += EliminarUsuario;
        id_eliminación.SetSizeRequest(100, 50);
        fixedContainer.Put(id_eliminación, 260, 265);

        ID_Eliminación = new Entry();
        fixedContainer.Put(ID_Eliminación, 90, 275);

        Add(fixedContainer);
    }

    private unsafe void VerUsuario(object? sender, EventArgs e)
    {
        MessageDialog md;
        if (int.TryParse(ID_Vision.Text, out int id))
        {
            Nodo<Usuario>* usus = lista.Cabeza;
            while (usus != null)
            {
                if (usus->Data.Id == id)
                {
                    string Nombre = new string(usus->Data.Nombre);
                    string Apellido = new string(usus->Data.Apellido);
                    string Correo = new string(usus->Data.Correo);
                    string Contraseña = new string(usus->Data.Contraseña);
                    string mensaje = $"Usuario encontrado \nID: {usus->Data.Id}\nNombre: {Nombre}\nApellido: {Apellido}\nCorreo: {Correo}\nContraseña: {Contraseña}\n\nVehículos:\n";

                    // Recorrer la lista de vehículos y agregar los vehículos del usuario al mensaje
                    Vehiculos.Node* vehiculo = listaAutos.Head;
                    bool tieneVehiculos = false;
                    while (vehiculo != null)
                    {
                        if (vehiculo->IdUsuario == id)
                        {
                            tieneVehiculos = true;
                            mensaje += $"ID Vehículo: {vehiculo->Id}, Marca: {new string(vehiculo->Marca)}, Modelo: {vehiculo->Modelo}, Placa: {new string(vehiculo->Placa)}\n";
                        }
                        vehiculo = vehiculo->Next;
                    }

                    if (!tieneVehiculos)
                    {
                        mensaje += "No tiene vehículos registrados";
                    }

                    md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje);
                    md.Run();
                    md.Destroy();
                    return;
                }
                usus = usus->Sig;
            }

            md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado");
            md.Run();
            md.Destroy();
        }
        else
        {
            md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "ID inválido");
            md.Run();
            md.Destroy();
        }
    }

    private unsafe void EditarUsuario(object? sender, EventArgs e)
    {
        try
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Entrando a editar usuario"))
            {
                md.Run();
            }
            EdicionUsuario ediciones = new EdicionUsuario(lista, listaAutos, ListaRepuestos, colas, pilaFacturas);
            ediciones.ShowAll();
        }
        catch (Exception ex)
        {
            using (MessageDialog errorDialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, $"Error: {ex.Message}"))
            {
                errorDialog.Run();
            }
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private unsafe void EliminarUsuario(object? sender, EventArgs e)
    {
        if (int.TryParse(ID_Eliminación.Text, out int id))
        {
            // Verificar si el usuario existe
            Nodo<Usuario>* usus = lista.Cabeza;
            bool usuarioEncontrado = false;
            while (usus != null)
            {
                if (usus->Data.Id == id)
                {
                    usuarioEncontrado = true;
                    break;
                }
                usus = usus->Sig;
            }

            if (usuarioEncontrado)
            {
                lista.Eliminar(id);
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario eliminado con éxito"))
                {
                    md.Run();
                }
                lista.Imprimir();
            }
            else
            {
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "Usuario no encontrado"))
                {
                    md.Run();
                }
            }
        }
        else
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "ID inválido"))
            {
                md.Run();
            }
        }
    }
}