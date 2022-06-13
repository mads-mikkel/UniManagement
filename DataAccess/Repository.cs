using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Repository
    {
        private const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UniDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public Repository()
        {
            // Test if application can contact database server:
            SqlConnection connection = new(connectionString);
            connection.Open();
            connection.Close();
        }

        public List<ContactInformation> GetAllContactInformations()
        {
            // Make the list that the method returns:
            List<ContactInformation> contactInformations = new();

            // Make a connection to the DB and open it:
            SqlConnection connection = new(connectionString);
            connection.Open();

            // Make the SQl query:
            string sql = "SELECT * FROM ContactInformations";

            // Make the command object:
            SqlCommand command = new(sql, connection);

            // Execute query and save the returned data in a variable:
            SqlDataReader reader = command.ExecuteReader();

            // Convert reader data to C# objects. For each row in the reader:
            while (reader.Read())
            {
                // Extract database data from the reader to C# variables:
                int id = (int)reader[0];
                string phone = (string)reader[1];
                string mail = (string)reader[2];

                // Create a new object:
                ContactInformation ci = new()
                {
                    Id = id,
                    PhoneNumber = phone,
                    Mail = mail
                };

                // Add to the list:
                contactInformations.Add(ci);
            }

            // Always remember to close the connection:
            connection.Close();

            // Return the list of contact infos, if any:
            return contactInformations;
        }

        public List<Person> GetAllPersons()
        {
            // First, get all the contact infos, because they are aggregated from person:
            List<ContactInformation> contactInformations = GetAllContactInformations();

            // Make the list that the method returns:
            List<Person> persons = new();

            // Make a connection to the DB and open it:
            SqlConnection connection = new(connectionString);
            connection.Open();

            // Make the SQl query:
            string sql = "SELECT * FROM Persons";

            // Make the command object:
            SqlCommand command = new(sql, connection);

            // Execute query and save the returned data in a variable:
            SqlDataReader reader = command.ExecuteReader();

            // Convert reader data to C# objects. For each row in the reader:
            while (reader.Read())
            {
                int id = (int)reader[0];
                string firstname = (string)reader[1];
                string lastname = (string)reader[2];

                // This is the FK
                int contactInformation_FKid = (int)reader[3];

                // The aggregated contact information object. Initialize to null:
                ContactInformation contactInformation = null;

                // Loop through all the contact information objects in the contact informations list, that we got before from the database:
                for(int i = 0; i < contactInformations.Count; i++)
                {
                    // If there is a match in the retrieved person row's FK value,
                    if(contactInformation_FKid == contactInformations[i].Id)
                    {
                        // then assign the object from the list, to the property on the person object, thereby making the OOP aggregation:
                        contactInformation = contactInformations[i];

                        // Break out of the loop, because there' no reason to continue:
                        break;
                    }
                }

                // Assign all the retrieved values to the person object:
                Person p = new()
                {
                    Id = id,
                    Firstname = firstname,
                    Lastname = lastname,
                    ContactInformation = contactInformation
                };

                // Add the retrieved person to the list of persons:
                persons.Add(p);
            }

            // Return the list of Persons:
            return persons;
        }

        public void AddNewContactInformation(ContactInformation contactInformationToAdd)
        {
            // Make a connection to the DB and open it:
            SqlConnection connection = new(connectionString);
            connection.Open();

            // Make the SQl query:
            string sql = $"INSERT INTO ContactInformations (Mail, PhoneNumber) VALUES('{contactInformationToAdd.Mail}', '{contactInformationToAdd.PhoneNumber}')";

            // Make the command object:
            SqlCommand command = new(sql, connection);

            // Execute the command:
            command.ExecuteNonQuery();

            // Close the connetion:
            connection.Close();
        }
    }
}
