using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Collections;
using System.Data.SqlClient;
using Economy.Publibrary;
using SCM.GSCMDKen;
using SFC.TJWEB;
using System.Windows.Forms;
using System.Threading;
using EconomyUser;
using System.Linq;
using System.Web.UI;
using Economy.BLL;
// using CCVANClass.Publibrary;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Net; 


namespace SFC.TJWEB
{
/// <summary>
/// Summary description for public ShipPlanlib()	
/// </summary>
/// // ShipPlanlibPointer.TrsStrToInteger(arrayEtdUpload[var1 + 1, 5]); 
///       
public class FGISlib
{
    protected DateTime tmptoday = DateTime.Today;
    protected string Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    protected string CuurDate = DateTime.Today.ToString("yyyyMMdd"); 
    static string PDefaultConnString = "Data Source=10.186.19.207;Initial Catalog=SCM;User ID =sa;Password=Sa123456;Timeout=120;";// ConfigurationManager.AppSettings["DefaultConnectionString"];
    string RunDBPath = ConfigurationManager.AppSettings["DefaultConnectionString"];
    ShipPlanlib ShipPlanlibPointer = new ShipPlanlib();
    DeLinkPidlib DeLinkPidlibPointer = new DeLinkPidlib();
    DeLinkPidlib3 DeLinkPidlib3Pointer = new DeLinkPidlib3();
    // FSplitlib FSplitlibPointer = new FSplitlib();
    // FSplitArraylib FSplitArraylibPointer = new FSplitArraylib();
    ClassLibrarySCM1.Class1 LibSCM1Pointer = new ClassLibrarySCM1.Class1();
    ClassLibraryUSR1.Class1 LibUsrPointer = new ClassLibraryUSR1.Class1();
   
    string DBType = "oracle";
    protected string Backdbstring = ConfigurationManager.AppSettings["NormalBakupConnectionString"];


    static string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
    static string tDocumentIDPid = DateTime.Now.ToString("yyyyMMddHHmmssmm");


    public string XM101Proc(ref string[,] arrPara)
    {
        string Cmdno = arrPara[7, 0].ToString().Trim();

        if ((Cmdno == "A") && ( Cmdno == "A"))   // update
        {
            string DBType = arrPara[6, 0].ToString().Trim();
            string DBString = arrPara[9, 0].ToString().Trim();
            string SqlrW = "update FGIS.DATABOM set FLAG3 = '" + arrPara[8, 0].ToString().Trim() + "', UPTIME = '" + tDocumentID + "'  "
                         + " where DATABOMID  = '" + arrPara[1, 0].ToString().Trim() + "'  and SEQ = '" + arrPara[4, 0].ToString().Trim() + "' ";
            int v4 = PDataBaseOperation.PExecSQL(DBType, DBString, SqlrW);
            return (v4.ToString());
        }

        return ("");
    }

    public string SetupNokPSarray(string Cmdno, string UserACT, ref string[,] arrPS, string DBType, string PSDBA, ref string pst1, ref string pst2, ref string pst3, ref string pst4)
    {
        int v1 = 0, v2 = 0, v3 = 0;
        string t2 = "", t6 = "", t10 = "", t12 = "", t13 = "", t14 = "", t15 = "", t16 = "", t17 = "", t18 = "", t19 = "", t20 = "", t21 = "", t22 = "", t23 = "", t24 = "", t25 = "", t26 = "";
        string t27 = "", t28 = "", t29 = "", t30 = ""; // , t15 = "", t16 = "", t17 = "", t18 = ""
        
        if (Cmdno.ToUpper() == "GETSNBYUSER")
        {
            for (v1 = 0; v1 < 200; v1++) // 找第一個回填
            {
                if ( UserACT.ToUpper() == arrPS[v1 + 1, 1].ToString().Trim().ToUpper() && (UserACT != ""))  // User name
                {
                    pst1 = (v1 + 1).ToString();
                    return ((v1 + 1).ToString());
                    //arrPS[v1 + 1, 15] = t2.ToString().Trim();   // User Name 
                    //arrPS[v1 + 1, 16] = t33.ToString().Trim();  // User name
                    //arrPS[v1 + 1, 17] = t34.ToString().Trim();  // FGIS DBA REAL/TEST 
                    //arrPS[v1 + 1, 18] = t35.ToString().Trim();  // FGIS DBA NO
                    //arrPS[v1 + 1, 19] = t36.ToString().Trim();  // FGIS DBA REAL/TEST 
                    //arrPS[v1 + 1, 20] = t37.ToString().Trim();  // FGIS DBA NO
                }
            }

            return ("0");
        }


        string st1 = " select  *  from [IMSCMWS].[dbo].[Tbl_user] order  by userid  asc ";
        // where item='PSACCOUNT' and upper(flag2)=upper('" + strUsername + "')  and flag5 = '" + strPassword + "' ";
        DataSet dt1 = PDataBaseOperation.PSelectSQLDS(DBType, PSDBA, st1);
        if ((dt1 == null) || (dt1.Tables.Count <= 0) || (dt1.Tables[0].Rows.Count < 1))  return("-1");

        for (v1 = 0; v1 < dt1.Tables[0].Rows.Count; v1++)
        {
            arrPS[v1 + 1, 1] = dt1.Tables[0].Rows[v1]["userid"].ToString().Trim();   // Login 
            arrPS[v1 + 1, 2] = dt1.Tables[0].Rows[v1]["F2"].ToString().Trim();       // factory number  SiteNo1003
            arrPS[v1 + 1, 0] = dt1.Tables[0].Rows[v1]["F1"].ToString().Trim();       // Vendid
            //arrPS[v1 + 1, 3] = "NokFoxDB";                                         // FGIS DBA REAL/TEST 
            //arrPS[v1 + 1, 4] = dt1.Tables[0].Rows[v1]["DTYPE"].ToString().Trim();  // FGIS DBA NO
            //arrPS[v1 + 1, 5] = dt1.Tables[0].Rows[v1]["FLAG11"].ToString().Trim(); // RUNMODE 
            //arrPS[v1 + 1, 6] = dt1.Tables[0].Rows[v1]["FLAG12"].ToString().Trim(); // TEST/REAL
            //arrPS[v1 + 1, 7] = "Fatcory Name Model";                                     // factory 
            //arrPS[v1 + 1, 8] = dt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // FGIS DBA NO
        }

        string st2 = " select  *  from [IMSCMWS].[dbo].[FGISM1001] where F1 = 'NokFoxM2002' order  by F2  asc ";
        DataSet dt2 = PDataBaseOperation.PSelectSQLDS(DBType, PSDBA, st2);
        if ((dt2 == null) || (dt2.Tables.Count <= 0) || (dt2.Tables[0].Rows.Count < 1)) return("-1");
        for (v2 = 0; v2 < dt2.Tables[0].Rows.Count; v2++)
        {
            t2  = dt2.Tables[0].Rows[v2]["F2"].ToString().Trim();     // factory numeber SiteNo1003
            t6  = dt2.Tables[0].Rows[v2]["F6"].ToString().Trim();    // VendID name
            t10 = dt2.Tables[0].Rows[v2]["F10"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t12 = dt2.Tables[0].Rows[v2]["F12"].ToString().Trim();  // FGIS DBA NO
            t13 = dt2.Tables[0].Rows[v2]["F13"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t14 = dt2.Tables[0].Rows[v2]["F14"].ToString().Trim();  // FGIS DBA NO
            t15 = dt2.Tables[0].Rows[v2]["F15"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t16 = dt2.Tables[0].Rows[v2]["F16"].ToString().Trim();  // FGIS DBA NO
            t17 = dt2.Tables[0].Rows[v2]["F17"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t18 = dt2.Tables[0].Rows[v2]["F18"].ToString().Trim();  // FGIS DBA NO
            t19 = dt2.Tables[0].Rows[v2]["F19"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t20 = dt2.Tables[0].Rows[v2]["F20"].ToString().Trim();  // FGIS DBA NO
            t21 = dt2.Tables[0].Rows[v2]["F21"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t22 = dt2.Tables[0].Rows[v2]["F22"].ToString().Trim();  // FGIS DBA NO
            t23 = dt2.Tables[0].Rows[v2]["F23"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t24 = dt2.Tables[0].Rows[v2]["F24"].ToString().Trim();  // FGIS DBA NO
            t25 = dt2.Tables[0].Rows[v2]["F25"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t26 = dt2.Tables[0].Rows[v2]["F26"].ToString().Trim();  // FGIS DBA NO
            t27 = dt2.Tables[0].Rows[v2]["F27"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t27 = dt2.Tables[0].Rows[v2]["F28"].ToString().Trim();  // FGIS DBA NO
            t29 = dt2.Tables[0].Rows[v2]["F29"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t30 = dt2.Tables[0].Rows[v2]["F30"].ToString().Trim();  // FGIS DBA NO
         


            for (v1 = 0; v1 < dt1.Tables[0].Rows.Count; v1++) // 找第一個回填
            {
                if (t2 == "Hanoi") 
                    t2 = t2;
                if (t2 == arrPS[v1 + 1, 2].ToString().Trim()  &&  ( t2 != "" ) )  // User name
                {
                    arrPS[v1 + 1, 03] = t6.ToString().Trim();    
                    arrPS[v1 + 1, 04] = t10.ToString().Trim();  
                    arrPS[v1 + 1, 05] = t12.ToString().Trim();  
                    arrPS[v1 + 1, 06] = t13.ToString().Trim(); 
                    arrPS[v1 + 1, 07] = t14.ToString().Trim(); 
                    arrPS[v1 + 1, 08] = t15.ToString().Trim(); 
                    arrPS[v1 + 1, 09] = t16.ToString().Trim(); 
                    arrPS[v1 + 1, 10] = t17.ToString().Trim(); 
                    arrPS[v1 + 1, 11] = t18.ToString().Trim(); 
                    arrPS[v1 + 1, 12] = t19.ToString().Trim(); 
                    arrPS[v1 + 1, 13] = t20.ToString().Trim(); 
                    arrPS[v1 + 1, 14] = t21.ToString().Trim();    
                    arrPS[v1 + 1, 15] = t22.ToString().Trim();  
                    arrPS[v1 + 1, 16] = t23.ToString().Trim(); 
                    arrPS[v1 + 1, 17] = t24.ToString().Trim(); 
                    arrPS[v1 + 1, 18] = t25.ToString().Trim(); 
                    arrPS[v1 + 1, 19] = t26.ToString().Trim(); 
                    arrPS[v1 + 1, 20] = t27.ToString().Trim(); 
                    arrPS[v1 + 1, 21] = t28.ToString().Trim(); 
                    arrPS[v1 + 1, 22] = t29.ToString().Trim(); 
                    arrPS[v1 + 1, 23] = t30.ToString().Trim();                    

                }
            }
          
        }

       


        return ("0");
    }
    
    public string SetupPSarray(string Cmdno, string UserACT, ref string[,] arrPS, string PSDBA, ref string pst1, ref string pst2, ref string pst3, ref string pst4)
    {
        int v1 = 0, v2 = 0, v3 = 0;
        string t2 = "", t33 = "", t34 = "", t35 = "", t36 = "", t37 = "";

        if (Cmdno.ToUpper() == "GETSNBYUSER")
        {
            for (v1 = 0; v1 < 200; v1++) // 找第一個回填
            {
                if ( UserACT.ToUpper() == arrPS[v1 + 1, 1].ToString().Trim().ToUpper() && (UserACT != ""))  // User name
                {
                    pst1 = (v1 + 1).ToString();
                    return ((v1 + 1).ToString());
                    //arrPS[v1 + 1, 15] = t2.ToString().Trim();   // User Name 
                    //arrPS[v1 + 1, 16] = t33.ToString().Trim();  // User name
                    //arrPS[v1 + 1, 17] = t34.ToString().Trim();  // FGIS DBA REAL/TEST 
                    //arrPS[v1 + 1, 18] = t35.ToString().Trim();  // FGIS DBA NO
                    //arrPS[v1 + 1, 19] = t36.ToString().Trim();  // FGIS DBA REAL/TEST 
                    //arrPS[v1 + 1, 20] = t37.ToString().Trim();  // FGIS DBA NO
                }
            }

            return ("0");
        }


        string st1 = " select  *  from FGIS.DATABOM1001 where  item = 'PSACCOUNT' order  by DATABOMID  asc ";
        //      string str11 = "select * from FGIS.DATABOM1001 where item='PSACCOUNT' and upper(flag2)=upper('" + strUsername + "')  and flag5 = '" + strPassword + "' ";
        DataSet dt1 = PDataBaseOperation.PSelectSQLDS("oracle", PSDBA, st1);
        if ((dt1 == null) || (dt1.Tables.Count <= 0) || (dt1.Tables[0].Rows.Count < 1))  return("-1");

        for (v1 = 0; v1 < dt1.Tables[0].Rows.Count; v1++)
        {
            arrPS[v1 + 1, 1] = dt1.Tables[0].Rows[v1]["FLAG1"].ToString().Trim();  // Login 
            arrPS[v1 + 1, 2] = dt1.Tables[0].Rows[v1]["FLAG2"].ToString().Trim();  // User name
            arrPS[v1 + 1, 3] = "FGISDB";                                           // FGIS DBA REAL/TEST 
            arrPS[v1 + 1, 4] = dt1.Tables[0].Rows[v1]["DTYPE"].ToString().Trim();  // FGIS DBA NO
            arrPS[v1 + 1, 5] = dt1.Tables[0].Rows[v1]["FLAG11"].ToString().Trim(); // RUNMODE 
            arrPS[v1 + 1, 6] = dt1.Tables[0].Rows[v1]["FLAG12"].ToString().Trim(); // TEST/REAL
            arrPS[v1 + 1, 7] = "Fatcory Name Model";                                     // factory 
            arrPS[v1 + 1, 8] = dt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();  // FGIS DBA NO
        }

        string st2 = " select  *  from FGIS.FGISM1001 where  F1 = 'NokFoxM2002' order  by SEQID  asc ";
        DataSet dt2 = PDataBaseOperation.PSelectSQLDS("oracle", PSDBA, st2);
        if ((dt2 == null) || (dt2.Tables.Count <= 0) || (dt2.Tables[0].Rows.Count < 1)) return("-1");
        for (v2 = 0; v2 < dt2.Tables[0].Rows.Count; v2++)
        {
            t2  = dt2.Tables[0].Rows[v2]["F2"].ToString().Trim();   // User Name 
            t33 = dt2.Tables[0].Rows[v2]["F33"].ToString().Trim();  // User name
            t34 = dt2.Tables[0].Rows[v2]["F34"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t35 = dt2.Tables[0].Rows[v2]["F35"].ToString().Trim();  // FGIS DBA NO
            t36 = dt2.Tables[0].Rows[v2]["F36"].ToString().Trim();  // FGIS DBA REAL/TEST 
            t37 = dt2.Tables[0].Rows[v2]["F37"].ToString().Trim();  // FGIS DBA NO

            for (v1 = 0; v1 < dt1.Tables[0].Rows.Count; v1++) // 找第一個回填
            {
                if (t2 == "Hanoi") 
                    t2 = t2;
                if (t2 == arrPS[v1 + 1, 8].ToString().Trim()  &&  ( t2 != "" ) )  // User name
                {
                    arrPS[v1 + 1, 15] = t2.ToString().Trim();   // User Name 
                    arrPS[v1 + 1, 16] = t33.ToString().Trim();  // User name
                    arrPS[v1 + 1, 17] = t34.ToString().Trim();  // FGIS DBA REAL/TEST 
                    arrPS[v1 + 1, 18] = t35.ToString().Trim();  // FGIS DBA NO
                    arrPS[v1 + 1, 19] = t36.ToString().Trim();  // FGIS DBA REAL/TEST 
                    arrPS[v1 + 1, 20] = t37.ToString().Trim();  // FGIS DBA NO
                }
            }
          
        }

       


        return ("0");
    }
    

    //string Ret4 = FGISlibPointer.GetSNFromRecv01(tDocumentid, DBOracle, tmpReadDBA, "ENGITTRS1011", DBOracle, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2");
    public string SendGMail( string DBOracle, string tmpReadDBA, string Wfile, string DataTypeF20, string mailtype, string sendaccount, string sendmessage )
    {
        string SDNBASecIP = "11201";

        if (DBOracle == "") DBOracle = "oracle";
        if (tmpReadDBA == "" ) 
            tmpReadDBA = TrsSecondIP("WebReadParam.txt", SDNBASecIP); // ReadParaTxt("WebReadParam.txt", DBA156);

        if (Wfile == "") Wfile = "FGIS.GMAILBOX";
        if (DataTypeF20 == "") DataTypeF20 = "FGISGMAILBOX";
        if (mailtype == "") mailtype = "MAIL"; // MAIL, MAILGROUP
        if (sendaccount == "") sendaccount = "FGIS.GMAILBOX";
        if (sendmessage == "") sendmessage = "FGIS Send Mail";

        string tdocumentid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tdocseq = "0000000001";
        string tsite = "LF";
        string tflag1 = "S";
        string tflag7 = "1";


        string t2 = "Insert into " + Wfile + " ( Documentid, docseq, flag1, flag4, flag5, flag6, flag7, flag20 ) Values "
                       + " ( '" + tdocumentid + "', '" + tdocseq + "', '" + tflag1 + "', '" + mailtype + "', "
                       + " '" + sendaccount + "', '" + sendmessage + "', '" + tflag7 + "', '" + DataTypeF20 + "' ) ";
        int v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                   

        return ( v4.ToString().Trim());
    }

    public string SendGMailV01(string MailType, string DBOracle, string tmpReadDBA, string Wfile, string DataTypeF20, string mailtype, string sendaccount, string sendmessage, string[,] arrMail)
    {
        string SDNBASecIP = "11201";

        if (DBOracle == "") DBOracle = "oracle";
        if (tmpReadDBA == "")
            tmpReadDBA = TrsSecondIP("WebReadParam.txt", SDNBASecIP); // ReadParaTxt("WebReadParam.txt", DBA156);

        if (Wfile == "") Wfile = "FGIS.GMAILBOX";
        if (DataTypeF20 == "") DataTypeF20 = "FGISGMAILBOX";
        if (mailtype == "") mailtype = "MAIL"; // MAIL, MAILGROUP
        if (sendaccount == "") sendaccount = "FGIS.GMAILBOX";
        if (sendmessage == "") sendmessage = MailType;

        sendmessage = MailType;

        string tdocumentid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tdocseq = "0000000001";
        string tsite = "LF";
        string tflag1 = "R";
        string tflag7 = "1";
        string tmpflag21 = arrMail[1, 1].ToString().Trim();
        string tmpflag22 = arrMail[1, 2].ToString().Trim();

        string tmpflag23 = arrMail[2, 1].ToString().Trim();
        string tmpflag24 = arrMail[2, 2].ToString().Trim();

        string tmpflag25 = arrMail[3, 1].ToString().Trim();
        string tmpflag26 = arrMail[3, 2].ToString().Trim();


        string t2 = "Insert into " + Wfile + " ( Documentid, docseq, flag1, flag4, flag5, flag6, flag7, flag20, flag21, flag22, flag23, flag24,  "
                       + " flag25, flag26, flag27, flag28, flag29, flag30, flag31, flag32, flag33, flag34, flag35, flag36, flag37, flag38, flag39, flag40,  "
                       + " flag41,flag42,flag43,flag44,flag45,flag46,flag47,flag48,flag49, flag50 )  Values "
                       + " ( '" + tdocumentid + "', '" + tdocseq + "', '" + tflag1 + "', '" + mailtype + "', "
                       + " '" + sendaccount + "', '" + sendmessage + "', '" + tflag7 + "', '" + DataTypeF20 + "',  "
                       + " '" + arrMail[1, 1].ToString().Trim() + "', '" + arrMail[1, 2].ToString().Trim() + "', "
                       + " '" + arrMail[2, 1].ToString().Trim() + "', '" + arrMail[2, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[3, 1].ToString().Trim() + "', '" + arrMail[3, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[4, 1].ToString().Trim() + "', '" + arrMail[4, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[5, 1].ToString().Trim() + "', '" + arrMail[5, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[6, 1].ToString().Trim() + "', '" + arrMail[6, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[7, 1].ToString().Trim() + "', '" + arrMail[7, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[8, 1].ToString().Trim() + "', '" + arrMail[8, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[9, 1].ToString().Trim() + "', '" + arrMail[9, 2].ToString().Trim() + "',  "
                       + " '" + arrMail[10,1].ToString().Trim() + "', '" + arrMail[10,2].ToString().Trim() + "',  "
                       + " '" + arrMail[11,1].ToString().Trim() + "', '" + arrMail[11,2].ToString().Trim() + "',  "
                       + " '" + arrMail[12,1].ToString().Trim() + "', '" + arrMail[12,2].ToString().Trim() + "',  "
                       + " '" + arrMail[13,1].ToString().Trim() + "', '" + arrMail[13,2].ToString().Trim() + "',  "
                       + " '" + arrMail[14,1].ToString().Trim() + "', '" + arrMail[14,2].ToString().Trim() + "',  "
                       + " '" + arrMail[15,1].ToString().Trim() + "', '" + arrMail[15,2].ToString().Trim() + "'   ) ";
        
        
        //string t22 = "Insert into " + Wfile + " ( Documentid, docseq, flag1, flag4, flag5, flag6, flag7, flag20 ) Values "
        //               + " ( '" + tdocumentid + "', '" + tdocseq + "', '" + tflag1 + "', '" + mailtype + "', "
        //               + " '" + sendaccount + "', '" + sendmessage + "', '" + tflag7 + "', '" + DataTypeF20 + "' ) ";

        int v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);


        return (v4.ToString().Trim());
    }

    public string Uploadlabel01( string DBOracle, string tmpReadDBA, string Updfile , string DBOracle1, string tmpWriDBA, string Wrifile , string tmpdate, string Funno, string UpdfileID )
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1=0, v2=-0, v3=0, v4=0, v5=0, DNCnt1=0, DNCnt2=0;
 
        
        DataSet DNdt1 = null, DNdt2 = null;

        if (UpdfileID == "")
        {
            if (Funno == "F3") t1 = " select distinct(F3 ) from " + Updfile + "  order  by F3 asc ";
            if (Funno == "F4") t1 = " select distinct(F4 )  data1 from '" + Updfile + "'   order  by F4 asc ";
            if (Funno == "F5") t1 = " select distinct(F5 )  data1 from '" + Updfile + "'   order  by F5 asc ";
        }
        else
        {
            if (Funno == "F3") t1 = " select distinct(F3 ) from " + Updfile + " where documentid = '" + UpdfileID + "' order  by F3 asc ";
            if (Funno == "F4") t1 = " select distinct(F4 )  data1 from " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F4 asc ";
            if (Funno == "F5") t1 = " select distinct(F5 )  data1 from " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F5 asc ";
        }
        DNdt1  = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());
 
        //int arrX = DNCnt1, arrY = 30;
        //string[,] arrDN = new string[arrX + 1, arrY + 1];
        //for (v2 = 0; v2 < arrX; v2++)
        //    for (v3 = 0; v3 < arrY; v3++)
        //        arrDN[arrX, arrY] = "";


        if (Funno == "F3")
        {

            // Get Last SEQID

            t3 = Wrifile;

            t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //  + " order by last_update_date  ";
            DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            if (DNCnt2 <= 0) t4 = "1000000000";
            else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();

            int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;
            string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
            int tDocseq = 10001;
            string tF1 = "NOKFOXM1005";
            string tF2 = "LABEL PARAM";
            string tF3 = "Param no"; int tF4 = 11001; string tF5 = "GROUP"; string tF6 = "01"; string tF7 = "Attrib SubSeq"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            int    tF8 = 1001; string tF9 = "Context"; string tF10 = "Country And Customer Rules"; string tflag1 = "1"; string trdate = DateTime.Now.ToString("yyyyMMdd");


            for (v2 = 0; v2 < DNCnt1; v2++)
            {
                // Get Last SEQID
                tF10 = DNdt1.Tables[0].Rows[v2]["F3"].ToString().Trim();
                t2 = "Insert into " + t3 + " ( Documentid, Docseq, DDATE,  F1, F2 , F3, F4,  F5, F6, F7, F8, F9, F10, FLAG1,  RDATE, SEQID ) Values "
                      + " ( '" + tdoc + "', '" + tDocseq.ToString().Trim() + "', '" + tddate + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "',  '" + tF4.ToString().Trim() + "',  "
                      + "   '" + tF5 + "', '" + tF6 + "', '" + tF7 + "', '" + tF8.ToString().Trim() + "', '" + tF9 + "', '" + tF10 + "',  '" + tflag1 + "',     "
                      + "   '" + trdate + "',  '" + tseqid.ToString().Trim() + "' ) ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                
                if (v4 >= 1)
                {
                    tDocseq++;
                    tF4++;
                    tF8++;
                    tseqid++;

                }

            } 

        }

        if (Funno == "F4")
        {

            // Get Last SEQID

            t3 = Wrifile;
            t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //  + " order by last_update_date  ";
            DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            if (DNCnt2 <= 0) t4 = "1000000000";
            else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();

            int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;
            string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
            int tDocseq = 10001;
            string tF1 = "NOKFOXM1005";
            string tF2 = "LABEL PARAM";
            string tF3 = "Param no"; int tF4 = 12001; string tF5 = "RULE TYPE"; string tF6 = "02"; string tF7 = "Attrib SubSeq"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            int tF8 = 2001; string tF9 = "Context"; string tF10 = "Country And Customer Rules"; string tflag1 = "1"; string trdate = DateTime.Now.ToString("yyyyMMdd");


            for (v2 = 0; v2 < DNCnt1; v2++)
            {
                // Get Last SEQID
                tF10 = DNdt1.Tables[0].Rows[v2]["data1"].ToString().Trim();
                t2 = "Insert into " + t3 + " ( Documentid, Docseq, DDATE,  F1, F2 , F3, F4,  F5, F6, F7, F8, F9, F10, FLAG1,  RDATE, SEQID ) Values "
                      + " ( '" + tdoc + "', '" + tDocseq.ToString().Trim() + "', '" + tddate + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "',  '" + tF4.ToString().Trim() + "',  "
                      + "   '" + tF5 + "', '" + tF6 + "', '" + tF7 + "', '" + tF8.ToString().Trim() + "', '" + tF9 + "', '" + tF10 + "',  '" + tflag1 + "',     "
                      + "   '" + trdate + "',  '" + tseqid.ToString().Trim() + "' ) ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                if (v4 >= 1)
                {
                    tDocseq++;
                    tF4++;
                    tF8++;
                    tseqid++;

                }

            }

        }

        if (Funno == "F5")
        {

            // Get Last SEQID

            t3 = Wrifile;

            t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //  + " order by last_update_date  ";
            DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            if (DNCnt2 <= 0) t4 = "1000000000";
            else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();

            int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;
            string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
            int tDocseq = 30001;
            string tF1 = "NOKFOXM1005";
            string tF2 = "LABEL PARAM";
            string tF3 = "Param no"; int tF4 = 13001; string tF5 = "key1"; string tF6 = "03"; string tF7 = "Attrib SubSeq"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            int tF8 = 3001; string tF9 = "Context"; string tF10 = "Country And Customer Rules"; string tflag1 = "1"; string trdate = DateTime.Now.ToString("yyyyMMdd");


            for (v2 = 0; v2 < DNCnt1; v2++)
            {
                // Get Last SEQID
                tF10 = DNdt1.Tables[0].Rows[v2]["data1"].ToString().Trim();
                t2 = "Insert into " + t3 + " ( Documentid, Docseq, DDATE,  F1, F2 , F3, F4,  F5, F6, F7, F8, F9, F10, FLAG1,  RDATE, SEQID ) Values "
                      + " ( '" + tdoc + "', '" + tDocseq.ToString().Trim() + "', '" + tddate + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "',  '" + tF4.ToString().Trim() + "',  "
                      + "   '" + tF5 + "', '" + tF6 + "', '" + tF7 + "', '" + tF8.ToString().Trim() + "', '" + tF9 + "', '" + tF10 + "',  '" + tflag1 + "',     "
                      + "   '" + trdate + "',  '" + tseqid.ToString().Trim() + "' ) ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                if (v4 >= 1)
                {
                    tDocseq++;
                    tF4++;
                    tF8++;
                    tseqid++;

                }

            }

        }


        return( "" );

    }


    // LBP1TOP2
    public string Uploadlabel02(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "LBP1TOP2") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBP1TOP2") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "LBP1TOP2") t2 = " select *  from  " + NokFoxM01Ref + " where  F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBP1TOP2") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F3, F4 asc ";
        }
        // if (Funno == "F5") t1 = " select distinct(F5 )  data1 from labelmanagement.UPLOADTMP order  by F5 asc ";
        //  + " order by last_update_date  ";
        DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
        DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt2 <= 0) return (v1.ToString());

        int arrX = DNCnt2, arrY = 30;
        string[,] arrDN = new string[arrX + 1, arrY + 1];
        for (v2 = 0; v2 < arrX; v2++)
            for (v3 = 0; v3 < arrY; v3++)
                arrDN[v2, v3] = "";


        for (v1 = 0; v1 < arrX; v1++)
        {
            arrDN[v1+1, 04] = DNdt2.Tables[0].Rows[v1]["F4"].ToString().Trim();  // No
            arrDN[v1+1, 05] = DNdt2.Tables[0].Rows[v1]["F5"].ToString().Trim();  // Save Group
            arrDN[v1+1, 06] = DNdt2.Tables[0].Rows[v1]["F6"].ToString().Trim();  // 01
            arrDN[v1+1, 10] = DNdt2.Tables[0].Rows[v1]["F10"].ToString().Trim();  // Context
        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3
        if (UpdfileID == "")    t1 = " select *  from " + Updfile + "  order  by F3, F4 asc ";
        else                    t1 = " select *  from " + Updfile + "  where documentid = '" + UpdfileID + "'order  by F3, F4 asc ";
       
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if ( DNCnt1 <= 0) return ("");

        if (Funno == "LBP1TOP2")
        {

            // Get Last SEQID

            t3 = Wrifile;

            //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            //DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            //if (DNCnt2 <= 0) t4 = "1000000000";
            //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            //int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;
            string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
            string tDATABOMID = "LBP1TOP2";
            string tMODEL = "NOKFOXM1005";
            string tSITE = "ALL";
            string tITEM = "";
            string tSEQ = "02";
            string tDUEDATE = "29991231";

            int    tDocseq = 10001;
            string tSite = "ALL";
            string tF1 = "Parent";
            string tF2 = "";
            string tF3 = "Seqno"; int tF4 = 101; string tF5 = "SubSSeqno"; int tF6 = 101; string tF7 = "Child  Attrib"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            string tF8 = ""; string tF9 = "Child No"; string tF10 = ""; string tF11 = "NOKFOXM1005"; string tF12 = "";  string trdate = DateTime.Now.ToString("yyyyMMdd");
            string ttmpF3 = "";
            string ttmpF4 = "";  

            ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
            ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
            string updF3  = ""; string updF4 = "";


            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                 
                 updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                 updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                 tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                 tF8   = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                 if ((ttmpF3 == updF3) && (ttmpF4 == updF4)) v2++;
                 else
                 {
                     tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();

                     t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6, FLAG7, FLAG8,  "
                        + "  FLAG9, FLAG10,FLAG11, FLAG12, CSTATUS,  UPTIME ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ + "', "
                        + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4.ToString().Trim() + "', '" + tF5 + "', "
                        + " '" + tF6.ToString().Trim() + "', '" + tF7 + "', '" + tF8 + "', '" + tF9 + "','" + tF10 + "','" + tF11 + "','" + tF12 + "', '1','" + trdate + "') ";
                     v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                     if (v4 >= 1)
                     {
                         //tDocseq++;
                         tF4++;
                         //tF8++;
                         //tseqid++;

                     }
                               
                 }    
             
                 ttmpF3 = updF3;
                 ttmpF4 = updF4;
                                  

            }  // End for 

        } //   if (Funno == "LBP1TOP2")


        // Update data

        v2 = 0;
        t2 = NokFoxM01Ref; 
        t1 = " select *  from " + NokFoxM01Ref + " where F6 = '01' "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();   // number
                t4 = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // name

                t2 = "update " + Wrifile + "  set FLAG2 = '" + t3 + "' where ITEM =  '" + t4 + "' ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                v2 = v2 + v4;
            }


        }


        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from  " + NokFoxM01Ref + " where F6 = '02' "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();   // number
                t4 = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // name

                t2 = "update " + Wrifile + "  set FLAG10 = '" + t3 + "' where FLAG8 =  '" + t4 + "' ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                v2 = v2 + v4;
            }


        }


        return ("");

    }  // End 102


     // LBP1TOP2
    public string Uploadlabel03(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F3, F4 asc ";
        }
        
        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
      
        DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
        DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt2 <= 0) return (v1.ToString());

        int arrX = DNCnt2, arrY = 30;
        string[,] arrDN = new string[arrX + 1, arrY + 1];
        for (v2 = 0; v2 < arrX; v2++)
            for (v3 = 0; v3 < arrY; v3++)
                arrDN[v2, v3] = "";


        for (v1 = 0; v1 < arrX; v1++)
        {
            arrDN[v1 + 1, 04] = DNdt2.Tables[0].Rows[v1]["F4"].ToString().Trim();  // No
            arrDN[v1 + 1, 05] = DNdt2.Tables[0].Rows[v1]["F5"].ToString().Trim();  // Save Group
            arrDN[v1 + 1, 06] = DNdt2.Tables[0].Rows[v1]["F6"].ToString().Trim();  // 01
            arrDN[v1 + 1, 10] = DNdt2.Tables[0].Rows[v1]["F10"].ToString().Trim();  // Context
        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        if (UpdfileID == "") t1 = " select *  from " + Updfile + "  order  by F3, F4, F5 asc ";
        else t1 = " select *  from " + Updfile + "  where documentid = '" + UpdfileID + "'order  by F3, F4, F5 asc ";
        // t1 = " select *  from " + Updfile + "  order  by F3, F4, F5 asc ";
        
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 <= 0) return ("");

        if (Funno == "LBBOM01")
        {

            // Get Last SEQID

            t3 = Wrifile;

            //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            //DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            //if (DNCnt2 <= 0) t4 = "1000000000";
            //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            //int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;
            string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
            string tDATABOMID = "LBBOM01";
            string tMODEL = "NOKFOXM1005";
            string tSITE = "ALL";
            string tITEM = "";
            int    tSEQ = 10000;
            string tDUEDATE = "29991231";

            int tDocseq = 10001;
            string tSite = "ALL";
            string tF1 = "Parent";
            string tF2 = "";
            string tF3 = "Seqno"; string tF4 = ""; string tF5 = "SubSSeqno"; string tF6 = "01"; string tF7 = "Child  Attrib"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            string tF8 = ""; string tF9 = "Child No"; string tF10 = ""; string tF11 = "NOKFOXM1005"; string tF12 = ""; string trdate = DateTime.Now.ToString("yyyyMMdd");
            string ttmpF3 = "", ttmpF4 = "", ttmpF5="";

            ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
            ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
            string updF3 = "",  updF4 = "",  updF5 = "";
            int var1=1, var2=0, var3=0;

            for (v1 = 0; v1 < DNCnt1; v1++)
            {

                updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();
                updF5 = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();

                //tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                //tF8 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                if ((ttmpF3 == updF3) && (ttmpF4 == updF4) && ( ttmpF3 != "" )  )
                {
                    if (ttmpF5 != updF5) // 
                    {
                        tSEQ = tSEQ + 1; tF1 = "Parent"; tF4 = "Child"; tF6 = "03"; tF7 = updF5;
                        t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6, FLAG7, FLAG8,  "
                        + "  FLAG9, FLAG10,FLAG11, FLAG12, CSTATUS,  UPTIME ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ + "', "
                        + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4.ToString().Trim() + "', '" + tF5 + "', "
                        + " '" + tF6.ToString().Trim() + "', '" + tF7 + "', '" + tF8 + "', '" + tF9 + "','" + tF10 + "','" + tF11 + "','" + tF12 + "', '1','" + trdate + "') ";
                        v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);                           
                    }
                    
                }   
                else
                {
                    var1 = 2;
                    tMODEL = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
                    tITEM  = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // Result Code
                    tF2    = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No

                    tF7 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // Rule No 
                    // Attr1
                    tSEQ = tSEQ+1; tF1 = "Parent"; tF4 = "Child"; tF6 = "01"; tF7 = updF3; 
                    
                    t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,  "
                       + "  FLAG7 ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4.ToString().Trim() + "', '" + tF5 + "', "
                       + " '" + tF6.ToString().Trim() + "', '" + tF7 + "' ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                    // Arrr 2
                    tSEQ = tSEQ+1; tF1 = "Parent"; tF4 = "Child"; tF6 = "02"; tF7 = updF4;   
                       t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6, FLAG7, FLAG8,  "
                       + "  FLAG9, FLAG10,FLAG11, FLAG12, CSTATUS,  UPTIME ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4.ToString().Trim() + "', '" + tF5 + "', "
                       + " '" + tF6.ToString().Trim() + "', '" + tF7 + "', '" + tF8 + "', '" + tF9 + "','" + tF10 + "','" + tF11 + "','" + tF12 + "', '1','" + trdate + "') ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                     // Arrr 3
                    tSEQ = tSEQ+1; tF1 = "Parent"; tF4 = "Child"; tF6 = "03"; tF7 = updF5;   
                       t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6, FLAG7, FLAG8,  "
                       + "  FLAG9, FLAG10,FLAG11, FLAG12, CSTATUS,  UPTIME ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4.ToString().Trim() + "', '" + tF5 + "', "
                       + " '" + tF6.ToString().Trim() + "', '" + tF7 + "', '" + tF8 + "', '" + tF9 + "','" + tF10 + "','" + tF11 + "','" + tF12 + "', '1','" + trdate + "') ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);                    

                }

              


                ttmpF3 = updF3;
                ttmpF4 = updF4;
                ttmpF5 = updF5;

            }  // End for 

        } //   if (Funno == "LBP1TOP2")


        // Update data

        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + NokFoxM01Ref + " where F6 = '01' "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();   // number
                t4 = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // name

                t2 = "update " + Wrifile + "  set FLAG2 = '" + t3 + "' where ITEM =  '" + t4 + "' ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                v2 = v2 + v4;
            }


        }


        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + NokFoxM01Ref + " where F6 = '02' "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();   // number
                t4 = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // name

                t2 = "update " + Wrifile + "  set FLAG10 = '" + t3 + "' where FLAG8 =  '" + t4 + "' ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                v2 = v2 + v4;
            }


        }


        return ("");

    } //   103


    // 103-1
    public string Uploadlabel03V01(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, bomno=10000;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "", BomStr="";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2, F3 asc ";
        }

        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";

        DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
        DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt2 <= 0) return (v1.ToString());

