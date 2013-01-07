using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Runtime.Serialization;

namespace ServicesExchange
{
    [DataContract]
    public class Category
    {

        public static List<Category> ListCat;

        [DataMember]
        public string category { get; set; }

        [DataMember]
        public int id { get; set; }

        public Category(int Id, string Cat)
        {
            category = Cat;
            id = Id;
        }


        public static void LoadCategoriesGen(DropDownList DDL)
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

        public static int GetCategoryId(string Category)
        {

            try
            {
                string query = @"
                                BEGIN
                                    SELECT 
                                        [ID]
                                    FROM 
                                        [dbo].[Categories]
                                    WHERE 
                                        [Categorie] = @Cat
                                END
                            ";


                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Cat", Category);

                adapter.Fill(result);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(result.Tables[0].Rows[0]["ID"]);
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

        public static string GetCategoryValue(int CategoryId)
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
                                        [ID] = @Id
                                END
                            ";


                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db_SE"].ConnectionString);

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                command.CommandTimeout = 0;
                DataSet result = new DataSet();
                result.Locale = CultureInfo.InvariantCulture;

                command.Parameters.AddWithValue("@Id", CategoryId);

                adapter.Fill(result);

                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count > 0)
                {
                    return result.Tables[0].Rows[0]["Categorie"].ToString();
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



        public static List<Category> GetCategories()
        {
            ListCat = new List<Category>();

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
                    foreach (DataRow reader in result.Tables[0].Rows)
                    {
                        Category Cat = new Category(Convert.ToInt32(reader["ID"]), Convert.ToString(reader["Categorie"]));
                        ListCat.Add(Cat);
                    }

                    return ListCat;
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