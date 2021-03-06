using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;


/// <summary>
/// Summary description for Operation
/// </summary>
public class Operation
{
    
    DataBase db = new DataBase();
    public Operation()
	{
		//
		// TODO: Add constructor logic here
		//
	}   
    #region 添加公告
    /// <summary>
    /// 添加公告
    /// </summary>
    /// <param name="title">標題</param>
    /// <param name="info">內容</param>
    /// <param name="position">發布地點</param>
    public void InsertInfo(string title, string info, string Position)
    {
        string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        SqlParameter[] parms ={             
            db.MakeInParam("@title",SqlDbType.VarChar,50,title),
            db.MakeInParam("@info",SqlDbType.NText,500,info),
            db.MakeInParam("@position",SqlDbType.VarChar,50,Position),            
        };
        int i = db.RunProc("INSERT INTO news (title,news_content, position,createtime) VALUES (@title,@info,@position,'" + dt+ "')", parms);
    }
    #endregion
    #region 添加常見問題
    /// <summary>
    /// 添加常見問題
    /// </summary>
    /// <param name="title">標題</param>
    /// <param name="info">內容</param>
    /// <param name="empno">編輯人</param>
    /// <param name="rate">重要性</param>
    public void InsertQu(string title, string info, string empno,string rate)
    {
        string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        SqlParameter[] parms ={             
            db.MakeInParam("@title",SqlDbType.VarChar,50,title),
            db.MakeInParam("@info",SqlDbType.NText,500,info),
            db.MakeInParam("@empno",SqlDbType.VarChar,50,empno), 
            db.MakeInParam("@rate",SqlDbType.VarChar,10,rate),           
        };
        int i = db.RunProc("INSERT INTO Question (question,answer, empno,createtime,rate) VALUES (@title,@info,@empno,'" + dt + "',@rate)", parms);
    }
    public void Insertad(string No, string name, string photo, string notes , string content,string Ip)
    {
        string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        SqlParameter[] parms ={             
            db.MakeInParam("@No",SqlDbType.VarChar,20,No),
            db.MakeInParam("@name",SqlDbType.VarChar,20,name),
            db.MakeInParam("@photo",SqlDbType.VarChar,20,photo),
            db.MakeInParam("@notes",SqlDbType.VarChar,50,notes),
            db.MakeInParam("@content",SqlDbType.NText,500,content), 
            db.MakeInParam("@IP",SqlDbType.VarChar,20,Ip),
     
        };
        int i = db.RunProc("INSERT INTO advice  VALUES (@No,@name,@photo,@notes,@content,@IP,'" + dt + "')", parms);
    }

    #endregion
    #region 刪除各類數據庫紀錄
    /// <summary>
    /// 刪除文件信息
    /// </summary>
    /// <param name="id">文件序號</param>
    public void DeleteInfo(string id)
    {
        int d = db.RunProc("Delete from fileInfo where fileid='" + id + "'");
    }
    /// <summary>
    /// 刪除公告信息
    /// </summary>
    /// <param name="id">公告序號</param>
    public void DeleteNews(string id)
    {
        int d = db.RunProc("Delete from news where newsid='" + id + "'");
    }
    /// <summary>
    /// 刪除常見問題
    /// </summary>
    /// <param name="id">常見問題序號</param>
    public void DeleteQu(string id)
    {
        int d = db.RunProc("Delete from question where id='" + id + "'");
    }
    public void DeleteUser(string id)
    {
        int d = db.RunProc("Delete from users where userid='" + id + "'");
    }
    public void Deletead(string id)
    {
        int d = db.RunProc("Delete from advice where ad_id='" + id + "'");
    }
    #endregion
    #region 查詢各類信息
    /// <summary>
    /// 按類型查詢文件
    /// </summary>
    /// <param name="type">文件類型</param>
    /// <returns>返回結果DataSet</returns>
    public DataSet SelectInfo(string type)
    {
        SqlParameter[] parms ={ db.MakeInParam("@type", SqlDbType.VarChar, 50, type) };
        return db.RunProcReturn("SELECT fileID, filename, datetime FROM fileInfo where type=@type ORDER BY datetime DESC", parms, "fileInfo");
    }
    /// <summary>
    /// 按文件名稱進行模糊查詢
    /// </summary>
    /// <param name="name">文件名稱</param>
    /// <returns>返回結果DataSet</returns>
    public DataSet SelectAll(string name)
    {
        return db.RunProcReturn("SELECT fileID, filename,type,datetime FROM fileInfo where filename like '%" + name + "%' ORDER BY datetime DESC", "fileInfo");
    }
    /// <summary>
    /// 查詢所有常用表單
    /// </summary>
    /// <param name="name">常用表單</param>
    /// <returns></returns>
    public DataSet SelectTable(string name)
    {
        return db.RunProcReturn("SELECT fileID, filename,type,datetime FROM fileInfo where type like '%" + name + "%' ORDER BY datetime DESC", "fileInfo");
    }
    /// <summary>
    /// 查詢所有文件信息
    /// </summary>
    public DataSet SelectAll()
    {
        return db.RunProcReturn("SELECT fileID, filename,type,datetime FROM fileInfo ORDER BY datetime DESC", "fileInfo");
    }
    /// <summary>
    /// 查詢公告信息
    /// </summary>
    public DataSet SelectInfo()
    {
        return db.RunProcReturn("SELECT newsid, title,news_content,createtime FROM news ORDER BY createtime DESC", "news");
    }
    /// <summary>
    /// 查詢常見問題
    /// </summary>
    public DataSet SelectQu()
    {
        return db.RunProcReturn("SELECT ID, Question,answer,empno,createtime FROM question ORDER BY rate DESC", "question");
    }
    /// <summary>
    /// 查詢所有用戶
    /// </summary>
    public DataSet SelectUser()
    {
        return db.RunProcReturn("SELECT UserID, userpwd,authority,username FROM users ORDER BY userid DESC", "users");
    }
    /// <summary>
    /// 查詢用戶
    /// </summary>
    public DataSet SelectUser(string id)
    {
        return db.RunProcReturn("SELECT UserID, userpwd,authority,username FROM users where userid='"+id+"'", "users");
    }
    /// <summary>
    /// 查詢建議意見
    /// </summary>
    /// <returns></returns>
    public DataSet Selectad()
    {
        return db.RunProcReturn("SELECT ad_ID, ad_No,ad_name,ad_photo,ad_notes,ad_content,ad_createtime FROM advice ORDER BY ad_createtime DESC", "advice");
    }

