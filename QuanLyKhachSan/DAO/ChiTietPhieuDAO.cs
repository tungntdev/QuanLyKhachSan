using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.EF;


namespace DAO
{
   public class ChiTietPhieuDAO
    {
        QuanLyKhachSanDBContext db = null;
        public ChiTietPhieuDAO()
        {
            db = new QuanLyKhachSanDBContext();
        }
        //them chi tiet phieu
        public void Insert(tblChiTietPhieu phieu)
        {
            try
            {
                db.tblChiTietPhieux.Add(phieu);
                db.SaveChanges();
            }
            catch
            {

            }
        }
        
        //sua chi tiet phieu
        public void Edit(tblChiTietPhieu phieu, int ID)
        {
            // tim den ban ghi dautien khi iddichvu =0 || idDoDung =0, neu khong ton tai thi?
            var k = db.tblChiTietPhieux.Where(n => n.IDPhong == ID && n.IDChiTietPhieu== phieu.IDChiTietPhieu).First();
            k.IDPhieuThue = phieu.IDPhieuThue;
            k.IDPhong = phieu.IDPhong;
            k.IDDichVu = phieu.IDDichVu;
            k.IDDoDung = phieu.IDDoDung;
            k.SoLuong = phieu.SoLuong;
            k.ThanhTien = phieu.ThanhTien;
            db.SaveChanges();
        }   

        //xoa chi tiet phieu
        public void Delete(int ID)
        {
            /*
            while ( true)
            {
                if(db.tblChiTietPhieux.SingleOrDefault(x => x.IDPhieuThue == ID) == null)
                {
                    break;
                }
                var k = db.tblChiTietPhieux.Find(ID);
                db.tblChiTietPhieux.Find(ID);
                db.tblChiTietPhieux.Remove(k);
                db.SaveChanges();
            }*/
            // chon mot bang co idphieu = ID
            IEnumerable<tblChiTietPhieu> x = from p in db.tblChiTietPhieux
                    where p.IDPhieuThue == ID
                    select p;
            foreach(tblChiTietPhieu a in x)
            {
                db.tblChiTietPhieux.Remove(a);           
            }
            db.SaveChanges();
        }

    }
}
