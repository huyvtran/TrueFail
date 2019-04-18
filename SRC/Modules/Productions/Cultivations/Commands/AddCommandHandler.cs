﻿using Common;
using Common.Exceptions;
using DAL;
using MDM.UI.Storages.Enumerations;
using MDM.UI.Storages.Interfaces;
using Productions.UI.Cultivations.Interfaces;
using Productions.UI.Methods.Models;
using Productions.UI.Plots;
using Productions.UI.Plots.Models;
using Productions.UI.Seeds;
using Productions.UI.Seeds.Models;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Productions.Cultivations.Commands
{
    public class AddCommandHandler : BaseCommandHandler<AddCommand, int>
    {
        private readonly ICultivationRepository cultivationRepository = null;
        private readonly ICultivationQueries cultivationQueries = null;
        private readonly IStorageQueries storageQueries = null;

        public AddCommandHandler(ICultivationRepository cultivationRepository,
            ICultivationQueries cultivationQueries,
            IStorageQueries storageQueries)
        {
            this.cultivationRepository = cultivationRepository;
            this.cultivationQueries = cultivationQueries;
            this.storageQueries = storageQueries;
        }

        public override async Task<int> HandleCommand(AddCommand request, CancellationToken cancellationToken)
        {
            if (request.Cultivation == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            if (request.Cultivation.PlotId > 0)
            {
                var plot = await WebHelper.HttpGet<Plot>(GlobalConfiguration.APIGateWayURI,
                    $"{PlotUrl.ApiGet}?id={request.Cultivation.PlotId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (plot == null) { throw new BusinessException("Plot.NotExisted"); }
            }
            else { throw new BusinessException("Plot.NotExisted"); }

            if (request.Cultivation.SeedId > 0)
            {
                var seed = await WebHelper.HttpGet<Seed>(GlobalConfiguration.APIGateWayURI,
                    $"{SeedUrl.ApiGet}?id={request.Cultivation.SeedId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (seed == null) { throw new BusinessException("Seed.NotExisted"); }
            }
            else { throw new BusinessException("Seed.NotExisted"); }

            if ((request.Cultivation.MethodId ?? 0) > 0)
            {
                var method = await WebHelper.HttpGet<Method>(GlobalConfiguration.APIGateWayURI,
                    $"{SeedUrl.ApiGet}?id={request.Cultivation.SeedId}",
                    request.LoginSession.AccessToken, request.LoginSession.LanguageCode);

                if (method == null) { throw new BusinessException("Method.NotExisted"); }
            }
            else if (request.Cultivation.MethodId != null)
            {
                throw new BusinessException("Method.NotExisted");
            }

            var rs = -1;
            using (var conn = DALHelper.GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        cultivationRepository.JoinTransaction(conn, trans);
                        cultivationQueries.JoinTransaction(conn, trans);

                        request.Cultivation.Code = await storageQueries.GenarateCodeAsync(StorageKeys.CultivationCode);
                        request.Cultivation = CreateBuild(request.Cultivation, request.LoginSession);
                        var cultivationId = await cultivationRepository.Add(request.Cultivation);

                        rs = 0;
                    }
                    finally
                    {
                        if (rs == 0)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            try
                            {
                                trans.Rollback();
                            }
                            catch { }
                        }
                    }
                }
            }

            return rs;
        }
    }
}
