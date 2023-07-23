using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        private List<ISubject> modelsList;
        public SubjectRepository()
        {
            this.modelsList = new List<ISubject>();
        }
        public IReadOnlyCollection<ISubject> Models => this.modelsList.AsReadOnly();

        public void AddModel(ISubject model)
        => this.modelsList.Add(model);

        public ISubject FindById(int id)
        {
            ISubject subject = modelsList.FirstOrDefault(s => s.Id == id);
            return subject;
        }

        public ISubject FindByName(string name)
        {
            ISubject subject = modelsList.FirstOrDefault(s => s.Name == name);
            return subject;
        }
    }
}
