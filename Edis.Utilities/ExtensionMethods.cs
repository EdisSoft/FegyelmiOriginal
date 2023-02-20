using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Edis.Utilities
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty<T>(this List<T> source)
        {
            if (source == null || !source.Any())
            {
                return true;
            }
            return false;
        }

        public static string UppercaseFirst(this string s)
        {

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
        public static string ToTitleCase(this string s)
        {

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            string[] parts = s.Split(' ');
            List<string> res = new List<string>();
            foreach (var item in parts)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var a = item.ToLower().ToCharArray();
                    a[0] = char.ToUpper(a[0]);
                    res.Add(new string(a));
                }
            }
            return string.Join(" ", res);
        }

        public static string ToForintFormat(this int i)
        {
            return i.ToPenzFormat("Ft");
        }
        public static string ToForintFormat(this double i)
        {
            return i.ToPenzFormat("Ft");
        }
        public static string ToForintFormat(this double? i)
        {
            return i?.ToPenzFormat("Ft");
        }
        public static string ToForintFormat(this decimal i)
        {
            return i.ToPenzFormat("Ft");
        }
        public static string ToForintFormat(this decimal? i)
        {
            return i?.ToPenzFormat("Ft");
        }

        public static string ToDarabFormat(this int i)
        {
            return i.ToPenzFormat("db");
        }

        public static string ToSzazalekFormat(this int i)
        {
            return i.ToPenzFormat("%");
        }


        public static string ToNapFormat(this int i)
        {
            return i.ToPenzFormat("nap");
        }

        public static string ToNapFormat(this int? i)
        {
            return i?.ToPenzFormat("nap");
        }

        public static string ToHoNapFormat(this int i)
        {
            return i.ToPenzFormat("hónap");
        }

        public static string ToHoNapFormat(this int? i)
        {
            return i?.ToPenzFormat("hónap");
        }

        public static string ToHyphenIfEmptyOrNull(this string i)
        {
            return !string.IsNullOrWhiteSpace(i) ? i : "-";
        }

        public static string ToHyphenIfEmptyOrNull(this int? i)
        {
            if (i == null)
                return "-";
            return i.ToString();
        }

        public static string ToHyphenIfEmptyOrNull(this double? i)
        {
            if (i == null)
                return "-";
            return i.ToString();
        }

        public static string ToHyphenIfEmptyOrNull(this decimal? i)
        {
            if (i == null)
                return "-";
            return i.ToString();
        }

        public static string ToPenzFormat(this int i, string penzNem)
        {
            return string.Format("{0} {1}", i.ToString("### ### ### ##0"), penzNem).Trim();
        }

        public static string ToPenzFormat(this double? i, string penzNem)
        {
            if (i.HasValue)
                return i.Value.ToPenzFormat(penzNem);
            else
                return "-";
        }

        public static string ToPenzFormat(this double i, string penzNem)
        {
            string osszeg = i.ToString("### ### ### ##0.##");
            if (osszeg.EndsWith(".")) osszeg = osszeg.Replace(".", "");
            return string.Format("{0} {1}", osszeg, penzNem).Trim();
        }

        public static string ToPenzFormat(this decimal? i, string penzNem)
        {
            if (i.HasValue)
                return i.Value.ToPenzFormat(penzNem);
            else
                return "-";
        }

        public static string ToPenzFormat(this decimal i, string penzNem)
        {
            //if (ispontosan2tizedesjegy)
            //{
            //    string osszeg = i.ToString("### ### ### ##0.##");
            //    if (osszeg.EndsWith(".")) osszeg = osszeg.Replace(".", "");
            //    return string.Format("{0} {1}", osszeg, penzNem).Trim();
            //}
            //else
            //{
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ",";
            var strDec = i.ToString("n4", nfi);
            var formatedDec = strDec.Contains(",") ? strDec.TrimEnd('0').TrimEnd(',') : strDec;
            return $"{formatedDec} {penzNem}";
            //}
        }

        public static string ToEzresFormat(this int i)
        {
            string osszeg = i.ToString("### ### ### ##0");
            if (osszeg.EndsWith(".")) osszeg = osszeg.Replace(".", "");
            return string.Format("{0}", osszeg).Trim();
        }
        
        public static string ToEzresFormat(this int? i)
        {
            if (!i.HasValue)
            {
                return "";
            }
            string osszeg = i.Value.ToString("### ### ### ##0");
            if (osszeg.EndsWith(".")) osszeg = osszeg.Replace(".", "");
            return string.Format("{0}", osszeg).Trim();
        }

        public static string ToEzresFormat(this double i)
        {
            string osszeg = i.ToString("### ### ### ##0.##");
            if (osszeg.EndsWith(".")) osszeg = osszeg.Replace(".", "");
            return string.Format("{0}", osszeg).Trim();
        }

        public static string ToEzresFormat(this double? i)
        {
            if (!i.HasValue)
                return "-";
            string osszeg = i.Value.ToString("### ### ### ##0.##");
            if (osszeg.EndsWith(".")) osszeg = osszeg.Replace(".", "");
            return string.Format("{0}", osszeg).Trim();
        }

        public static string ToEzresFormat(this decimal i)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ",";
            var strDec = i.ToString("n4", nfi);
            var formatedDec = strDec.Contains(",") ? strDec.TrimEnd('0').TrimEnd(',') : strDec;
            return $"{formatedDec}";
        }

        public static string ToEzresFormatEgyTizedesJegy(this decimal i)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ",";
            var strDec = i.ToString("n1", nfi);
            var formatedDec = strDec.Contains(",") ? strDec.TrimEnd('0').TrimEnd(',') : strDec;
            return $"{formatedDec}";
        }

        public static string ToEzresFormat(this string i)
        {
            decimal j = decimal.Parse(i);
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";
            nfi.NumberDecimalSeparator = ",";
            var strDec = j.ToString("n4", nfi);
            var formatedDec = strDec.Contains(",") ? strDec.TrimEnd('0').TrimEnd(',') : strDec;
            return $"{formatedDec}";
        }


        public static string ToEzresFormat(this decimal? i)
        {
            if (i.HasValue)
                return i.Value.ToEzresFormat();
            else
                return "-";
        }

        public static string ToIgenNemText(this bool b)
        {
            return b ? "Igen" : "Nem";
        }
        public static string ToMuveletGombVonalkod(this string i)
        {
            return !string.IsNullOrWhiteSpace(i) ? $"@--{i}*" : null;
        }

        public static bool Between(this DateTime dt, DateTime start, DateTime end)
        {
            return dt >= start && dt <= end;
        }

        public static bool Between(this int dt, int start, int end)
        {
            return dt >= start && dt <= end;
        }

        public static string ReplaceSpecialTag(this string source, string newText)
        {
            return Regex.Replace(source, "#(.*)#", newText.ToLower());
        }

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        public static string RemoveDiacriticsAndSpace(this string text)
        {
            return text.RemoveDiacritics().Replace(" ", "");
        }

        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        public static string TruncateWithout3Point(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }

        public static int ToInt32OrDefault(this string value, int defaultValue = 0)
        {
            int result;
            return int.TryParse(value, out result) ? result : defaultValue;
        }

        public static string NameOfController(this string value)
        {
            return value.Replace("Controller", "");
        }

        public static string TrimNonAscii(this string value)
        {
            string pattern = "[^ -~]+";
            Regex reg_exp = new Regex(pattern);
            return reg_exp.Replace(value, "");
        }
        public static bool DeepCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                if (!objValue.Equals(anotherValue)) result = false;
            }

            return result;
        }

        public static bool TryParseNullable<T>(this string text, out T? outValue) where T : struct
        {
            outValue = null;
            try
            {
                var result = Convert.ChangeType(text, typeof(T));
                if (result != null)
                {
                    outValue = (T)result;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public static string AfterLastNumber(this string text) 
        //{
        //    for
        //}
    }

    public static class ListExtensions
    {
        public static List<T> InList<T>(this T item)
        {
            return new List<T> { item };
        }

    }

    public static class DateTimeExtensions
    {
        public static bool IsInRange(this DateTime date, DateTime startDate, DateTime endDate)
        {
            return date >= startDate && date < endDate;
        }

        public static string ToTimeSpan24H(this DateTime date)
        {
            return date == new DateTime(1900, 1, 2, 0, 0, 0) ? "24:00:00" : date.ToString("HH:mm:ss");
        }

        public static string ToTimeSpan24HDisplay(this DateTime date)
        {
            return date == new DateTime(1900, 1, 2, 0, 0, 0) ? "24:00" : date.ToString("HH:mm");
        }

        public static DateTime ToDateTime24H(this string date)
        {
            const string pattern = @"[0-9][0-9]?:[0-9][0-9]:[0-9][0-9]";
            var match = Regex.Match(date, pattern);

            if (!match.Success)
                throw new Exception($"A kapott string ({date}) nem egyezik meg az időformátummal (HH:mm:ss).");

            return date == "24:00:00" ? new DateTime(1900, 1, 2, 0, 0, 0) : DateTime.Parse($"1900.01.01. {date}");

        }

        public static DateTime Max(DateTime a, DateTime b)
        {
            return a > b ? a : b;
        }


        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }
        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek, int days)
        {
            // ha a days 7 akkor a következő hetet fogja visszadani
            // ha 14 akkor az azt követőt
            // ha 21 akkor ...
            // stb...
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += days;
            return from.AddDays(target - start);
        }
        public static DateTime MondayOfLastWeek(this DateTime date)
        {
            DateTime mondayOfLastWeek = date.AddDays(-(int)date.DayOfWeek - 6);
            return mondayOfLastWeek;
        }

        public static bool IsWeekend(this DateTime date)
        {
            return new[] { DayOfWeek.Sunday, DayOfWeek.Saturday }.Contains(date.DayOfWeek);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
    (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// DistinctBy(<paramref name="property"/>) and Do <paramref name="action"/> before duplicate removal
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="property">Key selector for the distinct function</param>
        /// <param name="action">Action of (kept item, removed duplicate item)</param>
        /// <returns></returns>
        public static IList<TModel> DistinctActionBy<TModel, TProp>(this IList<TModel> model, Func<TModel, TProp> property, Action<TModel, TModel> action)
        {
            var result = new List<TModel>(model);
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (property(result[i]).Equals(property(result[j])))
                    {
                        action(result[i], result[j]);
                        result.RemoveAt(j);
                    }
                }
            }
            return result;
        }
    }

    public enum PaddingDirection
    {
        Left,
        Right
    }
    public static class GiroHelper
    {
        public static string HosszBeallitas(string adat, int hossz, char kitoltoAdat, PaddingDirection irany)
        {
            if (irany == PaddingDirection.Left)
            {
                return adat.PadLeft(hossz, kitoltoAdat).Substring(0, hossz);
            }
            if (irany == PaddingDirection.Right)
            {
                return adat.PadRight(hossz, kitoltoAdat).Substring(0, hossz);
            }

            return adat;
        }
    }
    public static class ReplaceHelper
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }

    public static class StringBuilderExtensions
    {
        public static StringBuilder Prepend(this StringBuilder sb, string content)
        {
            return sb.Insert(0, content);
        }
    }

    public static class EnumerableExtensions
    {
        public struct CurrentAndPrevious<T>
        {
            public T Current { get; private set; }
            public T Previous { get; private set; }

            public CurrentAndPrevious(T current, T previous) : this()
            {
                Previous = previous;
                Current = current;
            }
        }

        public static IEnumerable<CurrentAndPrevious<T>> WithPrevious<T>(this IEnumerable<T> enumerable)
        {
            var previous = default(T);

            using (var enumerator = enumerable.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return new CurrentAndPrevious<T>(enumerator.Current, previous);
                    previous = enumerator.Current;
                }
            }
        }
    }
}
