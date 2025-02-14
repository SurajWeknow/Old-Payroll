<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AdminMaster.master" AutoEventWireup="true" CodeFile="ReportCriteria_New.aspx.cs" Inherits="Reports_ReportCriteria_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
        function ConfirmDeletion() {
            answer = confirm("Do you want to delete selected records?");
            if (answer != 0) {
                return true;
            }
            else
                return false;
        }
    </script>

    <script language="javascript" type="text/javascript">
        function popupWindow(url, width, height) {
            testwindow = window.open(url, "NewsEvent", "menubar=0,location=0,status=1,resizable=0,scrollbars=1,width=" + width + ",height=" + height);
            testwindow.moveTo(0, 0);
        }
        function check_changed() {
            PageMethods.check_changed();
            //  alert('hi');
        }
    </script>
    
<div id="main-content">     
    <asp:HiddenField ID="hidCurrentPageNo" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidEditRecordKey" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidGridSection" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidSearchCondition" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidOrderByClause" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidAscOrDsc" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidRecordStartIndex" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidRecordEndIndex" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hidTotalRecord" runat="server"></asp:HiddenField>
        <!-- BEGIN PAGE CONTAINER-->
        <div class="form-horizontal form-label-left">
            <!-- BEGIN PAGE HEADER-->
            <div class="page-title">
                <div class="title_left">
                    <h3>
                        <asp:Label ID="lblTitle1" runat="server" ForeColor="#73879C"></asp:Label>
                    </h3>
                </div>
            </div>
            <div class="clearfix"></div>
            <!-- END PAGE HEADER-->
            <!-- BEGIN PAGE CONTENT-->
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="x_panel">
                        <div class="x_title">
                            <h2>
                                <asp:Label ID="lbltitle" runat="server" ForeColor="#73879C"></asp:Label>
                              <%--  Employee Paybill Report--%>
                            </h2>
                         
                            <div class="clearfix">
                            </div>
                        </div>
                                   <div class="x_content"> 
                              <div id="fromto" runat="server" visible="false">
                      
                              <div class="col-md-6 col-sm-6 col-xs-12">
                              <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        From Month <span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                       
                                         <asp:DropDownList ID="cmbfrommonth" runat="server" CssClass="form-control col-md-7 col-xs-12">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                               <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                       From  Year <span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                       <asp:DropDownList ID="cmbfromyear" runat="server" CssClass="form-control col-md-7 col-xs-12"
                                            required="required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            </div>              
                           
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Month <span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">                       
                                         <asp:DropDownList ID="cmbMonth" runat="server" CssClass="form-control col-md-7 col-xs-12">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                               <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12">
                                        Year <span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                       <asp:DropDownList ID="cmbYear" runat="server" CssClass="form-control col-md-7 col-xs-12"
                                            required="required">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group" id="divunit" runat="server">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Unit Name <span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                           
                                        <asp:DropDownList ID="cmbPosting" runat="server" CssClass="form-control col-md-7 col-xs-12"
                                       AutoPostBack="True" 
                                            onselectedindexchanged="cmbPosting_SelectedIndexChanged">
                                        </asp:DropDownList>
                              
                                    </div>
                                </div>
                                 <div class="form-group" id="selectall" runat="server" >
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Select All Employee  <span class="required"></span>
                                         
                                    </label>

                                    <div class="col-md-6 col-sm-6 col-xs-12">
                            <asp:CheckBox ID="chkAllSlip" Text="Show All" runat="server" 
                                    AutoPostBack="True" oncheckedchanged="chkAllSlip_CheckedChanged" />
                                   
                                    </div>
                                   
                                </div>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                                 <div class="form-group" id="billno" runat="server">
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                       Bill No.<span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                 

                                         <asp:TextBox ID="txtBillno" runat="server"  Text="01"  CssClass="form-control col-md-7 col-xs-12"  MaxLength="6" required="required"></asp:TextBox>
                                    </div>
                                   
                                </div>                               
                               
                            </div>

                             

                            <div class="col-md-6 col-sm-6 col-xs-12">
                                <div class="form-group" id="EmpName" runat="server" >
                                    <label class="control-label col-md-4 col-sm-4 col-xs-12" for="first-name">
                                        Employee Name <span class="required">*</span>
                                    </label>
                                    <div class="col-md-6 col-sm-6 col-xs-12">
                                    <div id="divemplist" runat="server" style=" border-style:ridge; overflow:scroll; padding-left:5px; height:35px">
                                     <asp:CheckBoxList ID="chkEmployeeName" runat="server" >
                                        </asp:CheckBoxList>    </div>
                              
                                    </div>
                                </div>
                                </div>

                            <div class="clearfix"> </div>                      

                     
                             <div id="divchkAll" runat="server" class="col-md-6 col-sm-6 col-xs-12">
                               
                            </div>
                                   <div class="clearfix">
                               
                            </div>
                            <div class="ln_solid">
                            </div>
                                  
                             <div class="form-group">
                                <div class="col-md-12 col-sm-12 col-xs-12 col-md-offset-11">
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary"
                                        Visible="false" onclick="btnCancel_Click" />
                                    <asp:Button ID="btnSearch" runat="server" Text="Go" CssClass="btn btn-success" 
                                        onclick="btnSearch_Click" />
                                        <asp:Button ID="btnExcel" runat="server" Text="Import to Excel" 
                                        CssClass="btn btn-success" onclick="btnExcel_Click" 
                                        />
                                </div>
                            </div>
                            
                          
                            
                        </div>
                       
                    </div>
                </div>
            </div>
            <div class="clearfix">
            </div>
          
            <!-- END PAGE CONTENT-->
        </div>

        <!-- END PAGE CONTAINER-->
        
        <script type="text/javascript" src="<%= ResolveClientUrl("~/js/datatables/js/jquery.dataTables.js")%>"></script> 
         
    </div>

</asp:Content>

