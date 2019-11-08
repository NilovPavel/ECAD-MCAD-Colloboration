using System;
using EDP;
using IAssembly;

namespace AltiumVariantsSpace
{
    class AltiumBoardIComponentCAD : IComponentCAD
    {
        private IBoardCAD iBoard;
        private IProject project;

        public AltiumBoardIComponentCAD(IProject project, IBoardCAD iBoard)
        {
            this.project = project;
            this.iBoard = iBoard;
        }

        IComponentCAD IComponentCAD.Clone()
        {
            throw new NotImplementedException();
        }

        string IComponentCAD.GetConfiguration()
        {
            return "default";
        }

        string IComponentCAD.GetDesignator()
        {
            return null;
        }

        bool IComponentCAD.GetFitted()
        {
            return true;
        }

        IDataESKD IComponentCAD.GetIComponentData()
        {
            return new AltiumBoardESKD(this.project, this.iBoard);
        }

        ICoordinats IComponentCAD.GetICoordinats()
        {
            return new AltiumBoardCoordinats(this.iBoard);
        }

        string IComponentCAD.GetLogicalDesignator()
        {
            return null;
        }

        string IComponentCAD.GetUniqueID()
        {
            return "Board";
        }

        void IComponentCAD.SetFitted(bool isFitted)
        {
            return;
        }
    }
}