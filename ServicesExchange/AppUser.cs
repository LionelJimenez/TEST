using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.Serialization;

namespace ServicesExchange
{
    [DataContract]
    public class AppUser
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Pass { get; set; }

        public AppUser(string login, string pass, int id)
        {
            Id = id;            
            Login = login;
            Pass = pass;
        }

        public static bool isExitingUser(string Log)
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
                    //return Convert.ToInt32(result.Tables[0].Rows[0]["ID"]);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool isValidUser(string Log)
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


        public static bool isCorrectUserLogQuery(string Log, string Pass)
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

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static int GetUserId(string Log, string Pass)
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
                    return Int32.Parse(result.Tables[0].Rows[0]["ID"].ToString());
                }

                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public static AppUser GetUser(string Log, string Pass)
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

                    AppUser Usr = new AppUser(Log.Trim(), Pass.Trim(), Convert.ToInt32(result.Tables[0].Rows[0]["ID"]));
                    return Usr;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static bool SendMailForValidation(string MailTo, string Pass)
        {
            try
            {

                // J'envoie un mail

                MailMessage mail = new MailMessage();
                mail.To.Add(MailTo);

                mail.From = new MailAddress("lionel.jimenez.netdevelopper@gmail.com");
                mail.Subject = "SE - Mail de validation";

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
                            + "<a href=http://servicesexchange.apphb.com/AddPost.aspx?Lgn=" + MailTo + "&Ps=" + Pass + ">Lien de validation</a>"
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

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static int CreateNewUser(string Login, string Pass)
        {
            int NewUser;

            try
            {
                if (Login == "" || Login == null || Pass == "" || Pass == null)
                {
                    return 0;
                }
                else
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

            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public static bool ValidateUser(string Login, string Pass)
        {
            try
            {
                if (GetUserId(Login, Pass) != 0)
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

                    return true;
                }
                else 
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}