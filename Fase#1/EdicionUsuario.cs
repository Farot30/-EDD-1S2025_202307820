using System;
using Gtk;
using Usuarios;
using List;
using ListaDobleUnsafe;

namespace Ediciones
{
    public class EdicionUsuario : Window
    {
        private Entry Id;
        private Entry Nombre;
        private Entry Apellido;
        private Entry Correo;
        private Entry Contraseña;
        
        private ListaSimple<Usuario> lista;
        private ListaDoblementeEnlazada listaAutos;
        private ListaCircular.ListaCircular ListaRepuestos;
        private Cola.Cola colas;
        private Pila.Pila pilaFacturas;

        public unsafe EdicionUsuario(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila) : base("Edición de Usuario")
        {
            this.listaAutos = listaAutos;
            this.lista = lista;
            this.ListaRepuestos = ListasPiezas;
            this.colas = colas;
            this.pilaFacturas = pila;

            SetDefaultSize(500, 600);
            SetPosition(WindowPosition.Center);

            Fixed fixedContainer = new Fixed();

            Label labelesBox = new Label("Edición de Usuario");
            fixedContainer.Put(labelesBox, 160, 50);

            Label ids = new Label("ID:");
            fixedContainer.Put(ids, 50, 150);

            Id = new Entry();
            fixedContainer.Put(Id, 250, 150);

            Label nombres = new Label("Nombres:");
            fixedContainer.Put(nombres, 50, 200);

            Nombre = new Entry();
            fixedContainer.Put(Nombre, 250, 200);

            Label apellidos = new Label("Apellidos:");
            fixedContainer.Put(apellidos, 50, 250);

            Apellido = new Entry();
            fixedContainer.Put(Apellido, 250, 250);

            Label correos = new Label("Correo:");
            fixedContainer.Put(correos, 50, 300);

            Correo = new Entry();
            fixedContainer.Put(Correo, 250, 300);

            Label contraseñas = new Label("Contraseña:");
            fixedContainer.Put(contraseñas, 50, 350);

            Contraseña = new Entry();
            fixedContainer.Put(Contraseña, 250, 350);

            Button button1 = new Button("Actualizar");
            button1.Clicked += Actualizar;
            button1.SetSizeRequest(100, 50);
            fixedContainer.Put(button1, 290, 400);

            Add(fixedContainer);
        }

        public unsafe void Actualizar(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Id.Text) ||
                string.IsNullOrWhiteSpace(Nombre.Text) ||
                string.IsNullOrWhiteSpace(Apellido.Text) ||
                string.IsNullOrWhiteSpace(Correo.Text) ||
                string.IsNullOrWhiteSpace(Contraseña.Text))
            {
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Llene todos los campos"))
                {
                    md.Run();
                }
            }
            else
            {
                if (int.TryParse(Id.Text, out int id))
                {
                    bool actualizado = lista.ModificarUsuario(id, Nombre.Text, Apellido.Text, Correo.Text, Contraseña.Text);
                    if (actualizado)
                    {
                        lista.ModificarUsuario(id, Nombre.Text, Apellido.Text, Correo.Text, Contraseña.Text);
                        using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario actualizado"))
                        {
                            md.Run();
                        }
                    }
                    else
                    {
                        using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado"))
                        {
                            md.Run();
                        }
                    }
                }
                else
                {
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "ID no valido"))
                    {
                        md.Run();
                    }
                }
            }
        }

    }
}