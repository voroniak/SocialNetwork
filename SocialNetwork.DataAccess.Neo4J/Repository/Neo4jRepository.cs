using Neo4jClient;
using SocialNetwork.DataAccess.Neo4J.Entities;
using SocialNetwork.DataAccess.Neo4J.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DataAccess.Neo4J.Repository
{
    public class Neo4jRepository<TEntity> : IRepository<TEntity>
    where TEntity :  Neo4jEntity, new()
    {
        protected readonly GraphClient client;

        public Neo4jRepository()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data"));
            client.ConnectAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> All()
        {
            TEntity entity = new TEntity();

            return await client.Cypher
                .Match("(e:" + entity.Label + ")")
                .Return(e => e.As<TEntity>())
                .ResultsAsync;
        }

        public virtual async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> query)
        {
            string name = query.Parameters[0].Name;
            TEntity entity = (TEntity)Activator.CreateInstance(query.Parameters[0].Type);
            Expression<Func<TEntity, bool>> newQuery = PredicateRewriter.Rewrite(query, "e");

            return await client.Cypher
                .Match("(e:" + entity.Label + ")")
                .Where(newQuery)
                .Return(e => e.As<TEntity>())
                .ResultsAsync;
        }


        public virtual async Task<TEntity> Single(Expression<Func<TEntity, bool>> query)
        {
            IEnumerable<TEntity> results = await Where(query);
            return results.FirstOrDefault();
        }

        public virtual async Task Add(TEntity item)
        {
            await client.Cypher
                    .Create("(e:" + item.Label + " {item})")
                    .WithParam("item", item)
                    .ExecuteWithoutResultsAsync();
        }

        public virtual async Task Update(Expression<Func<TEntity, bool>> query, TEntity newItem)
        {
            string name = query.Parameters[0].Name;

            TEntity itemToUpdate = await this.Single(query);
            this.CopyValues(itemToUpdate, newItem);

            await client.Cypher
               .Match("(" + name + ":" + newItem.Label + ")")
               .Where(query)
               .Set(name + " = {item}")
               .WithParam("item", itemToUpdate)
               .ExecuteWithoutResultsAsync();
        }

        public virtual async Task Patch(Expression<Func<TEntity, bool>> query, TEntity item)
        {
            string name = query.Parameters[0].Name;

            await client.Cypher
               .Match("(" + name + ":" + item.Label + ")")
               .Where(query)
               .Set(name + " = {item}")
               .WithParam("item", item)
               .ExecuteWithoutResultsAsync();
        }

        public void CopyValues(TEntity target, TEntity source)
        {
            Type t = typeof(TEntity);

            var properties = t.GetProperties().Where(prop => prop.CanRead && prop.CanWrite);

            foreach (var prop in properties)
            {
                var value = prop.GetValue(source, null);
                if (value != null)
                    prop.SetValue(target, value, null);
            }
        }

        public virtual async Task Delete(Expression<Func<TEntity, bool>> query)
        {
            string name = query.Parameters[0].Name;
            TEntity entity = (TEntity)Activator.CreateInstance(query.Parameters[0].Type);

            await client.Cypher
                .Match("(" + name + ":" + entity.Label + ")")
                .Where(query)
                .Delete(name)
                .ExecuteWithoutResultsAsync();
        }

        public virtual async Task Relate<TEntity2, TRelationship>(Expression<Func<TEntity, bool>> query1, Expression<Func<TEntity2, bool>> query2, TRelationship relationship)
            where TEntity2 : Neo4jEntity, new()
            where TRelationship : Neo4jRelationship, new()
        {
            string name1 = query1.Parameters[0].Name;
            TEntity entity1 = (TEntity)Activator.CreateInstance(query1.Parameters[0].Type);
            string name2 = query2.Parameters[0].Name;
            TEntity2 entity2 = (TEntity2)Activator.CreateInstance(query2.Parameters[0].Type);

            object properties = new object();

            await client.Cypher
                .Match("(" + name1 + ":" + entity1.Label + ")", "(" + name2 + ":" + entity2.Label + ")")
                .Where(query1)
                .AndWhere(query2)
                .CreateUnique(name1 + "-[:" + relationship.Name + " {rel}]->" + name2)
                .WithParam("rel", relationship)
                .ExecuteWithoutResultsAsync();
        }

        public virtual async Task<IEnumerable<TEntity2>> GetRelated<TEntity2, TRelationship>(Expression<Func<TEntity, bool>> query1, Expression<Func<TEntity2, bool>> query2, TRelationship relationship)
            where TEntity2 : Neo4jEntity, new()
            where TRelationship : Neo4jRelationship, new()
        {
            string name1 = query1.Parameters[0].Name;
            TEntity entity1 = (TEntity)Activator.CreateInstance(query1.Parameters[0].Type);
            string name2 = query2.Parameters[0].Name;
            TEntity2 entity2 = (TEntity2)Activator.CreateInstance(query2.Parameters[0].Type);

            Expression<Func<TEntity2, bool>> newQuery = PredicateRewriter.Rewrite(query2, "e");

            return await client.Cypher
                .Match("(" + name1 + ":" + entity1.Label + ")-[:" + relationship.Name + "]->(" + name2 + ":" + entity2.Label + ")")
                .Where(query1)
                .AndWhere(query2)
                .Return(e => e.As<TEntity2>())
                .ResultsAsync;
        }

        public virtual async Task DeleteRelationship<TEntity2, TRelationship>(Expression<Func<TEntity, bool>> query1, Expression<Func<TEntity2, bool>> query2, TRelationship relationship)
            where TEntity2 : Neo4jEntity, new()
            where TRelationship : Neo4jRelationship, new()
        {
            string name1 = query1.Parameters[0].Name;
            TEntity entity1 = (TEntity)Activator.CreateInstance(query1.Parameters[0].Type);
            string name2 = query2.Parameters[0].Name;
            TEntity2 entity2 = (TEntity2)Activator.CreateInstance(query2.Parameters[0].Type);

            await client.Cypher
                .Match("(" + name1 + ":" + entity1.Label + ")-[r:" + relationship.Name + "]->(" + name2 + ":" + entity2.Label + ")")
                .Where(query1)
                .AndWhere(query2)
                .Delete("r")
                .ExecuteWithoutResultsAsync();
        }
    }
}
