using Gtk;
using System;
using Listas.BinarioServicio;
using Listas.AVLRepuestos;
using Listas.ListaDobleVehiculos;
using ListasArboles.GrafoNodiri;
using Clases.Repuesto;
using Clases.Servicios;

namespace INterfacesAdmin.GenerarServicio
{
    public class GenerarServicios : Window
    {

        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ListaDeListas grafoNodiri;
        private EventBox Id;
        private EventBox Repuesto;
        private EventBox Vehiculo;
        private EventBox Detalles;
        private EventBox Costos;
        private Entry ID;
        private Entry ID_repuesto;
        private Entry Id_vehiculo;
        private Entry Detalle;
        private Entry Costo;
        private Button Insertar;
        public GenerarServicios(ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Generar Servicios")
        {
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Generar Servicios", "Arial 20");
            fixeds.Put(label, 50, 50);

            Id = CreateStyledLabel("ID Servicio:", "Arial 12");
            fixeds.Put(Id, 50, 100);

            ID = new Entry();
            ID.SetSizeRequest(200, 30);
            ApplyFont(ID, "Arial 12");
            fixeds.Put(ID, 150, 100);

            Repuesto = CreateStyledLabel("ID Repuesto:", "Arial 12");
            fixeds.Put(Repuesto, 50, 140);

            ID_repuesto = new Entry();
            ID_repuesto.SetSizeRequest(200, 30);
            ApplyFont(ID_repuesto, "Arial 12");
            fixeds.Put(ID_repuesto, 150, 140);

            Vehiculo = CreateStyledLabel("ID Vehículo:", "Arial 12");
            fixeds.Put(Vehiculo, 50, 180);

            Id_vehiculo = new Entry();
            Id_vehiculo.SetSizeRequest(200, 30);
            ApplyFont(Id_vehiculo, "Arial 12");
            fixeds.Put(Id_vehiculo, 150, 180);

            Detalles = CreateStyledLabel("Detalles:", "Arial 12");
            fixeds.Put(Detalles, 50, 220);

            Detalle = new Entry();
            Detalle.SetSizeRequest(200, 30);
            ApplyFont(Detalle, "Arial 12");
            fixeds.Put(Detalle, 150, 220);

            Costos = CreateStyledLabel("Costo:", "Arial 12");
            fixeds.Put(Costos, 50, 260);

            Costo = new Entry();
            Costo.SetSizeRequest(200, 30);
            ApplyFont(Costo, "Arial 12");
            fixeds.Put(Costo, 150, 260);

            Insertar = new Button("Insertar");
            Insertar.SetSizeRequest(200, 30);
            Insertar.Clicked += InsertarServicio;
            ApplyFont(Insertar, "Arial 12");
            fixeds.Put(Insertar, 50, 300);

            Add(fixeds);

        }

        private void InsertarServicio(object? sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(ID.Text) && !string.IsNullOrWhiteSpace(ID_repuesto.Text) && !string.IsNullOrWhiteSpace(Id_vehiculo.Text) && !string.IsNullOrWhiteSpace(Detalle.Text) && !string.IsNullOrWhiteSpace(Costo.Text))
            {
                if(int.TryParse(ID.Text, out int id) && int.TryParse(ID_repuesto.Text, out int IDrepuesto) && int.TryParse(Id_vehiculo.Text, out int id_vehiculo) && double.TryParse(Costo.Text, out double costo))
                {
                    if(!binarioServicio.Existeid(id))
                    {
                        if(avlTreeRepuestos.RepuestoExiste(IDrepuesto))
                        {
                            if(listaVehiculos.VehiculoExiste(id_vehiculo))
                            {
                                Servicio servicio = new Servicio(id, IDrepuesto, id_vehiculo, Detalle.Text, costo);
                                binarioServicio.Insertar(servicio);
                                binarioServicio.Mostrar();

                                // Calcular el total de la factura
                                Repuesto? repuesto = avlTreeRepuestos.BuscarRepuestoPorId(IDrepuesto);
                                double total = costo + (repuesto?.Precio ?? 0);
                                // Crear e insertar la factura
                            }
                            else
                            {
                                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El vehiculo no existe");
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
                        MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El servicio ya existe");
                        messageDialog.Run();
                        messageDialog.Destroy();
                    }
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe ingresar datos válidos en los campos de ID y costo");
                    messageDialog.Run();
                    messageDialog.Destroy();
                }
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe llenar todos los campos");
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
            else if (widget is Entry entry)
            {
                entry.ModifyFont(fontDesc);
            }
        }
    }
}