using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities.Employees;

namespace Core.Specifications.EmployeeSpec
{
    public class EmployeeWithFilterSpec : BaseSpecification<Employee>
    {
        private ParameterExpression parameterExpression;
        private readonly EmployeeSpecParams param;
        public EmployeeWithFilterSpec() : base()
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
            AddEnumValue(x => x.Status);

        }

        public EmployeeWithFilterSpec(int id, EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
            // AddInclude(x => x.Department);
        }

        public EmployeeWithFilterSpec(EmployeeSpecParams param,int Id) : base(x =>
            (string.IsNullOrEmpty(param.Status) || x.Status == param.Status)
            && (string.IsNullOrEmpty(param.EmployeeNature) || x.EmployeeNature == param.EmployeeNature)
            && (string.IsNullOrEmpty(param.Role) || x.TeamRole == param.Role))
            //&& (string.IsNullOrEmpty(param.DepartmentIds) || param.DepartmentIds.Split(',').Any(ele => ele == x.Department.Id.ToString())))
        {
            var parameterExpression = Expression.Parameter(typeof(Employee), "x");
            if(!string.IsNullOrEmpty(param.DepartmentIds))
            {
                var departmentIds = param.DepartmentIds.Split(',');
                var conditions = new List<Expression>();
                foreach (var departmentId in departmentIds)
                {
                    int id = int.Parse(departmentId);
                    var condition = Expression.Equal(Expression.Property
                                        (Expression.Property(parameterExpression, "Department"), "Id")
                                        , Expression.Constant(id));
                    conditions.Add(condition);
                }

                var combinedConditons = conditions.Aggregate((acc, nxt) =>
                                                    Expression.OrElse(acc, nxt));
                if(combinedConditons != null)
                {
                    var lambda = Expression.Lambda<Func<Employee, bool>>(combinedConditons, parameterExpression);
                    Criteria = Expression.Lambda<Func<Employee, bool>>(
                            Expression.AndAlso(Criteria.Body, lambda.Body), Criteria.Parameters[0]
                        );
                }
                
            }

            //if (!string.IsNullOrEmpty(param.DepartmentId))
            //{
            //    string[] departmentIdsArray = param.DepartmentId.Split(',');
            //    int[] intArray = departmentIdsArray.Select(int.Parse).ToArray();
            //    // Create a new condition for DepartmentIds
            //    Expression<Func<Employee, bool>> departmentCondition = x =>
            //        intArray.Any(ele => ele == x.DivisionId);

            //    // Combine the new condition with the existing criteria
            //    var combinedExpression = Expression.AndAlso(base.Criteria.Body, departmentCondition.Body);

            //    // Create a new expression
            //    var lambda = Expression.Lambda<Func<Employee, bool>>(combinedExpression, base.Criteria.Parameters[0]);

            //    // Assign the new expression to the Criteria property
            //    base.Criteria = lambda;
            //}

            //if (!string.IsNullOrEmpty(param.DepartmentId))
            //{
            //    var departmentIds = param.DepartmentId.Split(',');
            //    Expression<Func<Employee, bool>> combinedCondition = null;
            //    var employeeParameter = Expression.Parameter(typeof(Employee),"x");
            //    Expression finalCondition = Expression.Constant(false);
            //    foreach (var departmentId in departmentIds)
            //    {
            //        var propertyExpression = Expression.Property(employeeParameter, "DivisionId");
            //        var condtion = Expression.Equal(propertyExpression, Expression.Constant(int.Parse(departmentId)));
            //        //combinedCondition = combinedCondition == null ? condtion : Expression.OrElse(combinedCondition, condtion);
            //        //if (combinedCondition != null)
            //        //    combinedCondition = Expression.Lambda<Func<Employee, bool>>(
            //        //    Expression.AndAlso(combinedCondition.Body, condtion),
            //        //    combinedCondition.Parameters[0]);
            //        //else
            //        //{ combinedCondition = Expression.OrElse(finalCondition, condtion); }
            //        finalCondition = Expression.OrElse(finalCondition, condtion);
            //    }
            //    var lambda = Expression.Lambda<Func<Employee, bool>>(
            //                finalCondition,
            //                employeeParameter);
            //    //base.Criteria = Expression.Lambda(Expression.AndAlso(base.Criteria.Body, lambda.Body), base.Criteria.Parameters[0]);
            //    base.Criteria = Expression.Lambda<Func<Employee, bool>>(
            //                Expression.AndAlso(base.Criteria.Body, lambda.Body),
            //                base.Criteria.Parameters[0]);
            //    //if (combinedCondition != null)
            //    //{
            //    //    //base.Criteria = Expression.AndAlso(base.Criteria, combinedCondition);
            //    //    base.Criteria = Expression.Lambda<Func<Employee, bool>>(
            //    //            Expression.AndAlso(base.Criteria.Body, combinedCondition),
            //    //            base.Criteria.Parameters[0]);
            //    //}
            //}
            //if (!string.IsNullOrEmpty(param.DepartmentId))
            //{

            //    var departmentIdsArray = param.DepartmentId.Split(',');

            //    var departmentIdParameter = Expression.Parameter(typeof(int), "departmentId");
            //    var idParameter = Expression.Parameter(typeof(int), "id");
            //    Expression<Func<Employee, bool>> IdCondition = null;
            //    foreach (var departmentId in departmentIdsArray)
            //    {
            //        int id = int.Parse(departmentId);
            //        Expression<Func<Employee,bool>> idConditionCondition = x => x.DepartmentId == id;
            //        //base.Criteria = Expression.Lambda<Func<Employee, bool>>(
            //        //    Expression.AndAlso(
            //        //        base.Criteria.Body, idConditionCondition.Body),
            //        //    base.Criteria.Parameters);
            //        MethodInfo containsMethod = typeof(System.Linq.Enumerable)
            //                                    .GetMethods()
            //                                    .Where(m => m.Name == "Contains")
            //                                    .First(m => m.GetParameters().Length == 2)
            //                                    .MakeGenericMethod(typeof(int));
            //        base.Criteria = Expression.Lambda<Func<Employee, bool>>(
            //                                    Expression.AndAlso(
            //                                        Expression.Call(
            //                                                Expression.Constant(departmentIdParameter),
            //                                                containsMethod,
            //                                                new[] { Expression.Constant(id) }),
            //                                            Expression.Equal(
            //                                                Expression.Constant(idConditionCondition),
            //                                                Expression.Constant(id))));
            //        //if (departmentIdCondition == null)
            //        //{
            //        //    departmentIdCondition = employee => employee.Department.Id == id;
            //        //}
            //        //else
            //        //{
            //        //    IdCondition = employee => employee.Department.Id == id;
            //        //    Func<Employee, bool> condition = employee => employee.Id == id;
            //        //    //departmentIdCondition = departmentIdCondition.or(employee => employee.Department.Id.ToString() == id);
            //        //    departmentIdCondition = Expression.OrElse(departmentIdCondition);
            //        //}
            //    }
            //    //foreach (var departmentId in departmentIdsArray)
            //    //{
            //    //    var id = departmentId;
            //    //    Expression<Func<Employee, bool>> condition = employee => employee.Department.Id.ToString() == id;

            //    //    if (departmentIdCondition == null)
            //    //    {
            //    //        departmentIdCondition = condition;
            //    //    }
            //    //    else
            //    //    {
            //    //        departmentIdCondition = Expression.Lambda<Func<Employee, bool>>(
            //    //            Expression.OrElse(departmentIdCondition.Body, condition.Body),
            //    //            departmentIdCondition.Parameters[0]
            //    //        );
            //    //    }
            //    //}

            //    //if (departmentIdCondition != null)
            //    //{
            //    //    base.Criteria = Expression.Lambda<Func<Employee, bool>>(
            //    //        Expression.AndAlso(base.Criteria.Body, departmentIdCondition.Body),
            //    //        base.Criteria.Parameters[0]
            //    //    );
            //    //}
            //}
            //if (!string.IsNullOrEmpty(param.DepartmentId))
            //{
            //    var departmentIdsArray = param.DepartmentId.Split(',');

            //    Expression<Func<Employee, bool>> departmentIdCondition = null;

            //    foreach (var departmentId in departmentIdsArray)
            //    {
            //        var id = departmentId;
            //        Expression<Func<Employee, bool>> condition = employee => employee.Department.Id.ToString() == id;

            //        if (departmentIdCondition == null)
            //        {
            //            departmentIdCondition = condition;
            //        }
            //        else
            //        {
            //            departmentIdCondition = Expression.Lambda<Func<Employee, bool>>(
            //                Expression.AndAlso(departmentIdCondition.Body, condition.Body),
            //                departmentIdCondition.Parameters[0]
            //            );
            //        }
            //    }

            //    if (departmentIdCondition != null)
            //    {
            //        base.Criteria = Expression.Lambda<Func<Employee, bool>>(
            //            Expression.AndAlso(base.Criteria.Body, departmentIdCondition.Body),
            //            base.Criteria.Parameters[0]
            //        );
            //    }
            //}
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // AddInclude(x => x.Team);
            // AddInclude(x => x.TeamRole);
        }
        public EmployeeWithFilterSpec(EmployeeSpecParams param) : base()
        {
            this.param = param;
            parameterExpression = Expression.Parameter(typeof(Employee), "x"); // Store the 'x' parameter

            // Your existing filtering criteria
            Criteria = BuildFilterExpression();
        }

