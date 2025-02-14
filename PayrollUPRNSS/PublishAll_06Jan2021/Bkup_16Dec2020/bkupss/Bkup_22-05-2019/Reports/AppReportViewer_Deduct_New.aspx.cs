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
            string deduction = Convert.ToString(Request.QueryString["deduction"]);
            DataSet ds = new DataSet();
            string RptPath = "~/Reports/";
            DataSet DSResults = new DataSet();

            if (RptType == 1)  // All Emp Paybill report (Regular)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetDeductions_UPRNSS(deduction,MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey);
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    if (deduction == "Incometax")
                    ObjRpt.Load(MapPath(RptPath + "rpt_Incometax_UPRNSS.rpt"));
                    else if(deduction=="CUG")
                        ObjRpt.Load(MapPath(RptPath + "rpt_CUGDeduct_UPRNSS.rpt"));
                    else if (deduction=="GIS")
                          ObjRpt.Load(MapPath(RptPath + "rpt_GISDeduct_UPRNSS.rpt"));
                        ObjRpt.SetDataSource(DSResults.Tables[0]);                                 

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "IncomeTax_UPRNSS");
                }
                else
                {
                    Response.Write("No Record Found");
                }
            }

            //---------------------- BELOW CODE fOR EXCEL REPORTS ------------------

            if (RptType == 11)  // All Emp Paybill report (Regular)
            {
                strMonthYear = GlobalFunctions.GetMonth(MonthKey) + " " + YearKey;
                DSResults = clsRpt.GetDeductions_UPRNSS(deduction, MonthKey, YearKey, Convert.ToInt32(YearText), UnitKey);
                if (DSResults != null && DSResults.Tables[0].Rows.Count > 0)
                {
                    if (deduction == "Incometax")
                        ObjRpt.Load(MapPath(RptPath + "Excelrpt_Incometax_UPRNSS.rpt"));
                    else if (deduction == "CUG")
                        ObjRpt.Load(MapPath(RptPath + "Excelrpt_CUGDeduct_UPRNSS.rpt"));
                    else if (deduction == "GIS")
                        ObjRpt.Load(MapPath(RptPath + "Excelrpt_GISDeduct_UPRNSS.rpt"));
                    ObjRpt.SetDataSource(DSResults.Tables[0]);

                    CRViewer.ReportSource = ObjRpt;
                    ObjRpt.ExportToHttpResponse(ExportFormatType.Excel, Response, false, deduction + "_" + MonthKey +"_"+ YearText);
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