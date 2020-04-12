using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using SolidWorks.Interop.sldworks;

namespace PositionSpecification
{
	// Token: 0x02000004 RID: 4
	internal class ManualPositionSpecification : AbstractPositions
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000022F2 File Offset: 0x000004F2
		public ManualPositionSpecification(SolidWorks.Interop.sldworks.ModelDoc2 swModel, Assembly assembly) : base(swModel, assembly)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000026F4 File Offset: 0x000008F4
		protected override void SetConcreteBehavior()
		{
			if (ManualPositionSpecification.<>o__3.<>p__0 == null)
			{
				ManualPositionSpecification.<>o__3.<>p__0 = CallSite<Func<CallSite, object, ModelView>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(ModelView), typeof(ManualPositionSpecification)));
			}
			this.mouse = ManualPositionSpecification.<>o__3.<>p__0.Target(ManualPositionSpecification.<>o__3.<>p__0, this.swModel.ActiveView).GetMouse();
			if (ManualPositionSpecification.<>o__3.<>p__1 == null)
			{
				ManualPositionSpecification.<>o__3.<>p__1 = CallSite<Func<CallSite, object, SelectionMgr>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SelectionMgr), typeof(ManualPositionSpecification)));
			}
			this.selectManager = ManualPositionSpecification.<>o__3.<>p__1.Target(ManualPositionSpecification.<>o__3.<>p__1, this.swModel.SelectionManager);
			this.AttachSWEvents();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027B0 File Offset: 0x000009B0
		private void AttachSWEvents()
		{
			new ComAwareEventInfo(typeof(DDrawingDocEvents_Event), "UserSelectionPostNotify").AddEventHandler(this.drawing, new DDrawingDocEvents_UserSelectionPostNotifyEventHandler(this, (UIntPtr)ldftn(clickDrawingComponent)));
			new ComAwareEventInfo(typeof(DMouseEvents_Event), "MouseRBtnDownNotify").AddEventHandler(this.mouse, new DMouseEvents_MouseRBtnDownNotifyEventHandler(this, (UIntPtr)ldftn(rightMouseClick)));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002814 File Offset: 0x00000A14
		private int rightMouseClick(int X, int Y, int WParam)
		{
			new ComAwareEventInfo(typeof(DDrawingDocEvents_Event), "UserSelectionPostNotify").RemoveEventHandler(this.drawing, new DDrawingDocEvents_UserSelectionPostNotifyEventHandler(this, (UIntPtr)ldftn(clickDrawingComponent)));
			new ComAwareEventInfo(typeof(DMouseEvents_Event), "MouseRBtnDownNotify").RemoveEventHandler(this.mouse, new DMouseEvents_MouseRBtnDownNotifyEventHandler(this, (UIntPtr)ldftn(rightMouseClick)));
			return 0;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002878 File Offset: 0x00000A78
		private SolidWorks.Interop.sldworks.Component2 GetCurrentComponentFromDrawing()
		{
			if (ManualPositionSpecification.<>o__6.<>p__0 == null)
			{
				ManualPositionSpecification.<>o__6.<>p__0 = CallSite<Func<CallSite, object, DrawingComponent>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(DrawingComponent), typeof(ManualPositionSpecification)));
			}
			DrawingComponent drawingComponent = ManualPositionSpecification.<>o__6.<>p__0.Target(ManualPositionSpecification.<>o__6.<>p__0, this.selectManager.GetSelectedObjectsComponent4(1, -1));
			if (this.selectManager.GetSelectedObjectCount2(-1) == 2)
			{
				if (ManualPositionSpecification.<>o__6.<>p__1 == null)
				{
					ManualPositionSpecification.<>o__6.<>p__1 = CallSite<Func<CallSite, object, DrawingComponent>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(DrawingComponent), typeof(ManualPositionSpecification)));
				}
				drawingComponent = ManualPositionSpecification.<>o__6.<>p__1.Target(ManualPositionSpecification.<>o__6.<>p__1, this.selectManager.GetSelectedObjectsComponent4(2, -1));
			}
			if (drawingComponent != null)
			{
				return drawingComponent.Component;
			}
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000293C File Offset: 0x00000B3C
		private int clickDrawingComponent()
		{
			SolidWorks.Interop.sldworks.View activeDrawingView = this.GetActiveDrawingView();
			if (activeDrawingView == null)
			{
				return 0;
			}
			if (ManualPositionSpecification.<>o__7.<>p__0 == null)
			{
				ManualPositionSpecification.<>o__7.<>p__0 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.View>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(SolidWorks.Interop.sldworks.View), typeof(ManualPositionSpecification)));
			}
			DrawingComponent drawingComponent;
			base.GetCurrentAssemblyConfiguration(ManualPositionSpecification.<>o__7.<>p__0.Target(ManualPositionSpecification.<>o__7.<>p__0, this.drawing.ActiveDrawingView), out drawingComponent);
			SolidWorks.Interop.sldworks.Component2 currentComponentFromDrawing = this.GetCurrentComponentFromDrawing();
			string positionFromComponent = base.GetPositionFromComponent(currentComponentFromDrawing, activeDrawingView);
			this.InsertPosition(currentComponentFromDrawing, positionFromComponent);
			return 0;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000029C4 File Offset: 0x00000BC4
		private void InsertPosition(SolidWorks.Interop.sldworks.Component2 currentComponent, string position)
		{
			if (ManualPositionSpecification.<>o__8.<>p__0 == null)
			{
				ManualPositionSpecification.<>o__8.<>p__0 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(SolidWorks.Interop.sldworks.ModelDoc2), typeof(ManualPositionSpecification)));
			}
			ModelDocExtension extension = ManualPositionSpecification.<>o__8.<>p__0.Target(ManualPositionSpecification.<>o__8.<>p__0, currentComponent.GetModelDoc2()).Extension;
			if (ManualPositionSpecification.<>o__8.<>p__1 == null)
			{
				ManualPositionSpecification.<>o__8.<>p__1 = CallSite<Func<CallSite, object, double[]>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(double[]), typeof(ManualPositionSpecification)));
			}
			ManualPositionSpecification.<>o__8.<>p__1.Target(ManualPositionSpecification.<>o__8.<>p__1, this.selectManager.GetSelectionPoint2(1, -1));
			BalloonOptions ballonOptions = base.GetBallonOptions(extension);
			ballonOptions.LowerText = position;
			this.swModel.Extension.InsertBOMBalloon2(ballonOptions).SetText(position);
			this.swModel.GraphicsRedraw2();
			this.swModel.ClearSelection2(true);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002AAC File Offset: 0x00000CAC
		private SolidWorks.Interop.sldworks.View GetActiveDrawingView()
		{
			if (ManualPositionSpecification.<>o__9.<>p__0 == null)
			{
				ManualPositionSpecification.<>o__9.<>p__0 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.View>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SolidWorks.Interop.sldworks.View), typeof(ManualPositionSpecification)));
			}
			SolidWorks.Interop.sldworks.View view = ManualPositionSpecification.<>o__9.<>p__0.Target(ManualPositionSpecification.<>o__9.<>p__0, this.drawing.ActiveDrawingView);
			if (view == null)
			{
				this.swModel.ForceRebuild3(true);
				if (ManualPositionSpecification.<>o__9.<>p__1 == null)
				{
					ManualPositionSpecification.<>o__9.<>p__1 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.View>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(SolidWorks.Interop.sldworks.View), typeof(ManualPositionSpecification)));
				}
				view = ManualPositionSpecification.<>o__9.<>p__1.Target(ManualPositionSpecification.<>o__9.<>p__1, this.drawing.ActiveDrawingView);
			}
			if (view == null)
			{
				MessageBox.Show("DrawingView is not selected. Please, click to component in drawing again.", "DrawingView is null", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			return view;
		}

		// Token: 0x04000007 RID: 7
		private SelectionMgr selectManager;

		// Token: 0x04000008 RID: 8
		private Mouse mouse;
	}
}
