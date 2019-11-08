using System.Collections.Generic;

namespace ReportsOutput
{
    public interface ISpecificationWriter
    {
        void Write(ProjectProperties projectProperties, Spisok specification, List<Spisok> spisok);
    }
}