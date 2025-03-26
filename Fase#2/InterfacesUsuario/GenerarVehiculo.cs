using Gtk;
using CLases.Usuarios;
using CLases.Vehiculo;
using Listas.ListaUsuarioSimple;
using Listas.ListaDobleVehiculos;
using Listas.AVLTREEsRE;
using Listas.BinarioTree;
using Listas.B5Factura;
using System;
using System.Collections.Generic;

namespace InterfacesUsuario.GenerarVehiculo
{
    public class InsertarVehiuclo : Window
    {
        private ListaUsuarioSimple<Usuario> ListaUsuarioSimple;
        private ListaDobleVehiculo listaAutos;
        private AVLTREErepues ListaRepuestos;
        private Binariostree ListaServicios;
        private B5Facturas ListaFacturas;
        private Entry ID;
        private Entry Marca;
        private Entry Modelo;
        private Entry Placa;
        private Button Guardar;
        private int usuarioId;

        public InsertarVehiuclo(ListaUsuarioSimple<Usuario> listaUsuario, ListaDobleVehiculo lIstaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, B5Facturas ListaFactura, int usuarioId) : base("Insertar Vehiculo")
        {
            ListaUsuarioSimple = listaUsuario;
            listaAutos = lIstaAuto;
            ListaRepuestos = ListaRepuesto;
            ListaServicios = ListaServicio;
            ListaFacturas = ListaFactura;
            this.usuarioId = usuarioId;

            SetPosition(WindowPosition.Center);
            SetDefaultSize(400, 400);

            Fixed fixedcontainer = new Fixed();

            EventBox Titulo = CreateStyledLabel("Insertar Vehiculo", "Arial 20");
            fixedcontainer.Put(Titulo, 100, 50);

            EventBox IDLabel = CreateStyledLabel("ID:", "Arial 14");
            fixedcontainer.Put(IDLabel, 50, 100);

            ID = new Entry();
            ApplyFont(ID, "Arial 12");
            fixedcontainer.Put(ID, 150, 100);

            EventBox MarcaLabel = CreateStyledLabel("Marca:", "Arial 14");
            fixedcontainer.Put(MarcaLabel, 50, 150);

            Marca = new Entry();
            ApplyFont(Marca, "Arial 12");
            fixedcontainer.Put(Marca, 150, 150);

            EventBox ModeloLabel = CreateStyledLabel("Modelo:", "Arial 14");
            fixedcontainer.Put(ModeloLabel, 50, 200);

            Modelo = new Entry();
            ApplyFont(Modelo, "Arial 12");
            fixedcontainer.Put(Modelo, 150, 200);

            EventBox PlacaLabel = CreateStyledLabel("Placa:", "Arial 14");
            fixedcontainer.Put(PlacaLabel, 50, 250);
            
            Placa = new Entry();
            ApplyFont(Placa, "Arial 12");
            fixedcontainer.Put(Placa, 150, 250);

            Guardar = new Button("Guardar");
            Guardar.SetSizeRequest(50, 50);
            ApplyFont(Guardar, "Arial 12");
            fixedcontainer.Put(Guardar, 150, 300);
            Guardar.Clicked += Guardar_Clicked;

            Add(fixedcontainer);
        }

        private void Guardar_Clicked(object? sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(ID.Text) || !string.IsNullOrEmpty(Marca.Text) || !string.IsNullOrEmpty(Modelo.Text) || !string.IsNullOrEmpty(Placa.Text))
            {
                if(int.TryParse(ID.Text, out int id) && int.TryParse(Modelo.Text, out int modelos))
                {
                    if (!listaAutos.VehiculoExiste(id))
                    {
                        Vehiculos vehiculo = new Vehiculos(id, usuarioId, Marca.Text, modelos, Placa.Text);
                        listaAutos.Insertar(vehiculo);
                        listaAutos.Mostrar();
                        MostrarMensaje("Vehículo guardado exitosamente.");
                    }
                    else
                    {
                        MostrarMensaje("El ID del vehículo ya existe.");
                    }
                }
                else
                {
                    MostrarMensaje("El ID del vehículo debe ser un número.");
                }
            }
            else
            {
                MostrarMensaje("Todos los campos son requeridos.");
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
        }

        private void MostrarMensaje(string mensaje)
        {
            using (MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, mensaje))
            {
                md.Run();
                md.Destroy();
            }
        }
    }
}