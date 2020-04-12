using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.CSharp.RuntimeBinder;
using SolidWorks.Interop.sldworks;
using SolidWorksAssemblySpace;

namespace PositionSpecification
{
	// Token: 0x02000003 RID: 3
	internal class AutomaticalPositionSpecification : AbstractPositions
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022F2 File Offset: 0x000004F2
		public AutomaticalPositionSpecification(SolidWorks.Interop.sldworks.ModelDoc2 swModel, Assembly assembly) : base(swModel, assembly)
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022FC File Offset: 0x000004FC
		protected override void SetConcreteBehavior()
		{
			this.updateLog = new List<string>();
			if (AutomaticalPositionSpecification.<>o__2.<>p__0 == null)
			{
				AutomaticalPositionSpecification.<>o__2.<>p__0 = CallSite<Func<CallSite, object, string[]>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(string[]), typeof(AutomaticalPositionSpecification)));
			}
			string[] array = AutomaticalPositionSpecification.<>o__2.<>p__0.Target(AutomaticalPositionSpecification.<>o__2.<>p__0, this.drawing.GetSheetNames());
			for (int i = 0; i < this.drawing.GetSheetCount(); i++)
			{
				Sheet sheet = this.drawing.get_Sheet(array[i]);
				if (AutomaticalPositionSpecification.<>o__2.<>p__1 == null)
				{
					AutomaticalPositionSpecification.<>o__2.<>p__1 = CallSite<Func<CallSite, object, object[]>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(object[]), typeof(AutomaticalPositionSpecification)));
				}
				AutomaticalPositionSpecification.<>o__2.<>p__1.Target(AutomaticalPositionSpecification.<>o__2.<>p__1, sheet.GetViews()).ToList<object>().ForEach(delegate(object itemView)
				{
					this.UpdateViewPositions((View)itemView);
				});
			}
			this.WriteLog();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023E8 File Offset: 0x000005E8
		private void WriteLog()
		{
			INotesCAD notesCAD = new SolidworksNotesCAD(this.swModel);
			notesCAD.WriteNotes(new Notes(DateTime.Now.ToString("Обновление позиций: dd-MMMM-yyyy HH:mm:ss"), notesCAD)
			{
				RazdelNotesCollection = this.updateLog.ToArray()
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002434 File Offset: 0x00000634
		private void UpdateViewPositions(View itemView)
		{
			if (AutomaticalPositionSpecification.<>o__4.<>p__0 == null)
			{
				AutomaticalPositionSpecification.<>o__4.<>p__0 = CallSite<Func<CallSite, object, object[]>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(object[]), typeof(AutomaticalPositionSpecification)));
			}
			AutomaticalPositionSpecification.<>o__4.<>p__0.Target(AutomaticalPositionSpecification.<>o__4.<>p__0, itemView.GetNotes()).ToList<object>().ForEach(delegate(object itemNote)
			{
				this.RewriteNote((Note)itemNote, itemView);
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024B8 File Offset: 0x000006B8
		private void RewriteNote(Note itemNote, View view)
		{
			if (AutomaticalPositionSpecification.<>o__5.<>p__0 == null)
			{
				AutomaticalPositionSpecification.<>o__5.<>p__0 = CallSite<Func<CallSite, object, Annotation>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(Annotation), typeof(AutomaticalPositionSpecification)));
			}
			Annotation annotation = AutomaticalPositionSpecification.<>o__5.<>p__0.Target(AutomaticalPositionSpecification.<>o__5.<>p__0, itemNote.GetAnnotation());
			if (!itemNote.IsBomBalloon())
			{
				return;
			}
			SolidWorks.Interop.sldworks.Component2 componentFromAnnotation = this.GetComponentFromAnnotation(annotation);
			annotation.Style = 0;
			string positionFromComponent = base.GetPositionFromComponent(componentFromAnnotation, view);
			string text = itemNote.GetText();
			string text2 = this.NoteNumber(positionFromComponent, text);
			itemNote.SetText(text2);
			this.updateLog.Add(string.Concat(new string[]
			{
				text,
				"=>",
				text2,
				" : ",
				(componentFromAnnotation != null) ? componentFromAnnotation.Name2 : "[component is not]"
			}));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000258C File Offset: 0x0000078C
		private SolidWorks.Interop.sldworks.Component2 GetComponentFromAnnotation(Annotation annotation)
		{
			if (AutomaticalPositionSpecification.<>o__6.<>p__0 == null)
			{
				AutomaticalPositionSpecification.<>o__6.<>p__0 = CallSite<Func<CallSite, object, object[]>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(object[]), typeof(AutomaticalPositionSpecification)));
			}
			object[] array = AutomaticalPositionSpecification.<>o__6.<>p__0.Target(AutomaticalPositionSpecification.<>o__6.<>p__0, annotation.GetAttachedEntities3());
			Entity entity = null;
			if (array != null)
			{
				foreach (Entity entity in array)
				{
					if (entity == null)
					{
						break;
					}
				}
			}
			if (AutomaticalPositionSpecification.<>o__6.<>p__1 == null)
			{
				AutomaticalPositionSpecification.<>o__6.<>p__1 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.Component2>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SolidWorks.Interop.sldworks.Component2), typeof(AutomaticalPositionSpecification)));
			}
			return AutomaticalPositionSpecification.<>o__6.<>p__1.Target(AutomaticalPositionSpecification.<>o__6.<>p__1, (entity == null) ? null : entity.GetComponent());
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000264C File Offset: 0x0000084C
		private string NoteNumber(string number, string textNote)
		{
			if (textNote.Length > 0)
			{
				textNote = textNote.Replace("[N/A]", string.Empty).Replace("?", string.Empty);
			}
			Match match = new Regex("^[\\d]+").Match(textNote);
			match = new Regex("[^\\d]").Match(textNote);
			string text = "";
			if (match.Success)
			{
				text = textNote.Substring(match.Index, textNote.Length - match.Index);
			}
			return (number + " " + text.Trim()).Trim();
		}

		// Token: 0x04000006 RID: 6
		private List<string> updateLog;
	}
}
