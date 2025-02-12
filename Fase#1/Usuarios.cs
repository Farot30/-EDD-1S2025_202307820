namespace Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }

        public string Contraseña {get; set;}

        public Usuario(int id, string nombre, string apellido, string correo, string contraseña)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contraseña = contraseña;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Apellido: {Apellido}, Correo: {Correo}, Contraseña: {Contraseña}";
        }
    }
}