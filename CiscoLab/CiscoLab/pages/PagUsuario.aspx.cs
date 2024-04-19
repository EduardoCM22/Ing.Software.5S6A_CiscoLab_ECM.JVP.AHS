using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CiscoLab.pages
{
    public partial class PagUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["Usuario"];
        }
    }
}