
namespace CLases.Usuarios
{

    public struct Usuario{
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public string Contrasena { get; set; }

        public Usuario(int id, string nombre, string apellido, string correo, int edad, string contrasena){
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Edad = edad;
            Contrasena = contrasena;
        }

        public override string ToString(){
            return $"Id: {Id}, Nombre: {Nombre}, Apellido: {Apellido}, Correo: {Correo}, Edad: {Edad}, Contrasena: {Contrasena}";
        }
    }
    
}