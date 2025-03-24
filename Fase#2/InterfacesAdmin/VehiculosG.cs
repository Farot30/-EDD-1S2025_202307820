using Gtk;
using CLases.Usuarios;
using CLases.Vehiculo;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;

namespace InterfacesAdmin.VehiculoG
{
    public class VehiculosG : Window
    {
        private EventBox ID_ver;
        private Entry IDver;
        private Button IDVision;
        private EventBox ID_Eliminar;
        private Entry IDEliminar;
        private Button IDEliminacion;
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        public VehiculosG(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, B5Facturas ListaFactura) : base("Gestión de vehículos")
        {
            ListaUsuarioSimple = listaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(450, 350);
            Fixed fixedcontainer = new Fixed();
            
            EventBox Titulo = CreateStyledLabel("Gestión de vehículos", "Arial 20");
            fixedcontainer.Put(Titulo, 100, 50);

            ID_ver = CreateStyledLabel("ID ver:", "Arial 14");
            fixedcontainer.Put(ID_ver, 50, 100);
            
            IDver = new Entry();
            ApplyFont(IDver, "Arial 12");
            fixedcontainer.Put(IDver, 150, 100);

            IDVision = new Button("Ver");
            IDVision.Clicked += Ver;
            IDVision.SetSizeRequest(50, 50);
            ApplyFont(IDVision, "Arial 12");
            fixedcontainer.Put(IDVision, 330, 100);

            ID_Eliminar = CreateStyledLabel("ID eliminar:", "Arial 14");
            fixedcontainer.Put(ID_Eliminar, 50, 200);

            IDEliminar = new Entry();
            ApplyFont(IDEliminar, "Arial 12");
            fixedcontainer.Put(IDEliminar, 150, 200);

            IDEliminacion = new Button("Eliminar");
            IDEliminacion.Clicked += Eliminar;
            IDEliminacion.SetSizeRequest(50, 50);
            ApplyFont(IDEliminacion, "Arial 12");
            fixedcontainer.Put(IDEliminacion, 330, 200);
            
            Add(fixedcontainer);
        }

        private void Ver(object? sender, System.EventArgs e)
        {
            if (int.TryParse(IDver.Text, out int ids))
            {
                if (ListaDobleVehiculos.VehiculoExiste(ids))
                {
                    Vehiculos? vehiculo = ListaDobleVehiculos.BuscarVehiculoPorId(ids);
                    if (vehiculo.HasValue)
                    {
                        Vehiculos vehiculoNoNull = vehiculo.Value;
                        string mensaje = $"ID: {vehiculoNoNull.Id}\nID Usuario: {vehiculoNoNull.IdUsuario}\nMarca: {vehiculoNoNull.Marca}\nModelo: {vehiculoNoNull.Modelo}\nPlaca: {vehiculoNoNull.Placa}";

                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje);
                        md.Run();
                        md.Destroy();
                    }
                    else
                    {
                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Vehículo no encontrado");
                        md.Run();
                        md.Destroy();
                    }
                }
                else
                {
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Vehículo no encontrado");
                    md.Run();
                    md.Destroy();
                }
            }
        }

        private void Eliminar(object? sender, System.EventArgs e)
        {
            if (int.TryParse(IDEliminar.Text, out int ids))
            {
                if(ListaDobleVehiculos.VehiculoExiste(ids))
                {
                    ListaDobleVehiculos.Eliminar(ids);
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Vehículo eliminado");
                    md.Run();
                    md.Destroy();
                }else{
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Vehículo no encontrado");
                    md.Run();
                    md.Destroy();
                }
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