using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ServicesExchange
{
    public partial class Login_MyPosts : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                Category.LoadCategoriesGen(ddlbCat);
            }

        }

        protected void SearchArticle(object sender, EventArgs e)
        {
            SearchData SD = new SearchData(txtbxSearchArticle.Text, hiddenddlbCat.Value);

            Session["Search"] = SD;

            Response.Redirect("Home.aspx");
        }

        protected bool isCorrectUserLog(string Log, string Pass)
        {
            sLoginAccessMyPosts.ForeColor = Color.Black;
            sPassAccessMyPosts.ForeColor = Color.Black;
            MsgErLogPassAccessMyPosts.Visible = false;


            if (AppUser.isCorrectUserLogQuery(Log,Pass))
            {
                return true;
            }
            else
            {
                MsgErLogPassAccessMyPosts.Visible = true;
                sLoginAccessMyPosts.ForeColor = Color.Red;
                sPassAccessMyPosts.ForeColor = Color.Red;
            }

            return false;

        }

        protected void ManagePosts(object sender, EventArgs e)
        {
            if (isCorrectUserLog(LoginAccessMyPosts.Value.Trim(), PassAccessMyPosts.Text.Trim()) == true)
            {
                AppUser Usr = new AppUser(LoginAccessMyPosts.Value.Trim(), PassAccessMyPosts.Text.Trim(), AppUser.GetUserId(LoginAccessMyPosts.Value.Trim(), PassAccessMyPosts.Text.Trim()));
                Session["User"]= Usr;
                Response.Redirect("MyPosts.aspx");             
            }
        }

        

    }
}