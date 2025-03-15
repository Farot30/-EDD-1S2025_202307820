using Gtk;
using System;
using InterfacesAdmin.menu;

namespace logiarse
{
    public class Logiarse : Window
    {
        private Entry correo;
        private Entry contrasena;
        public Logiarse() : base("Inicio de sesión")
        {
            SetDefaultSize(430, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixedcontainer = new Fixed();

            EventBox labelesBox = CreateStyledLabel("Inicio de sesión", "Arial 20");
            fixedcontainer.Put(labelesBox, 120, 50);


            EventBox SUS = CreateStyledLabel("Correo:", "Arial 14");
            fixedcontainer.Put(SUS, 100, 150);

            correo = new Entry();
            ApplyFont(correo, "Arial 12");
            fixedcontainer.Put(correo, 220, 140);

            EventBox Contras = CreateStyledLabel("Contrasenia:", "Arial 14");
            fixedcontainer.Put(Contras, 85, 250);

            contrasena = new Entry();
            ApplyFont(contrasena, "Arial 12");
            contrasena.Visibility = false;
            fixedcontainer.Put(contrasena, 220, 240);

            Button button = new Button("Validar");
            button.SetSizeRequest(100, 50); // Aumenta el tamaño del botón
            button.Clicked += Validacion;
            ApplyFont(button, "Arial 12");
            fixedcontainer.Put(button, 175, 320);
            
            Add(fixedcontainer);
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

        private void Validacion(object? sender, EventArgs e)
        {
            if ( correo.Text == "admin@usac.com" && contrasena.Text == "admin123")
            {
                Menus menu = new Menus();
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
    }
}