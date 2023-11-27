using Core.Entities.Actions;
using Core.Entities.Entries;
using Core.Specifications.EntriesSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class RequestSpec : BaseSpecification<Request>
    {
        private ParameterExpression parameterExpression;
        private readonly RequestSpecParams param;
        public RequestSpec() : base()
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.Department);
            AddInclude(x => x.Type);
            AddInclude(x => x.Actions);

        }
        //public RequestSpec(RequestParams param)
        //        : base(x =>
        //            (string.IsNullOrEmpty(param.Search) || x.Employee.DisplayName.ToLower().Contains(param.Search)) &&
        //            (!param.EmpId.HasValue || x.Employee.Id == param.EmpId) &&
        //            (!string.IsNullOrEmpty(param.Status) || x.Status == param.Status) &&
        //            (!param.RequestId.HasValue || x.Id == param.RequestId))
        //{
        //    AddInclude(x => x.Employee);
        //    AddInclude(x => x.Employee.Department);

        //}
        public RequestSpec(RequestSpecParams param) :base()
        {
            this.param = param;
            parameterExpression = Expression.Parameter(typeof(Request), "x"); // Store the 'x' parameter

            // Your existing filtering criteria
            Criteria = BuildFilterExpression();
        }
        private Expression<Func<Request, bool>> BuildFilterExpression()
        {
            // Access 'x' using the parameterExpression
            var conditions = new List<Expression>();
            if (param.RequestId.HasValue)
            {
                var idCondition = Expression.Equal(Expression.Property(parameterExpression, "Id"), Expression.Constant(param.RequestId));
                conditions.Add(idCondition);
            }
            if (param.EmpId.HasValue)
            {
                var codeCondition = Expression.Equal(Expression.Property(Expression.Property(parameterExpression,"Employee"), "Id"), Expression.Constant(param.EmpId));
                conditions.Add(codeCondition);
            }
            if (!string.IsNullOrEmpty(param.Status))
            {
                var statusCondition = Expression.Equal(Expression.Property(parameterExpression, "CurrentState"), Expression.Constant(param.Status));
                conditions.Add(statusCondition);
            }

            if (!string.IsNullOrEmpty(param.EmpIds))
            {
                var empIdsArray = param.EmpIds.Split(',');
                if (empIdsArray.Any())
                {
                    // The sequence is not empty, you can safely access its elements
                    foreach (var empId in empIdsArray)
                    {
                        int id = int.Parse(empId);
                        var empIdCondition = Expression.Equal(Expression.Property(Expression.Property(parameterExpression, "Employee"), "Id"), Expression.Constant(id));
                        conditions.Add(empIdCondition);
                    }
                }
            }
            if (!string.IsNullOrEmpty(param.RequestIds))
            {
                var reqIdsArray = param.RequestIds.Split(',');
                if (reqIdsArray.Any())
                {
                    // The sequence is not empty, you can safely access its elements
                    foreach (var reqId in reqIdsArray)
                    {
                        int id = int.Parse(reqId);
                        var reqIdCondition = Expression.Equal(Expression.Property(parameterExpression, "Id"), Expression.Constant(id));
                        conditions.Add(reqIdCondition);
                    }
                }
            }
            AddInclude(x => x.Employee);
            AddInclude(x => x.Employee.Department);
            AddInclude(x => x.Type);
            // Combine conditions using OrElse
            if (!conditions.Any())
            {
                return null;
            }
            var combinedConditions = conditions
                .Aggregate((accumulatedCondition, nextCondition) =>
                    Expression.OrElse(accumulatedCondition, nextCondition));

            // Create the final lambda expression
            var lambda = Expression.Lambda<Func<Request, bool>>(combinedConditions, parameterExpression);

            return lambda;
        }

    }
}
