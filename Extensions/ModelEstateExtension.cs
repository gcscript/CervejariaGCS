using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CervejariaGCS.Extensions
{
    public static class ModelEstateExtension
    {

        public static List<string> GetErros(this ModelStateDictionary modelState)
        {
            var result = new List<string>();
            foreach (var item in modelState.Values)
                result.AddRange(item.Errors.Select(error => error.ErrorMessage));

            return result;
        }
    }
}
