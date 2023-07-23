using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private int id;
        private string firstname;
        private string lastname;
        private readonly List<int> coveredExamsInList;
       

        public Student(int studentId, string firstName, string lastName)
        {
            Id = studentId;
            FirstName = firstName;
            LastName = lastName;
            coveredExamsInList = new List<int>();
           

        }

        public int Id { get; private set; }

        public string FirstName
        {
            get => firstname;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                firstname = value;
            }
        }

        public string LastName
        {
            get => lastname;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                lastname = value;
            }
        }


        public IReadOnlyCollection<int> CoveredExams => coveredExamsInList;

        public IUniversity University { get; private set; }

        public void CoverExam(ISubject subject)
        {
            coveredExamsInList.Add(subject.Id);
        }       

        public void JoinUniversity(IUniversity university)
        {
            University = university;
        }
    }
}
