﻿using Common.Exceptions;
using MDM.UI.Companies.Interfaces;
using Production.UI.Materials.Interfaces;
using Production.UI.Materials.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;

namespace Production.Materials.Commands
{
    public class GetAllMaterialCommand : BaseCommand<IEnumerable<Material>>
    {
        public GetAllMaterialCommand()
        {
        }
    }

    public class GetAllMaterialCommandHandler : BaseCommandHandler<GetAllMaterialCommand, IEnumerable<Material>>
    {
        private readonly IMaterialRepository materialRepository = null;
        private readonly IMaterialQueries materialQueries = null;

        private readonly ICompanyRepository companyRepository = null;
        private readonly ICompanyQueries companyQueries = null;

        public GetAllMaterialCommandHandler(IMaterialRepository materialRepository, IMaterialQueries materialQueries,
                                            ICompanyRepository companyRepository, ICompanyQueries companyQueries)
        {
            this.materialRepository = materialRepository;
            this.materialQueries = materialQueries;

            this.companyRepository = companyRepository;
            this.companyQueries = companyQueries;
        }

        public override async Task<IEnumerable<Material>> HandleCommand(GetAllMaterialCommand request, CancellationToken cancellationToken)
        {
            var company = await companyQueries.GetByUserIdAsync(request.LoginSession.Id);
            if (company == null)
            {
                throw new BusinessException("Common.NoPermission");
            }

            var lst = (await materialQueries.GetAllAsync(company?.Id));

            return lst;
        }
    }
}
