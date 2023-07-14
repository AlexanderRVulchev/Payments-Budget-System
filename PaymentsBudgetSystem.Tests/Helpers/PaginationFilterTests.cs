namespace PaymentsBudgetSystem.Tests.Helpers
{
    using PaymentsBudgetSystem.Core.Helpers;
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

            var paginationFilter = new PaginationFilter<object>();

            var result = paginationFilter.FilterItemsByPage(items, page);

            Assert.That(result.Count, Is.EqualTo(items.Count - ItemsPerPage));
        }
    }
}
