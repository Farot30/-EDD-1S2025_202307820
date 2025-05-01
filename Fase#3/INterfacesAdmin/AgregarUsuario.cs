using Gtk;
using System;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.ArbolMerkles;
using ListasArboles.GrafoNodiri;
using Clases.Usuarios;

namespace INterfacesAdmin.AgregarUsuarios
{
    public class AgregarUsuario : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ArbolMerklesFac ListaFacturas;
        private ListaDeListas grafoNodiri;
        private EventBox Id;
        private EventBox Nombres;
        private EventBox Apellidos;
        private EventBox Correos;
        private EventBox Edades;
        private EventBox Contrasenias;

        private Entry ID;
        private Entry Nombre;
        private Entry Apellido;
        private Entry Correo;
        private Entry Edad;
        private Entry Contrasenia;
        private Button Insertar;
        public AgregarUsuario(Blockchain BlockchainUsuarios, ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ArbolMerklesFac ListaFactura, ListaDeListas Grafo) : base("Agregar Usuario")
        {
            blockchainUsuarios = BlockchainUsuarios;
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            ListaFacturas = ListaFactura;
            grafoNodiri = Grafo;

            SetDefaultSize(400, 450);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Agregar Usuario", "Arial 20");
            fixeds.Put(label, 100, 50);

            Id = CreateStyledLabel("ID:", "Arial 12");
            fixeds.Put(Id, 50, 100);

            ID = new Entry();
            ID.SetSizeRequest(200, 30);
            ApplyFont(ID, "Arial 12");
            fixeds.Put(ID, 150, 100);

            Nombres = CreateStyledLabel("Nombre:", "Arial 12");
            fixeds.Put(Nombres, 50, 140);

            Nombre = new Entry();
            Nombre.SetSizeRequest(200, 30);
            ApplyFont(Nombre, "Arial 12");
            fixeds.Put(Nombre, 150, 140);

            Apellidos = CreateStyledLabel("Apellido:", "Arial 12");
            fixeds.Put(Apellidos, 50, 180);

            Apellido = new Entry();
            Apellido.SetSizeRequest(200, 30);
            ApplyFont(Apellido, "Arial 12");
            fixeds.Put(Apellido, 150, 180);

            Correos = CreateStyledLabel("Correo:", "Arial 12");
            fixeds.Put(Correos, 50, 220);

            Correo = new Entry();
            Correo.SetSizeRequest(200, 30);
            ApplyFont(Correo, "Arial 12");
            fixeds.Put(Correo, 150, 220);

            Edades = CreateStyledLabel("Edad:", "Arial 12");
            fixeds.Put(Edades, 50, 260);

            Edad = new Entry();
            Edad.SetSizeRequest(200, 30);
            ApplyFont(Edad, "Arial 12");
            fixeds.Put(Edad, 150, 260);

            Contrasenias = CreateStyledLabel("Contraseña:", "Arial 12");
            fixeds.Put(Contrasenias, 50, 300);

            Contrasenia = new Entry();
            Contrasenia.SetSizeRequest(200, 30);
            ApplyFont(Contrasenia, "Arial 12");
            fixeds.Put(Contrasenia, 150, 300);

            Insertar = new Button("Insertar");
            Insertar.SetSizeRequest(200, 30);
            Insertar.Clicked += InsertarUsuario;
            ApplyFont(Insertar, "Arial 12");
            fixeds.Put(Insertar, 150, 340);


            Add(fixeds);
        }

        private void InsertarUsuario(object? sender, EventArgs e)
        {
            try
            {
                // Obtener los valores de los campos de entrada
                int id = int.Parse(ID.Text);
                string nombre = Nombre.Text;
                string apellido = Apellido.Text;
                string correo = Correo.Text;
                int edad = int.Parse(Edad.Text);
                string contrasenia = Contrasenia.Text;

                // Validar que los campos no estén vacíos
                if (!string.IsNullOrEmpty(nombre) || !string.IsNullOrEmpty(apellido) || !string.IsNullOrEmpty(correo) || !string.IsNullOrEmpty(contrasenia))
                {
                    // Validar que el ID no exista en la blockchain
                        if (!blockchainUsuarios.VerificarId(id))
                        {
                            // Validar que el correo no exista en la blockchain
                            if (!blockchainUsuarios.VerificarCorreo(correo))
                            {
                                string contrasenaEncriptada = Blockchain.EncriptacionSHa256(contrasenia);
                                Usuario nuevoUsuarios = new Usuario(id, nombre, apellido, correo, edad, contrasenaEncriptada);

                                // Agregar el usuario a la blockchain
                                blockchainUsuarios.AddBlock(nuevoUsuarios);

                                // Mostrar mensaje de éxito
                                MostrarMensaje("Usuario agregado exitosamente.");

                                // Limpiar los campos de entrada
                                ID.Text = string.Empty;
                                Nombre.Text = string.Empty;
                                Apellido.Text = string.Empty;
                                Correo.Text = string.Empty;
                                Edad.Text = string.Empty;
                                Contrasenia.Text = string.Empty;
                            }
                            else
                            {
                                MostrarMensaje($"El correo {correo} ya existe en la blockchain.");
                            }
                        }
                        else
                        {
                            MostrarMensaje($"El ID {id} ya existe en la blockchain.");
                        }
                }
                else
                {
                    MostrarMensaje("Por favor, complete todos los campos.");
                }
            }
            catch (FormatException)
            {
                MostrarMensaje("Por favor, ingrese valores válidos en los campos.");
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Ocurrió un error al insertar el usuario: {ex.Message}");
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje))
            {
                md.Run();
                md.Destroy();
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