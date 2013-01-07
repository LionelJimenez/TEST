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
    public partial class MyPosts : System.Web.UI.Page
    {

        public static List<Post> ShowPosts = new List<Post>();
        public static int del;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PostToEdit"] = null;

            if (Session["User"] != null)
            {
                AppUser Usr = (AppUser)Session["User"];

                RptrMyPosts.DataSource = ShowPosts;

                if (!Page.IsPostBack)
                {
                    Category.LoadCategoriesGen(ddlbCat);
                    LoadUserPosts(Usr.Id);
                }
            }
            else 
            {
                Response.Redirect("Login_MyPosts.aspx");
            }
        }

        protected void SearchArticle(object sender, EventArgs e)
        {
            SearchData SD = new SearchData(txtbxSearchArticle.Text, hiddenddlbCat.Value);

            Session["Search"] = SD;

            Response.Redirect("Home.aspx");
        }



        protected void LoadUserPosts(int user)
        {

            ShowPosts.Clear();

            List<Post> result = new List<Post>();

            result = Post.LoadUserPostsQuery(user);

            if (result != null)
            {
                foreach (Post pst in result)
                {                    
                    ShowPosts.Add(pst);
                    RptrMyPosts.DataBind();
                }
            }

        }

        protected void EditPost(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                Button Btn = (Button)sender;
                Label lbl = (Label)Btn.FindControl("LblID");
                int Id = Int32.Parse(lbl.Text);


                Post PostToEdit = ShowPosts.Find(
                delegate(Post pst)
                {
                    return pst.Id == Id;
                }
                );


                Session["PostToEdit"] = PostToEdit;

                Response.Redirect("EditPost.aspx");
            }
            else
            {
                Response.Redirect("Login_MyPosts.aspx");
            }
        }


        protected void DeletePost(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                Button Btn = (Button)sender;
                Label lbl = (Label)Btn.FindControl("LblID");
                int Id = Int32.Parse(lbl.Text);


                if (Post.deletePostFromDb(Id))
                {

                    for (int i = 0; i < ShowPosts.Count; i++)
                    {
                        if (ShowPosts[i].Id == Id)
                        {
                            del = i;
                        }
                    }
                    ShowPosts.RemoveAt(del);
                    RptrMyPosts.DataBind();
                }
            }
            else
            {
                Response.Redirect("Login_MyPosts.aspx");
            }
        }

    }
}