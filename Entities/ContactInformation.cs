using System;

namespace Entities
{
    public class ContactInformation
    {
        private int id;
        private string mail;
        private string phoneNumber;

        public ContactInformation()
        {

        }

        public ContactInformation(int id, string mail, string phoneNumber)
        {
            Id = id;
            Mail = mail;
            PhoneNumber = phoneNumber;
        }

        public int Id
        {
            get => id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                id = value;
            }
        }

        public string Mail
        {
            get => mail;
            set
            {
                mail = value;
            }
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
            }
        }
    }
}