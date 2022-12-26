using Autodesk.Navisworks.Api;
using NWDTierExporter;
using NWDTierExporter.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Application = Autodesk.Navisworks.Api.Application;

namespace NWD_Tier_Exporter
{
    public partial class JanelaPrincipal : Form
    {

        private HierarchyManager hierMan = new HierarchyManager();
        public static JanelaPrincipal mainWin;
        private FileOperations fileOp = new FileOperations();

        public JanelaPrincipal()
        {
            InitializeComponent();
            mainWin = this;
        }


        #region Events
     

        private void Individual_Click(object sender, EventArgs e)
        {
            try
            {

                ModelItemCollection modelItems = new ModelItemCollection();
                modelItems = Application.ActiveDocument.CurrentSelection.SelectedItems;


                if (modelItems.Count == 0)
                {
                    MessageBox.Show("Selecione pelo menos um componente na Selection Tree.");
                    return;
                }
                else
                {
                    List<string[]> hierarchies = new List<string[]>();
                    List<string[]> tempHier = new List<string[]>();
                    List<string> colItems = new List<string>();

                    foreach (ModelItem doc in modelItems)
                    {
                        #region Settar Ocultos

                        hierMan.SetHiddenChildrenGroup(new ModelItemCollection() { doc }, true); //Oculta componentes filhos

                        #endregion

                        tempHier = hierMan.GetHierarchy(doc);
                        hierarchies.AddRange(tempHier);

                        ModelItemCollection _startHiddenItems = HierarchyManager.StartHiddenCollection; //Armazena o pai que estava oculto inicialmente

                        hierMan.SetHiddenChildrenGroup(new ModelItemCollection() { doc }, false);
                        hierMan.SetHiddenGroup(_startHiddenItems, true);

                        fileOp.CsvCreator(hierarchies, doc.DisplayName, "NIVEL");

                        this.pgBar.Value = 0;
                    }

                }
            }
            catch(Exception ex)
            {

            }
            
        }

        #endregion

    }
}
