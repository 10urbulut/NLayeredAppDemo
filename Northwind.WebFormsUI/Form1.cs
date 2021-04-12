using Northwind.Bussiness.Abstract;
using Northwind.Bussiness.Concrete;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete.EntityFramework0;
using Northwind.DataAccess.Concrete.NHibernate;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Northwind.Bussiness.DependencyResolvers.Ninject;

namespace Northwind.WebFormsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _productService = InstanceFactory.GetInstance<IProductService>();
            _categoryService = InstanceFactory.GetInstance<ICategoryService>();

        }
        IProductService _productService;
        ICategoryService _categoryService;
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadCategories();

        }

        private void LoadCategories()
        {
            cbxCategory.DataSource = _categoryService.GetAll();
            cbxCategory.DisplayMember = "CategoryName";
            cbxCategory.ValueMember = "CategoryId";

            cbxCategoryId.DataSource = _categoryService.GetAll();
            cbxCategoryId.DisplayMember = "CategoryName";
            cbxCategoryId.ValueMember = "CategoryId";

            cbxCategoryIdUpdate.DataSource = _categoryService.GetAll();
            cbxCategoryIdUpdate.DisplayMember = "CategoryName";
            cbxCategoryIdUpdate.ValueMember = "CategoryId";

        }

        private void LoadProducts()
        {
            dgvProduct.DataSource = _productService.GetAll();
        }

        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvProduct.DataSource = _productService.GetProductsByCategory(Convert.ToInt32(cbxCategory.SelectedValue));

            }
            catch
            {


            }
        }

        private void tbxProductName_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxProductName.Text))
            {
                dgvProduct.DataSource = _productService.
                               GetByProductName(tbxProductName.Text);
            }
            else
            {
                LoadProducts();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _productService.Add(new Product
            {
                CategoryId = Convert.ToInt32(cbxCategoryId.SelectedValue),
                ProductName = tbxProductName2.Text,
                QuantityPerUnit = tbxQuantityPerUnit.Text,
                UnitPrice = Convert.ToDecimal(tbxQuantityPerUnit.Text),
                UnitsInStock = Convert.ToInt16(tbxStocksInUnit.Text)

            });
            LoadProducts();
            MessageBox.Show("Added!");
        }

        private void btnProductsUpdate_Click(object sender, EventArgs e)
        {
            _productService.Update(new Product
            {
                ProductId = Convert.ToInt32
            (dgvProduct.CurrentRow.Cells[0].Value),
                ProductName = tbxProductNameUpdate.Text,
                CategoryId = Convert.ToInt32(cbxCategoryIdUpdate.SelectedValue),
                UnitsInStock = Convert.ToInt16(tbxUnitsInStockUpdate.Text),
                QuantityPerUnit = tbxQuantityPerUnitUpdate.Text,
                UnitPrice = Convert.ToDecimal(tbxUnitPriceUpdate.Text)

            });
            LoadProducts();
            MessageBox.Show("Updated!");
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvProduct.CurrentRow;
            tbxProductNameUpdate.Text = dgvProduct.CurrentRow.Cells[1].Value.ToString();
            cbxCategoryIdUpdate.SelectedValue = row.Cells[2].Value;
            tbxUnitPriceUpdate.Text = row.Cells[3].Value.ToString();
            tbxQuantityPerUnitUpdate.Text = row.Cells[4].Value.ToString();
            tbxUnitsInStockUpdate.Text = row.Cells[5].Value.ToString();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvProduct.CurrentRow != null)
            {

                _productService.Delete(new Product
                {
                    ProductId = Convert.ToInt32(dgvProduct.CurrentRow.Cells[0].Value)
                });

            }
            MessageBox.Show("Deleted!");
            LoadProducts();
        }
    }
}
