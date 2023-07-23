using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private SubjectRepository subjectRepository;
        private StudentRepository studentRepository;
        private UniversityRepository universityRepository;

        public Controller()
        {
            this.subjectRepository = new SubjectRepository();
            this.studentRepository = new StudentRepository();
            this.universityRepository = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != typeof(EconomicalSubject).Name && subjectType != typeof(HumanitySubject).Name && subjectType != typeof(TechnicalSubject).Name)
            {
                return $"{string.Format(OutputMessages.SubjectTypeNotSupported, subjectType)}";
            }
            else if (subjectRepository.Models.Any(x => x.Name == subjectName))
            {
                return $"{string.Format(OutputMessages.AlreadyAddedSubject, subjectName)}";
            }

            ISubject subject = null;
            int id = subjectRepository.Models.Count + 1;
            if (subjectType == typeof(EconomicalSubject).Name)
            {
                subject = new EconomicalSubject(id, subjectName);
            }
            else if (subjectType == typeof(HumanitySubject).Name)
            {
                subject = new HumanitySubject(id, subjectName);
            }
            else if (subjectType == typeof(TechnicalSubject).Name)
            {
                subject = new TechnicalSubject(id, subjectName);
            }

            subjectRepository.AddModel(subject);

            return $"{string.Format(OutputMessages.SubjectAddedSuccessfully, subject.GetType().Name, subject.Name, subjectRepository.GetType().Name)}";


        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universityRepository.Models.Any(x => x.Name == universityName))
            {
                return $"{string.Format(OutputMessages.AlreadyAddedUniversity, universityName)}";
            }
            List<int> rs = new List<int>();
            foreach (var subName in requiredSubjects)
            {
                rs.Add(this.subjectRepository.FindByName(subName).Id);
            }
            IUniversity university =
                new University(this.universityRepository.Models.Count + 1, universityName, category, capacity, rs);
            this.universityRepository.AddModel(university);

            return $"{string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universityRepository.GetType().Name)}";


        }

        public string AddStudent(string firstName, string lastName)
        {
            if (studentRepository.Models.Any(x => x.FirstName == firstName && x.LastName == lastName))
            {
                return $"{string.Format(OutputMessages.AlreadyAddedStudent,firstName,lastName)}";
            }
            IStudent student = new Student(studentRepository.Models.Count + 1, firstName, lastName);
            studentRepository.AddModel(student);
            return $"{string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, studentRepository.GetType().Name)}";

        }


        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student  = studentRepository.FindById(studentId);
            if (student == null)
            {
                return $"{string.Format(OutputMessages.InvalidStudentId, studentId)}";
            }
            ISubject subject = subjectRepository.FindById(subjectId);
            if (subject == null)
            {
                return $"{string.Format(OutputMessages.InvalidSubjectId, subjectId)}";
            }
           bool isCovered = student.CoveredExams.Any(x => x == subjectId);
            if (isCovered)
            {
                  return $"{string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name)}";
            }
            student.CoverExam(subject);
            return $"{string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name)}";

            
        }


        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] names = studentName.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            IStudent student = studentRepository.FindByName(studentName);
            if (student == null)
            {
                return $"{string.Format(OutputMessages.StudentNotRegitered, names[0], names[1])}";
            }
            IUniversity university = universityRepository.FindByName(universityName);
            if (university == null)
            {
                return $"{string.Format(OutputMessages.UniversityNotRegitered, universityName)}";
            }
            var a = university.RequiredSubjects;
            var b = student.CoveredExams;
            bool isCovered = university.RequiredSubjects.All(x => student.CoveredExams.Contains(x));
            if (!isCovered)
            {
                return $"{string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName)}";
            }
            if(student.University != null)
            {
                return $"{string.Format(OutputMessages.StudentAlreadyJoined, names[0], names[1],universityName)}";
            }
            student.JoinUniversity(university);
            return $"{string.Format(OutputMessages.StudentSuccessfullyJoined, names[0], names[1],universityName)}";

        }


        public string UniversityReport(int universityId)
        {
            IUniversity university = universityRepository.FindById(universityId);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");

            int countOfStudentsInTheUniversity = studentRepository.Models.Count(x => x.University == university);

            sb.AppendLine($"Students admitted: {countOfStudentsInTheUniversity}");
            sb.AppendLine($"University vacancy: {university.Capacity - countOfStudentsInTheUniversity}");




            return sb.ToString().TrimEnd();
        }
    }
}
