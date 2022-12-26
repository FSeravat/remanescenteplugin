using Autodesk.Navisworks.Api;
using ClashMarcacao.Util;
using Application = Autodesk.Navisworks.Api.Application;
using System;
using System.Windows.Controls.Primitives;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ClashMarcacao
{
    public partial class Manual
    {
        System.Windows.Controls.ListView list;
        System.Windows.Controls.CheckBox checkbox;
        public void marker_Click()
        {
            try
            {
                ManualWindow manual = new ManualWindow();
                manual.manualWindow_Loaded();
                list = manual.listObj;
                checkbox = manual.Propagar;
                Application.ActiveDocument.CurrentSelection.Changed += CurrentSelection_Changed;
                manual.Unloaded += StopSelection;
            }
            catch (Exception ex)
            {

            }
        }

        private void StopSelection(object sender, EventArgs e)
        {
            Application.ActiveDocument.CurrentSelection.Changed -= CurrentSelection_Changed;
        }

        private void CurrentSelection_Changed(object sender, EventArgs e)
        {
            PropertyManager pm = new PropertyManager();
            string value = "";
            foreach (System.Windows.Controls.RadioButton r in list.Items)
            {
                if (r.IsChecked == true) value = r.Content.ToString();
            }
            if (value == "")
            {
                MessageBox.Show("Por favor, selecione uma opção.");
                return;
            }
            if (checkbox.IsChecked == true)
            {
                foreach (ModelItem item in Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentSelection.SelectedItems.DescendantsAndSelf)
                {
                    pm.PropertyInsertion("Marcado", "Status", value, item);
                    pm.PropertyInsertion("Marcado", "Modo", "Manual", item);
                }
            }
            else
            {
                foreach (ModelItem item in Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentSelection.SelectedItems)
                {
                    pm.PropertyInsertion("Marcado", "Status", value, item);
                    pm.PropertyInsertion("Marcado", "Modo", "Manual", item);
                }
            }
            
        }
    }
}