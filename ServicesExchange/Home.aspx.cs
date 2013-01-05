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
            string SelectedCat = hiddenddlbCat.Value;

            ShowPosts.Clear();
            RepeatPosts.DataBind();

            // les 2 sont vides
            if (string.IsNullOrEmpty(txtbxSearchArticle.Text) && SelectedCat == "")
            {
                LoadView();
            }
            else
            {             
                //MC seul
                if (!string.IsNullOrEmpty(txtbxSearchArticle.Text) && SelectedCat == "")
                {
                    SearchMC(txtbxSearchArticle.Text);
                }

                //Cat seule
                else if (string.IsNullOrEmpty(txtbxSearchArticle.Text) && SelectedCat != "")
                {
                    SearchCat(SelectedCat);
                }
                //les 2
                else if (!string.IsNullOrEmpty(txtbxSearchArticle.Text) && SelectedCat != "")
                {
                    SearchMC_Cat(txtbxSearchArticle.Text, SelectedCat);
                }
            }

        }

        protected void SearchMC(string MC)
        {

            DataSet result;

            if (Post.getPostsByMC(MC) != null)
            {
                result = Post.getPostsByMC(MC);


                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }
            }
        }

        protected void SearchCat(string Cat)
        {

            DataSet result;
            if (Post.getPostsByCat(Cat) != null)
            {
                result = Post.getPostsByCat(Cat);


                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }
            }

        }

        protected void SearchMC_Cat(string MC, string Cat)
        {

            DataSet result;

            if (Post.getPostsByCat_MC(MC,Cat) != null)
            {
                result = Post.getPostsByCat_MC(MC, Cat);

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }
            }

        }

        protected void LoadView()
        {
            ShowPosts.Clear();
            DataSet result;

            if (Post.getLatestPosts() != null)
            {
                result = Post.getLatestPosts();

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }
            }

        }

    }
}







