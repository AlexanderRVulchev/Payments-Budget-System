﻿using Microsoft.EntityFrameworkCore;

namespace PaymentsBudgetSystem.Core.Services
{
    using Contracts;
    using Models.Information;
    using Data;
    using Data.Entities.Enums;
    using Core.Helpers;

    using static Common.DataConstants.General;

    public class InformationService : IInformationService
    {
        private readonly PBSystemDbContext context;

        public InformationService(PBSystemDbContext _context)
        {
            context = _context;
        }

        public async Task<PaymentInformationViewModel> GetPaymentsInfoAsync(string userId, PaymentInformationViewModel model)
        {
            var payments = context
                .Payments
                .Where(p => p.UserId == userId)
                .Where(p => p.Date >= model.StartDate && p.Date <= model.EndDate)
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
              .AsQueryable();

            if (model.AmountMin != null)
            {
                payments = payments.Where(p => p.Amount >= model.AmountMin).AsQueryable();
            }
            if (model.AmountMax != null)
            {
                payments = payments.Where(p => p.Amount <= model.AmountMax).AsQueryable();
            }

            Sorter sorter = new();
            model.Payments = await sorter
                .SortInformationResults(payments, model)
                .ToListAsync();

            model.NumberOfPages = (model.Payments.Count / ItemsPerPage);

            if (model.Payments.Count % ItemsPerPage > 0)
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

            PaginationFilter<PaymentInformationItemModel> paginationFilter = new();
            model.Payments = paginationFilter
                .FilterItemsByPage(model.Payments, model.Page);

            return model;
        }
    }
}
