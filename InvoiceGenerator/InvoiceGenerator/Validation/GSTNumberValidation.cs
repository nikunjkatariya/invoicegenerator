using System.Text.RegularExpressions;

namespace InvoiceGenerator.Validation
{
    public class GSTNumberValidation
    {
        public bool IsValid(string gst, string pan)
        {
            var GST = gst;
            var PAN = pan;
            string pattern = @"^[0-9]{2}[A-Z]{5}[0-9]{4}" + "[A-Z]{1}[1-9A-Z]{1}" + "Z[0-9A-Z]{1}$";
            String pattern_pan = "[A-Z]{5}[0-9]{4}[A-Z]{1}";
            Match m = Regex.Match(GST, pattern, RegexOptions.IgnoreCase);
            Match p = Regex.Match(PAN, pattern_pan, RegexOptions.IgnoreCase);
            if (m.Success && p.Success)
            {
                return true;
            }
            return false;
        }
    }
}
