using Gtk;
using System;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;
using INterfacesAdmin.Menu;
using InterfacesUsuario.MenuUsuario;

namespace Logiarse
{
    public class Logiarses : Window
    {
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private ListaDeListas grafoNodiri;

        private Entry Correo;
        private Entry Contrasena;
        private Button IniciarSesion;
        private EventBox Usus;
        private EventBox Contras;
        public Logiarses(ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Inicio de sesión")
        {
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Bienvenido a Logiarse", "Arial 20");
            fixeds.Put(label, 50, 50);

            Usus = CreateStyledLabel("Correo", "Arial 12");
            fixeds.Put(Usus, 50, 100);

            Correo = new Entry();
            Correo.SetSizeRequest(200, 30);
            fixeds.Put(Correo, 50, 130);
            ApplyFont(Correo, "Arial 12");

            Contras = CreateStyledLabel("Contraseña", "Arial 12");
            fixeds.Put(Contras, 50, 170);

            Contrasena = new Entry();
            Contrasena.SetSizeRequest(200, 30);
            Contrasena.Visibility = false;
            fixeds.Put(Contrasena, 50, 200);
            ApplyFont(Contrasena, "Arial 12");

            IniciarSesion = new Button("Iniciar sesión");
            IniciarSesion.SetSizeRequest(200, 30);
            IniciarSesion.Clicked += Validación;
            fixeds.Put(IniciarSesion, 50, 240);
            ApplyFont(IniciarSesion, "Arial 12");

            Add(fixeds);
            
        }

        private void Validación(object? sender, EventArgs e)
        {

            if(Correo.Text == "admin@usac.com" || Contrasena.Text == "admin123")
            {
                Menus menu = new Menus(ListaDobleVehiculos, listaRepuesto, ListaServicios, grafoNodiri);
                menu.ShowAll();
                this.Hide();
            }
            else
            {
                MessageDialog md1 = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario Incorrecto o Contraseña Incorrecta");
                md1.Run();
                md1.Hide();
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