using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Configuration;
using System.Runtime.Serialization;

namespace ServicesExchange
{
    [DataContract]
    public class Post
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string _Post { get; set; }

        [DataMember]
        public int User { get; set; }

        [DataMember]
        public string Categorie { get; set; }

        [DataMember]
        public DateTime CreatedDate { get; set; }


        public Post(int id, string post, int user, string categorie, DateTime createddate)
        {
            Id = id;
            _Post = post;
            User = user;
            Categorie = categorie;
            CreatedDate = createddate;
        }


        public static DataSet getLatestPosts()
        {

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


                return result;

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static DataSet getPostsByMC(string MC)
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

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DataSet getPostsByCat(string Cat)
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

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static DataSet getPostsByCat_MC(string MC, string Cat)
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

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static void AddNewPostQuery(int user, string category, string PostTxt)
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
                command.Parameters.AddWithValue("@post", PostTxt.Trim());

                adapter.Fill(result);

            }
            catch (Exception ex)
            {
            }
        }

        public static DataSet LoadUserPostsQuery(int user)
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

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static bool deletePostFromDb(int PostId)
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

        public static int UpdatePost(string post, int category, int user, int postId)
        {

            try
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

                command.Parameters.AddWithValue("@Post", post);
                command.Parameters.AddWithValue("@Categorie", category);
                command.Parameters.AddWithValue("@User", user);
                command.Parameters.AddWithValue("@ID", postId);
                adapter.Fill(result);

                if (result != null)
                {
                    return postId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


    }
}