using BoardBuilder.KompasBoardSpace.Bodies.KompasRigid;
//using BoardBuilder.KompasBoardSpace.CutOut;
using KompasAPI7;
using Kompas6Constants;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace BoardBuilder.KompasBoardSpace
{
    internal class KompasBoard
    {
        private string xmlPath;
        private Assembly assembly;
        private Board board;
        private IApplication kompasApp;
        private IKompasDocument kompasBoard;

        public string IPartPath
        { get; private set; }

        public List<KompasBodyRigid> KompasRigides
        { get; private set; }

        private void Initialization()
        {
            this.board = this.assembly.board;
            this.IPartPath = Path.GetDirectoryName(this.xmlPath) + @"\" + Path.GetFileNameWithoutExtension(this.xmlPath) + ".m3d";
            this.kompasApp = (IApplication)Marshal.GetActiveObject("KOMPAS.Application.7");
            this.KompasRigides = new List<KompasBodyRigid>();
        }

        public KompasBoard(string xmlPath, Assembly assembly)
        {
            this.xmlPath = xmlPath;
            this.assembly = assembly;
            this.Initialization();

            this.CreateBoard();
            /*this.ChangeMaterial();
            this.WriteProperties();*/
            this.CreateBodies();
            this.CreateCutOut();
            /*this.CreateBubbles(this.SolidworksRigides);
            this.CreateBendFlexes(this.solidworksFlexes);
            this.Save();*/
        }

        private void CreateCutOut()
        {
            List<Contour> contours = new List<Contour>();
            this.board.CutOut.ForEach(item => contours.Add(item.contour));
            /*new KompasGroupCutOut(this.kompasBoardDoc, contours, "CutOut");
            contours.Clear();
            this.board.HolePad.ForEach(item => contours.Add(item.contour));
            new KompasGroupCutOut(this.kompasBoardDoc, contours, "PTH");
            contours.Clear();
            this.board.Via.ForEach(item => contours.Add(item.contour));
            new KompasGroupCutOut(this.kompasBoardDoc, contours, "NPTH");*/
        }

        private void CreateBodies()
        {
            //this.board.Body.Where(body => body.isFlex).ToList().ForEach(body => this.solidworksFlexes.Add(new SolidworksBodyFlex(body)));
            this.board.Body.Where(body => !body.isFlex).ToList().ForEach(body => this.KompasRigides.Add(new KompasBodyRigid(body, this.kompasBoard)));
        }

        private void CreateBoard()
        {
            this.kompasBoard = this.kompasApp.Application.Documents.Add(DocumentTypeEnum.ksDocumentPart);
        }

        public void Save()
        {
            this.kompasBoard.SaveAs(this.IPartPath);
        }
    }
}