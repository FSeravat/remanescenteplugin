using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Interop.ComApi;
using ClashMarcacao.Util;
using Navis2P3dMigration;
using NWD_Tier_Exporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NWDTierExporter.Util
{
    class HierarchyManager
    {
        private Basic _func = new Basic();
        private JanelaPrincipal mainWinref = JanelaPrincipal.mainWin;
        private PropertyGetter propGetter = new PropertyGetter();
        ModelItem customAncestLevel;
        public static ModelItemCollection StartHiddenCollection { get; set; }
        ComponentGrouping compGrouping = new ComponentGrouping();
        ModelItem prevParent;

        public HierarchyManager()
        {
            
        }

        private void ProgressBarSetter(ProgressBar pgBar, int length)
        {
            pgBar.Visible = true;
            pgBar.Minimum = 0;
            pgBar.Value = 0;
            pgBar.Step = 1;
            pgBar.Maximum = length;
        }


        public List<string> GetFullCollectionNames(ModelItem model)
        {
            ModelItemCollection discItems = _func.EnumToCollection(model.Children);
            List<string> discNames = new List<string>();

            foreach (ModelItem item in discItems)
            {
                discNames.Add(item.DisplayName);
            }

            return discNames;
        }

        public List<string[]> GetHierarchy(ModelItem elemento)
        {
            mainWinref = JanelaPrincipal.mainWin;
            ModelItemCollection docChildren = new ModelItemCollection();
            docChildren = _func.EnumToCollection(elemento.DescendantsAndSelf);
            List<string[]> childrenNames = new List<string[]>();

            //ProgressBar setup
            ProgressBarSetter(mainWinref.pgBar, docChildren.Count);

            List<string> branchPropBag = new List<string>();

            foreach (ModelItem docChild in docChildren)
            {
                mainWinref.pgBar.PerformStep();

                if (docChild.HasGeometry && docChild.Geometry != null && !docChild.IsHidden)
                {

                    List<string> allItems = new List<string>();

                    string[] ancestNames =  _func.EnumToCollection(docChild.AncestorsAndSelf).Select(item => item.DisplayName == "" ? item.ClassDisplayName : item.DisplayName).Reverse().ToArray();

                    //Dados gerais
                    string[] customLevelHier = new string[0];
                    /* customLevelHier = GetHierarchyProperties(docChild, ancestNames.Length, "Marcado"); */
                    customLevelHier = propGetter.HyperlinkGetter(docChild, "Marcado").ToArray();
                    allItems.AddRange(customLevelHier.ToList());

                    //Ancestralidade
                    allItems.AddRange(ancestNames.ToList());

                    //Propriedades gerais
                    customLevelHier = new string[0];
                    /* customLevelHier = GetHierarchyProperties(docChild, ancestNames.Length, "Marcado"); */
                    customLevelHier = propGetter.HyperlinkGetter(docChild, "PDMS").ToArray();
                    allItems.AddRange(customLevelHier.ToList());

                    //Dados de tubulação injetado
                    customLevelHier = new string[0];
                    /* customLevelHier = GetHierarchyProperties(docChild, ancestNames.Length, "PDMS1"); */
                    customLevelHier = propGetter.HyperlinkGetter(docChild, "PDMS1").ToArray();
                    allItems.AddRange(customLevelHier.ToList());

                    //Dados do Branch Pai
                    ModelItem newParent = docChild.Parent;

                    if (newParent != prevParent)
                    {
                        List<string> branchProps = propGetter.HyperlinkGetter(newParent, "PDMS");
                        branchPropBag = new List<string>();

                        foreach (string branchprop in branchProps)
                        {
                            if (branchprop.Contains("HBOR"))
                            {
                                
                                allItems.Add(branchprop);
                                branchPropBag.Add(branchprop);
                            }

                            if (branchprop.Contains("TBOR"))
                            {
                                allItems.Add(branchprop);
                                branchPropBag.Add(branchprop);
                            }
                        }
                    }
                    else
                    {
                        if(branchPropBag != null && branchPropBag.Count > 0) allItems.AddRange(branchPropBag);
                    }
                    prevParent = docChild.Parent;

                    //Dados do 4° nível
                    if (ancestNames[1].Contains("EQU") || ancestNames[1].Contains("ELE") || ancestNames[1].Contains("TUB") || ancestNames[1].Contains("SEG"))
                    {
                        if(ancestNames.Length > 4)
                        {
                            //Quarto Nível
                            customLevelHier = new string[0];
                            customLevelHier = GetHierarchyProperties(docChild, 4, "PDMS");
                            allItems.AddRange(customLevelHier.ToList());
                        }
                    }

                    //Dados do 6° nível
                    if (ancestNames[1].Contains("ELE") || ancestNames[1].Contains("TUB") || ancestNames[1].Contains("SEG"))
                    {
                        if(ancestNames.Length > 6)
                        {
                            customLevelHier = new string[0];
                            customLevelHier = GetHierarchyProperties(docChild, 6, "PDMS");
                            allItems.AddRange(customLevelHier.ToList());
                        }
                    }

                    //Compilado
                    childrenNames.Add(allItems.ToArray());
                }
            }

            return childrenNames;
        }


        public void SetHiddenChildrenGroup(ModelItemCollection modelItems, bool condition)
        {
            ModelItemCollection hiddenCollection = new ModelItemCollection();

            ModelItemCollection dummyCollection = new ModelItemCollection();

            hiddenCollection = compGrouping.SearchBooleanValue("Item", "Hidden", condition);

            StartHiddenCollection = hiddenCollection;

            foreach (ModelItem model in hiddenCollection)
            {
                dummyCollection = _func.EnumToCollection(model.DescendantsAndSelf);

                Autodesk.Navisworks.Api.Application.ActiveDocument.Models.SetHidden(dummyCollection, condition);
                
            }
        }

        public void SetHiddenGroup(ModelItemCollection modelItems, bool condition)
        {
            Autodesk.Navisworks.Api.Application.ActiveDocument.Models.SetHidden(modelItems, condition);
        }

        private string[] GetHierarchyProperties(ModelItem elemento, int levels, string property)
        {

            ModelItemCollection elmenAncest = new ModelItemCollection();
            elmenAncest.CopyFrom(_func.EnumToCollection(elemento.AncestorsAndSelf).Reverse().ToArray());


            try
            {
                customAncestLevel = elmenAncest[levels - 1];
            }
            catch(Exception ex)
            {
                return new string[0];
            }


            return propGetter.HyperlinkGetter(customAncestLevel, property).ToArray();
        }


    }
}
