using EL;
using System.IO.Pipes;

namespace DAL
{
    public static class DAL_Productos
    {
        public static Productos Insert(Productos Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                Entidad.Activo = true;
                Entidad.FechaRegistro = DateTime.Now;
                bd.Productos.Add(Entidad);
                bd.SaveChanges();
                return Entidad;
            }
        }
        public static bool Update(Productos Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                var Registro = bd.Productos.Find(Entidad.IdProducto);

                Registro.Descripcion = Entidad.Descripcion;
                Registro.IdUsuarioActualiza = Entidad.IdUsuarioActualiza;
                Registro.FechaActualizacion = Entidad.FechaActualizacion;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool Anular(Productos Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                var Registro = bd.Productos.Find(Entidad.IdProducto);
                Registro.Activo = false;
                Registro.IdUsuarioActualiza = Entidad.IdUsuarioActualiza;
                Registro.FechaActualizacion = DateTime.Now;
                return bd.SaveChanges() > 0;
            }
        }
        public static bool Existe(Productos Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                return bd.Productos.Where(a => a.IdProducto == Entidad.IdProducto).Count() > 0;
            }
        }
        public static Productos Registro(Productos Entidad)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                return bd.Productos.Where(a => a.IdProducto == Entidad.IdProducto).SingleOrDefault();
            }
        }
        public static List<Productos> Lista(bool Activo = true)
        {
            using (BDMPOO bd = new BDMPOO())
            {
                return bd.Productos.Where(a => a.Activo == Activo).ToList();
            }
        }

        public static bool ValidarCorreo(string productName, int IdRegistro)
        {
            using (BDMPOO bd = new())
            {
                return bd.Productos.Where(a => a.Descripcion == productName && a.IdProducto != IdRegistro && a.Activo == true).Count() > 0;
            }
        }

    }
}
