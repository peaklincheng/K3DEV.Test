using Kingdee.BOS;
using Kingdee.BOS.App;
using Kingdee.BOS.App.Data;
using Kingdee.BOS.Contracts;
using Kingdee.BOS.Contracts.Report;
using Kingdee.BOS.Core.Enums;
using Kingdee.BOS.Core.Report;
using Kingdee.BOS.Orm.DataEntity;
using Kingdee.BOS.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;


namespace LIN.K3.DEV.App.Report
{

    [Description("商品畅销清单")]
    public class LFG_LIN_Report_GoodMarket : Kingdee.BOS.Contracts.Report.SysReportBaseService
    {
        public override void Initialize()
        {
            base.Initialize();
            this.ReportProperty.ReportType = ReportType.REPORTTYPE_NORMAL;
            this.ReportProperty.IsUIDesignerColumns = true;
            this.ReportProperty.IsGroupSummary = true;
            this.ReportProperty.FormIdFieldName = "FNUMBER";
            this.ReportProperty.ReportName = new LocaleValue("商品畅销清单");

        }

        public override void BuilderReportSqlAndTempTable(IRptParams filter, string tableName)
        {
            base.BuilderReportSqlAndTempTable(filter, tableName);
            string sql = BuilderSql(filter, tableName);
            DBUtils.Execute(this.Context, sql);
        }

        protected string BuilderSql(IRptParams filter, string tableName)
        {
            string seqFld = string.Format(base.KSQL_SEQ, "FNUMBER");
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendLine("SELECT  FPICTURE ,FNUMBER ,FNAME ,FSXNAME ,SUM(FSumQty) FSumQty ,COUNT(FCount) FCount ,FPICTUREName,{0} into {1} ");
            builder.AppendLine("FROM    dbo.V_LIN_Report_GoodMarket");
            builder.AppendLine("{2}"); //where 条件
            builder.AppendLine("GROUP BY FPICTURE ,");
            builder.AppendLine("        FNUMBER ,");
            builder.AppendLine("        FNAME ,");
            builder.AppendLine("        FSXNAME,");
            builder.AppendLine("        FPICTUREName");
            string sql = string.Format(builder.ToString(), seqFld, tableName, BuilderSqlWhere(filter));
            return sql;
        }


        protected string BuilderSqlWhere(IRptParams filter)
        {
            return getFilterStr(filter);


        }


        protected string getFilterStr(IRptParams filter)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("where 1=1 ");
            string F_LFG_sDate, F_LFG_eDate, F_LFG_BM, F_LFG_GYS, F_LFG_FZ;
            DynamicObject dy = filter.FilterParameter.CustomFilter;
            F_LFG_sDate = GetDataByKey(dy, "F_LFG_sDate");
            F_LFG_eDate = GetDataByKey(dy, "F_LFG_eDate");
            F_LFG_FZ = GetDataByKey(dy, "F_LFG_FZ");
            F_LFG_BM = GetBaseDataByKey(dy, "F_LFG_BM", "ID");
            F_LFG_GYS = GetBaseDataByKey(dy, "F_LFG_GYS", "ID");
            if (!F_LFG_sDate.IsNullOrEmptyOrWhiteSpace() && !F_LFG_eDate.IsNullOrEmptyOrWhiteSpace())
            {
                builder.AppendLine(string.Format("and Fdate >='{0}' and Fdate<='{1}'", F_LFG_sDate, F_LFG_eDate));

            }

            if (!F_LFG_FZ.IsNullOrEmptyOrWhiteSpace())
            {
                builder.AppendLine(string.Format("and FMATERIALGROUP like '%{0}%' ", F_LFG_FZ));

            }
            if (!F_LFG_GYS.IsNullOrEmptyOrWhiteSpace() && F_LFG_GYS!="0")
            {
                builder.AppendLine(string.Format("and FDEFAULTVENDORID = {0}", F_LFG_GYS));

            }
            if (!F_LFG_BM.IsNullOrEmptyOrWhiteSpace() && F_LFG_BM !="0")
            {
                builder.AppendLine(string.Format("and FDEPARTMENTID={0}", F_LFG_BM));

            }


            return builder.ToString();

        }





        public override List<SummaryField> GetSummaryColumnInfo(IRptParams filter)
        {
            List<SummaryField> summaryFields = new List<SummaryField>();
            summaryFields.Add(new SummaryField("FSumQty", BOSEnums.Enu_SummaryType.SUM));
            summaryFields.Add(new SummaryField("FCount", BOSEnums.Enu_SummaryType.SUM));
            return summaryFields;

        }







        protected string GetBaseDataByKey(DynamicObject dy, string key, string val)
        {
            string rtValue = "0";
            if (dy != null && dy[key] != null && !string.IsNullOrWhiteSpace(((DynamicObject)dy[key])[val].ToString()))
            {
                rtValue = ((DynamicObject)dy[key])[val].ToString();
            }
            return rtValue;
        }

        private string GetDataByKey(DynamicObject doFilter, string sKey)
        {
            string sReturnValue = string.Empty;
            if (doFilter != null && doFilter[sKey] != null && !string.IsNullOrWhiteSpace(Convert.ToString(doFilter[sKey])))
            {
                sReturnValue = Convert.ToString(doFilter[sKey]);
            }
            return sReturnValue;
        }
    }
}
