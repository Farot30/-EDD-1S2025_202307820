namespace Vehiculos
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public Vehiculo(int id, int idusuario, string marca, string modelo, string placa)
        {
            Id = id;
            IdUsuario = idusuario;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
        }

        public override string ToString()
        {
            return $"Id: {Id}, IdUsuario: {IdUsuario}, Marca: {Marca}, Modelo: {Modelo}, Placa: {Placa}";
        }
    }
}