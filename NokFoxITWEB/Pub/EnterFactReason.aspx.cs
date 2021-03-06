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

public partial class Pub_EnterFactReason : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //登陸超時代碼
        if (!PubFunction.CheckLogin(this))
            return;
        if (!IsPostBack)
        {
            gvList.PageSize = System.Convert.ToInt32(Session["PageSize"]);
            btnAdd.Attributes.Add("onclick", GetWinPageStr("EnterFactReasonAdd.aspx", "add", ""));
            //操作權限管控
            PubFunction.BindOperPermission(this, "H01", "");
        }
         //btnAddDept.Attributes.Add("onclick", "window.open('EnterFactReasonAdd.aspx?ReasonCode=" + .Text + "','one','width=800,height=600,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');");
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        gvList.DataBind();
    }
    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
            ibtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";

            string StrID = gvList.DataKeys[e.Row.RowIndex].Value.ToString();
            ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
            ibtnEdit.Attributes.Add("onclick", GetWinPageStr("EnterFactReasonAdd.aspx", "edit", StrID));


            LinkButton lbtnReasonCode = (LinkButton)e.Row.FindControl("lbtnReasonCode");
            lbtnReasonCode.Attributes.Add("onclick", GetWinPageStr("EnterFactReasonAdd.aspx", "view", StrID));
        //    //lbtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";

        //    //ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");
        //    //ibtnEdit.Attributes.Add("onclick", GetWinPageStr("SysModuleAdd.aspx", "edit", StrID));

        //    //Label myLabel = (Label)e.Row.FindControl("lblItem");
        //    //myLabel.Text = System.Convert.ToString((e.Row.RowIndex + 1 + gvList.PageIndex * System.Convert.ToInt32(Session["PageSize"])));
        }
    }

    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    #region   得到链接文件
    private string GetWinPageStr(string winPage, string action, string StrID)
    {
        return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=630,height=340,status=no,resizable=no,scrollbars=yes,top=200,left=200')";
    }
    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
