using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Serializer
{
    public partial class PersonSerialzer : Form
    {
        private int _fileNumber = 1;
        private int _fileListStepper;
        private readonly string _workingFolderPath = @"C:\serialized_persons";
        private string _fileName = "person01.dat";
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
                    Person personFromFile = Deserialize(_binaryFormatter, _workingFolderPath + @"\" + _fileName);
                    SetTextFieldValues(personFromFile);
                    _fileNumber += _fileList.Length;
                }
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string serialInfoTitle = "Serialization info";
            string serialWarnTitle = "Warning!";
            string serialWarnMsg = "Fields can't be empty!";

            if (!txtName.Text.Equals("") && !txtName.Text.Equals(" ") &&
                !txtAddress.Text.Equals("") && !txtAddress.Text.Equals(" ") &&
                !txtPhone.Text.Equals("") && !txtPhone.Text.Equals(" ") &&
                _fileList.Length < 100)
            {
                SetSerializationValues();
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
            if (_fileListStepper > 0)
            {
                _fileListStepper--;
                Person personFromFile = Deserialize(_binaryFormatter, _fileList[_fileListStepper]);
                SetTextFieldValues(personFromFile);
            }
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            if (_fileListStepper < _fileList.Length - 1)
            {
                _fileListStepper++;
                Person personFromFile = Deserialize(_binaryFormatter, _fileList[_fileListStepper]);
                SetTextFieldValues(personFromFile);
            }
        }

        private void Serialze(Person person, IFormatter iformatter)
        {
            _fileName = _fileNumber < 10 ? $"person{0}{_fileNumber}.dat" : $"person{_fileNumber}.dat";

            FileStream fileStream = new FileStream(_workingFolderPath + @"\" + _fileName, FileMode.Create);
            iformatter.Serialize(fileStream, person);
            fileStream.Close();
        }

        private Person Deserialize(IFormatter iformatter, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            Person personFromFile = (Person)iformatter.Deserialize(fileStream);
            fileStream.Close();

            return personFromFile;
        }

        private void SetTextFieldValues(Person personFromFile)
        {
            txtName.Text = personFromFile.Name;
            txtAddress.Text = personFromFile.Address;
            txtPhone.Text = personFromFile.Phone;

            txtPersonId.Text = _fileListStepper.ToString();
            txtSerialDate.Text = personFromFile.DateOfRecording.ToString();
        }

        private void SetSerializationValues()
        {
            Person newPerson = new Person(txtName.Text, txtAddress.Text, txtPhone.Text);
            Serialze(newPerson, _binaryFormatter);

            _fileList = Directory.GetFiles(_workingFolderPath);
            _fileListStepper = _fileList.Length - 1;
            _fileNumber++;

            txtPersonId.Text = _fileListStepper.ToString();
            txtSerialDate.Text = newPerson.DateOfRecording.ToString();
        }
    }
}
