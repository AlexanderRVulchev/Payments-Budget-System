
namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Microsoft.EntityFrameworkCore;
    using Models.Information;
    using Data;
    using Data.Entities.Enums;

    public class InformationService : IInformationService
    {
        private readonly PBSystemDbContext context;

        public InformationService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<List<PaymentInformationItemModel>> GetPaymentsInfoAsync(string userId, DateTime from, DateTime to)
        {
            var payments = await context
                .Payments
                .Where(p => p.UserId == userId)
                .Where(p => p.Date >= from && p.Date <= to)
                .Include(p => p.SupportDetails)
                .Include(p => p.SalariesDetails)
                .Include(p => p.AssetsDetails)
                .Include(p => p.CashDetails)
                .Select(p => new PaymentInformationItemModel
                {
                    Amount = p.Amount,
                    Date = p.Date,
                    Description = p.Description,
                    ParagraphType = p.Paragraph,
                    PaymentType = p.PaymentType,
                    PaymentId = p.Id,                                        
                    ReceiverName = p.ReceiverName
                })
                .ToListAsync();

            return payments;
        }
    }
}
