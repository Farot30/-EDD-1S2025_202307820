using Gtk;
using System;
using System.Collections.Generic;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;

namespace InterfacesUsuario.PagarFacturas
{
    public class PagarFactura : Window
    {

        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Entry Id;
        private Entry Orden;
        private Entry Total;
        private Button Pagar;
        private Button Buscar;
        public PagarFactura(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto) : base("Pagar Factura")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;

            SetDefaultSize(400, 400);
            SetPosition(WindowPosition.Center);

            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Pagar Factura", "Arial 20");
            fixedcontainer.Put(Titulo, 80, 50);

            EventBox IdLabel = CreateStyledLabel("ID:", "Arial 14");
            fixedcontainer.Put(IdLabel, 50, 100);

            Id = new Entry();
            ApplyFont(Id, "Arial 12");
            fixedcontainer.Put(Id, 150, 100);

            EventBox OrdenLabel = CreateStyledLabel("Orden:", "Arial 14");
            fixedcontainer.Put(OrdenLabel, 50, 150);

            Orden = new Entry();
            ApplyFont(Orden, "Arial 12");
            fixedcontainer.Put(Orden, 150, 150);

            EventBox TotalLabel = CreateStyledLabel("Total:", "Arial 14");
            fixedcontainer.Put(TotalLabel, 50, 200);
            
            Total = new Entry();
            ApplyFont(Total, "Arial 12");
            fixedcontainer.Put(Total, 150, 200);

            Pagar = new Button("Pagar");
            Pagar.SetSizeRequest(120, 50);
            Pagar.Clicked += Pagar_Factura;
            ApplyFont(Pagar, "Arial 12");
            fixedcontainer.Put(Pagar, 80, 250);

            Buscar = new Button("Buscar");
            Buscar.SetSizeRequest(120, 50);
            Buscar.Clicked += Buscar_Factura;
            ApplyFont(Buscar, "Arial 12");
            fixedcontainer.Put(Buscar, 230, 250);

            Add(fixedcontainer);
        }
        public void Pagar_Factura(object? sender, EventArgs e)
        {

        }

        public void Buscar_Factura(object? sender, EventArgs e)
        {
            if(int.TryParse(Id.Text, out int id))
            {
                
            }else
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Close, "El ID debe ser un numero");
                md.Run();
                md.Destroy();
            }
        }
        private void ApplyFont(Widget widget, string fontDescription)
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