
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
            string MonthPrs = DateTime.Now.Month.ToString();
            string YearPrs = DateTime.Now.Year.ToString();
            cmbMonth.SelectedValue = MonthPrs;

            cmbYear.SelectedValue = Convert.ToString(DBLayer.DBLInstance.FinYearCollection.Find(obj => obj.IsActive == true && obj.Name == YearPrs).FinYearKey);
            SetCombos();
            RptType = Convert.ToInt16(Request.QueryString["RepNo"]);

            if (RptType == 1)
            {
                lbltitle.Text = "Employee Report";
                //lblTitle1.Text = "Regular ";

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
           
                //List<String> lstempName = new List<String>();
                //for (int i = 0; i < chkEmployeeName.Items.Count; i++)
                //{
                //if (chkEmployeeName.Items[i].Selected == true)
                //        key += "," + chkEmployeeName.Items[i].Value;
                //}
                //key = key.Remove(0, 1);

            key = "";
            string postingkey= cmbPosting.SelectedValue;
    
            if(chkall.Checked == true )
                postingkey = "0";
            



            string deduction = cmbDeductRpt.SelectedValue;

            if (RptType == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_Deduct_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&deduction=" + deduction + "&Billno=0&repno=1','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=980,height=800');FillEmployee();", true);
            }
            

        }
        catch (Exception ex)
        {
        }        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string key = "";

            key = "";
            string postingkey = cmbPosting.SelectedValue;
            if (chkall.Checked == true)
                postingkey = "0";

            string deduction = cmbDeductRpt.SelectedValue;
            if (RptType == 1)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('../Reports/AppReportViewer_Deduct_New.aspx?UnitKey=" + postingkey + "&MonthKey=" + cmbMonth.SelectedValue + "&MonthText=" + cmbMonth.SelectedItem.Text + "&YearKey=" + cmbYear.SelectedValue + "&YearText=" + cmbYear.SelectedItem.Text + "&UnitName=" + cmbPosting.SelectedItem.Text + "&deduction=" + deduction + "&Billno=0&repno=11','_blank','status=1,toolbar=0,menubar=0,location=1,scrollbars=1,resizable=1,width=500,height=400');FillEmployee();", true);
            }


        }
        catch (Exception ex)
        {
        } 
    }
}