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

public partial class Pub_GateHouseAdd : System.Web.UI.Page
{
    protected string NoticeCode; //公告Code
    public string LinkFile = null;      //連接附檔的路徑
    protected string Permission = null; //權限字符串

    PubNoticeInfo myEntryInfo = new PubNoticeInfo();
    PubNotice myEntry = new PubNotice();

    protected void Page_Load(object sender, EventArgs e)
    {
        //判斷有沒有登陸系統
        if (!PubFunction.CheckLogin(this))
        {
            return;
        }
         

        //判斷按鈕權限並且返回權限字符串
        Permission = PubFunction.BindOperPermission(this, "L01", "");
        if (Request.QueryString["NoticeCode"] != null)
        {
            NoticeCode = Request.QueryString["NoticeCode"];
        }
        if (ViewState["op"] == null)
        {
            ViewState["op"] = Request.QueryString["op"];
        }

        if (!IsPostBack)
        {
            if (ViewState["op"].ToString() == "edit")
            {
                textUnable(true, "edit");
                this.btnCommit.Visible = true;
                this.btnEdit.Visible = false;

                myEntryInfo = myEntry.getPubNotice(NoticeCode);
                txtNoticeCode.Text = myEntryInfo.NoticeCode;
                txtTitle.Text = myEntryInfo.Title;
                txtMemo.Text = myEntryInfo.Memo;
                DatabindFile(NoticeCode,"edit");
            }
            else if (ViewState["op"].ToString() == "view")
            {
                textUnable(false, "view");
                if (Permission.IndexOf("modify") > -1)
                {
                    this.btnCommit.Visible = false;
                    this.btnEdit.Visible = true;
                }
                else
                {
                    this.btnCommit.Visible = false;
                    this.btnEdit.Visible = false;
                }
                myEntryInfo = myEntry.getPubNotice(NoticeCode);
                txtNoticeCode.Text = myEntryInfo.NoticeCode;
                myEntryInfo = myEntry.getPubNotice(NoticeCode);
                txtTitle.Text = myEntryInfo.Title;
                txtMemo.Text = myEntryInfo.Memo;
                DatabindFile(NoticeCode,"view");
            }
            else if (ViewState["op"].ToString() == "add")
            {
                textUnable(true, "Add");
                this.btnCommit.Visible = true;
                this.btnEdit.Visible = false;
                DatabindFile(NoticeCode,"add");
            }
        }
    }

