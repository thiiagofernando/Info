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
    public partial class FrmCategoria : Form
    {
        public FrmCategoria()
        {
            InitializeComponent();
        }

        private void FrmCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            MeusFormularios.FormCategoria = null;
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.categoriaBindingSource.DataSource = DataContextFactory.DataContext.Categorias;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            txtCategoria.Enabled = true;
            this.categoriaBindingSource.AddNew();
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            if(this.Valida())
            {
                this.categoriaBindingSource.EndEdit();
                DataContextFactory.DataContext.SubmitChanges();
                MessageBox.Show("Categoria Gravada com Sucesso!!","Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoria.Enabled = false;
            }


        }
        private bool Valida()
        {
            if(txtCategoria.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Campo Categoria não pode ser vazio", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoria.Focus();
                return false;
            }
            return true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.categoriaBindingSource.CancelEdit();
            txtCategoria.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Deseja Realmente Excluir Esta Categoria", "Excluir", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (this.CategoriaPossuiProduto(this.CategoriaCorrente))
                        MessageBox.Show("Impossível excluir, existe um ou mais produtos vinculados a esta categoria!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    else
                    {
                        this.categoriaBindingSource.RemoveCurrent();
                        DataContextFactory.DataContext.SubmitChanges();
                        MessageBox.Show("Categoria Excluída com Sucesso!!", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Operação Invalida!!, Use a opção Cancelar ou Reinicie o Sistema",ex.Message,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }


        }

        public Categoria CategoriaCorrente
        {
            get
            {
                return (Categoria)this.categoriaBindingSource.Current;
            }
        }
        private bool CategoriaPossuiProduto(Categoria categoria)
        {
            var produtos = DataContextFactory.DataContext.Produtos.Where(xProduto => xProduto.CodigoCategoria == categoria.Codigo);
            if (produtos.Count() > 0)
                return true;
            else
                return false;
        }
    }
}
