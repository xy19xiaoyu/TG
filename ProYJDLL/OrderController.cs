using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;

namespace ProYJDLL
{
   public class OrderController
    {
        public static List<C_EARLY_WARNING> GetOrders<TKey>(int currentPage, int pagesize, Expression<Func<C_EARLY_WARNING, bool>> whereQuery, out int pagCount)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            IQueryable<C_EARLY_WARNING> query = db.C_EARLY_WARNING;
             
            if (whereQuery != null)
            {
                query = query.Where(whereQuery);
            }

            pagCount = query.Count();
            return query.Skip((currentPage - 1) * pagesize).Take(pagesize).ToList();
        }
    }
}
