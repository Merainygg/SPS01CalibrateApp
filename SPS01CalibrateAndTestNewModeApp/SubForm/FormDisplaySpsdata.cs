using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Mode;

namespace SPS01CalibrateAndTestNewModeApp.SubForm
{
    public partial class FormDisplaySpsdata : Form
    {
        private readonly SpsCalibration _spsCalibration;
        public FormDisplaySpsdata()
        {
            InitializeComponent();
            _spsCalibration = ServiceContainer.Resolve<SpsCalibration>();
            
            // 将_spsCalibration中的数据输出到TextBox中
            TextBoxSpsdata.Text = _spsCalibration.ToJson();
        }

        
    }
}