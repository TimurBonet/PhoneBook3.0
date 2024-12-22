using PhoneBook3._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook3._0
{
    class Organization : IContact, IEquatable<Organization>
    {
        public CType ContactType
        { get; set; }

        public string Name
        { get; set; }

        public int PhoneNumber
        { get; set; }

        public string TextComment
        { get; set; }

        public bool IsPrivate
        { get; set; }

        public string City
        { get; set; }

        public string Url
        { get; set; }

        public Organization( CType contactType, string name, int phoneNumber, string textComment, bool isPrivate , string city, string url)
        {
            ContactType = contactType;
            Name = name;
            PhoneNumber = phoneNumber;
            TextComment = textComment;
            IsPrivate = isPrivate;
            City = city;
            Url = url;
        }


        public override bool Equals(object obj)
        {
            return Equals(obj as Organization);
        }

        public bool Equals(Organization other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return other != null &&
                   ContactType == other.ContactType &&
                   Name == other.Name &&
                   PhoneNumber == other.PhoneNumber &&
                   TextComment == other.TextComment &&
                   City == other.City &&
                   Url == other.Url;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + ContactType.GetHashCode();
            hash = hash * 23 + Name.GetHashCode();
            hash = hash * 23 + PhoneNumber.GetHashCode();
            hash = hash * 23 + TextComment.GetHashCode();
            hash = hash * 23 + IsPrivate.GetHashCode();
            hash = hash * 23 + City.GetHashCode();
            hash = hash * 23 + Url.GetHashCode();
            return hash;
        }

    }
}
