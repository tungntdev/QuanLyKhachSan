using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAO.EF;
using DAO;
using System.Data.Sql;
using System.Data.SqlClient;
namespace QuanLyKhachSan
{
    public partial class frm_quanly_KhachHang_ThanhToan : Form
    {
        static int luu = 0;// luu=1; them khach hang;
                           // luu = 2; sua khach hang;
                           // luu =3; xoa khach hang;    
        static int choice_them = 1;
        static int choice_sua = 2;
        static int choice_xoa = 3;
        QuanLyKhachSanDBContext db = new QuanLyKhachSanDBContext();
        public frm_quanly_KhachHang_ThanhToan()
        {
            InitializeComponent();
            
            // khoi tao datagridview
            KhoiTaoDataGridView();
            // setheader columns
            SetHeaderColumn();
            // khoi tao listview do ra
            KhoitaoListView();
            //load combobox do dung, dich vu
            LoadComboBox();
        }
        public void LoadComboBox()
        {
            var x = (from p in db.tblDoDungs select p).ToList();
            cbDoDung.DataSource = x;
            cbDoDung.DisplayMember = "TenDoDung";
            cbDoDung.ValueMember = "DonGia";
            var y = (from p in db.tblDichVus select p).ToList();
            cbDichVu.DataSource = y;
            cbDichVu.ValueMember = "GiaThanh";
            cbDichVu.DisplayMember = "TenDichVu";
            txtGiaDichVu.Text = "0";
            txtGiaDoDung.Text = "0";
        }
        public void ResetAll()
        {
            txtDiaChi.ResetText();
            txtTen.ResetText();
            txtSDT.ResetText();
            txtSoLuongNguoi.ResetText();
            txtTongTien.ResetText();
        }
        public void SetHeaderColumn()
        {
            dgvKhachHang.Columns["IDKhach"].HeaderText = "Mã số khách hàng";
            dgvKhachHang.Columns["TenKhach"].HeaderText = "Tên khách hàng";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns["SDT"].HeaderText = "Số điện thoại";
            dgvKhachHang.Columns["SoNguoi"].HeaderText = "Số người";
        }
        public void KhoitaoListView()
        {
           // IEnumerable<tblPhong> a = from p in db.tblPhongs
          //                        select p;// 2 cach deu ok
            var a = from p in db.tblPhongs
                                      select p;
            //foreach(tblPhong m in a)
            foreach ( var m in a)
            {
                int x = m.IDPhong;
                ListViewItem item = new ListViewItem("phongso"+x.ToString());
                 item.SubItems.Add(x.ToString());// subitem[1]
                lvPhong.Items.Add(item);
            }
        }

        public void KhoiTaoDataGridView()
        {
            dgvKhachHang.DataSource = null;
            var x = (from p in db.tblKhaches select new { p.IDKhach, p.TenKhach, p.DiaChi, p.SDT, p.SoNguoi }).ToList();
            dgvKhachHang.DataSource = x;
        }

