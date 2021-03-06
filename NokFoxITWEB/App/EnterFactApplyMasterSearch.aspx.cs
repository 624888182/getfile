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
using FIH.ForeignStaff.db;
using System.IO;

public partial class App_EnterFactApplyMasterSearch : System.Web.UI.Page
{
    DbAccessing ds = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {

        string SqlQuery = null;
        //判斷有沒有登陸系統
        if (!PubFunction.CheckLogin(this))
        {
            return;
        }
        string isPurview = "";

        if (!this.IsPostBack)
        {
            gvList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            btnColligateSearch.Attributes.Add("onclick", PubFunction.GetPubQueryWin(Request.ApplicationPath, "v_EnterFactApply", "form1"));            
        }

        isPurview = PubFunction.BindOperPermission(this, "B03", "gvAll");
        if (SecFunction.IsPurview(isPurview, "browse"))
        {
            if ((Session["SQLQuery"] != null) && (Session["SQLQuery"].ToString() != ""))
            {
                SqlQuery = Session["SQLQuery"].ToString();
                Session["ApplyHead"] = SqlQuery;
                Session["SQLQuery"] = null;
                PubFunction.ShowPubQueryWin(SqlQuery, this.gvList, "v_EnterFactApply");
            }
            else
            {
                if ((Session["ApplyHead"] != null) && (Session["ApplyHead"].ToString() != ""))
                {
                    PubFunction.ShowPubQueryWin(Session["ApplyHead"].ToString(), gvList, "v_EnterFactApply");
                }
                else
                {
                    ShowGrid();
                }
            }
        }
    }

    /// <summary>
    /// 綁定GridView的數據
    /// </summary>
    private void ShowGrid()
    {
        string SqlStr = "";

        if (txtApplyCode.Text != "")
        {
            SqlStr += " and ApplyCode like '%" + txtApplyCode.Text.Trim() + "%'";
        }
        try
        {
            if (SqlStr.Trim() != "")
            {
                SqlStr = " select * from v_EnterFactApply where 1=1 " + SqlStr + " order by ApplyCode DESC ";
            }
            else
            {
                SqlStr = " select * from v_EnterFactApply where 2=1 ";
                return;
            }
            DataTable dtBom = new DataTable();
            dtBom = ds.ExecuteSqlTable(SqlStr);
            gvList.DataSource = dtBom.DefaultView;
            gvList.DataBind();
            gvList.Visible = true;
        }
        catch (System.Data.SqlClient.SqlException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        Session["ApplyHead"] = null;
        Session["SQLQuery"] = null;
        ShowGrid();
    }
    protected void btnColligateSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    protected void btnToExcel_Click(object sender, EventArgs e)
    {
        ToExcel(gvList);
    }

    public void ToExcel(System.Web.UI.Control ctl)
    {
        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=Excel.xls");
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
        HttpContext.Current.Response.ContentType = "application/ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword
        ctl.Page.EnableViewState = false;
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        ctl.RenderControl(hw);
        HttpContext.Current.Response.Write(tw.ToString());
        HttpContext.Current.Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //一定要重载此方法。否则会报错
    }

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
    }
}
