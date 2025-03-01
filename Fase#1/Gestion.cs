using Gtk;
using List;
using Usuarios;
using ListaDobleUnsafe;
using Ediciones;
using Matriz;

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
    private MatrizDispersa<int> matriz;

    public unsafe Gestion(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila, MatrizDispersa<int> matrizDispersa) : base("Gestionar los Usuarios")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;
        this.matriz = matrizDispersa;

        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Gestionar los Usuarios", "Arial 20");
        fixedContainer.Put(labelesBox, 130, 50);

        EventBox idsVisionLabel = CreateStyledLabel("ID:", "Arial 12");
        fixedContainer.Put(idsVisionLabel, 50, 125);

        ID_Vision = new Entry();
        ApplyFont(ID_Vision, "Arial 12");
        fixedContainer.Put(ID_Vision, 90, 125);

        Button id_vision = new Button("Ver Usuario");
        id_vision.Clicked += VerUsuario;
        id_vision.SetSizeRequest(100, 50);
        ApplyFont(id_vision, "Arial 12");
        fixedContainer.Put(id_vision, 260, 115);

        EventBox idsEdicionLabel = CreateStyledLabel("ID:", "Arial 12");
        fixedContainer.Put(idsEdicionLabel, 50, 200);

        ID_Edisicón = new Entry();
        ApplyFont(ID_Edisicón, "Arial 12");
        fixedContainer.Put(ID_Edisicón, 90, 200);

        Button id_edición = new Button("Editar Usuario");
        id_edición.Clicked += EditarUsuario;
        id_edición.SetSizeRequest(100, 50);
        ApplyFont(id_edición, "Arial 12");
        fixedContainer.Put(id_edición, 260, 190);

        EventBox idsEliminacionLabel = CreateStyledLabel("ID:", "Arial 12");
        fixedContainer.Put(idsEliminacionLabel, 50, 275);

        ID_Eliminación = new Entry();
        ApplyFont(ID_Eliminación, "Arial 12");
        fixedContainer.Put(ID_Eliminación, 90, 275);

        Button id_eliminación = new Button("Eliminar Usuario");
        id_eliminación.Clicked += EliminarUsuario;
        id_eliminación.SetSizeRequest(100, 50);
        ApplyFont(id_eliminación, "Arial 12");
        fixedContainer.Put(id_eliminación, 260, 265);

        Add(fixedContainer);
    }

    private unsafe EventBox CreateStyledLabel(string text, string font)
    {
        Label label = new Label(text);
        ApplyFont(label, font);
        EventBox eventBox = new EventBox();
        eventBox.Add(label);
        return eventBox;
    }

    private unsafe void ApplyFont(Widget widget, string fontDescription)
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
            if(int.TryParse(ID_Edisicón.Text, out int id))
            {
                if(UsuarioExists(id)){
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Entrando a editar usuario"))
                    {
                        md.Run();
                        md.Destroy();
                    }
                    EdicionUsuario ediciones = new EdicionUsuario(lista, listaAutos, ListaRepuestos, colas, pilaFacturas, matriz);
                    ediciones.ShowAll();
                }
                else
                {
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "Usuario no encontrado"))
                    {
                        md.Run();
                        md.Destroy();
                    }
                }
            }
            
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
                    md.Destroy();
                }
                lista.Imprimir();
            }
            else
            {
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "Usuario no encontrado"))
                {
                    md.Run();
                    md.Destroy();
                }
            }
        }
        else
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "ID inválido"))
            {
                md.Run();
                md.Destroy();
            }
        }
    }

    private unsafe bool UsuarioExists(int idUsuario)
    {
        Nodo<Usuario>* actual = lista.Cabeza;
        while (actual != null)
        {
            if (actual->Data.Id == idUsuario)
            {
                return true;
            }
            actual = actual->Sig;
        }
        return false;
    }
}