        public void MoKhoaAll()
        {
            btnThem.Enabled = true;
            btnThem2.Enabled = true;
            btnSua.Enabled = true;
            
            btnXoa2.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem2.Enabled = false;
            
            btnXoa2.Enabled = false;
            luu = choice_them;
        }
        /*
        private void lvPhong_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (ListViewItem item in lvPhong.Items)
            {
                if(item.Checked== true)
                {
                    int a = int.Parse(item.SubItems[1].Text);
                    txtTen.Text = a.ToString();
                }
            }
        }
        */
        private void btnLuu_Click(object sender, EventArgs e)
        {
            /*foreach (ListViewItem item in lvPhong.Items)
            {
                if(item.Checked== true)
                {
                    int a = int.Parse(item.SubItems[1].Text);
                    txtTen.Text = a.ToString();
                }
            }*/
            KhachHangDAO dao_kh = new KhachHangDAO();
            ChiTietPhieuDAO dao_ctp = new ChiTietPhieuDAO();
            PhieuThueDAO dao_pt = new PhieuThueDAO();

            if ( luu == choice_them)
            {
                
              
                // them khach hang
                tblKhach k = new tblKhach();
                k.TenKhach = txtTen.Text;
                k.SDT = txtSDT.Text;
                k.DiaChi = txtDiaChi.Text;
                k.SoNguoi = int.Parse(txtSoLuongNguoi.Text);
                if(db.tblKhaches.Count()== db.tblKhaches.Max(x=>x.IDKhach))
                {
                    var temp = db.tblKhaches.Max(x => x.IDKhach) + 1;
                    k.IDKhach = temp;
                }
                if(db.tblKhaches.Count() != db.tblKhaches.Max(x=>x.IDKhach))
                {
                   while(true)
                    {
                        if(db.tblKhaches.Min(x=>x.IDKhach)>1)
                        {
                            var temp1 = db.tblKhaches.Min(x => x.IDKhach) - 1;
                            k.IDKhach = temp1;
                            break;
                        }
                        for (int i = db.tblKhaches.Min(x => x.IDKhach); i <= db.tblKhaches.Max(x => x.IDKhach); i++)
                        {
                            if (db.tblKhaches.SingleOrDefault(x => x.IDKhach == i) != null)
                            {
                                continue;
                            }
                                if (db.tblKhaches.SingleOrDefault(x => x.IDKhach == i) == null)
                            {
                                k.IDKhach = i;
                                break;
                            }
                        }
                        break;
                    }
                }
                
                dao_kh.Insert(k);

                // them vao trong phieuthue
                tblPhieuThue pt = new tblPhieuThue();
                pt.IDKhach = k.IDKhach;
                pt.ThoiGianThue = DateTime.Now;
                pt.ThoiGianTra = DateTime.Now;
                pt.TongTien = 0;
                /*for( int i=1; i<db.tblPhieuThues.Max(x=>x.IDPhieuThue);i++)
                {
                    if( db.tblPhieuThues.SingleOrDefault(x=>x.IDPhieuThue==i)==null)
                    {
                        pt.IDPhieuThue = i;
                    }
                }*/
                if (db.tblPhieuThues.Count() == db.tblPhieuThues.Max(x => x.IDPhieuThue))
                {
                    var temp = db.tblPhieuThues.Max(x => x.IDPhieuThue) + 1;
                    pt.IDPhieuThue = temp;
                }
                if (db.tblPhieuThues.Count() != db.tblPhieuThues.Max(x => x.IDPhieuThue))
                {
                    while (true)
                    {
                        if (db.tblPhieuThues.Min(x => x.IDPhieuThue) > 1)
                        {
                            var temp1 = db.tblPhieuThues.Min(x => x.IDPhieuThue) - 1;
                            pt.IDPhieuThue = temp1;
                            break;
                        }
                        for (int i = db.tblPhieuThues.Min(x => x.IDPhieuThue); i <= db.tblPhieuThues.Max(x => x.IDPhieuThue); i++)
                        {
                            if(db.tblPhieuThues.SingleOrDefault(x => x.IDPhieuThue == i) != null)
                            {
                                continue;
                            }
                            if (db.tblPhieuThues.SingleOrDefault(x => x.IDPhieuThue == i) == null)
                            {
                                pt.IDPhieuThue =  i;
                                break;
                            }
                           
                        }
                        break;
                    }
                }
                //
                
                dao_pt.Insert(pt);


                // them phong voi id khach da cho va them vao o tong tien
                // them vao chitietphieuthue
                foreach(ListViewItem item in lvPhong.Items)
                {
                    if(item.Checked == true)
                    {
                        int i_room = int.Parse(item.SubItems[1].Text);

                        tblChiTietPhieu ctp = new tblChiTietPhieu();
                        ctp.IDPhong = i_room;
                        ctp.IDDichVu = 0;
                        ctp.IDDoDung = 0;
                        ctp.SoLuong = 0;
                        ctp.ThanhTien = 0;
                        ctp.IDPhieuThue = pt.IDPhieuThue;
                        
                        if (db.tblChiTietPhieux.Count() == db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu))
                        {
                            var temp = db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu) + 1;
                           ctp.IDChiTietPhieu = temp;
                        }
                        if (db.tblChiTietPhieux.Count() != db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu))
                        {
                            while (true)
                            {
                                if (db.tblChiTietPhieux.Min(x => x.IDChiTietPhieu) > 1)
                                {
                                    var temp1 = db.tblChiTietPhieux.Min(x => x.IDChiTietPhieu) - 1;
                                    ctp.IDChiTietPhieu = temp1;
                                    break;
                                }
                                for (int i = db.tblChiTietPhieux.Min(x => x.IDChiTietPhieu); i <= db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu); i++)
                                {
                                    if (db.tblChiTietPhieux.SingleOrDefault(x => x.IDChiTietPhieu == i) != null)
                                    {
                                        continue;
                                    }
                                        if (db.tblChiTietPhieux.SingleOrDefault(x => x.IDChiTietPhieu == i) == null)
                                    {
                                        ctp.IDChiTietPhieu = i;
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                        dao_ctp.Insert(ctp);
                    }
                }
                //luu xong
                luu = 0;
                MoKhoaAll();
                KhoiTaoDataGridView();
                ResetAll();
            }

            if(luu==choice_sua)
            {
                // sua trong bang khachhang
                // sua trong bang co idphong, tuc la bang chitietphieuthue( ma muon co bang chi tiet phieu thue de sua thi lai
                // phai lay duoc idphieuthue co ma khach hang
                DataGridViewRow dr = dgvKhachHang.SelectedRows[0];
                int id = int.Parse(dr.Cells["IDKhach"].Value.ToString());
                tblKhach k = new tblKhach();
                k.IDKhach = id;
                k.SDT = txtSDT.Text;
                k.SoNguoi = int.Parse(txtSoLuongNguoi.Text);
                k.TenKhach = txtTen.Text;
                k.DiaChi = txtDiaChi.Text;
                dao_kh.Edit(k);
                MessageBox.Show("Bạn sửa thành công!");
                /*
                // lay idphieu thue co makhach hang
                var phieu = from u in db.tblPhieuThues
                            join u1 in db.tblKhaches on u.IDKhach equals u1.IDKhach
                            where u1.IDKhach == id
                            select new
                            {
                                u.IDPhieuThue
                            };
                foreach( var u in phieu)
                {
                    int a = u.IDPhieuThue;
                    // sua trong bang chitietphieuthue co IDPhieuThue, IDPhong=
                }
                */
                luu = 0;
                MoKhoaAll();
                KhoiTaoDataGridView();
                ResetAll();
            }

            if(luu == choice_xoa)
            {
                // xoa thi phai xoa o trong khach, phieu, chitietphieu
                DataGridViewRow dr = dgvKhachHang.SelectedRows[0];
                int id = int.Parse(dr.Cells["IDKhach"].Value.ToString());
                if (DialogResult.Yes == MessageBox.Show("Bạn chắc không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    //xoa kh
                    dao_kh.Delete(id);
                    //xoa  phieu
                    var phieu = from u in db.tblPhieuThues
                                join u1 in db.tblKhaches on u.IDKhach equals u1.IDKhach
                                where u1.IDKhach == id
                                select new
                                {
                                    u.IDPhieuThue
                                };
                    foreach (var u in phieu)
                    {
                        int a = u.IDPhieuThue;
                        // sua trong bang chitietphieuthue co IDPhieuThue, IDPhong=
                        dao_pt.Delete(a);
                        // xoa trong chi tiet phieu
                        dao_ctp.Delete(a);
                            
                    }
                }
                luu = 0;
                MoKhoaAll();
                KhoiTaoDataGridView();
                ResetAll();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            btnThem2.Enabled = false;
          
            btnXoa2.Enabled = false;
            lvPhong.Enabled = false;
            luu = choice_sua;
        }
        public void LoadBangDoDung()
        {
            DataGridViewRow dr = dgvKhachHang.SelectedRows[0];
            int id = int.Parse(dr.Cells["IDKhach"].Value.ToString());
            var bangDoDung = (from a in db.tblKhaches
                              join b in db.tblPhieuThues on a.IDKhach equals b.IDKhach
                              join c in db.tblChiTietPhieux on b.IDPhieuThue equals c.IDPhieuThue
                              join d in db.tblDoDungs on c.IDDoDung equals d.IDDoDung
                              where a.IDKhach == id
                              select new
                              {
                                  c.IDChiTietPhieu,
                                  c.IDPhong,
                                  d.TenDoDung,
                                  d.DonGia,
                                  c.SoLuong
                              }).ToList();
            dgvDoDung.DataSource = bangDoDung;
        }

        public void LoadBangDichVu()
        {
            DataGridViewRow dr = dgvKhachHang.SelectedRows[0];
            int id = int.Parse(dr.Cells["IDKhach"].Value.ToString());
            var bangDV = (from a in db.tblKhaches
                          join b in db.tblPhieuThues on a.IDKhach equals b.IDKhach
                          join c in db.tblChiTietPhieux on b.IDPhieuThue equals c.IDPhieuThue
                          join d in db.tblDichVus on c.IDDichVu equals d.IDDichVu
                          where a.IDKhach == id
                          select new
                          {
                              c.IDChiTietPhieu,
                              c.IDPhong,
                              d.TenDichVu,
                              d.GiaThanh
                          }).ToList();
            dgvDichVu.DataSource = bangDV;
        }
        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            DataGridViewRow dr = dgvKhachHang.SelectedRows[0];
            txtTen.Text = dr.Cells["TenKhach"].Value.ToString();
            txtDiaChi.Text = dr.Cells["DiaChi"].Value.ToString();
            txtSDT.Text = dr.Cells["SDT"].Value.ToString();
            txtSoLuongNguoi.Text = dr.Cells["SoNguoi"].Value.ToString();

                int id = int.Parse(dr.Cells["IDKhach"].Value.ToString());
                var x = from pt in db.tblPhieuThues
                        join ctp in db.tblChiTietPhieux on pt.IDPhieuThue equals ctp.IDPhieuThue
                        where pt.IDKhach == id
                        select new
                        {
                            ctp.IDPhong
                        };
            foreach (ListViewItem item in lvPhong.Items)
            {
                item.Checked = false;
            }
            foreach (var y in x)
                {
                    int i_room = y.IDPhong;
               
                foreach (ListViewItem item in lvPhong.Items)
                    {
                        if (int.Parse(item.SubItems[1].Text) == i_room)
                        {
                            item.Checked = true;
                        }
                    }
                }

            // load du lieu vao 2 datagridview o duoi
            // join 3 bang voi nhau chon iddichvu, iddodung (chung IDkhach, chung idphieuthue)( 2 bang: 1 bang dodung, 1 bang dich vu)
            // 2 bang lai join voi tung bang tblDoDung, tbDichVu de lay ra ten, gia
            // sau do do? vao datagridview 

            // lay duoc gia tien phai tra
            // lay duoc tien phai tra trong chitietphieuthue, tien phaitra doi voi phong
            int tongtien = 0;
            var bang = (from a in db.tblChiTietPhieux
                        join b in db.tblPhieuThues on a.IDPhieuThue equals b.IDPhieuThue
                        join c in db.tblKhaches on b.IDKhach equals c.IDKhach
                        where c.IDKhach == id
                        select new { a.ThanhTien });
            foreach( var i in bang)
            {
                tongtien += i.ThanhTien;
            }

           foreach(ListViewItem item in lvPhong.Items)
            {
                if(item.Checked==true)
                {
                    int m = int.Parse(item.SubItems[1].Text);
                    var i = from p in db.tblPhongs where p.IDPhong == m  select p;
                    foreach (var k in i)
                    {
                        tongtien += k.GiaPhong;
                    }

                }
               
            }
            txtTongTien.Text = tongtien.ToString();
            LoadBangDichVu();
            LoadBangDoDung();

            //
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnThem2.Enabled = false;
      
            btnXoa2.Enabled = false;
            lvPhong.Enabled = false;
            luu = choice_xoa;
        }

        private void cbDoDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGiaDoDung.Text = cbDoDung.SelectedValue.ToString();
        }

