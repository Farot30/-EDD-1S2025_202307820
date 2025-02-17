using Gtk;

public class MenuIngresos : Window
{
    public MenuIngresos () : base ("Menu de ingresos")
    {
        SetDefaultSize(500, 700);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Menu de ingresos", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Ingresar Usuario");
        button1.Clicked += Usus1;
        button1.SetSizeRequest(100, 50);
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 150, 100);

        Button button2 = new Button("Ingresar Vehículo");
        button2.Clicked += Vehí;
        button2.SetSizeRequest(100, 50);
        ApplyFont(button2, "Arial 20");
        fixedContainer.Put(button2, 140, 200);

        Button button3 = new Button("Repuestos");
        button3.Clicked += Repuestos;
        button3.SetSizeRequest(100, 50);
        ApplyFont(button3, "Arial 20");
        fixedContainer.Put(button3, 175, 300);

        Button button4 = new Button("Ingresar Servicios");
        button4.Clicked += IngresarServicios;
        button4.SetSizeRequest(100, 50);
        ApplyFont(button4, "Arial 20");
        fixedContainer.Put(button4, 140, 400);

        Button button5 = new Button("Regresar");
        button5.Clicked += Regresar;
        button5.SetSizeRequest(100, 50);
        ApplyFont(button5, "Arial 20");
        fixedContainer.Put(button5, 190, 500);

        Add(fixedContainer);
    }

    private void Usus1(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a ingresar usuario");
        Console.WriteLine("entrando a ingresar usuario");
        md.Run();
        md.Destroy();
        Individuals individuals = new Individuals();
        individuals.ShowAll();
        this.Hide();
    }

    private void Vehí(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a ingresar Vehículos");
        Console.WriteLine("entrando a ingresar Vehículos");
        md.Run();
        md.Destroy();
        Autos autos = new Autos();
        autos.ShowAll();
        this.Hide();

    }

    private void Repuestos(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a ingresar Repuestos");
        Console.WriteLine("entrando a ingresar Repuestos");
        md.Run();
        md.Destroy();
        Piezas piezas = new Piezas();
        piezas.ShowAll();
        this.Hide();

    }

    private void IngresarServicios(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "entrando a ingresar Servicios");
        Console.WriteLine("entrando a ingresar Servicios");
        md.Run();
        md.Destroy();
        Servir servir = new Servir();
        servir.ShowAll();
        this.Hide();

    }

    private void Regresar(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Regresando");
        Console.WriteLine("Regresando");
        md.Run();
        md.Destroy();
        Menus menus = new Menus();
        menus.ShowAll();
        this.Hide();
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
}