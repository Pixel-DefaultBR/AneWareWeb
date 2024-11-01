using AnewareApp.Data;
using AnewareApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnewareApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ReportController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("reports")]
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 10;

            if (page.GetType() != typeof(int) || page <= 0)
            {
                page = 1;
            }
            
            List<ReportModel> reports = await _dbContext.Reports
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalReports = await _dbContext.Reports.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalReports / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(reports);
        }
            
        [HttpPost]
        [Route("reports")]
        public async Task<IActionResult> GetReportBySearch(string search, string severity, int page = 1)
        {
            int pageSize = 10;

            var query = _dbContext.Reports.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r => r.Title.Contains(search)
                    || r.Description.Contains(search)
                    || r.User.Contains(search)
                );
            }

            if (!string.IsNullOrWhiteSpace(severity))
            {
                query = query.Where(r => r.Severity == severity);
            }

            int totalReports = await query.CountAsync();
            var reports = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling((double)totalReports/pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = search;
            ViewBag.SelectedSeverity = severity;

            return View("Index", reports);
        }
    }
}
