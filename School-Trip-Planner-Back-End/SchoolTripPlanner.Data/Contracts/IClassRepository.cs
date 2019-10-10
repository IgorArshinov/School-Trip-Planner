using System.Collections.Generic;
using SchoolTripPlanner.Domain.Models;

namespace SchoolTripPlanner.Data.Contracts
{
    public interface IClassRepository
    {
        List<Class> GetAll();
    }
}