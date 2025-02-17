using Gtk;

public class Autos : Window
{
    private Entry ids;
    private Entry ids_Usuario;
    private Entry Marcas;
    private Entry Modelos;
    private Entry Placas;
    public Autos() : base("Ingreso de vehículos"){
        
        SetDefaultSize(500, 600);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Ingreso de Vehículos", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        EventBox ids1 = CreateStyledLabel("ID:", "Arial 20");
        fixedContainer.Put(ids1, 100, 150);

        ids = new Entry();
        ApplyFont(ids, "Arial 12");
        fixedContainer.Put(ids, 250, 150);

        EventBox ids_Usuario1 = CreateStyledLabel("ID Usuario:", "Arial 20");
        fixedContainer.Put(ids_Usuario1, 100, 200);

        ids_Usuario = new Entry();
        ApplyFont(ids_Usuario, "Arial 12");
        fixedContainer.Put(ids_Usuario, 250, 200);

        EventBox marcas = CreateStyledLabel("Marca:", "Arial 20");
        fixedContainer.Put(marcas, 100, 250);

        Marcas = new Entry();
        ApplyFont(Marcas, "Arial 12");
        fixedContainer.Put(Marcas, 250, 250);

        EventBox modelos = CreateStyledLabel("Modelo:", "Arial 20");
        fixedContainer.Put(modelos, 100, 300);

        Modelos = new Entry();
        ApplyFont(Modelos, "Arial 12");
        fixedContainer.Put(Modelos, 250, 300);

        EventBox placas = CreateStyledLabel("Placas:", "Arial 20");
        fixedContainer.Put(placas, 100, 350);

        Placas = new Entry();
        ApplyFont(Placas, "Arial 12");
        fixedContainer.Put(Placas, 250, 350);

        Button button = new Button("Guardar");
        button.Clicked += OnShowButtonClicked;
        button.SetSizeRequest(100, 50);
        ApplyFont(button, "Arial 20");
        fixedContainer.Put(button, 100, 400);

        Button button1 = new Button("Regresar");
        button1.Clicked += Regresar;
        button1.SetSizeRequest(100, 50);
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 250, 400);

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

    private void OnShowButtonClicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ids.Text) || 
        string.IsNullOrWhiteSpace(ids_Usuario.Text) || 
        string.IsNullOrWhiteSpace(Marcas.Text) || 
        string.IsNullOrWhiteSpace(Modelos.Text) || 
        string.IsNullOrWhiteSpace(Placas.Text))
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, "Por favor llene todos los campos");
            md.Run();
            md.Destroy();
        }
        else
        {
            MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Guardado");
            md.Run();
            md.Destroy();
        }
    }

}