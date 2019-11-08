using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class RecordESKDPartNumberComparer : IComparer<RecordESKD>
{
    public int Compare(RecordESKD x, RecordESKD y)
    {
        return x.PartNumber.CompareTo(x.PartNumber);
    }
}
