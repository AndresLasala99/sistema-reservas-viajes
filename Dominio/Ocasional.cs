using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ocasional : Cliente
    {
        private bool _regalo;

        public bool Regalo
        {
            get { return _regalo; }
        }

        public Ocasional(string correo, string pass, string cedula, string nombre, string nacionalidad, bool regalo) : base(correo, pass, cedula, nombre, nacionalidad)
        {
            _regalo = regalo;
        }

        public override void Validar()
        {
            base.Validar();
        }

        public override string ToString()
        {
            return $"Nombre: {_nombre} - Email: {_correo} - Nacionalidad: {_nacionalidad} - Elegible para regalos: {_regalo}";
        }

        public void CambiarRegalo(bool nuevoRegalo)
        {
            _regalo = nuevoRegalo;
        }

        public override double ObtenerPorcentajeEquipaje(TipoEquipaje equipaje)
        {
            double porcentaje = 0;
            if (equipaje == TipoEquipaje.CABINA) porcentaje = 10;
            else if (equipaje == TipoEquipaje.BODEGA) porcentaje = 20;
            return porcentaje;
        }
    }
}