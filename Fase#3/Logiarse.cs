using Gtk;
using System;
using System.Diagnostics;
using Listas.Blockchains;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using Clases.Usuarios;
using INterfacesAdmin.Menu;
using InterfacesUsuario.MenuUsuario;
using Programa;
using Clases.ControlLogeo;

namespace Logiarse
{
    public class Logiarses : Window
    {
        private Blockchain blockchainUsuarios;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ListaDeListas grafoNodiri;

        private Entry Correo;
        private Entry Contrasena;
        private Button IniciarSesion;
        private EventBox Usus;
        private EventBox Contras;
        public Logiarses(Blockchain BlockchainUsuarios, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Inicio de sesión")
        {
            blockchainUsuarios = BlockchainUsuarios;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Loginis", "Arial 20");
            fixeds.Put(label, 150, 50);

            Usus = CreateStyledLabel("Correo", "Arial 12");
            fixeds.Put(Usus, 165, 100);

            Correo = new Entry();
            Correo.SetSizeRequest(200, 30);
            fixeds.Put(Correo, 100, 130);
            ApplyFont(Correo, "Arial 12");

            Contras = CreateStyledLabel("Contraseña", "Arial 12");
            fixeds.Put(Contras, 150, 170);

            Contrasena = new Entry();
            Contrasena.SetSizeRequest(200, 30);
            Contrasena.Visibility = false;
            fixeds.Put(Contrasena, 100, 200);
            ApplyFont(Contrasena, "Arial 12");

            IniciarSesion = new Button("Iniciar sesión");
            IniciarSesion.SetSizeRequest(200, 30);
            IniciarSesion.Clicked += Validación;
            fixeds.Put(IniciarSesion, 100, 270);
            ApplyFont(IniciarSesion, "Arial 12");

            Add(fixeds);
            
        }

        private void Validación(object? sender, EventArgs e)
        {
            string correo = Correo.Text;
            string contrasena = Contrasena.Text;

            if (correo == "admin@usac.com" && contrasena == "admin123")
            {

                Menus menu = new Menus(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
                menu.ShowAll();
                this.Hide();
            }
            else if (blockchainUsuarios.ValidarUsuario(correo, contrasena))
            {
                // Obtener el ID del usuario
                Usuario? usuario = blockchainUsuarios.BuscarUsuarioPorCorreo(correo);
                if (usuario != null)
                {
                    // Registrar el inicio de sesión del usuario
                    MainClass.LogUsuarios.Add(new LogUsuario(correo, DateTime.Now));

                    // Redirigir al menú de usuario con el ID del usuario
                    MenuUsuarios menuUsuario = new MenuUsuarios(blockchainUsuarios, ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri, usuario.Value.Id);
                    menuUsuario.ShowAll();
                    this.Hide();
                }
            }
            else
            {
                // Mostrar mensaje de error
                MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario o contraseña incorrectos.");
                md1.Run();
                md1.Destroy();
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