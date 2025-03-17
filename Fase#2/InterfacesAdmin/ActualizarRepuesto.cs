using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;

namespace InterfacesAdmin.ActualizarRepuestos
{
    public class ActualizarRepuesto : Window{
        private Entry ID;
        private Button Actualizar;
        private Entry Repuesto;
        private Entry Detalles;
        private Entry Precio;
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        public ActualizarRepuesto(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto) : base("Actualizar de Repuestos")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            
            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 400);
            Fixed fixedcontainer = new Fixed();
            
            EventBox Titulo = CreateStyledLabel("Actualizar de Repuestos", "Arial 20");
            fixedcontainer.Put(Titulo, 80, 50);

            EventBox IDLabel = CreateStyledLabel("ID:", "Arial 14");
            fixedcontainer.Put(IDLabel, 50, 100);
            
            ID = new Entry();
            ApplyFont(ID, "Arial 12");
            fixedcontainer.Put(ID, 150, 100);

            EventBox RepuestoLabel = CreateStyledLabel("Repuesto:", "Arial 14");
            fixedcontainer.Put(RepuestoLabel, 50, 150);

            Repuesto = new Entry();
            ApplyFont(Repuesto, "Arial 12");
            fixedcontainer.Put(Repuesto, 150, 150);

            EventBox DetallesLabel = CreateStyledLabel("Detalles:", "Arial 14");
            fixedcontainer.Put(DetallesLabel, 50, 200);

            Detalles = new Entry();
            ApplyFont(Detalles, "Arial 12");
            fixedcontainer.Put(Detalles, 150, 200);

            EventBox PrecioLabel = CreateStyledLabel("Precio:", "Arial 14");
            fixedcontainer.Put(PrecioLabel, 50, 250);

            Precio = new Entry();
            ApplyFont(Precio, "Arial 12");
            fixedcontainer.Put(Precio, 150, 250);

            Actualizar = new Button("Actualizar");
            Actualizar.Clicked += Actualizarrepue;
            Actualizar.SetSizeRequest(50, 50);
            ApplyFont(Actualizar, "Arial 12");
            fixedcontainer.Put(Actualizar, 150, 300);
            
            Add(fixedcontainer);
        }

        private void Actualizarrepue(object? sender, System.EventArgs e)
        {
            if (int.TryParse(ID.Text, out int ids) && double.TryParse(Precio.Text, out double precio))
            {
                if (listaRepuesto.BuscarRepuestoPorId(ids) != null)
                {
                    bool actualizado = listaRepuesto.ActualizarRepuesto(ids, Repuesto.Text, Detalles.Text, precio);
                    if (actualizado)
                    {
                        MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Repuesto actualizado correctamente");
                        messageDialog.Run();
                        messageDialog.Destroy();
                    }
                    else
                    {
                        MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al actualizar el repuesto");
                        messageDialog.Run();
                        messageDialog.Destroy();
                    }
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El repuesto no existe");
                    messageDialog.Run();
                    messageDialog.Destroy();
                }
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Ingrese un ID y un precio válidos");
                messageDialog.Run();
                messageDialog.Destroy();
            }
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