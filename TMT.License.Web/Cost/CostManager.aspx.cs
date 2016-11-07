using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataLayer;
using Entities;
using Ext.Net;
using TMT.License.Core;

namespace TMT.License.Web.TSSystem
{
    public partial class CostManager : System.Web.UI.Page
    {
        #region >- Page -<

        private static string _Exception;
        protected void Page_Load(object sender, EventArgs e)
        {   LoadPage();
            
                
        }
        private void LoadConfig()
        {

            DataTable dt = new CostConfigData().Search(null, "");
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                switch (dt.Rows[r][(string)CostConfigData.TBC_CostTag].ToString())
                {
                    case "Location":
                        {
                            NumberField txt = AddTextField(dt.Rows[r]);
                            fieldsetLocation.Items.Add(txt);
                            break;
                        }
                    case "Type":
                        {
                            NumberField txt = AddTextField(dt.Rows[r]);
                            fieldsetType.Items.Add(txt);
                            break;
                            
                        }
                    case "Status":
                        {
                            NumberField txt = AddTextField(dt.Rows[r]);
                            fieldsetStatus.Items.Add(txt);
                            break;
                        }
                    case "BasePrice":
                        {
                            NumberField txt = AddTextField(dt.Rows[r]);
                            txt.Step = 100000;
                            fieldsetBasePrice.Items.Add(txt);
                            break;
                        }
                    default:
                        break;
                }
            }
        }
        private NumberField AddTextField(DataRow dtrow)
        {
            Ext.Net.NumberField txt = new Ext.Net.NumberField();
            txt.ID = "Cost" + dtrow[(string)CostConfigData.TBC_CostID].ToString();
            txt.Text = dtrow[(string)CostConfigData.TBC_CostDetail].ToString();
            txt.FieldLabel = dtrow[(string)CostConfigData.TBC_CostName].ToString();
            txt.MarginSpec = "1 0 0 1";
            return txt;
        }
        private void LoadPage()
        {
            bool bRight = WebPermission.ViewPermission(WebPermission.SYSTEM_USER);
            if (!bRight)
            {
                UserCommon.SetSession(UserCommon.SS_Message, Message.MSE_RGNoPermissionView);
                Response.Redirect(UserCommon.Web_ErrorPage, true);
            }
            ResourceManager1.Theme = UserCommon.GetCurrentTheme();
            LoadConfig();
        }

        protected void btRefresh_Click(object sender, DirectEventArgs e)
        {

            Response.Redirect(UserCommon.TT_CostManager);
        }
        #endregion

        #region >- UserInfo -<

        protected void btSave_Click(object sender, DirectEventArgs e)
        {

          
            List<Ext.Net.FieldSet> list = new List<FieldSet>{fieldsetBasePrice,fieldsetLocation,fieldsetStatus,fieldsetType};
            for (int i = 0; i < list.Count; i++)
            {
                InputFieldSet(list[i]);
            }
            
            
                
            
        }
        private void Update(Ext.Net.NumberField nf)
        {
            CostConfigEntities objCost = new CostConfigEntities();
            objCost.CostID = int.Parse(nf.ID.Substring(4,nf.ID.Length-4));
            objCost.CostName = nf.FieldLabel;
            objCost.CostDetail = nf.Text;
            bool bResult = new CostConfigData().Update(objCost);
        }
        private void InputFieldSet(Ext.Net.FieldSet fs)
        {

            foreach (Object obj in fs.Items) {
                Ext.Net.NumberField nf = (Ext.Net.NumberField)obj;
                Update(nf);
            }        
        }
        
        #endregion



    }
}