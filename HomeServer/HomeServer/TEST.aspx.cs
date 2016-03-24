using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HomeServer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach(RadioButton rb in RadioButtonList1.Items)
                {
                    Image img = new Image();
                    img.ImageUrl = "~/Images/" + rb.LabelAttributes["Text"] + ".png";
                    rb.Controls.Add(img);
                }
            }
        }
    }
}