using PhoneBook3._0;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace PhoneBook3._0
{
    public class ExtractInfo
    {
        public object GetContactByType(CType contact)
        {
            string type = contact.ToString;
            switch (type)
            {
                case "PERSON":
                    return new Person
                    {
                        ContactType = "PERSON";
                        Name = txtName.Text;
                        PhoneNumber = txtPhoneNumber.Text;
                        TextComment = txtTextComment.Text;
                        Email = txtEmail.Text;
                    };
                case "PUBLIC_ORGANIZATION":
                    return new Organization
                    {
                        ContactType = "PUBLIC_ORGANIZATION";
                        Name = txtName.Text;
                        PhoneNumber = txtPhoneNumber.Text;
                        TextComment = txtTextComment.Text;
                        City = txtCity.Text;
                        Url = txtUrl.Text;
                    };
                case "PRIVATE_ORGANIZATION":
                    return new Organization
                    {
                        ContactType = "PRIVATE_ORGANIZATION";
                        Name = txtName.Text;
                        PhoneNumber = txtPhoneNumber.Text;
                        TextComment = txtTextComment.Text;
                        City = null;
                        Url = txtUrl.Text;
                    };
            }
        }
    }
}