        private Expression<Func<Employee, bool>> BuildFilterExpression()
        {
            // Access 'x' using the parameterExpression
            var conditions = new List<Expression>();
            if(param.Id.HasValue)
            {
                var idCondition = Expression.Equal(Expression.Property(parameterExpression, "Id"), Expression.Constant(param.Id));
                conditions.Add(idCondition);
            }
            if(!string.IsNullOrEmpty(param.Code))
            {
                var codeCondition = Expression.Equal(Expression.Property(parameterExpression, "EmployeeCode"), Expression.Constant(param.Code));
                conditions.Add(codeCondition);
            }
            if (!string.IsNullOrEmpty(param.Status))
            {
                var statusCondition = Expression.Equal(Expression.Property(parameterExpression, "Status"), Expression.Constant(param.Status));
                conditions.Add(statusCondition);
            }

            if (!string.IsNullOrEmpty(param.EmployeeNature))
            {
                var natureCondition = Expression.Equal(Expression.Property(parameterExpression, "EmployeeNature"), Expression.Constant(param.EmployeeNature));
                conditions.Add(natureCondition);
            }

            if (!string.IsNullOrEmpty(param.Role))
            {
                var roleCondition = Expression.Equal(Expression.Property(parameterExpression, "TeamRole"), Expression.Constant(param.Role));
                conditions.Add(roleCondition);
            }
            if(!string.IsNullOrEmpty(param.DivisionIds))
            {
                var divisionIdsArray = param.DivisionIds.Split(',');
                if (divisionIdsArray.Any())
                {
                    // The sequence is not empty, you can safely access its elements
                    foreach (var divisionId in divisionIdsArray)
                    {
                        int id = int.Parse(divisionId);
                        var divisionCondition = Expression.Equal(Expression.Property(Expression.Property( parameterExpression,"Division"), "Id"), Expression.Constant(id));
                        conditions.Add(divisionCondition);
                    }
                }
            }
            if (!string.IsNullOrEmpty(param.DepartmentIds))
            {
                var departmentIdsArray = param.DepartmentIds.Split(',');
                if (departmentIdsArray.Any())
                {
                    // The sequence is not empty, you can safely access its elements
                    foreach (var departmentId in departmentIdsArray)
                    {
                        int id = int.Parse(departmentId);
                        var departmentCondition = Expression.Equal(Expression.Property(Expression.Property(parameterExpression, "Department"), "Id"), Expression.Constant(id));
                        conditions.Add(departmentCondition);
                    }
                }
            }
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddInclude(x => x.Branch);
            AddInclude(x => x.Division);
            // Combine conditions using OrElse
            if (!conditions.Any())
            {
                return null;
            }
            var combinedConditions = conditions
                .Aggregate((accumulatedCondition, nextCondition) =>
                    Expression.OrElse(accumulatedCondition, nextCondition));

            // Create the final lambda expression
            var lambda = Expression.Lambda<Func<Employee, bool>>(combinedConditions, parameterExpression);

            return lambda;
        }
    }
    public class EmployeePersonalWithFilterSpec : BaseSpecification<EmployeePersonalInfo>
    {
        public EmployeePersonalWithFilterSpec(EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.Employee.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
        }

      
    }
    public class EmployeeExpWithFilterSpec : BaseSpecification<EmployeeExperienceInfo>
    {
        public EmployeeExpWithFilterSpec(EmployeeFindSpec param) : base(x =>
            (string.IsNullOrEmpty(param.EmpCode) || x.Employee.EmployeeCode == param.EmpCode)
            && (!param.Id.HasValue || x.Id == param.Id)
            && (!param.IdNotEqual.HasValue || x.Id != param.IdNotEqual))
        {
        }
    }
}