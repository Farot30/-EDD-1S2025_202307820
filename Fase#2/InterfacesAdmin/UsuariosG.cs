using Gtk;
using CLases.Usuarios;
using CLases.Vehiculo;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;

namespace InterfacesAdmin.UsuarioG
{
    public class UsuariosG : Window
    {
        private EventBox ID_ver;
        private Entry IDver;
        private Button IDVision;
        private EventBox ID_Eliminar;
        private Entry IDEliminar;
        private Button IDEliminarButton;
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues listaRepuesto;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        public UsuariosG(ListaUsuarioSimple<Usuario> ListaUsuario, ListaDobleVehiculo listaDobleVehiculo, AVLTREErepues listarepuesto, Binariostree ListaServicio, B5Facturas ListaFactura) : base("Gestión de usuarios")
        {
            ListaUsuarioSimple = ListaUsuario;
            ListaDobleVehiculos = listaDobleVehiculo;
            listaRepuesto = listarepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 350);
            Fixed fixedcontainer = new Fixed();
            
            EventBox Titulo = CreateStyledLabel("Gestión de usuarios", "Arial 20");
            fixedcontainer.Put(Titulo, 100, 50);

            ID_ver = CreateStyledLabel("ID ver:", "Arial 14");
            fixedcontainer.Put(ID_ver, 50, 100);

            IDver = new Entry();
            ApplyFont(IDver, "Arial 12");
            fixedcontainer.Put(IDver, 150, 100);

            IDVision = new Button("Ver");
            IDVision.SetSizeRequest(50, 50);
            IDVision.Clicked += Ver;
            ApplyFont(IDVision, "Arial 12");
            fixedcontainer.Put(IDVision, 150, 150);

            ID_Eliminar = CreateStyledLabel("ID eliminar:", "Arial 14");
            fixedcontainer.Put(ID_Eliminar, 50, 220);

            IDEliminar = new Entry();
            ApplyFont(IDEliminar, "Arial 12");
            fixedcontainer.Put(IDEliminar, 150, 220);

            IDEliminarButton = new Button("Eliminar");
            IDEliminarButton.Clicked += Eliminar;
            IDEliminarButton.SetSizeRequest(50, 50);
            ApplyFont(IDEliminarButton, "Arial 12");
            fixedcontainer.Put(IDEliminarButton, 150, 270);
            
            Add(fixedcontainer);
        }

        private void Ver(object? sender, System.EventArgs e)
        {
            if (int.TryParse(IDver.Text, out int id))
            {
                if (ListaUsuarioSimple.UsuarioExiste(id))
                {
                    Usuario? usuario = ListaUsuarioSimple.BuscarUsuarioPorId(id);
                    if (usuario.HasValue)
                    {
                        Usuario usuarioNoNull = usuario.Value;
                        string mensaje = $"ID: {usuarioNoNull.Id}\nNombre: {usuarioNoNull.Nombre}\nApellido: {usuarioNoNull.Apellido}\nCorreo: {usuarioNoNull.Correo}\nEdad: {usuarioNoNull.Edad}";

                        List<Vehiculos> vehiculos = ListaDobleVehiculos.BuscarVehiculosPorIdUsuario(id);
                        if (vehiculos.Count > 0)
                        {
                            mensaje += "\n\nVehículos:";
                            foreach (var vehiculo in vehiculos)
                            {
                                mensaje += $"\nID Vehículo: {vehiculo.Id}\nMarca: {vehiculo.Marca}\nModelo: {vehiculo.Modelo}\nPlaca: {vehiculo.Placa}\n";
                            }
                        }
                        else
                        {
                            mensaje += "\n\nNo se encontraron vehículos asociados a este usuario.";
                        }

                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje);
                        md.Run();
                        md.Destroy();
                    }
                    else
                    {
                        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado");
                        md.Run();
                        md.Destroy();
                    }
                }
                else
                {
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado");
                    md.Run();
                    md.Destroy();
                }
            }
        }

        private void Eliminar(object? sender, System.EventArgs e)
        {
            if (int.TryParse(IDEliminar.Text, out int id))
            {
                if(ListaUsuarioSimple.UsuarioExiste(id))
                {
                    ListaUsuarioSimple.Eliminar(id);
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario eliminado");
                    md.Run();
                    md.Destroy();
                }
                else
                {
                    MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Usuario no encontrado");
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
