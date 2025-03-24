using Gtk;
using CLases.Usuarios;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using CLases.Servicios;
using Listas.B5Factura;
using System;
using System.Collections.Generic;
using Clases.Factura;
using CLases.Repuesto;

namespace InterfacesAdmin.GenerarServicios
{
    public class GenerarServicio : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo ListaDobleVehiculos;
        private AVLTREErepues AVLTree;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        private Entry ID;
        private Entry ID_repuesto;
        private Entry Id_vehiculo;
        private Entry Detalle;
        private Entry Costo;
        private Button Guardar;
        private int ID_factura = 1;

        public GenerarServicio(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo listaDoble, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, B5Facturas ListaFactura) : base("Generar Servicio")
        {
            ListaUsuarioSimple = listaUsuario;
            ListaDobleVehiculos = listaDoble;
            AVLTree = ListaRepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 425);
            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Generar Servicio", "Arial 20");
            fixedcontainer.Put(Titulo, 80, 50);

            EventBox IDLabel = CreateStyledLabel("ID:", "Arial 14");
            fixedcontainer.Put(IDLabel, 50, 100);

            ID = new Entry();
            ApplyFont(ID, "Arial 12");
            fixedcontainer.Put(ID, 170, 100);

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
                if(!ListaServicios.Existeid(id))
                {
                    if(AVLTree.RepuestoExiste(IDrepuesto))
                    {
                        if(ListaDobleVehiculos.VehiculoExiste(id_vehiculo))
                        {
                            Servicio servicio = new Servicio(id, IDrepuesto, id_vehiculo, Detalle.Text, costo);
                            ListaServicios.Insertar(servicio);

                            // Calcular el total de la factura
                            Repuesto? repuesto = AVLTree.BuscarRepuestoPorId(IDrepuesto);
                            double total = costo + (repuesto?.Precio ?? 0);
                            if(ListaFacturas.FacturaExiste(ID_factura))
                            {
                                // Crear e insertar la factura
                                Facturas factura1 = new Facturas(ID_factura, id, total);
                                ListaFacturas.Insertar(factura1.ID, factura1.ID_servicio, factura1.Total);
                                ID_factura++;

                                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Servicio y factura guardados exitosamente.");
                                messageDialog.Run();
                                messageDialog.Destroy();

                            }
                            else
                            {
                                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Factura ya existe");
                                messageDialog.Run();
                                messageDialog.Destroy();
                            }
                            
                        }
                        else
                        {
                            MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El vehiculo no existe");
                            messageDialog.Run();
                            messageDialog.Destroy();
                        }
                        
                    }else
                    {
                        MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El repuesto no existe");
                        messageDialog.Run();
                        messageDialog.Destroy();
                    }
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El servicio ya existe");
                    messageDialog.Run();
                    messageDialog.Destroy();
                }
            }else
            {
                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe ingresar datos válidos en los campos de ID y costo");
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