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
	public partial class AddPost : System.Web.UI.Page
	{
        public static int ActivatedSession;
        public static AppUser Usr;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["User"] != null)
            {
                Usr = (AppUser)Session["User"];

                if (Login.Value == "")
                {
                    Pass.Attributes.Add("value", "ThePassword");
                }
                else
                {
                    Pass.Attributes.Add("value", "");
                }
                

                ActivatedSession = 1;                
                Login.Value = Usr.Login;                                
            }
            else 
            {
                ActivatedSession = 0;
            }

            string _Login = Request.Params["Lgn"];
            string _Pass = Request.Params["Ps"];

            if (_Login != null && _Pass != "")
            {
                AppUser.ValidateUser(_Login, _Pass);
            }

            if (!Page.IsPostBack)
            {
                Category.LoadCategoriesGen(ddlbCat);
                Category.LoadCategoriesGen(ddlbCat1);
            }
        }


        protected void SearchArticle(object sender, EventArgs e)
        {
            SearchData SD = new SearchData(txtbxSearchArticle.Text, hiddenddlbCat.Value);

            Session["Search"] = SD;

            Response.Redirect("Home.aspx");
        }


        protected void PostSelection(object sender, EventArgs e)
        {
            PnlGoodPost.Visible = false;
            PnlBadAfterConf.Visible = false;
            PnlValidMail.Visible = false;
            PnlAddPost.Visible = true;
        }


        protected void AddNewPost(object sender, EventArgs e)
        {
            try
            {

                if (ActivatedSession == 1 && Pass.Text == "ThePassword")
                {
                    Pass.Text = Usr.Pass;
                }

                bool ExistUsr = AppUser.isExitingUser(Login.Value.Trim());


                if (isValidForm())
                {
                    int DdlIndex = Convert.ToInt32(hiddenddlbCat1.Value);
                    string SelectedCat = ddlbCat.Items[DdlIndex].Text;
                    int categoryId = Category.GetCategoryId(SelectedCat);

                    if (ExistUsr)
                    {
                        if (isCorrectUserLog(Login.Value.Trim(), Pass.Text.Trim()))
                        {
                            if (AppUser.isValidUser(Login.Value.Trim()))
                            {
                                AddNewPost(AppUser.GetUserId(Login.Value.Trim(), Pass.Text.Trim()), categoryId, Post.InnerText);
                                PnlAddPost.Visible = false;
                                PnlGoodPost.Visible = true;
                                RefreshCurrentPost();
                            }
                            else
                            {
                                //Veuillez valider votre e-mail et on renvoit.
                                AppUser.SendMailForValidation(Login.Value.Trim(), Pass.Text.Trim());
                                AddNewPost(AppUser.GetUserId(Login.Value.Trim(), Pass.Text.Trim()), categoryId, Post.InnerText);
                                PnlAddPost.Visible = false;
                                PnlBadAfterConf.Visible = true;
                                RefreshCurrentPost();
                            }
                        }
                    }
                    else
                    {

                        //New User
                        int iduser = AppUser.CreateNewUser(Login.Value, Pass.Text);
                        //Post
                        AppUser.SendMailForValidation(Login.Value.Trim(), Pass.Text.Trim());
                        AddNewPost(iduser, categoryId, Post.InnerText);
                        PnlAddPost.Visible = false;
                        PnlBadAfterConf.Visible = true;
                        RefreshCurrentPost();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void AddNewPost(int user, int category, string PostTxt)
        {            
            ServicesExchange.Post.AddNewPostQuery(user, category, PostTxt);
            RefreshCurrentPost();
        }


        protected bool isValidForm()
        {
            sLogin.ForeColor = Color.Black;
            sPass.ForeColor = Color.Black;
            sPost.ForeColor = Color.Black;
            sCategorie.ForeColor = Color.Black;

            var errors = 0;
            Match match = Regex.Match(Login.Value, RegexMail.Value);

            if (Login.Value == "" || match.Success == false)
            {
                errors += 1;
                sLogin.ForeColor = Color.Red;
            }

            if (Pass.Text == "")
            {
                errors += 1;
                sPass.ForeColor = Color.Red;
            }

            if (Post.Value == "")
            {
                errors += 1;
                sPost.ForeColor = Color.Red;
            }

            if (hiddenddlbCat1.Value == "")
            {
                errors += 1;
                sCategorie.ForeColor = Color.Red;
            }


            if (errors > 0)
            {
                return false;
            }

            return true;
        }


        protected bool isCorrectUserLog(string Log, string Pass)
        {
            sLogin.ForeColor = Color.Black;
            sPass.ForeColor = Color.Black;
            MsgErLogPass.Visible = false;

            
            if (AppUser.isCorrectUserLogQuery(Log, Pass))
            {
                return true;
            }
            else
            {
                MsgErLogPass.Visible = true;
                sLogin.ForeColor = Color.Red;
                sPass.ForeColor = Color.Red;
            }

            return false;

        }

        protected void RefreshCurrentPost()
        {
            Post.Value = "";
            ddlbCat1.SelectedIndex = 0;
            hiddenddlbCat1.Value = "";
            Login.Value = "";
            Pass.Text = "";
        }

    }
}