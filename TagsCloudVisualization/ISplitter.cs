using System.Collections.Generic;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    public interface ISplitter
    {
        IEnumerable<WordModel> Split(string text);
    }
}
