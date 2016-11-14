using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using DAO.EF;
namespace DAO
{
    
    public class KhachHangDAO
    {
        QuanLyKhachSanDBContext db = null;
        public KhachHangDAO()
        {
            db = new QuanLyKhachSanDBContext();
        }
        // thêm khách hàng
       
        public int Insert(tblKhach khach)
        {
            
                db.tblKhaches.Add(khach);
                db.SaveChanges();
            return khach.IDKhach;
           
        }    

        // sửa khách hàng
        public void Edit(tblKhach khach)
        {
            var k = db.tblKhaches.Find(khach.IDKhach);
            k.DiaChi = khach.DiaChi;
            k.SDT = khach.SDT;
            k.SoNguoi = khach.SoNguoi;
            k.TenKhach = khach.TenKhach;
            db.SaveChanges();
        }

        //xoa khach hang
        public void Delete(int ID)
        {
            var k = db.tblKhaches.Find(ID);
            db.tblKhaches.Remove(k);
            db.SaveChanges();
        }

        public void Insert(global::DAO.KhachHangDAO k)
        {
            throw new NotImplementedException();
        }
    }
}
