using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder builder = builsql();
      
            System.Diagnostics.Debug.WriteLine(builder.ToString());
 

        }
        static StringBuilder builsql()
        {

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("SELECT isnull(T3.F_JN_Image,t2.F_JNSINGEIMAGE) F_JN_PICTURE,t1.FMATERIALID FMATERIALID,SUM(ISNULL(t1.FQTY,0)) FSumQty,COUNT(t.fbillno) FCount\t");
            builder.AppendLine("FROM JN_SAL_SalseOrder t");
            builder.AppendLine("INNER JOIN JN_SAL_SalseOrderDetail t1 ON t.FID=t1.FID");
            builder.AppendLine("LEFT JOIN T_BD_MATERIAL t2 ON t2.FMATERIALID=t1.FMATERIALID");
            builder.AppendLine("LEFT JOIN JN_V_BD_MTRLIMAGELIST t3 ON T3.f_jn_materialid=t1.FMATERIALID AND T3.F_jn_AUXPROPID=t1.FAUXPROPID and T3.F_JN_Image<>''");
            builder.AppendLine("LEFT JOIN T_BD_MATERIALGROUP_L t4 ON t4.FID=t2.FMATERIALGROUP AND t4.FLOCALEID=2052");
            builder.AppendFormat("WHERE t.FDATE >='{0}' AND t.FDATE <='{1}' and t.FFORMID<>'JN_FI_SalseOrderChange' ", 1111, 22222);
            return builder;

        }

    }



}
