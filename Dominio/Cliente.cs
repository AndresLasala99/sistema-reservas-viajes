using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Cliente : Usuario , IComparable<Cliente>
    {
        protected string _cedula;
        protected string _nombre;
        protected string _nacionalidad;
        protected List<Pasaje> _pasajes = new List<Pasaje>();

        public string Nombre
        {
            get { return _nombre; }
        }

        public string Cedula
        {
            get { return _cedula;}
        }

        public string Nacionalidad
        {
            get { return _nacionalidad;}
        }

        public Cliente(string correo, string pass, string cedula, string nombre, string nacionalidad) : base(correo, pass)
        {
            _cedula = cedula;
            _nombre = nombre;
            _nacionalidad = nacionalidad;
        }

        public override void Validar()
        {
            base.Validar();
            if (string.IsNullOrEmpty(_cedula)) throw new Exception("La cédula no puede estar vacía.");
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede estar vacío.");
            if (string.IsNullOrEmpty(_nacionalidad)) throw new Exception("La nacionalidad no puede estar vacía.");
        }

        public override bool Equals(object? obj)
        {
            return obj is Cliente c && c._cedula == _cedula;
        }

        public override string Rol()
        {
            return "Cliente";
        }

        public abstract double ObtenerPorcentajeEquipaje(TipoEquipaje equipaje);

        public int CompareTo(Cliente? other)
        {
            return this._cedula.CompareTo(other._cedula);
        }
    }
}