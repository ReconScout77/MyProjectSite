using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyProjectSite.Models;

namespace MyProjectSite.Controllers
{
    public class ProjectsController : Controller
    {
        public IActionResult Index()
        {
            var starredProjects = Project.GetStarredProjects();
            return View(starredProjects);
        }

        public IActionResult AllProjects()
        {
            var allProjects = Project.GetProjects();
            return View(allProjects);
        }
    }
}
