﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;

public partial class Boundary_B2B_Label_NEW : System.Web.UI.UserControl
{
    private void DisableControls(Control gv)
    {

        LinkButton lb = new LinkButton();
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(LinkButton))
            {
                l.Text = (gv.Controls[i] as LinkButton).Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }
            else if (gv.Controls[i].GetType() == typeof(DropDownList))
            {
                l.Text = (gv.Controls[i] as DropDownList).SelectedItem.Text;
                gv.Controls.Remove(gv.Controls[i]);
                gv.Controls.AddAt(i, l);
            }

            if (gv.Controls[i].HasControls())
            {
                DisableControls(gv.Controls[i]);
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            tbStartDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 08:00";
            tbEndDate.DateTextBox.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            if (tbStartDate.DateTextBox.Text.CompareTo(tbEndDate.DateTextBox.Text) > 0)
                tbEndDate.DateTextBox.Text = tbStartDate.DateTextBox.Text;
        }
    }

    private void bindTio()
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetDataTio();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "TioLabel");
        if (ds.Tables[0].Rows.Count > 0)
            this.gvTioLabel.Visible = true;
        this.gvTioLabel.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvTioLabel.DataSource = ds;
            gvTioLabel.DataBind();
            int columnCount = gvTioLabel.Rows[0].Cells.Count;
            gvTioLabel.Rows[0].Cells.Clear();
            gvTioLabel.Rows[0].Cells.Add(new TableCell());
            gvTioLabel.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvTioLabel.Rows[0].Cells[0].Text = "No Records Found.";
            gvTioLabel.Rows[0].Visible = true;
        }
        else
        {
            this.gvTioLabel.AllowPaging = true;
            string pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvTioLabel.PageSize = Convert.ToInt16(pagerows);//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈
            this.gvTioLabel.DataBind();
        }
    }

    private string GetDataTio()
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strPoNo = txtPoNo.Text.Trim();
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        int PageRows = Convert.ToInt16(strPageRows);  
        string strsql = "select * from dbo.tiolabel t1(nolock) where  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.ordnum in (" + strPoNo + ")";
        }

        strsql += " order by t1.lasteditdt " + strOrder;

        return strsql;
    }

    private void bind()//数据绑定方法
    {
        //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
        //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
        string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
        SqlConnection conn = new SqlConnection(strConn);
        string strsql = GetData();
        if (strsql.Equals("input error"))
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
            return;
        }
        SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

        //SqlDataAdapter sda1 = new SqlDataAdapter(strSql, conn);
        DataSet ds = new DataSet();
        sda.Fill(ds, "shiplabel");
        this.gvShipLabel.DataSource = ds.Tables[0].DefaultView;

        if (ds.Tables[0].Rows.Count == 0)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gvShipLabel.DataSource = ds;
            gvShipLabel.DataBind();
            int columnCount = gvShipLabel.Rows[0].Cells.Count;
            gvShipLabel.Rows[0].Cells.Clear();
            gvShipLabel.Rows[0].Cells.Add(new TableCell());
            gvShipLabel.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvShipLabel.Rows[0].Cells[0].Text = "No Records Found.";
            gvShipLabel.Rows[0].Visible = true;
            //btnAll.Visible = false;
            //lblAll.Visible = false;
            //lblSingle.Visible = false;
            //btnSingle.Visible = false;
            btnExport.Visible = false;

        }
        else
        {
            btnExport.Visible = true;
            panel2.Visible = true;
            this.gvShipLabel.AllowPaging = true;
            string pagerows = ddlPageRows.SelectedValue.Trim().ToString();
            this.gvShipLabel.PageSize = Convert.ToInt16(pagerows);//设置分页大小　／／要先设置分页后绑定数据不然分页不起作用哈哈

            this.gvShipLabel.DataBind();
        }
        conn.Close();
    }

    private string GetData()
    {
        string strStartDate = tbStartDate.DateTextBox.Text.Trim();
        string strEndDate = tbEndDate.DateTextBox.Text.Trim();
        string strPoNo = txtPoNo.Text.Trim();
        string strOrder = ddlOrder.SelectedValue.Trim();
        string strPageRows = ddlPageRows.SelectedValue.Trim();
        int PageRows = Convert.ToInt16(strPageRows); 

        string strsql = "select * from dbo.shiplabel t1(nolock) where  t1.lasteditdt>=convert(varchar,'" + strStartDate + "',120)  and t1.lasteditdt<=convert(varchar,'" + strEndDate + "',120)";

        if (!strPoNo.Equals(""))
        {
            strPoNo = Split(strPoNo);
            if (strPoNo.Equals("split error"))
                return "input error";
            else
                strsql += " and t1.ordnum in (" + strPoNo + ")";
        }

        strsql += " order by t1.lasteditdt " + strOrder;

        return strsql;
    }

    private string Split(string strPoNo)
    {
        string strInput = strPoNo;
        string strOutput = "";
        try
        {
            string[] strInput1 = strInput.Split(new Char[] { ',' });
            for (int i = 0; i < strInput1.Length; i++)
                strOutput = strOutput + "'" + strInput1[i].Trim().ToString() + "',";
            strOutput = strOutput.Substring(0, strOutput.Length - 1);

            return strOutput;
        }
        catch
        {
            return "split error";
        }
    } 

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblShip.Visible = true;
        lblTio.Visible = true;
        bind(); //调用数据绑定方法
        bindTio();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string ExportPath = Request.PhysicalApplicationPath + "Temp\\";
            string strFileName = Session.SessionID + DateTime.Now.ToString("yyyyMMdd hhmmss") + ".xls";
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF7;
            Response.AppendHeader("Content-Disposition", "attachment;filename=B2B_Label_MainReport.xls");
            Response.ContentType = "application/ms-excel";
            Response.Write("<meta http-equiv=Content-Type content=\"text/html; charset=GB2312\">");
            this.EnableViewState = false;
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);

            //string strConn = "Data Source=10.134.140.86,7653;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_TY;Password=eFpeL07@";
            //string strConn = "Data Source=10.134.93.90,6593;Initial Catalog=Dell;Persist Security Info=True;User ID=DellB2B_WEB;Password=aL+qIN!HQq";
            string strConn = ConfigurationManager.ConnectionStrings["DellConnectionString"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            string strsql = GetData();
            if (strsql.Equals("input error"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('條件輸入錯誤！！');</script>");
                return;
            }

            SqlDataAdapter sda = new SqlDataAdapter(strsql, conn);

            DataSet ds = new DataSet();
            sda.Fill(ds, "PURCHASEORDERSMAIN");
            this.gvShipLabel.DataSource = ds.Tables[0].DefaultView;

            this.gvShipLabel.AllowPaging = false;
            this.gvShipLabel.DataBind();
            DisableControls(gvShipLabel);
            gvShipLabel.RenderControl(hw);
            Response.Write(tw.ToString());
            Response.End();

            // turn the paging on again 
            gvShipLabel.AllowPaging = true;
            bind();

        }
        catch
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Error", "<script language='javascript'>alert('導出Excel異常！！');</script>");
            return;
        }       
    }
    protected void gvShipLabel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");

        e.Row.Cells[5].Attributes.Add("style", "vnd.ms-excel.numberformat:@");
        e.Row.Cells[12].Attributes.Add("style", "vnd.ms-excel.numberformat:@");

        LinkButton lbtOrdnum;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            lbtOrdnum = (LinkButton)e.Row.Cells[5].Controls[1];
            if (lbtOrdnum != null)
            {
                if (lbtOrdnum.CommandName == "OrdNum")
                    lbtOrdnum.CommandArgument = e.Row.RowIndex.ToString();

            }
        }   
    }
    protected void gvShipLabel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex");
            olabel.Text = Convert.ToString(gvShipLabel.PageIndex * gvShipLabel.PageSize + e.Row.RowIndex + 1);
        }
    }
    protected void gvShipLabel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "OrdNum")
        {
            string strMsegid = ((Label)gvShipLabel.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Controls[1]).Text;
            string strMsegtype = ((Label)gvShipLabel.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Controls[1]).Text;
            string strSendid = ((Label)gvShipLabel.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Controls[1]).Text;
            string strReceid = ((Label)gvShipLabel.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Controls[1]).Text;
            string strOrdno = ((LinkButton)gvShipLabel.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Controls[1]).Text;
            string strBuid = ((Label)gvShipLabel.Rows[Convert.ToInt32(e.CommandArgument)].Cells[9].Controls[1]).Text;


            string strScript = "<script language='jscript'>var res = window.open('./B2B_Label_DetailReport.aspx?Msegid=" + strMsegid + "&Msegtype=" + strMsegtype
                + "&Sendid=" + strSendid + "&Receid=" + strReceid + "&Ordno=" + strOrdno + "&Buid=" + strBuid + "','_blank', '');</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "temp", strScript);
        }
    }
    protected void gvTioLabel_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#0099ff'");
        e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c;");
    }
    protected void gvTioLabel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label olabel;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            olabel = (Label)e.Row.Cells[0].FindControl("lblIndex1");
            olabel.Text = Convert.ToString(gvTioLabel.PageIndex * gvTioLabel.PageSize + e.Row.RowIndex + 1);
        }
    }
}
