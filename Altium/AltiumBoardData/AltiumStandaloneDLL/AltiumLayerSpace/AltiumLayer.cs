using Help;
using PCB;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BoardSpace
{
    class AltiumLayer
    {
        private IPCB_LayerObject layerObject;
        private IPCB_LayerObject_V7 layer_v7;

        public bool IsElectrical
        { get; private set; }

        public double OriginHeight
        { get; set; }

        public TV6_Layer Type
        { get; private set; }

        private void Initialization()
        {
            this.layer_v7 = (IPCB_LayerObject_V7)this.layerObject;
            this.Type = this.layer_v7.LayerID();
            this.IsElectrical = false;
        }

        public AltiumLayer(IPCB_LayerObject layerObject)
        {
            this.layerObject = layerObject;
            this.Initialization();
        }

        public string LayerName()
        {
            return this.layer_v7.GetState_LayerName();
        }

        public int LayerNumber()
        {
            return (int)this.layer_v7.LayerID();
        }

        public double GetThickness()
        {
            int height;
            switch (this.Type)
            {
                //Слой вставки компонента
                case TV6_Layer.eV6_BottomPaste:
                case TV6_Layer.eV6_TopPaste:
                    height = 0;
                    break;
                //Слой шелкографии
                case TV6_Layer.eV6_BottomOverlay:
                case TV6_Layer.eV6_TopOverlay:
                    height = 0;
                    break;
                //Слой "маски" платы
                case TV6_Layer.eV6_TopSolder:
                case TV6_Layer.eV6_BottomSolder:
                    height = 0;
                    //this.height = this.dielectric.GetState_DielectricHeight();
                    break;
                //Слой диэлектрика
                case TV6_Layer.eV6_NoLayer:
                    height = ((IPCB_DielectricLayer)layer_v7).GetState_DielectricHeight();
                    break;
                //Слой токопроводящий с дорожками
                case TV6_Layer.eV6_BottomLayer:
                case TV6_Layer.eV6_TopLayer:
                case TV6_Layer.eV6_ConnectLayer:
                case TV6_Layer.eV6_MidLayer1:
                case TV6_Layer.eV6_MidLayer2:
                case TV6_Layer.eV6_MidLayer3:
                case TV6_Layer.eV6_MidLayer4:
                case TV6_Layer.eV6_MidLayer5:
                case TV6_Layer.eV6_MidLayer6:
                case TV6_Layer.eV6_MidLayer7:
                case TV6_Layer.eV6_MidLayer8:
                case TV6_Layer.eV6_MidLayer9:
                case TV6_Layer.eV6_MidLayer10:
                case TV6_Layer.eV6_MidLayer11:
                case TV6_Layer.eV6_MidLayer12:
                case TV6_Layer.eV6_MidLayer13:
                case TV6_Layer.eV6_MidLayer14:
                case TV6_Layer.eV6_MidLayer15:
                case TV6_Layer.eV6_MidLayer16:
                case TV6_Layer.eV6_MidLayer17:
                case TV6_Layer.eV6_MidLayer18:
                case TV6_Layer.eV6_MidLayer19:
                case TV6_Layer.eV6_MidLayer20:
                case TV6_Layer.eV6_MidLayer21:
                case TV6_Layer.eV6_MidLayer22:
                case TV6_Layer.eV6_MidLayer23:
                case TV6_Layer.eV6_MidLayer24:
                case TV6_Layer.eV6_MidLayer25:
                case TV6_Layer.eV6_MidLayer26:
                case TV6_Layer.eV6_MidLayer27:
                case TV6_Layer.eV6_MidLayer28:
                case TV6_Layer.eV6_MidLayer29:
                case TV6_Layer.eV6_MidLayer30:
                //Слой токопроводящий экранирования
                case TV6_Layer.eV6_InternalPlane1:
                case TV6_Layer.eV6_InternalPlane2:
                case TV6_Layer.eV6_InternalPlane3:
                case TV6_Layer.eV6_InternalPlane4:
                case TV6_Layer.eV6_InternalPlane5:
                case TV6_Layer.eV6_InternalPlane6:
                case TV6_Layer.eV6_InternalPlane7:
                case TV6_Layer.eV6_InternalPlane8:
                case TV6_Layer.eV6_InternalPlane9:
                case TV6_Layer.eV6_InternalPlane10:
                case TV6_Layer.eV6_InternalPlane11:
                case TV6_Layer.eV6_InternalPlane12:
                case TV6_Layer.eV6_InternalPlane13:
                case TV6_Layer.eV6_InternalPlane14:
                case TV6_Layer.eV6_InternalPlane15:
                case TV6_Layer.eV6_InternalPlane16:
                    height = ((IPCB_ElectricalLayer)layer_v7).GetState_CopperThickness();
                    this.IsElectrical = true;
                    break;
                //По умолчанию толщина слоя получается таковым образом
                default:
                    height = layer_v7.GetState_CopperThickness();
                    break;
            }
            return new AltiumHelper().CoordToMMs(height);
        }

        string GetTypeName()
        {
            return this.layer_v7.LayerID().ToString();
        }

        public string GetUniqueID()
        {
            return this.layer_v7.V7_LayerID().Ord.ToString();
        }
    }
}
