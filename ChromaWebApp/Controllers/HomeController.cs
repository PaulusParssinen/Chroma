﻿using Chroma;
using ChromaWebApp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChromaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            bool isSmallFurni = false;
            int renderState = 0;
            int renderDirection = 0;
            int color = 0;
            string sprite = null;

            if (Request.Query.ContainsKey("sprite"))
            {
                Request.Query.TryGetValue("sprite", out var value);
                sprite = value.ToString();
            }

            if (Request.Query.ContainsKey("s"))
            {
                Request.Query.TryGetValue("s", out var value);

                if (value == "1" || value == "true")
                {
                    isSmallFurni = true;
                }
            }

            if (Request.Query.ContainsKey("small"))
            {
                Request.Query.TryGetValue("small", out var value);

                if (value == "1" || value == "true")
                {
                    isSmallFurni = true;
                }
            }

            if (Request.Query.ContainsKey("state"))
            {
                Request.Query.TryGetValue("state", out var value);

                if (value.ToString().IsNumeric())
                {
                    renderState = int.Parse(value.ToString());
                }
            }

            if (Request.Query.ContainsKey("direction"))
            {
                Request.Query.TryGetValue("direction", out var value);

                if (value.ToString().IsNumeric())
                {
                    renderDirection = int.Parse(value.ToString());
                }
            }

            if (Request.Query.ContainsKey("rotation"))
            {
                Request.Query.TryGetValue("rotation", out var value);

                if (value.ToString().IsNumeric())
                {
                    renderDirection = int.Parse(value.ToString());
                }
            }

            if (Request.Query.ContainsKey("color"))
            {
                Request.Query.TryGetValue("color", out var value);

                if (value.ToString().IsNumeric())
                {
                    color = int.Parse(value.ToString());
                }
            }

            if (Request.Query.ContainsKey("colour"))
            {
                Request.Query.TryGetValue("colour", out var value);

                if (value.ToString().IsNumeric())
                {
                    color = int.Parse(value.ToString());
                }
            }

            if (sprite != null && sprite.Length > 0)
            {
                var furni = new ChromaFurniture("swfs/hof_furni/" + sprite + ".swf", isSmallFurni: isSmallFurni, renderState: renderState, renderDirection: renderDirection, colourId: color);
                furni.Run();

                return File(furni.CreateImage(), "image/png");
            }

            return null;
        }
    }
}
