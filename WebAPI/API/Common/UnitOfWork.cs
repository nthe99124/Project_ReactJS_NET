using API.Common.Interface;
using Microsoft.EntityFrameworkCore;
using Model.Common;
using System;
using System.Threading.Tasks;

namespace API.Common
{
    public class UnitOfWork: IUnitOfWork
    {
        readonly MyDbContext _context;
        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> CommitAsync()
        {
            return  await _context.SaveChangesAsync();
        }
    }
}
