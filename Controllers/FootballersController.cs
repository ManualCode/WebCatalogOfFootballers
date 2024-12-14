using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCatalogOfFootballers.Data;
using WebCatalogOfFootballers.Models;

namespace WebCatalogOfFootballers.Controllers
{
    public class FootballersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public FootballersController(ApplicationDbContext dbContext) => this.dbContext = dbContext;

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["ListTeamNames"] = await dbContext.TeamNames
                .Select(x => x.NameTeam).Distinct().ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Footballer viewModel)
        {
            if (viewModel.Gender is null)
                ModelState.AddModelError("Gender.Name", "Необходимо выбрать пол");

            if (!ModelState.IsValid)
            {
                ViewData["ListTeamNames"] = await dbContext.TeamNames
                    .Select(x => x.NameTeam).Distinct().ToListAsync();
                return View("Add");
            }

            var g = await dbContext.Genders.FirstOrDefaultAsync(g => g.GenderName == viewModel.Gender.GenderName);
            var t = await dbContext.TeamNames.FirstOrDefaultAsync(g => g.NameTeam == viewModel.TeamName.NameTeam);
            var c = await dbContext.Countries.FirstOrDefaultAsync(g => g.CountryName == viewModel.Country.CountryName);

            await dbContext.Footballers.AddAsync(new Footballer()
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Birthday = viewModel.Birthday,
                CreatedDate = DateTime.UtcNow,
                Gender = g ?? viewModel.Gender,
                TeamName = t ?? viewModel.TeamName,
                Country = c ?? viewModel.Country
            });

            await dbContext.SaveChangesAsync();

            return Redirect("/");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var footballers = await dbContext.Footballers
                .Include(f => f.Gender).Include(f => f.TeamName).Include(f => f.Country)
                .ToListAsync();
            return View(footballers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewData["ListTeamNames"] = await dbContext.TeamNames
                .Select(x => x.NameTeam).Distinct().ToListAsync();

            var footballer = await dbContext.Footballers
                .Include(f => f.TeamName).Include(f => f.Country).Include(f => f.Gender)
                .FirstOrDefaultAsync(f => f.Id == id);

            return View(footballer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Footballer viewModel)
        {
            if (viewModel.Gender is null)
                ModelState.AddModelError("Gender.Name", "Необходимо выбрать пол");

            if (!ModelState.IsValid)
            {
                ViewData["ListTeamNames"] = await dbContext.TeamNames
                    .Select(x => x.NameTeam).Distinct().ToListAsync();
                return View(viewModel);
            }

            var footballer = await dbContext.Footballers
                .Include(f => f.TeamName).Include(f => f.Country).Include(f => f.Gender)
                .FirstOrDefaultAsync(f => f.Id == viewModel.Id);

            var temp = footballer.TeamName;
            var tempGender = await dbContext.Genders.FirstOrDefaultAsync(g => g.GenderName == viewModel.Gender.GenderName);
            var tempTeamName = await dbContext.TeamNames.FirstOrDefaultAsync(g => g.NameTeam == viewModel.TeamName.NameTeam);
            var tempCountry = await dbContext.Countries.FirstOrDefaultAsync(g => g.CountryName == viewModel.Country.CountryName);

            if (footballer is not null)
            {
                footballer.FirstName = viewModel.FirstName;
                footballer.LastName = viewModel.LastName;
                footballer.Birthday = viewModel.Birthday;
                footballer.CreatedDate = DateTime.UtcNow;
                footballer.Gender = tempGender ?? viewModel.Gender;
                footballer.TeamName = tempTeamName ?? viewModel.TeamName;
                footballer.Country = tempCountry ?? viewModel.Country;

                await dbContext.SaveChangesAsync();

                if (temp.NameTeam != viewModel.TeamName.NameTeam)
                {
                    var count = dbContext.Footballers.Select(x => x.TeamName == temp).Count(x => x == true);
                    if (count == 0)
                    {
                        var deletedTeamName = await dbContext.TeamNames
                            .FirstOrDefaultAsync(x => x.NameTeam == temp.NameTeam);
                        dbContext.TeamNames.Remove(deletedTeamName);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Footballers");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Footballer viewModel)
        {
            var footballer = await dbContext.Footballers.AsNoTracking()
                .Include(f => f.TeamName).Include(f => f.Country).Include(f => f.Gender)
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);

            if (footballer is not null)
            {
                dbContext.Footballers.Remove(footballer);

                await dbContext.SaveChangesAsync();

                var count = dbContext.Footballers.Select(x => x.TeamName == footballer.TeamName).Count(x => x == true);
                if (count == 0)
                {
                    var team = await dbContext.TeamNames.FirstOrDefaultAsync(x => x.NameTeam == footballer.TeamName.NameTeam);
                    dbContext.TeamNames.Remove(team);
                    await dbContext.SaveChangesAsync();
                }
            }
            return RedirectToAction("List", "Footballers");
        }
    }
}

