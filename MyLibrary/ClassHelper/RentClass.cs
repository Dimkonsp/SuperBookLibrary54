using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.DBModel;

namespace MyLibrary.ClassHelper
{
    class RentClass
    {
        public static double Debt(int bookId ,DateTime startDate,DateTime endDate)
        {
            List<Book> list = new List<Book>();
            list = AppDate.Context.Book.ToList();
            double sum = 0;
            var cost = list.Where(i => i.ID == bookId).FirstOrDefault();
            if ((DateTime.Now.Date - startDate.Date).TotalDays > 30)
            {
                sum = (Convert.ToDouble(cost.Cost) * 0.1) * ((endDate.Date - startDate.Date).TotalDays - 30);
            }

            return (double) sum;
        }
    }
}
