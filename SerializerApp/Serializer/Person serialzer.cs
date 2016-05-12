using System.Windows.Forms;

namespace Serializer
{

    //Needs to check if the folder exists and if there are valid files in it.
    //Valid files are Person[digit][digit] (eg. Person01).

    public partial class PersonSerialzer : Form
    {
        public PersonSerialzer()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            //Serialize Person object.
        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            //Deserialze Person object (previous - if it is not the first object).
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            //Deserialze Person object (next - if it si not the last object).
        }
    }
}
