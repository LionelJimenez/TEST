using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using ASP;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Drawing;
using System.Text.RegularExpressions;

namespace WebApplication6
{
    public partial class Default : System.Web.UI.Page
    {

        public static List<Post> ShowPosts = new List<Post>();
        public static int del;
        

        protected void Page_Load(object sender, EventArgs e)
        {

            string _Login = Request.Params["Lgn"];
            string _Pass = Request.Params["Ps"];

            if (_Login != null && _Login != "")
            {
                ValidateMail(_Login, _Pass);

                PnlAddPost.Visible = false;
                PnlViewSearch.Visible = false;
                PnlValidMail.Visible = true;
            }


            RepeatPosts.DataSource = ShowPosts;
            RptrMyPosts.DataSource = ShowPosts;

            if (!Page.IsPostBack)
            {
                LoadCategories1();
                LoadCategories2();
                LoadView();
            }
        }

        protected void LoadCategories1()
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT [ID]
                                    ,[Categorie]
                                    FROM [dbo].[Categories]
                                END
                            ";

                //SqlConnection connection = new SqlConnection("Data Source=XRDT-PC\\SQLEXPRESS;Initial Catalog=SE;Integrated Security=True");
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                //command.Parameters.AddWithValue("@Mail", TxtbxMail.Text);
                adapter.Fill(result);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    ddlbCat.DataSource = result;
                    ddlbCat.DataTextField = "Categorie";
                    ddlbCat.DataValueField = "ID";
                    ddlbCat.DataBind();
                    ddlbCat.Items.Insert(0, new ListItem(""));

                }

            }
            catch (Exception ex)
            {

            }


        }

        protected string CategoryValue(int CatId)
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT 
                                    [Categorie]
                                    FROM 
                                    [dbo].[Categories]
                                    WHERE
                                    [ID] = @IdCat
                                END
                            ";

                //SqlConnection connection = new SqlConnection("Data Source=XRDT-PC\\SQLEXPRESS;Initial Catalog=SE;Integrated Security=True");
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@IdCat", CatId);
                adapter.Fill(result);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {

                    return result.Tables[0].Rows[0]["Categorie"].ToString();
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        protected void LoadCategories2()
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT [ID]
                                    ,[Categorie]
                                    FROM [dbo].[Categories]
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                adapter.Fill(result);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    ddlbCat1.DataSource = result;
                    ddlbCat1.DataTextField = "Categorie";
                    ddlbCat1.DataValueField = "ID";
                    ddlbCat1.DataBind();
                    ddlbCat1.Items.Insert(0, new ListItem(""));

                }

            }
            catch (Exception ex)
            {

            }

        }

        protected void LoadCategoriesGen(DropDownList DDL)
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT [ID]
                                    ,[Categorie]
                                    FROM [dbo].[Categories]
                                END
                            ";

                //SqlConnection connection = new SqlConnection("Data Source=XRDT-PC\\SQLEXPRESS;Initial Catalog=SE;Integrated Security=True");
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                //command.Parameters.AddWithValue("@Mail", TxtbxMail.Text);
                adapter.Fill(result);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    DDL.DataSource = result;
                    DDL.DataTextField = "Categorie";
                    DDL.DataValueField = "ID";
                    DDL.DataBind();
                    DDL.Items.Insert(0, new ListItem(""));

                }

            }
            catch (Exception ex)
            {

            }
        }



        protected void SearchArticle(object sender, EventArgs e)
        {
            string SelectedCat = hiddenddlbCat.Value;

            PnlValidMail.Visible = false;
            PnlAddPost.Visible = false;
            PnlGoodPost.Visible = false;
            PnlBadAfterConf.Visible = false;
            PnlManagePost.Visible = false;
            PnlEditPost.Visible = false;
            RefreshCurrentPost();

            // les 2 sont vides
            if (string.IsNullOrEmpty(txtbxSearchArticle.Text) && SelectedCat == "")
            {
                //On précise qu'il faut au moins un des 2
            }
            else
            {
                PnlViewSearch.Visible = true;
                ShowPosts.Clear();

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

            // Affichage bouton ajout
            //if (ShowPrds.Count > 0)
            //{
            //    lnkbtnAjoutPanier.Visible = true;
            //    ArticleBarre.Visible = true;
            //}
            //else
            //{
            //    lnkbtnAjoutPanier.Visible = false;
            //    ArticleBarre.Visible = false;
            //}

        }

        protected void SearchMC(string MC)
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT
                                    Base.[ID]
                                    ,Base.[Post]
                                    ,Base.[Fk_User]
                                    ,Cat.[Categorie]
                                    ,Base.[CreatedDate]
									FROM
                                    (                                    
                                    SELECT 
                                    P.[ID]
                                    ,P.[Post]
                                    ,P.[Fk_User]
                                    ,P.[Fk_Categorie]
                                    ,P.[CreatedDate]
                                    FROM 
                                    [dbo].[Posts] as P,
                                    [dbo].[Users] as U
                                    WHERE
                                    Post like '%'+@MC+'%'
                                    and U.ID = P.Fk_User
                                    and U.IsValid = 1
                                    ) as Base
									left join
									[dbo].[Categories] as Cat
									on
									Base.Fk_Categorie = Cat.ID
                                    order by Cat.[Categorie], Base.CreatedDate desc
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@MC", MC);
                adapter.Fill(result);

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }


            }
            catch (Exception ex)
            {

            }
        }
        protected void SearchCat(string Cat)
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT
                                    Base.[ID]
                                    ,Base.[Post]
                                    ,Base.[Fk_User]
                                    ,Cat.[Categorie]
                                    ,Base.[CreatedDate]
									FROM
                                    (                                    
                                    SELECT 
                                    P.[ID]
                                    ,P.[Post]
                                    ,P.[Fk_User]
                                    ,P.[Fk_Categorie]
                                    ,P.[CreatedDate]
                                    FROM 
                                    [dbo].[Posts] as P,
                                    [dbo].[Users] as U
                                    WHERE
                                    Fk_Categorie = @Cat
                                    and U.ID = P.Fk_User
                                    and U.IsValid = 1
                                    ) as Base
									left join
									[dbo].[Categories] as Cat
									on
									Base.Fk_Categorie = Cat.ID
                                    order by Base.CreatedDate desc
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Cat", Cat);

                adapter.Fill(result);

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void SearchMC_Cat(string MC, string Cat)
        {


            try
            {
                string query = @"
                                BEGIN
                                    SELECT
                                    Base.[ID]
                                    ,Base.[Post]
                                    ,Base.[Fk_User]
                                    ,Cat.[Categorie]
                                    ,Base.[CreatedDate]
									FROM
                                    (                                    
                                    SELECT 
                                    P.[ID]
                                    ,P.[Post]
                                    ,P.[Fk_User]
                                    ,P.[Fk_Categorie]
                                    ,P.[CreatedDate]
                                    FROM 
                                    [dbo].[Posts] as P,
                                    [dbo].[Users] as U
                                    WHERE
                                    Fk_Categorie = @Cat
                                    AND
                                    Post like '%'+@MC+'%'
                                    and U.ID = P.Fk_User
                                    and U.IsValid = 1
                                    ) as Base
									left join
									[dbo].[Categories] as Cat
									on
									Base.Fk_Categorie = Cat.ID
                                    order by Base.CreatedDate desc
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Cat", Cat);
                command.Parameters.AddWithValue("@MC", MC);

                adapter.Fill(result);

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }

            }
            catch (Exception ex)
            {

            }

        }

        protected void LoadView()
        {

            ShowPosts.Clear();

            try
            {
                string query = @"
                                BEGIN
                                    SELECT
                                    TOP 10
                                    Base.[ID]
                                    ,Base.[Post]
                                    ,Base.[Fk_User]
                                    ,Cat.[Categorie]
                                    ,Base.[CreatedDate]
									FROM
                                    (
                                    SELECT 
                                    P.[ID]
                                    ,P.[Post]
                                    ,P.[Fk_User]
                                    ,P.[Fk_Categorie]
                                    ,P.[CreatedDate]
                                    FROM 
                                    [dbo].[Posts] as P,
                                    [dbo].[Users] as U
                                    WHERE
                                    U.ID = P.Fk_User
                                    and U.IsValid = 1
									) as Base
									left join
									[dbo].[Categories] as Cat
									on
									Base.Fk_Categorie = Cat.ID
                                    order by Cat.[Categorie], Base.CreatedDate desc
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                adapter.Fill(result);

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RepeatPosts.DataBind();
                }

            }
            catch (Exception ex)
            {

            }

        }


        protected void PostSelection(object sender, EventArgs e)
        {
            PnlEditPost.Visible = false;
            PnlManagePost.Visible = false;
            PnlGoodPost.Visible = false;
            PnlBadAfterConf.Visible = false;
            PnlValidMail.Visible = false;
            PnlAddPost.Visible = true;
            PnlViewSearch.Visible = false;


        }


        protected void AddNewPost(object sender, EventArgs e)
        {
            try
            {

                int usr = isExitingUser(Login.Value.Trim());

                if (isValid())
                {

                    if (usr != 0)
                    {
                        if (isCorrectUserLog(Login.Value.Trim(), Pass.Value.Trim()))
                        {
                            if (isValidUser(Login.Value.Trim()))
                            {
                                AddPost(usr, hiddenddlbCat1.Value);
                                PnlAddPost.Visible = false;
                                PnlGoodPost.Visible = true;
                                RefreshCurrentPost();
                            }
                            else
                            {
                                //Veuillez valider votre e-mail et on renvoit.
                                SendMail(Login.Value);
                                AddPost(usr, hiddenddlbCat1.Value);
                                PnlAddPost.Visible = false;
                                PnlBadAfterConf.Visible = true;
                                RefreshCurrentPost();
                            }
                        }
                    }
                    else
                    {

                        //New User
                        int iduser = CreateUser(Login.Value, Pass.Value);
                        //Post
                        SendMail(Login.Value);
                        AddPost(iduser, hiddenddlbCat1.Value);
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


        protected int CreateNewUser()
        {


            return 0;
        }

        protected int isExitingUser(string Log)
        {
            int user;

            try
            {
                string query = @"
                                BEGIN
                                SELECT [ID]
                                      ,[Login]
                                      ,[Password]
                                      ,[IsValid]
                                      ,[Firstname]
                                      ,[Name]
                                  FROM [dbo].[Users]
                                  WHERE
                                  Login = @mail COLLATE French_BIN
                                  and Login is not null and Login <>''
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@mail", Log.Trim());

                adapter.Fill(result);


                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    return user = Convert.ToInt32(result.Tables[0].Rows[0]["ID"]);
                }
                else
                {
                    return user = 0;
                }
            }
            catch (Exception ex)
            {
                return user = 0;
            }
        }



        protected bool isValidUser(string Log)
        {
            try
            {
                string query = @"
                                BEGIN
                                SELECT [ID]
                                      ,[Login]
                                      ,[Password]
                                      ,[IsValid]
                                      ,[Firstname]
                                      ,[Name]
                                  FROM [dbo].[Users]
                                  WHERE
                                  Login like @Mail COLLATE French_BIN
                                  and Login is not null
                                  and Login <>''
                                  and IsValid = 1
                                END
                            ";


                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Mail", Log.Trim());

                adapter.Fill(result);


                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        protected void AddPost(int user, string category)
        {

            try
            {


                string query = @"
                                BEGIN

                                BEGIN TRAN  
                                BEGIN TRY

                                INSERT INTO 
                                [dbo].[Posts]
                                   (
                                   [Post]
                                   ,[Fk_User]
                                   ,[Fk_Categorie]
                                   ,[CreatedDate]
                                   )
                             VALUES
                                   (
                                    @post
                                   ,@user
                                   ,@Cat
                                   ,GETDATE()
                                   )

                                COMMIT TRAN  
                                END TRY  
                                BEGIN CATCH  
                                ROLLBACK TRAN  
                                END CATCH  

                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Cat", category.Trim());
                command.Parameters.AddWithValue("@user", user);
                command.Parameters.AddWithValue("@post", Post.Value.Trim());

                adapter.Fill(result);

                RefreshCurrentPost();

            }
            catch (Exception ex)
            {
            }
        }


        protected void SendMail(string MailTo)
        {
            try
            {

                // J'envoie un mail

                MailMessage mail = new MailMessage();
                mail.To.Add(MailTo);

                mail.From = new MailAddress("lionel.jimenez.netdevelopper@gmail.com");
                mail.Subject = "Une nouvelle demande de contact est arrivée";

                mail.IsBodyHtml = true;

                mail.Body = "<html>"
                            + "<head>"
                            + "<meta http-equiv=\"Content-Language\" content=\"fr\">"
                            + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">"
                            + "</head>"
                            + "<body>"
                            + "<h2>VALIDATION DE VOTRE ADRESSE EMAIL</h2>"
                            + "<p>"
                            + "<span>Veuillez valider votre email en cliquant sur le lien suivant :</span><br />"
                            + "<a href=http://servicesexchange.apphb.com/Default.aspx?Lgn=" + Login.Value + "&Ps=" + Pass.Value + ">Lien de validation</a>"
                            + "</p>"
                            + "</body>"
                            + "</html>";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential
                     ("lionel.jimenez.netdevelopper@gmail.com", "Jcfgrgdd11");

                smtp.EnableSsl = true;
                smtp.Send(mail);

            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="Pass"></param>
        /// <returns></returns>
        protected int CreateUser(string Login, string Pass)
        {
            int NewUser;

            try
            {


                string query = string.Format(@"
                                BEGIN
                                    BEGIN TRAN  
                                    BEGIN TRY

                                    INSERT INTO [dbo].[Users]
                                           (
                                           [Login]
                                           ,[Password]
                                           ,[IsValid]
                                           )
                                     VALUES
                                           (
                                            @Login
                                           ,@Password
                                           ,0
                                           )

                                            Select @@Identity;

                                                                    
                                        COMMIT TRAN  
                                    END TRY  
                                    BEGIN CATCH  
                                        ROLLBACK TRAN  
                                    END CATCH  
                                END");


                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@Login", Login.Trim());
                    command.Parameters.AddWithValue("@Password", Pass.Trim());
                    connection.Open();

                    NewUser = Convert.ToInt32(command.ExecuteScalar());
                }


                return NewUser;

            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        protected bool isValid()
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

            if (Pass.Value == "")
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


        protected void ValidateMail(string Login, string Pass)
        {


            try
            {


                string query = @"
                                BEGIN

                                BEGIN TRAN  
                                BEGIN TRY

                                  UPDATE [dbo].[Users]
                                  SET
                                  [IsValid] = 1
                                  WHERE
                                  LOGIN = @login COLLATE French_BIN
                                  AND
                                  PASSWORD = @pass COLLATE French_BIN

                                COMMIT TRAN  
                                END TRY  
                                BEGIN CATCH  
                                ROLLBACK TRAN  
                                END CATCH  

                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@login", Login.Trim());
                command.Parameters.AddWithValue("@pass", Pass.Trim());

                adapter.Fill(result);

            }
            catch (Exception ex)
            {
            }

        }


        protected bool isCorrectUserLog(string Log, string Pass)
        {
            sLogin.ForeColor = Color.Black;
            sPass.ForeColor = Color.Black;
            MsgErLogPass.Visible = false;

            try
            {
                string query = @"
                                BEGIN
                                SELECT [ID]
                                      ,[Login]
                                      ,[Password]
                                      ,[IsValid]
                                      ,[Firstname]
                                      ,[Name]
                                  FROM [dbo].[Users]
                                  WHERE
                                  Login like @Mail COLLATE French_BIN
                                  and Login is not null and Login <>''
                                  and Password like @Pass COLLATE French_BIN
                                  and Password is not null and Password <>''
                                END
                            ";


                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Mail", Log.Trim());
                command.Parameters.AddWithValue("@Pass", Pass.Trim());

                adapter.Fill(result);


                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
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
            catch (Exception ex)
            {
                return false;
            }

        }

        protected void RefreshCurrentPost()
        {
            Post.Value = "";
            ddlbCat1.SelectedIndex = 0;
            hiddenddlbCat1.Value = "";
            Login.Value = "";
            Pass.Value = "";
        }



        protected void AccessMyPosts(object sender, EventArgs e)
        {
            PnlAccessMyPosts.Visible = true;

            PnlEditPost.Visible = false;
            PnlManagePost.Visible = false;
            PnlGoodPost.Visible = false;
            PnlBadAfterConf.Visible = false;
            PnlValidMail.Visible = false;
            PnlAddPost.Visible = false;
            PnlViewSearch.Visible = false;
        }


        protected void ManagePosts(object sender, EventArgs e)
        {
            PnlManagePost.Visible = true;

            PnlEditPost.Visible = false;
            PnlAccessMyPosts.Visible = false;
            PnlGoodPost.Visible = false;
            PnlBadAfterConf.Visible = false;
            PnlValidMail.Visible = false;
            PnlAddPost.Visible = false;
            PnlViewSearch.Visible = false;

            if (isCorrectUserLog(LoginAccessMyPosts.Value.Trim(), PassAccessMyPosts.Value.Trim()) == true)
            {
                LoadUserPosts(isExitingUser(LoginAccessMyPosts.Value.Trim()));
            }

        }

        protected void LoadUserPosts(int user)
        {

            ShowPosts.Clear();

            try
            {


                string query = @"
                                BEGIN

                                SELECT 
                                Base.[ID]
                                ,Base.[Post]
                                ,Base.[Fk_User]
                                ,Cat.[Categorie]
                                ,Base.[CreatedDate]
                                FROM
                                (
                                SELECT 
                                [ID]
                                ,[Post]
                                ,[Fk_User]
                                ,[Fk_Categorie]
                                ,[CreatedDate]
                                FROM [dbo].[Posts]
                                Where Fk_User = @User
                                )  as Base
                                left join
                                [dbo].[Categories] as Cat
                                on
                                Base.Fk_Categorie = Cat.ID
                                order by Cat.[Categorie], Base.CreatedDate desc 

                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@User", user);

                adapter.Fill(result);

                foreach (DataRow reader in result.Tables[0].Rows)
                {
                    Post p = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                    ShowPosts.Add(p);
                    RptrMyPosts.DataBind();
                }

            }
            catch (Exception ex)
            {
            }

        }

        protected void EditPost(object sender, EventArgs e)
        {
            PnlEditPost.Visible = true;

            PnlManagePost.Visible = false;
            PnlAccessMyPosts.Visible = false;
            PnlGoodPost.Visible = false;
            PnlBadAfterConf.Visible = false;
            PnlValidMail.Visible = false;
            PnlAddPost.Visible = false;
            PnlViewSearch.Visible = false;

            LoadCategoriesGen(ddlbCat1EditPost);

            Button Btn = (Button)sender;
            Label lbl = (Label)Btn.FindControl("LblID");
            int Id = Int32.Parse(lbl.Text);


            Post result = ShowPosts.Find(
            delegate(Post pst)
            {
                return pst.Id == Id;
            }
            );


            PostEditPost.Value = result._Post;

            //ddlbCat1EditPost.SelectedValue = result.Categorie;
            ListItem lstItm = ddlbCat1EditPost.Items.FindByText(result.Categorie);
            ddlbCat1EditPost.SelectedIndex = Int32.Parse(lstItm.Value);
            hiddenIdEditPostUser.Text = result.User.ToString();
            hiddenIdEditPost.Text = result.Id.ToString();

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

                    string query = @"
                                BEGIN

                                BEGIN TRAN  
                                BEGIN TRY

                                UPDATE [dbo].[Posts]
                                   SET 
                                   [Post] = @Post
                                   ,[Fk_Categorie] = @Categorie
                                   ,[CreatedDate] = GETDATE()
                                 WHERE 
                                [Fk_User] = @User
                                AND
                                ID= @ID

                                COMMIT TRAN  
                                END TRY  
                                BEGIN CATCH  
                                ROLLBACK TRAN  
                                END CATCH  

                                END
                            ";

                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    command.CommandTimeout = 0;
                    DataSet result = new DataSet();
                    result.Locale = CultureInfo.InvariantCulture;

                    command.Parameters.AddWithValue("@Post", PostEditPost.Value.Trim());
                    command.Parameters.AddWithValue("@Categorie", ddlbCat1EditPost.SelectedValue.Trim());
                    command.Parameters.AddWithValue("@User", hiddenIdEditPostUser.Text.Trim());
                    command.Parameters.AddWithValue("@ID", hiddenIdEditPost.Text.Trim());
                    adapter.Fill(result);
                    ManagePosts(sender, e);
                }

            }
            catch (Exception ex)
            {
            }

        }
        protected bool deletePostFromDb(int PostId)
        {
            try
            {

                    string query = @"
                                BEGIN

                                BEGIN TRAN  
                                BEGIN TRY

                                DELETE FROM [dbo].[Posts]                               
                                WHERE 
                                [ID] = @ID

                                COMMIT TRAN  
                                END TRY  
                                BEGIN CATCH  
                                ROLLBACK TRAN  
                                END CATCH  

                                END
                            ";

                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);

                    command.CommandTimeout = 0;
                    DataSet result = new DataSet();
                    result.Locale = CultureInfo.InvariantCulture;

                    command.Parameters.AddWithValue("@ID", PostId);
                    adapter.Fill(result);
                    return true;

            }
            catch (Exception ex)
            {
                return false;
            }        
        }

        protected void DeletePost(object sender, EventArgs e)
        {

            Button Btn = (Button)sender;
            Label lbl = (Label)Btn.FindControl("LblID");
            int Id = Int32.Parse(lbl.Text);


            if (deletePostFromDb(Id))
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
    }
}