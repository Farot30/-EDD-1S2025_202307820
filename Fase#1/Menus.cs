using Gtk;

public class Menus : Window
{
    public Menus() : base("Menu del usuario")
    {
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Menu del usuario", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargas Masivas");
        button1.Clicked += Cargas;
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 150, 100);

        Button button2 = new Button("Ingresos individuales");
        button2.Clicked += Ingresos;
        button2.SetSizeRequest(100, 50);
        ApplyFont(button2, "Arial 20");
        fixedContainer.Put(button2, 125, 200);

        Button button3 = new Button("Gestión de Usuarios");
        button3.Clicked += Gestiones;
        button3.SetSizeRequest(100, 50);
        ApplyFont(button3, "Arial 20");
        fixedContainer.Put(button3, 130, 300);

        Button button4 = new Button("Generar Servicio");
        button4.Clicked += Generaciones;
        button4.SetSizeRequest(100, 50);
        ApplyFont(button4, "Arial 20");
        fixedContainer.Put(button4, 150, 400);

        Button button5 = new Button("Cancelar Factura");
        button5.Clicked += Cancelaciones;
        button5.SetSizeRequest(100, 50);
        ApplyFont(button5, "Arial 20");
        fixedContainer.Put(button5, 150, 500);

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
    }

    private void Cargas(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Carga Masiva");
        Console.WriteLine("Cargas Masivas");
        md.Run();
        md.Destroy();
        Masivo mas = new Masivo();
        mas.ShowAll();
        this.Hide();
    }

    private void Ingresos(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Ingreso individual");
        Console.WriteLine("Ingreso individual");
        md.Run();
        md.Destroy();
        MenuIngresos individup = new MenuIngresos();
        individup.ShowAll();
        this.Hide();
    }

    private void Gestiones(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Gestión de Usuarios");
        Console.WriteLine("Gestión de Usuarios");
        md.Run();
        md.Destroy();
        Gestion gest = new Gestion();
        gest.ShowAll();
        this.Hide();
    }

    private void Generaciones(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Generar Servicio");
        Console.WriteLine("Generar Servicio");
        md.Run();
        md.Destroy();
    }

    private void Cancelaciones(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Cancelar Factura");
        Console.WriteLine("Cancelar Factura");
        md.Run();
        md.Destroy();
    }
}