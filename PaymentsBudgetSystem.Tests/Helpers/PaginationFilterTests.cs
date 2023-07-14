namespace PaymentsBudgetSystem.Tests.Helpers
{
    using Core.Helpers;

    using static Common.DataConstants.General;

    [TestFixture]
    internal class PaginationFilterTests
    {
        [Test]
        public void FilterItemsByPage_ReturnsCorrectNumberOfElements()
        {
            int page = 2;

            var items = new List<object>()
            {
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
                new object(),
            };

            int itemsCount = items.Count;
            int itemsPerPage = ItemsPerPage;

            int itemsToSkip = (page - 1) * itemsPerPage;
            var expectedItemsCount = itemsCount - itemsToSkip;

            var paginationFilter = new PaginationFilter<object>();

            var result = paginationFilter.FilterItemsByPage(items, page);

            Assert.That(result.Count, Is.EqualTo(expectedItemsCount));
        }
    }
}
