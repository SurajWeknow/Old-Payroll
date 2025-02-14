
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
    
        session = new Session();
        if (!Page.IsPostBack)
        {
            SetCombos();
            string MonthPrs = DateTime.Now.Month.ToString();
            string YearPrs = DateTime.Now.Year.ToString();
            cmbMonth.SelectedValue = MonthPrs;
            cmbYear.SelectedValue = Convert.ToString(DBLayer.DBLInstance.FinYearCollection.Find(obj => obj.IsActive == true && obj.Name == YearPrs).FinYearKey);
            RptType = Convert.ToInt16(Request.QueryString["RepNo"]);

            if (RptType == 1)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Regular Employee";

            }
            else if (RptType == 2)
            {
                lbltitle.Text = "Employee Payslip";
                lblTitle1.Text = "Regular EmployeeWise Payslip";
                chkAllSlip.Visible = false; ;
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
            

        }
        else
        {
            RptType = Convert.ToInt16(Request.QueryString["RepNo"]);

            if (RptType == 1)
            {
                lbltitle.Text = "Employee Paybill Report";
                lblTitle1.Text = "Regular Employee";

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

            string postingkey= cmbPosting.SelectedValue;
            if (postingkey == "")
                postingkey = "1";

            if (RptType == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&EmpKey=" + key + "&Billno="+ txtBillno.Text + "&repno=1','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
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

        }
        catch (Exception ex)
        {
        }        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void cmbPosting_SelectedIndexChanged(object sender, EventArgs e)
    {
        divemplist.Style.Add("Height", "250px");
        chkEmployeeName.Items.Clear();
        int i;
        List<String> lstempName = new List<String>();
        List<String> lstempkey = new List<String>();
        string key = "";

        DataSet dss = new DataSet();
        dss = DBLayer.DBLInstance.GetEmpUnitwise(Convert.ToInt32(cmbPosting.SelectedValue),RptType.ToString ());
        string name = "";
        try
        {

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
        catch (Exception ex)
        {

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
            string key = "";
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
                postingkey = "1";

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
        }
        catch (Exception ex)
        {
        }        
    }
}