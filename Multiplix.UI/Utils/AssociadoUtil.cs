using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multiplix.UI.Utils
{
    public static class AssociadoUtil
    {
        public static bool IsBase64(this string base64String)
        {
            // Credit: oybek https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                // Handle the exception
            }
            return false;
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static object AplicarFormatacaoStatus(string status)
        {
            switch (status)
            {
                case "Pendente":
                    return "<div class=\"badge border-danger danger badge-border\">" + status + "</div>";                   
                case "Pago":
                    return "<div cl<div class=\"badge border-success success badge-border\">" + status + "</div>";
                default:
                    return "<div class=\"badge border-primary primary badge-border\">" + status + "</div>";
            }
        }
    }
}
