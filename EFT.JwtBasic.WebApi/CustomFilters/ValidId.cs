using EFT.JwtBasic.Business.Interfaces;
using EFT.JwtBasic.Entites.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFT.JwtBasic.WebApi.CustomFilters
{
    public class ValidId<TEntity> : IActionFilter where TEntity:class,ITable,new()
    {
        private readonly IGenericService<TEntity> genericService;
        public ValidId(IGenericService<TEntity> genericService)
        {
            this.genericService = genericService;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }        
        

        public void OnActionExecuting(ActionExecutingContext context)
        {
           var dictinoray=  context.ActionArguments.Where(x => x.Key == "id").FirstOrDefault();
           var checkedID = (int)dictinoray.Value;
           var entity = this.genericService.GetById(checkedID).Result;
            if (entity==null)
            {
                context.Result = new NotFoundObjectResult($"{checkedID}'s model is not found");
            }
        }
    }
}
