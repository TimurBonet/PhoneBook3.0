using PhoneBook3._0;

namespace PhoneBook3._0

{
    public interface IContact 
    {
        public CType ContactType
        { get; set; }

        public string Name
        {get; set; }

        public int PhoneNumber
        { get; set; }

        public string TextComment
        { get; set; } 
    }
}
