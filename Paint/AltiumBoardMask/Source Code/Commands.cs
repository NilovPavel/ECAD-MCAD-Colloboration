using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DXP;
using EDP;
using SCH;
using PCB;
using System.IO;
using System.Xml.Serialization;
using System.Security;
using System.Security.Policy;
using System.Runtime.Remoting;
using IAssembly;
using PaintImage;
using System.Diagnostics;

public class Commands
{
    public void GetState_Screen(IServerDocumentView argContext, ref string argParameters, ref bool argEnabled, ref bool argChecked, ref bool argVisible, ref string argCaption, ref string argImageFile)
    {
    }

    public void Command_Screen(IServerDocumentView view, ref string parameters)
    {
        string projectPath = ((EDP.IWorkspace)DXP.GlobalVars.DXPWorkSpace).DM_FocusedProject().DM_ProjectFullPath();
        
        IAssemblyCAD assemblyCAD = new AltiumAssembly();
        Assembly assembly = new Assembly(assemblyCAD);

        string fileImagePath = Path.GetDirectoryName(projectPath) + @"\" + Path.GetFileNameWithoutExtension(projectPath) + "TOP.jpg";
        int[] array = new int[] {1, 33};
        ImageBuilder imageBuilder = new ImageBuilder(assembly, array, fileImagePath);
        //Process.Start(fileImagePath);

        array = new int[] { 34, 32 };
        fileImagePath = Path.GetDirectoryName(projectPath) + @"\" + Path.GetFileNameWithoutExtension(projectPath) + "BOTTOM.jpg";
        imageBuilder = new ImageBuilder(assembly, array, fileImagePath);
        //Process.Start(fileImagePath);

        //new AltiumAssembly();
    }
}

