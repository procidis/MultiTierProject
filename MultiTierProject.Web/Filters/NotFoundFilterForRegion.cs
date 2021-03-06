﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MultiTierProject.Core.AutoMapper.DTOs;
using MultiTierProject.Integration.ClientServiceses.MultiTierProject;
using System.Linq;

namespace MultiTierProject.Web.Filters
{
    public class NotFoundFilterForRegion : IActionFilter
    {
        private readonly RegionClientService<RegionDto> _service;
        public NotFoundFilterForRegion(RegionClientService<RegionDto> service)
        {
            _service = service;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = (int)context.ActionArguments.Values.FirstOrDefault();
            var entity = _service.GetByIdAsync(id).Result;
            if (entity == null)
            {
                ErrorDto errorDto = new ErrorDto();
                errorDto.Errors.Add($"was not found in the database.");
                context.Result = new JsonResult(errorDto);
            }
            else
            {
                context.HttpContext.Items.Add("entity", entity);
            }
        }
    }
}
