using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NWDTierExporter
{
    class PropertyGetter
    {

        string _propName = "";
        string _propValue = "";
        List<string> _propArray = new List<string>();
        Dictionary<string, string> _propDict = new Dictionary<string, string>();

        public PropertyGetter() { }
        public List<string> HyperlinkGetter(ModelItem item, string Category)
        {
            try
            {
                PropertyCategory dataColl = item.PropertyCategories.ToList().Where(x => x.DisplayName == Category).ToList()[0];

                _propArray = new List<string>();

                foreach (DataProperty dtProp in dataColl.Properties)
                {
                    _propValue = TypeCompatibilization(dtProp.Value);
                    _propName = dtProp.DisplayName;

                    _propArray.Add($"{_propName}:{_propValue}");

                }
            }
            catch (Exception ex)
            {
                _propArray = new List<string>();
            }


            return _propArray;
        }

        public Dictionary<string, string> HyperlinkGetterDict(ModelItem item, string Category)
        {
            try
            {
                PropertyCategory dataColl = item.PropertyCategories.ToList().Where(x => x.DisplayName == Category).ToList()[0];

                _propDict = new Dictionary<string, string>();

                foreach (DataProperty dtProp in dataColl.Properties)
                {

                    _propValue = TypeCompatibilization(dtProp.Value);
                    _propName = dtProp.DisplayName;

                    _propDict.Add(_propName, _propValue);

                }
            }
            catch (Exception ex) { }


            return _propDict;
        }


        private string TypeCompatibilization(VariantData data)
        {

            if (data.IsAnyDouble) return data.ToAnyDouble().ToString();
            else if(data.IsBoolean) return data.ToBoolean().ToString();
            else if (data.IsDateTime) return data.ToDateTime().ToString();
            else if (data.IsDisplayString) return data.ToDisplayString().ToString();
            else if (data.IsDouble) return data.ToDouble().ToString();
            else if (data.IsDoubleAngle) return data.ToDoubleAngle().ToString();
            else if (data.IsDoubleArea) return data.ToDoubleArea().ToString();
            else if (data.IsDoubleLength) return data.ToDoubleLength().ToString();
            else if (data.IsDoubleVolume) return data.ToDoubleVolume().ToString();
            else if (data.IsIdentifierString) return data.ToIdentifierString().ToString();
            else if (data.IsInt32) return data.ToInt32().ToString();
            else if (data.IsNamedConstant) return data.ToNamedConstant().ToString();
            else if (data.IsPoint2D) return data.ToPoint2D().ToString();
            else if (data.IsPoint3D) return data.ToPoint3D().ToString();
            else if (data.IsReadOnly) return data.ToPoint3D().ToString();

            return "";

        }
    }
}
