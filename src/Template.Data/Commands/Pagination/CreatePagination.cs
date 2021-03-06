﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Template.Business.Models;
using Template.Business.Models.Parameters;

namespace Template.Data.Commands.Pagination
{
    public class CreatePagination<TItem> : ICreatePagination<TItem>
    {
        private PaginationParameter _parameter;

        public ICreatePagination<TItem> With(PaginationParameter pagination)
        {
            _parameter = pagination;
            return this;
        }

        public IObservable<Pagination<TItem>> Execute(IQueryable<TItem> input)
        {
            return Observable.Return(BuildPage(input));
        }

        private Pagination<TItem> BuildPage(IQueryable<TItem> input)
        {
            return new Pagination<TItem>
            {
                Items = GetItems(input),
                PageIndex = _parameter.PageIndex,
                PageSize = _parameter.PageSize,
                TotalCount = input.Count()
            };
        }

        private IEnumerable<TItem> GetItems(IQueryable<TItem> input)
        {
            return input
                .Skip(_parameter.PageIndex * _parameter.PageSize)
                .Take(_parameter.PageSize)
                .ToList();
        }
    }
}
