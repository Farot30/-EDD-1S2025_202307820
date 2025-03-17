using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;

namespace InterfacesAdmin.VisualizarRepuestos
{
    public class VisualizarRepuesto : Window
    {

        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        public VisualizarRepuesto (ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto) : base("Visualización de Repuestos")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 350);
            Fixed fixedcontainer = new Fixed();
            
            EventBox Titulo = CreateStyledLabel("Visualización de Repuestos", "Arial 20");
            fixedcontainer.Put(Titulo, 100, 50);

            Add(fixedcontainer);
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