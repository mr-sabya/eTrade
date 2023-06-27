using eTrade.Data.Base;
using eTrade.Data.ViewModel;
using eTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace eTrade.Data.Services.TestimonialService
{
    public class TestimonialsService : EntityBaseRepository<Testimonial>, ITestimonialsService
    {
        private readonly AppDbContext _context;
        public TestimonialsService(AppDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task UpdateTestimonialAsync(TestimonialEditViewModel data)
        {
            var testimonial = await _context.Testimonials.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (testimonial != null)
            {
                testimonial.Name = data.Name;
                testimonial.Company = data.Company;
                testimonial.Image = data.Image;
                testimonial.Feedback = data.Feedback;
                await _context.SaveChangesAsync();
            };
        }
    }
}
