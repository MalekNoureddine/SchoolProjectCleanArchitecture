using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchProject.Data.Common
{
    public class GeneralLocalizableEntity
    {
        public string GetLocalized(string textEn, string textAr)
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToString().ToLower().Equals("ar"))
                return textAr;
            return textEn;
        }
    }
}
