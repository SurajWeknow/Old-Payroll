using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class DynamicPages_ReportHtml_IncrementArrear : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            pnlContents.Visible = false;
           
        }
    }

    public void DueDrawnArrear()
    {
        if (txtincometax.Text == "")
            txtincometax.Text = "0";

        if (txtotherdeduct.Text == "")
            txtotherdeduct.Text = "0";

            //--------------------------Due Amount   (IR is used as Specialpay)-----------------------------------javascript:__doPostBack('lnkbtn','')
        DataSet dssDue = new DataSet();
        dssDue = DBLayer.DBLInstance.Get_DueIncrementArrear( Convert.ToInt16(ddlmonth.Text), Convert.ToInt16(ddlyear.Text), txtCode.Text);
        rptDueArrear.DataSource = dssDue.Tables[0];        
        rptDueArrear.DataBind();
       
        //  DAPercent,a.MonthId,a.YearId,a.BasicPayDue,a.BasicPayDrawn,b.gradepay,b.da,b.ada,
        //  b.cca,b.hra,b.MedicalAllow,'' as IR,b.Total1, b.pf,b.Cpf_double,b.OtherDeduction,b.incomeTax,b.deductionTotal
        int BasicDue = 0; int BasicDrawn = 0; int dasum = 0; int daDrawnsum = 0; int ccasum = 0; int ccaDrawnsum = 0; int hrasum = 0; int hraDrawnsum = 0;
        int masum = 0; int maDrawnsum = 0; int pfsum = 0; int pfDrawnsum = 0; int Total = 0; int TotalDrawnsum = 0;
        for (int a = 0; a < dssDue.Tables[0].Rows.Count; a++)
        {
            BasicDue+=Convert.ToInt32(dssDue.Tables[0].Rows[a]["BasicPayDue"]);
            dasum += (Convert.ToInt32(dssDue.Tables[0].Rows[a]["BasicPayDue"]) * Convert.ToInt32(dssDue.Tables[0].Rows[a]["DAPercent"])) / 100;
            ccasum += Convert.ToInt32(dssDue.Tables[0].Rows[a]["CCA_Org"]);
            hrasum += Convert.ToInt32(dssDue.Tables[0].Rows[a]["HRA_Org"]);
            masum += Convert.ToInt32(dssDue.Tables[0].Rows[a]["Medicl_Org"]);
            pfsum +=(((Convert.ToInt32(dssDue.Tables[0].Rows[a]["BasicPayDue"]) + Convert.ToInt32(dssDue.Tables[0].Rows[a]["gradepay"])+   (Convert.ToInt32(dssDue.Tables[0].Rows[a]["BasicPayDue"])*Convert.ToInt32(dssDue.Tables[0].Rows[a]["DAPercent"]))/100)*12)/100 );
            Total += Convert.ToInt32(dssDue.Tables[0].Rows[a]["BasicPayDue"]) + Convert.ToInt32(dssDue.Tables[0].Rows[a]["gradepay"]) + (Convert.ToInt32(dssDue.Tables[0].Rows[a]["BasicPayDue"]) * Convert.ToInt32(dssDue.Tables[0].Rows[a]["DAPercent"]) / 100) + Convert.ToInt32(dssDue.Tables[0].Rows[a]["ada"]) + Convert.ToInt32(dssDue.Tables[0].Rows[a]["cca_Org"]) + Convert.ToInt32(dssDue.Tables[0].Rows[a]["hra_Org"]) + Convert.ToInt32(dssDue.Tables[0].Rows[a]["Medicl_Org"]);
        }
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl20") as Label).Text = BasicDue.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl21") as Label).Text = BasicDue.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl22") as Label).Text = "0";
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl23") as Label).Text = dasum.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl24") as Label).Text = "0";
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl25") as Label).Text = ccasum.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl26") as Label).Text = hrasum.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl27") as Label).Text = masum.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl28") as Label).Text = "0";
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl29") as Label).Text = Total.ToString ();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl30") as Label).Text = pfsum.ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl31") as Label).Text = (Total+pfsum).ToString ();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl32") as Label).Text = (pfsum*2).ToString();
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl33") as Label).Text = "0";
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl34") as Label).Text = "0";
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl35") as Label).Text = (pfsum * 2).ToString(); ;
        (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl36") as Label).Text = (Total + pfsum -((pfsum*2))).ToString();

        DataSet dssDrawn = new DataSet();
        dssDrawn = DBLayer.DBLInstance.Get_DueIncrementArrear(Convert.ToInt16(ddlmonth.Text), Convert.ToInt16(ddlyear.Text), txtCode.Text);
        rptDrawnArrear.DataSource = dssDrawn.Tables[0];
        rptDrawnArrear.DataBind();
        for (int a = 0; a < dssDrawn.Tables[0].Rows.Count; a++)
        {
           
            BasicDrawn += Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["BasicPay_Org"]);
            daDrawnsum += (Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["BasicPay_Org"]) * Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["DAPercent"])) / 100;
            ccaDrawnsum += Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["CCA_Org"]);
            hraDrawnsum += Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["HRA_Org"]);
            maDrawnsum += Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["Medicl_Org"]);
            pfDrawnsum += (((Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["BasicPay_Org"]) + Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["gradepay"]) + (Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["BasicPay_Org"]) * Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["DAPercent"])) / 100) * 12) / 100);
            TotalDrawnsum += Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["BasicPay_Org"]) + Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["gradepay"]) + (Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["BasicPay_Org"]) * Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["DAPercent"]) / 100) + Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["ada"]) + Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["cca_Org"]) + Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["hra_Org"]) + Convert.ToInt32(dssDrawn.Tables[0].Rows[a]["Medicl_Org"]);


        }

        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn20") as Label).Text = BasicDrawn.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn21") as Label).Text = BasicDrawn.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn22") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn23") as Label).Text = daDrawnsum.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn24") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn25") as Label).Text = ccaDrawnsum.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn26") as Label).Text = hraDrawnsum.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn27") as Label).Text = maDrawnsum.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn28") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn29") as Label).Text = TotalDrawnsum.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn30") as Label).Text = pfDrawnsum.ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn31") as Label).Text = (TotalDrawnsum + pfDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn32") as Label).Text = (pfDrawnsum * 2).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn33") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn34") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn35") as Label).Text = (pfDrawnsum * 2).ToString(); ;
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn36") as Label).Text = (TotalDrawnsum + pfDrawnsum - ((pfDrawnsum * 2))).ToString();

        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl20_GT") as Label).Text = (BasicDue-BasicDrawn).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl21_GT") as Label).Text = (BasicDue-BasicDrawn).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl22_GT") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl23_GT") as Label).Text = (dasum-daDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl24_GT") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl25_GT") as Label).Text = (ccasum-ccaDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl26_GT") as Label).Text = (hrasum-hraDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl27_GT") as Label).Text = (masum-maDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl28_GT") as Label).Text = "0";
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl29_GT") as Label).Text = (Total-TotalDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl30_GT") as Label).Text = (pfsum-pfDrawnsum).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl31_GT") as Label).Text = ((Total + pfsum)-(TotalDrawnsum + pfDrawnsum)).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl32_GT") as Label).Text = ((pfsum * 2)-(pfDrawnsum * 2)).ToString();
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl33_GT") as Label).Text = txtotherdeduct.Text;
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl34_GT") as Label).Text = txtincometax.Text;
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl35_GT") as Label).Text = ((pfsum * 2) - (pfDrawnsum * 2) - (Convert.ToInt32(txtotherdeduct.Text) + Convert.ToInt32(txtincometax.Text))).ToString(); ;
        (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl36_GT") as Label).Text = ((Total + pfsum - ((pfsum * 2)))-(TotalDrawnsum + pfDrawnsum - ((pfDrawnsum * 2))) -(Convert.ToInt32(txtotherdeduct.Text) + Convert.ToInt32(txtincometax.Text))).ToString();
        

        lblGAmount.Text = ((Total + pfsum) - (TotalDrawnsum + pfDrawnsum)).ToString();


     //   int BasicPay = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["BasicPayDue"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl20") as Label).Text = BasicPay.ToString();
     //   int Basic = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["BasicPayDue"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl21") as Label).Text = BasicPay.ToString();
     //   int gradepay = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["gradepay"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl22") as Label).Text = gradepay.ToString();
     //   int DA = dasum;

     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl23") as Label).Text = DA.ToString();
     //   int ADA = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["ada"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl24") as Label).Text = ADA.ToString();
     //   int CCA = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["cca"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl25") as Label).Text = CCA.ToString();
     //   int HRA = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["hra"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl26") as Label).Text = HRA.ToString();
     //   int MA = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["MedicalAllow"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl27") as Label).Text = MA.ToString();       
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl28") as Label).Text = "0";
     //   int CPF = dssDue.Tables[0].Select().Sum(p => Convert.ToInt32(p["pf"]));
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl30") as Label).Text = CPF.ToString();
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl32") as Label).Text = (CPF*2).ToString();
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl33") as Label).Text = "0";// txtotherdeduct.Text;
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl34") as Label).Text = "0";// txtincometax.Text;


     //   int Total = Basic + gradepay + DA + ADA + CCA + HRA + MA;
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl29") as Label).Text = Total.ToString();

     //   int gtotal = Total + CPF;
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl31") as Label).Text = gtotal.ToString();

     //   int deductionTotal = (CPF * 2);//+ Convert.ToInt32(txtincometax.Text) + Convert.ToInt32(txtotherdeduct.Text);
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl35") as Label).Text = deductionTotal.ToString();

     //   int grandtotal = gtotal - deductionTotal;
     //   (rptDueArrear.Controls[rptDueArrear.Controls.Count - 1].Controls[0].FindControl("lbl36") as Label).Text = grandtotal.ToString();


     //   int Tdeduct = (CPF * 2) + Convert.ToInt32(txtincometax.Text) + Convert.ToInt32(txtotherdeduct.Text);
        
     //   //((Label)rptDueArrear.Controls[5].Controls[0].FindControl("Label15")).Text = Tdeduct.ToString();
     ////**************************************Drawn********************************************
        //DataSet dssDrawn = new DataSet();
        //dssDrawn = DBLayer.DBLInstance.Get_DueIncrementArrear(Convert.ToInt16(ddlmonth.Text), Convert.ToInt16(ddlyear.Text), txtCode.Text);
        //rptDrawnArrear.DataSource = dssDrawn.Tables[0];
        //rptDrawnArrear.DataBind();

     //   int BasicPayDrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["BasicPayDrawn"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn20") as Label).Text = BasicPayDrawn.ToString();
     //   int BasicDrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["BasicPayDrawn"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn21") as Label).Text = BasicPayDrawn.ToString();
     //   int gradepayDrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["gradepay"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn22") as Label).Text = gradepay.ToString();
              
     //   int DADrawn = daDrawnsum;
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn23") as Label).Text = DADrawn.ToString();
     //   int ADADrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["ada"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn24") as Label).Text = ADADrawn.ToString();
     //   int CCADrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["cca"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn25") as Label).Text = CCADrawn.ToString();
     //   int HRADrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["hra"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn26") as Label).Text = HRADrawn.ToString();
     //   int MADrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["MedicalAllow"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn27") as Label).Text = MADrawn.ToString();
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn28") as Label).Text = "0";
     //   int CPFDrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["pf"]));
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn30") as Label).Text = CPFDrawn.ToString();
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn32") as Label).Text = (CPFDrawn * 2).ToString();
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn33") as Label).Text = "0";
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn34") as Label).Text = "0";

     //   int Total1Drawn = BasicDrawn + gradepayDrawn + DADrawn + ADADrawn + CCADrawn + HRADrawn + MADrawn;
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn29") as Label).Text = Total1Drawn.ToString();

     //   int gtotalDrawn = CPF + Total1Drawn;
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn31") as Label).Text = gtotalDrawn.ToString();

     //   // int deductionTotalDrawn = dssDrawn.Tables[0].Select().Sum(p => Convert.ToInt32(p["deductionTotal"]));
     //   int deductionTotalDrawn = (CPFDrawn * 2);//+ Convert.ToInt32(txtincometax.Text) + Convert.ToInt32(txtotherdeduct.Text);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn35") as Label).Text = deductionTotalDrawn.ToString();

     //   int grandtotalDrawn = gtotalDrawn - deductionTotalDrawn;
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lblDrawn36") as Label).Text = grandtotalDrawn.ToString();

     //   //-------------------------------Grand Total------------------------------
     //   int lbl20_GT = Convert.ToInt32(BasicPay) - Convert.ToInt32(BasicPayDrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl20_GT") as Label).Text = lbl20_GT.ToString();
     //   int lbl21_GT = Convert.ToInt32(Basic) - Convert.ToInt32(BasicDrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl21_GT") as Label).Text = lbl21_GT.ToString();
     //   int lbl22_GT = Convert.ToInt32(gradepay) - Convert.ToInt32(gradepayDrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl22_GT") as Label).Text = lbl22_GT.ToString();
     //   int lbl23_GT = Convert.ToInt32(DA) - Convert.ToInt32(DADrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl23_GT") as Label).Text = lbl23_GT.ToString();
     //   int lbl24_GT = Convert.ToInt32(ADA) - Convert.ToInt32(ADADrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl24_GT") as Label).Text = lbl24_GT.ToString();
     //   int lbl25_GT = Convert.ToInt32(CCA) - Convert.ToInt32(CCADrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl25_GT") as Label).Text = lbl25_GT.ToString();
     //   int lbl26_GT = Convert.ToInt32(HRA) - Convert.ToInt32(HRADrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl26_GT") as Label).Text = lbl26_GT.ToString();
     //   int lbl27_GT = Convert.ToInt32(MA) - Convert.ToInt32(MADrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl27_GT") as Label).Text = lbl27_GT.ToString();
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl28_GT") as Label).Text = "0";
     //   int lbl29_GT = Convert.ToInt32(Total) - Convert.ToInt32(Total1Drawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl29_GT") as Label).Text = lbl29_GT.ToString();
     //   int lbl30_GT = Convert.ToInt32(CPF) - Convert.ToInt32(CPF);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl30_GT") as Label).Text = lbl30_GT.ToString();
     //   int lbl31_GT = Convert.ToInt32(Total + CPF) - Convert.ToInt32(Total1Drawn + CPF);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl31_GT") as Label).Text = lbl31_GT.ToString();
     //   int lbl32_GT = Convert.ToInt32(CPF * 2) - Convert.ToInt32(CPF * 2);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl32_GT") as Label).Text = lbl32_GT.ToString();

     //   int lbl33_GT = Convert.ToInt32(txtotherdeduct.Text);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl33_GT") as Label).Text = lbl33_GT.ToString();
     //   int lbl34_GT = Convert.ToInt32(txtincometax.Text);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl34_GT") as Label).Text = lbl34_GT.ToString();
     //   int lbl35_GT = Convert.ToInt32(grandtotal) - Convert.ToInt32(grandtotalDrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl35_GT") as Label).Text = lbl35_GT.ToString();
     //   int lbl36_GT = Convert.ToInt32(grandtotal) - Convert.ToInt32(grandtotalDrawn);
     //   (rptDrawnArrear.Controls[rptDrawnArrear.Controls.Count - 1].Controls[0].FindControl("lbl36_GT") as Label).Text = lbl36_GT.ToString();
        
    }
    public String month(int monthid)
    {
        try
        {
            if (monthid == 1)
                return "Jan";
            else if (monthid == 2)
                return "Feb";
            else if (monthid == 3)
                return "Mar";
            else if (monthid == 4)
                return "Apr";
            else if (monthid == 5)
                return "May";
            else if (monthid == 6)
                return "Jun";
            else if (monthid == 7)
                return "Jul";
            else if (monthid == 8)
                return "Aug";
            else if (monthid == 9)
                return "Sep";
            else if (monthid == 10)
                return "Oct";
            else if (monthid == 11)
                return "Nov";
            else if (monthid == 12)
                return "Dec";
            else
                return "Month not provided..";
        }
        catch {
            return "Month not provided..";
        }
    }
    public String  DAPercDrawn(int monthid, int year)
    {
        try
        {
            DataSet DAdss = new DataSet();
            DAdss = DBLayer.DBLInstance.Get_infoDA6(monthid, year);

            if (DAdss.Tables[0].Rows.Count > 0)
            {
                int DueDaPer = Convert.ToInt32(DAdss.Tables[0].Rows[0]["DAPercent"]);
                return DueDaPer.ToString() + "%"; 
            }
             else{
            return "";
            }
        }
        catch {
            return "Chk";
        }
    }
    public String DAPerc(int monthid, int year)
    {
        try
        {
            DataSet DAdss = new DataSet();
            DAdss = DBLayer.DBLInstance.Get_infoDA7(monthid, year);

            if (DAdss.Tables[0].Rows.Count > 0)
            {
                int DueDaPer = Convert.ToInt32(DAdss.Tables[0].Rows[0]["DAPercent"]);
                return DueDaPer.ToString()+"%";
            }
            else
            {
                return "";
            }
        }
        catch
        {
            return "Chk";
        }
    }
    protected void lnkbtn_Click(object sender, EventArgs e)
    {
        if (txtCode.Text == "")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('Please Enter Employee Code...');", true);
            return;
        }
        DueDrawnArrear();
        
        // ---------- For Update the data Uncomment Update_DueIncrementArrear...
        //DBLayer.DBLInstance.Update_DueIncrementArrear(Convert.ToDecimal(txtincometax.Text), Convert.ToDecimal(txtotherdeduct.Text), txtCode.Text);

        DataSet dss = new DataSet();
        dss = DBLayer.DBLInstance.Get_info(txtCode.Text );
        

        if (dss.Tables[0].Rows.Count > 0)
        {
            pnlContents.Visible = true;
            lblmsg.Text = "";
            lblDesignation.Text = dss.Tables[0].Rows[0]["Designation"].ToString();
            lblUnit.Text = dss.Tables[0].Rows[0]["UnitName"].ToString();
            lblName.Text = dss.Tables[0].Rows[0]["EmpName"].ToString();
            lblOrdNo.Text = dss.Tables[0].Rows[0]["OrderNo"].ToString();
            lblOrdDate.Text = dss.Tables[0].Rows[0]["OrderDate"].ToString();
            lbltodayDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            lblempcode.Text = dss.Tables[0].Rows[0]["Code"].ToString();
            lblempkey.Text = dss.Tables[0].Rows[0]["employeekey"].ToString(); 

        }
        else
        {
            pnlContents.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('No Data found of Employeecode "+txtCode.Text+" ');", true);
        }
    }
    protected void lnkbtnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            String Msg=DBLayer.DBLInstance.Insert_IncrementDataforRpt(lblUnit.Text, txtCode.Text, lblName.Text, lblDesignation.Text, Convert.ToDecimal(lblGAmount.Text), Convert.ToDecimal(txtincometax.Text), Convert.ToDecimal(txtotherdeduct.Text),ddlyear.Text, Convert.ToInt32(lblempkey.Text));
            lblmsg.Text = Msg;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "alert('"+Msg+"');", true);
        }
        catch { }
    }
}