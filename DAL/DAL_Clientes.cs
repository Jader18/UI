using EL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DAL_Clientes
    {
        public static Clientes Insert(Clientes Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                Entidad.Activo = true;
                Entidad.FechaRegistro = DateTime.Now;
                bd.Clientes.Add(Entidad);
                bd.SaveChanges();
                return Entidad;
            }
        }
        public static bool Update(Clientes Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                var Registro = bd.Clientes.Find(Entidad.IdCliente);
                Registro.NombreCliente  = Entidad.NombreCliente;
                Registro.Correo = Entidad.Correo;
                Registro.IdUsuarioActualiza = Entidad.IdUsuarioActualiza;
                Registro.FechaActualizacion = Entidad.FechaActualizacion;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool Anular(Clientes Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                var Registro = bd.Clientes.Find(Entidad.IdCliente);
                Registro.Activo = Entidad.Activo;
                Registro.IdUsuarioActualiza = Entidad.IdUsuarioActualiza;
                Registro.FechaActualizacion = Entidad.FechaActualizacion;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool Existe(Clientes Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                return bd.Clientes.Where(a => a.IdCliente == Entidad.IdCliente).Count() > 0;
            }
        }
        public static Clientes Registro(Clientes Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                return bd.Clientes.Where(a => a.IdCliente == Entidad.IdCliente).SingleOrDefault();
            }
        }
        public static List<Clientes> Lista(bool Activo = true)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                return bd.Clientes.Where(a => a.Activo == Activo).ToList();
            }
        }


    }
}
