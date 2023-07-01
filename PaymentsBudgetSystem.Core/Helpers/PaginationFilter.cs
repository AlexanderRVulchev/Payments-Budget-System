namespace PaymentsBudgetSystem.Core.Helpers
{
    using static Common.DataConstants.General;

    public class PaginationFilter<T> where T : class
    {
        public List<T> FilterItemsByPage(List<T> items, int page)
        {
            int itemsCount = items.Count;
            int itemsPerPage = ItemsPerPage;

            int itemsToSkip = (page - 1) * itemsPerPage;
            int itemsLeft = itemsCount - itemsToSkip;
            int itemsToTake = itemsLeft < itemsPerPage 
                ? itemsLeft
                : itemsPerPage;

            items = items.Skip(itemsToSkip).Take(itemsToTake).ToList();

            return items;
        }
    }
}
