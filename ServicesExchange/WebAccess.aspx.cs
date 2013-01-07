using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServicesExchange
{
    public partial class WebAccess : System.Web.UI.Page
    {

        // verifier les parametres d'entrée des fonctions!

        public static string Log;
        public static string Pass;
        public static string posttxt;
        public static string categorytxt;
        public static int categoryid;
        public static int userid;
        public static int postid;
        public static string mc;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string Class = Request.Params["Cl"];
                string Func = Request.Params["Fc"];
                Log = Request.Params["Log"];
                Pass = Request.Params["Pass"];
                posttxt = Request.Params["Post"];
                categorytxt = Request.Params["Categorytxt"];
                categoryid = Convert.ToInt32(Request.Params["Categoryid"]);
                userid = Convert.ToInt32(Request.Params["UserId"]);
                postid = Convert.ToInt32(Request.Params["PostId"]);
                mc = Request.Params["Mc"];


                if (Class != null && Func != null)
                {

                    if (Class == "user")
                    {
                        UserFunctions(Func);
                    }

                    if (Class == "category")
                    {
                        CategoryFunctions(Func);
                    }

                    if (Class == "post")
                    {
                        PostFunctions(Func);
                    }

                    if (Class == "session")
                    {
                        SessionFunctions(Func);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(false);
            }

        }

        protected void UserFunctions(string Function)
        {
            if (Function == "GetUser")
            {
                AppUser usr = AppUser.GetUser(Log, Pass);

                string jsonString = JsonHelper.JsonSerializer<AppUser>(usr);
                Response.Write(jsonString);
            }

            if (Function == "GetUserId")
            {
                int usrid = AppUser.GetUserId(Log, Pass);

                string jsonString = JsonHelper.JsonSerializer<Int32>(usrid);
                Response.Write(jsonString);
            }

            // Utile pour savoir s'il faut créer un nouvel utilisateur ou pas.
            if (Function == "isExitingUser")
            {
                bool bl = AppUser.isExitingUser(Log);

                string jsonString = JsonHelper.JsonSerializer<bool>(bl);
                Response.Write(jsonString);
            }

            // Utile pour savoir s'il faut demander à l'utilisateur qu'il valide son compte (mail qu'il a recu pour ça)
            if (Function == "isValidUser")
            {
                bool bl = AppUser.isValidUser(Log);

                string jsonString = JsonHelper.JsonSerializer<bool>(bl);
                Response.Write(jsonString);
            }

            if (Function == "CreateNewUser")
            {
                int usrid = AppUser.CreateNewUser(Log, Pass);

                string jsonString = JsonHelper.JsonSerializer<Int32>(usrid);
                Response.Write(jsonString);
            }

            if (Function == "SendMailForValidation")
            {
                bool bl = AppUser.SendMailForValidation(Log, Pass);

                string jsonString = JsonHelper.JsonSerializer<bool>(bl);
                Response.Write(jsonString);
            }

            if (Function == "ValidateUser")
            {
                bool bl = AppUser.ValidateUser(Log, Pass);

                string jsonString = JsonHelper.JsonSerializer<bool>(bl);
                Response.Write(jsonString);
            }        

        }

        protected void CategoryFunctions(string Function)
        {
            if (Function == "GetCategories")
            {                
                List<Category> LstCat = new List<Category>();
                LstCat = Category.GetCategories();

                string jsonString = JsonHelper.JsonSerializer<List<Category>>(LstCat);
                Response.Write(jsonString);

            }

            if (Function == "GetCategoryId")
            {
                int CatId = Category.GetCategoryId(categorytxt);

                string jsonString = JsonHelper.JsonSerializer<Int32>(CatId);
                Response.Write(jsonString);
            }

            if (Function == "GetCategoryValue")
            {
                string CatTxt = Category.GetCategoryValue(categoryid);

                string jsonString = JsonHelper.JsonSerializer<string>(CatTxt);
                Response.Write(jsonString);
            }        
        }


        protected void PostFunctions(string Function)
        {
            if (Function == "getLatestPosts")
            {
                List<Post> LstPst = Post.getLatestPosts();

                string jsonString = JsonHelper.JsonSerializer<List<Post>>(LstPst);
                Response.Write(jsonString);
            }

            if (Function == "getPostsByMC")
            {
                List<Post> LstPst = Post.getPostsByMC(mc);

                string jsonString = JsonHelper.JsonSerializer<List<Post>>(LstPst);
                Response.Write(jsonString);
            }

            if (Function == "getPostsByCat")
            {
                List<Post> LstPst = Post.getPostsByCat(categoryid);

                string jsonString = JsonHelper.JsonSerializer<List<Post>>(LstPst);
                Response.Write(jsonString);
            }

            if (Function == "getPostsByCat_MC")
            {
                List<Post> LstPst = Post.getPostsByCat_MC(mc, categoryid);

                string jsonString = JsonHelper.JsonSerializer<List<Post>>(LstPst);
                Response.Write(jsonString);
            }

            if (Function == "LoadUserPostsQuery")
            {
                List<Post> LstPst = Post.LoadUserPostsQuery(userid);

                string jsonString = JsonHelper.JsonSerializer<List<Post>>(LstPst);
                Response.Write(jsonString);
            }

            if (Function == "AddNewPostQuery")
            {
                int PstId = Post.AddNewPostQuery(userid, categoryid, posttxt);

                string jsonString = JsonHelper.JsonSerializer<Int32>(PstId);
                Response.Write(jsonString);
            }

            if (Function == "deletePostFromDb")
            {
                bool bl = Post.deletePostFromDb(postid);

                string jsonString = JsonHelper.JsonSerializer<bool>(bl);
                Response.Write(jsonString);
            }

            if (Function == "UpdatePost")
            {
                int PstId = Post.UpdatePost(posttxt, categoryid, userid, postid);

                string jsonString = JsonHelper.JsonSerializer<Int32>(PstId);
                Response.Write(jsonString);
            }

            if (Function == "GetPostById")
            {
                Post Pst = Post.GetPostById(postid);

                string jsonString = JsonHelper.JsonSerializer<Post>(Pst);
                Response.Write(jsonString);
            }

        }

        protected void SessionFunctions(string Function)
        {
            if (Function == "SetUserSession")
            {
                SetUserSession(userid);
            }

            if (Function == "GetUserSessionStatus")
            {
                int SessStatus = GetUserSessionStatus();

                string jsonString = JsonHelper.JsonSerializer<Int32>(SessStatus);
                Response.Write(jsonString);

            }
        }

        protected void SetUserSession(int userid)
        {
            Session["USER"] = null;
            Session["USER"] = userid;
        }

        protected int GetUserSessionStatus()
        {
            if (Session["USER"] != null)
            {
                int usrid = Convert.ToInt32(Session["USER"]);
                return usrid;
            }
            else
            {
                return 0;
            }
        }



    }
}

