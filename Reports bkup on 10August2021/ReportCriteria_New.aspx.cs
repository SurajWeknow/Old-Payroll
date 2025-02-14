
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Text;

public partial class Reports_ReportCriteria_New : System.Web.UI.Page
{
    private int RptType;
    clsPageDetail cls = new clsPageDetail();
    ISession session;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            session = new Session();
            if (session.CurrentUser.UserType == 1)
                this.MasterPageFile = "~/MasterPages/AdminMaster.master";
            else if (session.CurrentUser.UserType == 2)
                this.MasterPageFile = "~/MasterPages/UnitMaster.master";
        }
        catch
        {
            Response.Redirect("~/secureLogin/Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       // url redirection test
       // String RequestedPage = Request.PhysicalPath.Replace(Request.PhysicalApplicationPath, "google.com");
        chkRegEmp.Visible = false;
        chkSuspended.Visible = false;
        session = new Session();
        if (!Page.IsPostBack)
        {
            SetCombos();
            string MonthPrs = DateTime.Now.Month.ToString();
            string YearPrs = DateTime.Now.Year.ToString();
            cmbMonth.SelectedValue = MonthPrs;
            cmbYear.SelectedValue = Convert.ToString(DBLayer.DBLInstance.FinYearCollection.Find(obj => obj.IsActive == true && obj.Name == YearPrs).FinYearKey);
            cmbfrommonth.SelectedValue =  MonthPrs;
            cmbfromyear.SelectedValue  = Convert.ToString(DBLayer.DBLInstance.FinYearCollection.Find(obj => obj.IsActive == true && obj.Name == YearPrs).FinYearKey);
            RptType = Convert.ToInt16(Request.QueryString["RepNo"]);
            if (RptType == 1)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Regular Employee";
                chkSuspended.Visible = true;
            }
            else if (RptType == 2)
            {
                lbltitle.Text = "Employee Payslip";
                lblTitle1.Text = "Regular EmployeeWise Payslip";
                chkAllSlip.Visible = false;
                selectall.Visible = false;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
            }
            else if (RptType == 3)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Agreement Employee Paybill";             
            }
            else if (RptType == 5)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Deputation Employee Paybill";            
            }
            else if (RptType == 6)
            {
                lbltitle.Text = "Employee Bank List Report";
                lblTitle1.Text = "Salary through Banking";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = true;
                chkAllSlip.Enabled = false;
                //divunit.Visible = false;
            }
            else if (RptType == 7)
            {
                lbltitle.Text = "RTGS Employee Report";
                lblTitle1.Text = "Salary through RTGS";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = true;
                chkAllSlip.Enabled = false;
               // divunit.Visible = false;
            }
            else if (RptType == 8)
            {
                lbltitle.Text = "RTGS Employee Deputation Report";
                lblTitle1.Text = "Salary through RTGS";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = true;
                chkAllSlip.Enabled = false;
                divunit.Visible = false;
            }
            else if (RptType == 9)
            {
                lbltitle.Text = "Employee Holiday Arrear Report";
                lblTitle1.Text = "";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = true;
                chkAllSlip.Enabled = false;
                divunit.Visible = true;
                Div1.Visible = false;
                btnBankListExcel.Visible = true;
            }
            else if (RptType == 12)
            {
                lbltitle.Text = "Employee GIS Deduction";
                lblTitle1.Text = "GIS Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 13)
            {
                lbltitle.Text = "Employee IncomeTax Deduction";
                lblTitle1.Text = "IncomeTax Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 14)
            {
                lbltitle.Text = "Employee Other Deduction";
                lblTitle1.Text = " Other Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 15)
            {
                lbltitle.Text = "Employee CUG Deduction";
                lblTitle1.Text = "CUG Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 17)  //  DA Arrear Report
            {
                lbltitle.Text = "Employee DA Arrear Report";
                lblTitle1.Text = "DA Arrear";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = false;
                chkAllSlip.Enabled = false;
                Div1.Visible = true;
                selectall.Visible = false;
                chkAllSlip.Checked = true;
                btnBankList.Visible = true;
                btnBankListExcel.Visible = true;
            }
            else if (RptType == 18)  //  Bonus Report finyear wise
            {
                lbltitle.Text = "Employee Bonus Report";
                lblTitle1.Text = "Bonus";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = false;
                chkAllSlip.Enabled = false;
                Div1.Visible = true;
                selectall.Visible = false;
                chkAllSlip.Checked = true;
                btnBankList.Visible = true;
                btnBankListExcel.Visible = true;
                chkisAgreement.Visible = true;
            }
            else if (RptType == 19)
            {
                lbltitle.Text = "Employee Increment Arrear Report";
                lblTitle1.Text = "Employee Increment Arrear";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = true;
                chkAllSlip.Enabled = false;
                btnExcel.Visible = false;
                Div1.Visible = false;
                btnSearch.Text = "Banklist Excel";
                //divunit.Visible = false;
            }
        }
        else
        {
            RptType = Convert.ToInt16(Request.QueryString["RepNo"]);

            if (RptType == 1)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Regular Employee";
                chkSuspended.Visible = true;
            }
            else if (RptType == 2)
            {
                lbltitle.Text = "Employee Payslip";
                lblTitle1.Text = "Regular EmployeeWise Payslip";
            }

            else if (RptType == 3)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Agreement Employee Paybill";
            }
            else if (RptType == 5)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Deputation Employee Paybill";
            }

            else if (RptType == 10)
            {
                lbltitle.Text = "Employee DA Arrear Report";
                lblTitle1.Text = "DA Arrear Report";

            }
            else if (RptType == 12)
            {
                lbltitle.Text = "Employee GIS Deduction";
                lblTitle1.Text = "GIS Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 13)
            {
                lbltitle.Text = "Employee IncomeTax Deduction";
                lblTitle1.Text = "IncomeTax Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 14)
            {
                lbltitle.Text = "Employee Other Deduction";
                lblTitle1.Text = " Other Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 15)
            {
                lbltitle.Text = "Employee CUG Deduction";
                lblTitle1.Text = "CUG Deduction";
                chkAllSlip.Visible = true; ;
                selectall.Visible = true;
                fromto.Visible = true;
                cmbfrommonth.Visible = true;
                cmbfromyear.Visible = true;
                chkRegEmp.Visible = true;

            }
            else if (RptType == 17)  //  DA Arrear Report
            {
                lbltitle.Text = "Employee DA Arrear Report";
                lblTitle1.Text = "DA Arrear";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = false;
                chkAllSlip.Enabled = false;
                Div1.Visible = true;
                selectall.Visible = false;
                btnBankList.Visible = true;
                btnBankListExcel.Visible = true;
            }
            else if (RptType == 18)  //  Bonus Report finyear wise
            {
                lbltitle.Text = "Employee Bonus Report";
                lblTitle1.Text = "Bonus";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = false;
                chkAllSlip.Enabled = false;
                Div1.Visible = true;
                selectall.Visible = false;
                chkAllSlip.Checked = true;
                btnBankList.Visible = true;
                btnBankListExcel.Visible = true;
                chkisAgreement.Visible = true;
            }
            else if (RptType == 19)
            {
                lbltitle.Text = "Employee Increment Arrear Banklist Report";
                lblTitle1.Text = "Employee Increment Arrear Banklist";
                EmpName.Visible = false;
                billno.Visible = false;
                chkAllSlip.Checked = true;
                chkAllSlip.Enabled = false;
                btnExcel.Visible = false;
                Div1.Visible = false;
                btnSearch.Text = "Banklist Excel";
                //divunit.Visible = false;
            }
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "FillEmployee()", true);
        }
        //if (session.CurrentUser.UserType == 2)
        //    divchkAll.Visible = false;
    }

    private void SetCombos()
    {
        // Fill Combo
        cls.FillPosting(ref cmbPosting, Convert.ToInt32(Session["usertype"]));
       // cls.FillPayBand(ref cmbPayBand);
        cls.FillMonth(ref cmbMonth);
        cls.FillYear(ref cmbYear);
        cls.FillMonth(ref cmbfrommonth);
        cls.FillYear(ref cmbfromyear);
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string key = "";
            if (txtCode.Text == "")
            {
                if (chkAllSlip.Checked == false && RptType!=17)
                {
                    List<String> lstempName = new List<String>();
                    for (int i = 0; i < chkEmployeeName.Items.Count; i++)
                    {
                        if (chkEmployeeName.Items[i].Selected == true)
                            key += "," + chkEmployeeName.Items[i].Value;
                    }
                    key = key.Remove(0, 1);
                }
                else
                    key = "0";
            }
            else
                key = hidEditRecordKey.Value;
            string postingkey= cmbPosting.SelectedValue;
            if (postingkey == "")
                postingkey = "0";
            String Suspended = "";
            if (chkSuspended.Checked == true)
            {
                Suspended = "Suspended";
            }
               if (RptType == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&issuspended=" + Suspended + "&repno=1','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            else if (RptType == 2) // ----------- For Single Regular employee 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&FromMonthKey=" + cmbfrommonth.SelectedValue + "&FromYearText=" + cmbfromyear.SelectedItem.Text + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=2','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            else if (RptType == 3)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=3','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            else if (RptType == 4)// ----------- For Single Agreement employee 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=4','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 5)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=5','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }

            if (RptType == 6)   //---------------- For Bank List
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=6','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }

            if (RptType == 7)  //---------------- For RTGS List
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=7','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 8)  //---------------- For RTGS List Deputation 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=8','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 9)  //---------------- For Holiday BankList 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&repno=9','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            //if (RptType == 10)  //---------------- For DA Arrear
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=10','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            //}
            String emptype = "";
            if (chkRegEmp.Checked == true)
                emptype = "Regular";
            else
                emptype = "Deputation";

            if (RptType == 12)  //---------------- For GIS
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", 
                    "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + 
                    "&FromMonthKey=" + cmbfrommonth.SelectedValue + 
                    "&FromYearText=" + cmbfromyear.SelectedItem.Text + 
                    "&MonthKey=" + cmbMonth.SelectedValue + 
                    "&MonthText=" + cmbMonth.SelectedItem.Text + 
                    "&YearKey=" + cmbYear.SelectedValue + 
                    "&YearText=" + cmbYear.SelectedItem.Text + 
                    "&UnitName=" + cmbPosting.SelectedItem.Text + 
                    "&EmpKey=" + key + "&emptype=" +emptype+
                    "&Billno=" + txtBillno.Text + 
                    "&repno=12','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 13)  //---------------- For Incometax
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow",
                  "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey +
                  "&FromMonthKey=" + cmbfrommonth.SelectedValue +
                  "&FromYearText=" + cmbfromyear.SelectedItem.Text +
                  "&MonthKey=" + cmbMonth.SelectedValue +
                  "&MonthText=" + cmbMonth.SelectedItem.Text +
                  "&YearKey=" + cmbYear.SelectedValue +
                  "&YearText=" + cmbYear.SelectedItem.Text +
                  "&UnitName=" + cmbPosting.SelectedItem.Text +
                  "&EmpKey=" + key + "&emptype=" + emptype +
                  "&Billno=" + txtBillno.Text +
                  "&repno=13','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 14)  //---------------- For Other Deduction
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow",
                  "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey +
                  "&FromMonthKey=" + cmbfrommonth.SelectedValue +
                  "&FromYearText=" + cmbfromyear.SelectedItem.Text +
                  "&MonthKey=" + cmbMonth.SelectedValue +
                  "&MonthText=" + cmbMonth.SelectedItem.Text +
                  "&YearKey=" + cmbYear.SelectedValue +
                  "&YearText=" + cmbYear.SelectedItem.Text +
                  "&UnitName=" + cmbPosting.SelectedItem.Text +
                  "&EmpKey=" + key + "&emptype=" + emptype +
                  "&Billno=" + txtBillno.Text +
                  "&repno=14','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }

            if (RptType == 15)  //---------------- For Other Deduction
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow",
                  "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey +
                  "&FromMonthKey=" + cmbfrommonth.SelectedValue +
                  "&FromYearText=" + cmbfromyear.SelectedItem.Text +
                  "&MonthKey=" + cmbMonth.SelectedValue +
                  "&MonthText=" + cmbMonth.SelectedItem.Text +
                  "&YearKey=" + cmbYear.SelectedValue +
                  "&YearText=" + cmbYear.SelectedItem.Text +
                  "&UnitName=" + cmbPosting.SelectedItem.Text +
                  "&EmpKey=" + key + "&emptype=" + emptype +
                  "&Billno=" + txtBillno.Text +
                  "&repno=15','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }

            if (RptType == 17)   //---------------- For DA Arrear
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&Type=Single&repno=17','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
           

            if (chkisAgreement.Checked == true)
            {
                if (RptType == 18)   //---------------- For Bonus Report
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Agreement&repno=18','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
                }
            }
            else {
                if (RptType == 18)   //---------------- For Bonus Report
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Regular&repno=18','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
                }
           
            }
              if (RptType == 19)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=199','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
        }
        catch (Exception ex)
        {
        }        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void cmbPosting_SelectedIndexChanged(object sender, EventArgs e)
    { try
        {
          RptType = Convert.ToInt16(Request.QueryString["RepNo"]);

          if (RptType == 2)         
                 WorkingEmp_All("Deputation"); 
          else
              WorkingEmp("Deputation");
    

        }
        catch (Exception ex)
        {
            //201145430  fA 206 01A   192
            //201145362  CONVERA BELT   STS PRINTING + PUSH Button  

        }

    }

    void WorkingEmp(String isEmp)
    {
        try{
        divemplist.Style.Add("Height", "250px");
        chkEmployeeName.Items.Clear();
        int i;
        List<String> lstempName = new List<String>();
        List<String> lstempkey = new List<String>();
        string key = "";

        DataSet dss = new DataSet();
        dss = DBLayer.DBLInstance.GetEmpUnitwise(Convert.ToInt32(cmbPosting.SelectedValue), RptType.ToString(), Convert.ToInt16(cmbMonth.SelectedValue), Convert.ToInt16(cmbYear.SelectedItem.Text), isEmp);
        string name = "";
       
            for (i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                lstempName.Add(dss.Tables[0].Rows[i]["EmployeeName"].ToString());
                name += ", " + dss.Tables[0].Rows[i]["Employeekey"].ToString();
                ListItem item = new ListItem();
                item.Text = dss.Tables[0].Rows[i]["EmployeeCode"].ToString() + " - <br/>" + dss.Tables[0].Rows[i]["EmployeeName"].ToString();
                item.Value = dss.Tables[0].Rows[i]["Employeekey"].ToString();
                chkEmployeeName.Items.Add(item);
            }
            key = name.Remove(0, 1);
        }
        catch (Exception ex)
        {
            divemplist.Style.Add("Height", "250px");
            chkEmployeeName.Items.Clear();
            int i;
            List<String> lstempName = new List<String>();
            List<String> lstempkey = new List<String>();
            string key = "";

            DataSet dss = new DataSet();
            dss = DBLayer.DBLInstance.GetEmpUnitwise(Convert.ToInt32(cmbPosting.SelectedValue), RptType.ToString(), Convert.ToInt16(cmbMonth.SelectedValue), Convert.ToInt16(cmbYear.SelectedItem.Text), isEmp);
            string name = "";
            for (i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                lstempName.Add(dss.Tables[0].Rows[i]["Name"].ToString());
                name += ", " + dss.Tables[0].Rows[i]["Employeekey"].ToString();
                ListItem item = new ListItem();
                item.Text = dss.Tables[0].Rows[i]["Code"].ToString() + " - <br/>" + dss.Tables[0].Rows[i]["Name"].ToString();
                item.Value = dss.Tables[0].Rows[i]["Employeekey"].ToString();
                chkEmployeeName.Items.Add(item);
            }
            key = name.Remove(0, 1);
        }
    
    }
      void WorkingEmp_All(String isEmp)
      {
        try{
        divemplist.Style.Add("Height", "250px");
        chkEmployeeName.Items.Clear();
        int i;
        List<String> lstempName = new List<String>();
        List<String> lstempkey = new List<String>();
        string key = "";

        DataSet dss = new DataSet();
      //  dss = DBLayer.DBLInstance.GetEmpUnitwiseAll(Convert.ToInt32(cmbPosting.SelectedValue), RptType.ToString());
        dss = DBLayer.DBLInstance.GetEmpUnitwise(Convert.ToInt32(cmbPosting.SelectedValue), RptType.ToString(), Convert.ToInt16(cmbMonth.SelectedValue), Convert.ToInt16(cmbYear.SelectedItem.Text), isEmp);
        string name = "";       

            for (i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                lstempName.Add(dss.Tables[0].Rows[i]["EmployeeName"].ToString());
                name += ", " + dss.Tables[0].Rows[i]["Employeekey"].ToString();
                ListItem item = new ListItem();
                item.Text = dss.Tables[0].Rows[i]["EmployeeCode"].ToString() + " - <br/>" + dss.Tables[0].Rows[i]["EmployeeName"].ToString();
                item.Value = dss.Tables[0].Rows[i]["Employeekey"].ToString();
                chkEmployeeName.Items.Add(item);
            }
            key = name.Remove(0, 1);
        }
        catch (Exception ex)
        {
            divemplist.Style.Add("Height", "250px");
            chkEmployeeName.Items.Clear();
            int i;
            List<String> lstempName = new List<String>();
            List<String> lstempkey = new List<String>();
            string key = "";

            DataSet dss = new DataSet();
            dss = DBLayer.DBLInstance.GetEmpUnitwise(Convert.ToInt32(cmbPosting.SelectedValue), RptType.ToString(), Convert.ToInt16(cmbMonth.SelectedValue), Convert.ToInt16(cmbYear.SelectedItem.Text), isEmp);
            string name = "";

            for (i = 0; i < dss.Tables[0].Rows.Count; i++)
            {
                lstempName.Add(dss.Tables[0].Rows[i]["Name"].ToString());
                name += ", " + dss.Tables[0].Rows[i]["Employeekey"].ToString();
                ListItem item = new ListItem();
                item.Text = dss.Tables[0].Rows[i]["Code"].ToString() + " - <br/>" + dss.Tables[0].Rows[i]["Name"].ToString();
                item.Value = dss.Tables[0].Rows[i]["Employeekey"].ToString();
                chkEmployeeName.Items.Add(item);
            }
            key = name.Remove(0, 1);
        }
    
    }
    protected void chkAllSlip_CheckedChanged(object sender, EventArgs e)
    {
        if(chkEmployeeName.Enabled== true )
        chkEmployeeName.Enabled = false;
        else if (chkEmployeeName.Enabled == false )
            chkEmployeeName.Enabled = true ;

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string key = "0";
            if (chkAllSlip.Checked == false)
            {
                List<String> lstempName = new List<String>();
                for (int i = 0; i < chkEmployeeName.Items.Count; i++)
                {
                    if (chkEmployeeName.Items[i].Selected == true)
                        key += "," + chkEmployeeName.Items[i].Value;
                }
                key = key.Remove(0, 1);
            }
            else

                key = "0";

            string postingkey = cmbPosting.SelectedValue;
            if (postingkey == "")
                postingkey = "0";

            if (RptType == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=21','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            else if (RptType == 2) // ----------- For Single Regular employee 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&FromMonthKey=" + cmbfrommonth.SelectedValue + "&FromYearText=" + cmbfromyear.SelectedItem.Text + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=22','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            else if (RptType == 3)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=23','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            else if (RptType == 4)// ----------- For Single Agreement employee 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=24','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            if (RptType == 5)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=25','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            if (RptType == 6)   //---------------- For Bank List
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=26','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            if (RptType == 7)  //---------------- For RTGS List
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=27','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }
            if (RptType == 8)  //---------------- For RTGS List Deputation 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&repno=28','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 9)  //---------------- For Holiday BankList 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&repno=29','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 10)  //---------------- For Holiday BankList 
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&repno=10','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (RptType == 17)   //---------------- For DA Arrear
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&Type=SingleExcelImport&repno=17','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
             if (chkisAgreement.Checked == true)
            {
                if (RptType == 18)   //---------------- For Bonus Report
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Agreement&repno=31','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
                }
            }
            else
            {
                if (RptType == 18)   //---------------- For Bonus Report
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Regular&repno=31','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
                }

            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
          DataSet dss = new DataSet();
          dss = DBLayer.DBLInstance.GetcodeData(txtCode.Text);
          string name=  dss.Tables[0].Rows[0]["Name"].ToString();
          string code=  dss.Tables[0].Rows[0]["code"].ToString();
          int id=  Convert.ToInt32(dss.Tables[0].Rows[0]["postingkey"]);
          string Unit=  dss.Tables[0].Rows[0]["unit"].ToString();
          cmbPosting.SelectedValue = id.ToString();
          hidEditRecordKey.Value = dss.Tables[0].Rows[0]["employeekey"].ToString () ;
        }
        catch(Exception exx){
        
        }
    }
    protected void btnBankList_Click(object sender, EventArgs e)
    {
        try
        {
            string key = "";
            if (txtCode.Text == "")
            {
                if (chkAllSlip.Checked == false && RptType != 17)
                {
                    List<String> lstempName = new List<String>();
                    for (int i = 0; i < chkEmployeeName.Items.Count; i++)
                    {
                        if (chkEmployeeName.Items[i].Selected == true)
                            key += "," + chkEmployeeName.Items[i].Value;
                    }
                    key = key.Remove(0, 1);
                }
                else
                    key = "0";
            }
            else
                key = hidEditRecordKey.Value;
            string postingkey = cmbPosting.SelectedValue;
            if (postingkey == "")
                postingkey = "0";

            if (RptType == 17)   //---------------- For DA Arrear Banklist
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&Type=All&repno=17','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            if (chkisAgreement.Checked == true)
            {
                if (RptType == 18)   //----------------For Bonus Report Banklist
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Agreement&repno=19','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
                }
            }
            else
            {
                if (RptType == 18)   //---------------- For Bonus Report Banklist
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Regular&repno=19','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
                }
            }
           

        }
        catch (Exception ex)
        {
        }        
    }
    protected void btnBankListExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string key = "0";
            if (chkAllSlip.Checked == false)
            {
                List<String> lstempName = new List<String>();
                for (int i = 0; i < chkEmployeeName.Items.Count; i++)
                {
                    if (chkEmployeeName.Items[i].Selected == true)
                        key += "," + chkEmployeeName.Items[i].Value;
                }
                key = key.Remove(0, 1);
            }
            else

                key = "0";

            string postingkey = cmbPosting.SelectedValue;
            if (postingkey == "")
                postingkey = "0";

            if (RptType == 17)   //---------------- For DA Arrear Banklist
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&Type=Banklist&repno=17','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }

            
         if (chkisAgreement.Checked == true)
         {
             if (RptType == 18)   //----------------For Bonus Report Banklist Excel
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Agreement&repno=32','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
             }
         }
         else
         {
             if (RptType == 18)   //---------------- For Bonus Report Banklist Excel
             {
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno=" + txtBillno.Text + "&isAgreemnt=Regular&repno=32','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
             }

         }
                  
         if (RptType == 9)  //---------------- For Holiday BankList  Excel
         {
             ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&repno=290','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
         }

        }
        catch (Exception ex)
        {
        }
    }
    protected void chkRegEmp_CheckedChanged(object sender, EventArgs e)
    {
        RptType = Convert.ToInt16(Request.QueryString["RepNo"]);


        if (chkRegEmp.Checked == true)
        {
            if (RptType == 2)
                WorkingEmp_All("Regular");
            else
                WorkingEmp("Regular");
        }
        else
        {
            if (RptType == 2)
                WorkingEmp_All("Deputation");
            else
                WorkingEmp("Deputation");
        }
    }
}