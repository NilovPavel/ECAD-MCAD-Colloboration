using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotes
{
    public interface IParser
    {
        double GetCount(string unparseString, int variantCount);

        string GetParameterValue(string unparseString, string parameterName);
    }
}
