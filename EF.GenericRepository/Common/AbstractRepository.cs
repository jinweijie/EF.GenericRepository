using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EF.GenericRepository.Entity;

namespace EF.GenericRepository.Common
{
    /// <summary>
    /// The Abstract Entity Framework Data Access Object, perform basic CRUD, paging query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId">The type of the id.</typeparam>
    public abstract class AbstractRepository<T, TId> where T : EntityBase<TId>
    {
        #region contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository&lt;T, TId&gt;"/> class.
        /// </summary>
        protected AbstractRepository()
            : this(EFContext.CONNECTION_STRING, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository&lt;T, TId&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected AbstractRepository(EFContext context)
            : this(context, true)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository&lt;T, TId&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="enableLazyLoading">if set to <c>true</c> [enable lazy loading].</param>
        protected AbstractRepository(EFContext context, bool enableLazyLoading)
        {
            this._context = context;
            this._entity = Context.ObjectContext.CreateObjectSet<T>();
            this.EnableLazyLoading = enableLazyLoading;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository&lt;T, TId&gt;"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        protected AbstractRepository(string connectionString)
            : this(connectionString, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository&lt;T, TId&gt;"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="enableLazyLoading">if set to <c>true</c> [enable lazy loading].</param>
        protected AbstractRepository(string connectionString, bool enableLazyLoading)
        {
            this._context = new EFContext(connectionString);
            this._entity = Context.ObjectContext.CreateObjectSet<T>();
            this.EnableLazyLoading = enableLazyLoading;
        }

        #endregion

        #region Properties

        #region EnableLazyLoading

        /// <summary>
        /// Gets or sets a value indicating whether [enable lazy loading].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [enable lazy loading]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableLazyLoading
        {
            get;
            set;
        }

        #endregion

        #region Context

        /// <summary>
        /// The context
        /// </summary>
        private readonly EFContext _context;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        protected EFContext Context
        {
            get { return _context; }
        }

        #endregion

        #region Entity

        private readonly ObjectSet<T> _entity;
        protected IObjectSet<T> Entity
        {
            get { return _entity; }
        }

        #endregion

        #endregion

        #region Protected Methods

        #region Initialize

        /// <summary>
        /// Call this method before each query
        /// </summary>
        protected virtual void Initialize()
        {
            this.Context.ObjectContext.ContextOptions.LazyLoadingEnabled = this.EnableLazyLoading;
        }

        #endregion

        #region LoadProperties

        /// <summary>
        /// for loading navigation properties
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="properties"></param>
        protected virtual void LoadProperties(object entity, params string[] properties)
        {
            foreach (string p in properties)
                this.Context.ObjectContext.LoadProperty(entity, p);
        }

        #endregion

        #region GetEntityName

        /// <summary>
        /// Get the entity name of the T
        /// </summary>
        /// <returns></returns>
        protected virtual string GetEntityName()
        {
            return string.Format("{0}.{1}", _entity.EntitySet.EntityContainer, _entity.EntitySet.Name);
        }

        #endregion

        #region PerformPaging

        /// <summary>
        /// Performs the paging.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="totalRowCount">The total row count.</param>
        /// <returns></returns>
        protected Tuple<IQueryable<T>,int>PerformPaging/*<T>*/(IQueryable<T> query
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc)
        {
            int index = 0;

            foreach (string a in asc)
            {
                if(string.IsNullOrWhiteSpace(a))
                    continue;

                query = CallMethod/*<T>*/(query, index == 0 ? "OrderBy" : "ThenBy", a);
                index++;
            }

            index = 0;
            foreach (string d in desc)
            {
                if (string.IsNullOrWhiteSpace(d))
                    continue;

                query = CallMethod/*<T>*/(query, index == 0 ? "OrderByDescending" : "ThenByDescending", d);
                index++;
            }

            var totalRowCount = query.Count<T>();

            query = query.Skip<T>(pageIndex * pageSize);
            query = query.Take<T>(pageSize);

            return new Tuple<IQueryable<T>, int>(query, totalRowCount);
        }

        /// <summary>
        /// Performs the paging asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <returns></returns>
        protected async Task<Tuple<IQueryable<T>, int>> PerformPagingAsync/*<T>*/(IQueryable<T> query
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc)
        {
            int index = 0;

            foreach (string a in asc)
            {
                query = CallMethod/*<T>*/(query, index == 0 ? "OrderBy" : "ThenBy", a);
                index++;
            }

            index = 0;
            foreach (string d in desc)
            {
                query = CallMethod/*<T>*/(query, index == 0 ? "OrderByDescending" : "ThenByDescending", d);
                index++;
            }

            var totalRowCount = await query.CountAsync<T>();

            query = query.Skip<T>(pageIndex * pageSize);
            query = query.Take<T>(pageSize);

            return new Tuple<IQueryable<T>, int>(query, totalRowCount);
        }

        #endregion

        #region CallMethod

        /// <summary>
        /// Calls the method.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="memberName">Name of the member.</param>
        /// <returns></returns>
        protected IOrderedQueryable<T> CallMethod/*<T>*/(IQueryable<T> query, string methodName, string memberName)
        {
            var typeParams = new ParameterExpression[] { Expression.Parameter(typeof(T), "") };

            System.Reflection.PropertyInfo pi = typeof(T).GetProperty(memberName);

            return (IOrderedQueryable<T>)query.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new Type[] { typeof(T), pi.PropertyType },
                    query.Expression,
                    Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
            );
        }

        #endregion

        #endregion

        #region IRepository<T> Members

        #region Create

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Create(T entity)
        {
            var fqen = GetEntityName();

            this.Context.ObjectContext.AddObject(fqen, entity);

            return entity;
        }


        #endregion

        #region Update

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T Update(T entity)
        {
            var fqen = GetEntityName();

            this.Context.ObjectContext.ApplyCurrentValues(fqen, entity);

            return entity;
        }

        #endregion

        #region CreateOrUpdate

        /// <summary>
        /// Creates the or update.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual T CreateOrUpdate(T entity)
        {
            if (entity.IsTransient())
            {
                Create(entity);
            }
            else
            {
                Update(entity);
            }

            return entity;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public virtual void Delete(TId id)
        {
            this.Context.ObjectContext.DeleteObject(this.FindByKey(id));

        }

        #endregion
        
        #region FindByKey

        /// <summary>
        /// Finds the by key.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual T FindByKey(TId id, params Expression<Func<T, object>>[] includeProperties)
        {
            var item = Expression.Parameter(typeof(T), "entity");
            var prop = Expression.Property(item, "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, item);

            var query = this.Entity.Where(lambda);

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return query.SingleOrDefault();
        }

        /// <summary>
        /// Finds the by key asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<T> FindByKeyAsync(TId id, params Expression<Func<T, object>>[] includeProperties)
        {
            var item = Expression.Parameter(typeof(T), "entity");
            var prop = Expression.Property(item, "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, item);

            var query = this.Entity.Where(lambda);

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }
            return await query.SingleOrDefaultAsync();
        }

        #endregion
        
        #region Find

        /// <summary>
        /// Finds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = PrepareFindQuery(criteria, includeProperties);

            return query.AsEnumerable();
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = PrepareFindQuery(criteria, includeProperties);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds the specified criteria.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Find(ISpecification<T> spec, params Expression<Func<T, object>>[] includeProperties)
        {
            return Find(spec.ToExpression(), includeProperties);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAsync(ISpecification<T> spec, params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAsync(spec.ToExpression(), includeProperties);
        }

        /// <summary>
        /// Finds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="totalRowCount">The total row count.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        [Obsolete("Use find with Tuple return value method.")]
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> criteria
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , out int totalRowCount
            , params Expression<Func<T, object>>[] includeProperties)
        {
            var paged = PrepareFindQuery(criteria
            , pageIndex
            , pageSize
            , asc
            , desc
            , includeProperties);

            var query = paged.Item1;
            totalRowCount = paged.Item2;

            return query.AsEnumerable();
        }

        /// <summary>
        /// Finds the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T>, int> Find(Expression<Func<T, bool>> criteria
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , params Expression<Func<T, object>>[] includeProperties)
        {
            var paged = PrepareFindQuery(criteria
            , pageIndex
            , pageSize
            , asc
            , desc
            , includeProperties);

            var query = paged.Item1;
            var totalRowCount = paged.Item2;

            return new Tuple<IEnumerable<T>, int>(query.AsEnumerable(), totalRowCount);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<Tuple<IEnumerable<T>, int>> FindAsync(Expression<Func<T, bool>> criteria
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , params Expression<Func<T, object>>[] includeProperties)
        {
            var paged = PrepareFindQuery(criteria
            , pageIndex
            , pageSize
            , asc
            , desc
            , includeProperties);

            var list = await paged.Item1.ToListAsync();
            var totalRowCount = paged.Item2;

            return new Tuple<IEnumerable<T>, int>(list, totalRowCount);
        }
        
        /// <summary>
        /// Finds the specified spec.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T>, int> Find(ISpecification<T> spec
                                                        , int pageIndex
                                                        , int pageSize
                                                        , string[] asc
                                                        , string[] desc
                                                        , params Expression<Func<T, object>>[] includeProperties)
        {
            return Find(spec.ToExpression()
                        , pageIndex
                        , pageSize
                        , asc
                        , desc
                        , includeProperties);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<Tuple<IEnumerable<T>, int>> FindAsync(ISpecification<T> spec
                                                                        , int pageIndex
                                                                        , int pageSize
                                                                        , string[] asc
                                                                        , string[] desc
                                                                        , params Expression<Func<T, object>>[] includeProperties)
        {
            return await FindAsync(spec.ToExpression()
                                    , pageIndex
                                    , pageSize
                                    , asc
                                    , desc
                                    , includeProperties);

        }
        
        /// <summary>
        /// Prepares the find query.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        protected virtual IQueryable<T> PrepareFindQuery(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();

            var query = this.Context.ObjectContext.CreateQuery<T>(entityName)
                                .Where(criteria);

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query;
        }

        /// <summary>
        /// Prepares the find query.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual Tuple<IQueryable<T>, int> PrepareFindQuery(Expression<Func<T, bool>> criteria
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();

            var query = this.Context.ObjectContext.CreateQuery<T>(entityName)
                                .Where(criteria);

            var paged = PerformPaging/*<T>*/(query, pageIndex, pageSize, asc, desc);
            query = paged.Item1;
            var totalRowCount = paged.Item2;

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return new Tuple<IQueryable<T>, int>(query, totalRowCount);
        }

        /// <summary>
        /// Prepares the find query asynchronous.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<Tuple<IQueryable<T>, int>> PrepareFindQueryAsync(Expression<Func<T, bool>> criteria
            , int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();

            var query = this.Context.ObjectContext.CreateQuery<T>(entityName)
                                .Where(criteria);

            var paged = await PerformPagingAsync/*<T>*/(query, pageIndex, pageSize, asc, desc);
            query = paged.Item1;
            var totalRowCount = paged.Item2;

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return new Tuple<IQueryable<T>, int>(query, totalRowCount);
        }
        

        #endregion

        #region FindOne

        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual T FindOne(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            var query = this.Context.ObjectContext.CreateQuery<T>(entityName)
                                .Where(criteria);

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query.FirstOrDefault();
        }

        /// <summary>
        /// Finds the one asynchronous.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<T> FindOneAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            var query = this.Context.ObjectContext.CreateQuery<T>(entityName)
                                .Where(criteria);

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Finds the one.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual T FindOne(ISpecification<T> spec, params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            return FindOne(spec.ToExpression(), includeProperties);
        }
        
        /// <summary>
        /// Finds the one asynchronous.
        /// </summary>
        /// <param name="spec">The spec.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<T> FindOneAsync(ISpecification<T> spec, params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            return await FindOneAsync(spec.ToExpression(), includeProperties);
        }

