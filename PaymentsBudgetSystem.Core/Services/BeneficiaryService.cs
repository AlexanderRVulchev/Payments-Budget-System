namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Beneficiaries;
    using Core.Models.Enums;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly PBSystemDbContext context;

        public BeneficiaryService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<AllBeneficiariesViewModel> GetAllBeneficiariesAsync(string userId, AllBeneficiariesViewModel model)
        {
            var beneficiaries = context
                .Beneficiaries
                .Where(b => b.UserId == userId)
                .AsQueryable();

            if (model.IdentifierFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Identifier == model.IdentifierFilter)
                    .AsQueryable();
            }
            if (model.NameFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Name == model.NameFilter)
                    .AsQueryable();
            }
            if (model.AddressFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Address == model.AddressFilter)
                    .AsQueryable();
            }

            if (model.SortBy == SortBy.Ascending)
            {
                switch (model.SortAttribute)
                {
                    case BeneficiarySort.Name:
                        beneficiaries = beneficiaries.OrderBy(b => b.Name); break;
                    case BeneficiarySort.Identifier:
                        beneficiaries = beneficiaries.OrderBy(b => b.Identifier); break;
                    case BeneficiarySort.Address:
                        beneficiaries = beneficiaries.OrderBy(b => b.Address); break;
                    default: break;
                }
            }
            else
            {
                switch (model.SortAttribute)
                {
                    case BeneficiarySort.Name:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.Name); break;
                    case BeneficiarySort.Identifier:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.Identifier); break;
                    case BeneficiarySort.Address:
                        beneficiaries = beneficiaries.OrderByDescending(b => b.Address); break;
                    default: break;
                }
            }

            model.Beneficiaries = await beneficiaries
                .Select(b => new BeneficiaryViewModel
                {
                    Name = b.Name,
                    Address = b.Address,
                    BeneficiaryId = b.Id,
                    Identifier = b.Identifier
                })
                .ToListAsync();
                        
            return model;
        }
    }
}
