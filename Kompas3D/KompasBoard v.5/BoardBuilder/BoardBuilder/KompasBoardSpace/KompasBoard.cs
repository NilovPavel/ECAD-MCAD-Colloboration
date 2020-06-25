using BoardBuilder.KompasBoardSpace.Bodies.KompasRigid;
using BoardBuilder.KompasBoardSpace.CutOut;
using Kompas6API5;
using Kompas6API7;
using System;
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
        private KompasObject kompasApp;
        private ksDocument3D kompasBoardDoc;

        public string IPartPath
        { get; private set; }

        public List<KompasBodyRigid> KompasRigides
        { get; private set; }

        private void Initialization()
        {
            this.board = this.assembly.board;
            this.IPartPath = Path.GetDirectoryName(this.xmlPath) + @"\" + Path.GetFileNameWithoutExtension(this.xmlPath) + ".m3d";
            this.kompasApp = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
            this.KompasRigides = new List<KompasBodyRigid>();
        }

        public KompasBoard(string xmlPath, Assembly assembly)
        {
            this.xmlPath = xmlPath;
            this.assembly = assembly;
            this.Initialization();

            this.CreateBoard();
            this.ChangeMaterial();
            /*this.WriteProperties();*/
            this.CreateBodies();
            this.CreateCutOut();
            /*this.CreateBubbles(this.SolidworksRigides);
            this.CreateBendFlexes(this.solidworksFlexes);
            this.Save();*/
        }

        private void ChangeMaterial()
        {
            IKompasDocument3D iKompasDocument = (IKompasDocument3D)((IApplication)Marshal.GetActiveObject("KOMPAS.Application.7")).ActiveDocument;
            iKompasDocument.TopPart.SetMaterial("Сталь 45  ГОСТ 1050-2013", 0);
            iKompasDocument.TopPart.Update();
        }

        private void CreateCutOut()
        {
            List<Contour> contours = new List<Contour>();
            this.board.CutOut.ForEach(item => contours.Add(item.contour));
            new KompasGroupCutOut(this.kompasBoardDoc, contours, "CutOut");
            contours.Clear();
            this.board.HolePad.ForEach(item => contours.Add(item.contour));
            new KompasGroupCutOut(this.kompasBoardDoc, contours, "PTH");
            contours.Clear();
            this.board.Via.ForEach(item => contours.Add(item.contour));
            new KompasGroupCutOut(this.kompasBoardDoc, contours, "NPTH");/**/
        }

        private void CreateBodies()
        {
            //this.board.Body.Where(body => body.isFlex).ToList().ForEach(body => this.solidworksFlexes.Add(new SolidworksBodyFlex(body)));
            this.board.Body.Where(body => !body.isFlex).ToList().ForEach(body => this.KompasRigides.Add(new KompasBodyRigid(body, this.kompasBoardDoc)));
        }

        private void CreateBoard()
        {
            this.kompasBoardDoc = this.kompasApp.Document3D();
            this.kompasBoardDoc.Create(false, true);
        }

        public void Save()
        {
            this.kompasBoardDoc.SaveAs(this.IPartPath);
            this.kompasBoardDoc.close();
        }
    }
}