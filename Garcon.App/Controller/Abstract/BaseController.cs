using Garcon.Business.Repository.Interface;
using System;


namespace Garcon.App.Controller.Abstract
{
    public abstract class BaseController
    {
        protected readonly IUnitOfWork unitOfWork;

        public BaseController(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork), "Argument cannot be null.");
            }

            this.unitOfWork = unitOfWork;
        }
    }
}
