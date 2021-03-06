﻿using System;
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
    public partial class FrmProdutosPorCategoria : Form
    {
        public FrmProdutosPorCategoria()
        {
            InitializeComponent();
        }

        private void FrmProdutosPorCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            MeusFormularios.FormProdutosPorCategoria = null;
        }

        private void FrmProdutosPorCategoria_Load(object sender, EventArgs e)
        {
            this.categoriaBindingSource.DataSource = DataContextFactory.DataContext.Categorias;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            this.Pesquisar((int)cboCategoria.SelectedValue);
        }

        public void Pesquisar(int codigoCategoria)
        {
            this.produtoBindingSource.DataSource =
                DataContextFactory.DataContext.Produtos.Where(xProdutoPesquisa => xProdutoPesquisa.CodigoCategoria == codigoCategoria);
        }
    }
}
