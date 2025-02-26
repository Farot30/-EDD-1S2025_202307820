using Gtk;
using List;
using System;
using Usuarios;
using ListaDobleUnsafe;
using Repuestos;
using ListaCircular;

public class Individuals : Window
{
    private Entry ID;
    private Entry Nombres;
    private Entry Apellidos;
    private Entry Correo;
    private Entry Contraseña;
    private ListaSimple<Usuario> lista;
    private ListaDoblementeEnlazada listaAutos;
    private ListaCircular.ListaCircular listaRepuestos;
    private Cola.Cola colas;
    private Pila.Pila pilaFacturas;

    public unsafe Individuals(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Ingresos Individuales")
    {
        this.listaAutos = listaAutos;
        this.lista = lista;
        this.listaRepuestos = ListasPiezas;
        this.colas = colas;
        this.pilaFacturas = pila;

        SetDefaultSize(500, 550);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        Label labelesBox = new Label("Ingreso de Usuario");
        fixedContainer.Put(labelesBox, 160, 50);

        Label ids = new Label("ID:");
        fixedContainer.Put(ids, 100, 150);

        ID = new Entry();
        fixedContainer.Put(ID, 250, 150);

        Label nombres = new Label("Nombres:");
        fixedContainer.Put(nombres, 100, 200);

        Nombres = new Entry();
        fixedContainer.Put(Nombres, 250, 200);

        Label apellidos = new Label("Apellidos:");
        fixedContainer.Put(apellidos, 100, 250);

        Apellidos = new Entry();
        fixedContainer.Put(Apellidos, 250, 250);

        Label correos = new Label("Correo:");
        fixedContainer.Put(correos, 100, 300);

        Correo = new Entry();
        fixedContainer.Put(Correo, 250, 300);

        Label contraseñas = new Label("Contraseña:");
        fixedContainer.Put(contraseñas, 100, 350);

        Contraseña = new Entry();
        fixedContainer.Put(Contraseña, 250, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        fixedContainer.Put(button, 170, 400);

        Add(fixedContainer);
    }

    private unsafe void OnShowButtonClicked(object? sender, System.EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ID.Text) || 
            string.IsNullOrWhiteSpace(Nombres.Text) || 
            string.IsNullOrWhiteSpace(Apellidos.Text) || 
            string.IsNullOrWhiteSpace(Correo.Text) || 
            string.IsNullOrWhiteSpace(Contraseña.Text))
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "Todos los campos deben estar llenos."))
            {
                md.Run();
            }
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
                        using (MessageDialog md2 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "El ID ya existe. Por favor, ingrese otro ID."))
                        {
                            md2.Run();
                        }
                        return;
                    }
                    temp = temp->Sig;
                }

                // Si el ID no existe, insertar el nuevo usuario
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, "Usuario guardado con éxito"))
                {
                    md.Run();
                }
                lista.Insertar(new Usuario(id, Nombres.Text, Apellidos.Text, Correo.Text, Contraseña.Text));
                Console.WriteLine("Lista de Usuarios:");
                lista.Imprimir();
            }
            else
            {
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "El ID debe ser un número."))
                {
                    md.Run();
                }
            }
        }
    }
}