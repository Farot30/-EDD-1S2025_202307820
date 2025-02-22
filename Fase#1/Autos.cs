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

    public unsafe Autos(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas) : base("Ingreso de vehículos")
    {
        this.lista = lista;
        this.listaAutos = listaAutos;
        this.ListaRepuestos = ListasPiezas;
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Vehículos", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids1, 100, 150);

        ids = new Entry();
        ApplyFont(ids, "Arial 12");
        fixedContainer.Put(ids, 250, 150);

        EventBox ids_Usuario1 = CreateStyledLabel("ID Usuario:", "Arial 20");
        fixedContainer.Put(ids_Usuario1, 100, 200);

        ids_Usuario = new Entry();
        ApplyFont(ids_Usuario, "Arial 12");
        fixedContainer.Put(ids_Usuario, 250, 200);

        EventBox marcas = CreateStyledLabel("Marca:", "Arial 20");
        fixedContainer.Put(marcas, 100, 250);

        Marcas = new Entry();
        ApplyFont(Marcas, "Arial 12");
        fixedContainer.Put(Marcas, 250, 250);

        EventBox modelos = CreateStyledLabel("Modelo:", "Arial 20");
        fixedContainer.Put(modelos, 100, 300);

        Modelos = new Entry();
        ApplyFont(Modelos, "Arial 12");
        fixedContainer.Put(Modelos, 250, 300);

        EventBox placas = CreateStyledLabel("Placas:", "Arial 20");
        fixedContainer.Put(placas, 100, 350);

        Placas = new Entry();
        ApplyFont(Placas, "Arial 12");
        fixedContainer.Put(Placas, 250, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 100, 400);

        Button button1 = new Button("Regresar");
        button1.Clicked += Regresar;
        button1.SetSizeRequest(100, 50);
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 250, 400);

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

    private unsafe void Regresar(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Regresando");
        Console.WriteLine("Regresando");
        md.Run();
        md.Destroy();
        Menus menus = new Menus(lista, listaAutos, ListaRepuestos);
        menus.ShowAll();
        this.Hide();
    }

    private unsafe void OnShowButtonClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ids.Text) || 
            string.IsNullOrWhiteSpace(ids_Usuario.Text) || 
            string.IsNullOrWhiteSpace(Marcas.Text) || 
            string.IsNullOrWhiteSpace(Modelos.Text) || 
            string.IsNullOrWhiteSpace(Placas.Text))
        {
            MessageDialog md3 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor llene todos los campos");
            md3.Run();
            md3.Destroy();
        }
        else
        {
            if (int.TryParse(ids.Text, out int id) && int.TryParse(ids_Usuario.Text, out int idUsuario) && int.TryParse(Modelos.Text, out int modelos))
            {
                // Verificar si el ID ya existe en la lista
                if (IdExists(id))
                {
                    MessageDialog md4 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID ya existe. Por favor, ingrese otro ID.");
                    md4.Run();
                    md4.Destroy();
                    return;
                }
                // Verificar si el ID de usuario existe en la lista de usuarios
                if (!UsuarioExists(idUsuario))
                {
                    MessageDialog md2 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID de usuario no existe. Por favor, ingrese un ID de usuario válido.");
                    md2.Run();
                    md2.Destroy();
                    return;
                }

                listaAutos.Insertar(id, idUsuario, Marcas.Text, modelos, Placas.Text);
                MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardado");
                md1.Run();
                md1.Destroy();
                listaAutos.Mostrar();
            }
            else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "ID y ID Usuario deben ser números enteros");
                md.Run();
                md.Destroy();
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