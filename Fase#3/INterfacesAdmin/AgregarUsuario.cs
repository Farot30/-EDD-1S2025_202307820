using Gtk;
using System;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;

namespace INterfacesAdmin.AgregarUsuarios
{
    public class AgregarUsuario : Window
    {
        private ListaDobleVehiculo listaVehiculos;
        private AVLTREErepues avlTreeRepuestos;
        private Binariostree binarioServicio;
        private ListaDeListas grafoNodiri;
        private EventBox Id;
        private EventBox Nombres;
        private EventBox Apellidos;
        private EventBox Correos;
        private EventBox Edades;
        private EventBox Contrasenias;

        private Entry ID;
        private Entry Nombre;
        private Entry Apellido;
        private Entry Correo;
        private Entry Edad;
        private Entry Contrasenia;
        private Button Insertar;
        public AgregarUsuario(ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Agregar Usuario")
        {
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            grafoNodiri = Grafo;

            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Agregar Usuario", "Arial 20");
            fixeds.Put(label, 50, 50);

            Id = CreateStyledLabel("ID:", "Arial 12");
            fixeds.Put(Id, 50, 100);

            ID = new Entry();
            ID.SetSizeRequest(200, 30);
            ApplyFont(ID, "Arial 12");
            fixeds.Put(ID, 150, 100);

            Nombres = CreateStyledLabel("Nombre:", "Arial 12");
            fixeds.Put(Nombres, 50, 140);

            Nombre = new Entry();
            Nombre.SetSizeRequest(200, 30);
            ApplyFont(Nombre, "Arial 12");
            fixeds.Put(Nombre, 150, 140);

            Apellidos = CreateStyledLabel("Apellido:", "Arial 12");
            fixeds.Put(Apellidos, 50, 180);

            Apellido = new Entry();
            Apellido.SetSizeRequest(200, 30);
            ApplyFont(Apellido, "Arial 12");
            fixeds.Put(Apellido, 150, 180);

            Correos = CreateStyledLabel("Correo:", "Arial 12");
            fixeds.Put(Correos, 50, 220);

            Correo = new Entry();
            Correo.SetSizeRequest(200, 30);
            ApplyFont(Correo, "Arial 12");
            fixeds.Put(Correo, 150, 220);

            Edades = CreateStyledLabel("Edad:", "Arial 12");
            fixeds.Put(Edades, 50, 260);

            Edad = new Entry();
            Edad.SetSizeRequest(200, 30);
            ApplyFont(Edad, "Arial 12");
            fixeds.Put(Edad, 150, 260);

            Contrasenias = CreateStyledLabel("Contraseña:", "Arial 12");
            fixeds.Put(Contrasenias, 50, 300);

            Contrasenia = new Entry();
            Contrasenia.SetSizeRequest(200, 30);
            ApplyFont(Contrasenia, "Arial 12");
            fixeds.Put(Contrasenia, 150, 300);

            Insertar = new Button("Insertar");
            Insertar.SetSizeRequest(200, 30);
            Insertar.Clicked += InsertarUsuario;
            ApplyFont(Insertar, "Arial 12");
            fixeds.Put(Insertar, 150, 340);


            Add(fixeds);
        }

        private void InsertarUsuario(object? sender, EventArgs e)
        {
            // Implementar la lógica para insertar un usuario
            Console.WriteLine("Insertar Usuario");
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