using System;
using System.Collections.Generic;

class DesignatorDevice : IComparable<DesignatorDevice>, IEqualityComparer<DesignatorDevice>
{
    public string Name { get; set; } // Это будет элементом

    public string Value { get; set; } // Это будет атрибутом

    public DesignatorDevice(string Name, string Value)
    {
        this.Name = Name;
        this.Value = Value;
    }

    bool IEqualityComparer<DesignatorDevice>.Equals(DesignatorDevice x, DesignatorDevice y)
    { return x.Name.Equals(y.Name); }

    int IEqualityComparer<DesignatorDevice>.GetHashCode(DesignatorDevice obj)
    { return 0; }

    public int CompareTo(DesignatorDevice other)
    {
        return string.Compare(this.Name, other.Name);
    }
}