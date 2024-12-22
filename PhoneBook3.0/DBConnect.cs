using PhoneBook3._0;
using System.Data.SqlClient;
using System;

namespace PhoneBook3._0

{
    public class DBConnect
    {
        private const String ConectionString = "Server=.;Database=myDataBase;Trusted_Connection=True";

        public Boolean AddConact(IContact contact)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConectionString))
            {
                sqlConnection.Open();

                string query = "INSERT INTO contacts (contactType, name, phoneNumber, textComments, email, isPrivate, city, url) " +
                                          "VALUES (@contactType, @name, @phoneNumber, @textComments, @email, @isPrivate, @city, @url)";
                /**
                 * Вносим в базу данных проверенный объект, неисползуемые поля будут null
                 */
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@contactType", contact.ContactType);
                    command.Parameters.AddWithValue("@name", contact.Name);
                    command.Parameters.AddWithValue("@phoneNumber", contact.PhoneNumber);
                    command.Parameters.AddWithValue("@textComments", contact.TextComment);
                    if (contact is Person p)
                    {
                        command.Parameters.AddWithValue("@email", String.IsNullOrEmpty(p.Email) ? null : p.Email);
                    }
                    else if (contact is Organization org)
                    {
                        command.Parameters.AddWithValue("@isPrivate", org.IsPrivate);
                        command.Parameters.AddWithValue("@city", org.IsPrivate == true ? null : org.City);
                        command.Parameters.AddWithValue("@url", String.IsNullOrEmpty(org.Url) ? null : org.Url);
                    }
                    command.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
            return true;
        }


        public List<IContact> FindContactByContactType(String contactType)
        {
            List<IContact> contacts = new List<IContact>();
            using (SqlConnection sqlConnection = new SqlConnection(ConectionString))
            {
                sqlConnection.Open();

                string query = "";
                switch (contactType)
                {
                    case "ALL":
                        query = "SELECT * FROM contacts";
                        break;
                    case "PERSON":
                        query = "SELECT * FROM contacts WHERE contactType = 'PERSON'";
                        break;
                    case "PUBLIC_ORGANIZATION":
                        query = "SELECT * FROM contacts WHERE contactType = 'PUBLIC_ORGANIZATION' ";
                        break;
                    case "PRIVATE_ORGANIZATION":
                        query = "SELECT * FROM contacts WHERE contactType = 'PRIVATE_ORGANIZATION' ";
                        break;
                    default:
                        throw new ArgumentException("Invalid contact type");
                }
                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["contactType"].Equals("PERSON"))
                            {
                                return contacts.Add(new Person(
                                    RerurnEnum(reader["contactType"].ToString()),
                                    reader["name"].ToString(),
                                    ReturnPhoneNumber(reader["phoneNumber"].ToString()),
                                    reader["textComments"].ToString(),
                                    reader["email"]?.ToString()));
                            }
                            else if (reader["contactType"].Equals("PUBLIC_ORGANIZATION"))
                            {
                                return contacts.Add(new Organization(
                                    RerurnEnum(reader["contactType"].ToString()),
                                    reader["name"].ToString(),
                                    ReturnPhoneNumber(reader["phoneNumber"].ToString()),
                                    reader["textComments"].ToString(),
                                    false,
                                    reader["city"]?.ToString(),
                                    reader["url"]?.ToString()));
                            }
                            else
                            {
                                return contacts.Add(new Organization(
                                    RerurnEnum(reader["contactType"].ToString()),
                                    reader["name"].ToString(),
                                    ReturnPhoneNumber(reader["phoneNumber"].ToString()),
                                    reader["textComments"].ToString(),
                                    true,
                                    null,
                                    reader["url"]?.ToString()));
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return contacts;
        }

        /**
         * В качестве уникального Id используем телефонный номер который будет уникальным
         */
        public IContact FindContactById(String phoneNumber)
        {        
            int number = 0;
            if (Int32.TryParse(phoneNumber, out number))
            {
                Console.WriteLine(String.Format("Номер телефона: {} ", phoneNumber));
            }
            else
            {
                Console.WriteLine(String.Format("Некорректные данные: {} - не является номером телефона", phoneNumber));
            }
            using (SqlConnection sqlConnection = new SqlConnection(ConectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("SELECT * FROM contacts WHERE phoneNumber = @number", sqlConnection);
                command.Parameters.AddWithValue("@number", number);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read() != null)
                    {
                        while (reader.Read())
                        {
                            if (reader["contactType"].Equals("PERSON"))
                            {
                                return new Person(
                                    RerurnEnum(reader["contactType"].ToString()),
                                    reader["name"].ToString(),
                                    ReturnPhoneNumber(reader["phoneNumber"].ToString()),
                                    reader["textComments"].ToString(),
                                    reader["email"]?.ToString());
                            }
                            else if (reader["contactType"].Equals("PUBLIC_ORGANIZATION"))
                            {
                                return new Organization(
                                    RerurnEnum(reader["contactType"].ToString()),
                                    reader["name"].ToString(),
                                    ReturnPhoneNumber(reader["phoneNumber"].ToString()),
                                    reader["textComments"].ToString(),
                                    false,
                                    reader["city"]?.ToString(),
                                    reader["url"]?.ToString());
                            }
                            else
                            {
                                return new Organization(
                                    RerurnEnum(reader["contactType"].ToString()),
                                    reader["name"].ToString(),
                                    ReturnPhoneNumber(reader["phoneNumber"].ToString()),
                                    reader["textComments"].ToString(),
                                    true,
                                    null,
                                    reader["url"]?.ToString());
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(String.Format("Некорректные данные: {} - такой номер отуствует в списке", phoneNumber));
                        return null;
                    }
                }
                sqlConnection.Close();
            }
        }

        private CType RerurnEnum(string value)
        {
            return (CType)Enum.Parse(typeof(CType), value);
        }

        private int ReturnPhoneNumber(string nums)
        {
            if (Int32.TryParse(nums, out int result))
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Невозможно преобразовать в телефонный номер");
            }
        }
    }
}



