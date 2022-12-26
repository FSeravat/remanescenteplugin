using Autodesk.Navisworks.Api;


namespace NWDTierExporter.Util
{
    public class Basic
    {

        public ModelItemCollection EnumToCollection(ModelItemEnumerableCollection modelItems)
        {

            ModelItemCollection modColl = new ModelItemCollection();

            foreach (ModelItem model in modelItems)
            {
                modColl.Add(model);
            }
            
            return modColl;
        }

    }
}
