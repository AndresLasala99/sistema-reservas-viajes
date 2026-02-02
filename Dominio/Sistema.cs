using Dominio.Comparadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private static Sistema s_instancia;
        private List<Usuario> _usuarios = new List<Usuario>();
        private List<Avion> _aviones = new List<Avion>();
        private List<Vuelo> _vuelos = new List<Vuelo>();
        private List<Aeropuerto> _aeropuertos = new List<Aeropuerto>();
        private List<Ruta> _rutas = new List<Ruta>();
        private List<Pasaje> _pasajes = new List<Pasaje>();

        public static Sistema Instancia
        {
            get
            {
                if (s_instancia == null) s_instancia = new Sistema();
                return s_instancia;
            }
        }

        public List<Vuelo> Vuelos
        {
            get { return _vuelos; }
        }

        public List<Pasaje> Pasajes
        {
            get { return _pasajes; }
        }

        public List<Usuario> Usuarios
        {
            get { return _usuarios; }
        }

        public Sistema()
        {
            PrecargarUsuarios();
            PrecargarAviones();
            PrecargarAeropuertos();
            PrecargarRutas();
            PrecargarVuelos();
            PrecargarPasaje();
        }

        public void AgregarUsuario(Usuario u)
        {
            if (u == null) throw new Exception("El usuario no puede ser nulo");
            u.Validar();
            foreach (Usuario usuario in _usuarios)
            {
                if (usuario.Correo == u.Correo) throw new Exception("Ya existe usuario con el correo dado.");//Acá estaba con el metodo Equals(), pero igualmente el sistema me estaba dejando crear usuarios con el mismo correo, por eso cambié
            }
            if (u is Cliente c)
            {
                foreach (Cliente cli in _usuarios.OfType<Cliente>())//con ayuda de chat gpt, se utiliza ToList para que se conveirta en una lista de tipo Cliente con "OfType<>".
                {
                    if (cli.Cedula == c.Cedula) throw new Exception("Ya existe cliente con la cédula dada.");//Acá lo mismo que mencione antes con el correo de los Usuarios
                }
            }
            _usuarios.Add(u);
        }

        private void PrecargarUsuarios()
        {
            // Administradores
            AgregarUsuario(new Administrador("admin1@correo.com", "admin123", "SuperAdmin"));
            AgregarUsuario(new Administrador("admin2@correo.com", "clave456", "GestorMain"));

            // Clientes Premium
            AgregarUsuario(new Premium("premium1@correo.com", "pass1", "12345678", "Juan Pérez", "Uruguaya", 1500));
            AgregarUsuario(new Premium("premium2@correo.com", "pass2", "87654321", "Ana Gómez", "Argentina", 2000));
            AgregarUsuario(new Premium("premium3@correo.com", "pass3", "45678912", "Luis Torres", "Chilena", 1800));
            AgregarUsuario(new Premium("premium4@correo.com", "pass4", "32165498", "Laura Silva", "Uruguaya", 1700));
            AgregarUsuario(new Premium("premium5@correo.com", "pass5", "98765432", "Carlos Méndez", "Paraguaya", 2200));

            // Clientes Ocasionales
            Random rand = new Random();// con ayuda de chat gpt en el atributo regalo, da un numero random que puede ser 0 o 1 y lo iguala a 1, si es 1 da true, si es 0 da false
            AgregarUsuario(new Ocasional("ocasional1@correo.com", "oc1", "14785236", "Marcela Ríos", "Uruguaya", rand.Next(0, 2) == 1));
            AgregarUsuario(new Ocasional("ocasional2@correo.com", "oc2", "36985214", "Federico Luna", "Argentina", rand.Next(0, 2) == 1));
            AgregarUsuario(new Ocasional("ocasional3@correo.com", "oc3", "75395146", "Romina Díaz", "Chilena", rand.Next(0, 2) == 1));
            AgregarUsuario(new Ocasional("ocasional4@correo.com", "oc4", "95175326", "Matías Suárez", "Uruguaya", rand.Next(0, 2) == 1));
            AgregarUsuario(new Ocasional("ocasional5@correo.com", "oc5", "15926374", "Camila Castro", "Uruguaya", rand.Next(0, 2) == 1));
        }

        public void AgregarAvion(Avion a)
        {
            if (a == null) throw new Exception("El avion no puede ser nulo.");
            a.Validar();
            _aviones.Add(a);
        }

        private void PrecargarAviones()
        {
            AgregarAvion(new Avion("Boeing", "737 MAX", 210, 6570.5, 8500.0));
            AgregarAvion(new Avion("Airbus", "A320neo", 180, 6300.0, 7900.0));
            AgregarAvion(new Avion("Embraer", "E195-E2", 132, 7315.3, 5600.0));
            AgregarAvion(new Avion("Bombardier", "CRJ900", 90, 6950.2, 4200.0));
        }

        public void AgregarAeropuerto(Aeropuerto a)
        {
            if (a == null) throw new Exception("El aeropuerto no puede ser nulo.");
            a.Validar();
            if (_aeropuertos.Contains(a)) throw new Exception("Ya existe Aeropuerto con el código IATA dado.");
            _aeropuertos.Add(a);
        }

        private void PrecargarAeropuertos()
        {
            AgregarAeropuerto(new Aeropuerto("MVD", "Montevideo", 5000.0, 1500.0));
            AgregarAeropuerto(new Aeropuerto("EZE", "Buenos Aires", 5200.0, 1600.0));
            AgregarAeropuerto(new Aeropuerto("GRU", "São Paulo", 6000.0, 1700.0));
            AgregarAeropuerto(new Aeropuerto("SCL", "Santiago", 5500.0, 1550.0));
            AgregarAeropuerto(new Aeropuerto("BOG", "Bogotá", 5800.0, 1650.0));
            AgregarAeropuerto(new Aeropuerto("LIM", "Lima", 5300.0, 1400.0));
            AgregarAeropuerto(new Aeropuerto("MEX", "Ciudad de México", 6100.0, 1750.0));
            AgregarAeropuerto(new Aeropuerto("JFK", "Nueva York", 7000.0, 1900.0));
            AgregarAeropuerto(new Aeropuerto("LAX", "Los Ángeles", 7200.0, 2000.0));
            AgregarAeropuerto(new Aeropuerto("MAD", "Madrid", 6800.0, 1850.0));
            AgregarAeropuerto(new Aeropuerto("BCN", "Barcelona", 6600.0, 1800.0));
            AgregarAeropuerto(new Aeropuerto("CDG", "París", 6900.0, 1880.0));
            AgregarAeropuerto(new Aeropuerto("FRA", "Frankfurt", 7100.0, 1950.0));
            AgregarAeropuerto(new Aeropuerto("LHR", "Londres", 7500.0, 2100.0));
            AgregarAeropuerto(new Aeropuerto("AMS", "Ámsterdam", 6700.0, 1820.0));
            AgregarAeropuerto(new Aeropuerto("MIA", "Miami", 6400.0, 1700.0));
            AgregarAeropuerto(new Aeropuerto("PTY", "Ciudad de Panamá", 5600.0, 1450.0));
            AgregarAeropuerto(new Aeropuerto("CUN", "Cancún", 5900.0, 1600.0));
            AgregarAeropuerto(new Aeropuerto("UIO", "Quito", 5400.0, 1500.0));
            AgregarAeropuerto(new Aeropuerto("ASU", "Asunción", 5100.0, 1400.0));
        }

        public void AgregarRuta(Ruta r)
        {
            if (r == null) throw new Exception("La ruta no puede ser nula.");
            r.Validar();
            _rutas.Add(r);
        }

        private void PrecargarRutas()
        {
            AgregarRuta(new Ruta(_aeropuertos[0], _aeropuertos[1], 950.0));
            AgregarRuta(new Ruta(_aeropuertos[1], _aeropuertos[2], 1700.0));
            AgregarRuta(new Ruta(_aeropuertos[2], _aeropuertos[3], 2600.0));
            AgregarRuta(new Ruta(_aeropuertos[3], _aeropuertos[4], 2450.0));
            AgregarRuta(new Ruta(_aeropuertos[4], _aeropuertos[5], 1900.0));
            AgregarRuta(new Ruta(_aeropuertos[5], _aeropuertos[6], 3600.0));
            AgregarRuta(new Ruta(_aeropuertos[6], _aeropuertos[7], 3300.0));
            AgregarRuta(new Ruta(_aeropuertos[7], _aeropuertos[8], 4000.0));
            AgregarRuta(new Ruta(_aeropuertos[8], _aeropuertos[9], 5600.0));
            AgregarRuta(new Ruta(_aeropuertos[9], _aeropuertos[10], 620.0));

            AgregarRuta(new Ruta(_aeropuertos[10], _aeropuertos[11], 840.0));
            AgregarRuta(new Ruta(_aeropuertos[11], _aeropuertos[12], 480.0));
            AgregarRuta(new Ruta(_aeropuertos[12], _aeropuertos[13], 750.0));
            AgregarRuta(new Ruta(_aeropuertos[13], _aeropuertos[14], 360.0));
            AgregarRuta(new Ruta(_aeropuertos[14], _aeropuertos[15], 900.0));
            AgregarRuta(new Ruta(_aeropuertos[15], _aeropuertos[16], 1900.0));
            AgregarRuta(new Ruta(_aeropuertos[16], _aeropuertos[17], 2400.0));
            AgregarRuta(new Ruta(_aeropuertos[17], _aeropuertos[18], 1800.0));
            AgregarRuta(new Ruta(_aeropuertos[18], _aeropuertos[19], 1700.0));
            AgregarRuta(new Ruta(_aeropuertos[19], _aeropuertos[0], 2200.0));

            AgregarRuta(new Ruta(_aeropuertos[1], _aeropuertos[0], 950.0));
            AgregarRuta(new Ruta(_aeropuertos[3], _aeropuertos[1], 1300.0));
            AgregarRuta(new Ruta(_aeropuertos[5], _aeropuertos[2], 2200.0));
            AgregarRuta(new Ruta(_aeropuertos[7], _aeropuertos[4], 2900.0));
            AgregarRuta(new Ruta(_aeropuertos[9], _aeropuertos[6], 4500.0));
            AgregarRuta(new Ruta(_aeropuertos[11], _aeropuertos[8], 3700.0));
            AgregarRuta(new Ruta(_aeropuertos[13], _aeropuertos[10], 750.0));
            AgregarRuta(new Ruta(_aeropuertos[15], _aeropuertos[12], 930.0));
            AgregarRuta(new Ruta(_aeropuertos[17], _aeropuertos[14], 1100.0));
            AgregarRuta(new Ruta(_aeropuertos[19], _aeropuertos[16], 1200.0));
        }

        public void AgregarVuelo(Vuelo v)
        {
            if (v == null) throw new Exception("El vuelo no puede ser nulo.");
            v.Validar();
            if (_vuelos.Contains(v)) throw new Exception("Ya existe vuelo con el código dado.");
            _vuelos.Add(v);
        }

        public void PrecargarVuelos()
        {
            AgregarVuelo(new Vuelo("AV1", _rutas[0], _aviones[0], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("LT23", _rutas[1], _aviones[1], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("XR105", _rutas[2], _aviones[2], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }));
            AgregarVuelo(new Vuelo("QZ9999", _rutas[3], _aviones[3], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));
            AgregarVuelo(new Vuelo("AV25", _rutas[4], _aviones[0], new List<DayOfWeek> { DayOfWeek.Monday }));
            AgregarVuelo(new Vuelo("LT300", _rutas[5], _aviones[1], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("XR888", _rutas[6], _aviones[2], new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday }));
            AgregarVuelo(new Vuelo("QZ42", _rutas[7], _aviones[3], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }));
            AgregarVuelo(new Vuelo("AV77", _rutas[8], _aviones[0], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));
            AgregarVuelo(new Vuelo("LT199", _rutas[9], _aviones[1], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }));

            AgregarVuelo(new Vuelo("XR7", _rutas[10], _aviones[2], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("QZ456", _rutas[11], _aviones[3], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));
            AgregarVuelo(new Vuelo("AV108", _rutas[12], _aviones[0], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }));
            AgregarVuelo(new Vuelo("LT950", _rutas[13], _aviones[1], new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday }));
            AgregarVuelo(new Vuelo("XR12", _rutas[14], _aviones[2], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("QZ1001", _rutas[15], _aviones[3], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("AV84", _rutas[16], _aviones[0], new List<DayOfWeek> { DayOfWeek.Monday }));
            AgregarVuelo(new Vuelo("LT478", _rutas[17], _aviones[1], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }));
            AgregarVuelo(new Vuelo("XR312", _rutas[18], _aviones[2], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("QZ5", _rutas[19], _aviones[3], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));

            AgregarVuelo(new Vuelo("AV999", _rutas[20], _aviones[0], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("LT66", _rutas[21], _aviones[1], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }));
            AgregarVuelo(new Vuelo("XR1000", _rutas[22], _aviones[2], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));
            AgregarVuelo(new Vuelo("QZ13", _rutas[23], _aviones[3], new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday }));
            AgregarVuelo(new Vuelo("AV44", _rutas[24], _aviones[0], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("LT70", _rutas[25], _aviones[1], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
            AgregarVuelo(new Vuelo("XR4321", _rutas[26], _aviones[2], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday }));
            AgregarVuelo(new Vuelo("QZ202", _rutas[27], _aviones[3], new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Thursday }));
            AgregarVuelo(new Vuelo("AV314", _rutas[28], _aviones[0], new List<DayOfWeek> { DayOfWeek.Monday }));
            AgregarVuelo(new Vuelo("LT900", _rutas[29], _aviones[1], new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }));
        }

        public void AgregarPasaje(Pasaje p)
        {
            if (p == null) throw new Exception("El pasaje no puede ser nulo.");
            p.Validar();
            _pasajes.Add(p);
        }

        public void PrecargarPasaje()
        {
            List<Cliente> clientes = ListarTodosLosClientes();

            AgregarPasaje(new Pasaje(_vuelos[0], new DateTime(2025, 05, 02), clientes[1], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[1], new DateTime(2025, 05, 05), clientes[1], TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[2], new DateTime(2025, 05, 06), clientes[2], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[3], new DateTime(2025, 05, 08), clientes[3], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[4], new DateTime(2025, 05, 12), clientes[4], TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[5], new DateTime(2025, 05, 14), clientes[5], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[6], new DateTime(2025, 05, 17), clientes[6], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[7], new DateTime(2025, 05, 20), clientes[7], TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[8], new DateTime(2025, 05, 22), clientes[8], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[9], new DateTime(2025, 05, 23), clientes[9], TipoEquipaje.CABINA));

            AgregarPasaje(new Pasaje(_vuelos[11], new DateTime(2025, 05, 27), clientes[1], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[12], new DateTime(2025, 05, 29), clientes[2], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[13], new DateTime(2025, 05, 31), clientes[3], TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[14], new DateTime(2025, 06, 2), clientes[4], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[15], new DateTime(2025, 06, 6), clientes[5], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[16], new DateTime(2025, 06, 9), clientes[6], TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[17], new DateTime(2025, 06, 10), clientes[7], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[18], new DateTime(2025, 06, 11), clientes[8], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[19], new DateTime(2025, 06, 12), clientes[9], TipoEquipaje.LIGHT));

            AgregarPasaje(new Pasaje(_vuelos[20], new DateTime(2025, 06, 13), clientes[0], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[21], new DateTime(2025, 06, 17), clientes[1], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[22], new DateTime(2025, 06, 19), clientes[2], TipoEquipaje.LIGHT));
            AgregarPasaje(new Pasaje(_vuelos[23], new DateTime(2025, 06, 21), clientes[3], TipoEquipaje.BODEGA));
            AgregarPasaje(new Pasaje(_vuelos[24], new DateTime(2025, 06, 23), clientes[4], TipoEquipaje.CABINA));
            AgregarPasaje(new Pasaje(_vuelos[10], new DateTime(2025, 05, 26), clientes[0], TipoEquipaje.LIGHT));
        }

        public List<Cliente> ListarTodosLosClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            foreach (Usuario u in _usuarios)
            {
                if (u is Cliente c) clientes.Add(c);
            }
            return clientes;
        }

        public List<Vuelo> BuscarVuelosPorAeropuerto(string iata)
        {
            List<Vuelo> buscados = new List<Vuelo>();
            foreach (Vuelo v in _vuelos)
            {
                if (v.Ruta.Salida.Iata.ToUpper() == iata.ToUpper() || v.Ruta.Llegada.Iata.ToUpper() == iata.ToUpper()) buscados.Add(v);
            }
            return buscados;
        }

        public Vuelo BuscarVueloPorCodigo(string codigo)
        {
            Vuelo v = null;
            List<Vuelo> vuelos = _vuelos;
            int i = 0;
            while (v == null && vuelos.Count != 0)
            {
                if (vuelos[i].Codigo.ToUpper() == codigo.ToUpper()) v = vuelos[i];
                i++;
            }
            return v;
        }

        public List<Pasaje> ListarPasajesEntre2Fechas(DateTime fecha1, DateTime fecha2)
        {
            List<Pasaje> pasajesEntre2Fechas = new List<Pasaje>();
            foreach (Pasaje p in _pasajes)
            {
                if (p.Fecha.Date >= fecha1.Date && p.Fecha.Date <= fecha2.Date) pasajesEntre2Fechas.Add(p);
            }
            return pasajesEntre2Fechas;
        }

        public Cliente BuscarClientePorCedula(string cedula)
        {
            Cliente c = null;
            List<Cliente> clientes = ListarTodosLosClientes();
            int i = 0;
            while (c == null && i < clientes.Count)
            {
                if (clientes[i].Cedula == cedula) c = clientes[i];
                i++;
            }
            return c;
        }

        public Usuario BuscarUsuarioPorCorreo(string correo)
        {
            Usuario u = null;
            int i = 0;
            while (u == null && i < _usuarios.Count)
            {
                if (_usuarios[i].Correo == correo) u = _usuarios[i];
                i++;
            }
            return u;
        }

        public void ModificarPuntosUsuario(string cedula, int nuevoPuntos)
        {
            Usuario u = BuscarClientePorCedula(cedula);
            if (u == null) throw new Exception("No se encontró cliente con la cédula dada.");
            if (u is Premium p) p.CambiarPuntos(nuevoPuntos);
        }

        public void ModificarRegaloUsuario(string cedula, bool nuevoRegalo)
        {
            Usuario u = BuscarClientePorCedula(cedula);
            if (u == null) throw new Exception("No se encontró cliente con la cédula dada.");
            if (u is Ocasional o) o.CambiarRegalo(nuevoRegalo);
        }

        public Usuario Login(string correo, string pass)
        {
            Usuario u = null;
            int i = 0;
            while (u == null && i < _usuarios.Count)
            {
                if (_usuarios[i].Correo == correo && _usuarios[i].Pass == pass) u = _usuarios[i];
                i++;
            }
            return u;
        }

        public void CrearCompra(string codigoVuelo, DateTime fecha, TipoEquipaje equipaje, string correo)
        {
            Vuelo v = BuscarVueloPorCodigo(codigoVuelo);
            if (v == null) throw new Exception("Vuelo no encontrado.");
            Usuario u = BuscarUsuarioPorCorreo(correo);
            Cliente c = u as Cliente;
            if (c == null) throw new Exception("Cliente no encontrado");
            Pasaje p = new Pasaje(v, fecha, c, equipaje);
            p.Validar();
            _pasajes.Add(p);
        }

        public List<Pasaje> PasajesDeUnCliente(string correo)
        {
            Usuario u = BuscarUsuarioPorCorreo(correo);
            Cliente c = u as Cliente;
            if (c == null) throw new Exception("Cliente no encontrado");
            List<Pasaje> pasajesComprados = new List<Pasaje>();
            foreach (Pasaje p in _pasajes)
            {
                if (p.Cliente.Equals(c)) pasajesComprados.Add(p);
            }
            pasajesComprados.Sort();
            return pasajesComprados;
        }

        public List<Cliente> ListarClientesOrdenadosPorCedula()
        {
            List<Cliente> clientesOrdenadosPorCedula = new List<Cliente>();
            foreach (Usuario u in _usuarios)
            {
                if (u is Cliente c) clientesOrdenadosPorCedula.Add(c);
            }
            clientesOrdenadosPorCedula.Sort();
            return clientesOrdenadosPorCedula;
        }

        public List<Pasaje> ListarPasajesOrdenadosPorFecha()
        {
            List<Pasaje> pasajes = _pasajes;
            pasajes.Sort(new PasajesOrdenadosPorFecha());
            return pasajes;
        }
    }
}