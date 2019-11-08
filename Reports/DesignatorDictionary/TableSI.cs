using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TableSI
{
    private Dictionary<string, int> dictionarySI;

    private void Initialization()
    {
        this.dictionarySI = new Dictionary<string, int>()
        {
            {"и", -24 },
            {"з", -21 },
            {"а", -18 },
            {"ф", -15 },
            {"п", -12 },
            {"н", -9  },
            {"мк",-6  },
            {"м", -3  },
            {"с", -2  },
            {"д", -1  },
            {"",   0  },
            {"да", 1  },
            {"г",  2  },
            {"к",  3  },
            {"М",  6  },
            {"Г",  9  },
            {"Т",  12 },
            {"П",  15 },
            {"Э",  18 },
            {"З",  21 },
            {"И",  24 },
        };
    }

    public int GetStepen(string siStepen)
    {
        int stepen;
        bool itsOk = this.dictionarySI.TryGetValue(siStepen, out stepen);
        return stepen;
    }

    public TableSI()
    {
        this.Initialization();
    }
}