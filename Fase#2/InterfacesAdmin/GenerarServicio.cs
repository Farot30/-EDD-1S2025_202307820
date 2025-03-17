using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using System;
using System.Collections.Generic;

namespace InterfacesAdmin.GenerarServicios
{
    public class GenerarServicio : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues AVLTree;
        private Entry ID;
        private Entry ID_repuesto;
        private Entry Id_vehiculo;
        private Entry Detalle;
        private Entry Costo;
        private Button Guardar;
        public GenerarServicio(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo listaDoble, AVLTREErepues ListaRepuesto) : base("Generar Servicio")
        {
            ListaUsuarioSimple = listaUsuario;
            ListaDobleVehiculos = listaDoble;
            AVLTree = ListaRepuesto;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 425);
            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Generar Servicio", "Arial 20");
            fixedcontainer.Put(Titulo, 80, 50);

            EventBox IDLabel = CreateStyledLabel("ID:", "Arial 14");
            fixedcontainer.Put(IDLabel, 80, 100);

            ID = new Entry();
            ApplyFont(ID, "Arial 12");
            fixedcontainer.Put(ID, 50, 100);

            EventBox ID_repuestoLabel = CreateStyledLabel("ID Repuesto:", "Arial 14");
            fixedcontainer.Put(ID_repuestoLabel, 50, 150);

            ID_repuesto = new Entry();
            ApplyFont(ID_repuesto, "Arial 12");
            fixedcontainer.Put(ID_repuesto, 170, 150);

            EventBox Id_vehiculoLabel = CreateStyledLabel("ID Vehiculo:", "Arial 14");
            fixedcontainer.Put(Id_vehiculoLabel, 50, 200);
            
            Id_vehiculo = new Entry();
            ApplyFont(Id_vehiculo, "Arial 12");
            fixedcontainer.Put(Id_vehiculo, 170, 200);

            EventBox DetalleLabel = CreateStyledLabel("Detalle:", "Arial 14");
            fixedcontainer.Put(DetalleLabel, 50, 250);

            Detalle = new Entry();
            ApplyFont(Detalle, "Arial 12");
            fixedcontainer.Put(Detalle, 170, 250);

            EventBox CostoLabel = CreateStyledLabel("Costo:", "Arial 14");
            fixedcontainer.Put(CostoLabel, 50, 300);

            Costo = new Entry();
            ApplyFont(Costo, "Arial 12");
            fixedcontainer.Put(Costo, 170, 300);

            Guardar = new Button("Guardar");    
            Guardar.Clicked += GuardarServicio;
            Guardar.SetSizeRequest(50, 50);
            ApplyFont(Guardar, "Arial 12");
            fixedcontainer.Put(Guardar, 150, 350);

            Add(fixedcontainer);
        }

        private void GuardarServicio(object? sender, EventArgs e)
        {
            if(int.TryParse(ID.Text, out int id) && int.TryParse(ID_repuesto.Text, out int IDrepuesto) && int.TryParse(Id_vehiculo.Text, out int id_vehiculo) && double.TryParse(Costo.Text, out double costo))
            {

            }else
            {
                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe ingresar con datos enteros y dobules en el los ID y los costos");
                messageDialog.Run();
                messageDialog.Destroy();
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
        }

    }
}