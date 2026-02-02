using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio
{
    public class Ruta : IValidable
    {
        private int _id;
        private Aeropuerto _salida;
        private Aeropuerto _llegada;
        private double _distancia;
        private static int s_ultId = 1;

        public double Distancia
        {
            get { return _distancia; }
        }

        public Aeropuerto Salida
        {
            get { return _salida; }
        }

        public Aeropuerto Llegada
        {
            get { return _llegada; }
        }

        public Ruta(Aeropuerto salida, Aeropuerto llegada, double distancia)
        {
            _id = s_ultId++;
            _salida = salida;
            _llegada = llegada;
            _distancia = distancia;
        }

        public void Validar()
        {
            if (_salida == null) throw new Exception("El aeropuerto de salida no puede ser nulo.");
            if (_llegada == null) throw new Exception("El aeropuerto de llegada no puede ser nulo.");
            if (_distancia < 0) throw new Exception("La distancia no puede ser negativa.");
        }
    }
}