using GymManagementSystem.Data; 
using GymManagementSystem.Models; 
using GymManagementSystem.Models.Entities; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Controllers
{
    public class PackagesController : Controller
    {
        // Dependency injection of the ApplicationDbContext to interact with the database
        private readonly ApplicationDbContext dbContext;

        // Constructor to initialize the dbContext
        public PackagesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Display the form to add a new package
        [HttpGet]
        public IActionResult AddPackages()
        {
            return View();
        }

        // POST: Handle the form submission to add a new package
        [HttpPost]
        public async Task<IActionResult> AddPackages(AddPackageViewModel viewModel)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Create a new Package entity from the view model data
                var package = new Package
                {
                    Type = viewModel.Type,
                    Duration = viewModel.Duration,
                    fee = viewModel.fee,
                };

                // Add the new package to the database
                await dbContext.Packages.AddAsync(package);
                // Save changes to the database
                await dbContext.SaveChangesAsync();
                // Return the same view to show the form again (could be improved to show a success message or redirect)
                return View();
            }
            // If model state is not valid, return the view with the same view model to show validation errors
            return View(viewModel);
        }

        // GET: Retrieve and display a list of all packages
        [HttpGet]
        public async Task<IActionResult> PackagesList()
        {
            // Get the list of all packages from the database
            var packages = await dbContext.Packages.ToListAsync();
            // Pass the list of packages to the view
            return View(packages);
        }

        // GET: Display the form to edit an existing package
        [HttpGet]
        public async Task<IActionResult> PackageEdit(Guid id)
        {
            // Find the package by ID in the database
            var package = await dbContext.Packages.FindAsync(id);
            // Pass the package to the view
            return View(package);
        }

        // POST: Handle the form submission to update an existing package
        [HttpPost]
        public async Task<IActionResult> PackageEdit(Package viewModel)
        {
            // Find the package by ID in the database
            var package = await dbContext.Packages.FindAsync(viewModel.Id);

            // If the package exists
            if (package is not null)
            {
                // Update the package properties with the data from the view model
                package.Type = viewModel.Type;
                package.Duration = viewModel.Duration;
                package.fee = viewModel.fee;

                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }
            // Redirect to the list of packages after editing
            return RedirectToAction("PackagesList", "Packages");
        }

        // POST: Handle the request to delete a package
        [HttpPost]
        public async Task<IActionResult> PackageDelete(Package viewModel)
        {
            // Find the package by ID in the database
            var package = await dbContext.Packages.FindAsync(viewModel.Id);

            // If the package exists
            if (package is not null)
            {
                // Remove the package from the database
                dbContext.Packages.Remove(package);
                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }
            // Redirect to the list of packages after deletion
            return RedirectToAction("PackagesList", "Packages");
        }
    }
}
