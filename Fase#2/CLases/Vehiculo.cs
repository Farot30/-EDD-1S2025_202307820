namespace CLases.Vehiculo
{
    public struct Vehiculo
    {

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }

        public Vehiculo(int id, int idUsuario, string marca, string modelo, string placa)
        {
            Id = id;
            IdUsuario = idUsuario;
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