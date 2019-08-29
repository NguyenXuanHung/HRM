using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Web.Core
{
    public static class StringExtension
    {
        private static readonly string KeySecure = ConfigurationManager.AppSettings["KeySecure"];private static readonly Regex WebUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex StripHTMLExpression = new Regex("<.*?>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex StripBlankParagraphExpression = new Regex(@"<p>(\s+|&nbsp;|</?\s?br\s?/?>)*</?p>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex StripNewLineExpression = new Regex("(\r\n|\r|\n)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private const string FindText = @"ỵỹỷỳýựữửừứưụũủùúợỡởờớơộỗổồốôọõỏòóịĩỉìíệễểềếêẹẽẻèéậẫẩầấâặẵẳằắăạãảàáđabcdefghijklmnopqrstuvwxyzỴỸỶỲÝỰỮỬỪỨƯỤŨỦÙÚỢỠỞỜỚƠỘỖỔỒỐÔỌÕỎÒÓỊĨỈÌÍỆỄỂỀẾÊẸẼẺÈÉẬẪẨẦẤÂẶẴẲẰẮĂẠÃẢÀÁĐABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string ReplaceText = @"yyyyyuuuuuuuuuuuoooooooooooooooooiiiiieeeeeeeeeeeaaaaaaaaaaaaaaaaadabcdefghijklmnopqrstuvwxyzYYYYYUUUUUUUUUUUOOOOOOOOOOOOOOOOOIIIIIEEEEEEEEEEEAAAAAAAAAAAAAAAAADABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private static readonly char[] IllegalUrlCharacters = { ';', '/', '\\', '?', ':', '@', '&', '=', '+', '$', ',', '<', '>', '#', '%', '.', '!', '*', '\'', '"', '(', ')', '[', ']', '{', '}', '|', '^', '`', '~', '‘', '’', '“', '”', '»', '«' };
        private static readonly string[] IllegalUrlSqlStrings = { ";", "--", "/*", "*/", "xp_" };

        [DebuggerStepThrough]
        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        [DebuggerStepThrough]
        public static string NullSafe(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        [DebuggerStepThrough]
        public static string FormatWith(this string target, params object[] args)
        {
            return string.IsNullOrEmpty(target) ? string.Empty : string.Format(CultureInfo.CurrentCulture, target, args);
        }

        [DebuggerStepThrough]
        public static string ToSHA256(this string target)
        {
            if (string.IsNullOrEmpty(target)) return string.Empty;
            var bytes = new UnicodeEncoding().GetBytes(target);
            var sha256Managed = new SHA256Managed();
            target = string.Empty;
            foreach (var b in sha256Managed.ComputeHash(bytes))
            {
                target += "{0:x2}".FormatWith(b);
            }
            return target;
        }

        [DebuggerStepThrough]
        public static string Hash(this string target)
        {
            if (string.IsNullOrEmpty(target)) return string.Empty;

            using (var md5 = MD5.Create())
            {
                var data = Encoding.Unicode.GetBytes(target);
                var hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        [DebuggerStepThrough]
        public static string StripHtml(this string target)
        {
            return StripHTMLExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static string StripBlankParagraph(this string target)
        {
            return StripBlankParagraphExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static string StripNewLine(this string target)
        {
            return StripNewLineExpression.Replace(target, string.Empty);
        }

        [DebuggerStepThrough]
        public static string StripCharacter(this string target, int characterCount)
        {
            target = target.StripHtml();
            if (target.Length <= characterCount)
                return target;
            var index = target.Substring(0, characterCount).LastIndexOf(' ');
            return index > 0 ? string.Format("{0}...", target.Substring(0, index)) : string.Format("{0}...", target.Substring(0, characterCount));

        }

        [DebuggerStepThrough]
        public static Guid ToGuid(this string target)
        {
            var result = Guid.Empty;

            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                var encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    var base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch (FormatException)
                {
                }
            }

            return result;
        }

        [DebuggerStepThrough]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }

        [DebuggerStepThrough]
        public static string UrlEncode(this string target)
        {
            return HttpUtility.UrlEncode(target);
        }

        [DebuggerStepThrough]
        public static string UrlDecode(this string target)
        {
            return HttpUtility.UrlDecode(target);
        }

        [DebuggerStepThrough]
        public static string AttributeEncode(this string target)
        {
            return HttpUtility.HtmlAttributeEncode(target);
        }

        [DebuggerStepThrough]
        public static string HtmlEncode(this string target)
        {
            return HttpUtility.HtmlEncode(target);
        }

        [DebuggerStepThrough]
        public static string HtmlDecode(this string target)
        {
            return HttpUtility.HtmlDecode(target);
        }

        [DebuggerStepThrough]
        public static string Replace(this string target, ICollection<string> oldValues, string newValue)
        {
            oldValues.ForEach(oldValue => target = target.Replace(oldValue, newValue));
            return target;
        }

        [DebuggerStepThrough]
        public static string ToDecryptDES(this string target)
        {
            //get the byte code of the string

            //        byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            var toEncryptArray = HexToBytes(target);

            //if hashing was used get the hash code with regards to your key
            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(KeySecure));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //set the secret key for the tripleDES algorithm
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            //padding mode(if any extra byte added)

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return Encoding.UTF8.GetString(resultArray);
        }

        [DebuggerStepThrough]
        public static string ToEncryptDES(this string target)
        {
            var toEncryptArray = Encoding.UTF8.GetBytes(target);

            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key

            var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(KeySecure));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();


            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            //set the secret key for the tripleDES algorithm
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            //padding mode(if any extra byte added)

            var cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            var resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format

            //StringBuilder hex = new StringBuilder();
            //for(int i =0 ; i < resultArray.Length -1; i++) hex.AppendFormat("{0:X2}", resultArray[i]);
            //return hex.ToString(); 
            return BytesToHex(resultArray);

            //return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        [DebuggerStepThrough]
        private static string BytesToHex(IList<byte> bytes)
        {
            var hex = new StringBuilder();
            for (var n = 0; n <= bytes.Count - 1; n++)
            {
                hex.AppendFormat("{0:X2}", bytes[n]);
            }
            return hex.ToString();
        }

        [DebuggerStepThrough]
        private static Byte[] HexToBytes(string hex)
        {
            var numBytes = hex.Length / 2;
            var bytes = new Byte[numBytes];
            for (var n = 0; n <= numBytes - 1; n++)
            {
                var hexByte = hex.Substring(n * 2, 2);
                bytes[n] = Convert.ToByte(int.Parse(hexByte, NumberStyles.HexNumber));
            }
            return bytes;
        }

        [DebuggerStepThrough]
        public static string FormatStringFullTextSearch(this string target)
        {
            target = target.ToStringSql();
            if (String.IsNullOrEmpty(target)) return target;
            var sb = new StringBuilder();
            var arrWords = target.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            int i;
            var length = arrWords.Length;

            for (i = 0; i < length; i++)
            {
                if (arrWords[i].Length > 1)
                {
                    sb.Append(i < length - 1
                        ? String.Format("\"{0}\" AND ", arrWords[i].Trim())
                        : String.Format("\"{0}\" ", arrWords[i].Trim()));
                }
            }

            if (!String.IsNullOrEmpty(sb.ToString()))
            {
                sb.Insert(0, "N'");
                sb.Append("'");
                return sb.ToString().Trim();
            }
            return string.Empty;

        }

        [DebuggerStepThrough]
        public static string ToStringSql(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(IllegalUrlCharacters) > -1)
            {
                target = IllegalUrlCharacters.Aggregate(target, (current, character) => current.Replace(character.ToString(), string.Empty));
            }
            return target;
        }

        [DebuggerStepThrough]
        public static string EscapeQuote(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }
            // trim
            target = target.Trim();
            // escape ' character
            target = target.Replace("'", "''");
            return IllegalUrlSqlStrings.Aggregate(target, (current, str) => current.Replace(str, string.Empty));
        }

        [DebuggerStepThrough]
        public static string ClearSign(this string text)
        {
            return text.ClearSign(false);
        }

        [DebuggerStepThrough]
        public static string ClearSign(this string text, bool replaceWhiteSpace)
        {
            var findText = FindText;
            var replaceText = ReplaceText;
            // check null value
            if (string.IsNullOrEmpty(text)) return "";
            if (replaceWhiteSpace)
            {
                findText += "{0}_- ".FormatWith(FindText);
                replaceText += "{0}   ".FormatWith(ReplaceText);
            }
            // compare each character with find text
            for (var i = 0; i < text.Length; i++)
            {
                var index = findText.IndexOf(text[i]);
                if (index > -1)
                {
                    text = text.Replace(text[i], replaceText[index]);
                }
                else
                {
                    text = text.Remove(i, 1);
                    i--;
                }
            }
            return text;
        }

        [DebuggerStepThrough]
        public static string ToLegalUrl(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(IllegalUrlCharacters) > -1)
            {
                target = IllegalUrlCharacters.Aggregate(target, (current, character) => current.Replace(character.ToString(), string.Empty));
            }

            target = target.Replace(" ", "-");

            while (target.Contains("--"))
            {
                target = target.Replace("--", "-");
            }

            return target;
        }

        [DebuggerStepThrough]
        public static string ConverValue(this string text)
        {
            var regex = new Regex(@"\s{2,}");
            text = regex.Replace(text.Trim(), " ");//This line removes extra spaces and make space exactly one.
            //To remove the  space between the end of a word and a punctuation mark used in the text we will
            //be using following line of code
            regex = new Regex(@"\s(\!|\.|\?|\;|\,|\:)"); // “\s” whill check for space near all puntuation marks in side ( \!|\.|\?|\;|\,|\:)”); )
            text = regex.Replace(text, "$1");
            return text.Replace(' ', '-').Replace(".", "") + "";
        }

        [DebuggerStepThrough]
        public static string StripQueryString(this string target)
        {
            var quesPosition = target.IndexOf('?');
            return quesPosition > 0 ? target.Substring(0, quesPosition) : target;
        }

        [DebuggerStepThrough]
        public static string ToUpperString(this string text)
        {
            //remove space
            var findText = FindText;
            var replaceText = ReplaceText;
            // check null value
            if (string.IsNullOrEmpty(text)) return "";

            findText += "{0}_- ".FormatWith(FindText);
            replaceText += "{0}   ".FormatWith(ReplaceText);

            // compare each character with find text
            for (var i = 0; i < text.Length; i++)
            {
                var index = findText.IndexOf(text[i]);
                
                if (index > -1)
                {
                    text = text.Replace(text[i], replaceText[index]);
                }
                else
                {
                    text = text.Remove(i, 1);
                    i--;
                }
            }
            // meet whiteSpace bold the first character in text
            var txtArr = text.Split(' ');
            var resultText = string.Empty;
            foreach (var textUpper in txtArr)
            {
                if (string.IsNullOrEmpty(textUpper)) continue;
                var resultUpper = textUpper.First().ToString().ToUpper() + textUpper.Substring(1);
                resultText += resultUpper;
            }
            //set
            text = resultText;
          
            //upper case
            return text;
        }

        
        [DebuggerStepThrough]
        public static int ToExcelColumnNumber(this string columnName)
        {
            if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException(nameof(columnName));

            columnName = columnName.ToUpperInvariant();

            var sum = 0;

            foreach (var t in columnName)
            {
                sum *= 26;
                sum += (t - 'A' + 1);
            }

            return sum;
        }

        [DebuggerStepThrough]
        public static string GetKeyword(this string keyword)
        {
            keyword = keyword.ToLower();
            var charArray = keyword.ToCharArray();
            keyword = "";
            foreach (char ch in charArray)
            {
                var str = ch.ToString();
                switch (ch)
                {
                    case 'a':
                        str = "[aáàạảãâấầậẩẫăắằặẳẵ]";
                        break;
                    case 'd':
                        str = "[dđ]";
                        break;
                    case 'e':
                        str = "[eéèẹẻẽêếềệểễ]";
                        break;
                    case 'i':
                        str = "[iíìịỉĩýyỳỷỹ]";
                        break;
                    case 'o':
                        str = "[oóòọỏõôốồộổỗơớờợởỡ]";
                        break;
                    case 's':
                    case 'x':
                        str = "[sx]";
                        break;
                    case 'u':
                        str = "[uúùụủũưứừựửữ]";
                        break;
                    case 'y':
                        str = "[ýyỳỷỹiíìịỉĩ]";
                        break;
                }
                keyword += str;
            }
            return keyword.Replace(" ", "%");
        }
    }
}