        int arrX = DNCnt2, arrY = 30;
        string[,] arrDN = new string[arrX + 1, arrY + 1];
        for (v2 = 0; v2 < arrX; v2++)
            for (v3 = 0; v3 < arrY; v3++)
                arrDN[v2, v3] = "";


        for (v1 = 0; v1 < arrX; v1++)
        {
            arrDN[v1 + 1, 01] = DNdt2.Tables[0].Rows[v1]["F4"].ToString().Trim();  // No
            arrDN[v1 + 1, 02] = DNdt2.Tables[0].Rows[v1]["F5"].ToString().Trim();  // Save Group
            arrDN[v1 + 1, 03] = DNdt2.Tables[0].Rows[v1]["F6"].ToString().Trim();  // 01
            arrDN[v1 + 1, 04] = DNdt2.Tables[0].Rows[v1]["F10"].ToString().Trim();  // Context
        }


        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());

        int arrBomX = DNCnt1, arrBomY = 30;
        string[,] arrBom1 = new string[arrBomX + 1, arrBomY + 1];
        for (v2 = 0; v2 < arrBomX; v2++)
            for (v3 = 0; v3 < arrBomY; v3++)
                arrBom1[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  // 
            arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
            arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
            arrBom1[v1 + 1, 07] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
            arrBom1[v1 + 1, 08] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
            arrBom1[v1 + 1, 09] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //   
            arrBom1[v1 + 1, 10] = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom1[v1 + 1, 11] = DNdt1.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 
            arrBom1[v1 + 1, 14] = DNdt1.Tables[0].Rows[v1]["F11"].ToString().Trim();  // 
            arrBom1[v1 + 1, 15] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 16] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 


