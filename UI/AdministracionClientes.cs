using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EL;
using BLL;
using Utility;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace UI
{
    public partial class AdministracionClientes : Form
    {
        private static int IdUsuarioSesion = 1;
        private static int IdRegistro = 0;
        public AdministracionClientes()
        {
            InitializeComponent();
        }

        private void AdministracionClientes_Load(object sender, EventArgs e)
        {
            cargarGrid();
            txtMensaje.Enabled = false;
            btnEnviar.Enabled = false;

        }

        #region Metodos y funciones
        private void cargarGrid()
        {
            try
            {
                //listo el orden de la entidad
                gridClientes.DataSource = BLL_Clientes.Lista();
                gridClientes.Columns[0].Visible = false;
                gridClientes.Columns[1].HeaderText = "Nombre del Cliente";
                gridClientes.Columns[2].HeaderText = "Número";
                gridClientes.Columns[4].Visible = false;
                gridClientes.Columns[5].Visible = false;
                gridClientes.Columns[6].Visible = false;
                gridClientes.Columns[7].Visible = false;
                gridClientes.Columns[8].Visible = false;
                LimpiarCampos();

            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool validarFormulario()
        {
            if (string.IsNullOrEmpty(txtNombreCliente.Text) || string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Ingrese el nombre del cliente");
                return false;
            }
            if (txtNombreCliente.Text.Length > 200)
            {
                MessageBox.Show("El campo nombre del cliente debe ser menor a 200 caracteres");
                return false;
            }
            if (string.IsNullOrEmpty(txtNumero.Text) || string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Ingrese el número del cliente");
                return false;
            }
            if (txtNumero.Text.Length < 8 || txtNumero.Text.Length > 8)
            {
                MessageBox.Show("Ingrese un número de teléfono válido.");
                return false;
            }
            if (BLL_Clientes.ValidarNumero(txtNumero.Text, IdRegistro))
            {
                MessageBox.Show("El número de teléfono ya se encuentra registrado.");
                return false;
            }
            if (string.IsNullOrEmpty(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Ingrese el correo del cliente");
                return false;
            }
            if (!(txtCorreo.Text.Length < 200))
            {
                MessageBox.Show("El campo correo debe ser menor a 200 caracteres");
                return false;
            }
            if (!General.ValidateEmail(txtCorreo.Text))
            {
                MessageBox.Show("Ingrese un correo válido");
                return false;
            }
            if (BLL_Clientes.ValidarCorreo(txtCorreo.Text, IdRegistro))
            {
                MessageBox.Show("El Correo ya se encuentra registrado.");
                return false;
            }

            return true;
        }
        private void Guardar()
        {
            if (validarFormulario())
            {
                Clientes Entidad = new Clientes();
                Entidad.NombreCliente  = txtNombreCliente.Text;
                Entidad.Numero = txtNumero.Text;
                Entidad.Correo = txtCorreo.Text;

                if (IdRegistro > 0)
                {
                    //Actualizar
                    Entidad.IdCliente = IdRegistro;
                    Entidad.IdUsuarioActualiza = IdUsuarioSesion;
                    if (BLL_Clientes.Update(Entidad))
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
                if (BLL_Clientes.Insert(Entidad).IdCliente > 0)
                {
                    MessageBox.Show("Registro agregado con exito");
                    cargarGrid();
                    return;
                }
                MessageBox.Show("El Registro no fue agregado con exito");
                return;
            }
        }
        private void Anular()
        {
            try
            {
                Clientes entidad = new();
                entidad.IdCliente = IdRegistro;
                entidad.IdUsuarioActualiza = IdUsuarioSesion;
                if (BLL_Clientes.Anular(entidad))
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
        private void cargarCampos()
        {
            try
            {
                IdRegistro = Convert.ToInt32(gridClientes.CurrentRow.Cells[0].Value);
                txtNombreCliente.Text = gridClientes.CurrentRow.Cells[1].Value.ToString();
                txtNumero.Text = gridClientes.CurrentRow.Cells[2].Value.ToString();
                txtCorreo.Text = gridClientes.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception Error)
            {
                MessageBox.Show(Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCampos()
        {
            IdRegistro = 0;
            txtNombreCliente.Text = string.Empty;
            txtNumero.Text = string.Empty;
            txtCorreo.Text = string.Empty;
        }
        #endregion

        #region Eventos de los controles
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            cargarGrid();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }
        private void gridClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cargarCampos();
        }
        private void btnAnular_Click(object sender, EventArgs e)
        {
            Anular();
        }

        #endregion

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            Guardar();


        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnAnular_Click_1(object sender, EventArgs e)
        {
            Anular();

        }

        private void gridClientes_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            cargarCampos();
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsNumber(e.KeyChar) || e.KeyChar == 8);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            btnEnviar.Enabled = txtMensaje.Enabled = ckEnviarCorreo.Checked;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void btnEnviar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMensaje.Text) || string.IsNullOrWhiteSpace(txtMensaje.Text))
            {
                MessageBox.Show("Escribe un mensaje");
                return;
            }

            if (General.EnviarCorreo("sistema@noreply.com", txtCorreo.Text, "Prueba",txtMensaje.Text))
            {
                MessageBox.Show("Mensaje enviado con exito.");
                return;
            }
            MessageBox.Show("No se envió el correo.");
        }

        private void txtMensaje_TextChanged(object sender, EventArgs e)
        {

        }
    }
}