namespace TagsCloudVisualization
{
    public class TextCleaner
    {
        public string RemoveSigns(char[] replacedChars, string text)
        {
            foreach (var replacedChar in replacedChars)
            {
                text = text.Replace(replacedChar, ' ');
            }
            return text;
        }
    }
}
