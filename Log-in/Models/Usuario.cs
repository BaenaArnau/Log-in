namespace Log_in.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

        public Usuario() { }

        public Usuario(int idUsuario, string userName, byte[] passwordHash, byte[] passwordSalt, string apellido, string email, DateTime? fechaNacimiento, string telefono, string direccion, string ciudad, string estado, string codigoPostal, DateTime fechaRegistro, bool activo)
        {
            IdUsuario = idUsuario;
            UserName = userName;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Apellido = apellido;
            Email = email;
            FechaNacimiento = fechaNacimiento;
            Telefono = telefono;
            Direccion = direccion;
            Ciudad = ciudad;
            Estado = estado;
            CodigoPostal = codigoPostal;
            FechaRegistro = fechaRegistro;
            Activo = activo;
        }
    }
}
