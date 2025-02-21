using Gtk;
using List;
using System;
using Usuarios;
using ListaDobleUnsafe;

public class Individuals : Window
{
    private Entry ID;
    private Entry Nombres;
    private Entry Apellidos;
    private Entry Correo;
    private Entry Contraseña;
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;

    public unsafe Individuals(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos) : base("Ingresos Individuales")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;

        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Usuario", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        EventBox ids = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids, 100, 150);

        ID = new Entry();
        ApplyFont(ID, "Arial 12");
        fixedContainer.Put(ID, 250, 150);

        EventBox nombres = CreateStyledLabel("Nombres:", "Arial 20");
        fixedContainer.Put(nombres, 100, 200);

        Nombres = new Entry();
        ApplyFont(Nombres, "Arial 12");
        fixedContainer.Put(Nombres, 250, 200);

        EventBox apellidos = CreateStyledLabel("Apellidos:", "Arial 20");
        fixedContainer.Put(apellidos, 100, 250);

        Apellidos = new Entry();
        ApplyFont(Apellidos, "Arial 12");
        fixedContainer.Put(Apellidos, 250, 250);

        EventBox correos = CreateStyledLabel("Correo:", "Arial 20");
        fixedContainer.Put(correos, 100, 300);

        Correo = new Entry();
        ApplyFont(Correo, "Arial 12");
        fixedContainer.Put(Correo, 250, 300);

        EventBox contraseñas = CreateStyledLabel("Contraseña:", "Arial 20");
        fixedContainer.Put(contraseñas, 100, 350);

        Contraseña = new Entry();
        ApplyFont(Contraseña, "Arial 12");
        fixedContainer.Put(Contraseña, 250, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 170, 400);

        Button button1 = new Button("Regresar");
        button1.Clicked += OnShowButtonClicked1;
        button1.SetSizeRequest(100, 50);
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 170, 470);

        Add(fixedContainer);
    }

    private unsafe void OnShowButtonClicked1(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Regresando");
        Console.WriteLine("Regresando");
        md.Run();
        md.Destroy();
        Menus menus = new Menus(lista, listaAutos);
        menus.ShowAll();
        this.Hide();
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

    private unsafe void OnShowButtonClicked(object? sender, System.EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ID.Text) || 
            string.IsNullOrWhiteSpace(Nombres.Text) || 
            string.IsNullOrWhiteSpace(Apellidos.Text) || 
            string.IsNullOrWhiteSpace(Correo.Text) || 
            string.IsNullOrWhiteSpace(Contraseña.Text))
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "Todos los campos deben estar llenos.");
            md.Run();
            md.Destroy();
        }
        else
        {
            if (int.TryParse(ID.Text, out int id))
            {
                // Verificar si el ID ya existe en la lista
                Nodo<Usuario>* temp = lista.Cabeza;
                while (temp != null)
                {
                    if (temp->Data.Id == id)
                    {
                        MessageDialog md2 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID ya existe. Por favor, ingrese otro ID.");
                        md2.Run();
                        md2.Destroy();
                        return;
                    }
                    temp = temp->Sig;
                }

                // Si el ID no existe, insertar el nuevo usuario
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, "Usuario guardado con éxito");
                md.Run();
                md.Destroy();
                lista.Insertar(new Usuario(id, Nombres.Text, Apellidos.Text, Correo.Text, Contraseña.Text));
                Console.WriteLine("Lista de Usuarios:");
                lista.Imprimir();
            }
            else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "El ID debe ser un número.");
                md.Run();
                md.Destroy();
            }
        }
    }
}