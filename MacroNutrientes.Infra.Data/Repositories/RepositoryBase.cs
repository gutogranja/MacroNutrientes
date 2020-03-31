using MacroNutrientes.Domain.Entities;
using MacroNutrientes.Domain.Interfaces.Repositories;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;

namespace MacroNutrientes.Infra.Data.Repositories
{
    public abstract class RepositoryBase<T, TView> : IRepositoryBase<T, TView> where T : Entity where TView : class
    {
        protected SqlConnection cn = new SqlConnection("Data Source=srvcon,6060;Initial Catalog=Central;Connection Timeout=180;Persist Security Info=True;User ID=Portal;Password=**@pwp0rt4l@**;Application Name=MacroNutrientes");
        //public abstract IEnumerable<TView> ListarTodos();

        public T ObterPorId(int id)
        {
            return cn.Get<T>(id);
        }

        public T Incluir(T entity)
        {
            var retorno = cn.Insert(entity);
            if (retorno > 0)
                return entity;
            return null;
        }

        public T Alterar(T entity)
        {
            bool retorno = cn.Update(entity);
            if (retorno)
                return entity;
            return null;
        }
    }
}
