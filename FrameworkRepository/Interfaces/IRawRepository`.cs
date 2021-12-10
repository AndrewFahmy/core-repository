using System.Collections.Generic;
using FrameworkRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace FrameworkRepository.Interfaces
{
    public interface IRawRepository<TDbContext> 
        where TDbContext: DbContext
    {
        T ExecuteProcedureAndGetSingle<T>(string command, params object[] parameters)
            where T : class, new();

        List<T> ExecuteProcedureAndGetMultiple<T>(string command, params object[] parameters)
            where T : class, new();

        List<T> ExecuteProcedureAndGetPrimitiveList<T>(string command, params object[] parameters);

        int ExecuteProcedure(string command, params object[] parameters);

        TResult ExecuteScalarProcedure<TResult>(string command, params object[] parameters);

        RawDataModel GetRawData(string command, params object[] parameters);
    }
}
