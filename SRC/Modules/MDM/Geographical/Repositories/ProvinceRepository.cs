﻿using Common.Models;
using DAL;
using MDM.UI.Geographical.Interfaces;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDM.Geographical.Repositories
{
    public class ProvinceRepository : BaseRepository, IProvinceRepository
    {
        public async Task<int> Add(Province province)
        {
            var cmd = QueriesCreatingHelper.CreateQueryInsert(province);
            cmd += ";SELECT LAST_INSERT_ID();";
            return (await DALHelper.ExecuteQuery<int>(cmd, dbTransaction: DbTransaction, connection: DbConnection)).First();
        }

        public async Task<int> Delete(Province province)
        {
            var cmd = $@"UPDATE `province`
                         SET
                         `is_deleted` = 1,
                         `modified_by` = {province.ModifiedBy},
                         `modified_date` = '{province.ModifiedDate.Value.ToString("yyyyMMddHHmmss")}'
                         WHERE `id` = {province.Id}";
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }

        public async Task<int> Update(Province province)
        {
            var cmd = QueriesCreatingHelper.CreateQueryUpdate(province);
            var rs = await DALHelper.Execute(cmd, dbTransaction: DbTransaction, connection: DbConnection);
            return rs == 0 ? -1 : 0;
        }
    }
}
