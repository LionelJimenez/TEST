using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Description résumée de Post
/// </summary>
public class Post
{
    public int Id { get; set; }
    public string _Post { get; set; }
    public int User { get; set; }
    public string Categorie { get; set; }
    public DateTime CreatedDate { get; set; }

    public Post(int id, string post, int user, string categorie, DateTime createddate)
    {
        Id = id;
        _Post = post;
        User = user;
        Categorie = categorie;
        CreatedDate = createddate;
    }
}