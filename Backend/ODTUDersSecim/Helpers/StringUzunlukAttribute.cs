using System.ComponentModel.DataAnnotations;

namespace ODTUDersSecim.Helpers
{
    public class StringUzunlukAttribute : DogrulamaAttribute
    {
        private readonly int enCok;

        private readonly int enAz;

        public StringUzunlukAttribute(int enAz = -1, int enCok = -1)
        {
            this.enCok = enCok;
            this.enAz = enAz;
        }

        public StringUzunlukAttribute(int enCok = -1)
        {
            this.enCok = enCok;
            enAz = -1;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                if (value == null || value!.ToString() == "")
                {
                    return ValidationResult.Success;
                }

                if (enAz == enCok && enAz != -1)
                {
                    if (((string)value).Length == enAz)
                    {
                        return ValidationResult.Success;
                    }

                    if (((string)value).Length != enAz)
                    {
                        return new ValidationResult(string.Format("{0} alanı {1}{2}karakter olmalıdır.", YardimciMetotlar.ParametreAdiGetir(validationContext), enAz.ToString(), " "));
                    }
                }

                bool flag = enCok == -1 || ((string)value).Length <= enCok;
                bool flag2 = enAz == -1 || ((string)value).Length >= enAz;
                if (flag && flag2)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult(string.Format("{0} alanı {1}{2}karakter olmalıdır.", YardimciMetotlar.ParametreAdiGetir(validationContext), flag ? "" : ("en fazla " + enCok + " "), flag2 ? "" : ("en az " + enAz + " ")));
            }
            catch (Exception ex)
            {
                return new ValidationResult(string.Format("Modelin doğrulanması sırasında istisnai durum oluştu. Hata mesajı: {0}", ex.Message));
            }
        }
    }
}
