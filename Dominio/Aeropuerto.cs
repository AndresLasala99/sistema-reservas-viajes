using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dominio.Interfaces;

namespace Dominio
{
    public class Aeropuerto : IValidable
    {
        private string _iata;
        private string _ciudad;
        private double _costoOperacion;
        private double _costoTasas;

        public double CostoOperacion
        {
            get { return _costoOperacion; }
        }

        public string Iata
        {
            get { return _iata; }
        }

        public double CostoTasas
        {
            get { return _costoTasas;}
        }

        public Aeropuerto(string iata, string ciudad, double costoOperacion, double costoTasas)
        {
            _iata = iata;
            _ciudad = ciudad;
            _costoOperacion = costoOperacion;
            _costoTasas = costoTasas;
        }
        
        public void Validar()
        {
            if (string.IsNullOrEmpty(_iata) && _iata.Length != 3) throw new Exception("El código IATA debe tener 3 dígitos.");
            if (string.IsNullOrEmpty(_ciudad)) throw new Exception("La ciudad no puede estar vacía.");
            if (_costoOperacion < 0) throw new Exception("El costo de operación no puede ser negativo.");
            if (_costoTasas < 0) throw new Exception("El costo de las tasas no puede ser negativo.");
        }

        public override bool Equals(object? obj)
        {
            return obj is Aeropuerto a && a._iata == _iata;
        }
    }
}