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

public partial class App_EnterFactApplyMaster : System.Web.UI.Page
{
    DbAccessing DAL = new DbAccessing();
    protected void Page_Load(object sender, EventArgs e)
    {   //判斷有沒有登陸系統
        if (!PubFunction.CheckLogin(this))
        {
            return;
        }
        //判斷按鈕權限
        PubFunction.BindOperPermission(this, "B01", "");
        if (!this.IsPostBack)
        {
            ShowGrid();
            txtApplyCode.Focus();
        }    
    }

    /// <summary>
    /// 綁定GridView的數據
    /// </summary>
    private void ShowGrid()
    {
        string sql = "select A.ApplyCode,B.DepartmentName,isnull(A.ApplyID,'') +' '+ isnull(C.UserName,'') as UserName,A.ApplyDate,A.Tel,case when A.IsBUMgrConfirm='true' then 'Y' else 'N' end as IsBUMgrConfirm"
                    + " ,A.Memo,A.Status,A.RejectReason"
                    + " ,isnull(A.DivisionMgrId,'')+' '+isnull(D.UserName,'') +'('+Convert(varchar(10),A.DivisionConfirmDate,111)+')' as UserName1"
                    + " ,isnull(A.BUMgrId,'')+' '+isnull(E.UserName,'')++'('+Convert(varchar(10),A.BUConfirmDate,111)+')' as UserName2"
                    + " from appEnterFactApplyMaster A "
                    + " left join usyDepartment B on A.ApplyDepartment = B.DepartmentID"
                    + " left join usyUsers C on A.ApplyID = C.UserID"
                    + " left join usyUsers D on A.DivisionMgrId = D.UserID"
                    + " left join usyUsers E on A.BUMgrId = E.UserID";

        if (Session["RoleCode"].ToString() == "SafeManage" || Session["RoleCode"].ToString() == "admins")    //安管
        {
            sql += " where 1=1 ";
        }
        else if (Session["RoleCode"].ToString() != "SafeManage")    //助理
        {
            
            //sql += " left join usyUserOperateDept F on A.ApplyDepartment = F.DeptID"
            //+ " where F.DeptID is not null and F.UserID = '" + Session["loginUser"] + "'";
            sql += " where ApplyID = '" + Session["loginUser"].ToString() + "'";
        }


        if (txtApplyCode.Text.Trim() != "")
        {
            sql += " and A.ApplyCode like N'%" + txtApplyCode.Text + "%'";
        }

        if (ddlStatus.SelectedValue == "0") //全部
        {
            
        }
        else if (ddlStatus.SelectedValue == "1")    //審核完成
        {
            sql += " and (A.IsBUMgrConfirm = '0' and A.Status='1' or A.IsBUMgrConfirm = '1' and A.Status='2')";
        }
        else if (ddlStatus.SelectedValue == "2")    //審核未完成
        {
            sql += " and (A.IsBUMgrConfirm = '0' and A.Status<>'1' or A.IsBUMgrConfirm = '1' and A.Status<>'2')";
        }
        else if (ddlStatus.SelectedValue == "3")    //審核完成卡發未完成
        {
            sql += " and (A.IsBUMgrConfirm = '0' and A.Status='1' or A.IsBUMgrConfirm = '1' and A.Status='2')"
                + "  and (A.ReserveField3='' or A.ReserveField3 is null or left(A.ReserveField3,CHARINDEX('/',A.ReserveField3,1)-1) <> right(A.ReserveField3,len(A.ReserveField3) - CHARINDEX('/',A.ReserveField3,1)))";
        }
        else if (ddlStatus.SelectedValue == "4")    //審核完成卡發完成
        {
            sql += " and (A.IsBUMgrConfirm = '0' and A.Status='1' or A.IsBUMgrConfirm = '1' and A.Status='2')"
                +  " and A.ReserveField3!='' and A.ReserveField3 is not null and left(A.ReserveField3,CHARINDEX('/',A.ReserveField3,1)-1) = right(A.ReserveField3,len(A.ReserveField3) - CHARINDEX('/',A.ReserveField3,1))";
        }

        sql += " order by A.InitiateDate desc ";
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
    protected void btnColligateSearch_Click(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("EnterFactApplyMasterAdd.aspx?action=add");
    }
    protected void btnOut_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("../MainDesktop.aspx");
    }

