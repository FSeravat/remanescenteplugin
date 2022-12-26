using Autodesk.Navisworks.Api;
using System.Collections.Generic;
using ComApiBridge = Autodesk.Navisworks.Api.Interop.ComApi;
using ComApi = Autodesk.Navisworks.Api.Interop.ComApi;
using ComApiBridge2 = Autodesk.Navisworks.Api.ComApi.ComApiBridge;
using System;
using System.Windows.Forms;
using Application = Autodesk.Navisworks.Api.Application;

namespace ClashMarcacao.Util
{
    class PropertyManager
    {
        private ComApi.InwOpState10 _oState = ComApiBridge2.State;
        private Search _searchCond = new Search();
        protected Dictionary<string, ComApiBridge.InwOaPropertyVec> newPropVec { get; set; } = new Dictionary<string, ComApiBridge.InwOaPropertyVec>();
        protected string keyName { get; set; } = "0";
        protected Dictionary<string, int> _userCatCollection = new Dictionary<string, int>();

        public void PropertyInsertion(string catName, string propName, string propValue, ModelItem selModel)
        {
            int _index = 1;
            ComApi.InwOaPath convModel = ComApiBridge2.ToInwOaPath(selModel);
            ComApiBridge.InwGUIPropertyNode2 propCat = (ComApiBridge.InwGUIPropertyNode2)_oState.GetGUIPropertyNode(convModel, true);
            ComApiBridge.InwGUIAttributesColl propCol = propCat.GUIAttributes();

            foreach (ComApiBridge.InwGUIAttribute2 attr in propCol)
            {
                if (attr.UserDefined)
                {
                    if (attr.ClassUserName == catName)
                    {
                        try {
                            propCat.SetUserDefined(_index, catName, $"{catName}_InternalName", AddNewPropertyToExtgCategory(attr, propName, propValue));
                            return;
                        }
                        catch(Exception e)
                        {
                            MessageBox.Show("Houve um problema ao tentar criar a categoria");
                            return;
                        }
                    }
                    _index++;
                }
            }

            FirstPropertyInsertion(catName, propName, propValue, new ModelItemCollection() { selModel }, 0);
            

        }

        private ComApiBridge.InwOaPropertyVec AddNewPropertyToExtgCategory(ComApiBridge.InwGUIAttribute2 propertCat, string PropertyName, string PropertyValue)
        {
            ComApiBridge.InwOpState10 cdoc = ComApiBridge2.State;
            ComApiBridge.InwOaPropertyVec category = (ComApiBridge.InwOaPropertyVec)cdoc.ObjectFactory(ComApiBridge.nwEObjectType.eObjectType_nwOaPropertyVec, null, null);

            foreach (ComApiBridge.InwOaProperty property in propertCat.Properties())
            {
                ComApiBridge.InwOaProperty extgProp = (ComApiBridge.InwOaProperty)cdoc.ObjectFactory(ComApiBridge.nwEObjectType.eObjectType_nwOaProperty, null, null);

                extgProp.name = property.name;
                extgProp.UserName = property.UserName;
                extgProp.value = property.value;


                if (extgProp.UserName != PropertyName) category.Properties().Add(extgProp);

            }
            
            ComApiBridge.InwOaProperty newProp = (ComApiBridge.InwOaProperty)cdoc.ObjectFactory(ComApiBridge.nwEObjectType.eObjectType_nwOaProperty, null, null);
            newProp.name = $"{PropertyName}_Internal";
            newProp.UserName = PropertyName;
            newProp.value = PropertyValue;
            category.Properties().Add(newProp);
            
            return category;
        }


