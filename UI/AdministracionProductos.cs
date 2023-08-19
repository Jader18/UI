using BLL;
using EL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace UI
{
    public partial class AdministracionProductos : Form
    {

        private static int IdUsuarioSesion = 1;
        private static int IdRegistro = 0;
        public AdministracionProductos()
        {

            InitializeComponent();
        }

        private void AdministracionProductosLoad(object sender, EventArgs e)
        {
            cargarGrid();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }


        //Validar formulario
        private bool validarFormulario()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text) || string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                return false;
            }
            if (txtDescripcion.Text.Length > 200)
            {
                MessageBox.Show("El campo descripcion debe ser menor a 200 caracteres");
                return false;
            }

            if (BLL_Productos.ValidarCorreo(txtDescripcion.Text, IdRegistro))
            {
                MessageBox.Show("El producto ya se encuentra registrado.");
                return false;
            }

            return true;
        }


        //Guardar 
        private void Guardar()
        {
            if (validarFormulario())
            {
                Productos Entidad = new Productos();
                Entidad.Descripcion  = txtDescripcion.Text;

                if (IdRegistro > 0)
                {
                    //Actualizar
                    Entidad.IdProducto = IdRegistro;
                    Entidad.IdUsuarioActualiza = IdUsuarioSesion;
                    if (BLL_Productos.Update(Entidad))
                    {
                        MessageBox.Show("Registro actualizado con exito");
                        cargarGrid();
                        return;
                    }
                    MessageBox.Show("El Registro no fue actualizado con exito");
                    return;
                }

                //Insertar          
                Entidad.IdUsuarioRegistra = IdUsuarioSesion;
                if (BLL_Productos.Insert(Entidad).IdProducto > 0)
                {
                    MessageBox.Show("Registro agregado con exito");
                    cargarGrid();
                    return;
                }
                MessageBox.Show("El Registro no fue agregado con exito");
                return;

            }


        }


        //Anular

        private void Anular()
        {
            try
            {
                Productos entidad = new();
                entidad.IdProducto = IdRegistro;
                entidad.IdUsuarioActualiza = IdUsuarioSesion;
                if (BLL_Productos.Anular(entidad))
                {
                    MessageBox.Show("Registro anulado con exito");
                    cargarGrid();
                    return;
                }
                MessageBox.Show("El Registro no fue anulado con exito");
                return;
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message);
            }
        }

        //Cargar grid
        private void cargarGrid()
        {
            try
            {
                //listo el orden de la entidad
                gridProductos.DataSource = BLL_Productos.Lista();
                gridProductos.Columns[0].Visible = false;
                gridProductos.Columns[1].HeaderText = "Descripcion";
                gridProductos.Columns[2].Visible = false;
                gridProductos.Columns[3].Visible = false;
                gridProductos.Columns[4].Visible = false;
                gridProductos.Columns[5].Visible = false;
                gridProductos.Columns[6].Visible = false;

                LimpiarCampos();

            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Limpiar campos
        private void LimpiarCampos()
        {
            IdRegistro = 0;
            txtDescripcion.Text = string.Empty;
        }

        //Cargar campos 
        private void cargarCampos()
        {
            try
            {
                IdRegistro = Convert.ToInt32(gridProductos.CurrentRow.Cells[0].Value);
                txtDescripcion.Text = gridProductos.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        private void gridProductosCellclick(object sender, DataGridViewCellEventArgs e)
        {
            cargarCampos();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }
    }
}
