using System.ComponentModel.DataAnnotations;

namespace ODTUDersSecim.Helpers
{
    public class ZorunluAlanAttribute : RequiredAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                int num;
                if (value != null)
                {
                    string text = value as string;
                    num = ((text != null && string.IsNullOrWhiteSpace(text)) ? 1 : 0);
                }
                else
                {
                    num = 1;
                }

                if (num == 0)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(string.Format("{0} alanı {1}{2}karakter olmalıdır.", YardimciMetotlar.ParametreAdiGetir(validationContext)));
            }
            catch (Exception ex)
            {
                return new ValidationResult(string.Format($"Modelin doğrulanması sırasında istisnai durum oluştu", ex.Message));
            }
        }
    }
}
