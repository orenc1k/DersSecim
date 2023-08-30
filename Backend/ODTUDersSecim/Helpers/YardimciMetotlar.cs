using System.ComponentModel.DataAnnotations;

namespace ODTUDersSecim.Helpers
{
    internal static class YardimciMetotlar
    {
        public static string ParametreAdiGetir(ValidationContext validationContext)
        {
            var tipUzunAdi = validationContext.ObjectInstance.GetType().ToString();
            var tipAdiSiplitArray = tipUzunAdi.Split(".");
            var tipAdi = tipAdiSiplitArray.LastOrDefault();
            return tipAdi + "." + validationContext.MemberName;
        }

        public static object PropertyAdindanDegerGetir(ValidationContext validationContext, string propertyName)
            => validationContext?.ObjectInstance.GetType().GetProperty(propertyName)?.GetValue(validationContext.ObjectInstance, null)!;
    }
}
