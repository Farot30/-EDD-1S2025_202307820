using Gtk;

public class Individuals : Window
{

    private Entry ID;
    private Entry Nombres;
    private Entry Apellidos;
    private Entry Correo;
    private Entry Contraseña;
    public Individuals() : base("Ingresos Individuales") {
    
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Usuario", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        EventBox ids = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids, 100, 150);

        ID = new Entry();
        ApplyFont(ID, "Arial 12");
        fixedContainer.Put(ID, 250, 150);

        EventBox nombres = CreateStyledLabel("Nombres:", "Arial 20");
        fixedContainer.Put(nombres, 100, 200);

        Nombres = new Entry();
        ApplyFont(Nombres, "Arial 12");
        fixedContainer.Put(Nombres, 250, 200);

        EventBox apellidos = CreateStyledLabel("Apellidos:", "Arial 20");
        fixedContainer.Put(apellidos, 100, 250);

        Apellidos = new Entry();
        ApplyFont(Apellidos, "Arial 12");
        fixedContainer.Put(Apellidos, 250, 250);

        EventBox correos = CreateStyledLabel("Correo:", "Arial 20");
        fixedContainer.Put(correos, 100, 300);

        Correo = new Entry();
        ApplyFont(Correo, "Arial 12");
        fixedContainer.Put(Correo, 250, 300);

        EventBox contraseñas = CreateStyledLabel("Contraseña:", "Arial 20");
        fixedContainer.Put(contraseñas, 100, 350);

        Contraseña = new Entry();
        ApplyFont(Contraseña, "Arial 12");
        fixedContainer.Put(Contraseña, 250, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 170, 400);

        Add(fixedContainer);
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
        else if (widget is Entry entry)
        {
            entry.ModifyFont(fontDesc);
        }
    }

    private void OnShowButtonClicked(object? sender, System.EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ID.Text) || 
            string.IsNullOrWhiteSpace(Nombres.Text) || 
            string.IsNullOrWhiteSpace(Apellidos.Text) || 
            string.IsNullOrWhiteSpace(Correo.Text) || 
            string.IsNullOrWhiteSpace(Contraseña.Text))
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "Todos los campos deben estar llenos.");
            md.Run();
            md.Destroy();
        }
        else
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Close, "Usuario guardado con éxito");
            Console.WriteLine("Usuario guardado con éxito");
            Console.WriteLine("ID           :" + ID.Text);
            Console.WriteLine("Nombres      :" + Nombres.Text);
            Console.WriteLine("Apellidos    :" + Apellidos.Text);
            Console.WriteLine("Correo       :" + Correo.Text);
            Console.WriteLine("Contraseña   :" + Contraseña.Text);
            md.Run();
            md.Destroy();
        }
    }
}