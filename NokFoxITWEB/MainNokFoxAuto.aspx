﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainNokFoxAuto.aspx.cs" Inherits="MainNokFoxAuto" StylesheetTheme="SkinFile"%>

<%@ Register Assembly="C1.Web.Command.2" Namespace="C1.Web.Command" TagPrefix="c1c" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SSI系统測試環境</title>
    <style type="text/css">
     .navPoint 
      { FONT-SIZE: 10px; CURSOR: hand; COLOR: Blue; FONT-FAMILY: Webdings }
        .style1
        {
            height: 116%;
            width: 78px;
        }
        .style2
        {
            background-color: #F3F7F9;
            border: solid 1px #69B2E4;
            height: 116%;
            width: 83%;
        }
        .style3
        {
            height: 34px;
        }
        .style4
        {
            height: 34px;
            width: 651px;
            font-weight: 700;
        }
        .style5
        {
            width: 651px;
        }
        .style6
        {
            height: 28px;
            width: 651px;
        }
        .style7
        {
            height: 28px;
        }
        .style8
        {
            width: 651px;
            font-weight: 700;
        }
    </style>

       <script language="javascript" type="text/javascript">

           function switchTopicBar() {
               if (document.getElementById("TopicBar").style.display == "none") {
                   document.getElementById("TopicBar").style.display = "";
                   document.getElementById("switchPoint").innerText = 3;
               }
               else {
                   document.getElementById("TopicBar").style.display = "none";
                   document.getElementById("switchPoint").innerText = 4;
               }
           }

            
    </script>

</head>
<body>
    <form id="fmMain" runat="server">
        <div>
            <table style="height: 111%; width: 1022px;">
                <tr>
                    <td colspan="3" class="header">
                        <div id="logo">
                        </div>
                        <div class="banner">
                            &nbsp;&nbsp;
                        <asp:TextBox ID="TextBox3" runat="server" Height="20px" Width="235px"></asp:TextBox>
&nbsp;&nbsp;
                        <asp:Button ID="Button3" runat="server" Height="19px" onclick="Button3_Click" 
                            Text="進入新網站" Width="80px" />                           
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="nav">
                        <div class="welcome" style="color: red">
                            <asp:Label ID="lblUserName" runat="server" Text="Admin ," SkinID="lblWelcome"></asp:Label>
                            Welcome20130622</div>
                        <div class="banner02">
                            <div id="home">
                                <ul>
                                    <li><a href="Main.aspx">首頁</a> | </li>
                                    <li><a>幫助</a> | </li>
                                    <li>
                                        <asp:LinkButton ID="btnLogout" runat="server" Height="7px" 
                                            onclick="btnLogout_Click"  >退出</asp:LinkButton>|</li>
                                </ul>
                            </div>
                        </div>
                    &nbsp; UserName:
                        <asp:TextBox ID="TextBox1" runat="server" Height="20px" Width="79px"></asp:TextBox>
&nbsp;&nbsp;&nbsp; Password:&nbsp;
                        <asp:TextBox ID="TextBox2" runat="server" Height="20px" Width="110px" 
                            ontextchanged="TextBox2_TextChanged" 
                            TextMode="Password"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" Height="19px" onclick="Button1_Click" 
                            Text="登錄網站" Width="60px" />
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    &nbsp;<asp:Button ID="Button2" runat="server" Height="19px" onclick="Button2_Click" 
                            Text="修改" Width="35px" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td id="TopicBar" class="leftMenu" style="width: 20%; height: 100%;" valign="top">
                        <table cellpadding="0" cellspacing="0" style="width: 36%; height: 75%;">
                            <tr>
                                <td style="width: 100%; height: 668px;" valign="top">
                        <div id="time">
                            <script language="JavaScript" type="text/javascript">
                                var enabled = 0; today = new Date();
                                var day; var date;
                                if(today.getDay()==0) day = "星期日"
                                if(today.getDay()==1) day = "星期一"
                                if(today.getDay()==2) day = "星期二"
                                if(today.getDay()==3) day = "星期三"
                                if(today.getDay()==4) day = "星期四"
                                if(today.getDay()==5) day = "星期五"
                                if(today.getDay()==6) day = "星期六"
                                document.fgColor = "000000";
                                date ="" + (today.getYear()) + "/" + (today.getMonth() + 1 ) + "/" + today.getDate() + " " +" ";
                                
                                document.write("<FONT COLOR=000000>" + date +'['+day+']'+"</FONT>");
                            </script>                          
                         &nbsp;
                    
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                                <ContentTemplate>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="145px" 
                                ImageUrl="~/Picture/PictS1.jpg" Width="149px" />
                            <br />
                            <asp:Button ID="Button15" runat="server" Text="Screen Desp" Width="160px" 
                                BackColor="White" />
                            <br />
                            <asp:Button ID="Button16" runat="server" Text="Screen Desp" Width="160px" 
                                BackColor="White" />
                            <br />
                                    <asp:ImageButton ID="ImageButton2" runat="server" Height="143px" 
                                ImageUrl="~/Picture/PictS1.jpg" Width="154px" /> 
                                        <asp:Timer ID="Timer1" runat="server" ontick="Timer1_Tick" Interval="1800000" 
                                            Enabled="False">
                                    </asp:Timer>
                                    <br />
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                                &nbsp;&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="copyRight">
                                    CopyRight &copy; 2010<br />
                                    SSI Develop</td>
                            </tr>
                        </table>
                    </td>
                    <td id="SwitchBar" onclick="switchTopicBar()" style="vertical-align: middle; " 
                        class="style1">
                        <span id="switchPoint" class="navPoint" style="font-size: 8px">3</span>
                    </td>
                    <td class="style2">
                        
                        <table style="height: 703px;">
                            <tr>
                                <td class="style6">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                    ZZ&nbsp; SCM, SFC, B2B AUtomatic BackGround Running&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button17" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text="系統自動啟動" Width="117px" onclick="Button17_Click" />
                                </td>
                                <td class="style7">
                                </td>
                                <td class="style7">
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
&nbsp;1. System Run Automatic 30 Min&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button18" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text=" (101) 說明" Width="95px" />
                                    &nbsp;&nbsp;<asp:Button ID="Button28" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text=" (102)  說  明" Width="100px" />
                                </td>
                                <td class="style3">
                                </td>
                                <td class="style3">
                                </td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    &nbsp;2. Function Selection&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button30" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text=" (102)  說  明" Width="100px" />
&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="Button31" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text=" (102)  說  明" Width="100px" />
                                </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    &nbsp;
                                    <asp:Button ID="Button35" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text=" Quit Process" Width="100px" onclick="Button35_Click" />
&nbsp;
                                    <asp:Button ID="Button36" runat="server" BackColor="#99FF99" 
                                        BorderColor="White" BorderStyle="Dotted" ForeColor="#CC00FF" Height="20px" 
                                        Text=" Desp" Width="100px" />
&nbsp;&nbsp;
                                </td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4">
                                    &nbsp;Function: 1. Tiptop Link SFC&nbsp;&nbsp; 2. Set DN Control File Automatic</td>
                                <td class="style3">
                                    &nbsp;</td>
                                <td class="style3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style8">
                                    &nbsp;Currency Status :&nbsp; &nbsp;<asp:TextBox ID="TextBox4" runat="server" 
                                        Height="25px" Width="439px"></asp:TextBox>
                                </td>
                                <td>
                                    </td>
                                <td>
                                    </td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
