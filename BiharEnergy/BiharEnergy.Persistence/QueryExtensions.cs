using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BiharEnergy.Core.Domain.Inferface;
using BiharEnergy.Core.Enums;

namespace BiharEnergy.Persistence
{
    public static class QueryExtensions
    {
        public static IQueryable<T> ForDate<T>(this IQueryable<T> data, DateTime date) where T : IHaveDateFilter
        {
            return data.Where(si => si.Date.Date == date);
        }
        public static IQueryable<T> ForDateRange<T>(this IQueryable<T> data, DateTime fromDate, DateTime toDate) where T : IHaveDateFilter
        {
            return data.Where(si => si.Date.Date >= fromDate && si.Date.Date <= toDate);
        }
        public static IQueryable<T> ForDateLessThan<T>(this IQueryable<T> data, DateTime date) where T : IHaveDateFilter
        {
            return data.Where(tr => tr.Date.Date < date);
        }
        public static IQueryable<T> ForAccountingUnit<T>(this IQueryable<T> data, string accountingUnitId) where T : IHaveAccountingUnit
        {
            return data.Where(si => si.AccountingUnitId == accountingUnitId);
        }
        public static bool IfExistInDateRange<T>(this IQueryable<T> data, DateTime fromDate, DateTime toDate) where T : IHaveDateFilter
        {
            return data.Any(si => si.Date.Date >= fromDate && si.Date.Date <= toDate);
        }
        public static bool IfExistInDateRange<T>(this IEnumerable<T> data, DateTime fromDate, DateTime toDate) where T : IHaveDateFilter
        {
            return data.Any(si => si.Date.Date >= fromDate && si.Date.Date <= toDate);
        }

        public static IQueryable<T> ForInvoiceCategory<T>(this IQueryable<T> data, InvoiceCategory invoiceCategory) where T : IHaveInvoiceCategory
        {
            return data.Where(si => si.InvoiceCategory == invoiceCategory);
        }
        public static IQueryable<T> ForInvoiceCategories<T>(this IQueryable<T> data, Expression<Func<T, bool>> predicate) where T : IHaveInvoiceCategory
        {
            return data.Where(predicate);
        }
        public static IQueryable<T> IsActive<T>(this IQueryable<T> data) where T : IHaveActiveFilter
        {
            return data.Where(dt => dt.IsActive);
        }
    }
}

