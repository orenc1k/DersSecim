using System.ComponentModel.DataAnnotations;
using System.Linq.Dynamic.Core;

namespace ODTUDersSecim.Helpers
{
    public abstract class DogrulamaAttribute : ValidationAttribute
    {
        /// <summary>
        /// String olarak lambda ifade yazdığımızı düşünelim. 
        /// Örneğin, `Alan1 != null && Alan2 == null && Alan3 == null && Alan4 == "ÖrnekTip" `
        /// Bu ifadenin bir obje için gereçerli olup olmadığını kontrol etmek için bu methodu kullanabiliriz.
        /// </summary>
        /// <param name="ifade"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected static bool IfadeGecerliMi(string ifade, ValidationContext validationContext)
        {
            try
            {
                if (validationContext.ObjectInstance == null)
                {
                    return true;
                }
                var lambdaIfade = DynamicExpressionParser.ParseLambda(validationContext.ObjectType, typeof(bool), ifade);
                var function = lambdaIfade.Compile();
                return ((bool)function.DynamicInvoke(validationContext.ObjectInstance)!);
            }
            catch (Exception e)
            {
                throw new Exception(nameof(IfadeGecerliMi), e);
            }
        }
    }
}
