using SistemaVentas.BusinessLayer;
using SistemaVentas.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SistemaVentas.GUILayer
{
    public partial class frmFactura : Form
    {
        private BindingList<FacturaDetalle> detalle;

        public frmFactura()
        {
            InitializeComponent();

            //detalle = new BindingList<FacturaDetalle>;

        }

        private void frmFactura_Load(object sender, EventArgs e)
        {
            this.LlenarCombo(this._cboArticulo, new ProductoService().ObtenerTodos(), "Nombre", "IdProducto");
            this.LlenarCombo(this.cboCliente, new ClienteService().ObtenerTodos(), "NombreCliente", "IdCliente");
            this.txtDireccion.Text = "";

            this.dgvDetalle.DataSource = detalle;
        }

        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            cbo.ValueMember = value;
            cbo.DisplayMember = display;
            cbo.DataSource = source;
            cbo.SelectedIndex = -1;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                var factura = new Factura();
                //valores de la factura, incluyendo su detalle

                var facturaService = new FacturaService();

                facturaService.Crear(factura);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void _cboArticulo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (_cboArticulo != null)
            {
                ProductoService productoService = new ProductoService();

                Producto oProducto = productoService.ObtenerProductoPorId((int)this._cboArticulo.SelectedValue);

                this._txtPrecio.Text = oProducto.Precio.ToString("C");

                this._txtCantidad.Enabled = true;
                this._txtCantidad.Focus();
            }
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboCliente.SelectedIndex != -1)
            {
                Cliente oCliente = new ClienteService().ConsultarClientesPorId((int)this.cboCliente.SelectedValue);
                this.txtDireccion.Text = string.Concat(oCliente.DomicilioCalle, " ", oCliente.DomicilioNumero.ToString());
            }
        }

        private void _txtCantidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this._txtPrecio.Text))
            {
                double precio = double.Parse(this._txtPrecio.Text.Substring(1));
                int cantidad = int.Parse(this._txtCantidad.Text);
                this._txtImporte.Text = (cantidad * precio).ToString("C");
            }
        }

        private void _btnAgregar_Click(object sender, EventArgs e)
        {
            //Producto oProducto = new ProductoService().ObtenerProductoPorId((int)this._cboArticulo.SelectedValue);

            int cantidad = 0;
            int.TryParse(this._txtCantidad.Text, out cantidad);

            var producto = (Producto)_cboArticulo.SelectedItem;
            detalle.Add(new FacturaDetalle());
        }
    }
}
