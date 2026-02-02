using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Vuelo : IValidable
    {
        private string _codigo;
        private Ruta _ruta;
        private Avion _avion;
        private List<DayOfWeek> _frecuencia;

        public Ruta Ruta
        {
            get { return _ruta; }
        }
        public double CostoPorAsiento
        {
            get { return ObtenerPrecioCostoPorAsiento(); }
        }

        public string Codigo
        {
            get { return _codigo; }
        }

        public List<DayOfWeek> Frecuencia
        {
            get { return _frecuencia; }
        }

        public Avion Avion
        {
            get { return _avion; }
        }

        public Vuelo(string codigo, Ruta ruta, Avion avion, List<DayOfWeek> frecuencia)
        {
            _codigo = codigo;
            _ruta = ruta;
            _avion = avion;
            _frecuencia = frecuencia;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_codigo)) throw new Exception("El codigo debe de ser alfanumérico entre 3 y 6 dígitos (debe de tener 2 letras y 4 números.");
            if (_ruta == null) throw new Exception("La ruta no puede ser nula.");
            if (_avion == null) throw new Exception("El avion no puede ser nulo.");
            if (_frecuencia.Count == 0) throw new Exception("La frecuencia debe ser al menos 1.");
            if (CostoPorAsiento < 0) throw new Exception("El costo por aciento no puede ser negativo.");
            if (_avion.Alcance < _ruta.Distancia) throw new Exception("El avion no alcanza a cubrir la distancia de la ruta.");
            ValidarNumeroVuelo(_codigo);
        }

        public override string ToString()
        {
            return $"Nro de vuelo: {_codigo} - Modelo del avion: {_avion.Modelo} - Ruta: {_ruta.Salida.Iata} - {_ruta.Llegada.Iata} - Frecuencia: {_frecuencia.Count} por semana.";
        }

        //VALIDAMOS NUMERO DE VUELO
        private bool ValidarNumeroVuelo(string codigo)
        {
            bool validarNroVuelo = false;

            while (!validarNroVuelo)
            {
                if (string.IsNullOrEmpty(codigo)) throw new Exception("El número de vuelo no puede estar vacío.");
                if (codigo.Length < 3 || codigo.Length > 6) throw new Exception("El número de vuelo debe tener 2 letras y de 1 a 4 números (de 3 a 6 caracteres).");
                if (!char.IsUpper(codigo[0]) || !char.IsLetter(codigo[0]) || !char.IsUpper(codigo[1]) || !char.IsLetter(codigo[1])) throw new Exception("El primer y segundo carácter deben ser en mayúscula.");
                for (int i = 2; i < codigo.Length; i++)
                {
                    if (!char.IsDigit(codigo[i])) throw new Exception("Después de las primeras dos letras, solo deben ir números.");
                }

                validarNroVuelo = true;
            }

            return validarNroVuelo;
        }

        public double ObtenerPrecioCostoPorAsiento()
        {
            double costoPorAsiento = ((_avion.CostoOperacion * _ruta.Distancia) + _ruta.Salida.CostoOperacion + _ruta.Llegada.CostoOperacion) / _avion.CantidadAsientos;
            return costoPorAsiento;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vuelo v && v._codigo == _codigo;
        }

        public string ObtenerDiasFrecuencia()
        {
            string dias = "";
            foreach (DayOfWeek dia in _frecuencia)
            {
                dias += dia.ToString() + " ";
            }
            return dias;
        }
    }
}