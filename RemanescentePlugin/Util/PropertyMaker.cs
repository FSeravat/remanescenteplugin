using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.ComApi;
using NWDTierExporter;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ComApi = Autodesk.Navisworks.Api.Interop.ComApi;

namespace ClashMarcacao.Util
{
    class PropertyMarker : PropertyManager
    {
        private ComApi.InwOpState10 _oState = ComApiBridge.State;
        private ComApi.InwOaPropertyVec _propVector;


        private ProgressMonitor progressWindow = new ProgressMonitor();

        public PropertyMarker()
        {
            _propVector = (ComApi.InwOaPropertyVec)_oState.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwOaPropertyVec, null, null);

        }

        public void ItemMarker(ModelItemCollection components, string CategoryName, string propertyObj, string propertyValue)
        {
            try
            {
                int idProv = 0;

                if (progressWindow != null || progressWindow.IsDisposed)
                {
                    progressWindow = new ProgressMonitor();
                }

                progressWindow.Show();
                progressWindow.BringToFront();

                ProgressBarSetter(progressWindow.pgBar, components.Count);

                foreach (ModelItem comp in components)
                {
                    progressWindow.pgBar.PerformStep();

                    if (!newPropVec.ContainsKey(idProv.ToString())) newPropVec.Add(idProv.ToString(), _propVector);

                    PropertyInsertion(CategoryName, propertyObj, propertyValue, comp);
                }

                progressWindow.Close();
            }
            catch (Exception e)
            {
                if (progressWindow != null || !progressWindow.IsDisposed) progressWindow.Close();
            }

        }

        private void ProgressBarSetter(ProgressBar pgBar, int length)
        {
            pgBar.Visible = true;
            pgBar.Minimum = 1;
            pgBar.Value = 1;
            pgBar.Step = 1;
            pgBar.Maximum = length;
        }
    }
}
