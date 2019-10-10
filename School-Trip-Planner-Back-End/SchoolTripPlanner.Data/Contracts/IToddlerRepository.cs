using System.Collections.Generic;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface IToddlerRepository
    {
        List<Toddler> GetAll();
        void Update(Toddler updatedToddler);
        bool ToddlerExists(long id);
        void Add(Toddler toddler);
        Toddler GetById(long id);
    }
}