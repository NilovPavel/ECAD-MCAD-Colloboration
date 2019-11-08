using System.IO;
using System.Reflection;

namespace ExecutableInterface
{
    class ItWork
    {
        public ItWork()
        {
            //Assembly assembly = Assembly.Load(File.ReadAllBytes(@"D:\Sources\C#\ChangeBoardPrimitives\ChangeBoardPrimitives\AltiumStandaloneDLL\bin\Debug\AltiumDebugDLL.dll"));
            Assembly assembly = Assembly.Load(File.ReadAllBytes(@"D:\Sources\CAD\Altium\AltiumBoardData\AltiumStandaloneDLL\bin\Debug\AltiumDebugDLL.dll"));
            dynamic proxyOfChildDomainObject = assembly.CreateInstance("AltiumDebugDLL.InterFaceKind");
            proxyOfChildDomainObject.Run();
            assembly = null;
        }
    }
}
