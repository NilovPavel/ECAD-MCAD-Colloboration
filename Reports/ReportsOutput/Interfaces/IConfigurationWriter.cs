using System.Collections.Generic;

namespace ReportsOutput
{
    public interface IConfigurationWriter
    {
        void Write(ProjectProperties projectProperties, Spisok configuration, List<Spisok> configurations);
    }
}