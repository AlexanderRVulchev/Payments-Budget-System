using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Core.Contracts;
    using Core.Models.Beneficiaries;
    using Core.Models.Enums;
    using Data;
    using Data.Entities;

    using static Common.ExceptionMessages.Beneficiary;

    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly PBSystemDbContext context;

        public BeneficiaryService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task AddBeneficiaryAsync(string userId, BeneficiaryFormModel model)
        {
            if (await context.Beneficiaries
                .AnyAsync(b => b.Name == model.Name || b.Identifier == model.Identifier))
            {
                throw new InvalidOperationException(BeneficiaryAlreadyExists);
            }

            var entry = new Beneficiary
            {
                Name = model.Name,
                Identifier = model.Identifier,
                Address = model.Address,
                UserId = userId
            };

            await context.Beneficiaries.AddAsync(entry);
            await context.SaveChangesAsync();
        }

        public async Task EditBeneficiary(string userId, BeneficiaryFormModel model)
        {
            var entity = await context
                .Beneficiaries
                .FindAsync(model.Id);

            if (entity == null)
            {
                throw new InvalidOperationException(BeneficiaryDoesNotExist);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(BeneficiaryAccessDenied);
            }

            entity.Address = model.Address;
            entity.Identifier = model.Identifier;
            entity.Name = model.Name;

            await context.SaveChangesAsync();
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
                    .Where(b => b.Identifier.Contains(model.IdentifierFilter.ToLower()))
                    .AsQueryable();
            }
            if (model.NameFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Name.Contains(model.NameFilter.ToLower()))
                    .AsQueryable();
            }
            if (model.AddressFilter != null)
            {
                beneficiaries = beneficiaries
                    .Where(b => b.Address != null
                            && b.Address.Contains(model.AddressFilter.ToLower()))
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

        public async Task<BeneficiaryFormModel> GetBeneficiaryAsync(string userId, Guid beneficiaryId)
        {
            var entity = await context
                .Beneficiaries
                .FindAsync(beneficiaryId);
                    
            if (entity == null)
            {
                throw new InvalidOperationException(BeneficiaryDoesNotExist);
            }
            if (entity.UserId != userId)
            {
                throw new InvalidOperationException(BeneficiaryAccessDenied);
            }

            return new BeneficiaryFormModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Address = entity.Address,
                Identifier = entity.Identifier
            };
        }
    }
}
