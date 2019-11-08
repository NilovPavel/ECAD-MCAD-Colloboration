using System;
using SolidWorks.Interop.sldworks;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using SolidworksActiveDocSpace;
using FlexBoardSolidworks.SolidworksBoardSpace;
using System.Collections.Generic;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;

namespace SolidworksBoardSpace
{
    class SolidworksBoard
    {
        private SldWorks app;
        private PartDoc partDoc;
        private string path;
        private Assembly assembly;
        private Board board;
        private List<SolidworksBodyFlex> solidworksFlexes;

        public List<SolidworksBodyRigid> SolidworksRigides
        { get; private set; }

        public Component2 Component
        { get; private set; }

        public string FilePath
        { get { return this.path; } }

        public SolidworksBoard(string path, Assembly assembly)
        {
            this.path = path;
            this.assembly = assembly;
            this.Initialization();
            this.CreateBoard();
            this.ChangeMaterial();
            this.WriteProperties();
            this.CreateBodies();
            this.CreateCutOut();
            this.CreateBubbles(this.SolidworksRigides);
            this.CreateBendFlexes(this.solidworksFlexes);
            this.Save();
        }

        private void WriteProperties()
        {
            IProjectPropertiesCAD swBoardProperties = new SolidWorksBoardProperties((ModelDoc2)this.partDoc);
            string[] propertieNames = swBoardProperties.GetPropertieNames();
            foreach (string propertie in propertieNames)
                swBoardProperties.WriteValue(propertie, this.assembly.projectProperties.GetPropertyByName(propertie));
        }

        private void ChangeMaterial()
        {
            try
            {
                SolidworksMaterial material = new SolidworksMaterial();
                Configuration activeConfiguration = ((ModelDoc2)this.partDoc).GetActiveConfiguration();
                this.partDoc.SetMaterialPropertyName2(activeConfiguration.Name, material.FileName, "Rogers 4003C");
            }
            catch (Exception ex)
            {
                MessageBox.Show(  ex.Message +"\r\n"+ "\nTry to change the material in manual mode.", "Material is not changed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateBubbles(List<SolidworksBodyRigid> solidworksRigides)
        {
            solidworksRigides.ForEach(x => x.BuildBubble());
        }

        private void CreateConfigurations()
        {
            ((ModelDoc2)this.partDoc).IGetActiveConfiguration().Name = "Flat";
            ((ModelDoc2)this.partDoc).AddConfiguration3("Bend", "Bend body", "Bend", (int)swConfigurationOptions_e.swUseAlternateName);
        }

        private void CreateBendFlexes(List<SolidworksBodyFlex> solidworksFlexes)
        {
            this.CreateConfigurations();
            List<short> sequence = new List<short>();
            this.board.Body.ForEach(x => x.BendingLine.ForEach(y => sequence.Add(y.FoldIndex)));
            sequence = sequence.Distinct().ToList();
            sequence.Sort();
            sequence.ForEach(x => solidworksFlexes.ForEach(y => y.CreateBendByFoldIndex(x)));
        }

        private void CreateBodies()
        {
            this.board.Body.Where(body => body.isFlex).ToList().ForEach(body => this.solidworksFlexes.Add(new SolidworksBodyFlex(body)));
            this.board.Body.Where(body => !body.isFlex).ToList().ForEach(body => this.SolidworksRigides.Add(new SolidworksBodyRigid(body)));
        }

        public void SetBoardComponent(Component2 component)
        {
            this.Component = component;
        }

        private void CreateCutOut()
        {
            List<Contour> contours = new List<Contour>();
            this.board.CutOut.ForEach(item => contours.Add(item.contour));
            new SolidworksGroupCutOut(contours, "CutOut");
            contours.Clear();
            this.board.HolePad.ForEach(item => contours.Add(item.contour));
            new SolidworksGroupCutOut(contours, "PTH");
            contours.Clear();
            this.board.Via.ForEach(item => contours.Add(item.contour));
            new SolidworksGroupCutOut(contours, "NPTH");
        }

        private void CreateBoard()
        {
            this.partDoc = this.app.INewPart();
        }

        private void Save()
        {
            ((ModelDoc2)this.partDoc).SaveAs(this.path);
        }

        private void Initialization()
        {
            this.board = this.assembly.board;
            this.app = new SolidworksActiveDoc().App;
            this.app.DocumentVisible(true, (int)swDocumentTypes_e.swDocPART);
            string smallFileNameAssembly = !string.IsNullOrEmpty(this.assembly.projectProperties.GetPropertyByName("Обозначение_PCB")) ? this.assembly.projectProperties.GetPropertyByName("Обозначение_PCB") : Path.GetFileNameWithoutExtension(this.path);
            this.path = Path.GetDirectoryName(this.path) + @"\" + smallFileNameAssembly + ".sldprt";
            this.solidworksFlexes = new List<SolidworksBodyFlex>();
            this.SolidworksRigides = new List<SolidworksBodyRigid>();
        }
    }
}