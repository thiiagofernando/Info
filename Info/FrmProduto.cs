using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Info.DAL;

namespace Info
{
    public partial class FrmProduto : Form
    {
        public FrmProduto()
        {
            InitializeComponent();
        }

        private void FrmProduto_FormClosed(object sender, FormClosedEventArgs e)
        {
            MeusFormularios.FormProduto = null;
        }

        private void FrmProduto_Load(object sender, EventArgs e)
        {
            this.produtoBindingSource.DataSource = DataContextFactory.DataContext.Produtos;
            this.categoriaBindingSource.DataSource = DataContextFactory.DataContext.Categorias;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            descricaoTextBox.Enabled = true;
            valorTextBox.Enabled = true;
            codigoCategoriaComboBox.Enabled = true;
            this.produtoBindingSource.AddNew();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                this.produtoBindingSource.EndEdit();
                DataContextFactory.DataContext.SubmitChanges();
                GridviewProdutos.Refresh();
                descricaoTextBox.Enabled = false;
                valorTextBox.Enabled = false;
                codigoCategoriaComboBox.Enabled = false;
                MessageBox.Show("Produto Cadastrado com Sucesso!!");
            }
            catch (Exception ex)
            {

                this.produtoBindingSource.RemoveCurrent();
                DataContextFactory.DataContext.SubmitChanges();
                GridviewProdutos.Refresh();
                descricaoTextBox.Enabled = false;
                valorTextBox.Enabled = false;
                codigoCategoriaComboBox.Enabled = false;
                MessageBox.Show("Dados Invalidos!!","Verifique se todos os Campos foram preenchidos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.produtoBindingSource.CancelEdit();
            descricaoTextBox.Enabled = false;
            valorTextBox.Enabled = false;
            codigoCategoriaComboBox.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            this.produtoBindingSource.RemoveCurrent();
            DataContextFactory.DataContext.SubmitChanges();
            MessageBox.Show("Produto Excluido com Sucesso!!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            descricaoTextBox.Enabled = false;
            valorTextBox.Enabled = false;
            codigoCategoriaComboBox.Enabled = false;
        }

        private void produtoDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.ColumnIndex == 2)
                e.Value = ((Categoria)e.Value).Descricao;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

            descricaoTextBox.Enabled = true;
            valorTextBox.Enabled = true;
            codigoCategoriaComboBox.Enabled = true;
            this.produtoBindingSource.EndEdit();
            DataContextFactory.DataContext.SubmitChanges();

        }
    }
}
