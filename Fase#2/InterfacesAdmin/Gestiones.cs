using Gtk;
using System;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using InterfacesAdmin.UsuarioG;
using InterfacesAdmin.VehiculoG;

namespace InterfacesAdmin.Gestion
{
    public class Gestiones : Window
    {

        private Button Usuarios;
        private Button Vehiculos;
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues  listaRepuesto;
        public Gestiones(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo lIstaAuto, AVLTREErepues ListaRepuesto) : base("Gestiones de usuarios y vehículos")
        {
            ListaUsuarioSimple = listaUsuario;
            ListaDobleVehiculos = lIstaAuto;
            listaRepuesto = ListaRepuesto;
            SetPosition(WindowPosition.Center); 
            SetDefaultSize(350, 350);
            Fixed fixedcontainer = new Fixed();
            
            EventBox Titulo = CreateStyledLabel("Gestiones de usuarios y vehículos", "Arial 20");
            fixedcontainer.Put(Titulo, 60, 50);
            
            Usuarios = new Button("Gestión de usuarios");
            Usuarios.SetSizeRequest(150, 50);
            Usuarios.Clicked += Usuariosg;
            ApplyFont(Usuarios, "Arial 12");
            fixedcontainer.Put(Usuarios, 180, 100);

            Vehiculos = new Button("Gestión de vehículos");
            Vehiculos.SetSizeRequest(150, 50);
            Vehiculos.Clicked += Vehiculosg;
            ApplyFont(Vehiculos, "Arial 12");
            fixedcontainer.Put(Vehiculos, 180, 160);
            
            Add(fixedcontainer);
        }

        private void Usuariosg(object? sender, EventArgs e)
        {
            UsuariosG usuariosG = new UsuariosG(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto);
            usuariosG.DeleteEvent += delegate { usuariosG.Destroy(); };
            usuariosG.ShowAll();
        }

        private void Vehiculosg(object? sender, EventArgs e)
        {
            VehiculosG vehiculosG = new VehiculosG(ListaUsuarioSimple, ListaDobleVehiculos, listaRepuesto);
            vehiculosG.DeleteEvent += delegate { vehiculosG.Destroy(); };
            vehiculosG.ShowAll();
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

        private EventBox CreateStyledLabel(string text, string font)
        {
            Label label = new Label(text);
            ApplyFont(label, font);
            EventBox eventBox = new EventBox();
            eventBox.Add(label);
            return eventBox;
        }


    }
    
}