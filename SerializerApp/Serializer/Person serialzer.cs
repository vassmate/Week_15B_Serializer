using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Serializer
{

    //Needs to check if the folder exists and if there are valid files in it.
    //Valid files are person[digit][digit].dat (eg. person01.dat).

    public partial class PersonSerialzer : Form
    {
        private int _tempId = 1;
        private readonly string _workingFolderPath = @"C:\serialized_persons";
        private string _fileName = "";
        private readonly IFormatter _binaryFormatter = new BinaryFormatter();
        private string[] _fileList;

        public PersonSerialzer()
        {
            InitializeComponent();
        }

        private void PersonSerialzer_Load(object sender, System.EventArgs e)
        {
            string msgBoxTitle = "Directory info";
            string noDirectoryMsg = "Directory created at:\n" + _workingFolderPath;
            string noFilesMsg = "There aren't any files to show in the folder:\n" + _workingFolderPath;

            if (!Directory.Exists(_workingFolderPath))
            {
                Directory.CreateDirectory(_workingFolderPath);
                MessageBox.Show(noDirectoryMsg, msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _fileList = Directory.GetFiles(_workingFolderPath);

                if (_fileList.Length == 0)
                {
                    MessageBox.Show(noFilesMsg, msgBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Person personFromFile = Deserialize(_binaryFormatter);

                    txtName.Text = personFromFile.Name;
                    txtAddress.Text = personFromFile.Address;
                    txtPhone.Text = personFromFile.Phone;
                    
                    txtPersonId.Text = _tempId.ToString();
                    txtSerialDate.Text = personFromFile.DateOfRecording.ToString();
                    _tempId += _fileList.Length;
                }
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string serialInfoTitle = "Serialization info";
            string serialWarnTitle = "Warning!";
            string serialWarnMsg = "Fields can't be empty!";

            if ((txtName.Text != "" || txtName.Text != " ") &&
                (txtAddress.Text != "" || txtAddress.Text != " ") &&
                (txtPhone.Text != "" || txtPhone.Text != " "))
            {
                Person newPerson = new Person(txtName.Text, txtAddress.Text, txtPhone.Text);
                Serialze(newPerson, _binaryFormatter);

                txtPersonId.Text = _tempId.ToString();
                txtSerialDate.Text = newPerson.DateOfRecording.ToString();
                _tempId++;

                MessageBox.Show($"Data serialized to:\n{_workingFolderPath}\\{_fileName}!",
                    serialInfoTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(serialWarnMsg, serialWarnTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            //Deserialze Person object (previous - if it is not the first object).
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            //Deserialze Person object (next - if it is not the last object).
        }

        private void Serialze(Person person, IFormatter iformatter)
        {
            _fileName = _tempId < 10 ? $"person{0}{_tempId}.dat" : $"person{_tempId}.dat";

            FileStream fileStream = new FileStream(_workingFolderPath + @"\" + _fileName, FileMode.Create);
            iformatter.Serialize(fileStream, person);
            fileStream.Close();
        }

        private Person Deserialize(IFormatter iformatter)
        {
            _fileName = _tempId < 10 ? $"person{0}{_tempId}.dat" : $"person{_tempId}.dat";

            FileStream fileStream = new FileStream(_workingFolderPath + @"\" + _fileName, FileMode.Open);
            Person personFromFile = (Person)iformatter.Deserialize(fileStream);
            fileStream.Close();

            return personFromFile;
        }
    }
}
