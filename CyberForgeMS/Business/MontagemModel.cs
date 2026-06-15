using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CyberForgeMS.Business;

public class MontagemModel : PageModel
{
    [BindProperty]
    public int CurrentIndex { get; set; } = 0;
    
    public List<string> Images { get; set; } = [];
    public string CurrentImage {get;set;}
    public int TotalImages {get;set;}

    public void OnGet()
    {
        // Initialize your images list
        Images = new List<string>
        {
            "images/p1.webp",
            "images/p2.webp",
            "images/p3.webp",
            "images/p4.webp",
            "images/p5.webp",
            "images/p6.webp",
            "images/p7.webp",
            "images/p8.webp"
            // Add more image paths
        };
        CurrentImage = Images[CurrentIndex];
        TotalImages = Images.Count;
    }

    public IActionResult OnPostPrevious()
    {
        Images = GetImages(); // Reload images
        CurrentIndex = (CurrentIndex - 1 + Images.Count) % Images.Count;
        return Page();
    }

    public IActionResult OnPostNext()
    {
        Images = GetImages(); // Reload images
        CurrentIndex = (CurrentIndex + 1) % Images.Count;
        return Page();
    }

    private List<string> GetImages()
    {
        // Method to get your images list
        return new List<string>
        {
            "images/p1.webp",
            "images/p2.webp",
            "images/p3.webp",
            "images/p4.webp",
            "images/p5.webp",
            "images/p6.webp",
            "images/p7.webp",
            "images/p8.webp"
            // Add more image paths
        };
    }
}