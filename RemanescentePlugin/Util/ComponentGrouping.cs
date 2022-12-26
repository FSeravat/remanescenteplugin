using Autodesk.Navisworks.Api;

namespace Navis2P3dMigration
{
    public class ComponentGrouping
    {
        private Search searchCond = new Search();


        /// <summary>
        /// Retorna uma lista de componentes com prop. e cat. especificadas
        /// </summary>
        /// <param name="catName">nome da categoria a ser consultada</param>
        /// <param name="propName">nome da propriedade a ser consultada</param>
        /// <returns>lista de componentes que pertencem às cat. e prop. informadas</returns>
        public ModelItemCollection SearchEngine(string catName, string propName, string propValue)
        {
            searchCond = new Search();
            searchCond.PruneBelowMatch = false;

            SearchCondition searchObj = SearchCondition.HasPropertyByDisplayName(catName, propName);
            searchObj = searchObj.DisplayStringContains(propValue); //Will search for all objects with the specified property value
            searchCond.Selection.SelectAll();
            searchCond.SearchConditions.Add(searchObj);

            //Execute Search
            ModelItemCollection searchResult = searchCond.FindAll(Application.ActiveDocument, false);
            
            return searchResult;
        }

        public ModelItemCollection SearchEngine(string catName, string propName)
        {
            searchCond = new Search();

            searchCond.PruneBelowMatch = false;

            SearchCondition searchObj = SearchCondition.HasPropertyByDisplayName(catName, propName);
            searchCond.Selection.SelectAll();
            searchCond.SearchConditions.Add(searchObj);
            
            //Execute Search
            ModelItemCollection searchResult = searchCond.FindAll(Application.ActiveDocument, false);
            
            return searchResult;
        }

        public ModelItemCollection SearchEngineFromSelection(string catName, string propName, ModelItemCollection modelColl)
        {
            searchCond = new Search();

            searchCond.PruneBelowMatch = false;

            SearchCondition searchObj = SearchCondition.HasPropertyByDisplayName(catName, propName);
            searchCond.Selection.CopyFrom(modelColl);
            searchCond.SearchConditions.Add(searchObj);

            //Execute Search
            ModelItemCollection searchResult = searchCond.FindAll(Application.ActiveDocument, false);

            return searchResult;
        }


        public ModelItemCollection SearchBooleanValue(string catName, string propName, bool propValue)
        {
            searchCond = new Search();
            SearchCondition searchObj = SearchCondition.HasPropertyByDisplayName(catName, propName);
            searchObj = searchObj.EqualValue(VariantData.FromBoolean(propValue)); //Will search for all objects with the specified property value
            
            searchCond.Selection.SelectAll();
            searchCond.SearchConditions.Add(searchObj);

            //Execute Search
            
            ModelItemCollection searchResult = searchCond.FindAll(Application.ActiveDocument, false);

            return searchResult;
        }


        /// <summary>
        /// Retorna o valor da propriedade de um componente.
        /// </summary>
        /// <param name="modelUnit">Objeto de entrada</param>
        /// <param name="catDisplayName">Nome da catedoria consultada</param>
        /// <param name="propDisplayName">Nome da propriedade consultada</param>
        /// <returns>Valor da propriedade, dentro da categoria, informada</returns>
        public string PropertyValue(ModelItem modelUnit, string catDisplayName, string propDisplayName)
        {
            string propValue = modelUnit.PropertyCategories.FindPropertyByDisplayName(catDisplayName, propDisplayName)?.Value?.ToDisplayString();
            return propValue;
        }
    }
}
