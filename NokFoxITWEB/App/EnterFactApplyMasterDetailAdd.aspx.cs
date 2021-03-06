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

public partial class App_EnterFactApplyMasterDetailAdd : System.Web.UI.Page
{

    FIH.ForeignStaff.db.EnterFactApplyDetail myEntityDetail = new FIH.ForeignStaff.db.EnterFactApplyDetail();
    FIH.ForeignStaff.db.EnterFactApplyDetailInfo myEntityInfoDetail = new FIH.ForeignStaff.db.EnterFactApplyDetailInfo();

    DbAccessing ds = new DbAccessing();
    string StrKeyId = "";
    string StrAction = "";
    protected string StrPurview = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        txtExpectedEnterDate.Attributes.Add("onclick", "calendar(this)");
        txtExpectedLeaveDate.Attributes.Add("onclick", "calendar(this)");

        //判斷按鈕權限
        StrPurview = PubFunction.BindOperPermission(this, "B01", "");

        if (!this.IsPostBack)
        {
            DataBindControls();


            //無論是新增、修改、瀏覽StrAction,StrKeyId都應該有值的。
            StrAction = Request.QueryString["action"];
            Session["action"] = (StrAction == null ? "" : StrAction);//動作
            StrKeyId = Server.UrlDecode(Request.QueryString["kerid"].Replace("'", ""));
            Session["KeyId"] = (StrKeyId == null ? "" : StrKeyId);//主表主健
            Session["ItemID"] = "";                      //明細表主健

            if ((Session["action"] == null ? "" : Session["action"].ToString()) == "add")
            {
                txtApplyCode.Text = Session["KeyId"].ToString();
                btnCommit.Visible = true;
                btnModify.Visible = false;
                txtStaffName.Focus();
                textUnable(true, StrPurview, Session["KeyId"].ToString(), Session["ItemID"].ToString());
            }
            else if ((Session["action"] == null ? "" : Session["action"].ToString()) == "edit")
            {
                txtApplyCode.Text = Session["KeyId"].ToString();
                Session["ItemID"] = Server.UrlDecode(Request.QueryString["ItemID"]);
                btnCommit.Visible = true;
                btnModify.Visible = false;
                textUnable(true, StrPurview, Session["KeyId"].ToString(), Session["ItemID"].ToString());
                BindData(Session["KeyId"].ToString(), Session["ItemID"].ToString());
            }
            else if ((Session["action"] == null ? "" : Session["action"].ToString()) == "view")
            {
                txtApplyCode.Text = Session["KeyId"].ToString();
                Session["ItemID"] = Server.UrlDecode(Request.QueryString["ItemID"]);
                btnCommit.Visible = false;
                btnModify.Visible = true;
                textUnable(false, StrPurview, Session["KeyId"].ToString(), Session["ItemID"].ToString());
                BindData(Session["KeyId"].ToString(), Session["ItemID"].ToString());
            }
        }
    }

    private void DataBindControls()
    {
        //入厰原因
        string sql = "select ReasonCode,Description from pubEnterFactReason";
        PubFunction.dlstBound(ddlEnterFactReason, sql);
        ddlEnterFactReason.Items.Remove("");

        //門崗
        sql = "select GateHouseCode , Description from pubGatehouse";
        PubFunction.dlstBound(ddlGateHouse, sql);
        ddlGateHouse.Items.Remove("");

        //預計入厰日期
        txtExpectedEnterDate.Text = DateTime.Now.ToString("yyyy-M-dd");
        txtExpectedLeaveDate.Text = DateTime.Now.ToString("yyyy-M-dd");

        txtStaffName.Focus();
    }

    protected void btnCommit_Click(object sender, EventArgs e)
    {
        //檢驗數據的完整性
        string strerror = CommitError().Trim();
        if (strerror != "")
        {
            btnCommit.Visible = true;
            btnModify.Visible = false;
            if (strerror.Length > 60)
            {
                lblMessage.Text = strerror.Substring(0, 60) + "......";
                return;
            }
            else
            {
                lblMessage.Text = strerror;
                return;
            }
        }
        try
        {
            myEntityInfoDetail.StaffName = txtStaffName.Text;
            myEntityInfoDetail.IDCardNo = txtIDCardNo.Text;
            myEntityInfoDetail.Company = txtCompany.Text;
            myEntityInfoDetail.Tel = txtTel.Text;
            myEntityInfoDetail.EnterFactReason = ddlEnterFactReason.SelectedValue;
            myEntityInfoDetail.GateHouse = ddlGateHouse.SelectedValue;
            myEntityInfoDetail.ReceptionDept = txtReceptionDept.Text;
            myEntityInfoDetail.ReceptionStaff = txtReceptionStaff.Text;
            myEntityInfoDetail.ReceptionTel = txtReceptionTel.Text;
            myEntityInfoDetail.CardNo = txtCardNo.Text;
            myEntityInfoDetail.RightDescription = txtRightDescription.Text;
            myEntityInfoDetail.ExpectedEnterDate = Convert.ToDateTime(txtExpectedEnterDate.Text);
            myEntityInfoDetail.ExpectedEnterTime = ddlExpectedEnterTimeHour.SelectedValue + ":" + ddlExpectedEnterTimeMinute.SelectedValue;
            myEntityInfoDetail.ExpectedLeaveDate = Convert.ToDateTime(txtExpectedLeaveDate.Text);
            myEntityInfoDetail.ExpectedLeaveTime = ddlExpectedLeaveTimeHour.SelectedValue + ":" + ddlExpectedLeaveTimeMinute.SelectedValue;
            myEntityInfoDetail.TakeItems = txtTakeItems.Text;
            myEntityInfoDetail.Memo = txtMemo.Text;
            if (txtActualEnterDate.Text != "")
            {
                myEntityInfoDetail.ActualEnterDate = Convert.ToDateTime(txtActualEnterDate.Text);
            }
            myEntityInfoDetail.ActualEnterTime = txtActualEnterTime.Text;

            if (txtActualLeaveDate.Text != "")
            {
                myEntityInfoDetail.ActualLeaveDate = Convert.ToDateTime(txtActualLeaveDate.Text);
            }
            myEntityInfoDetail.ActualLeaveTime = txtActualLeaveTime.Text;
            myEntityInfoDetail.CardStatus = txtCardStatus.Text;
            myEntityInfoDetail.InitiateId = txtInitiateID.Text;
            if (txtInitiateDate.Text != "")
            {
                myEntityInfoDetail.InitiateDate = Convert.ToDateTime(txtInitiateDate.Text);
            }


            if ((Session["action"] == null ? "" : Session["action"].ToString()) == "add")
            {

                txtItemNo.Text = PubFunction.GeItemNO("appEnterFactApplyDetail", Session["KeyId"].ToString(), "ApplyCode");
                myEntityInfoDetail.ApplyCode = txtApplyCode.Text;
                myEntityInfoDetail.ItemNo = Convert.ToInt32(txtItemNo.Text);
                myEntityInfoDetail.InitiateDate = DateTime.Now;
                myEntityInfoDetail.InitiateId = Session["loginUser"].ToString();
                myEntityDetail.Insert(myEntityInfoDetail);
            }
            else if ((Session["action"] == null ? "" : Session["action"].ToString()) == "edit")
            {
                txtItemNo.Text = Session["ItemID"].ToString();
                myEntityInfoDetail.ApplyCode = txtApplyCode.Text;
                myEntityInfoDetail.ItemNo = Convert.ToInt32(txtItemNo.Text);
                myEntityInfoDetail.ModiDate = DateTime.Now;
                myEntityInfoDetail.ModiId = Session["loginUser"].ToString();

                //有EditCard的就是安管發卡，
                //沒有EditCard的就是申請外來人員。
                if (StrPurview.IndexOf("EditCard") > 0)
                {
                    myEntityInfoDetail.CardStatus = "1";
                }
                else
                {
                    myEntityInfoDetail.CardStatus = "0";
                }
                myEntityDetail.Update(myEntityInfoDetail);
            }
            btnCommit.Visible = false;
            btnModify.Visible = true;
            lblMessage.Text = GetGlobalResourceObject("Message", "Add_Success").ToString();
            Response.Write("<script>window.opener.document.formDetail.submit();</script>");
        }
        catch(Exception ex) 
        {
            btnCommit.Visible = true;
            btnModify.Visible = false;
            lblMessage.Text = "保存失敗:" + ex.ToString();
        }

    }

    /// <summary>
    /// 檢驗數據的完整性
    /// </summary>
    /// <returns></returns>
    private string CommitError()
    {
        #region   数据验证
        string return_value = "";
        lblMessage.Text = "";
        if (txtStaffName.Text.Trim() == "")
        {
            return_value += lblStaffName.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (txtCompany.Text.Trim() == "")
        {
            return_value += lblCompany.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (txtReceptionDept.Text.Trim() == "")
        {
            return_value += lblReceptionDept.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (txtReceptionStaff.Text.Trim() == "")
        {
            return_value += lblReceptionStaff.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (txtCardNo.Enabled && txtCardNo.Text.Trim() == "")
        {
            return_value += lblCardNo.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
        }
        if (txtExpectedEnterDate.Text.Trim() == "")
        {
            return_value += lblExpectedEnterDate.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
            return return_value;
        }
        if (txtExpectedLeaveDate.Text.Trim() == "")
        {
            return_value += lblExpectedLeaveDate.Text + GetGlobalResourceObject("Message", "Save_CannotNull").ToString();
            return return_value;
        }

        DateTime startDate = Convert.ToDateTime(txtExpectedEnterDate.Text);
        DateTime startDateTime = new DateTime(startDate.Year,startDate.Month,startDate.Day,Convert.ToInt32(ddlExpectedEnterTimeHour.SelectedValue),Convert.ToInt32(ddlExpectedEnterTimeMinute.SelectedValue),0);

        DateTime closeDate = Convert.ToDateTime(txtExpectedLeaveDate.Text);
        DateTime closeDateTime = new DateTime(closeDate.Year, closeDate.Month, closeDate.Day, Convert.ToInt32(ddlExpectedLeaveTimeHour.SelectedValue), Convert.ToInt32(ddlExpectedLeaveTimeMinute.SelectedValue),0);

        if (startDateTime > closeDateTime)
        {
            return_value += "預計入廠時間不能大於預計出廠時間!";
        }
        return return_value;
        #endregion
    }

    private void BindData(string ID,string ItemID)
    {
        #region   數據榜定

        myEntityInfoDetail = myEntityDetail.getEnterFactApplyDetail(ID, Convert.ToInt32(ItemID));

        txtApplyCode.Text = ID;
        txtItemNo.Text = ItemID;
        txtStaffName.Text = myEntityInfoDetail.StaffName;
        txtIDCardNo.Text = myEntityInfoDetail.IDCardNo;
        txtCompany.Text = myEntityInfoDetail.Company;
        txtTel.Text = myEntityInfoDetail.Tel;
        ddlEnterFactReason.Items.FindByValue(myEntityInfoDetail.EnterFactReason).Selected = true;
        ddlGateHouse.Items.FindByValue(myEntityInfoDetail.GateHouse).Selected = true;
        txtReceptionDept.Text = myEntityInfoDetail.ReceptionDept;
        txtReceptionStaff.Text = myEntityInfoDetail.ReceptionStaff;
        txtReceptionTel.Text = myEntityInfoDetail.ReceptionTel;
        txtExpectedEnterDate.Text = Convert.ToDateTime(myEntityInfoDetail.ExpectedEnterDate).ToString("yyyy/M/dd");
        if (myEntityInfoDetail.ExpectedEnterTime.Length > 0)
        {
            ddlExpectedEnterTimeHour.Items.FindByValue(myEntityInfoDetail.ExpectedEnterTime.Substring(0, 2)).Selected = true;
            ddlExpectedEnterTimeMinute.Items.FindByValue(myEntityInfoDetail.ExpectedEnterTime.Substring(3, 2)).Selected = true;
        }

        txtExpectedLeaveDate.Text = Convert.ToDateTime(myEntityInfoDetail.ExpectedLeaveDate).ToString("yyyy/M/dd");
        if (myEntityInfoDetail.ExpectedLeaveTime.Length > 0)
        {
            ddlExpectedLeaveTimeHour.Items.FindByValue(myEntityInfoDetail.ExpectedLeaveTime.Substring(0, 2)).Selected = true;
            ddlExpectedLeaveTimeMinute.Items.FindByValue(myEntityInfoDetail.ExpectedLeaveTime.Substring(3, 2)).Selected = true;
        }
        txtTakeItems.Text = myEntityInfoDetail.TakeItems;
        txtMemo.Text = myEntityInfoDetail.Memo;
        switch (myEntityInfoDetail.CardStatus)
        {
            case "0":   //初始狀態
            case "":   //初始狀態
                txtCardStatusName.Text = "初始狀態";
                break;
            case "1":   //發卡
                txtCardStatusName.Text = "發卡";
                break;
            case "2":   //入廠
                txtCardStatusName.Text = "入廠";
                break;
            case "9":   //出廠
                txtCardStatusName.Text = "出廠";
                break;
        }

        txtActualEnterDate.Text = myEntityInfoDetail.ActualEnterDate.ToString(); ;
        txtActualEnterTime.Text = myEntityInfoDetail.ActualEnterTime;
        txtActualLeaveDate.Text = myEntityInfoDetail.ActualLeaveDate.ToString();
        
        txtActualLeaveTime.Text = myEntityInfoDetail.ActualLeaveTime;
        txtCardStatus.Text = myEntityInfoDetail.CardStatus;
        txtInitiateID.Text = myEntityInfoDetail.InitiateId;
        txtInitiateDate.Text = myEntityInfoDetail.InitiateDate.ToString();


        txtCardNo.Text = myEntityInfoDetail.CardNo;
        txtRightDescription.Text = myEntityInfoDetail.RightDescription;

        #endregion
    }
    protected void btnExit_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.close()</script>");
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        btnModify.Visible = false;
        btnCommit.Visible = true;
        Session["action"] = "edit";
        textUnable(true, StrPurview, Session["KeyId"].ToString(), Session["ItemID"].ToString());
    }

    private void textUnable(bool isAble, string StrPurview,string ApplyCode,string ItemNo)
    {
        #region
        string status = PubFunction.GetStatusByApplyCode(ApplyCode);
        string cardStatus = PubFunction.GetCardStatusByItemNo(ApplyCode, ItemNo);

        if (StrPurview.IndexOf("EditCard") > -1 && StrPurview.IndexOf("modify") > -1)
        {
            if (status == "0")
            {
                txtStaffName.Enabled = isAble;
                txtCompany.Enabled = isAble;
                txtIDCardNo.Enabled = isAble;
                txtTel.Enabled = isAble;
                ddlEnterFactReason.Enabled = isAble;
                ddlGateHouse.Enabled = isAble;
                txtReceptionDept.Enabled = isAble;
                txtReceptionStaff.Enabled = isAble;
                txtReceptionTel.Enabled = isAble;
                txtExpectedEnterDate.Enabled = isAble;
                ddlExpectedEnterTimeHour.Enabled = isAble;
                ddlExpectedEnterTimeMinute.Enabled = isAble;
                txtExpectedLeaveDate.Enabled = isAble;
                ddlExpectedLeaveTimeHour.Enabled = isAble;
                ddlExpectedLeaveTimeMinute.Enabled = isAble;
                txtTakeItems.Enabled = isAble;
                txtMemo.Enabled = isAble;

                txtCardNo.Enabled = isAble;
                txtRightDescription.Enabled = isAble;
            }
            else
            {
                txtStaffName.Enabled = false;
                txtCompany.Enabled = false;
                txtIDCardNo.Enabled = false;
                txtTel.Enabled = false;
                ddlEnterFactReason.Enabled = false;
                ddlGateHouse.Enabled = false;
                txtReceptionDept.Enabled = false;
                txtReceptionStaff.Enabled = false;
                txtReceptionTel.Enabled = false;
                txtExpectedEnterDate.Enabled = false;
                ddlExpectedEnterTimeHour.Enabled = false;
                ddlExpectedEnterTimeMinute.Enabled = false;
                txtExpectedLeaveDate.Enabled = false;
                ddlExpectedLeaveTimeHour.Enabled = false;
                ddlExpectedLeaveTimeMinute.Enabled = false;
                txtTakeItems.Enabled = false;
                txtMemo.Enabled = false;

                txtCardNo.Enabled = isAble;
                txtRightDescription.Enabled = isAble;
            }

        }
        else if(StrPurview.IndexOf("EditCard")>-1)
        {
            txtStaffName.Enabled = false;
            txtCompany.Enabled = false;
            txtIDCardNo.Enabled = false;
            txtTel.Enabled = false;
            ddlEnterFactReason.Enabled = false;
            ddlGateHouse.Enabled = false;
            txtReceptionDept.Enabled = false;
            txtReceptionStaff.Enabled = false;
            txtReceptionTel.Enabled = false;
            txtExpectedEnterDate.Enabled = false;
            ddlExpectedEnterTimeHour.Enabled = false;
            ddlExpectedEnterTimeMinute.Enabled = false;
            txtExpectedLeaveDate.Enabled = false;
            ddlExpectedLeaveTimeHour.Enabled = false;
            ddlExpectedLeaveTimeMinute.Enabled = false;
            txtTakeItems.Enabled = false;
            txtMemo.Enabled = false;

            txtCardNo.Enabled = isAble;
            txtRightDescription.Enabled = isAble;
        }
        else if (StrPurview.IndexOf("modify") > -1)
        {
            txtStaffName.Enabled = isAble;
            txtCompany.Enabled = isAble;
            txtIDCardNo.Enabled = isAble;
            txtTel.Enabled = isAble;
            ddlEnterFactReason.Enabled = isAble;
            ddlGateHouse.Enabled = isAble;
            txtReceptionDept.Enabled = isAble;
            txtReceptionStaff.Enabled = isAble;
            txtReceptionTel.Enabled = isAble;
            txtExpectedEnterDate.Enabled = isAble;
            ddlExpectedEnterTimeHour.Enabled = isAble;
            ddlExpectedEnterTimeMinute.Enabled = isAble;
            txtExpectedLeaveDate.Enabled = isAble;
            ddlExpectedLeaveTimeHour.Enabled = isAble;
            ddlExpectedLeaveTimeMinute.Enabled = isAble;
            txtTakeItems.Enabled = isAble;
            txtMemo.Enabled = isAble;

            txtCardNo.Enabled = false;
            txtRightDescription.Enabled = false;
        }
        else
        {
            txtStaffName.Enabled = false;
            txtCompany.Enabled = false;
            txtIDCardNo.Enabled = false;
            txtTel.Enabled = false;
            ddlEnterFactReason.Enabled = false;
            ddlGateHouse.Enabled = false;
            txtReceptionDept.Enabled = false;
            txtReceptionStaff.Enabled = false;
            txtReceptionTel.Enabled = false;
            txtExpectedEnterDate.Enabled = false;
            ddlExpectedEnterTimeHour.Enabled = false;
            ddlExpectedEnterTimeMinute.Enabled = false;
            txtExpectedLeaveDate.Enabled = false;
            ddlExpectedLeaveTimeHour.Enabled = false;
            ddlExpectedLeaveTimeMinute.Enabled = false;
            txtTakeItems.Enabled = false;
            txtMemo.Enabled = false;

            txtCardNo.Enabled = false;
            txtRightDescription.Enabled = false;
        }

        //判斷是不是已經發卡，
        if (cardStatus == "2" || cardStatus == "3")
        {
            txtCardNo.Enabled = false;
            txtRightDescription.Enabled = false;
        }
        if (status != "" && status != "0")
        {
            txtStaffName.Enabled = false;
            txtCompany.Enabled = false;
            txtIDCardNo.Enabled = false ;
            txtTel.Enabled = false;
            ddlEnterFactReason.Enabled = false;
            ddlGateHouse.Enabled = false;
            txtReceptionDept.Enabled = false;
            txtReceptionStaff.Enabled = false;
            txtReceptionTel.Enabled = false;
            txtExpectedEnterDate.Enabled = false;
            ddlExpectedEnterTimeHour.Enabled = false;
            ddlExpectedEnterTimeMinute.Enabled = false;
            txtExpectedLeaveDate.Enabled = false;
            ddlExpectedLeaveTimeHour.Enabled = false;
            ddlExpectedLeaveTimeMinute.Enabled = false;
            txtTakeItems.Enabled = false;
            txtMemo.Enabled = false;
        }

        #endregion
    }

    private string GetStatusByApplyCode(string applyCode)
    {
        string rtn = "";
        try
        {
            DataTable dt = ds.ExecuteSqlTable("select Status from appEnterFactApplyMaster where ApplyCode = '" + applyCode + "'");
            if (dt.Rows.Count > 0)
            {
                rtn = dt.Rows[0]["Status"].ToString();
            }
        }
        catch { }
        return rtn;
    }

    private void SetBtnPermission(string ApplyCode, string ItemNo)
    {
        //string status = PubFunction.GetStatusByApplyCode(ApplyCode);
    }


}
