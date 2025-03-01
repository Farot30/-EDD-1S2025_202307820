using System;

namespace Usuarios
{
    public unsafe struct Usuario
    {
        public int Id;
        public fixed char Nombre[50];
        public fixed char Apellido[50];
        public fixed char Correo[50];
        public fixed char Contraseña[50];

        public Usuario* sig;

        public unsafe Usuario(int id, string nombre, string apellido, string correo, string contraseña)
        {
            this.Id = id;
            sig = null;
            fixed (char* n = this.Nombre, l=this.Apellido, c = this.Correo, co = this.Contraseña)
            {
                setFixedString(nombre, n, 50);
                setFixedString(apellido, l, 50);
                setFixedString(correo, c, 50);
                setFixedString(contraseña, co, 50);
            }
        }

        private unsafe static void setFixedString(string str, char* fixedStr, int maxLength)
        {
            int i;
            for (i = 0; i < str.Length && i < maxLength - 1; i++)
            {
                fixedStr[i] = str[i];
            }
            fixedStr[i] = '\0';
        }

        private unsafe static string getFixedString(char* fixedStr)
        {
            string str = "";
            for (int i = 0; fixedStr[i] != '\0'; i++)
            {
                str += fixedStr[i];
            }
            return str;
        }

        public override string ToString()
        {   
            fixed(char* n = Nombre, l = Apellido, co = Correo, c = Contraseña)
            {
                return $"ID: {Id}, Nombre: {getFixedString(n)}, Apellido: {getFixedString(l)}, Correo: {getFixedString(co)}, Contraseña: {getFixedString(c)}";
            }
        }
    }
}