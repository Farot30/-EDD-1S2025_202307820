using Gtk;
using System;
using Listas.ListaDobleVehiculos;
using Listas.AVLRepuestos;
using Listas.BinarioServicio;
using ListasArboles.GrafoNodiri;

namespace INterfacesAdmin.VerUsuario
{
    public class VerUsuarios : Window
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
        private Label Nombre;
        private Label Apellido;
        private Label Correo;
        private Label Edad;
        private Label Contrasenia;
        private Button Buscar;
        public VerUsuarios(ListaDobleVehiculo ListaAuto, AVLTREErepues ListaRepuesto, Binariostree ListaServicio, ListaDeListas Grafo) : base("Ver Usuarios")
        {
            listaVehiculos = ListaAuto;
            avlTreeRepuestos = ListaRepuesto;
            binarioServicio = ListaServicio;
            grafoNodiri = Grafo;
            
            SetDefaultSize(400, 300);
            SetPosition(WindowPosition.Center);

            Fixed fixeds = new Fixed();

            EventBox label = CreateStyledLabel("Ver Usuarios", "Arial 20");
            fixeds.Put(label, 50, 50);

            Id = CreateStyledLabel("ID:", "Arial 12");
            fixeds.Put(Id, 50, 100);

            ID = new Entry();
            ID.SetSizeRequest(200, 30);
            ApplyFont(ID, "Arial 12");
            fixeds.Put(ID, 150, 100);

            Nombres = CreateStyledLabel("Nombre:", "Arial 12");
            fixeds.Put(Nombres, 50, 140);

            Nombre = new Label();
            Nombre.SetSizeRequest(200, 30);
            ApplyFont(Nombre, "Arial 12");
            fixeds.Put(Nombre, 150, 140);

            Apellidos = CreateStyledLabel("Apellido:", "Arial 12");
            fixeds.Put(Apellidos, 50, 180);

            Apellido = new Label();
            Apellido.SetSizeRequest(200, 30);
            ApplyFont(Apellido, "Arial 12");
            fixeds.Put(Apellido, 150, 180);

            Correos = CreateStyledLabel("Correo:", "Arial 12");
            fixeds.Put(Correos, 50, 220);

            Correo = new Label();
            Correo.SetSizeRequest(200, 30);
            ApplyFont(Correo, "Arial 12");
            fixeds.Put(Correo, 150, 220);

            Edades = CreateStyledLabel("Edad:", "Arial 12");
            fixeds.Put(Edades, 50, 260);

            Edad = new Label();
            Edad.SetSizeRequest(200, 30);
            ApplyFont(Edad, "Arial 12");
            fixeds.Put(Edad, 150, 260);

            Contrasenias = CreateStyledLabel("Contraseña:", "Arial 12");
            fixeds.Put(Contrasenias, 50, 300);

            Contrasenia = new Label();
            Contrasenia.SetSizeRequest(200, 30);
            ApplyFont(Contrasenia, "Arial 12");
            fixeds.Put(Contrasenia, 150, 300);

            Buscar = new Button("Buscar");
            Buscar.SetSizeRequest(200, 30);
            Buscar.Clicked += BuscarUsuario;
            ApplyFont(Buscar, "Arial 12");
            fixeds.Put(Buscar, 50, 340);

            Add(fixeds);
        }

        private void BuscarUsuario(object? sender, EventArgs e)
        {
            // Implementar la lógica para buscar un usuario
            Console.WriteLine("Buscar Usuario");
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