using Autodesk.Navisworks.Api;
using NWDTierExporter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWDTierExporter
{
    class PDMSTubePropertyContainer
    {
        ModelItem _model;

        PropertyGetter _propGetter = new PropertyGetter();

        Basic _basic = new Basic();

        public PDMSTubePropertyContainer(ModelItem model)
        {
            _model = model;
        }


        public Dictionary<string, string> ContainerProperties
        {
            get
            {
                return _propGetter.HyperlinkGetterDict(_model, "PDMS");

            }
        }

        public ModelItem ModelObj
        {
            get
            {
                return _model;
            }
        }

        public ModelItem ModelSuccessor
        {
            get
            {
                return GetSuccessor(_model);
            }
        }

        public bool IsPipe 
        {
            get
            {
                return IsSuccModelPipe(ModelSuccessor);
            }
        }


        private ModelItem GetSuccessor(ModelItem modelItem)
        {
            ModelItemCollection models = _basic.EnumToCollection(modelItem.Children);

            List<ModelItem> ancCollection = _basic.EnumToCollection(_basic.EnumToCollection(modelItem.Ancestors)[0].Children).ToList();

            int nextModelPos = ancCollection.IndexOf(modelItem) + 1;
            

            if (models.Count > 1)
            {
                return models[0];
            }
            else
            {
                return ancCollection[nextModelPos];
            }
        }

        private bool IsSuccModelPipe(ModelItem modelItem)
        {
            Dictionary<string, string> succProps = _propGetter.HyperlinkGetterDict(modelItem, "Item");
            
            string succFirstName = "";

            if (succProps.ContainsKey("Name"))
            {
                succFirstName = succProps["Name"].Split(new String[] { " " }, StringSplitOptions.None)[0].Trim();

                if (succFirstName == "TUBE") return true;

            }

            return false;
        }
        

    }
}
