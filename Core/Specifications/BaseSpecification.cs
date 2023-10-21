using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        // public Expression<Func<T, object>> GetEnumValues { get; private set; }
        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        public Expression<Func<T, object>> GetEnumValue { get; private set; }

        protected void AddEnumValue(Expression<Func<T, object>> enumExpression)
        {
            GetEnumValue = enumExpression;
        }
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected void AddInclude(string IncludeString)
        {
            IncludeStrings.Add(IncludeString);
        }
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }

        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public void AND(BaseSpecification<T> specification)
        {
            var parameter = Criteria.Parameters[0];
            var body = Expression.AndAlso(Criteria.Body, specification.Criteria.Body);
            Criteria = Expression.Lambda<Func<T, bool>>(body, parameter);

        }
        public void OR(BaseSpecification<T> specification)
        {
            var parameter = Criteria.Parameters[0];
            var body = Expression.OrElse(Criteria.Body, specification.Criteria.Body);
            Criteria = Expression.Lambda<Func<T, bool>>(body, parameter);

        }
        public static BaseSpecification<T> Combine(BaseSpecification<T> spec1, BaseSpecification<T> spec2, Func<Expression<Func<T, bool>>, Expression<Func<T, bool>>, BinaryExpression> combineOperator)
        {
            var paramExpr = Expression.Parameter(typeof(T));
            var combinedCriteria = combineOperator(spec1.Criteria, spec2.Criteria);
            var lambda = Expression.Lambda<Func<T, bool>>(combinedCriteria, paramExpr);

            var combinedSpec = new BaseSpecification<T>(lambda)
            {
                Includes = spec1.Includes.Union(spec2.Includes).ToList()
            };

            return combinedSpec;
        }
    }
    public class CombinedSpecification<T1, T2> : BaseSpecification<T1>
    {
        public BaseSpecification<T2> Specification2 { get; }

        public CombinedSpecification(BaseSpecification<T1> spec1, BaseSpecification<T2> spec2, Func<Expression<Func<T1, bool>>, Expression<Func<T2, bool>>, BinaryExpression> combineOperator)
        {
            Specification2 = spec2;
            //Criteria = CombineSpecifications(spec1.Criteria, spec2.Criteria, combineOperator);
            //Includes.AddRange(spec1.Includes);
            //Includes.AddRange(spec2.Includes);
        }

        //private Expression<Func<T1, bool>> CombineSpecifications(Expression<Func<T1, bool>> expr1, Expression<Func<T2, bool>> expr2, Func<Expression<Func<T1, bool>>, Expression<Func<T2, bool>>, BinaryExpression> combineOperator)
        //{
        //    var paramExpr = Expression.Parameter(typeof(T1));
        //    var expr1Body = new ReplaceVisitor(expr1.Parameters[0], paramExpr).Visit(expr1.Body);
        //    var expr2Body = new ReplaceVisitor(expr2.Parameters[0], paramExpr).Visit(expr2.Body);
        //    var combinedBody = combineOperator(expr1Body, expr2Body);
        //    return Expression.Lambda<Func<T1, bool>>(combinedBody, paramExpr);
        //}

        private class ReplaceVisitor : ExpressionVisitor
        {
            private readonly Expression _oldValue;
            private readonly Expression _newValue;

            public ReplaceVisitor(Expression oldValue, Expression newValue)
            {
                _oldValue = oldValue;
                _newValue = newValue;
            }

            public override Expression Visit(Expression node)
            {
                return node == _oldValue ? _newValue : base.Visit(node);
            }
        }
    }
}