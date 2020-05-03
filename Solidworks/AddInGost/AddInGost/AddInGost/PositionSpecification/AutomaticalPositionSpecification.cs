using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidWorksAssemblySpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PositionSpecification
{
    class AutomaticalPositionSpecification : AbstractPositions
    {
        private List<string> updateLog;

        public AutomaticalPositionSpecification(ModelDoc2 swModel, Assembly assembly) : base(swModel, assembly)
        {
        }

        protected override void SetConcreteBehavior()
        {
            this.updateLog = new List<string>();
            string[] sheetNames = (string[])this.drawing.GetSheetNames();
            for (int sheetCount = 0; sheetCount < this.drawing.GetSheetCount(); sheetCount++)
            {
                Sheet currentSheet = this.drawing.get_Sheet(sheetNames[sheetCount]);
                Object[] view = currentSheet.GetViews();
                view.ToList().ForEach(itemView => this.UpdateViewPositions((SolidWorks.Interop.sldworks.View)itemView));
            }
            this.WriteLog();
        }

        private void WriteLog()
        {
            INotesCAD noteWriter = new SolidworksNotesCAD(this.swModel);
            string noteName = DateTime.Now.ToString("Обновление позиций: dd-MMMM-yyyy HH:mm:ss");
            Notes logNote = new Notes(noteName, noteWriter);
            logNote.RazdelNotesCollection = this.updateLog.ToArray();
            noteWriter.WriteNotes(logNote);
        }

        private void UpdateViewPositions(SolidWorks.Interop.sldworks.View itemView)
        {
            //DrawingComponent currentRootComponent;
            //string currentAssemblyConfigurationName = this.GetCurrentAssemblyConfiguration(itemView, out currentRootComponent);
            ((object[])itemView.GetNotes()).ToList().ForEach(itemNote => this.RewriteNote((Note)itemNote, itemView));
        }

        private void RewriteNote(Note itemNote, SolidWorks.Interop.sldworks.View view)
        {
            Annotation annotation = (Annotation)itemNote.GetAnnotation();
            if (!itemNote.IsBomBalloon())
                return;
            Component2 component = this.GetComponentFromAnnotation(annotation);
            annotation.Style = (int)swBalloonStyle_e.swBS_None;
            string position = this.GetPositionFromComponent(component, view);
            string oldRecord = itemNote.GetText();
            string newRecord = this.NoteNumber(position, oldRecord);
            itemNote.SetText(newRecord);
            this.updateLog.Add( oldRecord + "=>" + newRecord + " : " + (component != null ? component.Name2 : "[component is not]"));
        }

        private Component2 GetComponentFromAnnotation(Annotation annotation)
        {
            object[] entitys = annotation.GetAttachedEntities3();
            Entity entity = default(Entity);
            if (entitys != null)
                foreach (object one_entity in entitys)
                {
                    entity = (Entity)one_entity;
                    if (entity == null)
                        break;
                }
            Component2 component = entity == null ? null : entity.GetComponent();
            return component;
        }

        private string NoteNumber(string number, string textNote)
        {
            if (textNote.Length > 0)
                textNote = textNote.Replace("[N/A]", string.Empty).Replace("?", string.Empty);

            Regex regex = new Regex("^[\\d]+");
            Match match = regex.Match(textNote);
            regex = new Regex("[^\\d]");
            match = regex.Match(textNote);
            string xvost = "";
            if (match.Success)
                xvost = textNote.Substring(match.Index, textNote.Length - match.Index);

            return (number + " " + xvost.Trim()).Trim();
        }
    }
}
