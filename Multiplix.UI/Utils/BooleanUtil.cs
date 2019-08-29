using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Multiplix.UI.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class BooleanUtil : Attribute
    {
        public static List<SelectListItem> SimNaoSelect()
        {
            return new List<SelectListItem>()
            {
                _(true, "Sim"),
                _(false, "Não"),
            };
        }
        private static SelectListItem _(bool valor, string titulo)
        {
            return new SelectListItem() {Text = titulo, Value = valor.ToString()};
        }

        public static string AplicarFormatacao(bool result)
        {
            return result ? "<span class=\"badge badge-pill badge-info mr-1 mb-1 mt-1\">Sim</span>" : "<span class=\"badge badge-pill badge-danger mr-1 mb-1 mt-1\">Não</span>";
        }
    }
}
