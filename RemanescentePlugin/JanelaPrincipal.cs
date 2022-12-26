using Autodesk.Navisworks.Api.Plugins;
using MedicaoPlugin;
using NWD_Tier_Exporter;

namespace Navis2P3dMigration
{
    [Plugin("ClashMark", "ADSK", DisplayName = "Clash Util")]
    [RibbonLayout("AddinRibbon.xaml")]
    [RibbonTab("ID_CustomTab_1", DisplayName = "Remanescente")]
    [Command("ID_Button_1", Icon="1_16.png", LargeIcon="1_32.png", ToolTip = "Main Menu")]
    [Command("ID_Button_2", Icon = "2_16.png", LargeIcon = "2_32.png", ToolTip = "Main Menu")]
    [Command("ID_Button_3", Icon = "2_16.png", LargeIcon = "2_32.png", ToolTip = "Main Menu")]

    public class Janela : CommandHandlerPlugin
    {
        public Medicao medir = new Medicao();
        public ClashMarcacao.Manual manual = new ClashMarcacao.Manual();
        public JanelaPrincipal nwd = new JanelaPrincipal();
        public static DockPanePlugin clashPane { get; set; } //Janela do Clash Detective
        public override int ExecuteCommand(string name, params string[] parameters)
        {
            if (nwd == null || nwd.IsDisposed)
            {
                nwd = new JanelaPrincipal();
                nwd.Hide();
            }
            {
                nwd.BringToFront();
            }

            switch (name)
            {
                case "ID_Button_1":
                    if (!Autodesk.Navisworks.Api.Application.IsAutomated)
                    {
                        manual.marker_Click();
                    }
                    break;

                case "ID_Button_2":
                    if (!Autodesk.Navisworks.Api.Application.IsAutomated)
                    {
                        medir.adicionarMedidas();
                    }
                    break;

                case "ID_Button_3":
                    if (!Autodesk.Navisworks.Api.Application.IsAutomated)
                    {
                        nwd.Show();
                    }
                    break;
            }

            return 0;
        }

    }
}