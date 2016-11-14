using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.EF;
namespace DAO
{
   public class PhieuThueDAO
    {
        QuanLyKhachSanDBContext db = null;
        public PhieuThueDAO()
        {
            db = new QuanLyKhachSanDBContext();
        }
          
        // them phieu
        public void Insert( tblPhieuThue phieu)
        {
            try
            {
                db.tblPhieuThues.Add(phieu);
                db.SaveChanges();
            }
            catch
            {

            }
             
        }
        
        //sua phieu
        public void Edit(tblPhieuThue phieu)
        {
            var k = db.tblPhieuThues.Find(phieu.IDPhieuThue);
            k.IDKhach = phieu.IDKhach;
            k.ThoiGianThue = phieu.ThoiGianThue;
            k.ThoiGianTra = phieu.ThoiGianTra;
            k.TongTien = phieu.TongTien;
            db.SaveChanges();
        }

        //xoa phieu
        public void Delete( int ID)
        {
            var k = db.tblPhieuThues.Find(ID);
            db.tblPhieuThues.Remove(k);
            db.SaveChanges();
        }
    }
}
