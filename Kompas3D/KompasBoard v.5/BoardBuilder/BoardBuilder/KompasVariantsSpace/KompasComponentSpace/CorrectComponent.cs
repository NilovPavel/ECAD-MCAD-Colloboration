namespace KompasComponentSpace
{
    internal class CorrectComponent
    {
        private ComponentCAD item;
        private DataESKD dataESKD;
        private Coordinats coordinats;

        private void Initialization()
        {
            this.dataESKD = this.item.dataESKD;
            this.coordinats = this.item.coordinats;
        }

        private bool isUncorrectData()
        {
            return this.coordinats == null || (new CorrectDataESKD(this.dataESKD)).IsUnCorrectESKD;
        }

        public CorrectComponent(ComponentCAD item)
        {
            this.item = item;
            this.Initialization();
        }

        public bool IsUnCorrectElement
        {            get { return this.isUncorrectData(); }        }
    }
}