    #endregion
    #region 查詢文件
    /// <summary>
    /// 查詢文件
    /// </summary>
    /// <param name="infoType">文件類型</param>
    /// <param name="top">顯示個數</param>
    /// <returns></returns>
    public DataSet SearchInfo(string infoType, int top)
    {
        return db.GetdataSet("Select top(" + top + ") * from fileInfo where type='" + infoType + "' order by datetime desc", "FileInfo");
    }
     #endregion
    #region 查詢常見問題
    /// <summary>
    /// 查詢常見問題
    /// </summary>
    /// <param name="top">顯示個數</param>
    /// <returns></returns>
    public DataSet SearchQuestion(int top)
    {
        return db.GetdataSet("Select top(" + top + ") * from Question  order by rate desc", "Question");
    }
    #endregion
    #region 查詢公告信息
    public DataSet SelectNews(bool all)
    {
        if (all)
        {
            return db.RunProcReturn("SELECT * FROM news ORDER BY createtime DESC", "news");
        }
        else
        {
            return db.RunProcReturn("SELECT top 10 * FROM news ORDER BY createtime DESC", "news");
        }
    }
    #endregion
    #region 根據id查詢各種信息
    public DataSet GetInfoSet(string id)
    {
        return db.RunProcReturn("Select * from news where newsid='" + id + "' order by createtime desc", "news");
    }

    public DataSet GetFileSet(string id)
    {
        return db.RunProcReturn("Select * from fileInfo where fileId='" + id + "'", "fileInfo");
    }
    public DataSet GetDpSet(string id)
    {
        return db.RunProcReturn("Select * from department where DpID='" + id + "'", "department");
    }
    public DataSet GetDown(string name)
    {
        return db.RunProcReturn("Select * from department where Dpname='" + name + "'", "department");
    }
    public DataSet GetQuSet(string id)
    {
        return db.RunProcReturn("Select * from Question where ID='" + id + "'", "Question");
    }
    public DataSet GetAdSet(string id)
    {
        return db.RunProcReturn("Select * from advice where ad_ID='" + id + "'", "advice");
    }
    #endregion
    #region 用戶登陸
    /// <summary>
    /// 用戶登陸
    /// </summary>
    /// <param name="user">用戶名</param>
    /// <param name="pwd">密碼</param>
    /// <returns></returns>
    public DataSet Logon(string user, string pwd)
    {
        SqlParameter[] parms ={
            db.MakeInParam("@sysName",SqlDbType.VarChar,20,user),
            db.MakeInParam("@sysPwd",SqlDbType.VarChar,20,pwd)
        };
        return db.RunProcReturn("Select * from users where userID=@sysName and userPWD=@sysPwd", parms, "users");
    }
    #endregion
    #region 修改密码
    public int Update(string sql)
    {
       return db.ExcuteSql(sql); 
    }
    #endregion
    #region 日志
    public void rizhi(string Content,Page page)
    {
        // string path = Server.MapPath("..");
        //string filename = page.Request.MapPath(".")+@"\background\rizhi\rizhi.txt";
        string filename = page.Request.PhysicalApplicationPath + @"\background\rizhi\rizhi.log";
        StreamWriter writer = new StreamWriter(filename, true); 
        string Time = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        string IP = page.Request.UserHostAddress.ToString();
        writer.WriteLine(Content + ";時間為:" + Time + "   ;IP:" + IP);
        writer.Flush();
        writer.Close();
    }
    #endregion
}
