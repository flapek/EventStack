using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventStack_API.Interfaces;
using EventStack_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventStack_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private IRepositoryFactory<Category> repository { get; set; }

        public CategoryController(IRepositoryFactory<Category> repository)
        {
            this.repository = repository;
        }

    }
}
