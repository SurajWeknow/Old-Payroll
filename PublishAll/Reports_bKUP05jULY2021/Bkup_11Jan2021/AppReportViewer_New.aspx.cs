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
            if (RptType == 2)  // Employee wise Summary PaySlip report (Regular)  
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
                    //ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }

            }

            if (RptType == 20)  // Employee wise PaySlip report (Regular)         New PaySlip report
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwisePaybill_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey));
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptEmpwisePaybillNew_UPRNSS.rpt"));
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
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["basicpay"]);  // netpayment
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
                
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), false, "", UnitKey, "BankList");
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
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "RTGSList");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_RTGSEmployee_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    //DataSet netsal = new DataSet();
                    double netsal = 0;
                    for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    {
                        netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["basicpay"]);
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

            if (RptType == 8)  // All RTGS Employee list report (Deputation ONLY)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "RTGSListDeputation");
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

            if (RptType == 9)  // Holiday Employee list report 
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GenHolidayBanklist(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "GenHolidayBanklist");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_GenHolidayBanklist.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);                   
                    //double netsal = 0;
                    //for (int a = 0; a < DSResults.Tables[0].Rows.Count; a++)
                    //{
                    //    netsal += Convert.ToDouble(DSResults.Tables[0].Rows[a]["netpayment"]);
                    //}
                    //string inword = GlobalFunctions.AmountConvertToWord(netsal);
                    //   ObjRpt.SetParameterValue("Billno", Billno);
                    ObjRpt.SetParameterValue("InWords", 123);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "GenHolidayBanklist");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

            // --------------------------- DA Paybill New (13 Dec 2019)---------------

            if (RptType == 10)
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpDarrear(UnitKey, Convert.ToInt16(MonthKey), Convert.ToInt16(YearText), "All");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptDaArrear_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);                    
                    ObjRpt.SetParameterValue("Billno", Billno);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpDAArrear_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

              // --------------------------- DA Paybill New (13 Dec 2019)---------------

            if (RptType == 11)
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpDarrear_Single(UnitKey, Convert.ToInt16(MonthKey), Convert.ToInt16(YearText), "All");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rptDaArrearSingle_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);                   
                    ObjRpt.SetParameterValue("Billno", Billno);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpDAArrear_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }
            
            //----------------------------------Reports for Quaterly and anual deduction -----------------

            if (RptType == 12)  //  GIS @Queryvar
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                String Queryvar = "";
                DSResults = clsRpt.GetEmpwiseDeductionSummary_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey), "a.GIS", "DeductionIncometax_Summary");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_GISDEDUCT_QutarlyUPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }

            }
            if (RptType == 13)  // 
            {   
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwiseDeductionSummary_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey), "a.IncomeTax", "DeductionIncometax_Summary");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_Incometax_QutarlyUPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }

            }
            if (RptType == 14)  // OtherDeduction
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwiseDeductionSummary_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey), "a.otherdeduction", "DeductionIncometax_Summary");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_OtherDeduct_QutarlyUPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }

            }
            
            if (RptType == 15)  //  Cpf Advnc Deduct    
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwiseDeductionSummary_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey), "a.cpfAdvan", "DeductionIncometax_Summary");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_CpfAdvncDeduct_QutarlyUPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }

            }
               if (RptType == 16)  //  CUG Deduct    
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwiseDeductionSummary_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey), "a.CUGDeduction", "DeductionIncometax_Summary");
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_CUGDeduct_QutarlyUPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);
                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "EmpPaybill_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }

            }
               if (RptType == 17)  //  DA Arrear   
               {
                   Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                   Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                   strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                   DSResults = clsRpt.GetEmpDarrear(UnitKey, MonthKey, Convert.ToInt16(YearText), "");
                   if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                   {
                       ObjRpt.Load(MapPath(RptPath + "rptDaArrear_UPRNSS.rpt"));
                       ObjRpt.SetDataSource(DSResults.Tables[0]);
                       ObjRpt.SetParameterValue("Billno", "");
                       CRViewer.ReportSource = ObjRpt;
                       ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "DaArrear_UPRNSS");
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
            if (RptType == 22)  // Employee wise Slip report (Regular Agreement)
            {
                Int16 frommonthkey = Convert.ToInt16(Request.QueryString["FromMonthKey"]);
                Int32 fromyearval = Convert.ToInt16(Request.QueryString["FromYearText"]);
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetEmpwisePaybill_UPRNSS(frommonthkey, fromyearval, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey, Convert.ToInt32(Empkey));
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "ExcelrptEmpwisePaybill_UPRNSS.rpt"));
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
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "ExcelEmpPaybill_UPRNSS");
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
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), false, "", UnitKey, "BankList");
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
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "RTGSList");
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
            if (RptType == 28)  // All RTGS Employee list report (Deputation ONLY)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.EmpBankList_RTGS(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "RTGSListDeputation");
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

            
if (RptType == 25)  // All Emp Paybill report (Deputation)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GenHolidayBanklist(MonthKey, Convert.ToInt32(YearText),  Empkey.ToString());
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    ObjRpt.Load(MapPath(RptPath + "rpt_GenHolidayBanklist.rpt"));
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

if (RptType == 29)  // All Holiday report 
{
    strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
    DSResults = clsRpt.GenHolidayBanklist(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "GenHolidayBanklist");
    if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
    {
        ObjRpt.Load(MapPath(RptPath + "Excelrpt_GenHolidayBanklist.rpt"));
        ObjRpt.SetDataSource(DSResults.Tables[0]);
        ObjRpt.SetParameterValue("InWords", 123);
        CRViewer.ReportSource = ObjRpt;
        ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "GenHolidayBanklist");
    }
    else
    {
        Response.Write("No Record Found");
    }
} if (RptType == 30)  // DA Arrear report 
{
    strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
    DSResults = clsRpt.GenHolidayBanklist(MonthKey, YearKey, Convert.ToInt32(YearText), true, "", UnitKey, "GenHolidayBanklist");
    if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
    {
        ObjRpt.Load(MapPath(RptPath + "ExcelrptDaArrear_UPRNSS.rpt"));
        ObjRpt.SetDataSource(DSResults.Tables[0]);
        ObjRpt.SetParameterValue("InWords", 123);
        CRViewer.ReportSource = ObjRpt;
        ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, "DAArrearExcel");
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