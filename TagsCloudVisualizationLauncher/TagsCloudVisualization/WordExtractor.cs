using System.Linq;
using YandexMystem.Wrapper;

namespace TagsCloudVisualization
{
    public class WordExtractor
    {
        public Result<Word[]> ExtractWords(string text)
        {
            Mysteam mysteam = new Mysteam();

            var mysreamInitialization = Result.Of(() => mysteam = new Mysteam());
            if (!mysreamInitialization.IsSuccess)
                return Result.Fail<Word[]>("Can't find mystem.exe for yandex library");            

            return Result.Of(() => mysteam.GetWords(text)
                    .Select(wordModel => new Word(wordModel))
                    .ToArray()
                ).ReplaceError(error => "Yandex libtaty can't handle text.");
        }
    }
}
