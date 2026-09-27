using AutoMapper;
using TestTask.DAL.Entities;

namespace TestTask.BLL.Extensions;

public static class MappingExpressionExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreBaseAuditFields<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> expression,
        bool ignoreId = true, 
        bool ignoreAuditDates = true,
        bool ignoreIsDeleted = true)
        where TDestination : BaseEntity
    {
        if (ignoreId)
            expression.ForMember(dest => dest.Id, opt => opt.Ignore());

        if (ignoreAuditDates)
        {
            expression.ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            expression.ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }

        if (ignoreIsDeleted)
            expression.ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        return expression;
    }
}