    //#region   得到網葉挑塼的字符串
    //private string GetWinPageStr(string winPage, string action, string StrID)
    //{
    //    return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=630,height=340,status=no,resizable=no,scrollbars=yes,top=200,left=200')";
    //}
    //#endregion

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ID = gvList.DataKeys[e.Row.RowIndex].Value.ToString();
            string status = GetStatusByApplyCode(ID);
            //string IsEnableApplyCode = e.Row.Cells[8].Text.ToString();
            //string IsBu = e.Row.Cells[7].Text.ToString();

            ImageButton ibtnDelete = (ImageButton)e.Row.FindControl("ibtnDelete");
            ImageButton ibtnEdit = (ImageButton)e.Row.FindControl("ibtnEdit");

            ibtnDelete.OnClientClick = "return confirm('" + GetGlobalResourceObject("Message", "Delete_Sure").ToString() + "')";
            switch (status)
            {
                case "0":
                    ibtnDelete.Visible = true;
                    ibtnEdit.Visible = true;
                    break;
                case "1":
                case "2":
                case "8":
                case "9":
                    ibtnDelete.Visible = false;
                    ibtnEdit.Visible = false;
                    break;
            }
        }
    }
    protected void gvList_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        EnterFactApplyMaster myEntry = new EnterFactApplyMaster();
        EnterFactApplyMasterInfo myEntryInfo = new EnterFactApplyMasterInfo();
        myEntryInfo.ApplyCode = gvList.DataKeys[e.RowIndex].Value.ToString();
        myEntry.Delete(myEntryInfo);
        ShowGrid();
    }
    #region   
    //private string GetWinPageStr(string winPage, string action, string StrID)
    //{
    //    //return "window.open('" + winPage + "?action=" + action + "&kerid=" + StrID + "','one','width=480,height=420,status=no,resizable=no,scrollbars=yes,top=220,left=350')";
    //    return "window.location.href='" + winPage + "?action=" + action + "&kerid=" + StrID + "'";
    //}
    #endregion

    protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string ID = gvList.DataKeys[e.NewEditIndex].Value.ToString();
        Response.Redirect("EnterFactApplyMasterAdd.aspx?action=edit&kerid='" + ID + "'");
    }


    private string GetStatusByApplyCode(string applyCode)
    {
        string rtn = "";
        try
        {
            DataTable dt = DAL.ExecuteSqlTable("select Status from appEnterFactApplyMaster where ApplyCode = '" + applyCode + "'");
            if (dt.Rows.Count > 0)
            {
                rtn = dt.Rows[0]["Status"].ToString();
            }
        }
        catch { }
        return rtn;
    }

    protected void gvList_RowCreated(object sender, GridViewRowEventArgs e)
    {
        PubFunction.gvList_RowCreatedNoMouseHand(sender, e);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnApplyCode = (LinkButton)e.Row.FindControl("lbtnApplyCode");
            lbtnApplyCode.CommandArgument = e.Row.RowIndex.ToString();

            ImageButton ibtnWorkFlow = (ImageButton)e.Row.FindControl("ibtnWorkFlow");
            ibtnWorkFlow.CommandArgument = e.Row.RowIndex.ToString();
        }

    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        ShowGrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        ShowGrid();
    }
    protected void gvList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ID = null;
        switch (e.CommandName)
        {

            case "Detail":
                ID = gvList.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                Response.Redirect("EnterFactApplyMasterAdd.aspx?action=view&kerid='" + ID + "'");
                break;
            case "WorkFlow":
                ID = gvList.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                Response.Write("<script>window.open('EnterFactApplyMasterWorkFlow.aspx?ApplyCode=" + ID + "','one','width=320,height=360,status=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,top=220,left=350');</script>");                
                break;
        }

    }
    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
