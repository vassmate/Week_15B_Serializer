using System;
using System.Runtime.Serialization;

namespace Serializer
{
    [Serializable]
    public class Person : IDeserializationCallback
    {
        private string _name;
        private string _address;
        private string _phone;
        private DateTime _dateOfRecording = DateTime.Now;

        [NonSerialized]
        private int _id;

        public Person()
        {
        }

        public Person(string name, string address, string phone)
        {
            _name = name;
            _address = address;
            _phone = phone;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public DateTime DateOfRecording
        {
            get { return _dateOfRecording; }
            set { _dateOfRecording = value; }
        }

        [OnSerializing]
        public void ShowInfo(StreamingContext context)
        {

        }

        public void OnDeserialization(object sender)
        {

        }
    }
}
