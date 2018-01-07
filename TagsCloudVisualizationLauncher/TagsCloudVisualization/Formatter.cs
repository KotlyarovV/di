namespace TagsCloudVisualization
{
    public class Formatter
    {
        public string GetOriginal(Word word)
        {
            return word.InitialForm.ToLower();
        }
    }
}
