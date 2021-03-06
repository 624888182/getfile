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

public partial class Pub_Notice : System.Web.UI.Page
{
    DbAccessing DAL = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {

        //判斷有沒有登陸系統
        if (!PubFunction.CheckLogin(this))
        {
            return;
        }

        //判斷按鈕權限
        PubFunction.BindOperPermission(this, "D05", "");

        if (!this.IsPostBack)
        {
            ShowGrid();
            txtTitle.Focus();
        }    
    }

    /// <summary>
    /// 綁定GridView的數據
    /// </summary>
    private void ShowGrid()
    {
        string sql = "select A.NoticeCode,A.Title,A.Memo,A.CreateDate,B.UserName "
                        + "  from pubNotice A"
                        + "  left join usyUsers B on A.CreateUser = B.UserID";




        if (txtTitle.Text.Trim() != "")
        {
            sql += " and A.Title like N'%" + txtTitle.Text + "%'";
        }

        sql += " order by A.CreateDate desc ";
        DbAccessing dbDAL = new DbAccessing();
        try
        {
            DataTable dt = dbDAL.ExecuteSqlTable(sql);
            gvList.DataSource = dt;
            gvList.DataBind();
        }
        catch(Exception ex)
        {
            string tmp = ex.ToString();
        }
    }

    protected void btnFind_Click(object sender, EventArgs e)
    {
        ShowGrid();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //Response.Redirect("pubNoticeAdd.aspx?op=Add");
        Response.Write("<script>window.open('PubNoticeAdd.aspx?op=add','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');</script>");
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string NoticeCode = gvList.DataKeys[e.Row.RowIndex].Value.ToString();
            ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
            ibtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";

            ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            ibtnEdit.OnClientClick = "window.open('PubNoticeAdd.aspx?NoticeCode=" + NoticeCode + "&op=edit','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');";

            LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");
            lbtnTitle.OnClientClick = "window.open('PubNoticeAdd.aspx?NoticeCode=" + NoticeCode + "&op=view','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');";
        }
    }
    #region   
    //private string GetWinPageStr(string winPage, string action, string StrID)
    //{
    //    //return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=480,height=420,status=no,resizable=no,scrollbars=yes,top=220,left=350')";
    //    return "window.location.href='" + winPage + "?action=" + action + "&kerid=" + StrID + "'";
    //}
    #endregion

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
            ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            LinkButton lbtnTitle = (LinkButton)e.Row.FindControl("lbtnTitle");

            ibtnDelete.CommandArgument = gvList.DataKeys[e.Row.RowIndex].ToString();
            ibtnEdit.CommandArgument = gvList.DataKeys[e.Row.RowIndex].ToString();
            lbtnTitle.CommandArgument = gvList.DataKeys[e.Row.RowIndex].ToString();
        }
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ShowGrid();
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        PubNotice myEntry = new PubNotice();
        PubNoticeInfo myEntryInfo = new PubNoticeInfo();
        myEntryInfo.NoticeCode = gvList.DataKeys[e.RowIndex].Value.ToString();
        myEntry.Delete(myEntryInfo);
        ShowGrid();
    }
}
