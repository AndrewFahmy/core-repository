using System.Collections.Generic;
using System.Linq;
using FrameworkRepository.Helpers;
using FrameworkRepository.Interfaces;
using FrameworkRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace FrameworkRepository
{
    public class RawRepository<TDbContext> : IRawRepository<TDbContext>
        where TDbContext : DbContext
    {
        private readonly TDbContext _context;

        public RawRepository(TDbContext context)
        {
            _context = context;
        }



        public int ExecuteProcedure(string command, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(command, parameters);
        }

        public List<T> ExecuteProcedureAndGetMultiple<T>(string command, params object[] parameters) where T : class, new()
        {
            return _context.Set<T>().FromSqlRaw(command, parameters).ToList();
        }

        public List<T> ExecuteProcedureAndGetPrimitiveList<T>(string command, params object[] parameters)
        {
            return _context.Database.GetPrimitiveList<T>(command, parameters);
        }

        public T ExecuteProcedureAndGetSingle<T>(string command, params object[] parameters) where T : class, new()
        {
            return _context.Set<T>().FromSqlRaw(command, parameters).AsEnumerable().FirstOrDefault();
        }

        public TResult ExecuteScalarProcedure<TResult>(string command, params object[] parameters)
        {
            return _context.Database.GetScalar<TResult>(command, parameters);
        }

        public RawDataModel GetRawData(string command, params object[] parameters)
        {
            return _context.Database.GetRawData(command, parameters);
        }
    }
}
