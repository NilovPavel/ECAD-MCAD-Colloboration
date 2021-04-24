using System.IO;
using System.Reflection;

namespace ExecutableInterface
{
    class ItWork
    {
        public ItWork()
        {
            Assembly assembly = Assembly.Load(File.ReadAllBytes(@"D:\Sources\ECAD-MCAD-Colloboration\Altium\AltiumBoardData\AltiumStandaloneDLL\bin\Debug\AltiumDebugDLL.dll"));
            dynamic proxyOfChildDomainObject = assembly.CreateInstance("AltiumDebugDLL.InterFaceKind");
            //Assembly assembly = Assembly.Load(File.ReadAllBytes(@"E:\Работа\Sources\CAD\Altium\Softline\SoftLineXML\bin\Debug\SoftlineBoardData.dll"));
            //dynamic proxyOfChildDomainObject = assembly.CreateInstance("SoftlineBoardData.InterFaceKind");
            
            proxyOfChildDomainObject.Run();
            assembly = null;
        }
    }
}
