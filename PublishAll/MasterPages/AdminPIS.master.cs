using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_AdminPIS : System.Web.UI.MasterPage
{
    public string username;
    protected void Page_Load(object sender, EventArgs e)
    {
        ISession session = new Session();

        if (session.CurrentUser == null)
        {
           // Response.Redirect(Server.MapPath("~/SecureLogin/Login.aspx"));
        }
        else
        {
            username = session.CurrentUser.UserName;
        }

    }
}
