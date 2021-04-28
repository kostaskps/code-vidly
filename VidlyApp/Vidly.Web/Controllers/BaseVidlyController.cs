using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vidly.Web.Contracts;

namespace Vidly.Web.Controllers
{
    public abstract class BaseVidlyController : Controller
    {
        private readonly IProvideUnitOfWork _unitOfWork;

        public BaseVidlyController(IProvideUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected IProvideUnitOfWork UnitOfWork { get { return _unitOfWork; } }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
