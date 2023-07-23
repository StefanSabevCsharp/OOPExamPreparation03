using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> studentsList;
        public StudentRepository()
        {
            this.studentsList = new List<IStudent>();
        }
        public IReadOnlyCollection<IStudent> Models => this.studentsList.AsReadOnly();

        public void AddModel(IStudent model)
        => this.studentsList.Add(model);

        public IStudent FindById(int id)
        {
           IStudent student = studentsList.FirstOrDefault(s => s.Id == id);
            return student;
        }

        public IStudent FindByName(string name)
        {
            string[] names = name.Split(" ",StringSplitOptions.RemoveEmptyEntries);
            string firstName = names[0];
            string lastName = names[1];
            IStudent student = studentsList.FirstOrDefault(s => s.FirstName == firstName && s.LastName == lastName);
            return student;
        }
    }
}
