using Gtk;
using System;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using Clases.Usuarios;

namespace INterfacesAdmin.VerUsuario
{
    public class VerUsuarios : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ListaDeListas grafoNodiri;
        private EventBox Id;
        private EventBox Nombres;
        private EventBox Apellidos;
        private EventBox Correos;
        private EventBox Edades;
        private Entry ID;
        private Label Nombre;
        private Label Apellido;
        private Label Correo;
        private Label Edad;
        private Button Buscar;
        public VerUsuarios(Blockchain BlockchainUsuarios, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Ver Usuarios")
        {
            blockchainUsuarios = BlockchainUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Ver Usuarios", "Arial 20");
            fixeds.Put(label, 100, 50);

            Id = CreateStyledLabel("ID:", "Arial 12");
            fixeds.Put(Id, 50, 100);

            ID = new Entry();
            ID.SetSizeRequest(200, 30);
            ApplyFont(ID, "Arial 12");
            fixeds.Put(ID, 150, 100);

            Nombres = CreateStyledLabel("Nombre:", "Arial 12");
            fixeds.Put(Nombres, 50, 140);

            Nombre = new Label();
            Nombre.SetSizeRequest(200, 30);
            ApplyFont(Nombre, "Arial 12");
            fixeds.Put(Nombre, 150, 140);

            Apellidos = CreateStyledLabel("Apellido:", "Arial 12");
            fixeds.Put(Apellidos, 50, 180);

            Apellido = new Label();
            Apellido.SetSizeRequest(200, 30);
            ApplyFont(Apellido, "Arial 12");
            fixeds.Put(Apellido, 150, 180);

            Correos = CreateStyledLabel("Correo:", "Arial 12");
            fixeds.Put(Correos, 50, 220);

            Correo = new Label();
            Correo.SetSizeRequest(200, 30);
            ApplyFont(Correo, "Arial 12");
            fixeds.Put(Correo, 150, 220);

            Edades = CreateStyledLabel("Edad:", "Arial 12");
            fixeds.Put(Edades, 50, 260);

            Edad = new Label();
            Edad.SetSizeRequest(200, 30);
            ApplyFont(Edad, "Arial 12");
            fixeds.Put(Edad, 150, 260);

            Buscar = new Button("Buscar");
            Buscar.SetSizeRequest(200, 30);
            Buscar.Clicked += BuscarUsuario;
            ApplyFont(Buscar, "Arial 12");
            fixeds.Put(Buscar, 50, 340);

            Add(fixeds);
        }

        private void BuscarUsuario(object? sender, EventArgs e)
        {
            // Obtener el ID ingresado
            if (int.TryParse(ID.Text, out int id))
            {
                // Buscar el usuario en la blockchain
                Usuario? usuario = blockchainUsuarios.BuscarUsuarioPorId(id);

                if (usuario != null)
                {
                    // Mostrar los datos del usuario en los labels
                    Nombre.Text = usuario.Value.Nombre;
                    Apellido.Text = usuario.Value.Apellido;
                    Correo.Text = usuario.Value.Correo;
                    Edad.Text = usuario.Value.Edad.ToString();
                }
                else
                {
                    // Mostrar mensaje si no se encuentra el usuario
                    using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado."))
                    {
                        md.Run();
                        md.Destroy();
                    }

                    // Limpiar los labels
                    Nombre.Text = string.Empty;
                    Apellido.Text = string.Empty;
                    Correo.Text = string.Empty;
                    Edad.Text = string.Empty;
                }
            }
            else
            {
                // Mostrar mensaje si el ID no es válido
                using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Por favor, ingrese un ID válido."))
                {
                    md.Run();
                    md.Destroy();
                }
            }
        }

        private EventBox CreateStyledLabel(string text, string font)
        {
            Label label = new Label(text);
            ApplyFont(label, font);
            EventBox eventBox = new EventBox();
            eventBox.Add(label);
            return eventBox;
        }

        private  void ApplyFont(Widget widget, string fontDescription)
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
    }
}