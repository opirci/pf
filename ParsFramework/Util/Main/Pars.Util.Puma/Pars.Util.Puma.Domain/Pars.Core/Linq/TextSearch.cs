using System.Runtime.Serialization;

namespace Pars.Core.Linq
{
    [DataContract]
    public class TextSearch
    {
        [DataMember(Name ="text")]
        public string Text { get; set; }
        [DataMember(Name = "searchAs")]
        public TextSearchAs SearchAs { get; set; }

        public TextSearch()
        {

        }

        public TextSearch(string text, TextSearchAs searchAs = TextSearchAs.Contains)
        {
            Text = text;
            SearchAs = searchAs;

        }

        public static bool IsNullOrEmpty(TextSearch textSearch)
        {
            return textSearch == null || textSearch != null && string.IsNullOrEmpty(textSearch.Text);
        }
    }
}
