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
    public partial class EditPost : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                if (!Page.IsPostBack)
                {
                    Category.LoadCategoriesGen(ddlbCat);
                    Category.LoadCategoriesGen(ddlbCat1EditPost);

                    if (Session["PostToEdit"] != null)
                    {
                        Post PostToEdit = (Post)Session["PostToEdit"];

                        PostEditPost.Value = PostToEdit._Post;
                        ListItem lstItm = ddlbCat1EditPost.Items.FindByText(PostToEdit.Categorie);
                        ddlbCat1EditPost.SelectedIndex = Int32.Parse(lstItm.Value);
                        hiddenddlbCat1EditPost.Value = lstItm.Value;
                        hiddenIdEditPostUser.Text = PostToEdit.User.ToString();
                        hiddenIdEditPost.Text = PostToEdit.Id.ToString();
                    }
                    else
                    {
                        Response.Redirect("MyPosts.aspx");
                    }
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

        protected bool isValidPostEdition()
        {
            sPostEditPost.ForeColor = Color.Black;
            sCategorieEditPost.ForeColor = Color.Black;

            var errors = 0;

            if (PostEditPost.Value == "")
            {
                errors += 1;
                sPostEditPost.ForeColor = Color.Red;
            }

            if (ddlbCat1EditPost.SelectedValue == "")
            {
                errors += 1;
                sCategorieEditPost.ForeColor = Color.Red;
            }

            if (errors > 0)
            {
                return false;
            }

            return true;

        }


        protected void UpdatePost(object sender, EventArgs e)
        {                      
            try
            {
                if (isValidPostEdition())
                {
                   int result = Post.UpdatePost(PostEditPost.Value.Trim(), Int32.Parse(hiddenddlbCat1EditPost.Value.Trim()), Int32.Parse(hiddenIdEditPostUser.Text.Trim()), Int32.Parse(hiddenIdEditPost.Text.Trim()));

                   if (result != 0)
                   {
                       Session["PostToEdit"] = null;
                       Response.Redirect("MyPosts.aspx");
                   }
                   else
                   {
                       Response.Redirect("EditPost.aspx");
                   }
                }
            }
            catch (Exception ex)
            {
            }

        }

    }
}