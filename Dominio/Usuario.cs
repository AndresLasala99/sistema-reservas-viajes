using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        protected string _correo;
        protected string _pass;

        public string Correo
        {
            get { return _correo; }
        }

        public string Pass
        {
            get { return _pass; }
        }

        public Usuario(string correo, string pass)
        {
            _correo = correo;
            _pass = pass;
        }

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(_correo)) throw new Exception("El correo no puede estar vacío.");
            if (string.IsNullOrEmpty(_pass)) throw new Exception("La contraseña no puede estar vacía.");
            if(!ValidarPass()) throw new Exception("La contraseña debe ser alfanumérica (contener letras y números).");
        }

        public override bool Equals(object? obj)
        {
            return obj is Usuario u && u.Correo == Correo;
        }

        public abstract string Rol();

        private bool ValidarPass()
        {
            bool validacion = false;
            bool tieneLetra = false;
            bool tieneNumero = false;

            foreach (char c in _pass)
            {
                if (char.IsLetter(c)) tieneLetra = true;
                if (char.IsDigit(c)) tieneNumero = true;
            }

            if (tieneLetra && tieneNumero) validacion = true;
            return validacion;
        }
    }
}