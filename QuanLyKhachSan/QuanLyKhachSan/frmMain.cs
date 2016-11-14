using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frmMain : Form
    {
        TabPage tab1 = new TabPage();
        TabPage tab2 = new TabPage();
        TabPage tab3 = new TabPage();
        TabPage tab4 = new TabPage();
        public frmMain()
        {
            InitializeComponent();
        }
        public void remove()
        {
            tabControl1.TabPages.Remove(tab1);
            tabControl1.TabPages.Remove(tab2);
            tabControl1.TabPages.Remove(tab3);
            tabControl1.TabPages.Remove(tab4);
        }
        private void kháchHàngVàThanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove();
            frm_quanly_KhachHang_ThanhToan f = new frm_quanly_KhachHang_ThanhToan();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            tab1.Controls.Add(f);
            f.Visible = true;
            tab1.Text = "Khách hàng và thanh toán";
            tabControl1.TabPages.Add(tab1);
        }

        private void phòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove();
            frmPhong f = new frmPhong();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            tab2.Controls.Add(f);
            f.Visible = true;
            tab2.Text = "Phòng";
            tabControl1.TabPages.Add(tab2);
        }

        private void dịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove();
            frmDichVu f = new frmDichVu();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            tab3.Controls.Add(f);
            f.Visible = true;
            tab3.Text = "Dịch vụ";
            tabControl1.TabPages.Add(tab3);
        }

        private void dồDùngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remove();
            frmDoDung f = new frmDoDung();
            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            f.Dock = DockStyle.Fill;
            tab4.Controls.Add(f);
            f.Visible = true;
            tab4.Text = "Đồ dùng";
            tabControl1.TabPages.Add(tab4);
        }
    }
}
