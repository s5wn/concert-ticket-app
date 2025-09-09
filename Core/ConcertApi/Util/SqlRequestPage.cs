using System.Reflection;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.OpenApi.Any;

namespace Util
{

    public class SqlPage
    {
        public static IQueryable<T> ToPages<T>(DbSet<T> dbset,int countPerPage, int index) where T : class
        {
            return dbset.FromSql($"SELECT * FROM Concerts ORDER BY Id OFFSET {countPerPage*index} ROWS FETCH NEXT {countPerPage} ROWS ONLY;");
        }
        
    }
}