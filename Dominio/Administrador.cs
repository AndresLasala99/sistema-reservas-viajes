using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador : Usuario
    {
        private string _apodo;

        public Administrador(string correo, string pass, string apodo) : base(correo, pass)
        {
            _apodo = apodo;
        }

        public override void Validar()
        {
            base.Validar();
            if (string.IsNullOrEmpty(_apodo)) throw new Exception("El apodo no puede estar vacío.");
        }

        public override string Rol()
        {
            return "Admin";
        }
    }
}