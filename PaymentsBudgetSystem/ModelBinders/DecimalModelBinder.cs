using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace PaymentsBudgetSystem.ModelBinders
{
    public class DecimalModelBinder : IModelBinder
    {
        private readonly ModelMetadata _metadata;

        public DecimalModelBinder(ModelMetadata modelMetadata)
        {
            _metadata = modelMetadata;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ValueProviderResult result = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            decimal? actualValue = null;
            bool success = false;

            if (result != ValueProviderResult.None && !string.IsNullOrEmpty(result.FirstValue))
            {
                try
                {
                    string decimalValue = result.FirstValue;
                    decimalValue = decimalValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    decimalValue = decimalValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    actualValue = Convert.ToDecimal(decimalValue, CultureInfo.CurrentCulture);
                    success = true;
                }
                catch (Exception e)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, e, bindingContext.ModelMetadata);
                }
            }
            else
            {
                if (_metadata.ModelType == typeof(decimal))
                {
                    success = true;
                }
            }

            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(actualValue);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
