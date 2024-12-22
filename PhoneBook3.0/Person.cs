using PhoneBook3._0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook3._0;

public class Person : IContact, IEquatable<Person>
{
    public CType ContactType
    { get; set; }

    public string Name
    { get; set; }

    public int PhoneNumber
    { get; set; }

    public string TextComment
    { get; set; }

    public string Email
    { get; set; }

    public Person( CType contactType, string name, int phoneNumber, string textComment, string email)
    {
        ContactType = contactType;
        Name = name;
        PhoneNumber = phoneNumber;
        TextComment = textComment;
        Email = email;
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Person);
    }

    public bool Equals(Person other)
    {
        if (other is null) 
            return false;

        if (ReferenceEquals(this, other)) 
            return true;

        return other != null &&
               ContactType == other.ContactType &&
               Name == other.Name &&
               PhoneNumber == other.PhoneNumber &&
               TextComment == other.TextComment &&
               Email == other.Email;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + ContactType.GetHashCode();
        hash = hash * 23 + Name.GetHashCode();
        hash = hash * 23 + PhoneNumber.GetHashCode();
        hash = hash * 23 + TextComment.GetHashCode();
        hash = hash * 23 + Email.GetHashCode();
        return hash;
    }
}
