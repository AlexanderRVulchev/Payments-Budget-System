using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Data;
    using Data.Entities;
    using Helpers;
    using Models.Beneficiaries;

    using static Common.DataConstants.General;
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
                .AnyAsync(b => (b.Name == model.Name && b.UserId == userId)
                    || (b.Identifier == model.Identifier && b.UserId == userId)))
            {
                throw new InvalidOperationException(BeneficiaryAlreadyExists);
            }

            var newBeneficiaryEntry = new Beneficiary
            {
                Name = model.Name,
                Identifier = model.Identifier,
                Address = model.Address,
                UserId = userId,
                BankAccount = model.BankAccount
            };

            await context.Beneficiaries.AddAsync(newBeneficiaryEntry);
            await context.SaveChangesAsync();
        }


        public async Task EditBeneficiaryAsync(string userId, BeneficiaryFormModel model)
        {
            var beneficiary = await context
                .Beneficiaries
                .FindAsync(model.Id);

            if (beneficiary == null)
            {
                throw new InvalidOperationException(BeneficiaryDoesNotExist);
            }
            if (beneficiary.UserId != userId)
            {
                throw new InvalidOperationException(BeneficiaryAccessDenied);
            }

            if (beneficiary.Name != model.Name)
            {
                var payments = await context
                .Payments
                .Where(p => p.ReceiverName == beneficiary.Name)
                .ToArrayAsync();

                foreach (var payment in payments)
                {
                    payment.ReceiverName = model.Name;
                };
            }           

            beneficiary.Address = model.Address;
            beneficiary.Identifier = model.Identifier;
            beneficiary.Name = model.Name;
            beneficiary.BankAccount = model.BankAccount;

            await context.SaveChangesAsync();
        }


        public async Task<AllBeneficiariesViewModel> GetAllBeneficiariesAsync(string userId, AllBeneficiariesViewModel model)
        {
            var beneficiaries = context
                .Beneficiaries
                .Where(b => b.UserId == userId)
                .AsQueryable();

            Sorter sorter = new();

            beneficiaries = sorter.SortBeneficiaries(beneficiaries, model);

            model.Beneficiaries = await beneficiaries
                .Select(b => new BeneficiaryViewModel
                {
                    Name = b.Name,
                    Address = b.Address,
                    BeneficiaryId = b.Id,
                    Identifier = b.Identifier,
                    BankAccount = b.BankAccount
                })
                .ToListAsync();

            model.NumberOfPages = model.Beneficiaries.Count / ItemsPerPage;

            if (model.Beneficiaries.Count % ItemsPerPage > 0)
            {
                model.NumberOfPages++;
            }

            if (model.Page < 1)
            {
                model.Page = 1;
            }
            else if (model.Page > model.NumberOfPages)
            {
                model.Page = model.NumberOfPages;
            }

            PaginationFilter<BeneficiaryViewModel> paginationFilter = new();

            model.Beneficiaries = paginationFilter.FilterItemsByPage(model.Beneficiaries, model.Page);

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
                Identifier = entity.Identifier,
                BankAccount = entity.BankAccount
            };
        }
    }
}
