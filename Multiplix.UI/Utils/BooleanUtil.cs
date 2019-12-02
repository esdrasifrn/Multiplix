using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Multiplix.Domain.Enums;

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

        public static List<SelectListItem> SexoSelect()
        {
            return new List<SelectListItem>()
            {
                _("M", "M"),
                _("F", "F"),
                _("O", "Outro")
            };
        }

        public static List<SelectListItem> MesoSelect()
        {
            return new List<SelectListItem>()
            {
                _("M", "M"),
                _("F", "F"),
                _("O", "Outro")
            };
        }

        public static List<SelectListItem> UFSelect()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var estado in EUnidadeFederativa.All())
            {
                items.Add(_(estado.Key, estado.Value));
            }
            return items;
        }      

        private static SelectListItem _(bool valor, string titulo)
        {
            return new SelectListItem() {Text = titulo, Value = valor.ToString()};
        }

        private static SelectListItem _(string valor, string titulo)
        {
            return new SelectListItem() { Text = titulo, Value = valor.ToString() };
        }

        public static string AplicarFormatacao(bool result)
        {
            return result ? "<span class=\"badge badge-pill badge-info mr-1 mb-1 mt-1\">Sim</span>" : "<span class=\"badge badge-pill badge-danger mr-1 mb-1 mt-1\">Não</span>";
        }
    }
}
