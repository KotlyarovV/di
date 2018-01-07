using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public interface IAnalysator
    {
        Dictionary<string, double> GetWeights(IReadOnlyCollection<string> words);
    }
}