            arrBom1[v1 + 1, 19] = DNdt1.Tables[0].Rows[v1]["rdate"].ToString().Trim();   // 14
            arrBom1[v1 + 1, 20] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 15 
            arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 05].ToString().Trim() + arrBom1[v1 + 1, 06].ToString().Trim() + arrBom1[v1 + 1, 07].ToString().Trim();  // 16  Setup 10 Set
            arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 08].ToString().Trim() + arrBom1[v1 + 1, 09].ToString().Trim() + arrBom1[v1 + 1, 10].ToString().Trim();
            arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 11].ToString().Trim() + arrBom1[v1 + 1, 12].ToString().Trim() + arrBom1[v1 + 1, 13].ToString().Trim();
            arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 14].ToString().Trim() + arrBom1[v1 + 1, 15].ToString().Trim() + arrBom1[v1 + 1, 16].ToString().Trim();
            BomStr = arrBom1[v1 + 1, 21].ToString().Trim();
           
        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tDATABOMID = "LBBOM01";
        string tMODEL = "10000";
        string tSITE = "ALL";
        string tITEM = "";
        int tSEQ = 100;
        string tDUEDATE = "29991231";
        if (Funno == "LBBOM01")
        {

            // Get Last SEQID

            t3 = Wrifile;

            //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            //DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            //if (DNCnt2 <= 0) t4 = "1000000000";
            //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            //int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;
           
            int tDocseq = 10000;
            string tSite = "ALL";
            string tF1 = "Parent";
            string tF2 = "";
            string tF3 = "Seqno"; string tF4 = ""; string tF5 = "SubSSeqno"; string tF6 = "01"; string tF7 = "Child  Attrib"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            string tF8 = ""; string tF9 = "Child No"; string tF10 = ""; string tF11 = "NOKFOXM1005"; string tF12 = ""; string trdate = DateTime.Now.ToString("yyyyMMdd");
            string ttmpF3 = "", ttmpF4 = "", ttmpF5 = "", ttmpF2="";

            ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
            ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
            string updF3 = "", updF4 = "", updF5 = "", updF2="";
            int var1 = 1, var2 = 0, var3 = 0;
            string tparent = "", tchild = "";

            for (v1 = 0; v1 < DNCnt1; v1++)
            {

                updF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();
                updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                //tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                //tF8 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                //tMODEL = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
                //tITEM = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // Result Code
                tF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
                tSEQ = 100;


                if ( ( updF2 == ttmpF2 ) && ( updF3 == ttmpF3 ) )
                {
                    var1 = var1 + 1;
                }
                else
                {
                    bomno = bomno + 1;
                    tF7 =  arrBom1[v1 + 1, 04].ToString().Trim();  // DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  // Rule No 
                    // Attr1
                    tF1 = "Parent"; tF4 = "Child"; tF6 = "01"; tF7 = updF3;
                   
                    tparent =  arrBom1[v1 + 1, 05].ToString().Trim();  
                    tchild  =  arrBom1[v1 + 1, 05].ToString().Trim();

                    tITEM = bomno.ToString().Trim();  // parent 
                    tMODEL = bomno.ToString().Trim();

                    tF2 = "";
                    tF3 = ""; // Parent
                    tF4 = "Child";
                    tF5 = ""; // child no
                    BomStr = "";
                    for (v2 = 5; v2 < 13; v2++)
                    {
                        tSEQ = tSEQ + 1;
                        tparent = arrBom1[v1 + 1, 05].ToString().Trim();  // parent
                        tITEM   = arrBom1[v1 + 1, 05].ToString().Trim();  // bomno
                        tchild  = arrBom1[v1 + 1, v2].ToString().Trim();  // 6,7,8,9,10,11,12 : Child
                        tF7     = arrBom1[v1 + 1, v2].ToString().Trim();
                        BomStr  = arrBom1[v1 + 1, 21].ToString().Trim();
                        if (v2 == 5) tF6 = "01";
                        else if (v2 == 6) tF6 = "02";
                        else tF6 = "03";

                        if (tchild == "")
                            v3 = v3;
                        else
                        {
                            t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12  "
                            + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                            + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                            + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL + "', '" + trdate + "', '" + BomStr + "' ) ";
                            v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);                            
                        }
                    } 
                    
                 }
                
                ttmpF2 = updF2;
                ttmpF3 = updF3;
                ttmpF4 = updF4;
               
            }  // End for 

        } //   if (Funno == "LBP1TOP2")


        // Update data

        string tmpSEQ = "";
        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + Wrifile + " where FLAG5 is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }

              

        return ("");

    } //   103-1


    // V01-V11 Add NokiaIndex, Rule Result
    public string Uploadlabel03V11(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, bomno = 10000;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "", BomStr = "", NokRuleNo = ""; 


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "LBBOM02") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM02") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "LBBOM02") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM02") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2, F3 asc ";
        }

        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";

        DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
        DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt2 <= 0) return (v1.ToString());

        int arrX = DNCnt2, arrY = 30;
        string[,] arrDN = new string[arrX + 1, arrY + 1];
        for (v2 = 0; v2 < arrX; v2++)
            for (v3 = 0; v3 < arrY; v3++)
                arrDN[v2, v3] = "";


        for (v1 = 0; v1 < arrX; v1++)
        {
            arrDN[v1 + 1, 01] = DNdt2.Tables[0].Rows[v1]["F4"].ToString().Trim();  // No
            arrDN[v1 + 1, 02] = DNdt2.Tables[0].Rows[v1]["F5"].ToString().Trim();  // Save Group
            arrDN[v1 + 1, 03] = DNdt2.Tables[0].Rows[v1]["F6"].ToString().Trim();  // 01
            arrDN[v1 + 1, 04] = DNdt2.Tables[0].Rows[v1]["F10"].ToString().Trim();  // Context
        }


        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());

        int arrBomX = DNCnt1, arrBomY = 50;
        string[,] arrBom1 = new string[arrBomX + 1, arrBomY + 1];
        for (v2 = 0; v2 < arrBomX; v2++)
            for (v3 = 0; v3 < arrBomY; v3++)
                arrBom1[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrBom1[v1 + 1, 41] = DNdt1.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom1[v1 + 1, 42] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  // 
            arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
            arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
            arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
            arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
            arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //   
            arrBom1[v1 + 1, 07] = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom1[v1 + 1, 08] = DNdt1.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
            arrBom1[v1 + 1, 09] = DNdt1.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom1[v1 + 1, 10] = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 
            arrBom1[v1 + 1, 11] = DNdt1.Tables[0].Rows[v1]["F11"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 


            arrBom1[v1 + 1, 43] = DNdt1.Tables[0].Rows[v1]["rdate"].ToString().Trim();   // 14
            arrBom1[v1 + 1, 44] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 15 
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 02].ToString().Trim() + arrBom1[v1 + 1, 03].ToString().Trim() + arrBom1[v1 + 1, 05].ToString().Trim();  // 16  Setup 10 Set
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 08].ToString().Trim() + arrBom1[v1 + 1, 09].ToString().Trim() + arrBom1[v1 + 1, 10].ToString().Trim();
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 11].ToString().Trim() + arrBom1[v1 + 1, 12].ToString().Trim() + arrBom1[v1 + 1, 13].ToString().Trim();
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 14].ToString().Trim() + arrBom1[v1 + 1, 15].ToString().Trim() + arrBom1[v1 + 1, 16].ToString().Trim();
            //BomStr = arrBom1[v1 + 1, 21].ToString().Trim();

        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tDATABOMID = "LBBOM01";
        string tMODEL = "";
        string tSITE = "ALL";
        string tITEM = "";
        int tSEQ = 100;
        int CurSEQNO = 10000;
        string tDUEDATE = "29991231";
        if (Funno != "LBBOM02")  return("");
       
            // Get Last SEQID

            t3 = Wrifile;

            //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            //DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            //if (DNCnt2 <= 0) t4 = "1000000000";
            //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            //int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;

            int tDocseq = 10000;
            string tSite = "ALL";
            string tF1 = "Parent";
            string tF2 = "";
            string tF3 = "Child no"; string tF4 = ""; string tF5 = "Attrib"; string tF6 = "01"; string tF7 = ""; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            string tF8 = "Context"; string tF9 = ""; string tF10 = ""; string tF11 = ""; string tF12 = ""; string trdate = DateTime.Now.ToString("yyyyMMdd");
            string ttmpF3 = "", ttmpF4 = "", ttmpF5 = "", ttmpF2 = "";

            ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
            ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
            string updF3 = "", updF4 = "", updF5 = "", updF2 = "";
            int var1 = 1, var2 = 0, var3 = 0;
            string tparent = "", tchild = "", CurrRuleTYPE = "";

            var2 = 1; // First 
            string CurRuleNO = "", PreRuleno = ""; 
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                CurRuleNO = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();
                updF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();
                updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                //tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                //tF8 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                //tMODEL = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
                //tITEM = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // Result Code
                tF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
                tITEM = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();


                if (CurRuleNO != PreRuleno)  // Write head
                {
                    var1=101;
                    tSEQ = 101;
                    CurSEQNO++;

                    tMODEL = "";
                    tF6 = (var1.ToString()).Substring(1, 2); //  "01";
                    tF7 = arrBom1[v1 + 1, 03].ToString().Trim(); // group
                    t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                           + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                           + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                           + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "' , '" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "') ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                    var1++;
                    tSEQ++;
                    tF6 = (var1.ToString()).Substring(1, 2); //"02";
                    tF7 = arrBom1[v1 + 1, 04].ToString().Trim(); // rule type
                    tMODEL = tF7;
                    t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                           + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                           + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                           + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "','" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "') ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                    var1++;
                    tSEQ++;
                    tF6 = (var1.ToString()).Substring(1, 2); // "03";
                    tF7 = arrBom1[v1 + 1, 05].ToString().Trim(); // rule type
                    tF9 = arrBom1[v1 + 1, 06].ToString().Trim(); // rule type
                    t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                           + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                           + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                           + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "','" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "' ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                }
                else
                {
                    //var1++;
                    tSEQ++;
                    tF6 = (var1.ToString()).Substring(1, 2); // "03";
                    tF7 = arrBom1[v1 + 1, 05].ToString().Trim(); // rule type
                    tF9 = arrBom1[v1 + 1, 06].ToString().Trim(); // rule type
                    t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                           + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                           + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                           + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "','" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "' ) ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                }
                PreRuleno = CurRuleNO;
               

            }  // End for 


            return ("");

        // Update data

        string tmpSEQ = "";
        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + Wrifile + " where FLAG5 is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                //tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }



        return ("");

    } //   103V11


    // 107 Update all Key and No
    public string Uploadlabel07(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, bomno = 10000;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "", BomStr = "", NokRuleNo = "";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;


        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from labelmgt.databom02 where model is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                //tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                //tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    //t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }


       

        if (UpdfileID == "")
        {
            if (Funno == "LBBOM02") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM02") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "LBBOM02") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM02") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2, F3 asc ";
        }

        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";

        DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
        DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt2 <= 0) return (v1.ToString());

        int arrX = DNCnt2, arrY = 30;
        string[,] arrDN = new string[arrX + 1, arrY + 1];
        for (v2 = 0; v2 < arrX; v2++)
            for (v3 = 0; v3 < arrY; v3++)
                arrDN[v2, v3] = "";


        for (v1 = 0; v1 < arrX; v1++)
        {
            arrDN[v1 + 1, 01] = DNdt2.Tables[0].Rows[v1]["F4"].ToString().Trim();  // No
            arrDN[v1 + 1, 02] = DNdt2.Tables[0].Rows[v1]["F5"].ToString().Trim();  // Save Group
            arrDN[v1 + 1, 03] = DNdt2.Tables[0].Rows[v1]["F6"].ToString().Trim();  // 01
            arrDN[v1 + 1, 04] = DNdt2.Tables[0].Rows[v1]["F10"].ToString().Trim();  // Context
        }


        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());

        int arrBomX = DNCnt1, arrBomY = 50;
        string[,] arrBom1 = new string[arrBomX + 1, arrBomY + 1];
        for (v2 = 0; v2 < arrBomX; v2++)
            for (v3 = 0; v3 < arrBomY; v3++)
                arrBom1[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrBom1[v1 + 1, 41] = DNdt1.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom1[v1 + 1, 42] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  // 
            arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
            arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
            arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
            arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
            arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //   
            arrBom1[v1 + 1, 07] = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom1[v1 + 1, 08] = DNdt1.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
            arrBom1[v1 + 1, 09] = DNdt1.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom1[v1 + 1, 10] = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 
            arrBom1[v1 + 1, 11] = DNdt1.Tables[0].Rows[v1]["F11"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 


            arrBom1[v1 + 1, 43] = DNdt1.Tables[0].Rows[v1]["rdate"].ToString().Trim();   // 14
            arrBom1[v1 + 1, 44] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 15 
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 02].ToString().Trim() + arrBom1[v1 + 1, 03].ToString().Trim() + arrBom1[v1 + 1, 05].ToString().Trim();  // 16  Setup 10 Set
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 08].ToString().Trim() + arrBom1[v1 + 1, 09].ToString().Trim() + arrBom1[v1 + 1, 10].ToString().Trim();
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 11].ToString().Trim() + arrBom1[v1 + 1, 12].ToString().Trim() + arrBom1[v1 + 1, 13].ToString().Trim();
            //arrBom1[v1 + 1, 21] = arrBom1[v1 + 1, 21].ToString().Trim() + arrBom1[v1 + 1, 14].ToString().Trim() + arrBom1[v1 + 1, 15].ToString().Trim() + arrBom1[v1 + 1, 16].ToString().Trim();
            //BomStr = arrBom1[v1 + 1, 21].ToString().Trim();

        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tDATABOMID = "LBBOM01";
        string tMODEL = "";
        string tSITE = "ALL";
        string tITEM = "";
        int tSEQ = 100;
        int CurSEQNO = 10000;
        string tDUEDATE = "29991231";
        if (Funno != "LBBOM02") return ("");

        // Get Last SEQID

        t3 = Wrifile;

        //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
        //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        //DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        //if (DNCnt2 <= 0) t4 = "1000000000";
        //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
        //int tseqid = Convert.ToInt32(t4) + 1;


        t3 = Wrifile;

        int tDocseq = 10000;
        string tSite = "ALL";
        string tF1 = "Parent";
        string tF2 = "";
        string tF3 = "Child no"; string tF4 = ""; string tF5 = "Attrib"; string tF6 = "01"; string tF7 = ""; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
        string tF8 = "Context"; string tF9 = ""; string tF10 = ""; string tF11 = ""; string tF12 = ""; string trdate = DateTime.Now.ToString("yyyyMMdd");
        string ttmpF3 = "", ttmpF4 = "", ttmpF5 = "", ttmpF2 = "";

        ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
        ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
        string updF3 = "", updF4 = "", updF5 = "", updF2 = "";
        int var1 = 1, var2 = 0, var3 = 0;
        string tparent = "", tchild = "";

        var2 = 1; // First 
        string CurRuleNO = "", PreRuleno = "";
        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            CurRuleNO = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();
            updF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();
            updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
            updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

            //tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
            //tF8 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

            //tMODEL = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
            //tITEM = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // Result Code
            tF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
            tITEM = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();


            if (CurRuleNO != PreRuleno)  // Write head
            {
                var1 = 101;
                tSEQ = 101;
                CurSEQNO++;

                tF6 = (var1.ToString()).Substring(1, 2); //  "01";
                tF7 = arrBom1[v1 + 1, 03].ToString().Trim(); // group
                t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                       + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                       + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "' , '" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "') ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                var1++;
                tSEQ++;
                tF6 = (var1.ToString()).Substring(1, 2); //"02";
                tF7 = arrBom1[v1 + 1, 04].ToString().Trim(); // rule type
                t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                       + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                       + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "','" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "') ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                var1++;
                tSEQ++;
                tF6 = (var1.ToString()).Substring(1, 2); // "03";
                tF7 = arrBom1[v1 + 1, 05].ToString().Trim(); // rule type
                tF9 = arrBom1[v1 + 1, 06].ToString().Trim(); // rule type
                t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                       + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                       + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "','" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "' ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
            }
            else
            {
                //var1++;
                tSEQ++;
                tF6 = (var1.ToString()).Substring(1, 2); // "03";
                tF7 = arrBom1[v1 + 1, 05].ToString().Trim(); // rule type
                tF9 = arrBom1[v1 + 1, 06].ToString().Trim(); // rule type
                t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12, FLAG8, FLAG9, FLAG10  "
                       + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                       + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                       + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL.ToString().Trim() + "', '" + trdate + "', '" + BomStr + "','" + tF8 + "', '" + tF9 + "', '" + CurSEQNO.ToString().Trim() + "' ) ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

            }
            PreRuleno = CurRuleNO;


        }  // End for 


        return ("");

        // Update data

        string tmpSEQ = "";
        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + Wrifile + " where FLAG5 is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                //tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }



        return ("");

    } //   106
    // 104  write stand bom unique
    public string Uploadlabel04(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, v6=0, DNCnt1 = 0, DNCnt2 = 0, bomno = 10000;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "", BomStr = "", tmpBomStr="";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "NOKFOXM1101") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "NOKFOXM1101") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "NOKFOXM1101") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "NOKFOXM1101") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2, F3 asc ";
        }

        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";

        DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
        DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt2 <= 0) return (v1.ToString());

        int arrX = DNCnt2, arrY = 30;
        string[,] arrDN = new string[arrX + 1, arrY + 1];
        for (v2 = 0; v2 < arrX; v2++)
            for (v3 = 0; v3 < arrY; v3++)
                arrDN[v2, v3] = "";


        for (v1 = 0; v1 < arrX; v1++)
        {
            arrDN[v1 + 1, 01] = DNdt2.Tables[0].Rows[v1]["F4"].ToString().Trim();  // No
            arrDN[v1 + 1, 02] = DNdt2.Tables[0].Rows[v1]["F5"].ToString().Trim();  // Save Group
            arrDN[v1 + 1, 03] = DNdt2.Tables[0].Rows[v1]["F6"].ToString().Trim();  // 01
            arrDN[v1 + 1, 04] = DNdt2.Tables[0].Rows[v1]["F10"].ToString().Trim();  // Context
        }


        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());

        int arrBomX = DNCnt1, arrBomY = 50;
        string[,] arrBom1 = new string[arrBomX + 1, arrBomY + 1];
        for (v2 = 0; v2 < arrBomX; v2++)
            for (v3 = 0; v3 < arrBomY; v3++)
                arrBom1[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrBom1[v1 + 1, 41] = DNdt1.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
            arrBom1[v1 + 1, 42] = DNdt1.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom1[v1 + 1, 33] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom1[v1 + 1, 44] = DNdt1.Tables[0].Rows[v1]["rdate"].ToString().Trim();   // 14
            arrBom1[v1 + 1, 45] = DNdt1.Tables[0].Rows[v1]["SEQID"].ToString().Trim();  // 15 


            arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  Label Management
            arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
            arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
            arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  rule type
            arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
            arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  key1 
            arrBom1[v1 + 1, 07] = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  //  key value
            arrBom1[v1 + 1, 08] = DNdt1.Tables[0].Rows[v1]["F8"].ToString().Trim();  //  
            arrBom1[v1 + 1, 09] = DNdt1.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom1[v1 + 1, 10] = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 
            arrBom1[v1 + 1, 11] = DNdt1.Tables[0].Rows[v1]["F11"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 
            arrBom1[v1 + 1, 14] = DNdt1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // 
            arrBom1[v1 + 1, 15] = DNdt1.Tables[0].Rows[v1]["F15"].ToString().Trim();  // 
            arrBom1[v1 + 1, 16] = DNdt1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // 
            arrBom1[v1 + 1, 17] = DNdt1.Tables[0].Rows[v1]["F17"].ToString().Trim();  // 
            arrBom1[v1 + 1, 18] = DNdt1.Tables[0].Rows[v1]["F18"].ToString().Trim();  // 
            arrBom1[v1 + 1, 19] = DNdt1.Tables[0].Rows[v1]["F19"].ToString().Trim();  // 
            arrBom1[v1 + 1, 20] = DNdt1.Tables[0].Rows[v1]["F20"].ToString().Trim();  // 
            arrBom1[v1 + 1, 21] = DNdt1.Tables[0].Rows[v1]["F21"].ToString().Trim();  // 
            arrBom1[v1 + 1, 22] = DNdt1.Tables[0].Rows[v1]["F22"].ToString().Trim();  // 
            arrBom1[v1 + 1, 23] = DNdt1.Tables[0].Rows[v1]["F23"].ToString().Trim();  // 
            arrBom1[v1 + 1, 24] = DNdt1.Tables[0].Rows[v1]["F24"].ToString().Trim();  // 
            arrBom1[v1 + 1, 25] = DNdt1.Tables[0].Rows[v1]["F25"].ToString().Trim();  // 
            arrBom1[v1 + 1, 26] = DNdt1.Tables[0].Rows[v1]["F26"].ToString().Trim();  // 


            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 04].ToString().Trim();  // 16  Setup 10 Set
            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 49].ToString().Trim() + arrBom1[v1 + 1, 05].ToString().Trim() + arrBom1[v1 + 1, 06].ToString().Trim() + arrBom1[v1 + 1, 07].ToString().Trim();
            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 49].ToString().Trim() + arrBom1[v1 + 1, 08].ToString().Trim() + arrBom1[v1 + 1, 09].ToString().Trim();
       }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tDATABOMID = "LBBOM01";
        string tMODEL = "10000";
        string tSITE = "ALL";
        string tITEM = "";
        int tSEQ = 10000;
        string tDUEDATE = "29991231";
        if (Funno != "NOKFOXM1101") return ("");
        

            // Get Last SEQID

            t3 = Wrifile;

            //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
            //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
            //DNCnt2 = DNdt2.Tables[0].Rows.Count;
            //if (v3 <= 0) return (Ret1);
            //if (DNCnt2 <= 0) t4 = "1000000000";
            //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            //int tseqid = Convert.ToInt32(t4) + 1;


            t3 = Wrifile;

            int tDocseq = 10000;
            string tSite = "ALL";
          //  string tF1 = "Parent";
          //  string tF2 = "";
          //  string tF3 = "Seqno"; string tF4 = ""; string tF5 = "SubSSeqno"; string tF6 = "01"; string tF7 = "Child  Attrib"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
          //  string tF8 = ""; string tF9 = "Child No"; string tF10 = ""; string tF11 = "NOKFOXM1005"; string tF12 = ""; 
          //  string trdate = DateTime.Now.ToString("yyyyMMdd");
            string ttmpF3 = "", ttmpF4 = "", ttmpF5 = "", ttmpF2 = "";

            ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
            ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
            string updF3 = "", updF4 = "", updF5 = "", updF2 = "";
            int var1 = 1, var2 = 0, var3 = 0;
            string tparent = "", tchild = "";
            tmpBomStr = "";

            int tseqid = 1000000000;
            //string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
            string tF1 = "NOKFOXM1101";
            string tF2 = "LaeblRuleStand";
            string tF3 = "DATABOMID"; 
            string tF4 = ""; string tF5 = "Nokia Index"; string tF6 = ""; string tF7 = ""; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
            string tF8 = ""; string tF9 = "Rule Result"; string tF10 = ""; string tflag1 = "1"; string trdate = DateTime.Now.ToString("yyyyMMdd");
            string tF11 = "GROUP "; string tF12 = ""; string tF13 = "RULE"; string tF14 = "";
            string tF15 = ""; // start key
            string tF16 = ""; // 10*2  16+20 = 36
            string tF17 = "";
            string tF18 = "";
            string tF19 = "";
            string tF20 = "";
            string tF21 = "";
            string tF22 = "";
            string tF23 = "";
            string tF24 = "";
            string tF25 = "";
            string tF26 = "";
            string tF27 = "";
            string tF28 = "";
            string tF29 = "";
            string tF30 = "";
            string tF31 = "";
            string tF32 = "";
            string tF33 = "";
            string tF34 = "";
            string tF35 = "";
            string tF36 = "";
            string tF49 = "";

            string tdocumentid = "";

            for (v1 = 0; v1 < DNCnt1; v1++)
            {

                updF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();
                updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                //tITEM = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();
                //tF8 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();

                //tMODEL = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
                //tITEM = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  // Result Code
                tF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  // Rule No
               

                if ((updF2 == ttmpF2) && (updF3 == ttmpF3))
                {
                    var1 = var1 + 1;
                }
                else
                {
                    bomno = bomno + 1;
                    // tF7 = arrBom1[v1 + 1, 04].ToString().Trim();  // DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  // Rule No 
                    // Attr1
                    tF1 = "Parent"; tF4 = "Child"; tF6 = "01"; tF7 = updF3;

                    tparent = arrBom1[v1 + 1, 05].ToString().Trim();
                    tchild = arrBom1[v1 + 1, 05].ToString().Trim();

                    tITEM = bomno.ToString().Trim();  // parent 
                    tMODEL = bomno.ToString().Trim();

                    tF2 = "";
                    tF3 = ""; // Parent
                    tF4 = "Child";
                    tF5 = ""; // child no
                    BomStr = "";
                    tSEQ = tSEQ + 1;
                    tseqid = tseqid + 1;
                    //tparent = arrBom1[v1 + 1, 05].ToString().Trim();  // parent
                    //tITEM = arrBom1[v1 + 1, 05].ToString().Trim();  // bomno
                    //tchild = arrBom1[v1 + 1, v2].ToString().Trim();  // 6,7,8,9,10,11,12 : Child
                    //tF7 = arrBom1[v1 + 1, v2].ToString().Trim();
                    //BomStr = arrBom1[v1 + 1, 16].ToString().Trim();
                    //if (v2 == 5) tF6 = "01";
                    //else if (v2 == 6) tF6 = "02";
                    //else tF6 = "03";

                 
                     //   t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12  "
                     //   + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                     //   + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                     //   + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL + "', '" + trdate + "', '" + BomStr + "' ) ";
                     //   v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                 
                    ///////////////////////
                 
                 
                        // Get Last SEQID
                     
                        t3 = Wrifile;
                        tdocumentid = arrBom1[v1 + 1, 41].ToString().Trim();

                        tF1 = "NOKFOXM1101";
                        tF2 = "LaeblRuleStand";
                        tF3 = "DATABOMID";
                        tF4 = "";
                        tF5 = "Nokia Index"; 
                        tF6 = ""; 
                        tF7 = "Para Count"; 
                        //string tddate = DateTime.Now.ToString("yyyy-MM-dd");
                        tF8 = ""; 
                        tF9 = "Rule Result"; 
                        tF10 = ""; 
                        tflag1 = "1"; 
                        //string trdate = DateTime.Now.ToString("yyyyMMdd");
                        tF11 = "GROUP "; 
                        tF12 = arrBom1[v1 + 1, 01];  // Group F1 --> F2 
                        tF13 = "RULE"; 
                        tF14 = arrBom1[v1 + 1, 02];  // RU:E TYPE F2 = F14
                        tF15 = arrBom1[v1 + 1, 03];  // start key
                        tF16 = "";
                        tF17 = arrBom1[v1 + 1, 04];
                        tF18 = "";
                        tF19 = arrBom1[v1 + 1, 05];
                        tF20 = ""; 
                        tF21 = arrBom1[v1 + 1, 06];
                        tF22 = "";
                        tF23 = arrBom1[v1 + 1, 07];
                        tF24 = "";
                        tF25 = arrBom1[v1 + 1, 08];
                        tF26 = "";
                        tF27 = arrBom1[v1 + 1, 09];
                        tF28 = "";    
                        tF29 = arrBom1[v1 + 1, 10];
                        tF30 = "";    
                        tF31 = arrBom1[v1 + 1, 11];
                        tF32 = "";
                        tF49 = arrBom1[v1 + 1, 49];

                        v5 = 0;
                        for ( v6=01; v6<10; v6++) // 
                              if (arrBom1[v1 + 1, v6] != "" )
                                  v5++;

                        tF8 = v5.ToString().Trim();
                        t2 = "Insert into " + t3 + " ( Documentid, Docseq, DDATE,  F1, F2 , F3, F4,  F5, F6, F7, F8, F9, F10, FLAG1,  RDATE, SEQID, F11, F12, F13, "
                              + " F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27, F28, F29, F30, F31, F32, F49 ) Values "
                              + " ( '" + tdocumentid + "', '" + tSEQ.ToString().Trim() + "', '" + tddate + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "',  '" + tF4.ToString().Trim() + "',  "
                              + "   '" + tF5 + "', '" + tSEQ.ToString().Trim() + "', '" + tF7 + "', '" + tF8.ToString().Trim() + "', '" + tF9 + "', '" + tF10 + "',  '" + tflag1 + "',     "
                              + "   '" + trdate + "',  '" + tseqid.ToString().Trim() + "', '" + tF11 + "', '" + tF12 + "', '" + tF13 + "', '" + tF14 + "',  "
                              + "   '" + tF15 + "', '" + tF16 + "', '" + tF17 + "', '" + tF18 + "', '" + tF19 + "', '" + tF20 + "',  "
                              + "   '" + tF21 + "' , '" + tF22 + "', '" + tF23 + "', '" + tF24 + "' , '" + tF25 + "', '" + tF26 + "',   "
                              + "   '" + tF27 + "', '" + tF28 + "',  '" + tF29 + "', '" + tF30 + "', '" + tF31 + "', '" + tF32 + "', '" + tF49 + "') ";                        
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                        if (v4 >= 1)
                        {
                            tDocseq++;
                            //tF4++;
                            //tF8++;
                            tseqid++;

                        }

                    
                    ////////////////////////

                }

                ttmpF2 = updF2;
                ttmpF3 = updF3;
                ttmpF4 = updF4;
                tmpBomStr = BomStr;

            }  // End for 

            return ("");

        // Update data

        string tmpSEQ = "";
        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + Wrifile + " where FLAG5 is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }



        return ("");

    } //   end 104


    // 105 write stand bom abd data unique
    public string Uploadlabel05(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0,v6=0, DNCnt1 = 0, DNCnt2 = 0, bomno = 10000, DNCnt3=0, SubCnt1=0;
        string tmpF1 = "", tmpF2="", tmpF3 = "", tmpF4 = "", tmpF5 = "", BomStr = "", NokRuleNo="";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "NokFoxM02") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "NokFoxM02") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "NokFoxM02") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "NokFoxM02") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2, F3 asc ";
        }

        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";

        t3 = " select distinct(F2)   from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2 asc ";  // Get Unique Role Number  
        DNdt3 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t3);
        if (DNdt3.Tables.Count <= 0) return ("-2");
        DNCnt3 = DNdt3.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt3 <= 0) return ( DNCnt3.ToString());

        int arrBomX3 = DNCnt3, arrBomY3 = 50;
        string[,] arrBom3 = new string[arrBomX3 + 1, arrBomY3 + 1];
        for (v2 = 0; v2 < arrBomX3; v2++)
            for (v3 = 0; v3 < arrBomY3; v3++)
                arrBom3[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt3; v1++)
        {
            arrBom3[v1 + 1, 01] = ""; //DNdt3.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
            arrBom3[v1 + 1, 02] = ""; //DNdt3.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom3[v1 + 1, 03] = ""; // DNdt3.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom3[v1 + 1, 01] = "NOKFOXM1201";        // F1 DNdt3.Tables[0].Rows[v1]["F1"].ToString().Trim();  // "NOKFOXM1101"  LABEL MANAGEMENT
            arrBom3[v1 + 1, 02] = "LaeblRuleStand";     // F2 DNdt3.Tables[0].Rows[v1]["F2"].ToString().Trim();  // N Group
            arrBom3[v1 + 1, 03] = "DATABOMID";          // F3 DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
            arrBom3[v1 + 1, 04] = "";                   // F4 DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
            arrBom3[v1 + 1, 05] = "Nokia Index";        // F5 DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
            arrBom3[v1 + 1, 06] = DNdt3.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  F6 Index rule no 
            arrBom3[v1 + 1, 07] = "Para Count";          // DNdt3.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom3[v1 + 1, 08] = "";                   // DNdt3.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
            arrBom3[v1 + 1, 09] = "Rule Result";        // DNdt3.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom3[v1 + 1, 10] = "";                   // DNdt3.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom3[v1 + 1, 11] = "GROUP";
            arrBom3[v1 + 1, 12] = "";
            arrBom3[v1 + 1, 13] = "RULE TYPE";
            arrBom3[v1 + 1, 14] = "";                       

        }


        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());

        int arrBomX1 = DNCnt1, arrBomY1 = 50;
        string[,] arrBom1 = new string[arrBomX1 + 1, arrBomY1 + 1];
        for (v2 = 0; v2 < arrBomX1; v2++)
            for (v3 = 0; v3 < arrBomY1; v3++)
                arrBom1[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrBom1[v1 + 1, 41] = DNdt1.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
            arrBom1[v1 + 1, 42] = DNdt1.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom1[v1 + 1, 33] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom1[v1 + 1, 44] = DNdt1.Tables[0].Rows[v1]["rdate"].ToString().Trim();   // 14
            arrBom1[v1 + 1, 45] = DNdt1.Tables[0].Rows[v1]["SEQID"].ToString().Trim();  // 15 


            arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  
            arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  NoKia Index
            arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  //  Group
            arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  Rule TYPE
            arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  //  Key1
            arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  value 
            arrBom1[v1 + 1, 07] = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  //  Rule Result
            arrBom1[v1 + 1, 08] = DNdt1.Tables[0].Rows[v1]["F8"].ToString().Trim();  //  
            arrBom1[v1 + 1, 09] = DNdt1.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom1[v1 + 1, 10] = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 
            arrBom1[v1 + 1, 11] = DNdt1.Tables[0].Rows[v1]["F11"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 
            arrBom1[v1 + 1, 14] = DNdt1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // 
            arrBom1[v1 + 1, 15] = DNdt1.Tables[0].Rows[v1]["F15"].ToString().Trim();  // 
            arrBom1[v1 + 1, 16] = DNdt1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // 
            arrBom1[v1 + 1, 17] = DNdt1.Tables[0].Rows[v1]["F17"].ToString().Trim();  // 
            arrBom1[v1 + 1, 18] = DNdt1.Tables[0].Rows[v1]["F18"].ToString().Trim();  // 
            arrBom1[v1 + 1, 19] = DNdt1.Tables[0].Rows[v1]["F19"].ToString().Trim();  // 
            arrBom1[v1 + 1, 20] = DNdt1.Tables[0].Rows[v1]["F20"].ToString().Trim();  // 
            arrBom1[v1 + 1, 21] = DNdt1.Tables[0].Rows[v1]["F21"].ToString().Trim();  // 
            arrBom1[v1 + 1, 22] = DNdt1.Tables[0].Rows[v1]["F22"].ToString().Trim();  // 
            arrBom1[v1 + 1, 23] = DNdt1.Tables[0].Rows[v1]["F23"].ToString().Trim();  // 
            arrBom1[v1 + 1, 24] = DNdt1.Tables[0].Rows[v1]["F24"].ToString().Trim();  // 
            arrBom1[v1 + 1, 25] = DNdt1.Tables[0].Rows[v1]["F25"].ToString().Trim();  // 
            arrBom1[v1 + 1, 26] = DNdt1.Tables[0].Rows[v1]["F26"].ToString().Trim();  // 


            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 04].ToString().Trim();  // 16  Setup 10 Set
            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 49].ToString().Trim() + arrBom1[v1 + 1, 05].ToString().Trim() + arrBom1[v1 + 1, 06].ToString().Trim() + arrBom1[v1 + 1, 07].ToString().Trim();
            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 49].ToString().Trim() + arrBom1[v1 + 1, 08].ToString().Trim() + arrBom1[v1 + 1, 09].ToString().Trim();
        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tDATABOMID = "NokFoxM02";
        string tMODEL = "10000";
        string tSITE = "ALL";
        string tITEM = "";
        int tSEQ = 10000;
        string tDUEDATE = "29991231";
        if (Funno != "NokFoxM02") return ("");


        // Get Last SEQID

        t3 = Wrifile;

        //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
        //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        //DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        //if (DNCnt2 <= 0) t4 = "1000000000";
        //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
        //int tseqid = Convert.ToInt32(t4) + 1;


        t3 = Wrifile;

        int tDocseq = 10000;
        string tSite = "ALL";
        //  string tF1 = "Parent";
        //  string tF2 = "";
        //  string tF3 = "Seqno"; string tF4 = ""; string tF5 = "SubSSeqno"; string tF6 = "01"; string tF7 = "Child  Attrib"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
        //  string tF8 = ""; string tF9 = "Child No"; string tF10 = ""; string tF11 = "NOKFOXM1005"; string tF12 = ""; 
        //  string trdate = DateTime.Now.ToString("yyyyMMdd");
        string ttmpF1 = "", ttmpF2 = "", ttmpF3 = "", ttmpF4 = "", ttmpF5 = "";

        ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
        ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
        string updF3 = "", updF4 = "", updF5 = "", updF2 = "";
        int var1 = 1, var2 = 0, var3 = 0;
        string tparent = "", tchild = "";
        string tmpBomStr = "";

        int tseqid = 1000000000;
        //string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tF1 = "NOKFOXM1201";
        string tF2 = "LaeblRuleStand";
        string tF3 = "DATABOMID";
        string tF4 = ""; string tF5 = "Nokia Index"; string tF6 = ""; string tF7 = ""; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
        string tF8 = ""; string tF9 = "Rule Result"; string tF10 = ""; string tflag1 = "1"; string trdate = DateTime.Now.ToString("yyyyMMdd");
        string tF11 = "GROUP "; string tF12 = ""; string tF13 = "RULE"; string tF14 = "";
        string tF15 = ""; // start key
        string tF16 = ""; // 10*2  16+20 = 36
        string tF17 = "";
        string tF18 = "";
        string tF19 = "";
        string tF20 = "";
        string tF21 = "";
        string tF22 = "";
        string tF23 = "";
        string tF24 = "";
        string tF25 = "";
        string tF26 = "";
        string tF27 = "";
        string tF28 = "";
        string tF29 = "";
        string tF30 = "";
        string tF31 = "";
        string tF32 = "";
        string tF33 = "";
        string tF34 = "";
        string tF35 = "";
        string tF36 = "";
        string tF49 = "";
        string tF50 = "";

        string tdocumentid = "";

        string updF1 = "";
        SubCnt1 = 0;
        int Bom3Loc = 0;
        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            updF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim(); // Nokia Rule No
            updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim(); // Group 
            updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim(); //Rupe 
                      
            if (updF2 == ttmpF2) // Nokia Index  
            {
                SubCnt1++;
                arrBom3[Bom3Loc, 13 + 2*SubCnt1  ] = arrBom1[v1 + 1, 05].ToString().Trim(); 
                arrBom3[Bom3Loc, 13 + 2*SubCnt1 + 1] = arrBom1[v1 + 1, 06].ToString().Trim(); 
                
            }
            else
            {
                //arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  NoKia Rule No
                //arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
                //arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  //  Rule TYPE
                //arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  Key1
                //arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  //  Value
                //arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  Rule Result 
              
              
                tF2 = "";
                tF3 = ""; // Parent
                tF4 = "Child";
                tF5 = ""; // child no
                BomStr = "";
                tSEQ = tSEQ + 1;
                tseqid = tseqid + 1;
                //tparent = arrBom1[v1 + 1, 05].ToString().Trim();  // parent
                //tITEM = arrBom1[v1 + 1, 05].ToString().Trim();  // bomno
                //tchild = arrBom1[v1 + 1, v2].ToString().Trim();  // 6,7,8,9,10,11,12 : Child
                //tF7 = arrBom1[v1 + 1, v2].ToString().Trim();
                //BomStr = arrBom1[v1 + 1, 16].ToString().Trim();
                //if (v2 == 5) tF6 = "01";
                //else if (v2 == 6) tF6 = "02";
                //else tF6 = "03";


                //   t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12  "
                //   + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                //   + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                //   + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL + "', '" + trdate + "', '" + BomStr + "' ) ";
                //   v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                ///////////////////////


                // Get Last SEQID

                t3 = Wrifile;
                tdocumentid = tdoc;

                tF1 = "NOKFOXM1201";
                tF2 = "LaeblRuleStand";
                tF3 = "DATABOMID";
                tF4 = "";
                tF5 = "Nokia Index";
                tF6 = arrBom3[Bom3Loc, 6].ToString().Trim(); 
                tF7 = "Para Count";
                //string tddate = DateTime.Now.ToString("yyyy-MM-dd");
                tF8 = (  3 + SubCnt1).ToString().Trim(); // arrBom3[Bom3Loc, 8]; 
                tF9 = "Rule Result";
                tF10 = arrBom1[Bom3Loc, 07].ToString().Trim(); 
                tflag1 = "1";
                //string trdate = DateTime.Now.ToString("yyyyMMdd");
                tF11 = "GROUP ";
                tF12 = arrBom3[Bom3Loc, 12];  // Group F1 --> F2 
                tF13 = "RULE";
                tF14 = arrBom3[Bom3Loc, 14];  // RU:E TYPE F2 = F14
                tF15 = arrBom3[Bom3Loc, 15];  // start key
                tF16 = arrBom3[Bom3Loc, 16]; 
                tF17 = arrBom3[Bom3Loc, 17];
                tF18 = arrBom3[Bom3Loc, 18];
                tF19 = arrBom3[Bom3Loc, 19];
                tF20 = arrBom3[Bom3Loc, 20];
                tF21 = arrBom3[Bom3Loc, 21];
                tF22 = arrBom3[Bom3Loc, 22];
                tF23 = arrBom3[Bom3Loc, 23];
                tF24 = arrBom3[Bom3Loc, 24];
                tF25 = arrBom3[Bom3Loc, 25];
                tF26 = arrBom3[Bom3Loc, 26];
                tF27 = arrBom3[Bom3Loc, 27];
                tF28 = arrBom3[Bom3Loc, 28];
                tF29 = arrBom3[Bom3Loc, 29];
                tF30 = arrBom3[Bom3Loc, 30];
                tF31 = arrBom3[Bom3Loc, 31];
                tF32 = arrBom3[Bom3Loc, 32];
                tF49 = tF12.ToString().Trim() + tF12.ToString().Trim();
                tF50 = tF12.ToString().Trim() + tF12.ToString().Trim();
                v5 = 0;
                //for (v6 = 01; v6 < 10; v6++) // 
                //    if (arrBom1[v1 + 1, v6] != "")
                //        v5++;

                tF8 = v5.ToString().Trim();

                
                t2 = "Insert into " + t3 + " ( Documentid, Docseq, DDATE,  F1, F2 , F3, F4,  F5, F6, F7, F8, F9, F10, FLAG1,  RDATE, SEQID, F11, F12, F13, "
                      + " F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27, F28, F29, F30, F31, F32, F49¡A F50 ) Values "
                      + " ( '" + tdocumentid + "', '" + tSEQ.ToString().Trim() + "', '" + tddate + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "',  '" + tF4.ToString().Trim() + "',  "
                      + "   '" + tF5 + "', '" + tF6 + "', '" + tF7 + "', '" + tF8.ToString().Trim() + "', '" + tF9 + "', '" + tF10 + "',  '" + tflag1 + "',     "
                      + "   '" + trdate + "',  '" + tseqid.ToString().Trim() + "', '" + tF11 + "', '" + tF12 + "', '" + tF13 + "', '" + tF14 + "',  "
                      + "   '" + tF15 + "', '" + tF16 + "', '" + tF17 + "', '" + tF18 + "', '" + tF19 + "', '" + tF20 + "',  "
                      + "   '" + tF21 + "', '" + tF22 + "', '" + tF23 + "', '" + tF24 + "', '" + tF25 + "', '" + tF26 + "',   "
                      + "   '" + tF27 + "', '" + tF28 + "', '" + tF29 + "', '" + tF30 + "', '" + tF31 + "', '" + tF32 + "', '" + tF49 + "', '" + tF50 + "') ";
                if ( v1 > 0 )
                     v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                if (v4 >= 1)
                {
                    tDocseq++;
                    //tF4++;
                    //tF8++;
                    tseqid++;

                }

                //arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  NoKia Rule No
                //arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
                //arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  //  Rule TYPE
                //arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  Key1
                //arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  //  Value
                //arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  Rule Result 
                SubCnt1 = 1; // start Next NokIndex No
                Bom3Loc++;
                NokRuleNo = arrBom1[v1 + 1, 01];
                bomno = bomno + 1;
                arrBom3[Bom3Loc, 12] = arrBom1[v1 + 1, 03].ToString().Trim(); //= DNdt1.Tables[0].Rows[v1]//  Group value
                arrBom3[Bom3Loc, 14] = arrBom1[v1 + 1, 04].ToString().Trim(); //= DNdt1.Tables[0].Rows[v1]//  RULE TYPE value
                arrBom3[Bom3Loc, 01] = ""; //DNdt3.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 02] = ""; //DNdt3.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 03] = ""; // DNdt3.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 01] = "NOKFOXM1201";        // F1 DNdt3.Tables[0].Rows[v1]["F1"].ToString().Trim();  // "NOKFOXM1101"  LABEL MANAGEMENT
                arrBom3[Bom3Loc, 02] = "LaeblRuleStand";     // F2 DNdt3.Tables[0].Rows[v1]["F2"].ToString().Trim();  // N Group
                arrBom3[Bom3Loc, 03] = "DATABOMID";          // F3 DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 04] = "";                   // F4 DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 05] = "Nokia Index";        // F5 DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 06] = arrBom1[v1 + 1, 02].ToString().Trim(); //  F6 Index rule no 
                arrBom3[Bom3Loc, 07] = "Para Count";          // DNdt3.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 08] = "";                   // DNdt3.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 09] = "Rule Result";        // DNdt3.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 10] = arrBom1[v1 + 1, 07].ToString().Trim();  
                arrBom3[Bom3Loc, 11] = "GROUP";
                arrBom3[Bom3Loc, 12] = arrBom1[v1 + 1, 03].ToString().Trim(); // Group Context 
                arrBom3[Bom3Loc, 13] = "RULE TYPE";
                arrBom3[Bom3Loc, 14] = arrBom1[v1 + 1, 04].ToString().Trim(); // RULE TYPE Context    
                arrBom3[Bom3Loc, 15] = arrBom1[v1 + 1, 05].ToString().Trim(); // RULE TYPE Context   
                arrBom3[Bom3Loc, 16] = arrBom1[v1 + 1, 06].ToString().Trim(); // RULE TYPE Context   


            } // end if



            ttmpF2 = updF2;
            ttmpF3 = updF3;
            ttmpF4 = updF4;
            tmpBomStr = BomStr;

        }  // End for 

        return ("");

        // Update data

        string tmpSEQ = "";
        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + Wrifile + " where FLAG5 is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }

        return ("");
    } //   105


    // 105 write stand bom abd data multi databom02
    public string Uploadlabel06(string DBOracle, string tmpReadDBA, string Updfile, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID)
    {
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        int v1 = 0, v2 = -0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, DNCnt1 = 0, DNCnt2 = 0, bomno = 10000, DNCnt3 = 0, SubCnt1 = 0;
        string tmpF1 = "", tmpF2 = "", tmpF3 = "", tmpF4 = "", tmpF5 = "", BomStr = "", NokRuleNo = "";


        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null;

        if (UpdfileID == "")
        {
            if (Funno == "LBBOM02") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM02") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        }
        else
        {
            if (Funno == "LBBOM02") t2 = " select *  from  " + NokFoxM01Ref + " where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
            if (Funno == "LBBOM02") t1 = " select *  from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2, F3 asc ";
        }

        //if (Funno == "LBBOM01") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //if (Funno == "LBBOM01") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";

        t3 = " select distinct(F2)   from  " + Updfile + "  where documentid = '" + UpdfileID + "' order  by F2 asc ";  // Get Unique Role Number  
        DNdt3 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t3);
        if (DNdt3.Tables.Count <= 0) return ("-2");
        DNCnt3 = DNdt3.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt3 <= 0) return (DNCnt3.ToString());

        int arrBomX3 = DNCnt3, arrBomY3 = 50;
        string[,] arrBom3 = new string[arrBomX3 + 1, arrBomY3 + 1];
        for (v2 = 0; v2 < arrBomX3; v2++)
            for (v3 = 0; v3 < arrBomY3; v3++)
                arrBom3[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt3; v1++)
        {
            arrBom3[v1 + 1, 01] = ""; //DNdt3.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
            arrBom3[v1 + 1, 02] = ""; //DNdt3.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom3[v1 + 1, 03] = ""; // DNdt3.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom3[v1 + 1, 01] = "NOKFOXM1201";        // F1 DNdt3.Tables[0].Rows[v1]["F1"].ToString().Trim();  // "NOKFOXM1101"  LABEL MANAGEMENT
            arrBom3[v1 + 1, 02] = "LaeblRuleStand";     // F2 DNdt3.Tables[0].Rows[v1]["F2"].ToString().Trim();  // N Group
            arrBom3[v1 + 1, 03] = "DATABOMID";          // F3 DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
            arrBom3[v1 + 1, 04] = "";                   // F4 DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
            arrBom3[v1 + 1, 05] = "Nokia Index";        // F5 DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
            arrBom3[v1 + 1, 06] = DNdt3.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  F6 Index rule no 
            arrBom3[v1 + 1, 07] = "Para Count";          // DNdt3.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom3[v1 + 1, 08] = "";                   // DNdt3.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
            arrBom3[v1 + 1, 09] = "Rule Result";        // DNdt3.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom3[v1 + 1, 10] = "";                   // DNdt3.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
            arrBom3[v1 + 1, 11] = "GROUP";
            arrBom3[v1 + 1, 12] = "";
            arrBom3[v1 + 1, 13] = "RULE TYPE";
            arrBom3[v1 + 1, 14] = "";

        }


        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        if (DNCnt1 <= 0) return (v1.ToString());

        int arrBomX1 = DNCnt1, arrBomY1 = 50;
        string[,] arrBom1 = new string[arrBomX1 + 1, arrBomY1 + 1];
        for (v2 = 0; v2 < arrBomX1; v2++)
            for (v3 = 0; v3 < arrBomY1; v3++)
                arrBom1[v2, v3] = "";


        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            arrBom1[v1 + 1, 41] = DNdt1.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
            arrBom1[v1 + 1, 42] = DNdt1.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
            arrBom1[v1 + 1, 33] = DNdt1.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
            arrBom1[v1 + 1, 44] = DNdt1.Tables[0].Rows[v1]["rdate"].ToString().Trim();   // 14
            arrBom1[v1 + 1, 45] = DNdt1.Tables[0].Rows[v1]["SEQID"].ToString().Trim();  // 15 


            arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  
            arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  NoKia Index
            arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  //  Group
            arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  Rule TYPE
            arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  //  Key1
            arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  value 
            arrBom1[v1 + 1, 07] = DNdt1.Tables[0].Rows[v1]["F7"].ToString().Trim();  //  Rule Result
            arrBom1[v1 + 1, 08] = DNdt1.Tables[0].Rows[v1]["F8"].ToString().Trim();  //  
            arrBom1[v1 + 1, 09] = DNdt1.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
            arrBom1[v1 + 1, 10] = DNdt1.Tables[0].Rows[v1]["F10"].ToString().Trim();  // 
            arrBom1[v1 + 1, 11] = DNdt1.Tables[0].Rows[v1]["F11"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 
            arrBom1[v1 + 1, 12] = DNdt1.Tables[0].Rows[v1]["F12"].ToString().Trim();  // 
            arrBom1[v1 + 1, 13] = DNdt1.Tables[0].Rows[v1]["F13"].ToString().Trim();  // 
            arrBom1[v1 + 1, 14] = DNdt1.Tables[0].Rows[v1]["F14"].ToString().Trim();  // 
            arrBom1[v1 + 1, 15] = DNdt1.Tables[0].Rows[v1]["F15"].ToString().Trim();  // 
            arrBom1[v1 + 1, 16] = DNdt1.Tables[0].Rows[v1]["F16"].ToString().Trim();  // 
            arrBom1[v1 + 1, 17] = DNdt1.Tables[0].Rows[v1]["F17"].ToString().Trim();  // 
            arrBom1[v1 + 1, 18] = DNdt1.Tables[0].Rows[v1]["F18"].ToString().Trim();  // 
            arrBom1[v1 + 1, 19] = DNdt1.Tables[0].Rows[v1]["F19"].ToString().Trim();  // 
            arrBom1[v1 + 1, 20] = DNdt1.Tables[0].Rows[v1]["F20"].ToString().Trim();  // 
            arrBom1[v1 + 1, 21] = DNdt1.Tables[0].Rows[v1]["F21"].ToString().Trim();  // 
            arrBom1[v1 + 1, 22] = DNdt1.Tables[0].Rows[v1]["F22"].ToString().Trim();  // 
            arrBom1[v1 + 1, 23] = DNdt1.Tables[0].Rows[v1]["F23"].ToString().Trim();  // 
            arrBom1[v1 + 1, 24] = DNdt1.Tables[0].Rows[v1]["F24"].ToString().Trim();  // 
            arrBom1[v1 + 1, 25] = DNdt1.Tables[0].Rows[v1]["F25"].ToString().Trim();  // 
            arrBom1[v1 + 1, 26] = DNdt1.Tables[0].Rows[v1]["F26"].ToString().Trim();  // 


            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 01].ToString().Trim() + arrBom1[v1 + 1, 04].ToString().Trim();  // 16  Setup 10 Set
            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 49].ToString().Trim() + arrBom1[v1 + 1, 05].ToString().Trim() + arrBom1[v1 + 1, 06].ToString().Trim() + arrBom1[v1 + 1, 07].ToString().Trim();
            arrBom1[v1 + 1, 49] = arrBom1[v1 + 1, 49].ToString().Trim() + arrBom1[v1 + 1, 08].ToString().Trim() + arrBom1[v1 + 1, 09].ToString().Trim();
        }

        //tmpF3 = DNdt2.Tables[0].Rows[v2]["data1"].ToString().Trim(); // GROUP  F3

        string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tDATABOMID = "NokFoxM02";
        string tMODEL = "10000";
        string tSITE = "ALL";
        string tITEM = "";
        int tSEQ = 10000;
        string tDUEDATE = "29991231";
        if (Funno != "LBBOM02") return ("");


        // Get Last SEQID

        t3 = Wrifile;

        //t1 = " select seqid from " + t3 + " order  by SEQID desc ";
        //DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        //DNCnt2 = DNdt2.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        //if (DNCnt2 <= 0) t4 = "1000000000";
        //else t4 = DNdt2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
        //int tseqid = Convert.ToInt32(t4) + 1;


        t3 = Wrifile;

        int tDocseq = 10000;
        string tSite = "ALL";
        //  string tF1 = "Parent";
        //  string tF2 = "";
        //  string tF3 = "Seqno"; string tF4 = ""; string tF5 = "SubSSeqno"; string tF6 = "01"; string tF7 = "Child  Attrib"; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
        //  string tF8 = ""; string tF9 = "Child No"; string tF10 = ""; string tF11 = "NOKFOXM1005"; string tF12 = ""; 
        //  string trdate = DateTime.Now.ToString("yyyyMMdd");
        string ttmpF1 = "", ttmpF2 = "", ttmpF3 = "", ttmpF4 = "", ttmpF5 = "";

        ttmpF3 = ""; // DNdt1.Tables[0].Rows[0]["F3"].ToString().Trim();
        ttmpF4 = ""; // DNdt1.Tables[0].Rows[0]["F6"].ToString().Trim();
        string updF3 = "", updF4 = "", updF5 = "", updF2 = "";
        int var1 = 1, var2 = 0, var3 = 0;
        string tparent = "", tchild = "";
        string tmpBomStr = "";

        int tseqid = 1000000000;
        //string tdoc = DateTime.Now.ToString("yyyyMMddHHmmss"); // document
        string tF1 = "NOKFOXM1201";
        string tF2 = "LaeblRuleStand";
        string tF3 = "DATABOMID";
        string tF4 = ""; string tF5 = "Nokia Index"; string tF6 = ""; string tF7 = ""; string tddate = DateTime.Now.ToString("yyyy-MM-dd");
        string tF8 = ""; string tF9 = "Rule Result"; string tF10 = ""; string tflag1 = "1"; string trdate = DateTime.Now.ToString("yyyyMMdd");
        string tF11 = "GROUP "; string tF12 = ""; string tF13 = "RULE"; string tF14 = "";
        string tF15 = ""; // start key
        string tF16 = ""; // 10*2  16+20 = 36
        string tF17 = "";
        string tF18 = "";
        string tF19 = "";
        string tF20 = "";
        string tF21 = "";
        string tF22 = "";
        string tF23 = "";
        string tF24 = "";
        string tF25 = "";
        string tF26 = "";
        string tF27 = "";
        string tF28 = "";
        string tF29 = "";
        string tF30 = "";
        string tF31 = "";
        string tF32 = "";
        string tF33 = "";
        string tF34 = "";
        string tF35 = "";
        string tF36 = "";
        string tF49 = "";
        string tF50 = "";

        string tdocumentid = "";

        string updF1 = "";
        SubCnt1 = 0;
        int Bom3Loc = 0;
        for (v1 = 0; v1 < DNCnt1; v1++)
        {
            updF2 = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim(); // Nokia Rule No
            updF3 = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim(); // Group 
            updF4 = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim(); //Rupe 

            if (updF2 == ttmpF2) // Nokia Index  
            {
                SubCnt1++;
                arrBom3[Bom3Loc, 13 + 2 * SubCnt1] = arrBom1[v1 + 1, 05].ToString().Trim();
                arrBom3[Bom3Loc, 13 + 2 * SubCnt1 + 1] = arrBom1[v1 + 1, 06].ToString().Trim();

            }
            else
            {
                //arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  NoKia Rule No
                //arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
                //arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  //  Rule TYPE
                //arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  Key1
                //arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  //  Value
                //arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  Rule Result 


                tF2 = "";
                tF3 = ""; // Parent
                tF4 = "Child";
                tF5 = ""; // child no
                BomStr = "";
                tSEQ = tSEQ + 1;
                tseqid = tseqid + 1;
                //tparent = arrBom1[v1 + 1, 05].ToString().Trim();  // parent
                //tITEM = arrBom1[v1 + 1, 05].ToString().Trim();  // bomno
                //tchild = arrBom1[v1 + 1, v2].ToString().Trim();  // 6,7,8,9,10,11,12 : Child
                //tF7 = arrBom1[v1 + 1, v2].ToString().Trim();
                //BomStr = arrBom1[v1 + 1, 16].ToString().Trim();
                //if (v2 == 5) tF6 = "01";
                //else if (v2 == 6) tF6 = "02";
                //else tF6 = "03";


                //   t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12  "
                //   + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                //   + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                //   + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL + "', '" + trdate + "', '" + BomStr + "' ) ";
                //   v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                ///////////////////////


                // Get Last SEQID

                t3 = Wrifile;
                tdocumentid = tdoc;

                tF1 = "NOKFOXM1201";
                tF2 = "LaeblRuleStand";
                tF3 = "DATABOMID";
                tF4 = "";
                tF5 = "Nokia Index";
                tF6 = arrBom3[Bom3Loc, 6].ToString().Trim();
                tF7 = "Para Count";
                //string tddate = DateTime.Now.ToString("yyyy-MM-dd");
                tF8 = (3 + SubCnt1).ToString().Trim(); // arrBom3[Bom3Loc, 8]; 
                tF9 = "Rule Result";
                tF10 = arrBom1[Bom3Loc, 07].ToString().Trim();
                tflag1 = "1";
                //string trdate = DateTime.Now.ToString("yyyyMMdd");
                tF11 = "GROUP ";
                tF12 = arrBom3[Bom3Loc, 12];  // Group F1 --> F2 
                tF13 = "RULE";
                tF14 = arrBom3[Bom3Loc, 14];  // RU:E TYPE F2 = F14
                tF15 = arrBom3[Bom3Loc, 15];  // start key
                tF16 = arrBom3[Bom3Loc, 16];
                tF17 = arrBom3[Bom3Loc, 17];
                tF18 = arrBom3[Bom3Loc, 18];
                tF19 = arrBom3[Bom3Loc, 19];
                tF20 = arrBom3[Bom3Loc, 20];
                tF21 = arrBom3[Bom3Loc, 21];
                tF22 = arrBom3[Bom3Loc, 22];
                tF23 = arrBom3[Bom3Loc, 23];
                tF24 = arrBom3[Bom3Loc, 24];
                tF25 = arrBom3[Bom3Loc, 25];
                tF26 = arrBom3[Bom3Loc, 26];
                tF27 = arrBom3[Bom3Loc, 27];
                tF28 = arrBom3[Bom3Loc, 28];
                tF29 = arrBom3[Bom3Loc, 29];
                tF30 = arrBom3[Bom3Loc, 30];
                tF31 = arrBom3[Bom3Loc, 31];
                tF32 = arrBom3[Bom3Loc, 32];
                tF49 = tF12.ToString().Trim() + tF12.ToString().Trim();
                tF50 = tF12.ToString().Trim() + tF12.ToString().Trim();
                v5 = 0;
                //for (v6 = 01; v6 < 10; v6++) // 
                //    if (arrBom1[v1 + 1, v6] != "")
                //        v5++;

                tF8 = v5.ToString().Trim();


                t2 = "Insert into " + t3 + " ( DATABOMID, SITE, ITEM, SEQ, DUEDATE, FLAG1, FLAG2 , FLAG3, FLAG4,  FLAG5, FLAG6,FLAG7, model, uptime, FLAG12  "
                   + "  ) Values ( '" + Funno + "', '" + tSite + "', '" + tITEM + "', '" + tSEQ.ToString().Trim() + "', "
                   + " '" + tDUEDATE + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "', '" + tF4 + "', '" + tF5 + "', "
                   + " '" + tF6 + "', '" + tF7 + "', '" + tMODEL + "', '" + trdate + "', '" + BomStr + "' ) ";
                v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);    

          //      t2 = "Insert into " + t3 + " ( Documentid, Docseq, DDATE,  F1, F2 , F3, F4,  F5, F6, F7, F8, F9, F10, FLAG1,  RDATE, SEQID, F11, F12, F13, "
          //            + " F14, F15, F16, F17, F18, F19, F20, F21, F22, F23, F24, F25, F26, F27, F28, F29, F30, F31, F32, F49¡A F50 ) Values "
          //            + " ( '" + tdocumentid + "', '" + tSEQ.ToString().Trim() + "', '" + tddate + "', '" + tF1 + "', '" + tF2 + "', '" + tF3 + "',  '" + tF4.ToString().Trim() + "',  "
          //            + "   '" + tF5 + "', '" + tF6 + "', '" + tF7 + "', '" + tF8.ToString().Trim() + "', '" + tF9 + "', '" + tF10 + "',  '" + tflag1 + "',     "
          //            + "   '" + trdate + "',  '" + tseqid.ToString().Trim() + "', '" + tF11 + "', '" + tF12 + "', '" + tF13 + "', '" + tF14 + "',  "
          //            + "   '" + tF15 + "', '" + tF16 + "', '" + tF17 + "', '" + tF18 + "', '" + tF19 + "', '" + tF20 + "',  "
          //            + "   '" + tF21 + "', '" + tF22 + "', '" + tF23 + "', '" + tF24 + "', '" + tF25 + "', '" + tF26 + "',   "
          //            + "   '" + tF27 + "', '" + tF28 + "', '" + tF29 + "', '" + tF30 + "', '" + tF31 + "', '" + tF32 + "', '" + tF49 + "', '" + tF50 + "') ";
          //      if (v1 > 0)
          //          v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);

                if (v4 >= 1)
                {
                    tDocseq++;
                    //tF4++;
                    //tF8++;
                    tseqid++;

                }

                //arrBom1[v1 + 1, 01] = DNdt1.Tables[0].Rows[v1]["F1"].ToString().Trim();  //  NoKia Rule No
                //arrBom1[v1 + 1, 02] = DNdt1.Tables[0].Rows[v1]["F2"].ToString().Trim();  //  Group
                //arrBom1[v1 + 1, 03] = DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  //  Rule TYPE
                //arrBom1[v1 + 1, 04] = DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  //  Key1
                //arrBom1[v1 + 1, 05] = DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  //  Value
                //arrBom1[v1 + 1, 06] = DNdt1.Tables[0].Rows[v1]["F6"].ToString().Trim();  //  Rule Result 
                SubCnt1 = 1; // start Next NokIndex No
                Bom3Loc++;
                NokRuleNo = arrBom1[v1 + 1, 01];
                bomno = bomno + 1;
                arrBom3[Bom3Loc, 12] = arrBom1[v1 + 1, 03].ToString().Trim(); //= DNdt1.Tables[0].Rows[v1]//  Group value
                arrBom3[Bom3Loc, 14] = arrBom1[v1 + 1, 04].ToString().Trim(); //= DNdt1.Tables[0].Rows[v1]//  RULE TYPE value
                arrBom3[Bom3Loc, 01] = ""; //DNdt3.Tables[0].Rows[v1]["Documentid"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 02] = ""; //DNdt3.Tables[0].Rows[v1]["Ddate"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 03] = ""; // DNdt3.Tables[0].Rows[v1]["Docseq"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 01] = "NOKFOXM1201";        // F1 DNdt3.Tables[0].Rows[v1]["F1"].ToString().Trim();  // "NOKFOXM1101"  LABEL MANAGEMENT
                arrBom3[Bom3Loc, 02] = "LaeblRuleStand";     // F2 DNdt3.Tables[0].Rows[v1]["F2"].ToString().Trim();  // N Group
                arrBom3[Bom3Loc, 03] = "DATABOMID";          // F3 DNdt1.Tables[0].Rows[v1]["F3"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 04] = "";                   // F4 DNdt1.Tables[0].Rows[v1]["F4"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 05] = "Nokia Index";        // F5 DNdt1.Tables[0].Rows[v1]["F5"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 06] = arrBom1[v1 + 1, 02].ToString().Trim(); //  F6 Index rule no 
                arrBom3[Bom3Loc, 07] = "Para Count";          // DNdt3.Tables[0].Rows[v1]["F7"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 08] = "";                   // DNdt3.Tables[0].Rows[v1]["F8"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 09] = "Rule Result";        // DNdt3.Tables[0].Rows[v1]["F9"].ToString().Trim();  // 
                arrBom3[Bom3Loc, 10] = arrBom1[v1 + 1, 07].ToString().Trim();
                arrBom3[Bom3Loc, 11] = "GROUP";
                arrBom3[Bom3Loc, 12] = arrBom1[v1 + 1, 03].ToString().Trim(); // Group Context 
                arrBom3[Bom3Loc, 13] = "RULE TYPE";
                arrBom3[Bom3Loc, 14] = arrBom1[v1 + 1, 04].ToString().Trim(); // RULE TYPE Context    
                arrBom3[Bom3Loc, 15] = arrBom1[v1 + 1, 05].ToString().Trim(); // RULE TYPE Context   
                arrBom3[Bom3Loc, 16] = arrBom1[v1 + 1, 06].ToString().Trim(); // RULE TYPE Context   


            } // end if



            ttmpF2 = updF2;
            ttmpF3 = updF3;
            ttmpF4 = updF4;
            tmpBomStr = BomStr;

        }  // End for 

        return ("");

        // Update data

        string tmpSEQ = "";
        v2 = 0;
        t2 = NokFoxM01Ref;
        t1 = " select *  from " + Wrifile + " where FLAG5 is null "; // databomid = 'LBP1TOP2' order  by F3, F4 asc ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t1);
        DNCnt1 = DNdt1.Tables[0].Rows.Count;
        if (DNCnt1 > 0)
        {
            for (v1 = 0; v1 < DNCnt1; v1++)
            {
                t3 = DNdt1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim();   // attrib number
                t4 = DNdt1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();   // name
                tMODEL = DNdt1.Tables[0].Rows[v1]["MODEL"].ToString().Trim();
                tmpSEQ = DNdt1.Tables[0].Rows[v1]["SEQ"].ToString().Trim();

                t2 = " select *  from " + NokFoxM01Ref + " where F6 = '" + t3 + "'  and F10 = '" + t4 + "'"; // attrib, name
                DNdt2 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
                DNCnt2 = DNdt2.Tables[0].Rows.Count;
                if (DNCnt2 > 0)
                {
                    t5 = DNdt2.Tables[0].Rows[0]["F4"].ToString().Trim();   // part number
                    t2 = "update " + Wrifile + "  set FLAG5 = '" + t5 + "' where MODEL =  '" + tMODEL + "' and SEQ = '" + tmpSEQ + "'  and FLAG7 = '" + t4 + "'  ";
                    v4 = PDataBaseOperation.PExecSQL(DBOracle, tmpReadDBA, t2);
                    v2 = v2 + v4;
                }
            }


        }

        return ("");
    } //   105
    // InFile = select *  from FGIS.GDNINFO  156 Server 
    // flag1 = status, flag2 = Read  flag3 = Write
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    // N Project Upload XML to FTP 20161130
    // http://localhost/CCVANWeb/MainCCVAN.aspx?5A5A=C10301kenken12345610000110.186.19.108/,/10.84.110.103/,/3/,/GY/,/DN2/,/FTP-File/,/
    // 5A5A C10201 kenken 123456 100001 10.186.19.108/,/WriteIP:10.186.19.108/,/3/,/GY/,/DN/,/FTP-File/,/
    // Para 1 : Source IP
    // Para 2 : Dest   IP
    // Para 3 : paramater number
    // Para 4 : DN
    // Para 5 : Ftp Fiel
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string UploadFGISDN(string CmdPara, string tmpDN, string DbType, string DBString, string InFile, string FtpFile, string DBType1, string BackDBString, ref string[,] arrPara)  
    {
        string Sqlr = "", SqlrW = "", Ret1 = "", t1="", t2="", tmpSite = "";
        int v1=0, v2=0, v3=0, v4=0;
        string tDocumentID = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string tDocseq = "1000000000";
        DataSet DNdt1 = null;
        string tflag1 = "", tflag2 = "", tflag3 = "";
        string Cmdno = "101";

        // Put Array 
        tmpSite = arrPara[4, 0].ToString().Trim();
        tmpDN   = arrPara[5, 0].ToString().Trim();
        FtpFile = arrPara[6, 0].ToString().Trim();
        // Get Last Docseq
        Sqlr = " select *  from " + InFile + " order  by  Docseq desc  ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DbType, DBString, Sqlr);
        if (DNdt1 == null) return ("-1");
        if (DNdt1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DNdt1.Tables[0].Rows.Count;
        if (v1 > 0)
        {
            t1 = DNdt1.Tables[0].Rows[0]["Docseq"].ToString();
            if (t1 != "") tDocseq = (Convert.ToInt32(t1) + 1).ToString().Trim();
        }


        Sqlr = " select *  from " + InFile + " where  DN_NO = '" + tmpDN + "' ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DbType, DBString, Sqlr);
         
        if (DNdt1 == null) return ("-1");
        if (DNdt1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DNdt1.Tables[0].Rows.Count;

        if (v1 > 0)
        {
            tflag1 = DNdt1.Tables[0].Rows[0]["flag1"].ToString().Trim();
            tflag2 = DNdt1.Tables[0].Rows[0]["flag2"].ToString().Trim();
            tflag3 = DNdt1.Tables[0].Rows[0]["flag3"].ToString().Trim();
            if (tflag1 == "S") return ("-1");  // Already Sending  

        }

        // if (v1 <= 0) return ("0"); 

        string tSendmode1 = "MAIL";
        string tSendPara2 = "MAILTEST";
        string cmdcode2 = "";

        if ((CmdPara != "") && (CmdPara.Length == 6))
            cmdcode2 = CmdPara.Substring(0, 1);
        else
            return ("-1");


        switch (cmdcode2)
        {
            case "2":  tSendmode1 = "MAILGROUP";
                       tSendPara2 = "MAILTEST"; 
                       break;
            case "1":  tSendmode1 = "MAILGROUP";
                       tSendPara2 = "MAILNTeam";
                       break;
            case "3":  tSendmode1 = "MAIL";
                       tSendPara2 = "ken.lin@foxconn.com";
                       break;
            case "4":  tSendmode1 = "MAIL";
                       tSendPara2 = "customer@com.com";
                       break;
            case "5":  tSendmode1 = "FTP";
                       tSendPara2 = "MAILNTeam";
                       break;
            case "7":  tSendmode1 = "B2B";
                       tSendPara2 = "MAILNTeam";
                       break; 
            default:   tSendmode1 = "MAILGROUP";
                       tSendPara2 = "MAILTEST"; 
                       break;
        }

       
        if ( (Cmdno == "101") && ( v1 <= 0 ) )
        {
            //SqlrW = "Insert into " + InFile + " ( Documentid, Docseq, DN_NO, FLAG1 , FLAG2, FLAG3,  FLAG11, SITE ) Values "
            //          + " ( '" + tDocumentID + "', '" + tDocseq + "', '" + tmpDN + "', 'R', '" + tDocumentID + "', '',  '" + FtpFile + "', '" + tmpSite + "'  ) ";

            SqlrW = "Insert into " + InFile + " ( Documentid, Docseq, DN_NO, FLAG1 , FLAG2, FLAG3,  FLAG11, SITE, FLAG4, FLAG5 ) Values "
                     + " ( '" + tDocumentID + "', '" + tDocseq + "', '" + tmpDN + "', 'R', '" + tDocumentID + "', '',  '" + FtpFile + "', '" + tmpSite + "',  "
                     + "   '" + tSendmode1 + "', '" + tSendPara2 + "'  ) ";
            v4 = PDataBaseOperation.PExecSQL( DBType, DBString, SqlrW);
            return( v4.ToString() );
        }

        // v1 > 0 found() DN_NO
        if ( (Cmdno == "101" ) && ( tflag1 == "S" )) return("");  // already Send to Customer
        if ((Cmdno == "101") && (tflag1 == "R"))   // update
        {
            SqlrW = "update " + InFile + " set FLAG2 = '" + tDocumentID + "', FLAG11 = '" + FtpFile + "', Site = '" + tmpSite + "'  where DN_NO = '" + tmpDN + "' ";
            v4 = PDataBaseOperation.PExecSQL(DBType, DBString, SqlrW);
            return (v4.ToString());
        }
       
      

      

        return ( Ret1 );
    }
        
  
    public string RecordINData(string Cmdno, string DbType, string DBString, string InFile, string InString, string Locnum)
    {    
        string tmp1 = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        tmp1 = Locnum + ":" +  tmp1;
        if (Cmdno == "C") tmp1 = "";
        string SqlrW = "update " + InFile + " set FLAG15 = '" + InString + "', FLAG16 = '" + tmp1 + "' where DOCSEQ = '1000000000'  ";
        int v4 = PDataBaseOperation.PExecSQL(DBType, DBString, SqlrW);
        return (v4.ToString());
        
    }
    public string CMDelUpdate(string ptype, string dbtype, string DBRead, string DBWri, string daycnts)
    {
        string Ret1 = "", sql1 = "", tmpsqlW = "", sp = "", sp0 = "0", tpo="", tdn="", tmpselwri = "";
        DataSet dt1 = null, dt2 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0, v6=0, v7=0, daycnt = Convert.ToInt32(daycnts);
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string t21 = "", t22 = "", t23 = "", t121 = "";
        string tmp1 = "", tmp2 = "";
        Decimal d1=0, d2=0, d3=0;
        string tmpDate = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays(daycnt).ToString("yyyyMMdd"); 

        // Get PO
        sql1 = " SELECT  PO, CDATE, DDATE  from PUBLIB.CPOM1 "
             + " where substr(CDATE, 1, 8 )  >= '" + tmp1Date + "' "
             + " and   substr(CDATE, 1, 8 )  <= '" + tmpDate + "'  "
             + " order by PO desc ";
        // sap.cmcs_sfc_packing_lines_all ";
        //     // + "  AND to_char((last_update_date),'yyyyMMdd') >= '20101231' "
        //     // + "      to_char((last_update_date),'yyyyMMdd') <= '20110431'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        // if (v3 <= 0) return (Ret1);

        int arrPOlong = v3, arrPOwidth = 20;
        string[,] arrayPO = new string[arrPOwidth + 1, arrPOlong + 1];

        for (v1 = 0; v1 < arrPOlong; v1++)
            for (v2 = 0; v2 < arrPOwidth; v2++)
                arrayPO[v2, v1] = "";

        for (v1 = 0; v1 < arrPOlong; v1++)
        {
            arrayPO[15, v1 + 1] = "Y";  // write flag
            arrayPO[1,  v1 + 1] = dt1.Tables[0].Rows[v1]["PO"].ToString();  // PO
            arrayPO[16, v1 + 1] = dt1.Tables[0].Rows[v1]["CDATE"].ToString();  // CPOM1- first-time
            arrayPO[17, v1 + 1] = dt1.Tables[0].Rows[v1]["DDATE"].ToString();  // CPOM1
        }
      
        // Check need update
        // for (v1 = arrPOlong; v1 < arrPOlong; v1++)  // Run from head
        for (v1 = 0; v1 < arrPOlong; v1++)
        {
            if (arrayPO[16, v1 + 1] != "")  // not-first-time
            {
                tpo = arrayPO[1, v1 + 1]; //  = dt1.Tables[0].Rows[v1]["PO"].ToString();  // PO
                tmpselwri = "select to_char( max(ship_date), 'yyyyMMddHHmmssmm') ship_date from SAP.CMCS_SFC_PACKING_LINES_ALL where customer_po = '" + tpo + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 > 0)
                {
                    arrayPO[18, v1 + 1] = dt2.Tables[0].Rows[0]["ship_date"].ToString();
                    tmp1 = arrayPO[17, v1 + 1];
                    tmp2 = arrayPO[18, v1 + 1];
                    if ((arrayPO[18, v1 + 1] == "") || (arrayPO[17, v1 + 1] == arrayPO[18, v1 + 1]))
                        arrayPO[15, v1 + 1] = "N";
                }
            } // end not-first-time
        }

        v7 = 0;

        // for (v1 = arrPOlong; v1 < arrPOlong; v1++)  // from head
        for (v1 = 0; v1 < arrPOlong; v1++)
        {

            if ((  arrayPO[15, v1 + 1] == "Y" ) || ( arrayPO[16, v1 + 1] == "" ) ) // need update or first-time
            {
            d1 = 0;
            t01 = arrayPO[1, v1 + 1];

            sql1 = " SELECT a.QUANTITY  QUANTITY, to_char(a.creation_date, 'yyyyMMdd') cdate,  to_char(a.ship_date, 'yyyyMMddHHmmssmm') ddate, "
            + " a.ship_to_country  ship_to_country, a.project  project , a.plant  plant, a.ship_to_customername ship_to_customername ,  "
            + "  a.customer_po  customer_po,    a.item_number item_number,  substr(a.item_number,3,3) model  "
            + "  from sap.cmcs_sfc_packing_lines_all a  where a.customer_po = '" + t01 + "' order by a.ship_date desc ";
            
            dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
            v3 = dt2.Tables[0].Rows.Count;
            if (v3 > 0)
            {
                d2 = 0;
                tmp1 = "";
                for (v4 = 0; v4 < v3; v4++)
                {
                    if (v4 == 0) // first time
                    {
                        // t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                        t03 = dt2.Tables[0].Rows[v4]["cdate"].ToString();
                        t04 = dt2.Tables[0].Rows[v4]["ddate"].ToString();
                        t05 = dt2.Tables[0].Rows[v4]["ship_to_country"].ToString();
                        t06 = dt2.Tables[0].Rows[v4]["plant"].ToString();
                        t07 = dt2.Tables[0].Rows[v4]["project"].ToString();
                        t08 = dt2.Tables[0].Rows[v4]["ship_to_customername"].ToString();
                        t09 = dt2.Tables[0].Rows[v4]["customer_po"].ToString();
                        t10 = dt2.Tables[0].Rows[v4]["item_number"].ToString();
                        t11 = dt2.Tables[0].Rows[v4]["model"].ToString();
                        // t12 = dt2.Tables[0].Rows[v4]["color"].ToString();                      
                    }

                    t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                    if (t02 != "") d2 = Convert.ToDecimal(t02);
                    d1 = d1 + d2;

                }

                t21 = d1.ToString();

                sql1 = " SELECT  b.color color, b.SW_VER SW_VER "
                + "  from sap.cmcs_sfc_packing_lines_all a,  shp.ROS_TCH_PN  b  where a.customer_po = '" + t01 + "' "
                + "  and  a.item_number = b.ppart   ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t12  = dt2.Tables[0].Rows[0]["color"].ToString();
                    t121 = dt2.Tables[0].Rows[0]["SW_VER"].ToString();
                }

                sql1 = " SELECT  customer_name brand "
                + "  from sfc.cmcs_sfc_model where substr(model,1,3)  = '" + t11 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t13 = dt2.Tables[0].Rows[0]["brand"].ToString();
                }

                tmpsqlW = "Update PUBLIB.CPOM1 set DELQUANTITY = '" + t21 + "', DDATE = '" + t04 + "', SITE = '" + t06 + "', BU = '" + t06 + "',  "
                + " CUSTOMER =  '" + t08 + "', SWVERSION =  '" + t121 + "', "
                + " CDATE = '" + t03 + "', COUNTRY = '" + t05 + "',  BRAND =  '" + t13 + "',"
                + " PO = '" + t09 + "', FLAG3 = '" + sp0 + "',  PROJ =  '" + t11 + "', COLOR =  '" + t12 + "', BOMPARTNO =  '" + t10 + "' "
                + " where PO  = '" + t01 + "'   ";
                v5 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                if (v5 <= 0) v5++;
                v7++;

                t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = ""; t12 = ""; t13 = "";

            } // end if ( arrayPO[15, v1 + 1] == "Y" )  // need update
            }  // end for 

        }


  //      return (Ret1);

        // Get DN
        sql1 = " SELECT  DN from PUBLIB.CDNM1 "
             + " where substr(CDATE, 1, 8 )  >= '" + tmp1Date + "' "
             + " and   substr(CDATE, 1, 8 )  <= '" + tmpDate + "'  "           
             + " order by DN desc ";
        // sap.cmcs_sfc_packing_lines_all ";
        //     // + "  AND to_char((last_update_date),'yyyyMMdd') >= '20101231' "
        //     // + "      to_char((last_update_date),'yyyyMMdd') <= '20110431'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);

        int arrDNlong = v3, arrDNwidth = 20;
        string[,] arrayDN = new string[arrDNwidth + 1, arrDNlong + 1];

        for (v1 = 0; v1 < arrDNlong; v1++)
            for (v2 = 0; v2 < arrDNwidth; v2++)
                arrayDN[v2, v1] = "";

        for (v1 = 0; v1 < arrDNlong; v1++)
        {
            arrayDN[1, v1 + 1] = dt1.Tables[0].Rows[v1]["DN"].ToString();  // PO
        }

        v7=0;
        for (v1 = 0; v1 < arrDNlong; v1++)
        {
            d1 = 0;
            t01 = arrayDN[1, v1 + 1];

             sql1 = " SELECT a.QUANTITY  QUANTITY, to_char(a.creation_date, 'yyyyMMdd') cdate,  to_char(a.ship_date, 'yyyyMMddHHmmssmm') ddate, "
             + " a.ship_to_country  ship_to_country, a.project  project , a.plant  plant, a.ship_to_customername ship_to_customername ,  "
             + "  a.customer_po  customer_po,    a.item_number item_number,  substr(a.item_number,3,3) model  "
             + "  from sap.cmcs_sfc_packing_lines_all a  where a.invoice_number = '" + t01 + "' ";
            

            // sql1 = " SELECT a.QUANTITY  QUANTITY, to_char(a.creation_date, 'yyyyMMdd') cdate,  to_char(a.ship_date, 'yyyyMMddHHmmssmm') ddate, "
            // + " a.ship_to_country  ship_to_country, a.project  project , a.plant  plant, a.ship_to_customername ship_to_customername ,  "
            // + "  a.customer_po  customer_po,    a.item_number item_number,  substr(a.item_number,3,3) model, b.color color "
            // + "  from sap.cmcs_sfc_packing_lines_all a,  shp.ROS_TCH_PN  b  where a.invoice_number = '" + t01 + "' "
            // + "  and  a.item_number = b.ppart   ";
            dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
            v3 = dt2.Tables[0].Rows.Count;
            if (v3 > 0)
            {
                d2 = 0;
                for (v4 = 0; v4 < v3; v4++)
                {
                    if (v4 == 0) // first time
                    {
                        // t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                        t03 = dt2.Tables[0].Rows[v4]["cdate"].ToString();
                        t04 = dt2.Tables[0].Rows[v4]["ddate"].ToString();
                        t05 = dt2.Tables[0].Rows[v4]["ship_to_country"].ToString();
                        t06 = dt2.Tables[0].Rows[v4]["plant"].ToString();
                        t07 = dt2.Tables[0].Rows[v4]["project"].ToString();
                        t08 = dt2.Tables[0].Rows[v4]["ship_to_customername"].ToString();
                        t09 = dt2.Tables[0].Rows[v4]["customer_po"].ToString();
                        t10 = dt2.Tables[0].Rows[v4]["item_number"].ToString();
                        t11 = dt2.Tables[0].Rows[v4]["model"].ToString();
                        // t12 = dt2.Tables[0].Rows[v4]["color"].ToString();
                    }

                    t02 = dt2.Tables[0].Rows[v4]["QUANTITY"].ToString();  // QUANTITY
                    if (t02 != "") d2 = Convert.ToDecimal(t02);
                    d1 = d1 + d2;                  

                }

                t21 = d1.ToString();

                sql1 = " SELECT  b.color color, b.SW_VER SW_VER "
                + "  from sap.cmcs_sfc_packing_lines_all a,  shp.ROS_TCH_PN  b  where a.invoice_number = '" + t01 + "' "
                + "  and  a.item_number = b.ppart   ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t12 = dt2.Tables[0].Rows[0]["color"].ToString();
                    t121 = dt2.Tables[0].Rows[0]["SW_VER"].ToString();
                }

                sql1 = " SELECT  customer_name brand "
                + "  from sfc.cmcs_sfc_model where substr(model,1,3)  = '" + t11 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1); // 211
                v3 = dt2.Tables[0].Rows.Count;
                if (v3 > 0)
                {
                    t13 = dt2.Tables[0].Rows[0]["brand"].ToString();
                }

                tmpsqlW = "Update PUBLIB.CDNM1 set DELQUANTITY = '" + t21 + "', DDATE = '" + t04 + "', SITE = '" + t06 + "', BU = '" + t06 + "',  "
                + " CUSTOMER =  '" + t08 + "', SWVERSION =  '" + t121 + "', "
                + " CDATE = '" + t03 + "', COUNTRY = '" + t05 + "',  BRAND =  '" + t13 + "',"
                + " PO = '" + t09 + "', FLAG3 = '" + sp0 + "',  PROJ =  '" + t11 + "', COLOR =  '" + t12 + "', BOMPARTNO =  '" + t10 + "' "
                + " where DN  = '" + t01 + "'   ";
                v5 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                if (v5 <= 0) v5++;
                v7++;

                t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = ""; t12 = ""; t13="";
            }  // end for 

        }


        return (Ret1);        
       
    }    


    /////////////////
    // select  distinct(customer_po), ship_date   from SAP.CMCS_SFC_PACKING_LINES_ALL  where SHIP_DATE in ( SELECT max( SHIP_DATE )
    // FROM SAP.CMCS_SFC_PACKING_LINES_ALL  group by customer_po ) order  by customer_po desc 
    ////////////////
    public string CMDelPOData(string ptype, string dbtype, string DBRead, string DBWri, string daycnts)  
    {
        string Ret1 = "", sql1 = "", tmpsqlW="", sp="", sp0="0", tmpselwri = "";
        DataSet dt1 = null, dt2 = null;
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0, v6=0, v7=0, preday = Convert.ToInt32(daycnts);
        string t01 = "", t02 = "", t03 = "", t04 = "", t05 = "", t06 = "", t07 = "", t08 = "", t09 = "", t10 = "", t11 = "", t12 = "", t13 = "", t14 = "", t15 = "";
        string tmpDate  = DateTime.Today.ToString("yyyyMMdd");
        string tmp1Date = DateTime.Today.AddDays( preday ) .ToString("yyyyMMdd"); 
        // GET DN
        sql1 = " SELECT  distinct( INVOICE_NUMBER)  from sap.cmcs_sfc_packing_lines_all   "
              + " where to_char((creation_date),'yyyyMMdd') >= '" + tmp1Date + "' "
              + " and   to_char((creation_date),'yyyyMMdd') <= '" + tmpDate + "'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        //if (v3 <= 0) return (Ret1);
        for (v1 = 0; v1 < v3; v1++)
        {
            //t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString("yyyyMMddHHmmssmm");   
            //  t01 = Convert.ToDateTime(dt1.Tables[0].Rows[v1]["emeidate"].ToString()).ToString("yyyyMMddHHmmssmm");
            t01 = dt1.Tables[0].Rows[v1]["INVOICE_NUMBER"].ToString();  // PO

            if ((t01 != "") && (t01 != ""))
            {
                tmpselwri = "select * from  PUBLIB.CDNM1 where DN =  '" + t01 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 <= 0) // not int 221
                {
                    tmpsqlW = "Insert into PUBLIB.CDNM1 ( DN, FLAG1 , FLAG2, FLAG3, FLAG4) Values "
                       + " ( '" + t01 + "', '" + sp + "', '" + sp + "', '" + sp + "', '" + sp + "' ) ";
                    v4 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                    if (v4 <= 0) v5++;
                }

            }
            t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = "";
            t12 = ""; t13 = ""; t14 = ""; t15 = "";
        }

        // return (Ret1);

         // GET CUSTOMER_PO
        sql1 = " SELECT  distinct(CUSTOMER_PO) from sap.cmcs_sfc_packing_lines_all "
             + " where to_char((creation_date),'yyyyMMdd') >= '" + tmp1Date + "' "
             + " and to_char((creation_date),'yyyyMMdd') <= '" + tmpDate + "'  ";
        //     // + "  AND to_char((last_update_date),'yyyyMMdd') >= '20101231' "
        //     // + "      to_char((last_update_date),'yyyyMMdd') <= '20110431'  ";
        //  + " order by last_update_date  ";
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        // if (v3 <= 0)  return (Ret1);
        v5 = 0;
        for (v1 = 0; v1 < v3; v1++)
        {
            //t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString("yyyyMMddHHmmssmm");   
            //  t01 = Convert.ToDateTime(dt1.Tables[0].Rows[v1]["emeidate"].ToString()).ToString("yyyyMMddHHmmssmm");
            t01 = dt1.Tables[0].Rows[v1]["CUSTOMER_PO"].ToString();  // PO
            
            if ((t01 != "") && (t01 != ""))
            {
                tmpselwri = "select * from  PUBLIB.CPOM1 where PO =  '" + t01 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 <= 0) // not int 221
                {
                    tmpsqlW = "Insert into PUBLIB.CPOM1 ( PO, FLAG1 , FLAG2, FLAG3, FLAG4) Values "
                       + " ( '" + t01 + "', '" + sp + "', '" + sp + "', '" + sp + "', '" + sp + "' ) ";
                    v4 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                    if (v4 <= 0) v5++;
                }

            }
            t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = "";
            t12 = ""; t13 = ""; t14 = ""; t15 = "";
        }

        // return (Ret1);
       

        // Get EMEI