        private void cbDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGiaDichVu.Text = cbDichVu.SelectedValue.ToString();
        }

        private void btnThem2_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;
    
            btnXoa2.Enabled = false;
            btnLuu.Enabled = false;
            luu = choice_them;
        }

       
        private void btnXoa2_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnThem2.Enabled = false;
        
            lvPhong.Enabled = false;
            btnLuu.Enabled = false;
            luu = choice_xoa;
        }

        private void btnLuu2_Click(object sender, EventArgs e)
        {
            KhachHangDAO dao_kh = new KhachHangDAO();
            ChiTietPhieuDAO dao_ctp = new ChiTietPhieuDAO();
            PhieuThueDAO dao_pt = new PhieuThueDAO();
            if (luu == choice_them)
            {
               
                  DataGridViewRow dr = dgvDoDung.SelectedRows[0];
                  int id = int.Parse(dr.Cells["IDPhong"].Value.ToString());
                  // lay duoc idphong, lay soluong va sua vao phong
                  DataGridViewRow dr2 = dgvKhachHang.SelectedRows[0];
                  int id2 = int.Parse(dr2.Cells["IDKhach"].Value.ToString());
                /*********************(that bai)  
               // tim ra duoc chitietphieu ung voi phong co ID, sua so luong
                 var y = (from a in db.tblKhaches
                          join b in db.tblPhieuThues on a.IDKhach equals b.IDKhach
                          join c in db.tblChiTietPhieux on b.IDPhieuThue equals c.IDPhieuThue
                          where c.IDPhong == id && a.IDKhach == id2
                          select new
                          {
                              IDPhieuThue = c.IDPhieuThue,
                              IDPhong = c.IDPhong,
                              IDDichVu = c.IDDichVu,
                              IDDoDung = c.IDDoDung,
                              SoLuong = c.SoLuong,
                              ThanhTien = c.ThanhTien,
                              IDChiTietPhieu = c.IDChiTietPhieu
                          }).AsEnumerable().Select(x => new tblChiTietPhieu
                          {
                              IDPhieuThue = x.IDPhieuThue,
                              IDPhong = x.IDPhong,
                              IDDichVu = x.IDDichVu,
                              IDDoDung = x.IDDoDung,
                              SoLuong = x.SoLuong,
                              ThanhTien = x.ThanhTien,
                              IDChiTietPhieu = x.IDChiTietPhieu
                          });

                 // x.toList() co the gan vao datasource
                 // sua so luong

                 foreach ( tblChiTietPhieu ctp in y)
                 {
                     ctp.SoLuong = int.Parse(txtSoLuongDoDung.Text);
                     //co ten do dung, bh lay id do dung
                     var dodung = from a in db.tblDoDungs where a.TenDoDung == cbDoDung.Text select a;

                     foreach ( var i in dodung)
                     {
                         ctp.IDDoDung = i.IDDoDung;
                     }

                     dao_ctp.Edit(ctp, id);
                 }
                 //db.SaveChanges();
                 //them mot ban ghi chitietphieuthue moi;
                  **************************************/

                // sinh ra chitietphieu, giu nguyen idphieu,         
                var phieu = from u in db.tblPhieuThues
                            join u1 in db.tblKhaches on u.IDKhach equals u1.IDKhach
                            where u1.IDKhach == id2
                            select new
                            {
                                u.IDPhieuThue
                            };
                foreach( var y in phieu)
                {
                    tblChiTietPhieu ctp = new tblChiTietPhieu();
                    if (db.tblChiTietPhieux.Count() == db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu))
                    {
                        var temp = db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu) + 1;
                        ctp.IDChiTietPhieu = temp;
                    }
                    if (db.tblChiTietPhieux.Count() != db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu))
                    {
                        while (true)
                        {
                            if (db.tblChiTietPhieux.Min(x => x.IDChiTietPhieu) > 1)
                            {
                                var temp1 = db.tblChiTietPhieux.Min(x => x.IDChiTietPhieu) - 1;
                                ctp.IDChiTietPhieu = temp1;
                                break;
                            }
                            for (int i = db.tblChiTietPhieux.Min(x => x.IDChiTietPhieu); i <= db.tblChiTietPhieux.Max(x => x.IDChiTietPhieu); i++)
                            {
                                if (db.tblChiTietPhieux.SingleOrDefault(x => x.IDChiTietPhieu == i) != null)
                                {
                                    continue;
                                }
                                if (db.tblChiTietPhieux.SingleOrDefault(x => x.IDChiTietPhieu == i) == null)
                                {
                                    ctp.IDChiTietPhieu = i;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                    ctp.IDPhieuThue = y.IDPhieuThue;
                    ctp.SoLuong = int.Parse(txtSoLuongDoDung.Text);
                    var dodung = from a in db.tblDoDungs where a.TenDoDung == cbDoDung.Text select a;

                    foreach (var i in dodung)
                    {
                        ctp.IDDoDung = i.IDDoDung;
                    }
                    var dichvu = from a in db.tblDichVus where a.TenDichVu == cbDichVu.Text select a;

                    foreach (var i in dichvu)
                    {
                        ctp.IDDichVu = i.IDDichVu;
                    }

                    ctp.IDPhong = id;
                    ctp.ThanhTien = ctp.SoLuong * int.Parse(txtGiaDoDung.Text) + int.Parse(txtGiaDichVu.Text);
                    dao_ctp.Insert(ctp);
                    luu = 0;
                    LoadBangDichVu();
                    LoadBangDoDung();
                }
            }
           if(luu == choice_sua)
            {
            
            }
            if(luu == choice_xoa)
            {
                if (DialogResult.Yes == MessageBox.Show("Bạn chắc không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    DataGridViewRow dr = dgvDoDung.SelectedRows[0];
                    int id = int.Parse(dr.Cells["IDChiTietPhieu"].Value.ToString());
                    var k = db.tblChiTietPhieux.Where(x => x.IDChiTietPhieu == id ).First();
                    // so luong bao nhieu thanh tien tru di bay nhieu
                    db.tblChiTietPhieux.Remove(k);
                    //  k.ThanhTien -= k.SoLuong * int.Parse(dr.Cells["DonGia"].Value.ToString());
                   // k.IDDoDung = 0;
                    //k.SoLuong = 0;
                    db.SaveChanges();
                }
                luu = 0;
                LoadBangDichVu();
                LoadBangDoDung();
                MoKhoaAll();
            }         
        }
    }
}
