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
    public partial class Home : System.Web.UI.Page
    {

        public static List<Post> ShowPosts = new List<Post>();
        public static int del;
        public static int FlagLoadManagePosts;

        protected void Page_Load(object sender, EventArgs e)
        {
            RepeatPosts.DataSource = ShowPosts;

            if (!Page.IsPostBack)
            {
                Category.LoadCategoriesGen(ddlbCat);                
                LoadView();
            }

            SearchData SD = (SearchData)Session["Search"];

            if (SD != null)
            {
                txtbxSearchArticle.Text = SD.MC;
                hiddenddlbCat.Value = SD.Cat;

                SearchArticle(sender, e);

                Session["Search"] = null;
            }
        }

        protected void SearchArticle(object sender, EventArgs e)
        {
            int categoryId;

            if (hiddenddlbCat.Value != "")
            {
                int DdlIndex = Convert.ToInt32(hiddenddlbCat.Value);
                string SelectedCat = ddlbCat.Items[DdlIndex].Text;
                categoryId = Category.GetCategoryId(SelectedCat);
            }
            else
            {
                categoryId = 0;
            }


            ShowPosts.Clear();
            RepeatPosts.DataBind();

            // les 2 sont vides
            if (string.IsNullOrEmpty(txtbxSearchArticle.Text) && categoryId == 0)
            {
                LoadView();
            }
            else
            {             
                //MC seul
                if (!string.IsNullOrEmpty(txtbxSearchArticle.Text) && categoryId == 0)
                {
                    SearchMC(txtbxSearchArticle.Text);
                }

                //Cat seule
                else if (string.IsNullOrEmpty(txtbxSearchArticle.Text) && categoryId != 0)
                {
                    SearchCat(categoryId);
                }
                //les 2
                else if (!string.IsNullOrEmpty(txtbxSearchArticle.Text) && categoryId != 0)
                {
                    SearchMC_Cat(txtbxSearchArticle.Text, categoryId);
                }
            }

        }

        protected void SearchMC(string MC)
        {

            List<Post> result = new List<Post>();

            result = Post.getPostsByMC(MC);

            if (result != null)
            {
                foreach (Post pst in result)
                {                    
                    ShowPosts.Add(pst);
                    RepeatPosts.DataBind();
                }
            }
        }

        protected void SearchCat(int Cat)
        {

            List<Post> result = new List<Post>();

            result = Post.getPostsByCat(Cat);

            if (result != null)
            {
                foreach (Post pst in result)
                {                    
                    ShowPosts.Add(pst);
                    RepeatPosts.DataBind();
                }
            }

        }

        protected void SearchMC_Cat(string MC, int Cat)
        {

            List<Post> result = new List<Post>();

            result = Post.getPostsByCat_MC(MC, Cat);

            if (result != null)
            {               
                foreach (Post pst in result)
                {
                    ShowPosts.Add(pst);
                    RepeatPosts.DataBind();
                }
            }

        }

        protected void LoadView()
        {
            ShowPosts.Clear();

            List<Post> result = new List<Post>();

            result = Post.getLatestPosts();

            if (result != null)
            {                
                foreach (Post pst in result)
                {                    
                    ShowPosts.Add(pst);
                    RepeatPosts.DataBind();
                }
            }

        }

    }
}







