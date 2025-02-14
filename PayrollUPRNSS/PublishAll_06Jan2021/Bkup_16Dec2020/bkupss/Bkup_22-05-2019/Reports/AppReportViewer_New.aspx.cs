using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Data;
using System.Globalization;
using CrystalDecisions.Shared;
using System.Data;
//using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Interop.Excel.Application;

using ClosedXML.Excel;
using System.Configuration;
using System.Data.SqlClient;


public partial class Reports_AppReportViewer_New : System.Web.UI.Page
{
    clsEmpPayStructure clsObj = new clsEmpPayStructure();
    ReportDocument ObjRpt = new ReportDocument();
    clsPrintingReport clsRpt = new clsPrintingReport();
    string strMonthYear = "";
    string rptDeptName = "उत्तर प्रदेश राज्य निर्माण सहकारी संघ लि. (यू.पी.आर.एन.एस.एस.)";
    string rptDeptTitle1 = "";
    string rptDeptTitle3 = "";
    string rptDeptTitle2 = "";
    string rptAddress = "जी-4/5,बी,सेक्टर-4,गोमती नगर विस्तार,लखनऊ";
    string strBillAmount = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ShowReport();
        }
        catch (Exception ex)
        {
            //lblMessage.Text = clsGen.GenExceptionHandler(ex);
        }
    }

    private void ShowReport()
    {
        try
        {
            Int32 RptType = Convert.ToInt32(Request.QueryString["repno"]);
            Int32 UnitKey = Convert.ToInt32(Request.QueryString["UnitKey"].ToString());
            string UnitName = Convert.ToString(Request.QueryString["UnitName"]);
            Int16 MonthKey = Convert.ToInt16(Request.QueryString["MonthKey"]);
            Int32 YearKey = Convert.ToInt32(Request.QueryString["YearKey"]);
            string MonthText = Convert.ToString(Request.QueryString["MonthText"]);
            string YearText = Convert.ToString(Request.QueryString["YearText"]);
            string Billno = Convert.ToString(Request.QueryString["Billno"]);
            string Empkey = Convert.ToString(Request.QueryString["EmpKey"]);

            DataSet ds = new DataSet();
            string RptPath = "~/Reports/";
            DataSet DSResults = new DataSet();

            if (RptType == 1)  // All Emp Paybill report (Regular)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetCompiledPaybill_UPRNSS(MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptCompiledPaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["Total1"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);

                    ObjRpt.SetParameterValue("InWords", inword);
                    ObjRpt.SetParameterValue("Billno", Billno);                    

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "CompiledPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }
            if (RptType == 2)  // Employee wise Paybill report (Regular)
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwisePaybill_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey));
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptEmpwisePaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["Total1"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    ObjRpt.SetParameterValue("Billno", Billno);      
                    ObjRpt.SetParameterValue("InWords", inword);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }
            if (RptType == 3)  // All Emp Paybill report (Agreement)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetCompiledPaybillAgreement_UPRNSS(MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptAgrrmntEmpPaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["basicpay"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    ObjRpt.SetParameterValue("Billno", Billno);      
                    ObjRpt.SetParameterValue("InWords", inword);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "AgrrmntEmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

            if (RptType == 5)  // All Emp Paybill report (Deputation)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetCompiledPaybillDeputation_UPRNSS(MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptDeputationCompiledPaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double  netsal = 0;
                    for (int a = 0; a< DSResults.Tables[0].Rows.Count; a++)
                    {
                         netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["Total1"]);
                    }
                   string inword= GlobalFunctions.AmountConvertToWord(netsal);
                   ObjRpt.SetParameterValue("Billno", Billno);      
                   ObjRpt.SetParameterValue("InWords", inword);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DeputationCompiledPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }


            if (RptType == 6)  // All Bank Employee list report (Regul./Agree.)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), false, "", UnitKey);
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_BANKEmployee_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["netpayment"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                  //  ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "BANKEmployeeList_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

            if (RptType == 7)  // All RTGS Employee list report (Regul./Agree.)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey);
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_RTGSEmployee_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["netpayment"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                 //   ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "RTGSEmployeeList_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }


            //---------------------Below Code is For Excell ------------------------------

            if (RptType == 21)  // All Emp Paybill report (Regular)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetCompiledPaybill_UPRNSS(MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptCompiledPaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["Total1"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "RegularEmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }
            if (RptType == 22)  // Employee wise Paybill report (Regular)
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwisePaybill_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey));
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptEmpwisePaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["Total1"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }
            if (RptType == 23)  // All Emp Paybill report (Agreement)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetCompiledPaybillAgreement_UPRNSS(MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptAgrrmntEmpPaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["basicpay"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);
                   // CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "AgrrmntEmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

            if (RptType == 25)  // All Emp Paybill report (Deputation)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetCompiledPaybillDeputation_UPRNSS(MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptDeputationCompiledPaybill_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["Total1"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "DeputationCompiledPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }


            if (RptType == 26)  // All Bank Employee list report (Regul./Agree.)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), false, "", UnitKey);
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "Excelrpt_BANKEmployee_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["netpayment"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    //  ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);

                    CRViewer.ReportSource = ObjRpt;
                    CRViewer.Height = 100;
                    CRViewer.Width = 300;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "BANKEmployeeList_" + MonthKey +"/"+ YearText);
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

            if (RptType == 27)  // All RTGS Employee list report (Regul./Agree.)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey);
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "Excelrpt_RTGSEmployee_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["netpayment"]);
                    }
                    string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    //   ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", inword);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "RTGSEmployeeList_" + MonthKey + "/" + YearText);
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }



        }
        catch (Exception ex)
        {
        }
    }
    protected void Page_Unload(object sender, EventArgs e)
    {
        ObjRpt.Close();
        ObjRpt.Dispose();
    }

  
}