using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Premium : Cliente
    {
        private int _puntos;

        public int Puntos
        {
            get { return _puntos; }
        }

        public Premium(string correo, string pass, string cedula, string nombre, string nacionalidad, int puntos) : base(correo, pass, cedula, nombre, nacionalidad)
        {
            _puntos = puntos;
        }

        public override void Validar()
        {
            base.Validar();
            if (_puntos < 0) throw new Exception("Los puntos no pueden ser negativos.");
        }

        public override string ToString()
        {
            return $"Nombre: {_nombre} - Email: {_correo} - Nacionalidad: {_nacionalidad} - Puntos: {_puntos}";
        }

        public void CambiarPuntos(int nuevoPuntos)
        {
            _puntos= nuevoPuntos;
        }
        public override double ObtenerPorcentajeEquipaje(TipoEquipaje equipaje)
        {
            double porcentaje = 0;
            if (equipaje == TipoEquipaje.BODEGA) porcentaje = 5;
            return porcentaje;
        }
    }
}