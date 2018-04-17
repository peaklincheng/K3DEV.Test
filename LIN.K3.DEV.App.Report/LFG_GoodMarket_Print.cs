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
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.DynamicForm;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.List.PlugIn;
using System.IO;
using Kingdee.BOS.Core.Report.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.ControlModel;

namespace LIN.K3.DEV.BILLPLUGIN
{
    [Description("商品畅销清单套打")]
    public class LFG_GoodMarket_Print : AbstractSysReportPlugIn
    {


        public override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.View.GetControl<EntryGrid>("FList").SetRowHeight(0x48);
        }

        public override void OnPrepareNotePrintData(PreparePrintDataEventArgs e)
        {

            base.OnPrepareNotePrintData(e);
            DynamicObject[] dataObject = e.DataObjects;
            for (int i = 0; i < dataObject.Length; i++)
            {
                DynamicObject entryObject = dataObject[i];
                // entryObject["FSumQty"] = "测试";

                string url = @"http://192.168.8.64/k3cloud/";
                string defaultURl = @"";
                string filepath = entryObject["FPICTURE"].ToString();
                if (!filepath.IsNullOrEmptyOrWhiteSpace())
                    {
                    string allurl = url + filepath;
                    entryObject["FPICTURE"] = allurl;
                }
           









            }

        }



        private Byte[] ReadFile(string fileName)
        {

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }
            else
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader br = new BinaryReader(fs);
                    return br.ReadBytes(Convert.ToInt32(fs.Length));
                }
            }
        }

    }
}
