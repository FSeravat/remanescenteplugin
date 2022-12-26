using Autodesk.Navisworks.Api;
using System.Windows;
using ClashMarcacao.Util;

namespace MedicaoPlugin
{
    public partial class Medicao
    {
        PropertyManager pm = new PropertyManager();
        public void adicionarMedidas()
        {
            var selection = Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentSelection.SelectedItems;
            if (selection.Count != 1)
            {
                MessageBox.Show("O Plugin exige que apenas 1 item seja selecionado");
            }
            else
            {
                var medida = Autodesk.Navisworks.Api.Application.ActiveDocument.CurrentMeasurement;
                if (!medida.HasMeasurement)
                {
                    MessageBox.Show("O Plugin exige que seja feita uma medida");
                }
                else
                {
                    ModelItem item = selection[0];
                    var tamanho = item.PropertyCategories.FindPropertyByDisplayName("PDMS1", ":TUB_COMP")?.Value?.ToDisplayString();
                    if(tamanho == null)
                    {
                        MessageBox.Show("O tamanho do tubo não foi encontrado.");
                    }
                    else
                    {
                        tamanho = tamanho.Replace("mm", "");
                        tamanho = tamanho.Replace(".", ",");
                        double resultado = (double.Parse(tamanho)/1000) - medida.MeasurementValue;
                        pm.PropertyInsertion("Marcado", "Tam_Res", medida.MeasurementValue.ToString(), item);
                        pm.PropertyInsertion("Marcado", "Status", "Parcialmente Montado (PM)", item);
                        pm.PropertyInsertion("Marcado", "Modo", "Manual", item);
                        pm.PropertyInsertion("Marcado", "Tam_Mon", resultado.ToString(), item);
                    }
                }
                
            }
        }
    }
}