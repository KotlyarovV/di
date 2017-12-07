using System.Collections.Generic;
using YandexMystem.Wrapper.Models;

namespace TagsCloudVisualization
{
    interface ISplitter
    {
        IEnumerable<WordModel> Split(string text);
    }
}