        #endregion

        #region FindAll

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual IEnumerable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            var query = this.Context.ObjectContext.CreateQuery<T>(entityName).AsQueryable();

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query.AsEnumerable();
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            var query = this.Context.ObjectContext.CreateQuery<T>(entityName).AsQueryable();

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="totalRowCount">The total row count.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        [Obsolete("Use FindAll with Tuple return value method instead.")]
        public virtual IEnumerable<T> FindAll(int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , out int totalRowCount
            , params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            IQueryable<T> query = this.Context.ObjectContext.CreateQuery<T>(entityName);

            var paged = PerformPaging/*<T>*/(query, pageIndex, pageSize, asc, desc);

            query = paged.Item1;
            totalRowCount = paged.Item2;

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query.AsEnumerable();
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual Tuple<IEnumerable<T>, int> FindAll(int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            IQueryable<T> query = this.Context.ObjectContext.CreateQuery<T>(entityName);

            var paged = PerformPaging/*<T>*/(query, pageIndex, pageSize, asc, desc);

            query = paged.Item1;
            var totalRowCount = paged.Item2;

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return new Tuple<IEnumerable<T>, int>(query.AsEnumerable(), totalRowCount);
        }

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="asc">The asc.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public virtual async Task<Tuple<IEnumerable<T>, int>> FindAllAsync(int pageIndex
            , int pageSize
            , string[] asc
            , string[] desc
            , params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            IQueryable<T> query = this.Context.ObjectContext.CreateQuery<T>(entityName);

            var paged = await PerformPagingAsync/*<T>*/(query, pageIndex, pageSize, asc, desc);

            query = paged.Item1;
            var totalRowCount = paged.Item2;

            if (includeProperties.Any())
            {
                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return new Tuple<IEnumerable<T>, int>(query.AsEnumerable(), totalRowCount);
        }

        #endregion


        #region GetQuery

        /// <summary>
        /// Gets the query.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetQuery()//(params Expression<Func<T, object>>[] includeProperties)
        {
            Initialize();

            var entityName = GetEntityName();
            var query = this.Context.ObjectContext.CreateQuery<T>(entityName).AsQueryable();

            //            if (includeProperties.Any())
            //            {
            //                query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            //            }

            return query;
        }

        #endregion
        
        #endregion
    }
}
