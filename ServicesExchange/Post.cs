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

        public static List<Post> ListPosts;

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


        public static List<Post> getLatestPosts()
        {
            ListPosts = new List<Post>();

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

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow reader in result.Tables[0].Rows)
                    {
                        Post P = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                        ListPosts.Add(P);
                    }

                    return ListPosts;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public static List<Post> getPostsByMC(string MC)
        {
            ListPosts = new List<Post>();

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

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow reader in result.Tables[0].Rows)
                    {
                        Post P = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                        ListPosts.Add(P);
                    }

                    return ListPosts;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static List<Post> getPostsByCat(int Cat)
        {
            ListPosts = new List<Post>();

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

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow reader in result.Tables[0].Rows)
                    {
                        Post P = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                        ListPosts.Add(P);
                    }

                    return ListPosts;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public static List<Post> getPostsByCat_MC(string MC, int Cat)
        {
            ListPosts = new List<Post>();

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

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow reader in result.Tables[0].Rows)
                    {
                        Post P = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                        ListPosts.Add(P);
                    }
                    return ListPosts;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static int AddNewPostQuery(int user, int category, string PostTxt)
        {
            int NewPost;

            try
            {

                if (user == 0 || category == 0 || PostTxt.Trim() == "" || PostTxt == null)
                {
                    return 0;
                }
                else
                {
                    string query = string.Format(@"
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

                                Select @@Identity;

                                COMMIT TRAN  
                                END TRY  
                                BEGIN CATCH  
                                ROLLBACK TRAN  
                                END CATCH  

                                END
                                        ");


                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.CommandTimeout = 0;
                        command.Parameters.AddWithValue("@Cat", category);
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@post", PostTxt.Trim());
                        connection.Open();

                        NewPost = Convert.ToInt32(command.ExecuteScalar());
                    }

                    return NewPost;
                }

            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public static List<Post> LoadUserPostsQuery(int user)
        {
            ListPosts = new List<Post>();

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

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow reader in result.Tables[0].Rows)
                    {
                        Post P = new Post(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Post"]), Convert.ToInt32(reader["Fk_User"]), Convert.ToString(reader["Categorie"]), Convert.ToDateTime(reader["CreatedDate"]));
                        ListPosts.Add(P);
                    }

                    return ListPosts;
                }
                else
                {
                    return null;
                }

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
                if (PostId == 0)
                {
                    return false;
                }
                else
                {
                    // il faut que ce post existe pour l'updater
                    if (GetPostById(PostId) != null)
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

                        if (result != null)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

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
                if (user == 0 || category == 0 || postId == 0 || post == "" || post == null)
                {
                    return 0;
                }
                else
                {
                    // il faut que ce post existe pour l'updater
                    if (GetPostById(postId) != null)
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
                    else
                    {
                        return 0;
                    }

                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static Post GetPostById(int postId)
        {
            try
            {
                string query = @"
                                BEGIN
                                    SELECT 
                                    [ID]
                                    ,[Post]
                                    ,[Fk_User]
                                    ,[Fk_Categorie]
                                    ,[CreatedDate]
                                    FROM 
                                    [dbo].[Posts]
                                    where [ID] = @postId
                                END
                            ";

                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@postId", postId);


                adapter.Fill(result);

                if (result != null && result.Tables[0].Rows.Count > 0)
                {
                    Post P = new Post(Convert.ToInt32(result.Tables[0].Rows[0]["ID"]), Convert.ToString(result.Tables[0].Rows[0]["Post"]), Convert.ToInt32(result.Tables[0].Rows[0]["Fk_User"]), Convert.ToString(result.Tables[0].Rows[0]["Fk_Categorie"]), Convert.ToDateTime(result.Tables[0].Rows[0]["CreatedDate"]));
                    return P;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}