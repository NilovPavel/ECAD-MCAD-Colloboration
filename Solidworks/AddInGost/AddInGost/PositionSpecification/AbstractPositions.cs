using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using SolidWorks.Interop.sldworks;

namespace PositionSpecification
{
	// Token: 0x02000002 RID: 2
	internal abstract class AbstractPositions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AbstractPositions(SolidWorks.Interop.sldworks.ModelDoc2 swModel, Assembly assembly)
		{
			this.swModel = swModel;
			this.assembly = assembly;
			this.Intialization();
			if (this.drawing == null)
			{
				return;
			}
			this.SetConcreteBehavior();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207B File Offset: 0x0000027B
		protected BalloonOptions GetBallonOptions(ModelDocExtension swDocDocExt)
		{
			BalloonOptions balloonOptions = swDocDocExt.CreateBalloonOptions();
			balloonOptions.Style = 0;
			balloonOptions.UpperTextContent = 0;
			return balloonOptions;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		private string GetCorrectComponentName(string fullName, DrawingComponent rootComponent)
		{
			string name = rootComponent.Component.Name2;
			string[] array;
			if (!fullName.Contains('/'))
			{
				(array = new string[1])[0] = fullName;
			}
			else
			{
				array = fullName.Split(new char[]
				{
					'/'
				});
			}
			string[] array2 = array;
			return (array2.Length > 1 && array2[0].Contains(name)) ? array2[1] : array2[0];
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020F4 File Offset: 0x000002F4
		protected string GetPositionFromComponent(SolidWorks.Interop.sldworks.Component2 currentComponent, SolidWorks.Interop.sldworks.View currentView)
		{
			string result = "[N/A]";
			string currentAssemblyConfiguration = this.GetCurrentAssemblyConfiguration(currentView, out this.currentRootDrawingComponent);
			if (this.currentRootDrawingComponent.Component.Equals(currentComponent) || currentComponent == null)
			{
				return result;
			}
			string correctComponentName = this.GetCorrectComponentName(currentComponent.Name2, this.currentRootDrawingComponent);
			return this.GetElementPosition(correctComponentName, currentAssemblyConfiguration).ToString();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002154 File Offset: 0x00000354
		private int GetElementPosition(string componentFirstLevelIdentifer, string assemblyConfigurationName)
		{
			Variant variant = (from variantItem in this.assembly.variantCAD.variant
			where variantItem.VariantName.Equals(assemblyConfigurationName)
			select variantItem).FirstOrDefault<Variant>();
			if (variant == null)
			{
				return 0;
			}
			ComponentCAD componentCAD = (from componentItem in variant.ComponentCAD
			where componentItem.UniqueID.Equals(componentFirstLevelIdentifer)
			select componentItem).FirstOrDefault<ComponentCAD>();
			if (componentCAD == null)
			{
				return 0;
			}
			return componentCAD.dataESKD.Позиция;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021D0 File Offset: 0x000003D0
		protected string GetCurrentAssemblyConfiguration(SolidWorks.Interop.sldworks.View activeDrawingView, out DrawingComponent currentRootComponent)
		{
			currentRootComponent = activeDrawingView.RootDrawingComponent;
			if (AbstractPositions.<>o__10.<>p__0 == null)
			{
				AbstractPositions.<>o__10.<>p__0 = CallSite<Func<CallSite, object, SolidWorks.Interop.sldworks.ModelDoc2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof(SolidWorks.Interop.sldworks.ModelDoc2), typeof(AbstractPositions)));
			}
			SolidWorks.Interop.sldworks.ModelDoc2 modelDoc = AbstractPositions.<>o__10.<>p__0.Target(AbstractPositions.<>o__10.<>p__0, currentRootComponent.Component.GetModelDoc2());
			this.currentAssemblyDoc = ((modelDoc.GetType() == 2) ? ((SolidWorks.Interop.sldworks.AssemblyDoc)modelDoc) : null);
			if (activeDrawingView.IsLightweight())
			{
				activeDrawingView.SetLightweightToResolved();
			}
			if (AbstractPositions.<>o__10.<>p__1 == null)
			{
				AbstractPositions.<>o__10.<>p__1 = CallSite<Func<CallSite, object, Configuration>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(Configuration), typeof(AbstractPositions)));
			}
			return AbstractPositions.<>o__10.<>p__1.Target(AbstractPositions.<>o__10.<>p__1, modelDoc.GetActiveConfiguration()).Name;
		}

		// Token: 0x06000007 RID: 7
		protected abstract void SetConcreteBehavior();

		// Token: 0x06000008 RID: 8 RVA: 0x000022A4 File Offset: 0x000004A4
		private void Intialization()
		{
			if (!this.swModel.GetType().Equals(3))
			{
				MessageBox.Show("Данный документ не является чертежом!", "Ошибка инициализации", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
				return;
			}
			this.drawing = (DrawingDoc)this.swModel;
		}

		// Token: 0x04000001 RID: 1
		private Assembly assembly;

		// Token: 0x04000002 RID: 2
		protected SolidWorks.Interop.sldworks.ModelDoc2 swModel;

		// Token: 0x04000003 RID: 3
		protected DrawingDoc drawing;

		// Token: 0x04000004 RID: 4
		protected SolidWorks.Interop.sldworks.AssemblyDoc currentAssemblyDoc;

		// Token: 0x04000005 RID: 5
		protected DrawingComponent currentRootDrawingComponent;
	}
}
