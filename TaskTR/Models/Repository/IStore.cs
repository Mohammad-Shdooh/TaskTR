using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTR.Models.Repository
{
    //internal
    public interface IStore<TEntity>
    {
         void Add(TEntity entity);
        void Update(TEntity entity );

    TEntity find(int id); 
        List<TEntity> GetList();
        bool DuplicationNames(TEntity entity);
    }
}