//        sql1 = " SELECT  to_char(a.last_update_date, 'yyyyMMddHHmmssmm') emeidate, to_char(b.last_update_date, 'yyyyMMddHHmmssmm') packdate, "
//       + " to_char(e.DDATE, 'yyyyMMddHHmmssmm') DDATE, 'FIHMLXTJ' site,  "
//       + " a.imeinum imei, b.customer_po po, b.invoice_number dn, "
//       + " to_char(b.last_update_date, 'yyyyMMdd') datetime, c.customer_name brand, "
//       + " b.ship_to_customername customer, c.model project, "
//       + " b.ship_to_country country, '' sw, '1' quantity, d.color color, "
//       + " '' charger, '' bompartno, a.product_id pid, '' flag1, '' flag2, '' flag3   "
//       + " FROM shp.cmcs_sfc_imeinum a,  "
//       + " sap.cmcs_sfc_packing_lines_all b, "
//       + " sfc.cmcs_sfc_model c, "
//       + " shp.ros_tch_pn d, "
//       + " shp.cmcs_sfc_shipping_data e "
//       + " WHERE a.imeinum = e.imei "
//       + "    AND e.carton_no = b.internal_carton "
//       + " AND e.model = c.model "
//       + "  AND a.ppart = d.ppart "
//       + "  AND e.DDATE >= to_date('20101215', 'yyyyMMdd') "
//       + "  AND e.DDATE <= to_date('20110415', 'yyyyMMdd') ";
//       //+ "  AND to_char((e.DDATE),'yyyyMMdd') >= '20101231' "
//       //+ "  AND to_char((e.DDATE),'yyyyMMdd') <= '20110431'  ";
       
        sql1 = " SELECT  to_char(a.last_update_date, 'yyyyMMddHHmmssmm') emeidate, to_char(b.last_update_date, 'yyyyMMddHHmmssmm') packdate, "
     + " to_char(e.DDATE, 'yyyyMMddHHmmssmm') DDATE, 'FIHMLXTJ' site,  "
     + " a.imeinum imei, b.customer_po po, b.invoice_number dn, "
     + " to_char(b.last_update_date, 'yyyyMMdd') datetime, c.customer_name brand, "
     + " b.ship_to_customername customer, c.model project, "
     + " b.ship_to_country country,  d.SW_VER swversion, '1' quantity, d.color color, "
     + " '' charger, '' bompartno, a.product_id pid, '' flag1, '' flag2, '' flag3   "
     + " FROM shp.cmcs_sfc_imeinum a,  "
     + " sap.cmcs_sfc_packing_lines_all b, "
     + " sfc.cmcs_sfc_model c, "
     + " shp.ros_tch_pn d, "
     + " shp.cmcs_sfc_shipping_data e "
     + " WHERE a.imeinum = e.imei "
     + " and e.carton_no = b.internal_carton "
     + " and e.model = c.model "
     + " and a.ppart = d.ppart "
     + " and to_char((b.CREATION_DATE),'yyyyMMdd') >= '" + tmp1Date + "' "
     + " and to_char((b.CREATION_DATE),'yyyyMMdd') <= '" + tmpDate + "'  ";
     //+ "  AND e.DDATE >= to_date('20130115', 'yyyyMMdd') "
     //+ "  AND e.DDATE <= to_date('20130425', 'yyyyMMdd') ";
        //+ "  AND to_char((e.DDATE),'yyyyMMdd') >= '20101231' "
        //+ "  AND to_char((e.DDATE),'yyyyMMdd') <= '20110431'  ";

        // -- AND a.imeinum in ('356384041593821')
        dt1 = DataBaseOperation.SelectSQLDS(dbtype, DBRead, sql1);
        v3 = dt1.Tables[0].Rows.Count;
        if (v3 <= 0) return (Ret1);
        t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = ""; t12 = ""; t13 = ""; t14 = ""; t15 = "";
        v5 = 0;
        for (v1 = 0; v1 < v3; v1++)
        {
            //t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString("yyyyMMddHHmmssmm");   
            //  t01 = Convert.ToDateTime(dt1.Tables[0].Rows[v1]["emeidate"].ToString()).ToString("yyyyMMddHHmmssmm");
            t01 = dt1.Tables[0].Rows[v1]["emeidate"].ToString();
            t02 = dt1.Tables[0].Rows[v1]["packdate"].ToString();
            t03 = dt1.Tables[0].Rows[v1]["DDATE"].ToString();
            t04 = dt1.Tables[0].Rows[v1]["site"].ToString();
            t05 = dt1.Tables[0].Rows[v1]["imei"].ToString();
            t06 = dt1.Tables[0].Rows[v1]["po"].ToString();
            t07 = dt1.Tables[0].Rows[v1]["dn"].ToString();
            t08 = dt1.Tables[0].Rows[v1]["datetime"].ToString();
            t09 = dt1.Tables[0].Rows[v1]["brand"].ToString();
            t10 = dt1.Tables[0].Rows[v1]["customer"].ToString();
            t11 = dt1.Tables[0].Rows[v1]["project"].ToString();
            t12 = dt1.Tables[0].Rows[v1]["country"].ToString();
            t13 = dt1.Tables[0].Rows[v1]["color"].ToString();
            t14 = dt1.Tables[0].Rows[v1]["pid"].ToString();
            t15 = dt1.Tables[0].Rows[v1]["swversion"].ToString();




            if ((t05 != "") && (t06 != ""))
            {
                tmpselwri = "select * from  PUBLIB.CEMEI1 where IMEI =  '" + t05 + "' ";
                dt2 = DataBaseOperation.SelectSQLDS(dbtype, DBWri, tmpselwri);
                v7 = dt2.Tables[0].Rows.Count;
                if (v7 <= 0) // not int 221
                {
                    tmpsqlW = "Insert into PUBLIB.CEMEI1 ( RDATE, DDATE, SITE, BU, IMEI, PO, DN, CDATE, PID, FLAG1 , FLAG2, FLAG3, FLAG4, COUNTRY, SWVERSION ) "
                    + " Values ( '" + sp + "', '" + t03 + "', '" + t04 + "', '" + t04 + "', '" + t05 + "',  "
                    + "  '" + t06 + "', '" + t07 + "', '" + t08 + "', '" + t14 + "', '" + sp + "',  "
                    + "  '" + sp + "', '" + sp0 + "', '" + sp + "' , '" + t12 + "' , '" + t15 + "' ) ";
                    v4 = DataBaseOperation.ExecSQL("oracle", DBWri, tmpsqlW);
                    if (v4 <= 0) v5++;
                }

            }


            t01 = ""; t02 = ""; t03 = ""; t04 = ""; t05 = ""; t06 = ""; t07 = ""; t08 = ""; t09 = ""; t10 = ""; t11 = "";
            t12 = ""; t13 = ""; t14 = ""; t15 = "";

            // if (v1 >= limno) v1 = v3; // break
        }
        

        return (Ret1);
    }

    /////////////////////////////////////////////////////////////////////////////////
    //    arr1[v2 + 1, 01] = DNdt1.Tables[0].Rows[v2]["DATABOMID"].ToString();
    //    arr1[v2 + 1, 02] = DNdt1.Tables[0].Rows[v2]["SITE"].ToString();
    //    arr1[v2 + 1, 03] = DNdt1.Tables[0].Rows[v2]["MODEL"].ToString();
    //    arr1[v2 + 1, 04] = DNdt1.Tables[0].Rows[v2]["ITEM"].ToString();
    //    arr1[v2 + 1, 05] = DNdt1.Tables[0].Rows[v2]["SEQ"].ToString();
    //    arr1[v2 + 1, 06] = DNdt1.Tables[0].Rows[v2]["DUEDATE"].ToString();
    //    arr1[v2 + 1, 07] = DNdt1.Tables[0].Rows[v2]["DTYPE"].ToString();
    //    arr1[v2 + 1, 08] = DNdt1.Tables[0].Rows[v2]["CSTATUS"].ToString();
    //    arr1[v2 + 1, 09] = DNdt1.Tables[0].Rows[v2]["UPTIME"].ToString();
    //    arr1[v2 + 1, 10] = DNdt1.Tables[0].Rows[v2]["FLAG1"].ToString();
    //    arr1[v2 + 1, 11] = DNdt1.Tables[0].Rows[v2]["FLAG2"].ToString();
    //    arr1[v2 + 1, 12] = DNdt1.Tables[0].Rows[v2]["FLAG3"].ToString();
    //    arr1[v2 + 1, 13] = DNdt1.Tables[0].Rows[v2]["FLAG4"].ToString();
    //    arr1[v2 + 1, 14] = DNdt1.Tables[0].Rows[v2]["FLAG5"].ToString();
    //    arr1[v2 + 1, 15] = DNdt1.Tables[0].Rows[v2]["FLAG6"].ToString();
    //    arr1[v2 + 1, 16] = DNdt1.Tables[0].Rows[v2]["FLAG7"].ToString();
    //    arr1[v2 + 1, 17] = DNdt1.Tables[0].Rows[v2]["FLAG8"].ToString();
    //    arr1[v2 + 1, 18] = DNdt1.Tables[0].Rows[v2]["FLAG9"].ToString();
    //    arr1[v2 + 1, 19] = DNdt1.Tables[0].Rows[v2]["FLAG10"].ToString();
    //    arr1[v2 + 1, 20] = DNdt1.Tables[0].Rows[v2]["FLAG11"].ToString();

    public string FGISBroadcast1(string tdate, string tmpDATABOMID, ref string [,] arr1, int arrloc)
    {
        // string Ret1 = "http://10.250.144.106/FuseXML/DnXml.aspx?DN=83065724";
        // Response.Redirect(Ret1);
        if (tmpDATABOMID == "") return ("");

        string Ret1 = "";

        switch (tmpDATABOMID)
        {
            case "MS101": Ret1 = "http://" + arr1[arrloc + 1, 04].ToString().Trim() + "/FGISWeb/MainCCVAN.aspx?5A5A=10401"; 
                          break;
            case "2": Ret1 =  "MAILGROUP";
                          break;
            case "7": Ret1 = "B2B";
                          break;
            default : Ret1 = "";
                          break;
        }


        return (Ret1);
        
    }

    // check Messlog file and recall Server running
    public string RecallAutoPrg(string Cmdno, string tmpF11, string tdate, string DBType, string DBAStr )
    {

        if (Cmdno == "") return ("-1");
       
        string t1 = "", t2 = "", t3 = "", t4="";
        int v1=0, v2=0, v3=0;
        DataSet DNdt1 = null;
        string Sqlr = " select *  from [ERPDBF].[dbo].[MessLog] where F2 = '" + Cmdno + "' and F11 = '" + tmpF11 + "' order  by F0 desc  ";
        DNdt1 = PDataBaseOperation.PSelectSQLDS(DBType, DBAStr, Sqlr);
        if (DNdt1 == null) return ("-1");
        if (DNdt1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DNdt1.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");
        
        t1 = DNdt1.Tables[0].Rows[0]["F4"].ToString().Trim();

        if (t1.Length >= 12) t2 = t1.Substring(8, 4);
        t3 = DateTime.Now.ToString("yyyyMMddHHmmssmm").Substring(8,4);

        v2 = ( Convert.ToInt32(t2.Substring(0,2)) * 60 ) + Convert.ToInt32(t2.Substring(2,2)); // hr + 60 + min = total min
        v3 = ( Convert.ToInt32(t3.Substring(0,2)) * 60 ) + Convert.ToInt32(t3.Substring(2,2)); // hr + 60 + min = total min

        if ((v3 - v2) > 120) t4 = "Y"; 

        return ( t4 );

    }  // end 

    public string GetSNFromRecv02V01(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpTAC)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10", t7 = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6 = 0, v7=0, v8=0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";

        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where FLAG6 = '2' and F40 = '1' and F1 ='NOKFOXT1021'   and F52 = '" + tmpTAC + "'  order by documentid, SEQID asc ";
        else
            Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where FLAG6 = '2' and F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 = '" + tmpTAC + "'  order by documentid, SEQID asc ";

        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-1");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-1");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock = "", L21Factory = "", L21CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L21SEQID = "";
        string ProductionLimit = "3";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 5;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L21CSTATUS = DN21.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L21SEQID = DN21.Tables[0].Rows[v2]["SEQID"].ToString().Trim();

            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
                SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital

            if (L21SIZE == "") L10SIZE = "0";

            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            v6 = Req21SIZEQty;
            if ((L21CSTATUS == "1") || (L21CSTATUS == ""))
            {
                t6 = t6;
            }
            else // CSTATUS = '2,3,4'
            {
                //if (L21CSTATUS == ProductionLimit)  // Chk Production Limit
                //{
                //    v6 = ChkReqProdLimitV02(L21SEQID, tDocumentid, DBType, tmpReadDBA, "ENGITTRS1011", DBType, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", DN21.Tables[0].Rows[v2]["F44"].ToString().Trim(), DN21.Tables[0].Rows[v2]["F48"].ToString().Trim(), Req21SIZEQty.ToString().Trim(), L21SEQID); // t21F44, t21F48, v5);
                //    if (v6 != Req21SIZEQty)  // Not enough Qty
                //    {   //  SIZE, Used, UnUsed
                //        //  100   80    20   --> 只給 8 個
                //        //  20+8  8     20   ( SIZE = UnUsed + v6, Used = v6, unUsed = UnUsed
                //        Req21SIZEQty = v6;
                //        DN21.Tables[0].Rows[v2]["F24"] = (v6 + tmpunused).ToString().Trim(); // SIZE
                //        L21SIZE = (v6 + tmpunused).ToString().Trim(); // SIZE
                //       DN21.Tables[0].Rows[v2]["F26"] = (v6).ToString().Trim(); // Used
                //    }
                //
                //}

                v3 = 0;
                while ((Req21SIZEQty > 0) && (GetCNT > v3))
                {
                    Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");

                    t4 = GetSN_FromRecvSupplier(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                    if (Convert.ToInt32(t4) > 0)
                    {
                        t4 = GetSN_FromRecvBy1V02(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                        if ((t4 != "") && (t4 != "-1") && (t4 != "-2"))
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    }  // end if 
                    else
                    {
                        t7 = "No:" + L21SNBlock + " " + L21SN;
                        t3 = " Update  ENGITMGT.ENGITTRS1011 set FLAG7 = '" + t7 + "', F40 = '2', updatetime = '" + Currtime + "' "
                           + " where documentid = '" + L21DocumentID + "' and docseq = '" + L21DOCSEQ + "' and F1 = 'NOKFOXT1021'  and  SEQID = '" + L21SEQID + "'";
                        v8 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                    }
                    v3++;
                }   // end while
            }  // end if if ((L21CSTATUS == "1") || (L21CSTATUS == ""))

        }  // end  for loop

        return ("");


    }  // end GetSN V02
    //  standard 
    public string GetSNFromRecv02(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpTAC)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10", t7="";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6=0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";

        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021'   and F52 = '" + tmpTAC + "'  order by documentid, SEQID asc ";
        else
            Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 = '" + tmpTAC + "'  order by documentid, SEQID asc ";

        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-1");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-1");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock = "", L21Factory = "", L21CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L21SEQID = "";
        string ProductionLimit = "3";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 10;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L21CSTATUS = DN21.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L21SEQID   = DN21.Tables[0].Rows[v2]["SEQID"].ToString().Trim();

            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
                SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital

            if (L21SIZE == "") L10SIZE = "0";

            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            v6 = Req21SIZEQty;
            if ((L21CSTATUS == "1") || (L21CSTATUS == ""))
            {
                t6 = t6;
            }
            else // CSTATUS = '2,3,4'
            {
                if ( L21CSTATUS == ProductionLimit )  // Chk Production Limit
                {
                    v6 = ChkReqProdLimitV02(L21SEQID, tDocumentid, DBType, tmpReadDBA, "ENGITTRS1011", DBType, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", DN21.Tables[0].Rows[v2]["F44"].ToString().Trim(), DN21.Tables[0].Rows[v2]["F48"].ToString().Trim(), Req21SIZEQty.ToString().Trim(), L21SEQID ); // t21F44, t21F48, v5);
                    if (v6 != Req21SIZEQty)  // Not enough Qty
                    {   //  SIZE, Used, UnUsed
                        //  100   80    20   --> 只給 8 個
                        //  20+8  8     20   ( SIZE = UnUsed + v6, Used = v6, unUsed = UnUsed
                        Req21SIZEQty = v6;
                        DN21.Tables[0].Rows[v2]["F24"] = (v6 + tmpunused).ToString().Trim(); // SIZE
                        L21SIZE = (v6 + tmpunused).ToString().Trim(); // SIZE
                        DN21.Tables[0].Rows[v2]["F26"] = (v6).ToString().Trim(); // Used
                    }
                   
                }

                v3 = 0;
                while ((Req21SIZEQty > 0) && (GetCNT > v3))
                {
                    Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
            
                    t4 = GetSN_FromRecvSupplier(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                    if (Convert.ToInt32(t4) > 0)
                    {
                        t4 = GetSN_FromRecvBy1V02(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                        if ( (t4 != "") && ( t4 != "-1" ) && ( t4 != "-2" ) ) 
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    }  // end if 
                    v3++;
                }   // end while
            }  // end if if ((L21CSTATUS == "1") || (L21CSTATUS == ""))

        }  // end  for loop

        return ("");


    }  // end GetSN V02



    
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 20170609
    // 1. Read 10 production limit  
    // 2. Read each group flag6 ( 10 ) still not process
    // 3. Read ENGITMGT.ENGITTRS1011 TD F18 數量, F20 已用, Total Qty 22 limit qty, 
    // 4. 還可用 = F18 - F20 = UnUsedQty
    // 4. if F20  = 0 UPdate F40 = '2' , Update Flag6 = '2'
    // 5. if Req ( UnUsedQty > Req ) UPdate SIZE = ENGITMGT.ENGITTRS1011 F20 = F20 + Req SiZE, Update Flag6 = '2'
    // 6. else
    //       UPdate SIZE = F20 - > ENGITMGT.ENGITTRS1011 F24 = UNUsedQty, Update Flag6 = '2' ( 10 )
    //       Update SIZE = UNUsedQty * ( Flag5 ) = UNUsedQty * ( SN 數量 )  
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string BookProductlimitQty(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpTAC, string tmpProto)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6 = 0, v7 = 0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null,  DN20= null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", RunType = "";

        // select *  from ENGITMGT.ENGITTRS1011 where  f1 = 'NOKFOXT1020'   and F16 = '3'  and flag6 is null order  by  seqid asc --   where  f1 = 'NOKFOXT1021'

        // First Time 
        //if (tDocumentid == "") Sqlr1 = " select  distinct(F6), seqid,f16,f40,  F24, f44, f46, flag5     from ENGITMGT.ENGITTRS1011   "
        //    + " where  f1 = 'NOKFOXT1021'  and f16 = '3'  and f40 = '1' and FLAG5 is null order  by f6 ";
        //else
        //    Sqlr1 = "select  distinct(F6), seqid,f16,f40,  F24, f44, f46, flag5     from ENGITMGT.ENGITTRS1011  "
        //          + "where  f1 = 'NOKFOXT1021'  and f16 = '3'  and f40 = '1' and FLAG5 is null and documentid = '" + tDocumentid + "' order  by f6 asc ";


        t2 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where flag6 is null and F16 != '3' and F1 = 'NOKFOXT1020'   ";
        t3 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where flag6 is null and F16 != '3' and F1 = 'NOKFOXT1021' ";
        v4 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t2);
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);


        // Second Time 
        if (tDocumentid == "") Sqlr1 = " select *  from ENGITMGT.ENGITTRS1011 where  f1 = 'NOKFOXT1020' and F40 = '1'  and F16 = '3'  and flag6 is null order  by  seqid asc ";
        else
            Sqlr1 = "select *  from ENGITMGT.ENGITTRS1011 where  f1 = 'NOKFOXT1020' and F40 = '1'  and F16 = '3'  and flag6 is null and documentid = '" + tDocumentid + "' order  by  seqid asc ";
        
        DN20 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN20 == null) return ("-1");
        if (DN20.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN20.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L20DocumentID = "", L20DOCSEQ = "", L20SIZE = "", L20SN = "", L20UnUsed = "", L20SNBlock = "", L20Factory = "", L20CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L20SEQID = "";
        string L20TD = "", L20Provider = "";
        string ProductionLimit = "3";
        int TDLimitQty = 0, TDToTLimitQty = 0, L21SNCnt = 1, TDUsedQty = 0, UnUsedQty = 0, NewSIZE = 0, L20SIZEQty = 0;

        string M1001F18LimitQTY = "";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 10;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L20DocumentID = DN20.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L20DOCSEQ = DN20.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L20SIZE = DN20.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L20SN = DN20.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L20UnUsed = DN20.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L20SNBlock = DN20.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L20Factory = DN20.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L20CSTATUS = DN20.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L20SEQID = DN20.Tables[0].Rows[v2]["SEQID"].ToString().Trim();
            L20TD       = DN20.Tables[0].Rows[v2]["F44"].ToString().Trim();
            L20Provider = DN20.Tables[0].Rows[v2]["F46"].ToString().Trim();


            if (L20SIZE != "") L20SIZEQty = Convert.ToInt32(L20SIZE);
            else L20SIZEQty = 0;

            TDLimitQty = 0; TDToTLimitQty = 0; UnUsedQty = 0; RunType = ""; NewSIZE = 0;
            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOKFOM1002' and F6 = '" + L20TD + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
            {
                //if (DNTMP.Tables[0].Rows[0]["F22"].ToString().Trim() != "" )
                //    TDLimitQty = Convert.ToInt32(DNTMP.Tables[0].Rows[0]["F22"].ToString().Trim());

                //if (DNTMP.Tables[0].Rows[0]["F18"].ToString().Trim() != "")
                //    TDToTLimitQty = Convert.ToInt32(DNTMP.Tables[0].Rows[0]["F18"].ToString().Trim());

                if (DNTMP.Tables[0].Rows[0]["F22"].ToString().Trim() != "")
                    TDToTLimitQty = Convert.ToInt32(DNTMP.Tables[0].Rows[0]["F22"].ToString().Trim());

                if (DNTMP.Tables[0].Rows[0]["F20"].ToString().Trim() != "" )
                    TDUsedQty = Convert.ToInt32(DNTMP.Tables[0].Rows[0]["F20"].ToString().Trim());

                if ((TDLimitQty != 0) && (TDToTLimitQty != 0))
                    L21SNCnt = TDToTLimitQty / TDLimitQty;


                // M1001F18LimitQTY = DNTMP.Tables[0].Rows[0]["F18"].ToString().Trim();
                M1001F18LimitQTY = DNTMP.Tables[0].Rows[0]["F22"].ToString().Trim();
            }
               
            //SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital


            UnUsedQty = TDToTLimitQty -  TDUsedQty;

            NewSIZE = L20SIZEQty;

            if ((M1001F18LimitQTY == "") || (M1001F18LimitQTY == "0") || (TDToTLimitQty == 0))  // 無限制 Not Limit
            {
                  // update 20 , F20 = F20 + F24
                t2 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where SEQID = '" + L20SEQID + "'  and F1 = 'NOKFOXT1020'  and DocumentID = '" + L20DocumentID + "'  ";
                t3 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where   F34 =  '" + L20DocumentID + "'  and F36 =  '" + L20DOCSEQ + "' and F1 = 'NOKFOXT1021' ";
                v4 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t2);
                v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                RunType = "Y";
            }
            else
            if (UnUsedQty == 0)  //  沒可使用 F18 = F20 
            {
                t2 = "update ENGITMGT.ENGITTRS1011 set flag7 = 'No Limit Qty', flag6  = '2', f40 = '3' where F40 = '1' and SEQID = '" + L20SEQID + "'  and F1 = 'NOKFOXT1020'  and DocumentID = '" + L20DocumentID + "' ";
                t3 = "update ENGITMGT.ENGITTRS1011 set flag7 = 'No Limit Qty', flag6  = '2', f40 = '3' where F40 = '1' and F34 =  '" + L20DocumentID + "'  and F36 =  '" + L20DOCSEQ + "' and F1 = 'NOKFOXT1021' ";
                v4 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t2);
                v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                RunType = "";
            }
            else
            if ( UnUsedQty >= L20SIZEQty  ) // 夠用
            {
                  // update 20 , F20 = F20 + F24
                  t2 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where SEQID = '" + L20SEQID + "'  and F1 = 'NOKFOXT1020'  and DocumentID = '" + L20DocumentID + "' ";
                  t3 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where   F34 =  '" + L20DocumentID + "'  and F36 =  '" + L20DOCSEQ + "' and F1 = 'NOKFOXT1021' ";
                  t4 = "update ENGITMGT.ENGITM1001   set F20 = F20 + " + L20SIZEQty + " where f1 = 'NOKFOM1002' and F6 = '" + L20TD + "' ";
                
                  v4 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t2);
                  v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                  v6 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);

                  RunType = "Y";
            }
            else
            if ( UnUsedQty < L20SIZEQty )  // SIZE > F18 - F20
            {
                  // 1020 
                  //          SIZE F24 = F18 - F20       
                  // F20 = F20 + F24  
                  // update 20 ZISE = NewSize
                  // update 21 SIZE = NewSize * Flag5
                  //  "Update ENGITMGT.ENGITTRS1011  set F24 =  to_number(F28) + " + v6 + "  where F1 = 'NOKFOXT1021'  and SEQID = '" + tSEQID21 + "'   ";

                  NewSIZE = UnUsedQty;

                  t2 = "update ENGITMGT.ENGITTRS1011 set flag7 = 'Part of Limit Qty', flag6  = '2', F24 = '" + NewSIZE.ToString().Trim() + "' where F40 = '1' and SEQID = '" + L20SEQID + "'  and F1 = 'NOKFOXT1020'  and DocumentID = '" + L20DocumentID + "' ";
                  t3 = "update ENGITMGT.ENGITTRS1011 set flag7 = 'Part of Limit Qty', flag6  = '2', F24 = ( " + NewSIZE + ") * to_number(FLAG5)    "
                     + " where F40 = '1' and   F34 =  '" + L20DocumentID + "'  and F36 =  '" + L20DOCSEQ + "' and F1 = 'NOKFOXT1021' ";
                  t4 = "update ENGITMGT.ENGITM1001   set F20 = F20 + " + NewSIZE + " where f1 = 'NOKFOM1002' and F6 = '" + L20TD + "' ";
              
                  v4 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t2);
                  v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                  v6 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);

                  RunType = "Y";
            }


            // Finish  Check Flag6 for production limit
            //t2 = "update ENGITMGT.ENGITTRS1011 set flag6  = '2' where SEQID = '" + L20SEQID + "'  and F1 = 'NOKFOXT1020'  and DocumentID = '" + L20DocumentID + "' ";
            //v4 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t2);
         
            //if (RunType == "Y")  // Run Production Limit
            //{
            //      string tmp21Documentid = "";
            //      Sqlr1 = "select *  from ENGITMGT.ENGITTRS1011 where f1 = 'NOKFOXT1021' and F40 = '1' and F34 = '" + L20DocumentID + "' and F36 = '" + L20DOCSEQ + "' order by seqid asc ";
            //      DN21= PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
            //      if (DN21 == null) return ("-1");
            //      if (DN21.Tables.Count <= 0) return ("-2");
            //
            //      if (DN21.Tables[0].Rows.Count >= 1)  // 找 1021
            //      {
            //          tmp21Documentid = DN21.Tables[0].Rows[0]["DOCUMENTID"].ToString().Trim();
            //          string DBOracle = DBType;
            //          string Ret15 = GetSNFromRecv12(tmp21Documentid, DBOracle, tmpReadDBA, "ENGITTRS1011", DBOracle, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", "TAC", "Proto");
            //          string Ret20 = GetSNFromRecv02(tmp21Documentid, DBOracle, tmpReadDBA, "ENGITTRS1011", DBOracle, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", "TAC");
            //          string Ret25 = GetSNFromRecv03(tmp21Documentid, DBOracle, tmpReadDBA, "ENGITTRS1011", DBOracle, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", "OUI");
            //      }
            //}

        }

        return("");


      


    }  // end GetSN V04

    // Proto Only
    // 20170610 ADD FLAG6 is check OK on 20, 21
    // TAC Provider = Proto , The Only Proto , Do not care TD, SN10 = Proto
    public string GetSNFromRecv01V01(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpTAC, string tmpProto)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10", t7="", t8="";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6 = 0, v7 = 0, v8=0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where FLAG6 = '2' and F40 = '1' and F1 ='NOKFOXT1021'   and F52 = '" + tmpTAC + "' and F48 = '" + tmpProto + "' order by documentid, SEQID asc ";
        else
            Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where FLAG6 = ‘2’ and F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 = '" + tmpTAC + "' and F48 = '" + tmpProto + "' order by documentid, SEQID asc ";

        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock = "", L21Factory = "", L21CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L21SEQID = "";
        string ProductionLimit = "3";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 10;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L21CSTATUS = DN21.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L21SEQID = DN21.Tables[0].Rows[v2]["SEQID"].ToString().Trim();


            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
                SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital

            if (L21SIZE == "") L10SIZE = "0";

            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            if ((L21CSTATUS == "1") || (L21CSTATUS == ""))
            {
                t6 = t6;
            }
            else // CSTATUS = '2,3,4'
            {
                // 20170610
                //if (L21CSTATUS == ProductionLimit)  // Chk Production Limit
                //{
                //    v6 = ChkReqProdLimitV02(L21SEQID, tDocumentid, DBType, tmpReadDBA, "ENGITTRS1011", DBType, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", DN21.Tables[0].Rows[v2]["F44"].ToString().Trim(), DN21.Tables[0].Rows[v2]["F48"].ToString().Trim(), Req21SIZEQty.ToString().Trim(), L21SEQID); // t21F44, t21F48, v5);
                //    if (v6 != Req21SIZEQty)  // Not enough Qty
                //    {   //  SIZE, Used, UnUsed
                //        //  100   80    20   --> 只給 8 個
                //        //  20+8  8     20   ( SIZE = UnUsed + v6, Used = v6, unUsed = UnUsed
                //        Req21SIZEQty = v6;
                //        DN21.Tables[0].Rows[v2]["F24"] = (v6 + tmpunused).ToString().Trim(); // SIZE
                //        L21SIZE = (v6 + tmpunused).ToString().Trim(); // SIZE
                //        DN21.Tables[0].Rows[v2]["F26"] = (v6).ToString().Trim(); // Used
                //    }
                //}
                v3 = 0;
                while ((Req21SIZEQty > 0) && (GetCNT > v3))
                {
                    Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                    t4 = GetSN_FromRecvSupplier12(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                    if (Convert.ToInt32(t4) > 0)
                    {
                        t4 = GetSN_FromRecvBy1V01(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                        if (t4 != "")
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    }  // end if
                    else
                    {
                        t7 = "No:" + L21SNBlock + " " + L21SN;
                        t3 = " Update  ENGITMGT.ENGITTRS1011 set FLAG7 = '" + t7 + "', F40 = '2', updatetime = '" + Currtime + "' "
                           + " where documentid = '" + L21DocumentID + "' and docseq = '" + L21DOCSEQ + "' and F1 = 'NOKFOXT1021'  and  SEQID = '" + L21SEQID + "'";
                        v8 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                    }
                    v3++;
                }   // end while
            }  // end if if ((L21CSTATUS == "1") || (L21CSTATUS == ""))

        }  // end  for loop

        return ("");


    }  // end GetSN V04

    // TAC Provider = Proto , The Only Proto , Do not care TD, SN10 = Proto
    public string GetSNFromRecv12(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpTAC, string tmpProto)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6=0, v7=0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021'   and F52 = '" + tmpTAC + "' and F48 = '" + tmpProto + "' order by documentid, SEQID asc ";
        else
            Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 = '" + tmpTAC + "' and F48 = '" + tmpProto + "' order by documentid, SEQID asc ";

        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock = "", L21Factory = "", L21CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L21SEQID = "";
        string ProductionLimit = "3";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 10;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L21CSTATUS = DN21.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L21SEQID = DN21.Tables[0].Rows[v2]["SEQID"].ToString().Trim();


            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
                SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital

            if (L21SIZE == "") L10SIZE = "0";

            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            if ((L21CSTATUS == "1") || (L21CSTATUS == ""))
            {
                t6 = t6;
            }
            else // CSTATUS = '2,3,4'
            {
                if (L21CSTATUS == ProductionLimit)  // Chk Production Limit
                {
                    v6 = ChkReqProdLimitV02(L21SEQID, tDocumentid, DBType, tmpReadDBA, "ENGITTRS1011", DBType, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", DN21.Tables[0].Rows[v2]["F44"].ToString().Trim(), DN21.Tables[0].Rows[v2]["F48"].ToString().Trim(), Req21SIZEQty.ToString().Trim(), L21SEQID); // t21F44, t21F48, v5);
                    if (v6 != Req21SIZEQty)  // Not enough Qty
                    {   //  SIZE, Used, UnUsed
                        //  100   80    20   --> 只給 8 個
                        //  20+8  8     20   ( SIZE = UnUsed + v6, Used = v6, unUsed = UnUsed
                        Req21SIZEQty = v6;
                        DN21.Tables[0].Rows[v2]["F24"] = (v6 + tmpunused).ToString().Trim(); // SIZE
                        L21SIZE = (v6 + tmpunused).ToString().Trim(); // SIZE
                        DN21.Tables[0].Rows[v2]["F26"] = (v6).ToString().Trim(); // Used
                    }
               }
               v3 = 0;
               while ((Req21SIZEQty > 0) && (GetCNT > v3))
                {
                    Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                    t4 = GetSN_FromRecvSupplier12(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                    if (Convert.ToInt32(t4) > 0)
                    {
                        t4 = GetSN_FromRecvBy1V01(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                        if (t4 != "")
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    }  // end if 
                    v3++;
                }   // end while
            }  // end if if ((L21CSTATUS == "1") || (L21CSTATUS == ""))

        }  // end  for loop

        return ("");


    }  // end GetSN V04

    // OUI
    public string GetSNFromRecv03V01(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpOUI)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10", t7 = "", t8 = "", t9 = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6 = 0, v7 = 0, v8 = 0, v9 = 0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";


        //20170601 if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and F52 = '" + tmpOUI + "' order by documentid, SEQID asc ";
        //else
        //    Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 = '" + tmpOUI + "' order by documentid, SEQID asc ";

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where FLAG6 = '2' and F40 = '1' and F1 ='NOKFOXT1021' and F52 != 'TAC' order by documentid, SEQID asc ";
        else
            Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where  FLAG6 = '2' and F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 != 'TAC'   order by documentid, SEQID asc ";


        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock = "", L21Factory = "", L21CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L21SEQID = "";
        string ProductionLimit = "3";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 10;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L21CSTATUS = DN21.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L21SEQID = DN21.Tables[0].Rows[v2]["SEQID"].ToString().Trim();

            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
                SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital

            if (L21SIZE == "") L10SIZE = "0";

            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            if ((L21CSTATUS == "1") || (L21CSTATUS == ""))
            {
                t6 = t6;
            }
            else // CSTATUS = '2,3,4'
            {
                //if (L21CSTATUS == ProductionLimit)  // Chk Production Limit
                //{
                //    v6 = ChkReqProdLimitV02(L21SEQID, tDocumentid, DBType, tmpReadDBA, "ENGITTRS1011", DBType, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", DN21.Tables[0].Rows[v2]["F44"].ToString().Trim(), DN21.Tables[0].Rows[v2]["F48"].ToString().Trim(), Req21SIZEQty.ToString().Trim(), L21SEQID); // t21F44, t21F48, v5);
                //    if (v6 != Req21SIZEQty)  // Not enough Qty
                //    {   //  SIZE, Used, UnUsed
                //        //  100   80    20   --> 只給 8 個
                //        //  20+8  8     20   ( SIZE = UnUsed + v6, Used = v6, unUsed = UnUsed
                //        Req21SIZEQty = v6;
                //        DN21.Tables[0].Rows[v2]["F24"] = (v6 + tmpunused).ToString().Trim(); // SIZE
                //        L21SIZE = (v6 + tmpunused).ToString().Trim(); // SIZE
                //        DN21.Tables[0].Rows[v2]["F26"] = (v6).ToString().Trim(); // Used
                //    }
                //}
                v3 = 0;
                while ((Req21SIZEQty > 0) && (GetCNT > v3))
                {
                    Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                    t4 = GetSN_FromRecvSupplier03(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                    if (Convert.ToInt32(t4) > 0)
                    {
                        t4 = GetSN_FromRecvBy1V03(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                        if (t4 != "")
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    }  // end if 
                    else
                    {
                        t7 = "No:" + L21SNBlock + " " + L21SN;
                        t3 = " Update  ENGITMGT.ENGITTRS1011 set FLAG7 = '" + t7 + "', F40 = '2', updatetime = '" + Currtime + "' "
                           + " where documentid = '" + L21DocumentID + "' and docseq = '" + L21DOCSEQ + "' and F1 = 'NOKFOXT1021'  and  SEQID = '" + L21SEQID + "'";
                        v8 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
                    }
                    v3++;
                }   // end while
            }  // end if if ((L21CSTATUS == "1") || (L21CSTATUS == ""))

        }  // end  for loop

        return ("");


    }  // end GetSN V03
    // OUI
    public string GetSNFromRecv03(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string tmpOUI)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0, v6=0, v7=0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";

      
        //20170601 if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and F52 = '" + tmpOUI + "' order by documentid, SEQID asc ";
        //else
        //    Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 = '" + tmpOUI + "' order by documentid, SEQID asc ";

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and F52 != 'TAC' order by documentid, SEQID asc ";
        else
            Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' and F52 != 'TAC'   order by documentid, SEQID asc ";


        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock = "", L21Factory = "", L21CSTATUS;
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "", L21SEQID = "";
        string ProductionLimit = "3";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT = 10;
        int tmpsize = 0, tmpunused = 0;
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim();
            L21CSTATUS = DN21.Tables[0].Rows[v2]["F16"].ToString().Trim();
            L21SEQID = DN21.Tables[0].Rows[v2]["SEQID"].ToString().Trim();

            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2");
            if (DNTMP.Tables[0].Rows.Count >= 1)
                SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital

            if (L21SIZE == "") L10SIZE = "0";

            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            if ((L21CSTATUS == "1") || (L21CSTATUS == ""))
            {
                t6 = t6;
            }
            else // CSTATUS = '2,3,4'
            {
                if (L21CSTATUS == ProductionLimit)  // Chk Production Limit
                {
                    v6 = ChkReqProdLimitV02(L21SEQID, tDocumentid, DBType, tmpReadDBA, "ENGITTRS1011", DBType, tmpWriDBA, "ENGITMGT", tmpdate, "NOKFOXT1020", "ENGITTRS1011", "", "ENGITMGT", "2", DN21.Tables[0].Rows[v2]["F44"].ToString().Trim(), DN21.Tables[0].Rows[v2]["F48"].ToString().Trim(), Req21SIZEQty.ToString().Trim(), L21SEQID); // t21F44, t21F48, v5);
                    if (v6 != Req21SIZEQty)  // Not enough Qty
                    {   //  SIZE, Used, UnUsed
                        //  100   80    20   --> 只給 8 個
                        //  20+8  8     20   ( SIZE = UnUsed + v6, Used = v6, unUsed = UnUsed
                        Req21SIZEQty = v6;
                        DN21.Tables[0].Rows[v2]["F24"] = (v6 + tmpunused).ToString().Trim(); // SIZE
                        L21SIZE = (v6 + tmpunused).ToString().Trim(); // SIZE
                        DN21.Tables[0].Rows[v2]["F26"] = (v6).ToString().Trim(); // Used
                    }
                }
                v3 = 0;
                while ((Req21SIZEQty > 0) && (GetCNT > v3))
                {
                    Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                    t4 = GetSN_FromRecvSupplier03(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                    if (Convert.ToInt32(t4) > 0)
                    {
                        t4 = GetSN_FromRecvBy1V03(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                        if (t4 != "")
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    }  // end if 
                    v3++;
                }   // end while
            }  // end if if ((L21CSTATUS == "1") || (L21CSTATUS == ""))

        }  // end  for loop

        return ("");


    }  // end GetSN V03

    public string GetSNFromRecv01(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "10";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, DNCnt1 = 0, DNCnt2 = 0, Req21SIZEQty = 0;
        string tmpF3 = "", tmpF4 = "", tmpF5 = "";

        Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        DataSet DNdt1 = null, DNdt2 = null, DNdt3 = null, DN21 = null, DNTMP = null;

        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "";

        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}

        if (tDocumentid == "") Sqlr1 = "  select *  from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' order by documentid, SEQID asc ";
        else 
        Sqlr1 = "select * from  ENGITMGT.ENGITTRS1011 where F40 = '1' and F1 ='NOKFOXT1021' and documentid = '" + tDocumentid + "' order by documentid, SEQID asc ";
        
        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN21.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int arrSN21long = v1, arrSN21width = 20;
        string[,] arrSN21 = new string[arrSN21long + 1, arrSN21width + 1];

        for (v3 = 0; v3 < arrSN21long; v3++)
            for (v2 = 0; v2 < arrSN21width; v2++)
                arrSN21[v3, v2] = "";

        string L10DocumentID = "", L10DOCSEQ = "", L10SIZE = "";
        string L21DocumentID = "", L21DOCSEQ = "", L21SIZE = "", L21SN = "", L21UnUsed = "", L21SNBlock="", L21Factory="";
        string L22DocumentID = "", L22DOCSEQ = "", L22SIZE = "";


        int v21 = 0, v22 = 0, v23 = 0, GetCNT=10;
        int tmpsize=0, tmpunused = 0; 
        string SNType = "TAC";

        for (v2 = 0; v2 < v1; v2++) // Firrst Level
        {
            L21DocumentID    = DN21.Tables[0].Rows[v2]["DOCUMENTID"].ToString().Trim();
            L21DOCSEQ        = DN21.Tables[0].Rows[v2]["DOCSEQ"].ToString().Trim();
            L21SIZE          = DN21.Tables[0].Rows[v2]["F24"].ToString().Trim();
            L21SN            = DN21.Tables[0].Rows[v2]["F10"].ToString().Trim();
            L21UnUsed        = DN21.Tables[0].Rows[v2]["F28"].ToString().Trim();
            L21SNBlock       = DN21.Tables[0].Rows[v2]["F52"].ToString().Trim();
            L21Factory       = DN21.Tables[0].Rows[v2]["F51"].ToString().Trim(); 

            // Get SNTYPE 10, 16
            t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 = '" + L21SN + "' ";
            DNTMP = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DNTMP == null) return ("-1");
            if (DNTMP.Tables.Count <= 0) return ("-2"); 
            if (  DNTMP.Tables[0].Rows.Count >= 1 )
                  SNDigit = DNTMP.Tables[0].Rows[0]["F12"].ToString().Trim();
            // Get Digital
           
            if (L21SIZE == "") L10SIZE = "0";
                                       
            if (L21SIZE == "") Req21SIZEQty = 0;
            else Req21SIZEQty = Convert.ToInt32(L21SIZE);


            if (L21SIZE == "") tmpsize = 0;
            else tmpsize = Convert.ToInt32(L21SIZE);

            if (L21UnUsed == "") tmpunused = 0;
            else tmpunused = Convert.ToInt32(L21UnUsed);

            Req21SIZEQty = tmpsize - tmpunused;


            v3 = 0;
            while ( ( Req21SIZEQty > 0) && ( GetCNT > v3 ))
            {
                Currtime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
                t4 = GetSN_FromRecvSupplier(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                if (  Convert.ToInt32(t4) > 0  )
                {
                     t4 = GetSN_FromRecvBy1V01(DBType, tmpReadDBA, L21DocumentID, L21DOCSEQ, L21SIZE, L21UnUsed, v2, ref DN21);
                     if (t4 != "")
                            Req21SIZEQty = Req21SIZEQty - Convert.ToInt32(t4);
                    
                }  // end if 
                v3++;
            }   // end while

        }  // end  for

        
        return ("");



    }  // end GetSN

    public string CheckSNFlag40ALL(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN

        int v1 = 0, v2 = 0, v3=0, v4=0, v5=0, v6=0, v7=0, v8=0, v9=0, v10=0, v11=0, v12=0;
        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", Sqlr4 = "", Sqlr5 = "", Sqlr6 = "", Sqlr7 = "", Sqlr8 = "", Sqlr9 = "", Sqlr10 = "", Sqlr11 = "", Sqlr12 = "";

        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}
        string t1 = " f40 = 4 where to_number(F28) <= 0    and f40 = 1  and  f1 = NOKFOXT1010 ";
        Sqlr1 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', f40 = '4' where to_number(F28) <= 0    and f40 = '1'  and  f1 = 'NOKFOXT1010' ";  // f24=size, f26 = unused

        t1 = " f40 = '4' where to_number(F28) >= to_number(F24)  and f40 = 1  and  f1 = NOKFOXT1021 ";
        Sqlr2 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', f40 = '4' where to_number(F28) >= to_number(F24)  and f40 = '1'  and  f1 = 'NOKFOXT1021' ";

        t1 = " f51 = Honai where f51 is null and  f1 = NOKFOXT1021 ";
        Sqlr3 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', f51 = 'Honai' where f51 is null and  f1 = 'NOKFOXT1021' ";

        t1 = " f52 = f10 where f52 is null and  f1 = NOKFOXT1010 ";
        Sqlr4 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', f52 = f10 where f52 is null and  f1 = 'NOKFOXT1010' ";

        t1 = " f40 = 2 where f16 = 1 and  f1 = NOKFOXT1021 ";
        Sqlr5 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', f40 = '2' where f16 = '1' and  f1 = 'NOKFOXT1021' ";   // Pending

        t1 = " f48 = f46 where f48 is null and  f1 = NOKFOXT1021 ";
        Sqlr6 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', f48 = f46 where f48 is null and  f1 = 'NOKFOXT1021' ";   // Pending

        t1 = " FLAG4 = F24 where FLAG4 is null and (  f1 = NOKFOXT1021  or f1 = NOKFOXT1020 ) ";
        Sqlr7 = "update ENGITMGT.ENGITTRS1011 set FLAG4 = F24 where FLAG4 is null and (  f1 = 'NOKFOXT1021'  or f1 = 'NOKFOXT1020' )  ";   // Pending

        t1 = " F40 = 3 where F16 = 1 and  f1 = NOKFOXT1021 and F40 = 1 ";
        Sqlr8 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', F40 = '3' where F16 = '1' and  f1 = 'NOKFOXT1021' and F40 = '1' ";   // Pending

        t1 = " F40 = 3 where F16 = 2 and ( F48 != Proto )  and  f1 = NOKFOXT1021  and F40 = 1  ";
        Sqlr9 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', F40 = '3' where F16 = '2' and ( F48 != 'Proto' )  and  f1 = 'NOKFOXT1021'  and F40 = '1' ";   // Pending

        Sqlr10 = "update ENGITMGT.ENGITM1001  set F20 = '0'  where F1 = 'NOKFOM1002' and F20 is null ";    // 
        Sqlr11 = "update ENGITMGT.ENGITM1001  set F22 = '0'  where F1 = 'NOKFOM1002' and F22 is null ";    // 

        t1 = " F40 = 3 where F16 is null , No Purpose  ";
        Sqlr12 = "update ENGITMGT.ENGITTRS1011 set flag7 = '" + t1 + "', F40 = '3' where F16 is null and ( f1 = 'NOKFOXT1021' or f1 = 'NOKFOXT1022' ) and F40 = '1' and upper(F52) = 'TAC'";   // Pending

                   
        v1  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr1);
        v2  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr2);
        v3  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr3);
        v4  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr4);
        v5  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr5);
        v6  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr6);
        v7  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr7);
        v8  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr8);
        v9  = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr9);
        v10 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr10);
        v11 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr11);
        v12 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, Sqlr12);
        
        

        return ((v1 + v2 + v3).ToString().Trim());

    }  // CHk GetSN

    // GetSN_FromRecv 
    // First In First Out
    public string GetSN_FromRecvSupplier(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L21SIZE, string L21UnUsed, int v22, ref DataSet DN21)
    {
        string tModel = DN21.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DN21.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP = DN21.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DN21.Tables[0].Rows[v22]["F10"].ToString().Trim();  // Level 2
        string tSNBlock = DN21.Tables[0].Rows[v22]["F52"].ToString().Trim();  // Level 1
        string tSNCSTATUS = DN21.Tables[0].Rows[v22]["F16"].ToString().Trim();  // 1,2,3,4
        string tmptime = Currtime;
        string tmpProtoProduction = "Production";

        int L21SIZEQty = Convert.ToInt32(L21SIZE);
        int L21SIZEUseQty = 0;
        int L21SIZEUnUseQty = Convert.ToInt32(L21UnUsed);

        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null, DN10 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", tSEQID="", t7 ="";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

        if ( tSNCSTATUS == "2" ) tmpProtoProduction = "Proto";

        // First, Check have tSN, tSuppier §ï¥Î SN Block  20170515
        // t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSN + "' "
        // 20170517 t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "' "
        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "' and F16 = '" + tmpProtoProduction + "' "
           + " and F44 = '" + tModel + "' and F48 = '" + tSupplier + "'  ";
        DN10 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN10 == null) return ("-1");
        if (DN10.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN10.Tables[0].Rows.Count;
        if (v1 <= 0)  // ¨S¦³¦¹ Supplier , then find new one 
        {
            // Get one recv one time    // Find then Book  Qty  F44 ¾÷ÀY, F48 Supplier 
            t2 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "'  and F44 is null and F48 is null  "
               + " and F16 = '" + tmpProtoProduction + "'     order by DOCUMENTID, seqid asc ";
            DN2 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t2);
            if (DN2 == null) return ("-1");
            if (DN2.Tables.Count <= 0) return ("-2");
            // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

            v2 = DN2.Tables[0].Rows.Count;
            if (v2 <= 0) return (v2.ToString());

            t2 = DN2.Tables[0].Rows[0]["DocumentID"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED
            t3 = DN2.Tables[0].Rows[0]["DOCSEQ"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED
            tSEQID = DN2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            t7     = DN2.Tables[0].Rows[0]["F28"].ToString().Trim(); // unused

            t4 = "Update ENGITMGT.ENGITTRS1011 set F44 = '" + tModel + "', F48 = '" + tSupplier + "', updatetime = '" + tmptime + "' "
                + " where Documentid = '" + t2 + "'  and DOCSEQ = '" + t3 + "' and SEQID = '" + tSEQID + "'  ";
            v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);

            return (v5.ToString().Trim());
        }

        return (v1.ToString().Trim());
    }  // End 52 

    public string GetSN_FromRecvSupplier12(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L21SIZE, string L21UnUsed, int v22, ref DataSet DN21)
    {
        string tModel = DN21.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DN21.Tables[0].Rows[v22]["F48"].ToString().Trim();
        string tSP = DN21.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DN21.Tables[0].Rows[v22]["F10"].ToString().Trim();  // Level 2
        string tSNBlock = DN21.Tables[0].Rows[v22]["F52"].ToString().Trim();  // Level 1
        string tSNCSTATUS = DN21.Tables[0].Rows[v22]["F16"].ToString().Trim();  // 1,2,3,4
        string tmptime = Currtime;
        string tmpProtoProduction = "Production";
        string FiXSN10Status = "Proto";

        int L21SIZEQty = Convert.ToInt32(L21SIZE);
        int L21SIZEUseQty = 0;
        int L21SIZEUnUseQty = Convert.ToInt32(L21UnUsed);

        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null, DN10 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", tSEQID = "", t7 = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

        if (tSNCSTATUS == "2") tmpProtoProduction = "Proto";

        // First, Check have tSN, tSuppier §ï¥Î SN Block  20170515
        // t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSN + "' "
        // 20170517 t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "' "
        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "' and F16 = '" + FiXSN10Status + "' "
           + " and F44 is null and F48 = '" + tSupplier + "'  ";
        DN10 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN10 == null) return ("-1");
        if (DN10.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN10.Tables[0].Rows.Count;
        if (v1 <= 0)  // ¨S¦³¦¹ Supplier , then find new one 
        {
            // Get one recv one time    // Find then Book  Qty  F44 ¾÷ÀY, F48 Supplier 
            t2 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "'  and F48 is null  "
               + " and F16 = '" + FiXSN10Status + "' and F44 is null     order by DOCUMENTID, seqid asc ";
            DN2 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t2);
            if (DN2 == null) return ("-1");
            if (DN2.Tables.Count <= 0) return ("-2");
            // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

            v2 = DN2.Tables[0].Rows.Count;
            if (v2 <= 0) return (v2.ToString());

            t2 = DN2.Tables[0].Rows[0]["DocumentID"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED
            t3 = DN2.Tables[0].Rows[0]["DOCSEQ"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED
            tSEQID = DN2.Tables[0].Rows[0]["SEQID"].ToString().Trim();
            t7 = DN2.Tables[0].Rows[0]["F28"].ToString().Trim(); // unused

            t4 = "Update ENGITMGT.ENGITTRS1011 set F48 = '" + tSupplier + "', updatetime = '" + tmptime + "' "
                + " where Documentid = '" + t2 + "'  and DOCSEQ = '" + t3 + "' and SEQID = '" + tSEQID + "'  ";
            v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);

            return (v5.ToString().Trim());
        }

        return (v1.ToString().Trim());
    }  // End 12 

    public string GetSN_FromRecvSupplier03(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L21SIZE, string L21UnUsed, int v22, ref DataSet DN21)
    {
        string tModel = DN21.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DN21.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP = DN21.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DN21.Tables[0].Rows[v22]["F10"].ToString().Trim();  // Level 2
        string tSNBlock = DN21.Tables[0].Rows[v22]["F52"].ToString().Trim();  // Level 1
        string tSNCSTATUS = DN21.Tables[0].Rows[v22]["F16"].ToString().Trim();  // 1,2,3,4
        string tmptime = Currtime;
        string tmpProtoProduction = "Production";

        int L21SIZEQty = Convert.ToInt32(L21SIZE);
        int L21SIZEUseQty = 0;
        int L21SIZEUnUseQty = Convert.ToInt32(L21UnUsed);

        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null, DN10 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", tSEQID = "", t7 = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

        if (tSNCSTATUS == "2") tmpProtoProduction = "Proto";

        // First, Check have tSN, tSuppier §ï¥Î SN Block  20170515
        // t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSN + "' "
        // 20170517 t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "' "
        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "'  ";
        DN10 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN10 == null) return ("-1");
        if (DN10.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN10.Tables[0].Rows.Count;
        

        return (v1.ToString().Trim());
    }  // End V0252
    // Check 1021 SN = Flag10, Proto/Production = FLAG16
    //         SN Type  Model¾÷ÀY  Supplier(Profile)
    // 1021  : F10      F44         F48         F16     (1,2,3,4)
    // M1001 : F11      F3          F7          CSTATUS (1,2,3,4)
    public string CheckSNFlag16(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN

        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        string tDocumentid21 = "", tDOCSEQ21 = "", tSEQID21 = "", tSNBlock21, tSNType21 = "";
        string t21F10 = "", t21F44 = "", t21F48 = "", t21F16 = "";
        string m1001F11 = "", m1001F3 = "", m1001F7 = "", m1001CSTATUS = "";
        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}
        
        DataSet DN2 = null, DN21 = null;

        Sqlr1 = "select *  from ENGITMGT.ENGITTRS1011 where  F1 = 'NOKFOXT1021' and F16 is null and f40 = '1' ";  // f24=size, f26 = unused
        DN21 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN21 == null) return ("-1");
        if (DN21.Tables.Count <= 0) return ("-2");

        v1 = DN21.Tables[0].Rows.Count;
        for (v3 = 0; v3 < v1; v3++)  //  Update into 52 Block SN 
        {
            tDocumentid21 = DN21.Tables[0].Rows[v3]["DOCUMENTID"].ToString().Trim();
            tDOCSEQ21 = DN21.Tables[0].Rows[v3]["DOCSEQ"].ToString().Trim();
            tSEQID21 = DN21.Tables[0].Rows[v3]["SEQID"].ToString().Trim();
            tSNType21 = DN21.Tables[0].Rows[v3]["F10"].ToString().Trim();
            tSNBlock21 = DN21.Tables[0].Rows[v3]["F52"].ToString().Trim();

            t21F10 = DN21.Tables[0].Rows[v3]["F10"].ToString().Trim();
            t21F44 = DN21.Tables[0].Rows[v3]["F44"].ToString().Trim();
            t21F48 = DN21.Tables[0].Rows[v3]["F48"].ToString().Trim();
            t21F16 = DN21.Tables[0].Rows[v3]["F16"].ToString().Trim();

            // Get one recv one time    // Find then Book  Qty  F44 ¾÷ÀY, F48 Supplier 
            // t2 = " select * from ENGITMGT.DATABOM1001 where DATABOMID = 'IDBOM1002'  and FLAG11 = '" + t21F10 + "' and  FLAG3 = '" + t21F44 + "' and FLAG7 = '" + t21F48 + "'  ";  // F3, F7, F11
            t2 = " select * from ENGITMGT.DATABOM1001 where DATABOMID = 'IDBOM1002'  and FLAG11 = '" + t21F10 + "' and  FLAG3 = '" + t21F44 + "' ";  // F3, F7, F11
            DN2 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t2);
            if (DN2 == null) return ("-1");
            if (DN2.Tables.Count <= 0) return ("-2");

            v2 = DN2.Tables[0].Rows.Count;
            if (v2 > 0)
            {
                t3 = DN2.Tables[0].Rows[0]["CSTATUS"].ToString().Trim(); // Block
                t4 = "Update ENGITMGT.ENGITTRS1011  set F16 = '" + t3 + "', F15 = 'SN Status' "
                    + " where F1 = 'NOKFOXT1021'  and Documentid = '" + tDocumentid21 + "'  and DOCSEQ = '" + tDOCSEQ21 + "'  and SEQID = '" + tSEQID21 + "'   ";
                v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
            }

        }


        return (v5.ToString().Trim());

    }  // CHk GetSN


    public string CheckSNMailV02(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        // string Ret6 = FGISlibPointer.SendGMailV01(DBOracle, tmpReadDBA, "", "", "MAIL", "ken.lin@foxconn.com", "Test Distribute SN", arrMail);
  
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        string tDocumentid22 = "", tDOCSEQ22 = "", tSEQID22 = "", tSNBlock22, tSNType22 = "";
        string t21F10 = "", t21F44 = "", t21F48 = "", t21F16 = "", t21Haed="";
        string m1001F11 = "", m1001F3 = "", m1001F7 = "", m1001CSTATUS = "";

        int arrX = 50, arrY = 5;
        string[,] arrMail = new string[arrX + 1, arrY + 1];

        for ( v1 = 0; v1 < arrX; v1++)
            for ( v2 = 0; v2 < arrY; v2++)
                arrMail[v1, v2] = "";
        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}

        DataSet DN2 = null, DN10 = null;

        Sqlr1 = "select *  from ENGITMGT.ENGITTRS1011 where  F1 = 'NOKFOXT1010' and ( FLAG6 is null or FLAG6 = '1' ) and ( f40 = '1' or f40 = '4' ) and ( F24 != F28 ) ";  // f24=size, f26 = unused
        DN10 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN10 == null) return ("-1");
        if (DN10.Tables.Count <= 0) return ("-2");

        v1 = DN10.Tables[0].Rows.Count;
        for (v3 = 0; v3 < v1; v3++)  //  Update into 52 Block SN 
        {
            tDocumentid22 = DN10.Tables[0].Rows[v3]["DOCUMENTID"].ToString().Trim();
            tDOCSEQ22 = DN10.Tables[0].Rows[v3]["DOCSEQ"].ToString().Trim();
            tSEQID22 = DN10.Tables[0].Rows[v3]["SEQID"].ToString().Trim();
            tSNType22 = DN10.Tables[0].Rows[v3]["F10"].ToString().Trim();
            tSNBlock22 = DN10.Tables[0].Rows[v3]["F52"].ToString().Trim();

            t21Haed = DN10.Tables[0].Rows[v3]["F14"].ToString().Trim();
            t21F10 = DN10.Tables[0].Rows[v3]["F10"].ToString().Trim();
            t21F44 = DN10.Tables[0].Rows[v3]["F44"].ToString().Trim();
            t21F48 = DN10.Tables[0].Rows[v3]["F48"].ToString().Trim();
            t21F16 = DN10.Tables[0].Rows[v3]["F16"].ToString().Trim();

            //Model:          <TBD> / Hambo / not decided   F44  TD
            //Model Name                                            TD Name
            //Profile:        Proto                         F48  
            //START
            //END
            //Block type:     TAC                           F52
            //Model:          <TBD> / Hambo / not decided   F44  TD
            //Model Name                                            TD Name
            //Profile:        Proto                         F48  

            arrMail[1, 1] = "Model";
            arrMail[1, 2] = DN10.Tables[0].Rows[v3]["F44"].ToString().Trim(); // "TA-1030";

            arrMail[2, 1] = "TAC Head";
            arrMail[2, 2] = DN10.Tables[0].Rows[v3]["F14"].ToString().Trim(); // "TA-1030";

            arrMail[3, 1] = "Profile";
            arrMail[3, 2] = DN10.Tables[0].Rows[v3]["F48"].ToString().Trim(); // "Proto";

            arrMail[4, 1] = "Start";
            arrMail[4, 2] = DN10.Tables[0].Rows[v3]["F20"].ToString().Trim();

            arrMail[5, 1] = "End";
            arrMail[5, 2] = DN10.Tables[0].Rows[v3]["F22"].ToString().Trim(); 

            arrMail[6, 1] = "Block type";
            arrMail[6, 2] = DN10.Tables[0].Rows[v3]["F10"].ToString().Trim(); // "TAC";


            arrMail[7, 1] = "Model";
            arrMail[7, 2] = DN10.Tables[0].Rows[v3]["F44"].ToString().Trim(); // "TA-1030";

            arrMail[8, 1] = "Model";
            arrMail[8, 2] = DN10.Tables[0].Rows[v3]["F44"].ToString().Trim(); // "TA-1030";

            arrMail[9, 1] = "Profile";
            arrMail[9, 2] = DN10.Tables[0].Rows[v3]["F48"].ToString().Trim(); // "Proto";




            t1 = SendGMailV01("SN102", DBType, tmpReadDBA, "", "", "MAILGROUP", "SN102MAILGROUP", "SN Buffer", arrMail);
            if (t1 == "1")
            {
                t3 = "2"; // flah6
                t4 = "Update ENGITMGT.ENGITTRS1011  set FLAG6 = '" + t3 + "'  "
                    + " where Documentid = '" + tDocumentid22 + "'  and DOCSEQ = '" + tDOCSEQ22 + "'  and SEQID = '" + tSEQID22 + "'   ";
                v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
            }

        }


        return (v5.ToString().Trim());

    }  // CHk GetSN

    public string CheckSNMailV03(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN
        // string Ret6 = FGISlibPointer.SendGMailV01(DBOracle, tmpReadDBA, "", "", "MAIL", "ken.lin@foxconn.com", "Test Distribute SN", arrMail);

        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        string tDocumentid22 = "", tDOCSEQ22 = "", tSEQID22 = "", tSNBlock22, tSNType22 = "";
        string t21F10 = "", t21F44 = "", t21F48 = "", t21F16 = "";
        string m1001F11 = "", m1001F3 = "", m1001F7 = "", m1001CSTATUS = "";

        int arrX = 50, arrY = 5;
        string[,] arrMail = new string[arrX + 1, arrY + 1];

        for (v1 = 0; v1 < arrX; v1++)
            for (v2 = 0; v2 < arrY; v2++)
                arrMail[v1, v2] = "";
        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}

        DataSet DN2 = null, DN22 = null;

        Sqlr1 = "select *  from ENGITMGT.ENGITTRS1011 where  F1 = 'NOKFOXT1022' and ( FLAG6 is null or FLAG6 = '1' ) and ( f40 = '1' or f40 = '4' ) ";  // f24=size, f26 = unused
        DN22 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN22 == null) return ("-1");
        if (DN22.Tables.Count <= 0) return ("-2");

        v1 = DN22.Tables[0].Rows.Count;
        for (v3 = 0; v3 < v1; v3++)  //  Update into 52 Block SN 
        {
            tDocumentid22 = DN22.Tables[0].Rows[v3]["DOCUMENTID"].ToString().Trim();
            tDOCSEQ22 = DN22.Tables[0].Rows[v3]["DOCSEQ"].ToString().Trim();
            tSEQID22 = DN22.Tables[0].Rows[v3]["SEQID"].ToString().Trim();
            tSNType22 = DN22.Tables[0].Rows[v3]["F10"].ToString().Trim();
            tSNBlock22 = DN22.Tables[0].Rows[v3]["F52"].ToString().Trim();

            t21F10 = DN22.Tables[0].Rows[v3]["F10"].ToString().Trim();
            t21F44 = DN22.Tables[0].Rows[v3]["F44"].ToString().Trim();
            t21F48 = DN22.Tables[0].Rows[v3]["F48"].ToString().Trim();
            t21F16 = DN22.Tables[0].Rows[v3]["F16"].ToString().Trim();

            //Type:           Offline
            //Model:          <TBD> / Hambo / not decided   F44
            //Profile:        Proto                         F48
            //Block type:     TAC                           F52
            //ESN type:       IMEI                          F10
            //Block:          00440297                      F14
            //Start:          047000                        F20
            //End:            047099                        F22
            //Quantity:       100                           F28
            //Factory:        iMES ODM Direct-ship database F51
            //Description:    for demonstration in Beijing phase 3

            arrMail[1, 1] = "TYPE";
            arrMail[1, 2] = "OffLine";

            arrMail[2, 1] = "Model";
            arrMail[2, 2] = DN22.Tables[0].Rows[v3]["F44"].ToString().Trim(); // "TA-1030";

            arrMail[3, 1] = "Profile";
            arrMail[3, 2] = DN22.Tables[0].Rows[v3]["F48"].ToString().Trim(); // "Proto";

            arrMail[4, 1] = "Block type";
            arrMail[4, 2] = DN22.Tables[0].Rows[v3]["F52"].ToString().Trim(); // "TAC";

            arrMail[5, 1] = "ESN type";
            arrMail[5, 2] = DN22.Tables[0].Rows[v3]["F10"].ToString().Trim();

            arrMail[6, 1] = "Block";
            arrMail[6, 2] = DN22.Tables[0].Rows[v3]["F14"].ToString().Trim();

            arrMail[7, 1] = "Start";
            arrMail[7, 2] = DN22.Tables[0].Rows[v3]["F20"].ToString().Trim();

            arrMail[8, 1] = "End";
            arrMail[8, 2] = DN22.Tables[0].Rows[v3]["F22"].ToString().Trim();

            arrMail[9, 1] = "Quantity";
            arrMail[9, 2] = DN22.Tables[0].Rows[v3]["F28"].ToString().Trim();

            arrMail[10, 1] = "Factory";
            arrMail[10, 2] = DN22.Tables[0].Rows[v3]["F51"].ToString().Trim();




            t1 = SendGMailV01("SN103", DBType, tmpReadDBA, "", "", "MAILGROUP", "SN103MAILGROUP", "Allocate SN103", arrMail);
            if (t1 == "1")
            {
                t3 = "2"; // flah6
                t4 = "Update ENGITMGT.ENGITTRS1011  set FLAG6 = '" + t3 + "'  "
                    + " where F1 = 'NOKFOXT1022'  and Documentid = '" + tDocumentid22 + "'  and DOCSEQ = '" + tDOCSEQ22 + "'  and SEQID = '" + tSEQID22 + "'   ";
                v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
            }

        }


        return (v5.ToString().Trim());

    }  // V03
    // Check Production Limit Qty is enough or not 


    public int ChkSNRequireQTYV01(string DBOracle, string tmpReadDBA, string file, string Model, string Supplier, int RequireQty)
    {
        int num1 = 0, v1=0;
        string aaa = "select F1, F6, F16, F18, F20 from " + file + " where F1 = 'NOKFOM1002' and F6 ='" + Model + "' ";
        DataSet dsdt = null;
        dsdt = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, aaa);
        if (dsdt == null) return num1;
        if (dsdt.Tables.Count <= 0)  return num1;;

        v1 = dsdt.Tables[0].Rows.Count;
        if ( v1 <= 0 )  return num1; 
       
        string tF16 = dsdt.Tables[0].Rows[0]["F16"].ToString().Trim();    
        string tF18 = dsdt.Tables[0].Rows[0]["F18"].ToString().Trim();   
        string tF20 = dsdt.Tables[0].Rows[0]["F20"].ToString().Trim();   


        if ( tF16 == "1" )
             return num1;
        else
        if ( ( tF16 == "2") || ( tF16 == "4" ) )
        {
            num1 = RequireQty;
            return num1;

        }
        else
        if ( tF16 == "3" )
        {
            int sum = Convert.ToInt32( tF20 ) + RequireQty;
            if (sum > Convert.ToInt32( tF18) )
            {
                num1 = Convert.ToInt32( tF18 ) - Convert.ToInt32( tF20 );
            }
            else
            {
                num1 = RequireQty;
            }
            //return num1;
        }



        return num1;
    }

    public int ChkReqProdLimitV02(string tSEQID, string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string t21F44, string t21F48, string t21F24, string L21SEQID)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN

        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0;
        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", t1 = "", t2 = "", t3 = "", t4 = "", t5 = "";
        string tDocumentid21 = "", tDOCSEQ21 = "", tSEQID21 = L21SEQID, tSNBlock21, tSNType21 = "";
        string t21F10 = "", t21F16 = "";
        string m1001F11 = "", m1001F3 = "", m1001F7 = "", m1001CSTATUS = "";
        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}
        // ChkSNRequireQTY(string DBOracle, string tmpReadDBA, string file, string Model, string Supplier, int RequireQty)

               
            // t21F24 = DN21.Tables[0].Rows[v3]["F24"].ToString().Trim();  // Require SIZE
            // t21F44 = DN21.Tables[0].Rows[v3]["F44"].ToString().Trim();
            // t21F48 = DN21.Tables[0].Rows[v3]["F48"].ToString().Trim();
           

            v5 = 0;
            if (t21F24 != "") v5 = Convert.ToInt32(t21F24);

            // update limit qty to ENGITMGT.ENGITM1001 v6 = FGISlib8Pointer.ChkSNRequireQTY(DBType, tmpReadDBA, "ENGITMGT.DATABOM1001", t21F44, t21F48, v5);
            v6 = ChkSNRequireQTYV01(DBType, tmpReadDBA, "ENGITMGT.ENGITM1001", t21F44, t21F48, v5);
            //t2 = " select * from ENGITMGT.DATABOM1001 where DATABOMID = 'IDBOM1002'  and FLAG11 = '" + t21F10 + "' and  FLAG3 = '" + t21F44 + "' ";  // F3, F7, F11
            //DN2 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t2);
            //if (DN2 == null) return ("-1");
            //if (DN2.Tables.Count <= 0) return ("-2");

            //v2 = DN2.Tables[0].Rows.Count;

            if (v6 <= 0) // Fail Qty   F4- = '3' delete
            {
                t4 = "Update ENGITMGT.ENGITTRS1011  set F40 = '3'  "
                   + " where F1 = 'NOKFOXT1021'   and SEQID = '" + tSEQID21 + "'   ";
                v6 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
                return (0);
            }
            else
            if (v5 != v6)  // Require ! = assign to_number(FLAG16) + " + v21ReqiureQty + ",
            {
                    // t3 = DN2.Tables[0].Rows[0]["CSTATUS"].ToString().Trim(); // Block
                t4 = "Update ENGITMGT.ENGITTRS1011  set F24 =  to_number(F28) + " + v6 + "   "
                + " where F1 = 'NOKFOXT1021'  and SEQID = '" + tSEQID21 + "'   ";
                v1 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
            }

      

        return (v6);

    }  // CHk GetSN V02

      

    // Check 1021 SN = Flag10, Block = FLAG52
    public string SetFactorySFC(string tDocumentid, string DBType, string tmpReadDBA, string ProcDir, string DBOracle1, string tmpWriDBA, string Wrifile, string tmpdate, string Funno, string NokFoxM01Ref, string UpdfileID, string RUNDIR, string GetSNExec, string PSDBAStr)
    {
        // GetSNExec = "1" Read, = "2" Read and GetSN

        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0;
        string Sqlr1 = "", Sqlr2 = "", Sqlr3 = "", t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", t7 = "", t8 = "";
        string tDocumentid21 = "", tDOCSEQ21 = "", tSEQID21 = "", tSNBlock21, tSNType21 = "";
        //if (UpdfileID == "")
        //{
        //    if (Funno == "NOKFOXT1020") t2 = " select *  from  " + NokFoxM01Ref + "  where F6='01'   order  by F3, F4 asc "; // select distinct(F3 ) data1 from labelmanagement.UPLOADTMP order  by F3 asc ";
        //    if (Funno == "NOKFOXT1020") t1 = " select *  from  " + Updfile + "  order  by F3, F4 asc ";
        //}

        DataSet DN2 = null, DN21 = null;


         DataSet DN22 = null, DN23 = null;
        string tDocumentid22 = "", tDOCSEQ22 = "", tSEQID22 = "", tSNBlock22, tSNType22 = "", tSNProvider22 = "", tSNTD22 = "", tSNProvider21 = "", tSNTD21 = "";

        // 54  51 factory SFC IP String 
        Sqlr1 = " select distinct(F51) tmpFactory from ENGITMGT.ENGITTRS1011 where "
              + " ( F1 = 'NOKFOXT1022' or F1 = 'NOKFOXT1021' or F1 = 'NOKFOXT1020' ) and  F54 is null ";  // f24=size, f26 = unused
        DN22 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, Sqlr1);
        if (DN22 == null) return ("-1");
        if (DN22.Tables.Count <= 0) return ("-2");

        v1 = DN22.Tables[0].Rows.Count;
        for (v3 = 0; v3 < v1; v3++)  //  Update into 52 Block SN 
        {
            t4 = "11";
            //tDocumentid22 = DN22.Tables[0].Rows[v3]["DOCUMENTID"].ToString().Trim();
            //tDOCSEQ22 = DN22.Tables[0].Rows[v3]["DOCSEQ"].ToString().Trim();
            //tSEQID22 = DN22.Tables[0].Rows[v3]["SEQID"].ToString().Trim();
            //tSNTD22 = DN22.Tables[0].Rows[v3]["F44"].ToString().Trim();
            //tSNProvider22 = DN22.Tables[0].Rows[v3]["F48"].ToString().Trim();
            //tSNProvider22 = DN22.Tables[0].Rows[v3]["F48"].ToString().Trim();
            t1 = DN22.Tables[0].Rows[v3]["tmpFactory"].ToString().Trim();
            // Get one recv one time    // Find then Book  Qty  F44 ¾÷ÀY, F48 Supplier 
            t2 = " select * from FGIS.FGISM1001 where F1 = 'NokFoxM2002' and F2 = '" + t1 + "'  ";
            DN23 = PDataBaseOperation.PSelectSQLDS(DBType, PSDBAStr, t2);
            if (DN23 == null) return ("-1");
            if (DN23.Tables.Count <= 0) return ("-2");
            v2 = DN23.Tables[0].Rows.Count;
            if (v2 > 0)
            {
                t3 = DN23.Tables[0].Rows[0]["F37"].ToString().Trim(); // DBA String
                if (t3 != "")
                {
                    t5 = "Update ENGITMGT.ENGITTRS1011  set F54 = '" + t3 + "' "
                        + " where ( F1 = 'NOKFOXT1022' or F1 = 'NOKFOXT1021' or F1 = 'NOKFOXT1020' )  and F51 = '" + t1 + "'  and F54 is null ";
                    v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t5);
                }
                
            }

           
        }


        return (v5.ToString().Trim());

    }  // CHk GetSN


    // GetSN_FromRecv 
    // First In First Out
    public string GetSN_FromRecvSupplierDUP(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L21SIZE, string L21UnUsed,int v22, ref DataSet DN21)
    {
        string tModel    = DN21.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DN21.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP       = DN21.Tables[0].Rows[v22]["F50"].ToString().Trim();      
        string tSN       = DN21.Tables[0].Rows[v22]["F10"].ToString().Trim();  // Level 2
        string tSNBlock  = DN21.Tables[0].Rows[v22]["F52"].ToString().Trim();  // Level 1

        int L21SIZEQty       = Convert.ToInt32(L21SIZE);
        int L21SIZEUseQty    = 0;
        int L21SIZEUnUseQty = Convert.ToInt32(L21UnUsed);  
            
        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null, DN10 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0,v5=0;

        string sw1="Y";
        int CurrSixe = 0;


        // First, Check have tSN, tSuppier §ï¥Î SN Block  20170515
        // t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSN + "' "
        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "' "
           + " and F48 = '" + tModel + "' and F48 = '" + tSupplier + "'  ";
        DN10 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN10 == null) return ("-1");
        if (DN10.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN10.Tables[0].Rows.Count;
        if (v1 <= 0)  // ¨S¦³¦¹ Supplier , then find new one 
        {   
            // Get one recv one time    // Find then Book  Qty  F44 ¾÷ÀY, F48 Supplier 
            t2 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSNBlock + "'  and F44 is null and F48 is null "
               + "      order by DOCUMENTID, seqid asc "; 
            DN2 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t2);
            if (DN2 == null) return ("-1");
            if (DN2.Tables.Count <= 0) return ("-2");
            // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

            v2 = DN2.Tables[0].Rows.Count;
            if (v2 <= 0) return( v2.ToString());

            t2 = DN2.Tables[0].Rows[0]["DocumentID"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED
            t3 = DN2.Tables[0].Rows[0]["DOCSEQ"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED
            t4 = "Update ENGITMGT.ENGITTRS1011 set F44 = '" + tModel + "',  set F48 = '" + tSupplier + "' "
                + " where Documentid = '" + t2 + "'  and DOCSEQ = '" + t3 + "'   ";
                v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);

                return (  v5.ToString().Trim());
        }          
        
         return ( v1.ToString().Trim() );
    }  // End GetSN_FromRecvSupplier 

    public string GetSN_FromRecvBy1V01(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L2SIZE,string L2UnUsed, int v22, ref DataSet DNdt2)
    {
        string tModel = DNdt2.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DNdt2.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP = DNdt2.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DNdt2.Tables[0].Rows[v22]["F10"].ToString().Trim();
        string v21Documentid = DNdt2.Tables[0].Rows[v22]["Documentid"].ToString().Trim();
        string v21Docseq = DNdt2.Tables[0].Rows[v22]["Docseq"].ToString().Trim();
        string v21id  = DNdt2.Tables[0].Rows[v22]["F6"].ToString().Trim();
        string v21factory = DNdt2.Tables[0].Rows[v22]["F51"].ToString().Trim();
        string v21SNBlock = DNdt2.Tables[0].Rows[v22]["F52"].ToString().Trim();
        string v21SEQID   = DNdt2.Tables[0].Rows[v22]["SEQID"].ToString().Trim();
        string v21CSTATUS = DNdt2.Tables[0].Rows[v22]["F16"].ToString().Trim();
        string FLAG3Offline = DNdt2.Tables[0].Rows[v22]["FLAG3"].ToString().Trim();
        string FiXSN10Status = "Proto";

        string tmpProtoProduction = "Production";
        if (v21CSTATUS == "2") tmpProtoProduction = "Proto";
        

        int v21SIZEQty = Convert.ToInt32(L2SIZE);
        int v21SIZEUseQty = 0;
        int v21SIZEUnUseQty = Convert.ToInt32(L2UnUsed);

        string tid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string ttime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        ttime = Currtime;

        string ReqUsed   = DNdt2.Tables[0].Rows[v22]["F26"].ToString().Trim();
        string ReqUnUsed = DNdt2.Tables[0].Rows[v22]["F28"].ToString().Trim();

        int v10Used = 0, v10UnUsed = 0, v10Usedafter = 0, v10UnUsedafter = 0, v10SIZE = 0;
        int v21Used = 0, v21UnUsed = 0, v21UnUsedafter = 0, v21Usedafter =0, v21ReqiureQty = 0;


        if (ReqUsed == "") v21Used = 0;
        else v21Used = Convert.ToInt32(ReqUsed);
        
        if ( ReqUnUsed == "" ) v21UnUsed = 0;
        else v21UnUsed = Convert.ToInt32(ReqUnUsed);

        v21SIZEUnUseQty = v21UnUsed;

        if (v21UnUsed >= v21SIZEQty) return ("0");  // Require is full
        
        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "", tmpSNBlock="";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0,v6=0,v7=0, RetGetQTY = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

            // Get SNTYPE 10, 16
        t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 =  '" + tSN + "' ";
            DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DN1 == null) return ("-1");
            if (DN1.Tables.Count <= 0) return ("-2"); 
            if (  DN1.Tables[0].Rows.Count >= 1 )
                if (DN1.Tables[0].Rows[v2]["F12"].ToString().Trim() != null)
                {
                    SNDigit = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();
                    tmpSNBlock = DN1.Tables[0].Rows[0]["F11"].ToString().Trim();  // Block
                }

            if (v21SNBlock == "") v21SNBlock = tmpSNBlock; 

            //  20170528 Do not cure TD( ¾÷ÀY ) t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' "
            //   + " and F44 = '" + tModel + "' and F48 = '" + tSupplier + "'   and F16 = '" + tmpProtoProduction + "' order  by Documentid, seqid  asc";

            t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' "
               + " and F44 is null and F48 = '" + tSupplier + "'   and F16 = '" + FiXSN10Status + "' order  by Documentid, seqid  asc";           
         
            DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
            if (DN1 == null) return ("-1");
            if (DN1.Tables.Count <= 0) return ("-2");
            // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

            v1 = DN1.Tables[0].Rows.Count;
            if (v1 <= 0) return("-1");
            // Find then Book  Qty
            t2 = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED

            //Receive 010
            string tSNHead  = DN1.Tables[0].Rows[v2]["F14"].ToString().Trim();
            string tIssued  = DN1.Tables[0].Rows[v2]["F16"].ToString().Trim();
            string tName    = DN1.Tables[0].Rows[v2]["F18"].ToString().Trim();


            string BUFStart = DN1.Tables[0].Rows[v2]["F20"].ToString().Trim();
            string BUFEnd   = DN1.Tables[0].Rows[v2]["F22"].ToString().Trim();
            string BUFSIZE  = DN1.Tables[0].Rows[v2]["F24"].ToString().Trim();
            string BUFUsed  = DN1.Tables[0].Rows[v2]["F26"].ToString().Trim();
            string BUFUnUsed= DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();

            string v10Documentid = DN1.Tables[0].Rows[v2]["Documentid"].ToString().Trim();
            string v10Docseq     = DN1.Tables[0].Rows[v2]["Docseq"].ToString().Trim();
            string v10SEQID      = DN1.Tables[0].Rows[v2]["SEQID"].ToString().Trim();
            string v22Documentid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
            string v22Docseq     = "10001";
            string v22FLAG3      = "3";

            string tmpRdate = DateTime.Now.ToString("yyyyMMdd");
            string tmpDdate = DateTime.Now.ToString("yyyy-MM-dd");;

                
            if ( BUFUsed == "" ) v10Used = 0;
            else v10Used = Convert.ToInt32( BUFUsed );

            if ( BUFUnUsed == "" ) v10UnUsed = 0;
            else v10UnUsed = Convert.ToInt32( BUFUnUsed );
        
            if ( BUFSIZE == "" ) v10SIZE = 0;
            else v10SIZE = Convert.ToInt32( BUFSIZE );

            
            // modift  20170501 v21ReqiureQty = v21SIZEQty - v21UnUsed;  // Require ZISE - unUsed ( alraedy dispatch )

            v21ReqiureQty = v21SIZEQty - v21UnUsed;  // SIZE == UNUsed ªí¥Üµo§¹  20170509

            string NewStart = "", NewEnd = "";            

            if (v21ReqiureQty <= 0) return ("0");
                
            v3 = v10UnUsed;
            v4 = v21UnUsed;
 
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //  File        SIZE        Used        UnUSED      FLAG40    ­pºâ«á        SIZE        Used        UnUSED      FLAG40
            //  10          200000      0           100000      1                       200000      4000        160000      1  UnUsed = 200000 start, and decrease each time
            //  20          4000                                                                                                UnUsed = 200000 start,
            //  21  TAC     4000        0           0                                   4000        0           4000        2  UnUsed = 0 start, and upcrase each time
            //  21  OUI     4000        0           0                                   4000        0           4000        2
            //  22  TAC                                                                 4000        0           4000        2
            //  22  OUI                                                                 4000        0           4000        2
            //  
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // int v10Usedafter = 0, v10UnUsedafter = 0, v21UnUsedafter = 0;

            // Not enough , Can have Require only ±N©Ò¦³ 10 µ¹ 21
            if (v10UnUsed < v21ReqiureQty) // not enough
                v21ReqiureQty = v10UnUsed;

            string Ret11 = "";
            //if ( SNDigit == "16" )
            //     Ret11 = GetSNRangeD16(tSN, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit );      
            //else

            string AllocateCnt = "2";
            if (v10SIZE == v10UnUsed) AllocateCnt = "1";

                 Ret11 = GetSNRangeD10V02(tSN, BUFSIZE, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit, AllocateCnt ); 

            v21UnUsedafter = v21UnUsed + v21ReqiureQty;
            v21Usedafter   = v21SIZEQty - v21UnUsedafter; // v21Used + v21ReqiureQty;

            v10UnUsedafter = v10UnUsed - v21ReqiureQty;
            v10Usedafter   = v10SIZE - v10UnUsedafter; //  v10Used + v21ReqiureQty;

            string F40v10 = "1", F40v20 = "1", F40v21 = "1", F40v22 = "1";

            if (v10UnUsedafter <= 0) F40v10 = "4";
            if (v21UnUsedafter >= v21SIZEQty) F40v21 = "4";
  
            if (  FLAG3Offline != "2" )  v22FLAG3  = FLAG3Offline; // Offline


            // update 10, 21, nsert 22,  20
            tid = tid + DateTime.Now.ToString("yyyyMMddHHmmssmm");
            
            t4 = "Insert into ENGITMGT.ENGITTRS1011 ( Documentid, Docseq, F1, RDATE, DDATE,  "
                     + " F5, F6, F7, F8,F9, F10, F11, F12, "
                     + " F23, F24, F25, F26, F27, F28, "
                     + " F33, F34, F35, F36, F37, F38, "
                     + " F39, F40, F41,F42, F43, F44,F45,F46, "
                     + " F49, F50, F2, F3, F13, F14, "
                     + " F15, F17, F18, F19, F21, F29, F30, F31,F32, "
                     + " FLAG1, FLAG2, FLAG3,updatetime, F20, F22, F51, F52, F16, F47, F48 ) "
                     + " Values ( '" + v22Documentid + "','" + v22Docseq + "', 'NOKFOXT1022','" + tmpRdate + "','" + tmpDdate + "', "
                     + " 'Disp Ticket NO', '" + tid + "', 'SubSeq', '10001', 'NAME', '" + tSN + "', 'Trs Code', '51',  "
                     + " 'SIZE', '" + v21SIZEQty.ToString().Trim() + "', 'Used QTy', '0', 'UnUserd QTY', '" + v21ReqiureQty.ToString().Trim() + "',  "
                     + " 'Pre DOCM', '" + v21Documentid + "',  "
                     + " 'Pre DOCSEQ','" + v21Docseq + "', 'Pre 1010 SEQID', '" + v10SEQID + "',    "
                  // 20170523  + "  'STATUS', '1', 'MODEL NO', '10001', 'Name', '" + tModel + "', 'Supplier NO', '" + tSupplier + "', "
                     + "  'STATUS', '1', 'MODEL NO', '10001', 'Name', '" + tModel + "', 'Supplier NO', '" + tSupplier + "', "
                     + " 'SP Code',  '" + tSP + "', 'Main Require each SN QTY A', 'Disp SN CODE', 'SN Haed','" + tSNHead + "', "
                     + "  'SN Issued', 'Name',  '" + tName + "', 'SN Start', 'SN End','Fail QTY','0', 'Resv QTy','0', "
                     + " 'BOM Attr', '10', '" + v22FLAG3 + "', '" + ttime + "', '" + NewStart + "','" + NewEnd + "','" + v21factory + "','" + v21SNBlock + "', '" + v21CSTATUS + "', 'Name', '" + tSupplier + "' ) ";

            v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
            if (v5 > 0)  // Insert OK
            {
                v7 = v7 + v5;
                RetGetQTY = v21ReqiureQty;
            }


            t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v10 + "', F26 = '" + v10Usedafter + "',  F28 = '" + v10UnUsedafter + "',  "
                 + " updatetime = '" + ttime + "' where documentid = '" + v10Documentid + "' "
               + " and docseq = '" + v10Docseq + "' and  F1 = 'NOKFOXT1010'  and SEQID = '" + v10SEQID + "' ";
            v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
            if (v5 > 0) v7 = v7 + v5;

            t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v21 + "', F26 = '" + v21Usedafter + "', F28 = '" + v21UnUsedafter + "',   "
             + " updatetime = '" + ttime + "' where documentid = '" + v21Documentid + "' and docseq = '" + v21Docseq + "' and "
             + "  F1 = 'NOKFOXT1021'  and  SEQID = '" + v21SEQID + "'";
            v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
            if (v5 > 0) // Update array in memory 
            {
                DNdt2.Tables[0].Rows[v22]["F40"] = F40v21.ToString().Trim();
                DNdt2.Tables[0].Rows[v22]["F26"] = v21Usedafter.ToString().Trim();
                DNdt2.Tables[0].Rows[v22]["F28"] = v21UnUsedafter.ToString().Trim();
            }

            // .Profile v21ReqiureQty
            if (v21CSTATUS == "3") // Production Limit , Need Add Qty
            {
                //t3 = " Update  ENGITMGT.DATABOM1001 set FLAG16 = to_number(FLAG16) + " + v21ReqiureQty + ",   "
                //          + " uptime = '" + ttime + "' where FLAG7 = '" + tSupplier + "' and FLAG3 = '" + tModel + "' and "
                //          + "  DATABOMID = 'IDBOM1002'  and  FLAG11 = '" + tSN + "'";
                //v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);

                //v5 = UpdatelimitQty(ref DBType, ref tmpReadDBA, ref v21ReqiureQty, ref ttime, ref tModel);
                //if (v5 > 0) v7 = v7 + v5;
            }

                     
        return ( v21ReqiureQty.ToString().Trim() );
        
    }  // End GetSN_FromRecv1

    public string GetSN_FromRecvBy1V02(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L2SIZE, string L2UnUsed, int v22, ref DataSet DNdt2)
    {
        string tModel = DNdt2.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DNdt2.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP = DNdt2.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DNdt2.Tables[0].Rows[v22]["F10"].ToString().Trim();
        string v21Documentid = DNdt2.Tables[0].Rows[v22]["Documentid"].ToString().Trim();
        string v21Docseq = DNdt2.Tables[0].Rows[v22]["Docseq"].ToString().Trim();
        string v21id = DNdt2.Tables[0].Rows[v22]["F6"].ToString().Trim();
        string v21factory = DNdt2.Tables[0].Rows[v22]["F51"].ToString().Trim();
        string v21SNBlock = DNdt2.Tables[0].Rows[v22]["F52"].ToString().Trim();
        string v21SEQID = DNdt2.Tables[0].Rows[v22]["SEQID"].ToString().Trim();
        string v21CSTATUS = DNdt2.Tables[0].Rows[v22]["F16"].ToString().Trim();
        string FLAG3Offline = DNdt2.Tables[0].Rows[v22]["FLAG3"].ToString().Trim();


        string tmpProtoProduction = "Production";
        if (v21CSTATUS == "2") tmpProtoProduction = "Proto";


        int v21SIZEQty = Convert.ToInt32(L2SIZE);
        int v21SIZEUseQty = 0;
        int v21SIZEUnUseQty = Convert.ToInt32(L2UnUsed);

        string tid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string ttime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        ttime = Currtime;

        string ReqUsed = DNdt2.Tables[0].Rows[v22]["F26"].ToString().Trim();
        string ReqUnUsed = DNdt2.Tables[0].Rows[v22]["F28"].ToString().Trim();

        int v10Used = 0, v10UnUsed = 0, v10Usedafter = 0, v10UnUsedafter = 0, v10SIZE = 0;
        int v21Used = 0, v21UnUsed = 0, v21UnUsedafter = 0, v21Usedafter = 0, v21ReqiureQty = 0;


        if (ReqUsed == "") v21Used = 0;
        else v21Used = Convert.ToInt32(ReqUsed);

        if (ReqUnUsed == "") v21UnUsed = 0;
        else v21UnUsed = Convert.ToInt32(ReqUnUsed);

        v21SIZEUnUseQty = v21UnUsed;

        if (v21UnUsed >= v21SIZEQty) return ("0");  // Require is full

        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "", tmpSNBlock = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, RetGetQTY = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

        // Get SNTYPE 10, 16
        t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 =  '" + tSN + "' ";
        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        if (DN1.Tables[0].Rows.Count >= 1)
            if (DN1.Tables[0].Rows[v2]["F12"].ToString().Trim() != null)
            {
                SNDigit = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();
                tmpSNBlock = DN1.Tables[0].Rows[0]["F11"].ToString().Trim();  // Block
            }

        if (v21SNBlock == "") v21SNBlock = tmpSNBlock;

        //  20170528 Do not cure TD( ¾÷ÀY ) t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' "
        //   + " and F44 = '" + tModel + "' and F48 = '" + tSupplier + "'   and F16 = '" + tmpProtoProduction + "' order  by Documentid, seqid  asc";

        // 20170531 t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' "
        //      + " and F44 is null and F48 = '" + tSupplier + "'   and F16 = '" + tmpProtoProduction + "' order  by Documentid, seqid  asc";

        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' "
          + " and F44 = '" + tModel + "' and F48 = '" + tSupplier + "'   and F16 = '" + tmpProtoProduction + "' order  by Documentid, seqid  asc";


        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN1.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-1");
        // Find then Book  Qty
        t2 = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED

        //Receive 010
        string tSNHead = DN1.Tables[0].Rows[v2]["F14"].ToString().Trim();
        string tIssued = DN1.Tables[0].Rows[v2]["F16"].ToString().Trim();
        string tName = DN1.Tables[0].Rows[v2]["F18"].ToString().Trim();


        string BUFStart = DN1.Tables[0].Rows[v2]["F20"].ToString().Trim();
        string BUFEnd = DN1.Tables[0].Rows[v2]["F22"].ToString().Trim();
        string BUFSIZE = DN1.Tables[0].Rows[v2]["F24"].ToString().Trim();
        string BUFUsed = DN1.Tables[0].Rows[v2]["F26"].ToString().Trim();
        string BUFUnUsed = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();

        string v10Documentid = DN1.Tables[0].Rows[v2]["Documentid"].ToString().Trim();
        string v10Docseq = DN1.Tables[0].Rows[v2]["Docseq"].ToString().Trim();
        string v10SEQID = DN1.Tables[0].Rows[v2]["SEQID"].ToString().Trim();
        string v22Documentid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string v22Docseq = "10001";
        string v22FLAG3 = "3";

        string tmpRdate = DateTime.Now.ToString("yyyyMMdd");
        string tmpDdate = DateTime.Now.ToString("yyyy-MM-dd"); ;


        if (BUFUsed == "") v10Used = 0;
        else v10Used = Convert.ToInt32(BUFUsed);

        if (BUFUnUsed == "") v10UnUsed = 0;
        else v10UnUsed = Convert.ToInt32(BUFUnUsed);

        if (BUFSIZE == "") v10SIZE = 0;
        else v10SIZE = Convert.ToInt32(BUFSIZE);


        // modift  20170501 v21ReqiureQty = v21SIZEQty - v21UnUsed;  // Require ZISE - unUsed ( alraedy dispatch )

        v21ReqiureQty = v21SIZEQty - v21UnUsed;  // SIZE == UNUsed ªí¥Üµo§¹  20170509

        string NewStart = "", NewEnd = "";

        if (v21ReqiureQty <= 0) return ("0");

        v3 = v10UnUsed;
        v4 = v21UnUsed;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  File        SIZE        Used        UnUSED      FLAG40    ­pºâ«á        SIZE        Used        UnUSED      FLAG40
        //  10          200000      0           100000      1                       200000      4000        160000      1  UnUsed = 200000 start, and decrease each time
        //  20          4000                                                                                                UnUsed = 200000 start,
        //  21  TAC     4000        0           0                                   4000        0           4000        2  UnUsed = 0 start, and upcrase each time
        //  21  OUI     4000        0           0                                   4000        0           4000        2
        //  22  TAC                                                                 4000        0           4000        2
        //  22  OUI                                                                 4000        0           4000        2
        //  
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // int v10Usedafter = 0, v10UnUsedafter = 0, v21UnUsedafter = 0;

        // Not enough , Can have Require only ±N©Ò¦³ 10 µ¹ 21
        if (v10UnUsed < v21ReqiureQty) // not enough
            v21ReqiureQty = v10UnUsed;

        string Ret11 = "";
        //if ( SNDigit == "16" )
        //     Ret11 = GetSNRangeD16(tSN, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit );      
        //else

        string AllocateCnt = "2";
        if (v10SIZE == v10UnUsed) AllocateCnt = "1";

        Ret11 = GetSNRangeD10V02(tSN, BUFSIZE, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit, AllocateCnt);

        v21UnUsedafter = v21UnUsed + v21ReqiureQty;
        v21Usedafter = v21SIZEQty - v21UnUsedafter; // v21Used + v21ReqiureQty;

        v10UnUsedafter = v10UnUsed - v21ReqiureQty;
        v10Usedafter = v10SIZE - v10UnUsedafter; //  v10Used + v21ReqiureQty;

        string F40v10 = "1", F40v20 = "1", F40v21 = "1", F40v22 = "1";

        if (v10UnUsedafter <= 0) F40v10 = "4";
        if (v21UnUsedafter >= v21SIZEQty) F40v21 = "4";

        if (FLAG3Offline != "2") v22FLAG3 = FLAG3Offline; // Offline


        // update 10, 21, nsert 22,  20
        tid = tid + DateTime.Now.ToString("yyyyMMddHHmmssmm");

        t4 = "Insert into ENGITMGT.ENGITTRS1011 ( Documentid, Docseq, F1, RDATE, DDATE,  "
                 + " F5, F6, F7, F8,F9, F10, F11, F12, "
                 + " F23, F24, F25, F26, F27, F28, "
                 + " F33, F34, F35, F36, F37, F38, "
                 + " F39, F40, F41,F42, F43, F44,F45,F46, "
                 + " F49, F50, F2, F3, F13, F14, "
                 + " F15, F17, F18, F19, F21, F29, F30, F31,F32, "
                 + " FLAG1, FLAG2, FLAG3,updatetime, F20, F22, F51, F52, F16, F47, F48 ) "
                 + " Values ( '" + v22Documentid + "','" + v22Docseq + "', 'NOKFOXT1022','" + tmpRdate + "','" + tmpDdate + "', "
                 + " 'Disp Ticket NO', '" + tid + "', 'SubSeq', '10001', 'NAME', '" + tSN + "', 'Trs Code', '51',  "
                 + " 'SIZE', '" + v21SIZEQty.ToString().Trim() + "', 'Used QTy', '0', 'UnUserd QTY', '" + v21ReqiureQty.ToString().Trim() + "',  "
                 + " 'Pre DOCM', '" + v21Documentid + "',  "
                 + " 'Pre DOCSEQ','" + v21Docseq + "', 'Pre 1010 SEQID', '" + v10SEQID + "',    "
            // 20170523  + "  'STATUS', '1', 'MODEL NO', '10001', 'Name', '" + tModel + "', 'Supplier NO', '" + tSupplier + "', "
                 + "  'STATUS', '1', 'MODEL NO', '10001', 'Name', '" + tModel + "', 'Supplier NO', '" + tSupplier + "', "
                 + " 'SP Code',  '" + tSP + "', 'Main Require each SN QTY A', 'Disp SN CODE', 'SN Haed','" + tSNHead + "', "
                 + "  'SN Issued', 'Name',  '" + tName + "', 'SN Start', 'SN End','Fail QTY','0', 'Resv QTy','0', "
                 + " 'BOM Attr', '10', '" + v22FLAG3 + "', '" + ttime + "', '" + NewStart + "','" + NewEnd + "','" + v21factory + "','" + v21SNBlock + "', '" + v21CSTATUS + "', 'Name', '" + tSupplier + "' ) ";

        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
        if (v5 > 0)  // Insert OK
        {
            v7 = v7 + v5;
            RetGetQTY = v21ReqiureQty;
        }


        t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v10 + "', F26 = '" + v10Usedafter + "',  F28 = '" + v10UnUsedafter + "',  "
             + " updatetime = '" + ttime + "' where documentid = '" + v10Documentid + "' "
           + " and docseq = '" + v10Docseq + "' and  F1 = 'NOKFOXT1010'  and SEQID = '" + v10SEQID + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        if (v5 > 0) v7 = v7 + v5;

        t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v21 + "', F26 = '" + v21Usedafter + "', F28 = '" + v21UnUsedafter + "',   "
         + " updatetime = '" + ttime + "' where documentid = '" + v21Documentid + "' and docseq = '" + v21Docseq + "' and "
         + "  F1 = 'NOKFOXT1021'  and  SEQID = '" + v21SEQID + "'";
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        if (  v5 > 0 ) // Update array in memory 
        {
            DNdt2.Tables[0].Rows[v22]["F40"] = F40v21.ToString().Trim();
            DNdt2.Tables[0].Rows[v22]["F26"] = v21Usedafter.ToString().Trim();
            DNdt2.Tables[0].Rows[v22]["F28"] = v21UnUsedafter.ToString().Trim();
        }

        // .Profile v21ReqiureQty
        if (v21CSTATUS == "3") // Production Limit , Need Add Qty
        {
            //t3 = " Update  ENGITMGT.DATABOM1001 set FLAG16 = to_number(FLAG16) + " + v21ReqiureQty + ",   "
            //          + " uptime = '" + ttime + "' where FLAG7 = '" + tSupplier + "' and FLAG3 = '" + tModel + "' and "
            //          + "  DATABOMID = 'IDBOM1002'  and  FLAG11 = '" + tSN + "'";

            // t3 = " Update  ENGITMGT.ENGITM1001 set F20 = to_number(F20) + " + v21ReqiureQty + ",   "
            //   + " updatetime = '" + ttime + "' where F6 = '" + tModel + "' and  F1 = 'NOKFOM1002' ";
            // v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
            //v5 = UpdatelimitQty(ref DBType, ref tmpReadDBA, ref v21ReqiureQty, ref ttime, ref tModel);
            //if (v5 > 0) v7 = v7 + v5;
        }


        return (v21ReqiureQty.ToString().Trim());

    }  // End GetSN_FromRecvV02 

    public int UpdatelimitQty( ref string DBType, ref string tmpReadDBA, ref int v21ReqiureQty, ref string ttime, ref string tModel)
    {
        string t3 = " Update  ENGITMGT.ENGITM1001 set F20 = to_number(F20) + " + v21ReqiureQty + ",   "
                  + " updatetime = '" + ttime + "' where F6 = '" + tModel + "' and  F1 = 'NOKFOM1002' ";

        int v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);

        return (v5);
    }

    public string GetSN_FromRecvBy1V12(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L2SIZE, string L2UnUsed, int v22, ref DataSet DNdt2)
    {
        string tModel = DNdt2.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DNdt2.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP = DNdt2.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DNdt2.Tables[0].Rows[v22]["F10"].ToString().Trim();
        string v21Documentid = DNdt2.Tables[0].Rows[v22]["Documentid"].ToString().Trim();
        string v21Docseq = DNdt2.Tables[0].Rows[v22]["Docseq"].ToString().Trim();
        string v21id = DNdt2.Tables[0].Rows[v22]["F6"].ToString().Trim();
        string v21factory = DNdt2.Tables[0].Rows[v22]["F51"].ToString().Trim();
        string v21SNBlock = DNdt2.Tables[0].Rows[v22]["F52"].ToString().Trim();
        string v21SEQID = DNdt2.Tables[0].Rows[v22]["SEQID"].ToString().Trim();
        string v21CSTATUS = DNdt2.Tables[0].Rows[v22]["F16"].ToString().Trim();

        string tmpProtoProduction = "Production";
        if (v21CSTATUS == "2") tmpProtoProduction = "Proto";


        int v21SIZEQty = Convert.ToInt32(L2SIZE);
        int v21SIZEUseQty = 0;
        int v21SIZEUnUseQty = Convert.ToInt32(L2UnUsed);

        string tid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string ttime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        ttime = Currtime;

        string ReqUsed = DNdt2.Tables[0].Rows[v22]["F26"].ToString().Trim();
        string ReqUnUsed = DNdt2.Tables[0].Rows[v22]["F28"].ToString().Trim();

        int v10Used = 0, v10UnUsed = 0, v10Usedafter = 0, v10UnUsedafter = 0, v10SIZE = 0;
        int v21Used = 0, v21UnUsed = 0, v21UnUsedafter = 0, v21Usedafter = 0, v21ReqiureQty = 0;


        if (ReqUsed == "") v21Used = 0;
        else v21Used = Convert.ToInt32(ReqUsed);

        if (ReqUnUsed == "") v21UnUsed = 0;
        else v21UnUsed = Convert.ToInt32(ReqUnUsed);

        v21SIZEUnUseQty = v21UnUsed;

        if (v21UnUsed >= v21SIZEQty) return ("0");  // Require is full

        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "", tmpSNBlock = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, RetGetQTY = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

        // Get SNTYPE 10, 16
        t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 =  '" + tSN + "' ";
        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        if (DN1.Tables[0].Rows.Count >= 1)
            if (DN1.Tables[0].Rows[v2]["F12"].ToString().Trim() != null)
            {
                SNDigit = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();
                tmpSNBlock = DN1.Tables[0].Rows[0]["F11"].ToString().Trim();  // Block
            }

        if (v21SNBlock == "") v21SNBlock = tmpSNBlock;

        // First, Check have tSN, tSuppier 20170515 t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSN + "' "
        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' "
           + " and F48 = '" + tSupplier + "'   and F16 = '" + tmpProtoProduction + "' order  by Documentid, seqid  asc";
        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN1.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-1");
        // Find then Book  Qty
        t2 = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED

        //Receive 010
        string tSNHead = DN1.Tables[0].Rows[v2]["F14"].ToString().Trim();
        string tIssued = DN1.Tables[0].Rows[v2]["F16"].ToString().Trim();
        string tName = DN1.Tables[0].Rows[v2]["F18"].ToString().Trim();


        string BUFStart = DN1.Tables[0].Rows[v2]["F20"].ToString().Trim();
        string BUFEnd = DN1.Tables[0].Rows[v2]["F22"].ToString().Trim();
        string BUFSIZE = DN1.Tables[0].Rows[v2]["F24"].ToString().Trim();
        string BUFUsed = DN1.Tables[0].Rows[v2]["F26"].ToString().Trim();
        string BUFUnUsed = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();

        string v10Documentid = DN1.Tables[0].Rows[v2]["Documentid"].ToString().Trim();
        string v10Docseq = DN1.Tables[0].Rows[v2]["Docseq"].ToString().Trim();
        string v10SEQID = DN1.Tables[0].Rows[v2]["SEQID"].ToString().Trim();
        string v22Documentid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string v22Docseq = "10001";

        string tmpRdate = DateTime.Now.ToString("yyyyMMdd");
        string tmpDdate = DateTime.Now.ToString("yyyy-MM-dd"); ;


        if (BUFUsed == "") v10Used = 0;
        else v10Used = Convert.ToInt32(BUFUsed);

        if (BUFUnUsed == "") v10UnUsed = 0;
        else v10UnUsed = Convert.ToInt32(BUFUnUsed);

        if (BUFSIZE == "") v10SIZE = 0;
        else v10SIZE = Convert.ToInt32(BUFSIZE);


        // modift  20170501 v21ReqiureQty = v21SIZEQty - v21UnUsed;  // Require ZISE - unUsed ( alraedy dispatch )

        v21ReqiureQty = v21SIZEQty - v21UnUsed;  // SIZE == UNUsed ªí¥Üµo§¹  20170509

        string NewStart = "", NewEnd = "";

        if (v21ReqiureQty <= 0) return ("0");

        v3 = v10UnUsed;
        v4 = v21UnUsed;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  File        SIZE        Used        UnUSED      FLAG40    ­pºâ«á        SIZE        Used        UnUSED      FLAG40
        //  10          200000      0           100000      1                       200000      4000        160000      1  UnUsed = 200000 start, and decrease each time
        //  20          4000                                                                                                UnUsed = 200000 start,
        //  21  TAC     4000        0           0                                   4000        0           4000        2  UnUsed = 0 start, and upcrase each time
        //  21  OUI     4000        0           0                                   4000        0           4000        2
        //  22  TAC                                                                 4000        0           4000        2
        //  22  OUI                                                                 4000        0           4000        2
        //  
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // int v10Usedafter = 0, v10UnUsedafter = 0, v21UnUsedafter = 0;

        // Not enough , Can have Require only ±N©Ò¦³ 10 µ¹ 21
        if (v10UnUsed < v21ReqiureQty) // not enough
            v21ReqiureQty = v10UnUsed;

        string Ret11 = "";
        //if ( SNDigit == "16" )
        //     Ret11 = GetSNRangeD16(tSN, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit );      
        //else
        Ret11 = GetSNRangeD10(tSN, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit);

        v21UnUsedafter = v21UnUsed + v21ReqiureQty;
        v21Usedafter = v21SIZEQty - v21UnUsedafter; // v21Used + v21ReqiureQty;

        v10UnUsedafter = v10UnUsed - v21ReqiureQty;
        v10Usedafter = v10SIZE - v10UnUsedafter; //  v10Used + v21ReqiureQty;

        string F40v10 = "1", F40v20 = "1", F40v21 = "1", F40v22 = "1";

        if (v10UnUsedafter <= 0) F40v10 = "4";
        if (v21UnUsedafter >= v21SIZEQty) F40v21 = "4";


        // Test 

        //if (v21CSTATUS == "3") // Production Limit , Need Add Qty
        //{
        //   t3 = " Update  ENGITMGT.DATABOM1001 set FLAG16 = to_number(FLAG16) + " + v21ReqiureQty + ",   "
        //             + " uptime = '" + ttime + "' where FLAG7 = '" + tSupplier + "' and FLAG3 = '" + tModel + "' and "
        //             + "  DATABOMID = 'IDBOM1002'  and  FLAG11 = '" + tSN + "'";
        //    v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        //    if (v5 > 0) v7 = v7 + v5;
        //}
        // End Test 
        // update 10, 21, nsert 22,  20³Ì«á³B²z
        tid = tid + DateTime.Now.ToString("yyyyMMddHHmmssmm");

        t4 = "Insert into ENGITMGT.ENGITTRS1011 ( Documentid, Docseq, F1, RDATE, DDATE,  "
                 + " F5, F6, F7, F8,F9, F10, F11, F12, "
                 + " F23, F24, F25, F26, F27, F28, "
                 + " F33, F34, F35, F36, F37, F38, "
                 + " F39, F40, F41,F42, F43, F44,F45,F46, "
                 + " F49, F50, F2, F3, F13, F14, "
                 + " F15, F17, F18, F19, F21, F29, F30, F31,F32, "
                 + " FLAG1, FLAG2, FLAG3,updatetime, F20, F22, F51, F52, F16, F47, F48 ) "
                 + " Values ( '" + v22Documentid + "','" + v22Docseq + "', 'NOKFOXT1022','" + tmpRdate + "','" + tmpDdate + "', "
                 + " 'Disp Ticket NO', '" + tid + "', 'SubSeq', '10001', 'NAME', '" + tSN + "', 'Trs Code', '51',  "
                 + " 'SIZE', '" + v21SIZEQty.ToString().Trim() + "', 'Used QTy', '0', 'UnUserd QTY', '" + v21ReqiureQty.ToString().Trim() + "',  "
                 + " 'Pre DOCM', '" + v21Documentid + "',  "
                 + " 'Pre DOCSEQ','" + v21Docseq + "', 'Pre 1010 SEQID', '" + v10SEQID + "',    "
                 + "  'STATUS', '1', 'MODEL NO', '10001', 'Name', '" + tModel + "', 'Supplier NO', '" + tSupplier + "', "
                 + " 'SP Code',  '" + tSP + "', 'Main Require each SN QTY A', 'Disp SN CODE', 'SN Haed','" + tSNHead + "', "
                 + "  'SN Issued', 'Name',  '" + tName + "', 'SN Start', 'SN End','Fail QTY','0', 'Resv QTy','0', "
                 + " 'BOM Attr', '10', '3', '" + ttime + "', '" + NewStart + "','" + NewEnd + "','" + v21factory + "','" + v21SNBlock + "', '" + v21CSTATUS + "', 'Name', '" + tSupplier + "' ) ";

        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
        if (v5 > 0)  // Insert OK
        {
            v7 = v7 + v5;
            RetGetQTY = v21ReqiureQty;
        }


        t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v10 + "', F26 = '" + v10Usedafter + "',  F28 = '" + v10UnUsedafter + "',  "
             + " updatetime = '" + ttime + "' where documentid = '" + v10Documentid + "' "
           + " and docseq = '" + v10Docseq + "' and  F1 = 'NOKFOXT1010'  and SEQID = '" + v10SEQID + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        if (v5 > 0) v7 = v7 + v5;

        t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v21 + "', F26 = '" + v21Usedafter + "', F28 = '" + v21UnUsedafter + "',   "
         + " updatetime = '" + ttime + "' where documentid = '" + v21Documentid + "' and docseq = '" + v21Docseq + "' and "
         + "  F1 = 'NOKFOXT1021'  and  SEQID = '" + v21SEQID + "'";
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        if (v5 > 0) v7 = v7 + v5;

        if (v21CSTATUS == "3") // Production Limit , Need Add Qty
        {
            // 20170606
            //t3 = " Update  ENGITMGT.DATABOM1001 set FLAG16 = to_number(FLAG16) + " + v21ReqiureQty + ",   "
            //         + " uptime = '" + ttime + "' where FLAG7 = '" + tSupplier + "' and FLAG3 = '" + tModel + "' and "
            //         + "  DATABOMID = 'IDBOM1002'  and  FLAG11 = '" + tSN + "'";
            // update  ENGITMGT.ENGITM1001 set F19 = 'limit Used Qty', F20 = '0' where F1 = 'NOKFOM1002' and F20 is null
            t3 = " Update  ENGITMGT.ENGITM1001 set F20= to_number(F20) + " + v21ReqiureQty + ",   "
               + " uptime = '" + ttime + "' where F6 = '" + tModel + "' and  F1 = 'NOKFOM1002' ";

            v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
            if (v5 > 0) v7 = v7 + v5;

        }

        return (v21ReqiureQty.ToString().Trim());

    }  // End GetSN_FromRecv12 
    public string GetSN_FromRecvBy1V03(string DBType, string tmpReadDBA, string L2DocumentID, string L2DOCSEQ, string L2SIZE, string L2UnUsed, int v22, ref DataSet DNdt2)
    {
        string tModel = DNdt2.Tables[0].Rows[v22]["F44"].ToString().Trim();
        string tSupplier = DNdt2.Tables[0].Rows[v22]["F46"].ToString().Trim();
        string tSP = DNdt2.Tables[0].Rows[v22]["F50"].ToString().Trim();
        string tSN = DNdt2.Tables[0].Rows[v22]["F10"].ToString().Trim();
        string v21Documentid = DNdt2.Tables[0].Rows[v22]["Documentid"].ToString().Trim();
        string v21Docseq = DNdt2.Tables[0].Rows[v22]["Docseq"].ToString().Trim();
        string v21id = DNdt2.Tables[0].Rows[v22]["F6"].ToString().Trim();
        string v21factory = DNdt2.Tables[0].Rows[v22]["F51"].ToString().Trim();
        string v21SNBlock = DNdt2.Tables[0].Rows[v22]["F52"].ToString().Trim();
        string v21SEQID = DNdt2.Tables[0].Rows[v22]["SEQID"].ToString().Trim();
        string v21CSTATUS = DNdt2.Tables[0].Rows[v22]["F16"].ToString().Trim();
        string FLAG3Offline = DNdt2.Tables[0].Rows[v22]["FLAG3"].ToString().Trim();


        string tmpProtoProduction = "Production";
        if (v21CSTATUS == "2") tmpProtoProduction = "Proto";


        int v21SIZEQty = Convert.ToInt32(L2SIZE);
        int v21SIZEUseQty = 0;
        int v21SIZEUnUseQty = Convert.ToInt32(L2UnUsed);

        string tid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string ttime = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        ttime = Currtime;

        string ReqUsed = DNdt2.Tables[0].Rows[v22]["F26"].ToString().Trim();
        string ReqUnUsed = DNdt2.Tables[0].Rows[v22]["F28"].ToString().Trim();

        int v10Used = 0, v10UnUsed = 0, v10Usedafter = 0, v10UnUsedafter = 0, v10SIZE = 0;
        int v21Used = 0, v21UnUsed = 0, v21UnUsedafter = 0, v21Usedafter = 0, v21ReqiureQty = 0;


        if (ReqUsed == "") v21Used = 0;
        else v21Used = Convert.ToInt32(ReqUsed);

        if (ReqUnUsed == "") v21UnUsed = 0;
        else v21UnUsed = Convert.ToInt32(ReqUnUsed);

        v21SIZEUnUseQty = v21UnUsed;

        if (v21UnUsed >= v21SIZEQty) return ("0");  // Require is full

        ////////////////////////////////////////////
        // 1. if not sn return.
        //      1.1 SN , Supplier
        //                  if enough  return
        //   if Not enough book and loop 1
        //
        // 2. 
        // end

        DataSet DN1 = null, DN2 = null, DN3 = null;

        string t1 = "", t2 = "", t3 = "", t4 = "", t5 = "", t6 = "", SNDigit = "", tmpSNBlock = "";
        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0, RetGetQTY = 0;

        string sw1 = "Y";
        int CurrSixe = 0;

        // Get SNTYPE 10, 16
        t1 = " select  *  from  ENGITMGT.ENGITM1001 where  f1 = 'NOFIDMM1101' and F10 =  '" + tSN + "' ";
        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        if (DN1.Tables[0].Rows.Count >= 1)
            if (DN1.Tables[0].Rows[v2]["F12"].ToString().Trim() != null)
            {
                SNDigit = DN1.Tables[0].Rows[0]["F12"].ToString().Trim();
                tmpSNBlock = DN1.Tables[0].Rows[0]["F11"].ToString().Trim();  // Block
            }

        if (v21SNBlock == "") v21SNBlock = tmpSNBlock;

        // First, Check have tSN, tSuppier 20170515 t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + tSN + "' "
        t1 = " select * from ENGITMGT.ENGITTRS1011 where F1 = 'NOKFOXT1010'  and F40 = '1' and F10 = '" + v21SNBlock + "' order  by Documentid, seqid  asc";
        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, tmpReadDBA, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        v1 = DN1.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-1");
        // Find then Book  Qty
        t2 = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED

        //Receive 010
        string tSNHead = DN1.Tables[0].Rows[v2]["F14"].ToString().Trim();
        string tIssued = DN1.Tables[0].Rows[v2]["F16"].ToString().Trim();
        string tName = DN1.Tables[0].Rows[v2]["F18"].ToString().Trim();


        string BUFStart = DN1.Tables[0].Rows[v2]["F20"].ToString().Trim();
        string BUFEnd = DN1.Tables[0].Rows[v2]["F22"].ToString().Trim();
        string BUFSIZE = DN1.Tables[0].Rows[v2]["F24"].ToString().Trim();
        string BUFUsed = DN1.Tables[0].Rows[v2]["F26"].ToString().Trim();
        string BUFUnUsed = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();

        string v10Documentid = DN1.Tables[0].Rows[v2]["Documentid"].ToString().Trim();
        string v10Docseq = DN1.Tables[0].Rows[v2]["Docseq"].ToString().Trim();
        string v10SEQID = DN1.Tables[0].Rows[v2]["SEQID"].ToString().Trim();
        string v22Documentid = DateTime.Now.ToString("yyyyMMddHHmmssmm");
        string v22Docseq = "10001";
        string v22FLAG3 = "3";

        string tmpRdate = DateTime.Now.ToString("yyyyMMdd");
        string tmpDdate = DateTime.Now.ToString("yyyy-MM-dd"); ;


        if (BUFUsed == "") v10Used = 0;
        else v10Used = Convert.ToInt32(BUFUsed);

        if (BUFUnUsed == "") v10UnUsed = 0;
        else v10UnUsed = Convert.ToInt32(BUFUnUsed);

        if (BUFSIZE == "") v10SIZE = 0;
        else v10SIZE = Convert.ToInt32(BUFSIZE);


        // modift  20170501 v21ReqiureQty = v21SIZEQty - v21UnUsed;  // Require ZISE - unUsed ( alraedy dispatch )

        v21ReqiureQty = v21SIZEQty - v21UnUsed;  // SIZE == UNUsed ªí¥Üµo§¹  20170509

        string NewStart = "", NewEnd = "";

        if (v21ReqiureQty <= 0) return ("0");

        v3 = v10UnUsed;
        v4 = v21UnUsed;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //  File        SIZE        Used        UnUSED      FLAG40    ­pºâ«á        SIZE        Used        UnUSED      FLAG40
        //  10          200000      0           100000      1                       200000      4000        160000      1  UnUsed = 200000 start, and decrease each time
        //  20          4000                                                                                                UnUsed = 200000 start,
        //  21  TAC     4000        0           0                                   4000        0           4000        2  UnUsed = 0 start, and upcrase each time
        //  21  OUI     4000        0           0                                   4000        0           4000        2
        //  22  TAC                                                                 4000        0           4000        2
        //  22  OUI                                                                 4000        0           4000        2
        //  
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // int v10Usedafter = 0, v10UnUsedafter = 0, v21UnUsedafter = 0;

        // Not enough , Can have Require only ±N©Ò¦³ 10 µ¹ 21
        if (v10UnUsed < v21ReqiureQty) // not enough
            v21ReqiureQty = v10UnUsed;

        string Ret11 = "";
        //if ( SNDigit == "16" )
        //     Ret11 = GetSNRangeD16(tSN, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit );      
        //else

        string AllocateCnt = "2";
        if (v10SIZE == v10UnUsed) AllocateCnt = "1";
        Ret11 = GetSNRangeD10V02(tSN, BUFSIZE, BUFStart, BUFEnd, BUFUnUsed, v21ReqiureQty, ref NewStart, ref NewEnd, SNDigit, AllocateCnt);

        v21UnUsedafter = v21UnUsed + v21ReqiureQty;
        v21Usedafter = v21SIZEQty - v21UnUsedafter; // v21Used + v21ReqiureQty;

        v10UnUsedafter = v10UnUsed - v21ReqiureQty;
        v10Usedafter = v10SIZE - v10UnUsedafter; //  v10Used + v21ReqiureQty;

        string F40v10 = "1", F40v20 = "1", F40v21 = "1", F40v22 = "1";

        if (v10UnUsedafter <= 0) F40v10 = "4";
        if (v21UnUsedafter >= v21SIZEQty) F40v21 = "4";

        if (FLAG3Offline != "2") v22FLAG3 = FLAG3Offline; // Offline

        // update 10, 21, nsert 22,  20³Ì«á³B²z
        tid = tid + DateTime.Now.ToString("yyyyMMddHHmmssmm");

        t4 = "Insert into ENGITMGT.ENGITTRS1011 ( Documentid, Docseq, F1, RDATE, DDATE,  "
                 + " F5, F6, F7, F8,F9, F10, F11, F12, "
                 + " F23, F24, F25, F26, F27, F28, "
                 + " F33, F34, F35, F36, F37, F38, "
                 + " F39, F40, F41,F42, F43, F44,F45,F46, "
                 + " F49, F50, F2, F3, F13, F14, "
                 + " F15, F17, F18, F19, F21, F29, F30, F31,F32, "
                 + " FLAG1, FLAG2, FLAG3,updatetime, F20, F22, F51, F52, F16, F47, F48 ) "
                 + " Values ( '" + v22Documentid + "','" + v22Docseq + "', 'NOKFOXT1022','" + tmpRdate + "','" + tmpDdate + "', "
                 + " 'Disp Ticket NO', '" + tid + "', 'SubSeq', '10001', 'NAME', '" + tSN + "', 'Trs Code', '51',  "
                 + " 'SIZE', '" + v21SIZEQty.ToString().Trim() + "', 'Used QTy', '0', 'UnUserd QTY', '" + v21ReqiureQty.ToString().Trim() + "',  "
                 + " 'Pre DOCM', '" + v21Documentid + "',  "
                 + " 'Pre DOCSEQ','" + v21Docseq + "', 'Pre 1010 SEQID', '" + v10SEQID + "',    "
                 + "  'STATUS', '1', 'MODEL NO', '10001', 'Name', '" + tModel + "', 'Supplier NO', '" + tSupplier + "', "
                 + " 'SP Code',  '" + tSP + "', 'Main Require each SN QTY A', 'Disp SN CODE', 'SN Haed','" + tSNHead + "', "
                 + "  'SN Issued', 'Name',  '" + tName + "', 'SN Start', 'SN End','Fail QTY','0', 'Resv QTy','0', "
                 + " 'BOM Attr', '10', '" + v22FLAG3 + "', '" + ttime + "', '" + NewStart + "','" + NewEnd + "','" + v21factory + "','" + v21SNBlock + "', '" + v21CSTATUS + "', 'Name', '" + tSupplier + "' ) ";
            //     + " 'BOM Attr', '10', '3', '" + ttime + "', '" + NewStart + "','" + NewEnd + "','" + v21factory + "','" + v21SNBlock + "', '" + v21CSTATUS + "', 'Name', '" + tSupplier + "' ) ";

        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t4);
        if (v5 > 0)  // Insert OK
        {
            v7 = v7 + v5;
            RetGetQTY = v21ReqiureQty;
        }

        t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v10 + "', F26 = '" + v10Usedafter + "',  F28 = '" + v10UnUsedafter + "',  "
             + " updatetime = '" + ttime + "' where documentid = '" + v10Documentid + "' "
           + " and docseq = '" + v10Docseq + "' and  F1 = 'NOKFOXT1010'  and SEQID = '" + v10SEQID + "' ";
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        if (v5 > 0) v7 = v7 + v5;

        t3 = " Update  ENGITMGT.ENGITTRS1011 set F40 = '" + F40v21 + "', F26 = '" + v21Usedafter + "', F28 = '" + v21UnUsedafter + "',   "
         + " updatetime = '" + ttime + "' where documentid = '" + v21Documentid + "' and docseq = '" + v21Docseq + "' and "
         + "  F1 = 'NOKFOXT1021'  and  SEQID = '" + v21SEQID + "'";
        v5 = PDataBaseOperation.PExecSQL(DBType, tmpReadDBA, t3);
        if (v5 > 0) // Update array in memory 
        {
            DNdt2.Tables[0].Rows[v22]["F40"] = F40v21.ToString().Trim();
            DNdt2.Tables[0].Rows[v22]["F26"] = v21Usedafter.ToString().Trim();
            DNdt2.Tables[0].Rows[v22]["F28"] = v21UnUsedafter.ToString().Trim();
        }

        if (v21CSTATUS == "3") // Production Limit , Need Add Qty
        {
            //v5 = UpdatelimitQty(ref DBType, ref tmpReadDBA, ref v21ReqiureQty, ref ttime, ref tModel);
            //if (v5 > 0) v7 = v7 + v5;
        }
        
        if (v5 > 0) v7 = v7 + v5;

        return (v21ReqiureQty.ToString().Trim());

    }  // End GetSN_FromRecvV03 

    public string GetSNRangeD10(string tSN, string BUFStart, string BUFEnd, string BUFUnUsed, int v21ReqiureQty, ref string NewStart, ref string NewEnd, string SNDigit) 
    {
        // Start - END, 1000-2000, Unused 400, Require Get 150
        // New start = ( 2000-400 ) = 1600 + 1 = 1601
        // New End   = 1600+150 = 1750
        int v10StartLoc = 0, v10EndLoc = 0, v10UnUsed = 0; 
        if (BUFStart    == "") v10StartLoc     = 0;  else v10StartLoc = Convert.ToInt32(BUFStart);
        if (BUFEnd      == "") v10EndLoc       = 0;  else v10EndLoc = Convert.ToInt32(BUFEnd);
        if (BUFUnUsed   == "") v10UnUsed       = 0;  else v10UnUsed = Convert.ToInt32(BUFUnUsed);

        int NewStartLoc = 0, NewEndLoc = 0;

        NewStartLoc = (v10EndLoc - v10UnUsed) + 1;
        NewEndLoc   = NewStartLoc + v21ReqiureQty;

        NewStart = NewStartLoc.ToString().Trim();
        NewEnd   = NewEndLoc.ToString().Trim();


        //string Ret11 = GetSNRange(tSN, BUFStart, BUFEnd, BUFUnused, v21ReqiureQty, ref NewStart, ref NewEnd);      

            return ("");
    }   // end get new SN range


    
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //  Requireqty  OrgStart    OrdEnd     SIZE    UnUsed       New Start                                                   New End
    //  ===========================================================================================================================================
    //  11          000000      99999    10000   10000          if ( ZISE=UnUsed)  New Start = OrgTart = 00000              New Start + Req -1 = 0  + 11 -1 = 10 
    //  12          000000      99999    10000    9989          if ( ZISE!=UnUsed) New Start = SIZE - UnUsed -1 + 1 = 11    New Start + Req -1 = 11 + 12 -1 = 22 
    //
    public string GetSNRangeD10V02(string tSN, string BUFSIZE, string BUFStart, string BUFEnd, string BUFUnUsed, int v21ReqiureQty, ref string NewStart, ref string NewEnd, string SNDigit, string ACnt)
    {
        // Start - END, 1000-2000, Unused 400, Require Get 150
        // New start = ( 2000-400 ) = 1600 + 1 = 1601
        // New End   = 1600+150 = 1750
        int v10StartLoc = 0, v10EndLoc = 0, v10UnUsed = 0, v10SIZE = 0;
        if (BUFStart == "") v10StartLoc = 0; else v10StartLoc = Convert.ToInt32(BUFStart);
        if (BUFEnd == "") v10EndLoc = 0; else v10EndLoc = Convert.ToInt32(BUFEnd);
        if (BUFUnUsed == "") v10UnUsed = 0; else v10UnUsed = Convert.ToInt32(BUFUnUsed);
        if (BUFSIZE == "") v10SIZE = 0; else v10SIZE = Convert.ToInt32(BUFSIZE);

        int NewStartLoc = 0, NewEndLoc = 0;

        if (v10SIZE == v10UnUsed) ACnt = "1";
        else ACnt = "2";


        // ²Ä¤@¦¸
        if ((ACnt == "1") || (ACnt == ""))
                NewStartLoc = v10StartLoc;
        else  // Second Time
                // 20170602 NewStartLoc = (v10SIZE - v10UnUsed) + 1 -1;
                NewStartLoc = (v10SIZE - v10UnUsed) + 1 - 1 + v10StartLoc;

        NewEndLoc = NewStartLoc + v21ReqiureQty - 1;
        NewStart = NewStartLoc.ToString().Trim();
        NewEnd = NewEndLoc.ToString().Trim();

        //NewStartLoc = (v10EndLoc - v10UnUsed) + 1;
        //NewEndLoc = NewStartLoc + v21ReqiureQty;
        //NewStart = NewStartLoc.ToString().Trim();
        //NewEnd = NewEndLoc.ToString().Trim();


        //string Ret11 = GetSNRange(tSN, BUFStart, BUFEnd, BUFUnused, v21ReqiureQty, ref NewStart, ref NewEnd);      

        return ("");
    }   // end get new SN range

    public string GetSNRangeD10V01(string tSN, string BUFStart, string BUFEnd, string BUFUnUsed, int v21ReqiureQty, ref string NewStart, ref string NewEnd, string SNDigit, string ACnt)
    {
        // Start - END, 1000-2000, Unused 400, Require Get 150
        // New start = ( 2000-400 ) = 1600 + 1 = 1601
        // New End   = 1600+150 = 1750
        int v10StartLoc = 0, v10EndLoc = 0, v10UnUsed = 0;
        if (BUFStart == "") v10StartLoc = 0; else v10StartLoc = Convert.ToInt32(BUFStart);
        if (BUFEnd == "") v10EndLoc = 0; else v10EndLoc = Convert.ToInt32(BUFEnd);
        if (BUFUnUsed == "") v10UnUsed = 0; else v10UnUsed = Convert.ToInt32(BUFUnUsed);

        int NewStartLoc = 0, NewEndLoc = 0;


        // ²Ä¤@¦¸
        if ((ACnt == "1") || (ACnt == ""))
        {
            NewStartLoc = v10StartLoc;
            NewEndLoc = v10StartLoc + v21ReqiureQty - 1;

            NewStart = NewStartLoc.ToString().Trim();
            NewEnd = NewEndLoc.ToString().Trim();

        }
        else  // Second Time
        {
            NewStartLoc = (v10EndLoc - v10UnUsed);
            NewEndLoc = NewStartLoc + v21ReqiureQty - 1;

            NewStart = NewStartLoc.ToString().Trim();
            NewEnd = NewEndLoc.ToString().Trim();
        }


        //NewStartLoc = (v10EndLoc - v10UnUsed) + 1;
        //NewEndLoc = NewStartLoc + v21ReqiureQty;
        //NewStart = NewStartLoc.ToString().Trim();
        //NewEnd = NewEndLoc.ToString().Trim();


        //string Ret11 = GetSNRange(tSN, BUFStart, BUFEnd, BUFUnused, v21ReqiureQty, ref NewStart, ref NewEnd);      

        return ("");
    }   // end get new SN range

    // Inout 10 Start End
    public string GetSNRangeD16(string tSN, string BUFStart, string BUFEnd, string BUFUnUsed, int v21ReqiureQty, ref string NewStart, ref string NewEnd, string SNDigit)
    {
        // Start - END, 1000-2000, Unused 400, Require Get 150
        // New start = ( 2000-400 ) = 1600 + 1 = 1601
        // New End   = 1600+150 = 1750
        int v10StartLoc = 0, v10EndLoc = 0, v10UnUsed = 0;
        if (BUFStart == "") v10StartLoc = 0; else v10StartLoc = Convert.ToInt32( ConvQtyTypeV01(BUFStart,SNDigit));   // 16 -> 10
        if (BUFEnd == "") v10EndLoc = 0; else v10EndLoc = Convert.ToInt32( ConvQtyTypeV01(BUFEnd,SNDigit));
        if (BUFUnUsed == "") v10UnUsed = 0; else v10UnUsed = Convert.ToInt32(BUFUnUsed);

        int NewStartLoc = 0, NewEndLoc = 0;

        NewStartLoc = (v10EndLoc - v10UnUsed) + 1;
        NewEndLoc = NewStartLoc + v21ReqiureQty;

        NewStart = ConvQtyTypeV02(NewStartLoc.ToString().Trim(), SNDigit );
        NewEnd   = ConvQtyTypeV02(NewEndLoc.ToString().Trim(), SNDigit); 


        //string Ret11 = GetSNRange(tSN, BUFStart, BUFEnd, BUFUnused, v21ReqiureQty, ref NewStart, ref NewEnd);      

        return ("");
    }   // end get new SN range

    public string RecLockFile(string DbType, string DBAStr, string RFile, string tDocumentid, string tDOCSEQ, string InRandom)
    {
        string t1 = "select *  from " + RFile + " where docuemntid = '" + tDocumentid + "' and docseq = '" + tDOCSEQ + "' ";
        DataSet DN1 = PDataBaseOperation.PSelectSQLDS(DBType, DBAStr, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        // if ((DNdt1 != null) && (DNdt1.Tables[0].Rows.Count <= 0)) return ("");

        int v1 = DN1.Tables[0].Rows.Count;
        if (v1 <= 0) return ("-3");

        int v2 = 0, v3 = 50, v4 = 0, v5 = 0;
        string t2 = "", t3 = "", t4 = "", t5 = "";
        string t6 = DateTime.Now.ToString("yyyyMMddHHmmssmm");

        for (v2 = 0; v2 < v3; v2++)
        {
            t2 = DN1.Tables[0].Rows[v2]["F50"].ToString().Trim();
            t3 = DN1.Tables[0].Rows[v2]["F51"].ToString().Trim();
            t2 = DN1.Tables[0].Rows[v2]["F28"].ToString().Trim();//F24, F26,F28 SIZE, USED,UNUSED

            if ((t2 == null) && (t3 == null))
                v2 = v3;
            else
                Thread.Sleep(1000);	 // 10 Min 6000 000---¥ð¯v2¬íÄÁ 2000   
        }

        t4 = InRandom;
        if ( t4 == "" ) t4 =  DateTime.Now.ToString("yyyyMMddHHmmssmm") + t6;

        t3 = " Update " + RFile + " set F50 = '" + t4 + "', F51 = '" + t4 + "' "
           + " where docuemntid = '" + tDocumentid + "' and docseq = '" + tDOCSEQ + "'  ";
        v5 = PDataBaseOperation.PExecSQL(DBType, DBAStr, t3);
        if (v5 > 0) t5 = t4;
        else t5 = "";

        return ( t5 );
    }


    // string ret1 = FGISlibPointer.putSNProfileSFC(DBOracle, tmpReadDBA, ReadFile, DBOracle, tmpWritDBA, WriFile, RTableF1);   
    public string putSNProfileSFC(string readDBType, string readDB, string ReadFile, string writeDBType, string writeDB, string WriFile, string RTableF1, ref DataSet DSFGISM1001)
    {
        
        string t1 = "", t2 = "", t3 = "", t4 = "", tmpWriStr="", t5 = "";
        DataSet DN1 = null, DN2 = null;  // DN1 = FGIS, DN2 = SFC
        t1 = "select * from " + ReadFile + " where DATABOMID = '" + RTableF1 + "' ";  // Read
        DN1 = PDataBaseOperation.PSelectSQLDS(DBType, readDB, t1);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");

        t2 = "select * from " + ReadFile + " where DATABOMID = '" + RTableF1 + "' ";   // Write
        DN2 = PDataBaseOperation.PSelectSQLDS(writeDBType, writeDB, t2);
        if (DN2 == null) return ("-1");
        if (DN2.Tables.Count <= 0) return ("-2");


        int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5=0, v6=0, v7=0;
        int DN1Cnt = DN1.Tables[0].Rows.Count;
        int DN2Cnt = DN2.Tables[0].Rows.Count;
        string DN1FLAG3 = "", DN1FLAG7 = "";  // FGIS
        string DN2FLAG3 = "", DN2FLAG7 = "";  // SFC
        string SW1 = "N";
        string tDATABOMID = "";
        string tSITE = "";
        string tMODEL = "";
        string tITEM = "";
        string tSEQ = "";
        string tDUEDATE = "";
        string tFLAG1 = "";
        string tFLAG2 = "";
        string tFLAG3 = "";
        string tFLAG4 = "";
        string tFLAG5 = "";
        string tFLAG6 = "";
        string tFLAG7 = "";
        string tFLAG12 = "";
        string tCSTATUS = "";
        string tUPTIME = "";
        string tFLAG13 = "";
        string tSEQID  = "";

              
            
        
        for (v1 = 0; v1 < DN1Cnt; v1++)   // FGIS
        {
            DN1FLAG3 = DN1.Tables[0].Rows[v1]["FLAG3"].ToString().Trim();  // FGIS ¾÷ÀY
            DN1FLAG7 = DN1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim();  // Supplier

            SW1 = "Y";
            for (v2 = 0; v2 < DN2Cnt; v2++)  // Loop Check SFC
            {
                DN2FLAG3 = DN2.Tables[0].Rows[v2]["FLAG3"].ToString().Trim();  // FGIS ¾÷ÀY
                DN2FLAG7 = DN2.Tables[0].Rows[v2]["FLAG7"].ToString().Trim();  // Supplier

                if ((DN1FLAG3 == DN2FLAG3) && (DN1FLAG7 == DN2FLAG7))
                {
                    v2 = DN2Cnt; // Break
                    SW1 = "N";
                }
            }

            if ( SW1 == "Y" )  // Not Found then Insert 
            {
                tDATABOMID = DN1.Tables[0].Rows[v1]["DATABOMID"].ToString().Trim(); 
                tSITE = DN1.Tables[0].Rows[v1]["SITE"].ToString().Trim(); 
                tMODEL = DN1.Tables[0].Rows[v1]["MODEL"].ToString().Trim(); 
                tITEM = DN1.Tables[0].Rows[v1]["ITEM"].ToString().Trim(); 
                tSEQ = DN1.Tables[0].Rows[v1]["SEQ"].ToString().Trim(); ;
                tDUEDATE = DN1.Tables[0].Rows[v1]["DUEDATE"].ToString().Trim(); 
                tFLAG1 = DN1.Tables[0].Rows[v1]["FLAG1"].ToString().Trim(); 
                tFLAG2 = DN1.Tables[0].Rows[v1]["FLAG2"].ToString().Trim(); 
                tFLAG3 = DN1.Tables[0].Rows[v1]["FLAG3"].ToString().Trim(); 
                tFLAG4 = DN1.Tables[0].Rows[v1]["FLAG4"].ToString().Trim(); 
                tFLAG5 = DN1.Tables[0].Rows[v1]["FLAG5"].ToString().Trim(); 
                tFLAG6 = DN1.Tables[0].Rows[v1]["FLAG6"].ToString().Trim(); 
                tFLAG7 = DN1.Tables[0].Rows[v1]["FLAG7"].ToString().Trim(); 
                tFLAG12 = DN1.Tables[0].Rows[v1]["FLAG12"].ToString().Trim(); 
                tCSTATUS = DN1.Tables[0].Rows[v1]["CSTATUS"].ToString().Trim(); 
                tUPTIME = DN1.Tables[0].Rows[v1]["UPTIME"].ToString().Trim(); 
                tFLAG13 = DN1.Tables[0].Rows[v1]["FLAG13"].ToString().Trim(); 
                tSEQID  = DN1.Tables[0].Rows[v1]["SEQID"].ToString().Trim();
                //t5      = DN1.Tables[0].Rows[v1]["F54"].ToString().Trim();
                tFLAG13 = tSEQID;

                tmpWriStr = writeDB;
                if ( t5 != "" ) tmpWriStr = t5;

                t4 = "Insert into " + WriFile + " ( DATABOMID, SITE, MODEL, ITEM, SEQ, DUEDATE,  "
                    + " FLAG1, FLAG2, FLAG3, FLAG4,FLAG5, FLAG6, FLAG7, FLAG12, "
                    + " CSTATUS, UPTIME, FLAG13 ) "
                    + " Values ( '" + tDATABOMID + "','" + tSITE + "', '" + tMODEL + "','" + tITEM + "','" + tSEQ + "','" + tDUEDATE + "', "
                    + " '" + tFLAG1 + "', '" + tFLAG2 + "', '" + tFLAG3 + "', '" + tFLAG4 + "', '" + tFLAG5 + "', '" + tFLAG6 + "','" + tFLAG7 + "', '" + tFLAG12 + "',"
                    + " '" + tCSTATUS + "', '" + tUPTIME + "', '" + tFLAG13 + "'  ) ";

                v5 = PDataBaseOperation.PExecSQL(writeDBType, tmpWriStr, t4);
                if (v5 > 0) v7 = v7 + v5;


            }
            
           
        }
        return ( v7.ToString().Trim());
    }


    // 16 -> 10
    public string ConvQtyTypeV01(string str, string NumType )
    {
        int v1 = 0, v2 = 0;
        if (NumType != "") v2 = Convert.ToInt32(NumType);
        else               v2 = 10;
             
            
        try
        {
            v1 = Convert.ToInt32(str, v2);
            return v1.ToString().Trim();   // Convert.ToString(v1, 10);
        }
        catch (Exception e)
        {
            throw e;
        }

        return(v1.ToString().Trim());
    }

    // 10 -> 16
    public string ConvQtyTypeV02(string str, string NumType)
    {
        int v1 = 0, v2 = 0;
        if (NumType != "") v2 = Convert.ToInt32(NumType);
        else v2 = 10;

        string s1 = "";
        v1 = Convert.ToInt32(str);

        try
        {
            // s1 = Convert.ToString(str, v2);
            s1 = Convert.ToString(v1, v2);
            return s1.ToString().Trim();   // Convert.ToString(v1, 10);
        }
        catch (Exception e)
        {
            throw e;
        }

        return (s1.ToString().Trim());
    }

    private string TrsSecondIP(string infile, string inpno)
    {
        string R1 = ReadParaTxt(infile, inpno); // ReadParaTxt("WebReadParam.txt", "40030"); 
        string R2 = ReadParaTxt(infile, R1);
        return (R2);
    }


    private string ReadParaTxt(string FilePara, string ParaNum)
    {
        string retPara = "";
     //   int ArrMax = 500;
     //   string[] ReadTxtArray = new string[ArrMax];
     //   string FileName = "SetReadParam.txt";
     //   if (FilePara != "") FileName = FilePara;
     //   string ServerPath = Server.MapPath("~\\" + FileName);
     //   FileInfo fi = new FileInfo(ServerPath);
     //   StreamReader sr = fi.OpenText();
     //   string InString = "";
     //   int i = 0, strlen = 0;

     //   while (((InString = sr.ReadLine()) != null) && (i < ArrMax))
     //   {
     //       ReadTxtArray[i] = InString;
     //                 if ((InString != "") && (InString != " ") && (InString.Substring(0, 2) != "//"))
     //       {
     //           strlen = InString.Length - 1;
     //           if ((InString.Substring(0, 5) == ParaNum) && (strlen >= 6))
     //           {
     //               retPara = InString.Substring(6, strlen - 5);
     //               i = ArrMax;  // Break
     //           }
     //
     //       }
     //       i++;
     //
     //   }

     //   sr.Close();
        return (retPara);
    }

    public string AlarmV01(string DBOracle, string tmpReadDBA, int tac_alarmLimit, int oui_alarmLimit)
	{
		//
		// TODO: Add constructor logic here
		//
        string t1 = "", t2="";
        string Ret1 = "";
        string Ret2 = "";
        string remain_qty = "";
        t2 = " SELECT F24, F28 from ENGITMGT.ENGITTRS1011 WHERE f1='NOKFOXT1010' and F24 is null and F28 is null  and F10 = 'TAC' ";
        DataSet DN1 = PDataBaseOperation.PSelectSQLDS(DBOracle, tmpReadDBA, t2);
     
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        int DN1Cnt = DN1.Tables[0].Rows.Count;

        if ( DN1Cnt < tac_alarmLimit )
        {
             t1 = "there is only " + DN1Cnt + " free TAC serial number," + "Alarm limited is: " + tac_alarmLimit + "";
                    Ret1 = SendGMail(DBOracle, tmpReadDBA, "", "", "MAILGROUP", "SN101MAILGROUP", t1);
        }

        remain_qty = "SELECT F10,SUM(F28) as totunused, SUM(F24) as totsize FROM ENGITMGT.ENGITTRS1011 WHERE f1='NOKFOXT1010'and F40='1' and F10 = 'OUI' GROUP BY F10";
        DataTable qtydt = PDataBaseOperation.PSelectSQLDT(DBOracle, tmpReadDBA, remain_qty);
        if (DN1 == null) return ("-1");
        if (DN1.Tables.Count <= 0) return ("-2");
        DN1Cnt = DN1.Tables[0].Rows.Count;
        int i = 0, v1=0, v2=0;


        if (qtydt.Rows[i]["F10"].ToString().Trim() == "OUI")
        {
            int oui_Unused = Convert.ToInt32(qtydt.Rows[i]["totunused"]);
            int oui_size = Convert.ToInt32(qtydt.Rows[i]["totsize"]);
            v1 = 100/oui_alarmLimit;  // 100/5=20
            v2 = oui_Unused * v1;     // 
            if (oui_size > v2)
            {
                string ouiInfo = "there is only " + oui_Unused + " free OUI serial number," +
                                 "Alarm limited is: " + oui_alarmLimit + "";
                //Ret5 = FGISlibPointer.SendGMail(DBOracle, tmpReadDBA,"FGIS.GMAILBOX", "MAILGROUP", "SN1GROUP", oui_remain, "", oui_alarmLimit, "", "", "");
                Ret2 = SendGMail(DBOracle, tmpReadDBA, "", "", "MAILGROUP", "SN101MAILGROUP", ouiInfo);
            }
        }

        return Ret1 + Ret2;
        // return ("");
    }

}  // end public class FGISlib
}  // end namespace SFC.TJWEB


