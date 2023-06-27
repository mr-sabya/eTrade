using eTrade.Data.Base;
using eTrade.Data.ViewModel;
using eTrade.Models;

namespace eTrade.Data.Services.TestimonialService
{
    public interface ITestimonialsService : IEntityBaseRepository<Testimonial>
    {
        Task UpdateTestimonialAsync(TestimonialEditViewModel data);
    }
}
