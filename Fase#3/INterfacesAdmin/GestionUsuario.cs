using Gtk;
using System;

namespace INterfacesAdmin.GesionUsuarios
{
    public class GestionUsuario : Window
    {
        private Button btnAgregarUsuario;
        private Button VisualizarUsuario;
        public GestionUsuario() : base("Gestion de Usuarios")
        {
            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Gestión de Usuarios", "Arial 20");
            fixeds.Put(label, 50, 50);

            btnAgregarUsuario = new Button("Agregar Usuario");
            btnAgregarUsuario.SetSizeRequest(200, 30);
            btnAgregarUsuario.Clicked += AgregarUsuario;
            ApplyFont(btnAgregarUsuario, "Arial 12");
            fixeds.Put(btnAgregarUsuario, 50, 100);

            VisualizarUsuario = new Button("Visualizar Usuarios");
            VisualizarUsuario.SetSizeRequest(200, 30);
            VisualizarUsuario.Clicked += VisualizarUsuarios;
            ApplyFont(VisualizarUsuario, "Arial 12");
            fixeds.Put(VisualizarUsuario, 50, 140);

            Add(fixeds);

        }

        private void AgregarUsuario(object sender, EventArgs e)
        {
            // Implementar la lógica para agregar un usuario
            Console.WriteLine("Agregar Usuario");
        }

        private void VisualizarUsuarios(object sender, EventArgs e)
        {
            // Implementar la lógica para visualizar usuarios
            Console.WriteLine("Visualizar Usuarios");
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