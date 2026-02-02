using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dominio.Interfaces;

namespace Dominio
{
    public class Pasaje : IValidable , IComparable<Pasaje>
    {
        private int _id;
        private Vuelo _vuelo;
        private DateTime _fecha;
        private Cliente _cliente;
        private TipoEquipaje _equipaje;
        private static int s_ultId = 1;

        public DateTime Fecha
        {
            get { return _fecha; }
        }
        public int Id
        {
            get { return _id; }
        }
        public Vuelo Vuelo
        {
            get { return _vuelo; }
        }

        public TipoEquipaje TipoEquipaje
        {
            get { return _equipaje; }
        }

        public double Precio
        {
            get { return CalcularPrecioPasaje(); }
        }

        public Cliente Cliente
        {
            get { return _cliente; }
        }

        public Pasaje(Vuelo vuelo, DateTime fecha, Cliente cliente, TipoEquipaje equipaje)
        {
            _id = s_ultId++;
            _vuelo = vuelo;
            _fecha = fecha;
            _cliente = cliente;
            _equipaje = equipaje;
        }

        public void Validar()
        {
            if (_vuelo == null) throw new Exception("El vuelo no puede ser nulo.");
            if (_fecha == new DateTime()) throw new Exception("La fecha es inválida.");
            if (_cliente == null) throw new Exception("El usuario no puede ser nulo.");
            if (!_vuelo.Frecuencia.Contains(_fecha.DayOfWeek)) throw new Exception("La frecuencia del vuelo no coincide con el día del pasaje.");
        }

        public override string ToString()
        {
            return $"Id: {_id} - Nombre del pasajero: {_cliente.Nombre} - Fecha: {_fecha.ToShortDateString()} - Nro Vuelo: {_vuelo.Codigo}";
        }

        public double CalcularPrecioPasaje()
        {
            double total = _vuelo.CostoPorAsiento * (1 + (25.0 + _cliente.ObtenerPorcentajeEquipaje(_equipaje)) / 100.0) + _vuelo.Ruta.Salida.CostoTasas + _vuelo.Ruta.Llegada.CostoTasas;
            return total;
        }

        public int CompareTo(Pasaje? other)
        {
            return other.CalcularPrecioPasaje().CompareTo(this.CalcularPrecioPasaje());
        }
    }
}