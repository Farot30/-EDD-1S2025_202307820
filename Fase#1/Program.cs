using Gtk;

class Program{
    public static void Main(string[] args){
        Application.Init();
        InicioS win = new InicioS();
        win.ShowAll();
        Application.Run();
    }
}