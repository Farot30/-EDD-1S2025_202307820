using System;
using Gtk;
using Usuarios;
using List;
using ListaDobleUnsafe;
using Matriz;

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
        private MatrizDispersa<int> matriz;

        public unsafe EdicionUsuario(ListaSimple<Usuario> lista, ListaDoblementeEnlazada listaAutos, ListaCircular.ListaCircular ListasPiezas, Cola.Cola colas, Pila.Pila pila, MatrizDispersa<int> matrizDispersa) : base("Edición de Usuario")
        {
            this.listaAutos = listaAutos;
            this.lista = lista;
            this.ListaRepuestos = ListasPiezas;
            this.colas = colas;
            this.pilaFacturas = pila;
            this.matriz = matrizDispersa;

            SetDefaultSize(500, 600);
            SetPosition(WindowPosition.Center);

            Fixed fixedContainer = new Fixed();

            EventBox labelesBox = CreateStyledLabel("Edición de Usuario", "Arial 20");
            fixedContainer.Put(labelesBox, 160, 50);

            EventBox ids = CreateStyledLabel("ID:", "Arial 12");
            Id = new Entry();
            ApplyFont(Id, "Arial 12");
            fixedContainer.Put(ids, 50, 150);
            fixedContainer.Put(Id, 250, 150);

            EventBox nombres = CreateStyledLabel("Nombres:", "Arial 12");
            Nombre = new Entry();
            ApplyFont(Nombre, "Arial 12");
            fixedContainer.Put(nombres, 50, 200);
            fixedContainer.Put(Nombre, 250, 200);

            EventBox apellidos = CreateStyledLabel("Apellidos:", "Arial 12");
            Apellido = new Entry();
            ApplyFont(Apellido, "Arial 12");
            fixedContainer.Put(apellidos, 50, 250);
            fixedContainer.Put(Apellido, 250, 250);

            EventBox correos = CreateStyledLabel("Correo:", "Arial 12");
            Correo = new Entry();
            ApplyFont(Correo, "Arial 12");
            fixedContainer.Put(correos, 50, 300);
            fixedContainer.Put(Correo, 250, 300);

            EventBox contraseñas = CreateStyledLabel("Contraseña:", "Arial 12");
            Contraseña = new Entry();
            ApplyFont(Contraseña, "Arial 12");
            fixedContainer.Put(contraseñas, 50, 350);
            fixedContainer.Put(Contraseña, 250, 350);

            Button button1 = new Button("Actualizar");
            button1.Clicked += Actualizar;
            button1.SetSizeRequest(100, 50);
            ApplyFont(button1, "Arial 12");
            fixedContainer.Put(button1, 290, 400);

            Add(fixedContainer);
            ShowAll();
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
                    md.Destroy();
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
                            md.Destroy();
                        }
                    }
                    else
                    {
                        using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado"))
                        {
                            md.Run();
                            md.Destroy();
                        }
                    }
                }
                else
                {
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "ID no valido"))
                    {
                        md.Run();
                        md.Destroy();
                    }
                }
            }
        }
    }
}