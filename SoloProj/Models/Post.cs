#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace SoloProj.Models;

public class Post
{
    [Key]
    public int PostId { get; set; }


    [Required]
    public string Img { get; set; }


    [Required]
    public string Name { get; set; }


    [Required]
    public string Gender { get; set; }

    [Required]
    public string Breed { get; set; }

    [Required]
    public string Location { get; set; }


    [Required]
    public string Bio { get; set; }


    public bool Monday { get; set; } = false;


    public bool Tuesday { get; set; } = false;


    public bool Wednesday { get; set; } = false;


    public bool Thursday { get; set; } = false;


    public bool Friday { get; set; } = false;


    public bool Saturday { get; set; } = false;


    public bool Sunday { get; set; } = false;


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    // ****** One to Many
    public int UserId { get; set; }
    public User? Creator { get; set; }

    public List<UserLike> PupLike { get; set; } = new List<UserLike>();
}