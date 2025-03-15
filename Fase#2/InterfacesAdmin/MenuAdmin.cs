using System;
using Gtk;

namespace InterfacesAdmin.menu
{
    public class Menus : Window
    {

        private Button masivos;
        private Button Gestion_Entidades;
        private Button Actualizacion_Repuestos;
        private Button Visualizacion_repuestos;
        private Button Generar_servicio;
        private Button Control_Logueo;
        private Button Generar_Reporte;
        public Menus() : base("Administrador")
        {

            SetDefaultSize(500, 550);
            SetPosition(WindowPosition.Center);

            Fixed fixedcontainer = new Fixed(); 

            EventBox labelesBox = CreateStyledLabel("Menu Administrador", "Arial 20");
            fixedcontainer.Put(labelesBox, 140, 50);

            masivos = new Button("Cargas Masivas");
            masivos.SetSizeRequest(150,50);
            ApplyFont(masivos, "Arial 12");
            fixedcontainer.Put(masivos, 180, 100);

            Gestion_Entidades = new Button("Gestión de Entidades");
            Gestion_Entidades.SetSizeRequest(150,50);
            ApplyFont(Gestion_Entidades, "Arial 12");
            fixedcontainer.Put(Gestion_Entidades, 165, 160);

            Actualizacion_Repuestos = new Button("Actualización de Repuestos");
            Actualizacion_Repuestos.SetSizeRequest(150, 50);
            ApplyFont(Actualizacion_Repuestos, "Arial 12");
            fixedcontainer.Put(Actualizacion_Repuestos, 150, 220);

            Visualizacion_repuestos = new Button("Visualización de Repuestos");
            Visualizacion_repuestos.SetSizeRequest(150, 50);
            ApplyFont(Visualizacion_repuestos, "Arual 12");
            fixedcontainer.Put(Visualizacion_repuestos, 150, 280);

            Generar_servicio = new Button("Generación de Servicios");
            Generar_servicio.SetSizeRequest(150, 50);
            ApplyFont(Generar_servicio, "Arial 12");
            fixedcontainer.Put(Generar_servicio, 160, 340);

            Control_Logueo = new Button("Control de Logueo");
            Control_Logueo.SetSizeRequest(150, 50);
            ApplyFont(Control_Logueo, "Arial 12");
            fixedcontainer.Put(Control_Logueo, 175, 400);

            Generar_Reporte = new Button("Generar Reporte");
            Generar_Reporte.SetSizeRequest(150, 50);
            ApplyFont(Generar_Reporte, "Arial 12");
            fixedcontainer.Put(Generar_Reporte, 185, 460);

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
    }
}