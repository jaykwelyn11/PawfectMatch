using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SoloProj.Models;

namespace SoloProj.Controllers;

public class PostController : Controller
{

    private MyContext db;

    public PostController(MyContext context)
    {
        db = context;
    }
    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("uid");
        }
    }


    [SessionCheck]
    [HttpGet("/dashboard")]
    public IActionResult Dashboard()
    {
        List<Post> allPosts = db.Posts
        .Include(p => p.Creator)
        .Include(p => p.PupLike)
        .ThenInclude(like => like.User)
        .OrderByDescending(p => p.CreatedAt)
        .ToList();

        // ViewBag.PostLike = PostLike;

        return View("Dashboard", allPosts);
    }


    [SessionCheck]
    [HttpGet("/dashboard/{postId}")]
    public IActionResult ViewPup(int postId)
    {
        Post? item = db.Posts
        .Include(item => item.Creator)
        .FirstOrDefault(item => item.PostId == postId);
        if (item == null)
        {
            return RedirectToAction("Dashboard");
        }
        else
        {
            // ViewBag.PostLike = postId;
            return View("ViewPup", item);
        }
    }


    [SessionCheck]
    [HttpGet("/dashboard/addPup")]
    public IActionResult AddPup()
    {
        return View("AddPup");
    }


    [HttpPost("/dashboard/createPup")]
    public IActionResult CreatePup(Post p)
    {
        p.UserId = (int)HttpContext.Session.GetInt32("uid");
        if (ModelState.IsValid)
        {
            db.Posts.Add(p);
            db.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        return View("AddPup");
    }

    [SessionCheck]
    [HttpGet("/dashboard/edit/{postId}")]
    public IActionResult EditPup(int postId)
    {
        Post? item = db.Posts
        .Include(item => item.Creator)
        .FirstOrDefault(p => p.PostId == postId);

        if (item == null || item.UserId != HttpContext.Session.GetInt32("uid"))
        {
            return RedirectToAction("Dashboard");
        }
        else
        {
            return View("EditPup", item);
        }
    }


    [SessionCheck]
    [HttpPost("/dashboard/updatePup/{postId}")]
    public IActionResult UpdatePup(Post p, int postId)
    {
        if (!ModelState.IsValid)
        {
            return EditPup(postId);
        }
        Post? item = db.Posts.FirstOrDefault(item => item.PostId == postId);
        if (item == null || item.UserId != HttpContext.Session.GetInt32("uid"))
        {
            return RedirectToAction("Dashboard");
        }
        else
        {
            item.Img = p.Img;
            item.Name = p.Name;
            item.Gender = p.Gender;
            item.Breed = p.Breed;
            item.Location = p.Location;
            item.Bio = p.Bio;
            item.Monday = p.Monday;
            item.Tuesday = p.Tuesday;
            item.Wednesday = p.Wednesday;
            item.Thursday = p.Thursday;
            item.Friday = p.Friday;
            item.Saturday = p.Saturday;
            item.Sunday = p.Sunday;
            item.UpdatedAt = DateTime.Now;

            db.Posts.Update(item);
            db.SaveChanges();

            return RedirectToAction("ViewPup", new { postId = postId });
        }
    }

    [SessionCheck]
    [HttpGet("/dashboard/delete/{postId}")]
    public IActionResult DeletePup(int postId)
    {
        Post? item = db.Posts.FirstOrDefault(item => item.PostId == postId);
        if (item != null)
        {
            db.Posts.Remove(item);
            db.SaveChanges();
        }
        return RedirectToAction("Dashboard");
    }


    [SessionCheck]
    [HttpPost("/dashboard/{postId}/like")]
    public IActionResult Like(int postId)
    {
        UserLike? existingLike = db.UserLikes
        .FirstOrDefault(like => like.UserId == (int)HttpContext.Session.GetInt32("uid") && like.PostId == postId);

        if (existingLike != null)
        {
            db.UserLikes.Remove(existingLike);
        }
        else
        {
            UserLike newLike = new UserLike()
            {
                PostId = postId,
                UserId = (int)HttpContext.Session.GetInt32("uid")
            };

            db.UserLikes.Add(newLike);
        }

        db.SaveChanges();

        return RedirectToAction("Dashboard");
    }
}