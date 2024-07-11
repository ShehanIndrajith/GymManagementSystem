using GymManagementSystem.Data; 
using GymManagementSystem.Models; 
using GymManagementSystem.Models.Entities; 
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 

namespace GymManagementSystem.Controllers
{
    public class MembersController : Controller
    {
        // Dependency injection of the ApplicationDbContext to interact with the database
        private readonly ApplicationDbContext dbContext;
        public MembersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: Display the form to add a new member
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // POST: Handle the form submission to add a new member
        [HttpPost]
        public async Task<IActionResult> Add(AddMemberViewModel viewModel)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Create a new Member entity from the view model data
                var member = new Member
                {
                    Name = viewModel.Name,
                    Birthday = viewModel.Birthday,
                    TelNO = viewModel.TelNO,
                    JoinDate = viewModel.JoinDate
                };

                // Add the new member to the database
                await dbContext.Members.AddAsync(member);
                await dbContext.SaveChangesAsync();
                return View();
            }
            return View(viewModel);
        }

        // GET: Retrieve and display a list of all members
        [HttpGet]
        public async Task<IActionResult> List()
        {
            // Get the list of all members from the database
            var members = await dbContext.Members.ToListAsync();
            // Pass the list of members to the view
            return View(members);
        }

        // GET: Display the form to edit an existing member
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // Find the member by ID in the database
            var member = await dbContext.Members.FindAsync(id);
            // Pass the member to the view
            return View(member);
        }

        // POST: Handle the form submission to update an existing member
        [HttpPost]
        public async Task<IActionResult> Edit(Member viewModel)
        {
            // Find the member by ID in the database
            var member = await dbContext.Members.FindAsync(viewModel.Id);

            // If the member exists
            if (member is not null)
            {
                // Update the member properties with the data from the view model
                member.Name = viewModel.Name;
                member.Birthday = viewModel.Birthday;
                member.TelNO = viewModel.TelNO;
                member.JoinDate = viewModel.JoinDate;

                // Save changes to the database
                await dbContext.SaveChangesAsync();
            }
            // Redirect to the list of members after editing
            return RedirectToAction("List", "Members");
        }
    }
}
