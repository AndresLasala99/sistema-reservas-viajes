using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Avion : IValidable
    {
        private string _fabricante;
        private string _modelo;
        private int _cantAsientos;
        private double _alcance;
        private double _costoOperacion;

        public double Alcance
        {
            get { return _alcance; }
        }

        public double CostoOperacion
        {
            get { return _costoOperacion; }
        }

        public int CantidadAsientos
        {
            get { return _cantAsientos; }
        }

        public string Modelo
        {
            get { return _modelo; }
        }

        public Avion(string fabricante, string modelo, int cantAsientos, double alcance, double costoOperacion)
        {
            _fabricante = fabricante;
            _modelo = modelo;
            _cantAsientos = cantAsientos;
            _alcance = alcance;
            _costoOperacion = costoOperacion;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_fabricante)) throw new Exception("El fabricante no puede ser vacío.");
            if (string.IsNullOrEmpty(_modelo)) throw new Exception("El modelo no puede ser vacío.");
            if (_cantAsientos <= 0) throw new Exception("La cantidad mínima de asientos debe ser al menos 1.");
            if (_alcance <= 0) throw new Exception("El alcance mínimo debe ser al menos 1.");
            if (_costoOperacion < 0) throw new Exception("El costo de operacion por km no puede ser negativo");
        }
    }
}