    #region   數據驗證
    private string CommitError()
    {
        string return_value = "";
        lblMessage.Text = "";
        if (txtTitle.Text == "")
        {
            return_value += lblTitle.Text.ToString() + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        return return_value;
    }
    #endregion
    protected void btnCommit_Click(object sender, EventArgs e)
    {
        if (this.CommitError().Trim() != "")
        {
            lblMessage.Text = CommitError().Trim();
            return;
        }

        try
        {
            myEntryInfo.Title = txtTitle.Text;
            myEntryInfo.Memo = txtMemo.Text;
            myEntryInfo.ModifyUser = Session["loginUser"].ToString();
            myEntryInfo.ModifyDate = DateTime.Now;

            if (ViewState["op"].ToString() == "add")
            {
                myEntryInfo.NoticeCode = PubFunction.GetFlowID("pubNotice", "NoticeCode");
                myEntryInfo.CreateUser = Session["loginUser"].ToString();
                myEntryInfo.CreateDate = DateTime.Now;
                myEntry.Insert(myEntryInfo);
                NoticeCode = myEntryInfo.NoticeCode.ToString();
                txtNoticeCode.Text = NoticeCode;
            }
            else
            {
                myEntryInfo.NoticeCode = txtNoticeCode.Text;
                myEntryInfo.CreateUser = txtCreateUser.Text.ToString();
                if (txtCreateDate.Text != "")
                {
                    myEntryInfo.CreateDate = Convert.ToDateTime(txtCreateDate.Text);
                }
                myEntry.Update(myEntryInfo);
            }
            this.btnCommit.Visible = false;
            this.btnEdit.Visible = true;
            ViewState["op"] = "view";
            textUnable(false, "view");
            DatabindFile(NoticeCode,"view");
            lblMessage.Text = GetGlobalResourceObject("Message", "Edit_Success").ToString();
        }
        catch (Exception ex)
        {
            string tmp = ex.ToString();
            lblMessage.Text = tmp;
            this.btnCommit.Visible = true;
            this.btnEdit.Visible = false;
        }
        //Response.Write("<script>window.opener.document.form1.submit();</script>");
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        ViewState["op"] = "edit";
        textUnable(true, "edit");
        this.btnCommit.Visible = true;
        this.btnEdit.Visible = false;
        DatabindFile("Notice","edit");
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close();</script>");
    }

    #region  able
    private void textUnable(bool isAble, string op)
    {
        txtTitle.Enabled = isAble;
        txtMemo.Enabled = isAble;   
    }
    #endregion

    private void DatabindFile(string BaseCode,string op)
    {
        DbAccessing DAL = new DbAccessing();
        string SQL = "select * from pubAttachments where BaseType = 'Notice' and BaseCode = '"+BaseCode+"'";
        DataTable dt = DAL.ExecuteSqlTable(SQL);

            DataList1.DataSource = dt;
            DataList1.DataBind();
            if (ViewState["op"].ToString() == "add")
            {
                FileDetail1.Visible = false;
                FileDetail2.Visible = false;
            }
            else if (ViewState["op"].ToString() == "edit")
            {
                FileDetail1.Visible = false;
                FileDetail2.Visible = false;
            }
            else if (ViewState["op"].ToString() == "view")
            {
                FileDetail1.Visible = true;
                FileDetail2.Visible = true;
                if (Permission.ToLower().IndexOf("add") > -1)
                {
                    fldFile.Visible = true;
                    btnUpload.Visible = true;
                }
                else
                {
                    fldFile.Visible = false;
                    btnUpload.Visible = false;
                }
                if (Permission.ToLower().IndexOf("delete") > -1)
                {
                    for (int i = 0; i < DataList1.Items.Count; i++)
                    {
                        ((ImageButton)DataList1.Items[i].FindControl("ibtnDelete")).Visible = true;
                    }
                }
                else
                {
                    for (int i = 0; i < DataList1.Items.Count; i++)
                    {
                        ((ImageButton)DataList1.Items[i].FindControl("ibtnDelete")).Visible = false;
                    }
                }
            }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        FileUploadAndUpdateData(txtNoticeCode.Text);
        DatabindFile(txtNoticeCode.Text,"view");
    }

    private bool FileUploadAndUpdateData(string NoticeCode)
    {
        bool rtn = false;
        try
        {
            string path = Server.MapPath("../attachment/Notice/");
            if (fldFile.HasFile)
            {
                string fileExtensionName = System.IO.Path.GetExtension(fldFile.FileName).ToLower();
                string fileUniqueName = DateTime.Now.ToString("yyyyMddhhmmss") + "_" + fldFile.FileName;
                string fileName = fldFile.FileName;
                fldFile.SaveAs(path + fileUniqueName);
                //判斷是不是上傳成功
                if (System.IO.File.Exists(path + fileUniqueName))
                {
                    PubAttachments myEntryAttachments = new PubAttachments();
                    PubAttachmentsInfo myEmtryInfoAttachments = new PubAttachmentsInfo();
                    myEmtryInfoAttachments.BaseType = "Notice";
                    myEmtryInfoAttachments.BaseCode = NoticeCode;
                    myEmtryInfoAttachments.OriginalFileName = fileName;
                    myEmtryInfoAttachments.UniqueFileName = fileUniqueName;
                    myEmtryInfoAttachments.FileType = fileExtensionName;
                    myEmtryInfoAttachments.CreateUser = Session["loginUser"].ToString();
                    myEmtryInfoAttachments.CreateDate = DateTime.Now;
                    myEntryAttachments.Insert(myEmtryInfoAttachments);
                }
            }
            rtn = true;
        }
        catch (Exception ex)
        {
            rtn = false;
            lblMessage.Text = "上傳文件失敗！原因代碼:" + ex.ToString();
        }
        return rtn;
    }


    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        PubAttachments myEntry = new PubAttachments();
        PubAttachmentsInfo myEmtryInfo = new PubAttachmentsInfo();
        string Path = Server.MapPath("../attachment/Notice/");
        switch (e.CommandName)
        {
            case "Delete":
                myEmtryInfo = myEntry.getPubAttachments(Convert.ToInt32(e.CommandArgument));
                myEntry.Delete(myEmtryInfo);
                try
                {
                    System.IO.File.Delete(Path + myEmtryInfo.UniqueFileName);
                }
                catch { }
                DatabindFile(NoticeCode, ViewState["op"].ToString());
                break;
            case "Download":
                myEmtryInfo = myEntry.getPubAttachments(Convert.ToInt32(e.CommandArgument));
                DownLoadToClient(Response, Path, myEmtryInfo.UniqueFileName, myEmtryInfo.OriginalFileName);
                break;
        }
    }
    protected void DataList1_ItemCreated(object sender, DataListItemEventArgs e)
    {
        ImageButton ibtnDelete = (ImageButton)e.Item.FindControl("ibtnDelete");
        ImageButton ibtnDownload = (ImageButton)e.Item.FindControl("ibtnDownload");
        LinkButton lbtnTitle = (LinkButton)e.Item.FindControl("lbtnTitle");

        ibtnDelete.CommandArgument = DataList1.DataKeys[e.Item.ItemIndex].ToString();
        ibtnDownload.CommandArgument = DataList1.DataKeys[e.Item.ItemIndex].ToString();

        PubAttachments myEntry = new PubAttachments();
        PubAttachmentsInfo myEmtryInfo = new PubAttachmentsInfo();
        myEmtryInfo = myEntry.getPubAttachments(Convert.ToInt32(DataList1.DataKeys[e.Item.ItemIndex].ToString()));

        LinkFile = "../attachment/Notice/"+myEmtryInfo.UniqueFileName;
        
    }


    public void DownLoadToClient(System.Web.HttpResponse response, string path, string realFilename, string VirtualName)
    {
        if (realFilename.Trim() == "")
        {
            response.Write("<script>alert('Invalid filename!');</script>");
            return;
        }

        string paths = path + realFilename;
        try
        {
            if (File.Exists(paths))
            {
                FileInfo fi = new FileInfo(paths);
                response.Clear();
                response.ClearHeaders();
                response.Buffer = false;
                response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(VirtualName)));
                response.AppendHeader("Content-Length", fi.Length.ToString());
                response.ContentType = "application/octet-stream";
                response.WriteFile(paths);
                response.Flush();
                response.End();
            }
        }
        catch (Exception) { }
    }


}
