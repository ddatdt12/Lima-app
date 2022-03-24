using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Utils
{
    public class Constant
    {
        public readonly static Dictionary<int, string>  PERMISSIONS = new Dictionary<int, string>
        {
            { 0, "Quản lý sách" },
            { 1, "Tiếp nhận sách" },
            { 2, "Quản lý độc giả" },
            { 3, "Quản lý nhân viên" },
            { 4, "Thống kê" },
            { 5, "Cài đặt" },
        };
    }

    public enum Permission
    {
        QuanLySach,
        TraCuuSach,
        QuanLyDocGia,
        QuanLyNhanVien,
        ThongKe,
        CaiDat
    }

    public class Rules
    {
        private Rules(string name) { Name = name; }

        public string Name { get; private set; }

        public static Rules MAX_AGE { get { return new Rules("MAX_AGE"); } }
        public static Rules MIN_AGE { get { return new Rules("MIN_AGE"); } }
        public static Rules VALIDITY_PERIOD_OF_CARD { get { return new Rules("VALIDITY_PERIOD_OF_CARD"); } }
        public static Rules YEAR_PUBLICATION_PERIOD { get { return new Rules("YEAR_PUBLICATION_PERIOD"); } }
        public static Rules ALLOWED_BOOK_MAXIMUM { get { return new Rules("ALLOWED_BOOK_MAXIMUM"); } }
        public static Rules MAXIMUM_NUMBER_OF_DAYS_TO_BORROW { get { return new Rules("MAXIMUM_NUMBER_OF_DAYS_TO_BORROW"); } }
        public static Rules FINE { get { return new Rules("FINE"); } }
    }

}
