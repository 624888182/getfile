using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using DBAccess.EAI;
using System.IO;
using System.Text;
using System.Data.OleDb;
public partial class Boundary_WFrmNuberWarning : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetBottomLineVisible(false);
            SetMailVisible(false);
            SetNumberVisible(true);
        }
    }
    //===========================Number Warning start=======================================================================
    private void InitiaData()
    {
       // Execute("UPDATE MES_MEID_IMEI_BLUETOOTH SET  Remain_Count='0' ,Remain_Percent=0");//剩餘量置空
        Execute("UPDATE MES_MEID_IMEI_BLUETOOTH SET  REMAIN_COUNT='0' WHERE status='Y'");
        //不查picasso和msn的情況,因為這2項是34進制的,不好計算
        string AccessSql = "SELECT Plant,Model,Type,Status,Bottom_Line FROM MES_MEID_IMEI_BLUETOOTH WHERE Status='Y' order by model asc";

        OleDbCommand cmd = new OleDbCommand(AccessSql);
        cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tjsfc;user id=sfc;password=sfc");
        cmd.Connection.Open();
        OleDbDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string Return_RemainNum = RemainNum(reader.GetString(1), reader.GetString(2)).ToString();
            int bottom = Convert.ToInt32(reader.GetValue(4).ToString().Trim());
            int current = Convert.ToInt32(Return_RemainNum.Trim());
            double percent;
            if (bottom == 0)
                percent = 100;
            else
                percent = Math.Round(((current - bottom) / (double)bottom) * 100, 0);

            Execute("UPDATE MES_MEID_IMEI_BLUETOOTH SET  REMAIN_COUNT='" + Return_RemainNum + "' ,REMAIN_PERCENT=" + percent + " WHERE MODEL='" + reader.GetString(1) + "' and TYPE='" + reader.GetString(2) + "'");

        }
        cmd.Connection.Close(); 
    }

    private void ShowData()
    {
        string sql = "SELECT Model 幾種類型,Type 號碼類型,Bottom_Line 底線,Remain_Count 剩餘數量,Remain_Percent 剩餘百分比 FROM MES_MEID_IMEI_BLUETOOTH WHERE status='Y' order by Remain_Percent asc";

        DataSet ds = ClsGlobal.objDataConnect.DataQuery(sql.ToString());

        if (ds != null && ds.Tables.Count > 0)
        {
            dgData.DataSource = ds.Tables[0];
            dgData.DataBind();

            dgData.Dispose();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('抱歉,沒有預警信息！！');</script>");
            return;
        }
    }
    int RemainNum(string gModel, string gType)
    {
        DataSet DBtmprs = new DataSet();
        string DBsql = "";
        string Last_No = "";
        int Remain_Num = 0;
        OleDbCommand cmd = null;

         
        if (gModel == "FD1" && gType == "MEID")
        {
            DBsql = "select  MEID_ID from SFC.MEID_INFORMATION where status = '0' and Model_Name ='FD1'";
            DBtmprs = ClsGlobal.objDataConnect.DataQuery(DBsql);

            if (DBtmprs != null && DBtmprs.Tables[0].Rows.Count > 0)
            {
                Remain_Num = DBtmprs.Tables[0].Rows.Count;
            }
            else
            {
                Remain_Num = 0;
            }
            return Remain_Num;
        }
        
            DBsql = "SELECT  ORGANIZATION_ID, NUMBER_CATEGORY, PROJECT_CODE,PREFIX_CODE, FIRST_NUMBER,LAST_NUMBER, NOW_NUMBER From SFC.CMCS_SFC_NUMBER_RANGES  Where NUMBER_CATEGORY='" + gType.Trim() + "' AND PROJECT_CODE='" + gModel.Trim() + "'";
            cmd = new OleDbCommand(DBsql);
            cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tjsfc;user id=sfc;password=sfc");
            cmd.Connection.Open();
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Last_No = reader.GetString(5);//獲取最後一位數字段
                DBsql = "SELECT MAX(num) FROM ((select to_char(max(now_number)) num from sfc.cmcs_sfc_number_ranges where  PREFIX_CODE='" + reader.GetString(3) + "' and FIRST_NUMBER='"
                    + reader.GetString(4) + "' and LAST_NUMBER='" + reader.GetString(5) + "') Union (SELECT SUBSTR(Max (IMEINUM)," + (reader.GetString(3).Trim().Length + 1) + ","
                    + reader.GetString(5).Trim().Length + ") num FROM SHP.CMCS_SFC_IMEINUM Where IMEINUM Between  '" + reader.GetString(3) + reader.GetString(4) + "0"
                    + "'  AND '" + reader.GetString(3) + reader.GetString(5) + "9'))";
                OleDbCommand cmd1 = new OleDbCommand(DBsql);
                cmd1.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tjsfc;user id=sfc;password=sfc");
                cmd1.Connection.Open();
                int max = 0;
                if (gType.Trim().ToString() == "BLUETOOTH" || gType.Trim().ToString() == "WIFI")//十六進制
                {
                    max = Convert.ToInt32(cmd1.ExecuteScalar().ToString(), 16);
                    Remain_Num = Convert.ToInt32(Last_No, 16) - max;
                }
                //else if (gType == "PICASSO" || gType == "MSN")//34進制,除了N和O了
                //{
                //    //max = Convert.ToInt32(cmd1.ExecuteScalar().ToString(),34);
                //    //Remain_Num = Convert.ToInt32(Last_No, 34) - max;
                //    Remain_Num = 0;
                //}
                else//十進制
                {
                    max = Convert.ToInt32(cmd1.ExecuteScalar());
                    Remain_Num = Convert.ToInt32(Last_No) - max;
                }
                cmd1.Connection.Close();
            }
            cmd.Connection.Close();
        
         
        return Remain_Num;
    }
    void Execute(string sql)
    {
        ClsGlobal.objDataConnect.DataExecute(sql.ToString());
        //OleDbCommand cmd = new OleDbCommand(sql);
        //cmd.Connection = new OleDbConnection("Provider=MSDAORA.Oracle;Data Source=tjsfc;user id=sfc;password=sfc");
        //cmd.Connection.Open();
        //cmd.ExecuteNonQuery();
        //cmd.Connection.Close();
    }
    protected void FailData_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='#bec5e7'");
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='#f5f5f5'");
        }

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        SetBottomLineVisible(false);
        SetMailVisible(false);
        SetNumberVisible(true);
        InitiaData();
        ShowData();
    }

    //===========================Number Warning end=======================================================================

    protected void SetMailVisible(bool b)
    {         
            lblMail.Visible = b;
            txtMail.Visible = b;
            lblMailEx.Visible = b;
            lstMails.Visible = b;
            btnAdd.Visible = b;
            btnRemove.Visible = b;
            //btnSave.Visible = b;         
    }

    protected void SetNumberVisible(bool b)
    {
        dgData.Visible = b;        
    }

    protected void SetBottomLineVisible(bool b)
    {
        dgBottomLine.Visible = b;   
    }
    
    //===========================Bottom Line start=======================================================================

    protected void btnSetWarningLine_Click(object sender, EventArgs e)
    {
        SetBottomLineVisible(true);
        SetMailVisible(false);
        SetNumberVisible(false);

        BindData();
    }
    protected void BindData()
    {
        string strsql = "SELECT model,type,bottom_line FROM sfc.MES_MEID_IMEI_BLUETOOTH where status='Y'";
        DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dt1.Rows.Count > 0)
        {
            dgBottomLine.DataSource = this.GetIDTable(dt1).DefaultView;
            dgBottomLine.DataBind();
        }
    }
    private DataTable GetIDTable(DataTable dt)
    {
        DataColumn col = new DataColumn("新增", Type.GetType("System.Int32"));
        dt.Columns.Add(col);
        for (int i = 0; i <= dt.Rows.Count - 1; i++)
        {
            if (0 == i)
                dt.Rows[0][col] = 1;
            else
                dt.Rows[i][col] = Convert.ToInt32(dt.Rows[i - 1][col]) + 1;
        }
        return dt;
    }

    protected void dgBottomLine_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex >= 0)
            e.Item.Cells[0].Text = ((e.Item.ItemIndex + 1) + (dgBottomLine.PageSize) * (dgBottomLine.CurrentPageIndex)).ToString();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ((LinkButton)(e.Item.Cells[5].Controls[1])).Attributes.Add("onclick", "return confirm('你确认删除吗?操作不可返回,按確認繼續..');");
        }
    }

    protected void dgBottomLine_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgBottomLine.EditItemIndex = -1;   //更新表格的EditItemIndex属性   
        BindData();
    }

    protected void dgBottomLine_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        string strmodelname = ((Label)e.Item.Cells[1].Controls[1]).Text.Trim();
        string strtype = ((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim();

        //OracleConnection orcn = null;
        //OracleDataAdapter da;
       // DataSet myds;
        //myds = new DataSet();
        try
        {
            //orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tjsfc");
             
            //orcn.Open();
             
            //da = new OracleDataAdapter("select MODEL,TYPE,BOTTOM_LINE from SFC.MES_MEID_IMEI_BLUETOOTH where model='" + strmodelname + "' and type='" + strtype + "'", orcn);
            //da.Fill(myds, "MES_MEID_IMEI_BLUETOOTH");

            //----------- update part-----------
            //DataTable myDt = myds.Tables["CDMA_MOTO_ORDERNO"];
            //myDt.PrimaryKey = new DataColumn[] { myDt.Columns["ORDER_NUMBER"] };
            //出错就是因为少了上面这一句。这条语句指定了DataTable的主键。或者用下一条语句也可以，下一条语句是让适配器自动加上表的架构（Key约束）
            //da.MissingSchemaAction = MissingSchemaAction.AddWithKey; 

            //myds.Tables["MES_MEID_IMEI_BLUETOOTH"].Rows[0]["MODEL"] = ((Label)e.Item.Cells[1].Controls[1]).Text;
            //myds.Tables["MES_MEID_IMEI_BLUETOOTH"].Rows[0]["TYPE"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
            //myds.Tables["MES_MEID_IMEI_BLUETOOTH"].Rows[0]["BOTTOM_LINE"] = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
             
            //OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
            //da.Update(myds, "MES_MEID_IMEI_BLUETOOTH");
            //orcn.Close();

            string strsql = "update sfc.MES_MEID_IMEI_BLUETOOTH set BOTTOM_LINE= '"+((TextBox)e.Item.Cells[3].Controls[1]).Text
                            +"' where model='" + strmodelname + "' and type='" + strtype + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
           
        }

        catch (Exception ex)
        {
            //orcn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('更新失敗,請檢查...');</script>");
            return;
        }
        dgBottomLine.EditItemIndex = -1;
        BindData();
    }
    protected void dgBottomLine_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgBottomLine.EditItemIndex = e.Item.ItemIndex;
        BindData();
    }
    protected void dgBottomLine_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dgBottomLine.CurrentPageIndex = e.NewPageIndex;
        BindData();
    }
    protected void dgBottomLine_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "AddItem")//点“新增”按钮
        {
            dgBottomLine.ShowFooter = true;

            //string strUser = txtUser.Text.Trim();
            string strsql = "SELECT model,type,bottom_line FROM sfc.MES_MEID_IMEI_BLUETOOTH where status='Y'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                dgBottomLine.DataSource = this.GetIDTable(dt1).DefaultView;
                dgBottomLine.DataBind();
            }
        }

        if (e.CommandName == "ItemCancel")//点新增状态下“取消”按钮
        {
            dgBottomLine.ShowFooter = false;
            //string strUser = txtUser.Text.Trim();
            BindData();
        }
        if (e.CommandName == "ItemSure")//点新增状态下“确定”按钮
        {
            dgBottomLine.ShowFooter = false;

            OracleConnection orcn = null;
            OracleDataAdapter da;
            DataSet myds;
            myds = new DataSet();

            try
            {
                orcn = new OracleConnection(@"User id =sfc;Password = sfc;Data Source=tjsfc");
                orcn.Open();

                da = new OracleDataAdapter("select model,type,status,bottom_line from sfc.MES_MEID_IMEI_BLUETOOTH where 0=1 ", orcn);
                da.Fill(myds, "MES_MEID_IMEI_BLUETOOTH");

                //----------- Insert new row----------
                DataRow mydr = myds.Tables["MES_MEID_IMEI_BLUETOOTH"].NewRow();

                mydr["status"] = "Y";

                if (((TextBox)e.Item.Cells[1].Controls[1]).Text.Trim().Length > 0)
                    mydr["model"] = ((TextBox)e.Item.Cells[1].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Model欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[2].Controls[1]).Text.Trim().Length > 0)
                    mydr["type"] = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('Type欄位不能為空...');</script>");
                    return;
                }
                if (((TextBox)e.Item.Cells[3].Controls[1]).Text.Trim().Length > 0)
                    mydr["bottom_line"] = ((TextBox)e.Item.Cells[3].Controls[1]).Text;
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('BottomLine欄位不能為空...');</script>");
                    return;
                }
                 

        //        mydr["EFFECTIVE_FROM"] = DateTime.Now.ToString();
        //        mydr["EFFECTIVE_TO"] = DateTime.Now.AddYears(1).ToString();

                myds.Tables["MES_MEID_IMEI_BLUETOOTH"].Rows.Add(mydr);

        //        // ---------------end -------------------

                OracleCommandBuilder myCommandBuilder = new OracleCommandBuilder(da);
                da.Update(myds, "MES_MEID_IMEI_BLUETOOTH");
                orcn.Close();
            }
            catch (Exception ex)
            {
                orcn.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('添加失敗,請檢查...');</script>");
                return;
            }
            dgBottomLine.EditItemIndex = -1;
        //    string strUser = txtUser.Text.Trim();
            BindData();

        }
        if (e.CommandName == "ItemDelete")
        {
            string strsql = "";
            string strmodelname = ((Label)e.Item.Cells[1].Controls[1]).Text;
            string strtype = ((Label)e.Item.Cells[2].Controls[1]).Text;
            //if (strtype.Length > 0)
                strsql = "delete from sfc.MES_MEID_IMEI_BLUETOOTH where model='" + strmodelname + "' and type='" + strtype + "'";
            //else
            //    strsql = "delete from SFC.NEW_USERS where loginname='" + strloginname + "'";
            ClsGlobal.objDataConnect.DataExecute(strsql);
            dgBottomLine.EditItemIndex = -1;
            //txtUser.Text = "";
            string strsql1 = "SELECT model,type,bottom_line FROM sfc.MES_MEID_IMEI_BLUETOOTH where status='Y'";
            DataTable dt1 = ClsGlobal.objDataConnect.DataQuery(strsql1).Tables[0];
            if (dt1.Rows.Count > 0)
            {
                dgBottomLine.DataSource = this.GetIDTable(dt1).DefaultView;
                dgBottomLine.DataBind();
            }
            else
            {
                dgBottomLine.Visible = false;
            }
        }
    }

    //===========================Bottom Line end=======================================================================
    //===========================Set Mail start=======================================================================
    protected void btnSetMail_Click(object sender, EventArgs e)
    {
        SetBottomLineVisible(false);
        SetMailVisible(true);
        SetNumberVisible(false);

        lstMails.Items.Clear();
        string strsql = "SELECT MAILTO FROM sfc.MES_SEND_MAILS WHERE SENDTYPE='NUMBERWARNING' AND ACTI=1";
        DataTable dtmails = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dtmails.Rows.Count > 0)
        {
            for (int i = 0; i < dtmails.Rows.Count; i++)
            {
                lstMails.Items.Add(new ListItem(dtmails.Rows[i][0].ToString(), lstMails.Items.Count.ToString()));
            }
            //dgBottomLine.DataSource = this.GetIDTable(dt1).DefaultView;
            //dgBottomLine.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string strmail = string.Empty;
        if (txtMail.Text.ToString() == "" || null == txtMail.Text.ToString())
        {
            return;
        }
        strmail = txtMail.Text;
        if (-1 == strmail.IndexOf('@'))
        {
            strmail += "/FIH/FOXCONN@FOXCONN";
        }

        string strsql = "SELECT MAILTO FROM sfc.MES_SEND_MAILS WHERE SENDTYPE='NUMBERWARNING' AND MAILTO='"+strmail+"'";
        DataTable dtmails = ClsGlobal.objDataConnect.DataQuery(strsql).Tables[0];
        if (dtmails.Rows.Count > 0)
        {
            strsql = "update sfc.MES_SEND_MAILS set acti=1 WHERE SENDTYPE='NUMBERWARNING' AND MAILTO='" + strmail + "'";
        }
        else
        {
            strsql = "INSERT INTO sfc.MES_SEND_MAILS(SENDTYPE,MAILTO,MAILFROM) VALUES('NUMBERWARNING','" + strmail + "','TJ-SFC-MANAGE/FIH/FOXCONN@FOXCONN')";
        }
        
        ClsGlobal.objDataConnect.DataExecute(strsql);
        lstMails.Items.Add(new ListItem(strmail, strmail));
        txtMail.Focus();
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < lstMails.Items.Count; i++)
        {
            ListItem Li = lstMails.Items[i];
            if (Li.Selected == true)
            {
                string strremove = Li.Text;
                string strsql = "update sfc.MES_SEND_MAILS set acti=0 WHERE SENDTYPE='NUMBERWARNING' AND mailto='"+strremove+"'";
                ClsGlobal.objDataConnect.DataExecute(strsql);
                lstMails.Items.Remove(Li);
                i -= 1;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    //===========================Set Mail end=======================================================================

}