        protected void FirstPropertyInsertion(string catName, string propName, string propValue, ModelItemCollection selectionModel, int index)
        {
            try
            {
                if (selectionModel.Count > 0)
                {

                    ComApi.InwOpSelection comSelectionOut = ComApiBridge2.ToInwOpSelection(selectionModel);

                    //create a new propety and add it to the category
                    ComApi.InwOaProperty customProp = (ComApi.InwOaProperty)_oState.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwOaProperty, null, null);

                    customProp.name = $"{catName}_Internal";
                    customProp.UserName = propName;
                    customProp.value = propValue;

                    #region Creating a New PropVector

                    ComApi.InwOaPropertyVec _propVector = (ComApi.InwOaPropertyVec)_oState.ObjectFactory(ComApi.nwEObjectType.eObjectType_nwOaPropertyVec, null, null);

                    #endregion

                    _propVector.Properties().Add(customProp);


                    foreach (ComApi.InwOaPath3 oPath in comSelectionOut.Paths())
                    {
                        ComApi.InwOaPath3 oPath1 = oPath;

                        ComApi.InwGUIPropertyNode2 propn = (ComApi.InwGUIPropertyNode2)_oState.GetGUIPropertyNode(oPath1, true);

                        //add the new category to the object
                        propn.SetUserDefined(index, catName, $"{catName}_Internal", _propVector);
                    }

                }
            }
            catch (Exception ex)
            {

            }

        }

        public void PropertyRemoval(ModelItemCollection selectionModel, int index)
        {

            if (selectionModel.Count > 0)
            {

                Autodesk.Navisworks.Api.Interop.ComApi.InwOpSelection comSelectionOut = ComApiBridge2.ToInwOpSelection(selectionModel);

                foreach (ComApi.InwOaPath3 oPath in comSelectionOut.Paths())
                {

                    ComApi.InwOaPath3 oPath1 = oPath;

                    ComApi.InwGUIPropertyNode2 propn = (ComApi.InwGUIPropertyNode2)_oState.GetGUIPropertyNode(oPath1, true);

                    //add the new category to the object
                    propn.RemoveUserDefined(index);

                }

            }
        }

        /// <summary>
        /// Retorna o valor da propriedade de um componente.
        /// </summary>
        /// <param name="modelUnit">Objeto de entrada</param>
        /// <param name="catDisplayName">Nome da categoria consultada</param>
        /// <param name="propDisplayName">Nome da propriedade consultada</param>
        /// <returns>Valor da propriedade dentro da categoria informada</returns>
        protected string PropertyValue(ModelItem modelUnit, string catDisplayName, string propDisplayName)
        {
            string propValue = modelUnit.PropertyCategories.FindPropertyByDisplayName(catDisplayName, propDisplayName)?.Value?.ToDisplayString();
            return propValue;
        }

        protected Dictionary<string, string> PropertyDict(ModelItem item, string category)
        {
            Dictionary<string, string> propDict = new Dictionary<string, string>();

            try
            {
                DataPropertyCollection dataColl = item.PropertyCategories.FindCategoryByDisplayName(category)?.Properties;

                if (dataColl != null)
                {
                    foreach (DataProperty dtProp in dataColl)
                    {
                        if (!propDict.ContainsKey(dtProp.DisplayName))
                        {
                            propDict.Add(dtProp.DisplayName, dtProp.Value.ToDisplayString());
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return propDict;
        }


        /// <summary>
        /// Retorna uma lista de componentes com prop. e cat. especificadas
        /// </summary>
        /// <param name="catName">nome da categoria a ser consultada</param>
        /// <param name="propName">nome da propriedade a ser consultada</param>
        /// <returns>lista de componentes que pertencem às cat. e prop. informadas</returns>
        public ModelItemCollection SearchEngine(string catName, string propName, string propValue)
        {

            _searchCond = new Search();
            _searchCond.Selection.SelectAll();
            _searchCond.PruneBelowMatch = false;
            SearchCondition searchObj = SearchCondition.HasPropertyByDisplayName(catName, propName);
            searchObj.DisplayStringWildcard(propValue);

            _searchCond.SearchConditions.Add(searchObj);

            //Execute Search
            ModelItemCollection searchResult = new ModelItemCollection();

            _searchCond.FindAll(Application.ActiveDocument, false).CopyTo(searchResult);

            return searchResult;
        }

    }
}