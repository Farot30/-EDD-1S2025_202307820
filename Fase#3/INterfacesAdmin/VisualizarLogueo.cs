using Gtk;

namespace INterfacesAdmin.VisualizarLogueo
{
    public class Visualizacion_Logueo : Window
    {
        public Visualizacion_Logueo() : base("Visualización de Logueo")
        {
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Visualización de Logueo", "Arial 20");
            fixeds.Put(label, 50, 50);


            Add(fixeds);
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