using Gtk;

public class Masivo : Window
{
    public Masivo() : base("Cargas masivas"){
        SetDefaultSize(500, 400);
        SetPosition(WindowPosition.Center);

        Fixed fixedContainer = new Fixed();

        EventBox labelesBox = CreateStyledLabel("Cargas masivas", "Arial 20");
        fixedContainer.Put(labelesBox, 160, 50);

        Button button1 = new Button("Cargar");
        button1.Clicked += Cargaras;
        ApplyFont(button1, "Arial 20");
        fixedContainer.Put(button1, 200, 250);

        Add(fixedContainer);
    }

    private void Cargaras(object? sender, EventArgs e)
    {
        MessageDialog md = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Aquí se cargará el archivo en teoría");
        Console.WriteLine("Se debe de cargar un archivo");
        md.Run();
        md.Destroy();
        /*FileChooserDialog fileChooser = new FileChooserDialog("Seleccione el archivo a cargar", this, FileChooserAction.Open, "Cancelar", ResponseType.Cancel, "Abrir", ResponseType.Accept);
        fileChooser.Run();
        fileChooser.Destroy();*/
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