using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    
    public class UniversityRepository : IRepository<IUniversity>
    {
        private List<IUniversity> modelsList;
        public UniversityRepository()
        {
            this.modelsList = new List<IUniversity>();
        }
        public IReadOnlyCollection<IUniversity> Models => modelsList.AsReadOnly();

        public void AddModel(IUniversity model)
        => this.modelsList.Add(model);

        public IUniversity FindById(int id)
        {
            IUniversity university = modelsList.FirstOrDefault(s => s.Id == id);
            return university;
        }

        public IUniversity FindByName(string name)
        {
            IUniversity university = modelsList.FirstOrDefault(s => s.Name == name);
            return university;
        }
    }
}
