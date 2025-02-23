using Gtk;
using List;
using Usuarios;
using Vehiculos;
using ListaDobleUnsafe;

public class Autos : Window
{
    private Entry ids;
    private Entry ids_Usuario;
    private Entry Marcas;
    private Entry Modelos;
    private Entry Placas;
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular ListaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe Autos(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Ingreso de vehículos")
    {
        this.lista = lista;
        this.listaAutos = listaAutos;
        this.ListaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Ingreso de Vehículos");
        fixedContainer.Put(labelesBox, 160, 50);

        Label ids1 = new Label("ID:");
        fixedContainer.Put(ids1, 100, 150);

        ids = new Entry();
        fixedContainer.Put(ids, 250, 150);

        Label ids_Usuario1 = new Label("ID Usuario:");
        fixedContainer.Put(ids_Usuario1, 100, 200);

        ids_Usuario = new Entry();
        fixedContainer.Put(ids_Usuario, 250, 200);

        Label marcas = new Label("Marca:");
        fixedContainer.Put(marcas, 100, 250);

        Marcas = new Entry();
        fixedContainer.Put(Marcas, 250, 250);

        Label modelos = new Label("Modelo:");
        fixedContainer.Put(modelos, 100, 300);

        Modelos = new Entry();
        fixedContainer.Put(Modelos, 250, 300);

        Label placas = new Label("Placas:");
        fixedContainer.Put(placas, 100, 350);

        Placas = new Entry();
        fixedContainer.Put(Placas, 250, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        fixedContainer.Put(button, 100, 400);

        Button button1 = new Button("Regresar");
        button1.Clicked += Regresar;
        button1.SetSizeRequest(100, 50);
        fixedContainer.Put(button1, 250, 400);

        Add(fixedContainer);
    }

    private unsafe void Regresar(object? sender, EventArgs e)
    {
        using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Regresando"))
        {
            Console.WriteLine("Regresando");
            md.Run();
        }
        Menus menus = new Menus(lista, listaAutos, ListaRepuestos, colas, pilaFacturas);
        menus.ShowAll();
        this.Destroy(); // Cambia this.Hide() por this.Destroy() para destruir la ventana actual
    }

    private unsafe void OnShowButtonClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ids.Text) || 
            string.IsNullOrWhiteSpace(ids_Usuario.Text) || 
            string.IsNullOrWhiteSpace(Marcas.Text) || 
            string.IsNullOrWhiteSpace(Modelos.Text) || 
            string.IsNullOrWhiteSpace(Placas.Text))
        {
            using (MessageDialog md3 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor llene todos los campos"))
            {
                md3.Run();
            }
        }
        else
        {
            if (int.TryParse(ids.Text, out int id) && int.TryParse(ids_Usuario.Text, out int idUsuario) && int.TryParse(Modelos.Text, out int modelos))
            {
                // Verificar si el ID ya existe en la lista
                if (IdExists(id))
                {
                    using (MessageDialog md4 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID ya existe. Por favor, ingrese otro ID."))
                    {
                        md4.Run();
                    }
                    return;
                }
                // Verificar si el ID de usuario existe en la lista de usuarios
                if (!UsuarioExists(idUsuario))
                {
                    using (MessageDialog md2 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID de usuario no existe. Por favor, ingrese un ID de usuario válido."))
                    {
                        md2.Run();
                    }
                    return;
                }

                listaAutos.Insertar(id, idUsuario, Marcas.Text, modelos, Placas.Text);
                using (MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardado"))
                {
                    md1.Run();
                }
                listaAutos.Mostrar();
            }
            else
            {
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "ID y ID Usuario deben ser números enteros"))
                {
                    md.Run();
                }
            }
        }
    }

    private unsafe bool IdExists(int id)
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